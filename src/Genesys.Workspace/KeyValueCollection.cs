using System;
using System.Collections.Generic;
using Genesys.Workspace.Model;

namespace Genesys.Workspace
{
    public class KeyValueCollection 
    {
        private Dictionary<string, KeyValuePair> data;

        public KeyValueCollection()
        {
            this.data = new Dictionary<string, KeyValuePair>();
        }

        public void addString(String key, String value)
        {
            this.data.Add(key, new KeyValuePair(key, value));
        }

        public void addInt(String key, int value)
        {
            this.data.Add(key, new KeyValuePair(key, value));
        }

        public void addList(String key, KeyValueCollection value)
        {
            this.data.Add(key, new KeyValuePair(key, value));
        }

        public String getString(String key)
        {
            KeyValuePair pair = this.data[key];
            if (pair == null || pair.getValueType() != ValueType.STRING || pair.getValue() == null)
            {
                return null;
            }

            return (String)pair.getValue();
        }

        public void getKeyValuePairs()
        {
                this.data.Values.GetEnumerator();
        }

        public IEnumerator<KeyValuePair> iterator()
        {
                return this.data.Values.GetEnumerator();
        }

        public int? getInt(String key)
        {
            KeyValuePair pair = this.data[key];
            if (pair == null || pair.getValueType() != ValueType.INT || pair.getValue() == null)
            {
                return null;
            }

            return (int)pair.getValue();
        }

        public KeyValueCollection getList(String key)
        {
            KeyValuePair pair = this.data[key];
            if (pair == null || pair.getValueType() != ValueType.LIST || pair.getValue() == null)
            {
                return null;
            }

            return (KeyValueCollection)pair.getValue();
        }

        public List<Kvpair> ToListKvpair()
        {
            return null;    
        }

        override public String ToString()
        {
            String str = "[";
            foreach (KeyValuePair pair in this.data.Values)
            {
                switch (pair.getValueType())
                {
                    case ValueType.STRING:
                        str += " STRING: " + pair.getKey() + "=" + pair.getStringValue() + "\n";
                        break;

                    case ValueType.INT:
                        str += " INT: " + pair.getKey() + "=" + pair.getIntValue() + "\n";
                        break;

                    case ValueType.LIST:
                        str += " LIST: " + pair.getKey() + "=" + pair.getListValue() + "\n";
                        break;
                }
            }

            str += "]";
            return str;
        }
    }
}
