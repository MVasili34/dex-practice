
namespace PracticeWithINotifyPropertyChanged;

public delegate void Message(object sender, string someText);

/// <summary>
/// Реализовать очередь, которая генерирует событие, когда кол - во
/// объектов в ней превышает n и событие, когда становится пустой
/// </summary>
/// <typeparam name="T"></typeparam>
public class EventQueue<T>
{
    private T? _value;
    public event Message? OnQueueExcessMessage;

    public EventQueue() { }

    public EventQueue(Queue<T> queueExcess) 
    {
        QueueExcess = queueExcess;
    }

    public Queue<T>? QueueExcess { get; } = new();

    /// <summary>
    /// Добавить элемент в очередь
    /// </summary>
    /// <param name="item">Добавляемый элемент</param>
    public void AddInqueue(T item) 
    {
        if (QueueExcess?.Count >= 2)
        {
            OnQueueExcessMessage?.Invoke(this, "Очередь превысела два элемента");
        }
        else
            QueueExcess?.Enqueue(item);
    }

    /// <summary>
    /// Получить элемент из очереди
    /// </summary>
    /// <returns>Элемент очереди</returns>
    public T GetOutQueue()
    {
        if (QueueExcess is not null)
        {
            if (!QueueExcess.TryDequeue(out _value))
            {
                OnQueueExcessMessage?.Invoke(this, "Очередь пуста");
                return default!;
            }
            return _value;
        }
        return default!;
    }
}
