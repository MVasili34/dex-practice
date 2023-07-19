
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

    public EventQueue()
    {
        this.QueueExcess = new Queue<T>();
    }

    public EventQueue(Queue<T> QueueExcess) 
    {
        this.QueueExcess = QueueExcess;
    }

    public Queue<T>? QueueExcess { get; }

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
                return default(T)!;
            }
            return _value;
        }
        return default(T)!;
    }
}
