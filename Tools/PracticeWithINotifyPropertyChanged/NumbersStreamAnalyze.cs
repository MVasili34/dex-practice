using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWithINotifyPropertyChanged;

public delegate void Text(object sender, string someText);

/// <summary>
/// Реализовать класс анализирующий поток чисел, и если число
/// отличается более чем x -процентов выбрасывает событие
/// </summary>
public class NumbersStreamAnalyze
{
    public event Message? OnNumbersExcess;
    private readonly double percent = 0.1;
    private List<double> numbers = new List<double>();
    public NumbersStreamAnalyze() { }
    public NumbersStreamAnalyze(double percent)
    {
        this.percent = percent;
    }

    /// <summary>
    /// Добавить число в поток чисел
    /// </summary>
    /// <param name="value">Входное число</param>
    public void AddElement(int value)
    {
        if (numbers.Count == 0)
        {
            numbers.Add(value);
        }
        else
        {
            if (numbers[numbers.Count - 1] > value && 1.0 - (value/numbers[numbers.Count - 1]) > percent) 
            {
                OnNumbersExcess?.Invoke(this, "Превышение");
            } 
            else if (numbers[numbers.Count - 1] < value && 1.0 - (numbers[numbers.Count - 1]/value) > percent)
                {
                    OnNumbersExcess?.Invoke(this, "Превышение");
                }
            numbers.Add(value);
        }
    }
}
