using System;

namespace PrefixTrie
{
    public class TrieWordAlredyInTheTrieException : Exception
    {
        public TrieWordAlredyInTheTrieException()
            : base("Word alredy in the Trie")
        {

        }
    }
}