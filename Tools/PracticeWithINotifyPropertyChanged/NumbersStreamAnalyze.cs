
namespace PracticeWithINotifyPropertyChanged;

public delegate void Text(object sender, string someText);

/// <summary>
/// Реализовать класс анализирующий поток чисел, и если число
/// отличается более чем x -процентов выбрасывает событие
/// </summary>
public class NumbersStreamAnalyze
{
    private readonly double _percent = 0.1;
    private readonly List<double> _numbers = new();
    public event Message? OnNumbersExcess;
    public NumbersStreamAnalyze() { }
    public NumbersStreamAnalyze(double percent)
    {
        _percent = percent;
    }

    /// <summary>
    /// Добавить число в поток чисел
    /// </summary>
    /// <param name="value">Входное число</param>
    public void AddElement(int value)
    {
        if (_numbers.Count == 0)
        {
            _numbers.Add(value);
        }
        else
        {
            if (_numbers.Last() > value && 1.0 - (value/_numbers.Last()) > _percent) 
            {
                OnNumbersExcess?.Invoke(this, "Превышение");
            } 
            else if (_numbers.Last() < value && 1.0 - (_numbers.Last()/value) > _percent)
                {
                    OnNumbersExcess?.Invoke(this, "Превышение");
                }
            _numbers.Add(value);
        }
    }
}
