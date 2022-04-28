using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PrefixTrie
{
    public class OwnList<T> : IEnumerable<T>
    {
        private OwnListElement<T> _head;

        public void Add(T value)
        {
            OwnListElement<T> current = _head;
            OwnListElement<T> before = null;
            
            
            
            while (current != null)
            {
                before = current;
                current = current.Next;
            }

            OwnListElement<T> newElement = new OwnListElement<T>(value);
            if (before == null)
                _head = newElement;
            else
                before.Next = newElement;
        }

        public int Count()
        {
            int result = 0;
            OwnListElement<T> current = _head;
            while (current != null)
            {
                result++;
                current = current.Next;
            }

            return result;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new OwnListEnumerator<T>(_head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}