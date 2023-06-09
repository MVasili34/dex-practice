using System.ComponentModel;

namespace PracticeWithINotifyPropertyChanged
{
    public class PropChangeTest : INotifyPropertyChanged
    {
        private string dogName = String.Empty;
        private string dogAge = String.Empty;

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
}