using Abp.Domain.Values;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace Shared.SeedWork
{
    [Serializable]
    public class LocalizedText : ValueObject
    {
        private ListDictionary _translations;
        private string _stringValueRaw = string.Empty;

        private LocalizedText()
        {
            _translations = new ListDictionary();
        }

        public LocalizedText(string json)
        {
            StringValue = json;
        }
        [JsonProperty]
        public string StringValue
        {
            get => _stringValueRaw;
            private set
            {
                try
                {
                    _translations = string.IsNullOrEmpty(value)
                        ? new ListDictionary()
                        : JsonConvert.DeserializeObject<ListDictionary>(value);
                    _stringValueRaw = value;
                }
                catch (Exception)
                {
                    _translations = new ListDictionary();
                }
            }
        }
        public string this[string lang]
        {
            get
            {
                var result = string.Empty;

                if (_translations.Contains(lang))
                    result = _translations[lang].ToString();

                return result;
            }
        }
        public string CurrentCultureText =>
           this[System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName];

        public static implicit operator string(LocalizedText value)
        {
            if (value == null)
                return null;

            return value.CurrentCultureText;
        }

        public int CompareTo(LocalizedText other)
        {
            if (other == null)
                return string.Compare(CurrentCultureText, "", StringComparison.Ordinal);
            return string.Compare(CurrentCultureText, other.CurrentCultureText, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return StringValue;
        }

        /// <summary>
        /// Get list of the languages used in the current translation string
        /// </summary>
        public List<string> Languages()
        {
            List<string> languages = new List<string>();
            foreach (var key in _translations.Keys)
            {
                languages.Add(key.ToString());
            }

            return languages;
        }
        /// <summary>
        /// Get list of the translations [language:translation]
        /// </summary>
        public Dictionary<string, string> Translations()
        {
            Dictionary<string, string> languages = new Dictionary<string, string>();
            foreach (DictionaryEntry de in _translations.Keys)
            {
                languages.Add(de.Key.ToString(), de.Value.ToString());
            }
            return languages;
        }

        // Added these overrides because of this issue
        // https://github.com/aspnetboilerplate/aspnetboilerplate/issues/4271
        public static bool operator ==(LocalizedText left, LocalizedText right)
        {
            if (Object.ReferenceEquals(left, null))
            {
                if (Object.ReferenceEquals(right, null))
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            return left.Equals(right);
        }

        public static bool operator !=(LocalizedText left, LocalizedText right)
        {
            return !(left == right);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as LocalizedText);
        }
        public bool Equals(LocalizedText text)
        {
            return text != null &&
                   text.StringValue == this.StringValue;
        }
        public override int GetHashCode()
        {
            var hashCode = -290354994;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_stringValueRaw);

            return hashCode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return StringValue;
        }
    }
}
