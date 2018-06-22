using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WISETRACK.Datos.Auxiliar
{
    public class Utils
    {
        public static KeyValuePair<string, string> mensaje(string key, string value, bool reemplazarEspeciales = true)
        {
            if (!reemplazarEspeciales)
            {
                return new KeyValuePair<string, string>(key, value);
            }
            else
            {
                return new KeyValuePair<string, string>(key, value.Replace("\"", "").Replace("\\", "").Replace("'", ""));
            }
        }
    }
}