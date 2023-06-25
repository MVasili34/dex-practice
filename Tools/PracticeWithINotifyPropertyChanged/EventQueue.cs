using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWithINotifyPropertyChanged;

public delegate void Message(object sender, string someText);
public class EventQueue<T>
{
    public event Message? QueueMes;

    public Queue<T>? someQueue;
    private T? value;

    public EventQueue()
    {
        someQueue = new Queue<T>();
    }

    public EventQueue(Queue<T> values) 
    {
        someQueue = values;
    }
    public void AddInqueue(T item) 
    {
        if (someQueue?.Count >= 2)
        {
            QueueMes?.Invoke(this, "Очередь превысела два элемента");
        }
        else
            someQueue?.Enqueue(item);
    }

    public T GetOutqueue()
    {
        if (someQueue is not null)
        {
            if (!someQueue.TryDequeue(out value))
            {
                QueueMes?.Invoke(this, "Очередь пуста");
                return default(T)!;
            }
            else
                return value;
        }
        return default(T)!;
    }
}
