using System;

namespace PrefixTrie
{
    public class OwnListElementNotInListException : Exception
    {
        public OwnListElementNotInListException()
        : base("This element is not in the list")
        {
            
        }
    }
}