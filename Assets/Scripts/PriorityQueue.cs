using System;
using System.Collections;
using System.Collections.Generic;

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> list;
    public int Count { get { return list.Count; } }
    public readonly bool IsDescending = false;

    public PriorityQueue()
    {
        list = new List<T>();
    }

    public PriorityQueue(bool isdesc)
        : this()
    {
        IsDescending = isdesc;
    }

    public PriorityQueue(int capacity)
        : this(capacity, false)
    { }

    public PriorityQueue(IEnumerable<T> collection)
        : this(collection, false)
    { }

    public PriorityQueue(int capacity, bool isdesc)
    {
        list = new List<T>(capacity);
        IsDescending = isdesc;
    }

    public PriorityQueue(IEnumerable<T> collection, bool isdesc)
        : this()
    {
        IsDescending = isdesc;
        foreach (var item in collection)
            Enqueue(item);
    }


    public void Enqueue(T x)
    {
        list.Add(x);
        if(Count == 1) return;
        int i = Count - 2;

        while (i >= 0)
        {
            if (list[i].CompareTo(x) >= 0) break;

            list[i+1] = list[i];
            i--;
        }

        list[i+1] = x;
    }

    public T Dequeue()
    {
        T target = Peek();
        list.RemoveAt(0);
        return target;
    }

    public T Peek()
    {
        if (Count == 0) throw new InvalidOperationException("Queue is empty.");
        return list[0];
    }

    public void Clear()
    {
        list.Clear();
    }
}
