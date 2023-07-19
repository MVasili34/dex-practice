
namespace PracticeWithINotifyPropertyChanged;

public delegate void Text(object sender, string someText);

/// <summary>
/// Реализовать класс анализирующий поток чисел, и если число
/// отличается более чем x -процентов выбрасывает событие
/// </summary>
public class NumbersStreamAnalyze
{
    private readonly double _percent = 0.1;
    private List<double> _numbers = new List<double>();
    public event Message? OnNumbersExcess;
    public NumbersStreamAnalyze() { }
    public NumbersStreamAnalyze(double _percent)
    {
        this._percent = _percent;
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
            if (_numbers[_numbers.Count - 1] > value && 1.0 - (value/_numbers[_numbers.Count - 1]) > _percent) 
            {
                OnNumbersExcess?.Invoke(this, "Превышение");
            } 
            else if (_numbers[_numbers.Count - 1] < value && 1.0 - (_numbers[_numbers.Count - 1]/value) > _percent)
                {
                    OnNumbersExcess?.Invoke(this, "Превышение");
                }
            _numbers.Add(value);
        }
    }
}
