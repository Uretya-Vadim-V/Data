using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public class Tree<T1, T2, T3> : IEnumerable<Node<T1, T2, T3>>
    {
        public T2 Root { get; set; }
        public int Count { get { return count; } }
        int count;
        public List<Node<T1, T2, T3>> Residue = new();
        Node<T1, T2, T3> head = null;
        Node<T1, T2, T3> tail = null;
        public Tree(T2 root)
        {
            Root = root;
        }
        public void Add(T1 idLog, T2 idLogParent, T3 comment)
        {
            if (idLog == null || idLogParent == null || comment == null)
            {
                throw new ArgumentNullException();
            }
            Node<T1, T2, T3> buffer = null;
            Node<T1, T2, T3> tag = null;
            Node<T1, T2, T3> node = new(idLog, idLogParent, comment);
            if (head == null)
                if (Equals(node.IdLogParent, Root))
                {
                    head = node;
                    head.Level = 0;
                    tail = node;
                    count++;
                }
                else
                {
                    Residue.Add(node);
                }
            else
            {
                tag = tail;
                tail = head;
                if (Equals(node.IdLogParent, Root))
                {
                    while (tail != null)
                    {
                        if (Equals(tail.IdLogParent, node.IdLogParent))
                        {
                            if (Compare(tail.Comment, node.Comment) <= 0)
                            {
                                break;
                            }
                        }
                        tail = tail.Next;
                    }
                    if (tail == null)
                    {
                        tail = tag;
                        tail.Next = node;
                        node.Previous = tail;
                        tail = node;
                        tail.Level = 0;
                    }
                    else
                    {
                        if (tail == head)
                        {
                            buffer = head;
                            head = node;
                            head.Level = 0;
                            head.Next = buffer;
                            buffer.Previous = head;
                        }
                        else
                        {
                            buffer = tail.Previous;
                            buffer.Next = node;
                            node.Previous = buffer;
                            tail.Previous = node;
                            node.Next = tail;
                            node.Level = 0;
                        }
                        tail = tag;
                    }
                    count++;
                }
                else
                {
                    while (tail != null)
                    {
                        if (Equals(tail.IdLog, node.IdLogParent))
                        {
                            buffer = tail;
                            tail = tail.Next;
                            while (tail != null && tail.Level > buffer.Level)
                            {
                                if (Equals(tail.IdLogParent, node.IdLogParent))
                                {
                                    if (Compare(tail.Comment, node.Comment) <= 0)
                                    {
                                        break;
                                    }
                                }
                                tail = tail.Next;
                            }
                            break;
                        }
                        tail = tail.Next;
                    }
                    if (buffer != null)
                    {
                        node.Level = buffer.Level + 1;
                        if (tail == null)
                        {
                            tail = tag;
                            tail.Next = node;
                            node.Previous = tail;
                            tail = node;
                        }
                        else
                        {
                            if (tail.Previous != null)
                            {
                                buffer = tail.Previous;
                                buffer.Next = node;
                                node.Previous = buffer;
                                tail.Previous = node;
                                node.Next = tail;
                            }
                            else
                            {
                                buffer = head.Next;
                                head.Next = node;
                                node.Previous = head;
                                buffer.Previous = node;
                                node.Next = buffer;

                            }
                            tail = tag;
                        }
                        count++;
                    }
                    else
                    {
                        tail = tag;
                        Residue.Add(node);
                    }
                }
            }
        }
        private void AddLast(Node<T1, T2, T3> node)
        {
            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }
        public void BuildTrees()
        {
            int length = -1;
            List<Node<T1, T2, T3>> listR = new();
            Residue = Residue.OrderBy(x => x.IdLog).ThenBy(x => x.Comment).ToList();
            while (Residue.Count > 0)
            {
                if (length == Residue.Count)
                {
                    AddLast(Residue[0]);
                    Residue.RemoveAt(0);
                }
                length = Residue.Count;
                listR.AddRange(Residue);
                Residue.Clear();
                foreach (var item in listR)
                {
                    Add(item.IdLog, item.IdLogParent, item.Comment);
                }
                listR.Clear();
            }
        }
        private int Compare(T3 comment1, T3 comment2)
        {
            if (Equals(comment1, comment2)) return 0;
            T3[] equals = new T3[2] { comment1, comment2 };
            equals = equals.OrderBy(x => x).ToArray();
            if (Equals(comment1, equals[0]))
                return 1;
            else return -1;
        }
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
        public IEnumerator<Node<T1, T2, T3>> GetEnumerator()
        {
            Node<T1, T2, T3> current = head;
            while (current != null)
            {
                yield return current;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }
}
