using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.XPath;

namespace PrefixTrie
{
    public class Trie // TODO lehet generikussá kéne tennem, egyenlőre maradjon így.
    {
        public class Edge
        {
            private Node _from;
            private Node _to;

            public Node To => _to;
            public Edge(Node from, Node to)
            {
                _from = from;
                _to = to;
            }
        }
        public class Node
        {
            private string _value;
            private OwnList<Edge> _edges;

            public string Value => _value;
            public OwnList<Edge> Edges => _edges;

            public Node(string value)
            {
                _value = value;
                _edges = new OwnList<Edge>();
            }

            public void NewChild(Node newNode)
            {
                bool contains = false;
                foreach (var edge in _edges)
                    if (edge.To == newNode)
                        contains = true;
                if (!contains)
                    _edges.Add(new Edge(this, newNode));
            }
        }

        private Node _root;
        private char _endChar;

        public Trie(char endChar)
        {
            _root = new Node("");
            _endChar = endChar;
        }

        public delegate void Teszt(string value);
        public void NewWord(string[] newWords, Teszt _method = null)
        {
            for (int i = 0; i < newWords.Length; i++)
            {
                try
                {
                    NewWordRek(_root, newWords[i], string.Empty);
                    _method?.Invoke(newWords[i]);
                }
                catch (TrieWordAlredyInTheTrieException e) // TODO nem tudom h itt hogy itt hoyg kéne a már létező szavakat lekezelni, egyenlőre így lesez...
                {
                    _method?.Invoke(e.Message);
                }
            }
        }
        /// <summary>
        /// A megadott stringet feldarabolja a fa végkaraktereknél, és azokat hozzáadja a fához. Ha nincs benne az a karakter, akkor egy szónak veszi a stringet.
        /// </summary>
        /// <param name="newWord"></param>
        public void NewWord(string newWord, Teszt _method = null) => NewWord(newWord.Split(_endChar), _method);

        private void NewWordRek(Node node, string newWord, string createdWord)
        {
            if (newWord == string.Empty)
            {
                if (!Trie.ValueIsExistsUnder(node, createdWord+_endChar.ToString())) // Végigment a rekurzió, és nem volt még ilyen szó.
                {
                    Node endNode = new Node(createdWord + _endChar);
                    node.NewChild(endNode);
                }
                else //  Végigment a rekurzió, és volt már ilyen szó
                    throw new TrieWordAlredyInTheTrieException(createdWord);
            }
            else
            {
                createdWord += newWord[0];
                newWord = newWord.Remove(0, 1);
                if (ValueIsExistsUnder(node, createdWord ))
                    NewWordRek(NodeIsExistsUnder(node, createdWord), newWord, createdWord);
                else
                {
                    Node newNode = new Node(createdWord);
                    node.NewChild(newNode);
                    NewWordRek(newNode, newWord, createdWord);
                }
            }
        }

        private static bool ValueIsExistsUnder(Node head, string searchValue)  // TODO ezt a két metódust lehet át kéne gondolni
        {
            bool result = false;
            if (head.Edges.Count() != 0) // TODO null reference exception miatt, lehet nicns rá szükség
                foreach (var edge in head.Edges)  
                    if (edge.To.Value == searchValue)
                        result = true;
            return result;
        }
        private static Node NodeIsExistsUnder(Node head, string searchValue)
        {
            Node result = null;
            foreach (var edge in head.Edges)
                if (edge.To.Value == searchValue)
                    result = edge.To;
            return result;
        }
    }
}