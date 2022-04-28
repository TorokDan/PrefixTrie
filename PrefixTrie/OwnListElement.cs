namespace PrefixTrie
{
    public class OwnListElement<T>
    {
        private T _value;
        private OwnListElement<T> _next;

        public T Value => _value;
        public OwnListElement<T> Next
        {
            get => _next;
            set => _next = value;
        }

        public OwnListElement()
        {
            
        }
        public OwnListElement(T value)
        {
            _value = value;
        }

    }
}