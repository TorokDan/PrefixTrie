using System;

namespace PrefixTrie
{
    public class TrieWordAlredyInTheTrieException : Exception
    {
        public TrieWordAlredyInTheTrieException(string word)
            : base($"\"{word}\" is alredy in the Trie")
        {

        }
    }
}