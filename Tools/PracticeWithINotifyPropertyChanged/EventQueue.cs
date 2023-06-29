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

    /// <summary>
    /// Добавить элемент в очередь
    /// </summary>
    /// <param name="item">Добавляемый элемент</param>
    public void AddInqueue(T item) 
    {
        if (someQueue?.Count >= 2)
        {
            QueueMes?.Invoke(this, "Очередь превысела два элемента");
        }
        else
            someQueue?.Enqueue(item);
    }

    /// <summary>
    /// Получить элемент из очереди
    /// </summary>
    /// <returns>Элемент очереди</returns>
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
