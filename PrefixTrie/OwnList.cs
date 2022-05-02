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

        public void Remove(T value)
        {
            if (this.Contains(value))
            {

                OwnListElement<T> current = _head;
                OwnListElement<T> before = null;

                while (current != null && !current.Equals(value))
                {
                    before = current;
                    current = current.Next;
                }

                before.Next = current.Next; // TODO null reference miatt egy vizsgálatot az elejére be kéne rakni.
            }
            else
                throw new OwnListElementNotInListException();
        }

        public bool Contains(T value)
        {
            OwnListElement<T> element = _head;

            while (element != null && !element.Value.Equals(value)) // itt lehet nem az értéket kell vizsgálni
                element = element.Next;
            return element != null;
        }

        public T Search(T valueToSearch) // Ez a logika nem biztos hógy jó. Mivel a Node-ok akkor egyenlőek (Equals), ha
        // a tartalmuk megegyezik.
        {
            OwnListElement<T> element = _head;

            while (element != null && element.Value.Equals(valueToSearch)) // itt lehet nem az értéket kell vizsgálni
                element = element.Next;

            return element == null ?  throw new OwnListElementNotInListException():  element.Value;
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