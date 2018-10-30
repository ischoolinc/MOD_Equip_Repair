using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campus.Import;
using Campus.DocumentValidator;
using Campus.Validator;
using FISCA.Data;
using K12.Data;

namespace Ischool.Equip_Repair
{
    class ImportPlaceEquipWizard : ImportWizard
    {
        private ImportOption _Option;
        private QueryHelper _qh = new QueryHelper();
        private UpdateHelper _up = new UpdateHelper();
        private Dictionary<string, string> _dicPlaceUIDByKey = new Dictionary<string, string>();
        private string _userAccount = FISCA.Authentication.DSAServices.UserAccount.Replace("'", "''");

        public ImportPlaceEquipWizard()
        {
            this.CustomValidate = (Rows, Messages) =>
            {
                CustomValidator(Rows, Messages);
            };
        }

        public override ImportAction GetSupportActions()
        {
            //新增或更新
            return ImportAction.InsertOrUpdate;
        }

        public override string GetValidateRule()
        {
            return Properties.Resources.EquipmentValidate;
        }

        /// <summary>
        /// 客製驗證規則
        /// </summary>
        /// <param name="Rows"></param>
        /// <param name="Messages"></param>
        private void CustomValidator(List<IRowStream> Rows, RowMessages Messages)
        {
            Dictionary<string, IRowStream> dicRowStreamByKey = new Dictionary<string, IRowStream>(); // 判斷資料是否重複

            // 資料檢查
            Rows.ForEach(x =>
            {
                // 規則1.位置名稱(階層1)欄位必填不可空白
                if (string.IsNullOrEmpty(x.GetValue("位置名稱(階層1)").Trim()))
                {
                    Messages[x.Position].MessageItems.Add(new MessageItem(Campus.Validator.ErrorType.Error, Campus.Validator.ValidatorType.Row, "位置名稱(階層1)欄位必填不可空白!"));
                }

                // 規則2.位置名稱(階層3)有資料，位置名稱(階層2)欄位必填不可空白
                if (string.IsNullOrEmpty(x.GetValue("位置名稱(階層2)").Trim()) && !string.IsNullOrEmpty(x.GetValue("位置名稱(階層3)").Trim()))
                {
                    Messages[x.Position].MessageItems.Add(new MessageItem(Campus.Validator.ErrorType.Error, Campus.Validator.ValidatorType.Row, "位置名稱(階層3)有資料，位置名稱(階層2)欄位必填不可空白!"));
                }

                // 規則3.位置名稱(階層1)_位置名稱(階層2)_位置名稱(階層3)_設施名稱 為KEY值不可重複
                string key = string.Format("{0}_{1}_{2}_{3}","" + x.GetValue("位置名稱(階層1)").Trim(),x.GetValue("位置名稱(階層2)").Trim(),x.GetValue("位置名稱(階層3)").Trim(),x.GetValue("設施名稱").Trim());
                
                if (dicRowStreamByKey.ContainsKey(key))
                {
                    Messages[x.Position].MessageItems.Add(new MessageItem(Campus.Validator.ErrorType.Error, Campus.Validator.ValidatorType.Row, "位置名稱(階層1)+位置名稱(階層2)+位置名稱(階層3)+設施名稱 : 為KEY值不可重複!"));
                }
                else
                {
                    dicRowStreamByKey.Add(key,x);
                }
            });
        }

        public override string Import(List<Campus.DocumentValidator.IRowStream> Rows)
        {
            Dictionary<string, List<EquipRecord>> dicEquipRecordByPlaceID = new Dictionary<string, List<EquipRecord>>();

            // 1   解析位置資料(將系統不存在的位置做新增並取回系統編號、 dicPlaceEquip)
            this.ParsePlaceData(Rows, dicEquipRecordByPlaceID);
            // 2   處理位置資料
            // 2.1 位置資料存入資料庫
            // 3   處理設備資料
            // 3.1 設備資料存入資料庫

            return "";
        }

        private void GetPlaceData()
        {
            this._dicPlaceUIDByKey.Clear();
            string sql = @"
SELECT
    place.uid
    , place.name
    , place.level
    , father.name AS father
    , grandfather.name AS grandfather
FROM
    $ischool.equip_repair.place AS place
    LEFT OUTER JOIN $ischool.equip_repair.place AS father
        ON father.uid = place.parent_id
    LEFT OUTER JOIN $ischool.equip_repair.place AS grandfather
        ON grndfather.uid = father.parent_id
";
            DataTable dt = this._qh.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                string key = "";
                switch("" + row["level"])
                {
                    case "0":
                        key = string.Format("{0}","" + row["name"]);
                        break;
                    case "1":
                        key = string.Format("{0}_{1}","" + row["father"],"" + row["name"]);
                        break;
                    case "2":
                        key = string.Format("{0}_{1}_{2}","" + row["grandfather"], "" + row["father"], "" + row["name"]);
                        break;
                }

                if (!this._dicPlaceUIDByKey.ContainsKey(key))
                {
                    this._dicPlaceUIDByKey.Add(key, "" + row["uid"]);
                }
            }
        }

        private void ParsePlaceData(List<IRowStream> Rows, Dictionary<string, List<EquipRecord>> dicEquipRecordByPlaceID)
        {
            #region 處理階層1
            {
                Dictionary<string, PlaceRecord> dicPlaceRecordByKey = new Dictionary<string, PlaceRecord>();
                List<string> listDataRow = new List<string>();

                // 取得位置資料
                GetPlaceData();

                #region DataRow資料整理
                foreach (IRowStream row in Rows)
                {
                    string key = row.GetValue("位置名稱(階層1)").Trim();

                    if (!dicPlaceRecordByKey.ContainsKey(key))
                    {
                        PlaceRecord place = new PlaceRecord();
                        place.Name = key;
                        place.Level = 0;
                        place.ParentID = null;
                        place.CreateTime = DateTime.Now;
                        place.CreateBy = this._userAccount;
                        dicPlaceRecordByKey.Add(key, place);
                    }
                }
                #endregion

                #region 需要新增的位置資料
                foreach (string key in dicPlaceRecordByKey.Keys)
                {
                    if (!this._dicPlaceUIDByKey.ContainsKey(key))
                    {
                        PlaceRecord place = dicPlaceRecordByKey[key];

                        string data = string.Format(@"
SELECT
    {0}::TEXT AS name
    , {1}::BIGINT AS level
    , {2}::BIGINT AS parent_id
    , '{3}'::TIMESTAMP AS create_time
    , '{4}'::TEXT AS created_by 
                    ", place.Name, place.Level, place.ParentID, place.CreateTime.ToString("yyyy/MM/dd"), place.CreateBy);

                        listDataRow.Add(data);
                    }
                }
                #endregion

                #region 新增至資料庫
                string sql = string.Format(@"
WITH data_row AS(
    {0}
) 
INSERT INTO $ischool.equip_repair.place(
    name
    , level
    , parent_id
    , create_time
    , created_by
)
SELECT
    name
    , level
    , parent_id
    , create_time
    , created_by
FROM
    data_row
                ", string.Join("UNION ALL",listDataRow));

                this._up.Execute(sql);
                #endregion

            }
            #endregion

            #region 處理階層2
            {
                Dictionary<string, PlaceRecord> dicPlaceRecordByKey = new Dictionary<string, PlaceRecord>();
                List<string> listDataRow = new List<string>();

                // 取得位置資料
                GetPlaceData();

                #region DataRow資料整理
                foreach (IRowStream row in Rows)
                {
                    string name = row.GetValue("位置名稱(階層2)").Trim();
                    if (!string.IsNullOrEmpty(name))
                    {
                        string key = string.Format("{0}_{1}", row.GetValue("位置名稱(階層1)").Trim(),name);

                        if (!dicPlaceRecordByKey.ContainsKey(key))
                        {
                            PlaceRecord place = new PlaceRecord();
                            place.Name = key;
                            place.Level = 0;
                            place.ParentID = null;
                            place.CreateTime = DateTime.Now;
                            place.CreateBy = this._userAccount;
                            dicPlaceRecordByKey.Add(key, place);
                        }
                    }
                }
                #endregion
            }
            #endregion
            // 1.2 處理階層2

            // 1.3 處理階層3

            {
                Dictionary<string, PlaceRecord> dicPlaceRecordByKey = new Dictionary<string, PlaceRecord>();
                foreach (IRowStream row in Rows)
                {
                    string place1 = row.GetValue("位置名稱(階層1)").Trim();
                    string place2 = row.GetValue("位置名稱(階層2)").Trim();
                    string place3 = row.GetValue("位置名稱(階層3)").Trim();

                    string place1Key = place1;
                    string place2Key = string.IsNullOrEmpty(place2) ? "" : string.Format("{0}_{1}",place1,place2);

                    if (!dicPlaceRecordByKey.ContainsKey(place1))
                    {

                    }
                }
            }
            

        }

        private void SavePlace(List<PlaceRecord> listInsertPlace, List<PlaceRecord> listUpdatePlace)
        {

        }

        public override void Prepare(ImportOption Option)
        {
            this._Option = Option;
        }
    }
}
