using System;

namespace Pacman
{
    class Entry<TKey, TValue>
    {
        public TKey Key { get; private set; }
        public TValue Value { get; private set; }
        
        public Entry(TKey aKey, TValue aValue)
        {
            Key = aKey;
            Value = aValue;
        }
        
        public override bool Equals(object aObject)
        {
            Entry<TKey, TValue> keyValue = (Entry<TKey, TValue>)aObject;
            return Key.Equals(keyValue.Key);
        }
    }
}
