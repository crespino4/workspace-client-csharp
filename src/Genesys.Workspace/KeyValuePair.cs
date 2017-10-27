using System;
namespace Genesys.Workspace
{
    public enum ValueType
    {
        STRING,
        INT,
        LIST
    }

    public class KeyValuePair
    {
        private String key;
        private Object value;
        private ValueType type;

        public KeyValuePair(String key, String value)
        {
            this.key = key;
            this.value = value;
            this.type = ValueType.STRING;
        }

        public KeyValuePair(String key, KeyValueCollection value)
        {
            this.key = key;
            this.value = value;
            this.type = ValueType.LIST;
        }

        public KeyValuePair(String key, int value)
        {
            this.key = key;
            this.value = value;
            this.type = ValueType.INT;
        }

        public String getKey()
        {
            return this.key;
        }

        public ValueType getValueType()
        {
            return this.type;
        }


        public String getStringValue()
        {
            return (this.type == ValueType.STRING) ? (String)this.value : null;
        }

        public int? getIntValue()
        {
            if (this.type == ValueType.INT)
                return (int)this.value;
            
            return null;
        }

        public KeyValueCollection getListValue()
        {
            return (this.type == ValueType.LIST) ? (KeyValueCollection)this.value : null;
        }

        public Object getValue()
        {
            return this.value;
        }
    }
}
