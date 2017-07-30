using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JNCReaderUWP.ViewModel
{
    public class KeyValueViewModel
    {
        public KeyValueViewModel(string key, object value)
        {
            _key = key;
            _value = value;
        }

        private string _key;
        private object _value;

        public string Key { get { return _key; } }
        public string Value { get
            {
                if (_value is List<string> || _value is string[] || _value is IEnumerable<string>)
                    return string.Join("\n", _value);
                else if (_value is string)
                    return (string)_value;
                else if (_value == null)
                    return "N/A";
                else return _value.ToString();
            }
        }
    }
}
