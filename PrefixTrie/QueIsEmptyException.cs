using System;

namespace PrefixTrie
{
    public class QueIsEmptyException : Exception
    {
        public QueIsEmptyException()
        : base("Que is empty")
        {
            
        }
    }
}