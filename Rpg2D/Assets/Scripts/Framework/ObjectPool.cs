using System.Collections.Generic;

public class ObjectPool<T>
{
    public Queue<T> pool = new Queue<T>();

    public T Get()
    {
        if (pool.Count > 0)
        {
            return pool.Dequeue();
        }

        return default;
    }

    public void Set(T t)
    {
        pool.Enqueue(t);
    }
}