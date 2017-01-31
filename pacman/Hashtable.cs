using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman
{
    class Hashtable<TKey, TValue>
    {
        private LinkedList<object> insertionOrder = new LinkedList<object>();
        private LinkedList<Entry<TKey, TValue>>[] table;

        public int Count { get { return insertionOrder.Count; } }

        public TValue this[TKey aKey]
        {
            get { return Get(aKey); }
            set
            {
                Remove(aKey);
                Put(aKey, value);
            }
        }

        public Hashtable(int aSize)
        {
            table = new LinkedList<Entry<TKey, TValue>>[aSize];
            for (int i = 0; i < aSize; i++)
            {
                table[i] = new LinkedList<Entry<TKey, TValue>>();
            }
        }
        
        private int HashIndex(TKey aKey)
        {
            int hashCode = aKey.GetHashCode();
            hashCode = hashCode % table.Length;
            return (hashCode < 0) ? -hashCode : hashCode;
        }
        
        public TValue Get(TKey aKey)
        {
            int hashIndex = HashIndex(aKey);

            if (table[hashIndex].Contains(new Entry<TKey, TValue>(aKey, default(TValue))) == true)
            {
                Entry<TKey, TValue> entry = table[hashIndex].Find(new Entry<TKey, TValue>(aKey, default(TValue))).Value;
                return entry.Value;
            }
            return default(TValue);
        }
        
        public void Put(TKey aKey, TValue aValue)
        {
            int hashIndex = HashIndex(aKey);

            if (table[hashIndex].Contains(new Entry<TKey, TValue>(aKey, default(TValue))) == false)
            {
                table[hashIndex].AddLast(new Entry<TKey, TValue>(aKey, aValue));
                insertionOrder.AddLast(aValue);
            }
            else
            {
                throw new Exception("Can't insert a key that already exists within the table");
            }
        }
        
        public void Remove(TKey aKey)
        {
            int hashIndex = HashIndex(aKey);

            if (table[hashIndex].Contains(new Entry<TKey, TValue>(aKey, default(TValue))) == true)
            {
                insertionOrder.Remove(Get(aKey));
                table[hashIndex].Remove(new Entry<TKey, TValue>((aKey), default(TValue)));
            }
        }
        
        public LinkedList<object> GetInsertionOrder()
        {
            return insertionOrder;
        }

        public bool ContainsKey (TKey aKey)
        {
            int hashIndex = HashIndex(aKey);
            if (table[hashIndex].Contains(new Entry<TKey, TValue>(aKey, default(TValue))) == true)
            {
                return true;
            }

            return false;
        }

        public bool TryGetValue(TKey aKey, out TValue aValue)
        {
            if (ContainsKey(aKey))
            {
                aValue = Get(aKey);
                return true;
            }

            aValue = default(TValue);
            return false;
        }

        public List<KeyValuePair<TKey, TValue>> ToList()
        {
            List<KeyValuePair<TKey, TValue>> list = new List<KeyValuePair<TKey, TValue>>();
            foreach (LinkedList<Entry<TKey, TValue>> bucket in table)
            {
                foreach (Entry<TKey, TValue> entry in bucket)
                {
                    list.Add(new KeyValuePair<TKey, TValue>(entry.Key, entry.Value));
                }
            }
            return list;
        }

    }
}
