using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ischool.Equip_Repair
{
    public enum FormMode
    {
        [Description("新增")]
        Add,
        [Description("修改")]
        Update
    }
    public class EnumDescription
    {
        public static string Get(Type type, string value)
        {
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }

            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // 無設定Description Attribute, 回傳Enum欄位名稱
            if (customAttribute == null || customAttribute.Length == 0)
            {
                return name;
            }

            // 回傳Description Attribute的設定
            return ((DescriptionAttribute)customAttribute[0]).Description;
        }
    }
}
