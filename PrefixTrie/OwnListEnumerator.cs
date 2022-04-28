using System;
using System.Collections;
using System.Collections.Generic;

namespace PrefixTrie
{
    public class OwnListEnumerator<T> : IEnumerator<T>
    {
        private OwnListElement<T> _head;
        private OwnListElement<T> _current;

        public OwnListEnumerator(OwnListElement<T> head)
        {
            _head = head;
            _current = new OwnListElement<T>();
            _current.Next = _head;
        }
        
        public bool MoveNext()
        {
            if (_current == null)
                return false;
            _current = _current.Next;
            return _current != null;
        }

        public void Reset()
        {
            _current = new OwnListElement<T>();
            _current.Next = _head;
        }

        public T Current => _current.Value;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }
    }
}