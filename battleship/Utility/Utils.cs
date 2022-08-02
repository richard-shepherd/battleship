using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Utility
{
    /// <summary>
    /// Static utility functions.
    /// </summary>
    public class Utils
    {
        #region Public methods

        /// <summary>
        /// Converts the object passed in to a JSON string.
        /// </summary>
        public static string toJSON(object o)
        {
            if(o == null)
            {
                return "";
            }
            var settings = getJSONSerializerSettings();
            return JsonConvert.SerializeObject(o, Formatting.None, settings);
        }

        /// <summary>
        /// Returns an object of type T deserialized from the JSON string passed in.
        /// </summary>
        public static T fromJSON<T>(string json)
        {
            var settings = getJSONSerializerSettings();
            var deserializedObject = JsonConvert.DeserializeObject<T>(json, settings);
            if(deserializedObject == null)
            {
                throw new Exception($"Failed to deserialize {json} as type {typeof(T).Name}");
            }
            return deserializedObject;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Returns the settings to use when serializing objects to and from JSON.
        /// </summary>
        private static JsonSerializerSettings getJSONSerializerSettings()
        {
            if(m_jsonSerializerSettings == null)
            {
                m_jsonSerializerSettings = new JsonSerializerSettings();
                m_jsonSerializerSettings.Converters.Add(new StringEnumConverter());
            }
            return m_jsonSerializerSettings;
        }

        #endregion

        #region Private data

        // Controls how JSON is serialized and deserialized...
        private static JsonSerializerSettings m_jsonSerializerSettings = null;

        #endregion
    }
}