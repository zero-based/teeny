using System;
using System.Collections.Generic;
using System.Linq;

namespace Teeny.Core.Scan.Attributes
{
    public static class AttributesHelper
    {
        public static T GetAttributeOfType<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memInfo = type.GetMember(enumValue.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0 ? (T)attributes[0] : null;
        }

        public static Dictionary<TEnum, TAttr> GetLookUpTable<TEnum, TAttr>() where TEnum : Enum where TAttr : Attribute
        {
            var table = new Dictionary<TEnum, TAttr>();
            var enums = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            foreach (var e in enums)
            {
                var attr = e.GetAttributeOfType<TAttr>();
                if (attr != null) table.Add(e, attr);
            }

            return table;
        }
    }
}