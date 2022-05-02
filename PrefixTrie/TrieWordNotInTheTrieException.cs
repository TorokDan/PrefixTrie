using System;

namespace PrefixTrie
{
    public class TrieWordNotInTheTrieException : Exception
    {
        public TrieWordNotInTheTrieException(string value)
            : base($"\"{value}\" is not exists in the trie")
        {
            
        }
    }
}