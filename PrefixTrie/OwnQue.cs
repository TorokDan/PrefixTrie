namespace PrefixTrie
{
    public class OwnQue<T>
    {
        protected class Node
        {
            private T _value;
            private Node _next;
            
            public T Value => _value;
            public Node Next
            {
                get => _next;
                set => _next = value;
            }

            public Node(T _value)
            {
                this._value = _value;
            }
        }

        private Node _head;

        public void Add(T newItem)
        {
            if (_head == null)
                _head = new Node(newItem);
            else
            {
                Node tmp = _head;
                _head = new Node(newItem);
                _head.Next = tmp;
            }
        }

        public T Pop()
        {
            if (_head == null)
                throw new QueIsEmptyException();
            Node tmp = _head;
            _head = _head.Next;
            return tmp.Value;
        }

        public int Count()
        {
            int count = 0;
            Node seged = _head;
            while (seged != null)
            {
                count++;
                seged = seged.Next;
            }

            return count;
        }
    }
}