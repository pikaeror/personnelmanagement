using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class KeyValue<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public KeyValue()
        {

        }

        public KeyValue(K key, V value){
            Key = key;
            Value = value;
        }
    }
}
