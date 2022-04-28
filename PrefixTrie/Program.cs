using System;

namespace PrefixTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie elsoTeszt = new Trie('$');
            elsoTeszt.NewWord("alma$korte$cekla$alma$haziko$haz", Console.WriteLine)
            ;
        }
    }
}