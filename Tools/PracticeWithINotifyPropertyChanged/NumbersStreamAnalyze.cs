using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWithINotifyPropertyChanged;

public delegate void Text(object sender, string someText);
public class NumbersStreamAnalyze
{
    public event Message? SomeMes;
    private readonly double somePercent = 0.1;
    private List<double> numbers = new List<double>();
    public NumbersStreamAnalyze() { }
    public NumbersStreamAnalyze(double somePercent)
    {
        this.somePercent = somePercent;
    }
    public void AddElement(int value)
    {
        if (numbers.Count == 0)
        {
            numbers.Add(value);
        }
        else
        {
            if (numbers[numbers.Count - 1]>value && 1.0-(value / numbers[numbers.Count-1])>somePercent) 
            {
                SomeMes?.Invoke(this, "Превышение");
            } else if (numbers[numbers.Count - 1] < value && 1.0-(numbers[numbers.Count - 1]/value) > somePercent)
                {
                    SomeMes?.Invoke(this, "Превышение");
                }
            numbers.Add(value);
        }
    }
}
