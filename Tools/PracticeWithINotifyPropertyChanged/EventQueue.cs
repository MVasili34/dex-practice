using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWithINotifyPropertyChanged;

public delegate void Message(object sender, string someText);

/// <summary>
/// Реализовать очередь, которая генерирует событие, когда кол - во
/// объектов в ней превышает n и событие, когда становится пустой
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventQueue<T>
{
    public event Message? OnQueueExcessMessage;

    public Queue<T>? queue;
    private T? value;

    public EventQueue()
    {
        queue = new Queue<T>();
    }

    public EventQueue(Queue<T> values) 
    {
        queue = values;
    }

    /// <summary>
    /// Добавить элемент в очередь
    /// </summary>
    /// <param name="item">Добавляемый элемент</param>
    public void AddInqueue(T item) 
    {
        if (queue?.Count >= 2)
        {
            OnQueueExcessMessage?.Invoke(this, "Очередь превысела два элемента");
        }
        else
            queue?.Enqueue(item);
    }

    /// <summary>
    /// Получить элемент из очереди
    /// </summary>
    /// <returns>Элемент очереди</returns>
    public T GetOutQueue()
    {
        if (queue is not null)
        {
            if (!queue.TryDequeue(out value))
            {
                OnQueueExcessMessage?.Invoke(this, "Очередь пуста");
                return default(T)!;
            }
            return value;
        }
        return default(T)!;
    }
}
