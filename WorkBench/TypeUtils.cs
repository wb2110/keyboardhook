using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WorkBench
{
    static class TypeUtils
    {
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }
        public static T[] Concat<T>(this T[] x, T[] y)
        {
            if (x == null)
                throw new ArgumentNullException("x");
            if (y == null)
                throw new ArgumentNullException("y");
            int oldLen = x.Length;
            Array.Resize<T>(ref x, x.Length + y.Length);
            Array.Copy(y, 0, x, oldLen, y.Length);
            return x;
        }
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null)
                { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null)
                { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }
        public static Dictionary<string, object> ToDictionary(this Object obj) {
            Type type = obj.GetType();
            var fields = type.GetFields();
            var dict = new Dictionary<String, Object>();
            foreach(var field in fields)
            {
                var value = field.GetValue(obj);
                dict.Add(field.Name, value);
               
            }
            return dict;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null)
            { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
        public static void SetPropValue<T>(this object obj, string property, string value)
        {
            if (obj == null)
            { return; }
            Type type = obj.GetType();
            type.GetProperty(property).SetValue(obj, value, null);
        }
        public static bool IsPositiveInteger(this string str)
        {
            int num;
            if (Int32.TryParse(str, out num))
            {
                return num > 0;
            }
            return false;
        }
        public static bool IsPositiveIntegerOrZero(this string str)
        {
            int num;
            if (Int32.TryParse(str, out num))
            {
                return num >= 0;
            }
            return false;
        }
        public static bool IsNegaitiveInteger(this string str)
        {
            int num;
            if (Int32.TryParse(str, out num))
            {
                return num < 0;
            }
            return false;
        }
        public static bool IsZero(this string str)
        {
            int num;
            if (Int32.TryParse(str, out num))
            {
                return num == 0;
            }
            return false;
        }
    }
}
