using System;
using System.Threading.Channels;

namespace PrefixTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie elsoTeszt = new Trie('$');
            elsoTeszt.NewWord("Bela$alma$korte$cekla$alma$haziko$haz");
            elsoTeszt.Traverse(Console.WriteLine);
            Console.WriteLine(elsoTeszt.Search("alma"));
            Console.WriteLine(elsoTeszt.Search("asdfasdf"));
            
            ;
        }
    }
}