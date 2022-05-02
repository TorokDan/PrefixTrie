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

            public Node From => _from;
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

            public void RemoveChild(Node nodeToRemove)
            {
                _edges.Remove(this.SearchToNode(nodeToRemove));
            }

            public Edge SearchToNode(Node nodeToSearch)
            {
                Edge edge = null;
                foreach (Edge edgeItem in _edges)
                {
                    if (edgeItem.To == nodeToSearch)
                        edge = edgeItem;
                }

                return edge == null ? throw new TrieEdgeNotInEdgesException() : edge;
            }
            public bool Equals(Node node) => node._value == this._value;
        }

        private  Node _root;  // Lehet constantnak kéne csinálni
        private char _endChar;

        public Trie(char endChar)
        {
            _root = new Node("");
            _endChar = endChar;
        }

        public delegate void NewWordHandler(string value);
        public void NewWord(string[] newWords, NewWordHandler _method = null)
        {
            for (int i = 0; i < newWords.Length; i++)
            {
                try
                {
                    NewWordRek(_root, newWords[i].ToLower(), string.Empty);
                    _method?.Invoke(newWords[i].ToLower());
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
        public void NewWord(string newWord, NewWordHandler _method = null) => NewWord(newWord.Split(_endChar), _method);

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

        public void DeleteWord(string wordToDelete)
        {
            if (!this.Search(wordToDelete))
                throw new TrieWordNotInTheTrieException(wordToDelete);
            DeleteWordRek(_root, null, wordToDelete+_endChar, string.Empty);
        }

        private void DeleteWordRek(Node nodeActual, Edge edgeToNode, string wordToDelete, string createdWord)
        {
            if (wordToDelete.Length != 0)
            {
                createdWord += wordToDelete[0];
                wordToDelete = wordToDelete.Remove(0, 1);
            }
            foreach (Edge edge in nodeActual.Edges)
            {
                if (edge.To.Value == createdWord && wordToDelete.Length != 0)
                    DeleteWordRek(edge.To, edge, wordToDelete, createdWord);
                // if (nodeActual.Edges.Count() == 0)
                //     edgeToNode.From.RemoveChild(nodeActual);
            }
            if (nodeActual.Value == createdWord && nodeActual.Value[^1] == _endChar)
                edgeToNode.From.RemoveChild(nodeActual);
        }

        public delegate void TraverseHandler(string value);
        public void Traverse(TraverseHandler _method)
        {
            TraverseRek(_root, _method);
        }

        private void TraverseRek(Node node, TraverseHandler _method)
        {
            if (node.Value.Length != 0 && node.Value[^1] == _endChar)
                _method?.Invoke(node.Value.Remove(node.Value.Length-1, 1));
            foreach (Edge edge in node.Edges)
            {
                TraverseRek(edge.To, _method);
            }
        }

        public bool Search(string valueToSearch)
        {
            bool found = false;
            SearchRek(_root, valueToSearch, ref found);
            return found;
        }

        private void SearchRek(Node node, string valueToSearch, ref bool found)
        {
            if (node.Value == valueToSearch)
                found = true;
            else
                foreach (Edge edge in node.Edges)
                    SearchRek(edge.To, valueToSearch, ref found);
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