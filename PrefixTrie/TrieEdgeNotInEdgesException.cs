using System;

namespace PrefixTrie
{
    public class TrieEdgeNotInEdgesException : Exception
    {
        public TrieEdgeNotInEdgesException()
            : base("There is no edge whit these params.")
        {
            
        }
    }
}