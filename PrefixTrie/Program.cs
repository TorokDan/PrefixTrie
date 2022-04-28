using System;

namespace PrefixTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie elsoTeszt = new Trie('$');
            elsoTeszt.NewWord("alma$");
            elsoTeszt.NewWord("alom$");
            elsoTeszt.NewWord("korte$");
            ;
        }
    }
}