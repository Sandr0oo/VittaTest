using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
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

        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        private MoneyInflow _selectedMoneyInflow;
        public MoneyInflow SelectedMoneyInflow
        {
            get { return _selectedMoneyInflow; }
            set
            {
                _selectedMoneyInflow = value;
                OnPropertyChanged("SelectedMoneyInflow");
            }
        }

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
            Orders = new ObservableCollection<Order>(await _db.LoadAllOrders());
            MoneyInflow = new ObservableCollection<MoneyInflow>(await _db.LoadAllMoneyInflows());
        }

        public DelegateCommand AddTestDataCommand => _addTestDataCommand ?? new DelegateCommand(async (obj) =>
        {
            List<Order> ordersForAdd = new List<Order>()
            {
                new Order()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 1000,
                    AmountPayable = 100
                },
                new Order()
                {
                    Date = new DateTime(2022, 4, 14),
                    MoneyAmount = 5000,
                    AmountPayable = 0
                },
                new Order()
                {
                    Date = new DateTime(2022, 4, 5),
                    MoneyAmount = 300,
                    AmountPayable = 300
                }
            };
            List<MoneyInflow> moneyInflows = new List<MoneyInflow>()
            {
                new MoneyInflow()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 1000,
                    RestMoney = 1000

                },
                new MoneyInflow()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 100,
                    RestMoney = 100

                },
                new MoneyInflow()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 3000,
                    RestMoney = 3000
                },
                new MoneyInflow()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 2000,
                    RestMoney = 1000
                },
                new MoneyInflow()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 2000,
                    RestMoney = 2000
                },
                new MoneyInflow()
                {
                    Date = new DateTime(2022, 4, 12),
                    MoneyAmount = 100,
                    RestMoney = 0
                }
            };

            await _db.AddDataRange(ordersForAdd);
            await _db.AddDataRange(moneyInflows);

            Orders = new ObservableCollection<Order>(await _db.GetAllOrders());
            MoneyInflow = new ObservableCollection<MoneyInflow>(await _db.GetAllMoneyInflows());
        });

        public DelegateCommand PayCommand => _payCommand ?? new DelegateCommand(async (obj) =>
        {
            if(SelectedOrder != null && SelectedMoneyInflow != null)
            {
                if(SelectedMoneyInflow.RestMoney == 0)
                {
                    MessageBox.Show("Недостаточно денег для оплаты заказа");
                }


            }
            else
            {
                MessageBox.Show("Необходиом выбрать \"Заказ\" и \"Приход денег\"");
            }
        });

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}