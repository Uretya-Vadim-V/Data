using System;

namespace Data
{
    public class Node<T1, T2, T3>
    {

        public T1 IdLog { get; set; }
        public T2 IdLogParent { get; set; }
        public T3 Comment { get; set; }
        public Node<T1, T2, T3> Next { get; set; }
        public Node<T1, T2, T3> Previous { get; set; }
        public int Level { get; set; }
        public Node(T1 idLog, T2 idLogParent, T3 comment)
        {
            if (idLog == null || idLogParent == null || comment == null)
            {
                throw new ArgumentNullException();
            }
            IdLog = idLog;
            IdLogParent = idLogParent;
            Comment = comment;
        }
        public override string ToString()
        {
            string indent = "";
            for (int i = 0; i < Level; i++)
                indent += "\t";
            return $"{indent}{IdLog}.{IdLogParent} {Comment}";
        }
    }
}