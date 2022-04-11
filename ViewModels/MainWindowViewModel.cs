using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using VittaTest.Models;
using VittaTest.Services;

namespace VittaTest
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private DbRequests _db;

        private DelegateCommand _addTestDataCommand;
        private DelegateCommand _payCommand;

        public Task Init { get; private set; }

        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged("Orders");
            }
        }
        private ObservableCollection<MoneyInflow> _moneyInflow;
        public ObservableCollection<MoneyInflow> MoneyInflow
        {
            get { return _moneyInflow; }
            set
            {
                _moneyInflow = value;
                OnPropertyChanged("MoneyInflow");
            }
        }

        public MainWindowViewModel(DbRequests db)
        {
            _db = db;

            Init = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            Orders = new ObservableCollection<Order>(await _db.GetAllOrders());
            MoneyInflow = new ObservableCollection<MoneyInflow>(await _db.GetAllMoneyInflows());
        }

        public DelegateCommand AddTestDataCommand => _addTestDataCommand ?? new DelegateCommand(async (obj) =>
        {

        });

        public DelegateCommand PayCommand => _payCommand ?? new DelegateCommand(async (obj) =>
        {

        });

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}