using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_B;
public class Node<TValue>
{
    public TValue Value { get; private set; }
    public Node<TValue> Next { get; set; }
    public Node(TValue value, Node<TValue> next)
    {
        Value = value;
        Next = next;
    }
}
