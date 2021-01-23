using System.Collections.Generic;
using System;

public interface IPriorityQueue<T> {
    int Count { get; }
    void Enqueue(T item, float priority);
    T Dequeue();
}

public class QueueItem<T> : IComparable {
    public T Item { get; private set; }
    public float Priority { get; private set; }

    public QueueItem(T item, float priority) {
        Item = item;
        Priority = priority;
    }

    public int CompareTo(object obj) {
        return Priority.CompareTo((obj as QueueItem<T>).Priority);
    }
}

public class PriorityQueue<T> : IPriorityQueue<T> {
    private List<QueueItem<T>> _queue;

    public PriorityQueue() {
        _queue = new List<QueueItem<T>>();
    }

    public int Count { get { return _queue.Count; } }

    public void Enqueue(T item, float priority) {
        _queue.Add(new QueueItem<T>(item, priority));
        int c = _queue.Count - 1;
        while(c > 0) {
            int p = (c - 1) / 2;
            if(_queue[c].CompareTo(_queue[p]) >= 0) break;
            var temp = _queue[c];
            _queue[c] = _queue[p];
            _queue[p] = temp;
            c = p;
        }
    }

    public T Dequeue() {
        int last = Count - 1;
        var frontItem = _queue[0];
        _queue[0] = _queue[last];
        _queue.RemoveAt(last);
        --last;
        int peek = 0;
        while(true) {
            int current = peek * 2 + 1;
            if(current > last) break;
            int rc = current + 1;
            if(rc <= last && _queue[rc].CompareTo(_queue[current]) < 0) {
                current = rc;
            }
            if(_queue[peek].CompareTo(_queue[current]) <= 0) break;
            var temp = _queue[peek];
            _queue[peek] = _queue[current];
            _queue[current] = temp;
            peek = current;
        }
        return frontItem.Item;
    }
}

public class HeapPriorityQueue<T> : IPriorityQueue<T> {
    private List<QueueItem<T>> _queue;

    public HeapPriorityQueue() {
        _queue = new List<QueueItem<T>>();
    }

    public int Count {
        get { return _queue.Count; }
    }

    public void Enqueue(T item, float priority) {
        _queue.Add(new QueueItem<T>(item, priority));
        int ci = _queue.Count - 1;
        while(ci > 0) {
            int pi = (ci - 1) / 2;
            if(_queue[ci].CompareTo(_queue[pi]) >= 0)
                break;
            var tmp = _queue[ci];
            _queue[ci] = _queue[pi];
            _queue[pi] = tmp;
            ci = pi;
        }
    }
    public T Dequeue() {
        int li = _queue.Count - 1;
        var frontItem = _queue[0];
        _queue[0] = _queue[li];
        _queue.RemoveAt(li);

        --li;
        int pi = 0;
        while(true) {
            int ci = pi * 2 + 1;
            if(ci > li) break;
            int rc = ci + 1;
            if(rc <= li && _queue[rc].CompareTo(_queue[ci]) < 0)
                ci = rc;
            if(_queue[pi].CompareTo(_queue[ci]) <= 0) break;
            var tmp = _queue[pi]; _queue[pi] = _queue[ci]; _queue[ci] = tmp;
            pi = ci;
        }
        return frontItem.Item;
    }
}
