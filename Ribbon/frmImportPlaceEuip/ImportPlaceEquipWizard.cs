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
using FISCA.Presentation.Controls;

namespace Ischool.Equip_Repair
{
    class ImportPlaceEquipWizard : ImportWizard
    {
        private ImportOption _Option;
        private QueryHelper _qh = new QueryHelper();
        private UpdateHelper _up = new UpdateHelper();
        private Dictionary<string, string> _dicPlaceUIDByKey = new Dictionary<string, string>();
        private Dictionary<string, List<EquipRecord>> _dicEquipRecordByPlaceID = new Dictionary<string, List<EquipRecord>>();
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
            return Properties.Resources.EquipRepairValidate;
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
            // 1 解析位置資料(將系統不存在的位置做新增並取回系統編號、 dicPlaceEquip)
            ParsePlaceData(Rows);

            // 2 解析設施資料(將系統不存在的位置設施資料做新增、存在的做更新)
            ParsePlaceEquipData(Rows);

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
        ON grandfather.uid = father.parent_id
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

        /// <summary>
        /// 處理位置資料:不存在系統的位置資料新增
        /// </summary>
        private void ParsePlaceData(List<IRowStream> Rows)
        {
            for (int level = 1; level <= 3; level++) // 從位置階層1開始處理 ~ 位置階層3
            {
                Dictionary<string, PlaceRecord> dicPlaceRecordByKey = new Dictionary<string, PlaceRecord>();
                List<string> listDataRow = new List<string>();

                // 取得位置資料
                GetPlaceData();

                #region DataRow資料整理
                foreach (IRowStream row in Rows)
                {
                    string key = "";
                    string parentKey = "";
                    string targetPlace = row.GetValue(string.Format("位置名稱(階層{0})", level));

                    switch (level)
                    {
                        case 1:
                            key = row.GetValue("位置名稱(階層1)").Trim();
                            parentKey = "";
                            break;
                        case 2:
                            key = string.Format("{0}_{1}", row.GetValue("位置名稱(階層1)").Trim(), row.GetValue("位置名稱(階層2)").Trim());
                            parentKey = row.GetValue("位置名稱(階層1)").Trim();
                            break;
                        case 3:
                            key = string.Format("{0}_{1}_{2}", row.GetValue("位置名稱(階層1)").Trim(), row.GetValue("位置名稱(階層2)"), row.GetValue("位置名稱(階層3)").Trim());
                            parentKey = string.Format("{0}_{1}", row.GetValue("位置名稱(階層1)").Trim(), row.GetValue("位置名稱(階層2)").Trim());
                            break;
                    }

                    if (!dicPlaceRecordByKey.ContainsKey(key) && !string.IsNullOrEmpty(targetPlace))
                    {
                        PlaceRecord place = new PlaceRecord();
                        place.Name = targetPlace;
                        place.Level = level - 1;
                        //string colName = string.Format("位置名稱(階層{0})",level - 1);
                        //string parentID = this._dicPlaceUIDByKey[colName];
                        place.ParentID = level == 1 ? null : this._dicPlaceUIDByKey[parentKey];
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
    '{0}'::TEXT AS name
    , {1}::BIGINT AS level
    , {2}::BIGINT AS parent_id
    , '{3}'::TIMESTAMP AS create_time
    , '{4}'::TEXT AS created_by 
                    ", place.Name, place.Level, place.ParentID == null ? "null" : place.ParentID, place.CreateTime.ToString("yyyy/MM/dd"), place.CreateBy);

                        listDataRow.Add(data);
                    }
                }
                #endregion

                #region 新增至資料庫
                if (listDataRow.Count > 0)
                {
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
                ", string.Join("UNION ALL", listDataRow));

                    this._up.Execute(sql);
                }
                #endregion
            }

            // 取得位置資料
            GetPlaceData();
        }

        private void GetPlaceEquipData()
        {
            this._dicEquipRecordByPlaceID.Clear();
            string sql = @"
SELECT
    place.uid
    , place.name
    , place.level
    , father.name AS father
    , grandfather.name AS grandfather
	, equip.uid AS equip_uid
	, equip.name AS equip_name
	, equip.property_no
	, equip.create_time
	, equip.created_by
FROM
    $ischool.equip_repair.place AS place
    LEFT OUTER JOIN $ischool.equip_repair.place AS father
        ON father.uid = place.parent_id
    LEFT OUTER JOIN $ischool.equip_repair.place AS grandfather
        ON grandfather.uid = father.parent_id
	LEFT OUTER JOIN $ischool.equip_repair.equip AS equip
		ON equip.ref_place_id = place.uid
";
            DataTable dt = this._qh.Select(sql);

            // 資料整理
            foreach (DataRow row in dt.Rows)
            {
                string placeID = "" + row["uid"];
                string equipID = "" + row["equip_uid"];
                if (!this._dicEquipRecordByPlaceID.ContainsKey(placeID))
                {
                    this._dicEquipRecordByPlaceID.Add(placeID,new List<EquipRecord>());
                }
                if (!string.IsNullOrEmpty(equipID))
                {
                    EquipRecord data = new EquipRecord();
                    data.UID = equipID;
                    data.Name = "" + row["equip_name"];
                    data.PropertyNo = "" + row["property_no"];
                    data.RefPlaceID = placeID;
                    data.CreateTime = DateTime.Parse("" + row["create_time"]);
                    data.CreatedBy = this._userAccount;

                    this._dicEquipRecordByPlaceID[placeID].Add(data);
                }
            }
        }

        private void ParsePlaceEquipData(List<IRowStream> Rows)
        {
            List<EquipRecord> listUpdateEquipData = new List<EquipRecord>();
            List<EquipRecord> listInsertEquipData = new List<EquipRecord>();

            // 取得位置設施資料
            GetPlaceEquipData();

            foreach (IRowStream row in Rows)
            {
                // 取得Key值
                string place1 = row.GetValue("位置名稱(階層1)");
                string place2 = row.GetValue("位置名稱(階層2)");
                string place3 = row.GetValue("位置名稱(階層3)");
                string key = GetKey(place1, place2, place3);

                // 透過key值取得位置系統編號
                string placeID = this._dicPlaceUIDByKey[key];

                // 透過位置系統編號取的該位置所屬設施
                List<EquipRecord> listEquipRecord = this._dicEquipRecordByPlaceID[placeID];

                // 透過該位置所屬設施判斷資料: 新增 or 更新 // 設施名稱為Key值
                if (!string.IsNullOrEmpty(row.GetValue("設施名稱"))) // 如果設施名稱不是空字串
                {
                    var find = from data in listEquipRecord
                               where data.Name == row.GetValue("設施名稱")
                               select data;

                    if (find.Count() > 0) // 找到 更新
                    {
                        EquipRecord equip = null;
                        foreach (EquipRecord data in find)
                        {
                            equip = data;
                            equip.PropertyNo = row.GetValue("設施財產編號");
                        }

                        listUpdateEquipData.Add(equip);
                    }
                    else  // 沒找到 新增
                    {
                        EquipRecord equip = new EquipRecord();
                        equip.UID = null;
                        equip.Name = row.GetValue("設施名稱");
                        equip.PropertyNo = row.GetValue("設施財產編號");
                        equip.RefPlaceID = placeID;
                        equip.CreateTime = DateTime.Now;
                        equip.CreatedBy = this._userAccount;

                        listInsertEquipData.Add(equip);
                    }
                }
            }

            SaveEquipData(listUpdateEquipData, listInsertEquipData);
        }

        private string GetKey(string place1,string place2,string place3)
        {
            string key = "";
            if (!string.IsNullOrEmpty(place1) && string.IsNullOrEmpty(place2) && string.IsNullOrEmpty(place3)) // 情況1
            {
                key = string.Format("{0}",place1);
            }
            else if (!string.IsNullOrEmpty(place1) && !string.IsNullOrEmpty(place2) && string.IsNullOrEmpty(place3)) // 情況2
            {
                key = string.Format("{0}_{1}",place1,place2);
            }
            else if (!string.IsNullOrEmpty(place1) && !string.IsNullOrEmpty(place2) && !string.IsNullOrEmpty(place3)) // 情況3
            {
                key = string.Format("{0}_{1}_{2}",place1,place2,place3);
            }
            return key;
        }

        private void SaveEquipData(List<EquipRecord> listUpdateEquipData, List<EquipRecord> listInsertEquipData)
        {
            #region 新增
            {
                if (listInsertEquipData.Count > 0)
                {
                    List<string> listDataRow = new List<string>();
                    foreach (EquipRecord equip in listInsertEquipData)
                    {
                        string data = string.Format(@"
SELECT
    '{0}'::TEXT AS name
    , {1}::TEXT AS property_no
    , {2}::BIGINT AS ref_place_id
    , '{3}'::TIMESTAMP AS create_time
    , '{4}'::TEXT AS created_by
                ", equip.Name, equip.PropertyNo == "" ? "null" : string.Format("'{0}'", equip.PropertyNo), equip.RefPlaceID, equip.CreateTime.ToString("yyyy/MM/dd"), equip.CreatedBy);

                        listDataRow.Add(data);
                    }

                    #region SQL
                    string sql = string.Format(@"
WITH data_row AS(
    {0}
)
INSERT INTO $ischool.equip_repair.equip(
    name
    , property_no
    , ref_place_id
    , create_time
    , created_by
) 
SELECT
    name
    , property_no
    , ref_place_id
    , create_time
    , created_by
FROM
    data_row
                ", string.Join("UNION ALL", listDataRow)); 
                    #endregion

                    try
                    {
                        this._up.Execute(sql);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
            #endregion

            #region 更新
            {
                if (listUpdateEquipData.Count > 0)
                {
                    List<string> listDataRow = new List<string>();
                    foreach (EquipRecord equip in listUpdateEquipData)
                    {
                        if (equip != null)
                        {
                            string data = string.Format(@"
SELECT 
    {0}::BIGINT AS uid
    , {1}::TEXT AS property_no
                    ", equip.UID,equip.PropertyNo == "" ? "null" : string.Format("'{0}'", equip.PropertyNo));

                            listDataRow.Add(data);
                        }
                    }

                    #region SQL
                    string sql = string.Format(@"
WITH data_row AS(
    {0}
)
UPDATE $ischool.equip_repair.equip SET
    property_no = data_row.property_no
FROM
    data_row
WHERE
    $ischool.equip_repair.equip.uid = data_row.uid
                ", string.Join("UNION ALL", listDataRow)); 
                    #endregion

                    try
                    {
                        this._up.Execute(sql);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
            #endregion
            
        }

        public override void Prepare(ImportOption Option)
        {
            this._Option = Option;
        }
    }
}
