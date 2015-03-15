using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FrigorificosBle.Almacen.Util
{
    public class CommonsTools
    {
        
        public const string FechaFormato = "dd/MM/yyyy";

        public static string GetFixedLengthString(string input, int length)
        {
            input = input ?? string.Empty;
            input = input.Length > length ? input.Substring(0, length) : input;
            return string.Format("{0,-" + length + "}", input);
        }

        public static string DateTimeToString(Nullable<DateTime> fecha)
        {
            if (fecha != null)
            {
                return ((DateTime)fecha).ToString(FechaFormato, System.Globalization.CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        public static Nullable<DateTime> StringToDateTime(String fecha)
        {
            if (!String.IsNullOrEmpty( fecha ))
            {
                return DateTime.ParseExact(fecha, FechaFormato, System.Globalization.CultureInfo.InvariantCulture);
            }
            return null;
        }

        public static Nullable<DateTime> StringToDateTime(String fecha, String formato)
        {
            if (!String.IsNullOrEmpty(fecha))
            {
                return DateTime.ParseExact(fecha, formato, System.Globalization.CultureInfo.InvariantCulture);
            }
            return null;
        }

        public static Int32 StringToInt32(String number) 
        {
            // always use dot separator for doubles
            CultureInfo enUsCulture = CultureInfo.GetCultureInfo("en-US");
            return Convert.ToInt32(Convert.ToDouble(number, enUsCulture)); 
        }

    }
}
