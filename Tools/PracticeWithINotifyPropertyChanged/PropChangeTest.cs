using System.ComponentModel;

namespace PracticeWithINotifyPropertyChanged;


/// <summary>
/// Реализовать интерфейс INotifyPropertyChanged на
/// произвольном классе, продемонстрировать его работу
/// </summary>
public class PropChangeTest : INotifyPropertyChanged
{
    private string _dogName = string.Empty;
    private string _dogAge = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public string DogName
    {
        get { return _dogName; }
        set 
        {
            if (_dogName != value)
             _dogName = value;
             OnPropertyChanged(nameof(_dogName));
        }
    }
}