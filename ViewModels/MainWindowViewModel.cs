using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VittaTest
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel() 
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}