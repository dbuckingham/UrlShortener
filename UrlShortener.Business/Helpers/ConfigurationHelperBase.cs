using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.Helpers
{
    public abstract class ConfigurationHelperBase
    {
        #region Private Members

        private static NameValueCollection _appSettings = null;

        #endregion Private Members

        public static NameValueCollection AppSettings
        {
            get
            {
                if (_appSettings == null) _appSettings = ConfigurationManager.AppSettings;
                return _appSettings;
            }
            set
            {
                _appSettings = value;
            }
        }

        public static T GetValue<T>(string key, T defaultValue)
        {
            try
            {
                string value = AppSettings[key];

                if (value == null) return defaultValue;

                // TODO - Define a dictionary of types, and ITypeConverters.
                // If typeof(T) is a key in the dictionary, use ITypeConverter to 
                // convert the string and return the value.
                if (typeof(T) == typeof(Guid))
                {
                    return (T)(object)(new Guid(value));
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
