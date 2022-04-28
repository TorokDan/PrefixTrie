using System;

namespace PrefixTrie
{
    public class NoNodeInListException : Exception
    {
        public NoNodeInListException()
        : base("There is no element in the list.")
        {
            
        }
    }
}