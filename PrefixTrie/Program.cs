using System;

namespace PrefixTrie
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie elsoTeszt = new Trie('$');
            elsoTeszt.NewWord("alma");
            elsoTeszt.NewWord("alom");
            try
            {
                elsoTeszt.NewWord("alma");
            }
            catch (TrieWordAlredyInTheTrieException e)
            {
                Console.WriteLine(e.Message);
            }
            elsoTeszt.NewWord("korte");
            ;
        }
    }
}