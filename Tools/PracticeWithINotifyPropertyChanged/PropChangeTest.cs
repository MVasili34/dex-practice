using System.ComponentModel;

namespace PracticeWithINotifyPropertyChanged;


/// <summary>
/// Реализовать интерфейс INotifyPropertyChanged на
/// произвольном классе, продемонстрировать его работу
/// </summary>
public class PropChangeTest : INotifyPropertyChanged
{
    private string dogName = string.Empty;
    private string dogAge = string.Empty;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public string DogName
    {
        get { return dogName; }
        set 
        {
            if (dogName != value)
             dogName = value;
             OnPropertyChanged(nameof(dogName));
        }
    }
}