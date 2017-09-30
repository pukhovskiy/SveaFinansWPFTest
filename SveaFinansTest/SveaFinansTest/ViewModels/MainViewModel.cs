using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GalaSoft.MvvmLight.CommandWpf;
using SveaFinansTest.Common;
using SveaFinansTest.DataProviders;
using SveaFinansTest.Enums;
using SveaFinansTest.Helpers;
using SveaFinansTest.Models;
using SveaFinansTest.Services;

namespace SveaFinansTest.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<KeyValuePair<int, string>> _departments;
        private CollectionViewSource _validPersons;
        private bool _isRunning;
        private RelayCommand _longOperationCommand;

        internal ObservableCollection<Person> AllPersons { get; set; }

        //as we need to filter collection and select next record in datagrid from code
        //we use CollectionViewSource
        public CollectionViewSource ValidPersons
        {
            get { return _validPersons; }
            set
            {
                _validPersons = value;
                OnPropertyChanged();
            }
        }


        //there are many ways of providing enum values to combobox etc.
        //ObjectDataProvider, MarkupExtension, Behavior, View model
        //there are situations where we might need to add language, configuration, filter etc
        //thus providing values via vm will give us more flexibility 

        public ObservableCollection<KeyValuePair<int, string>> Departments
        {
            get { return _departments; }
            set
            {
                if (Equals(_departments, value)) return;
                _departments = value;
                OnPropertyChanged();
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (Equals(_isRunning, value)) return;
                _isRunning = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand LongOpertionCommand
        {
            get
            {
                return _longOperationCommand
                       ?? (_longOperationCommand = new RelayCommand(
                           async () =>
                           {
                               if (IsRunning)
                               {
                                   return;
                               }

                               IsRunning = true;
                               LongOpertionCommand.RaiseCanExecuteChanged();

                               await StartLongOperation();

                               IsRunning = false;
                               LongOpertionCommand.RaiseCanExecuteChanged();
                           },
                           () => !IsRunning));
            }
        }



        public MainViewModel()
        {
            Departments = new ObservableCollection<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>((int) Department.Dept1, Department.Dept1.GetDescription()),
                new KeyValuePair<int, string>((int) Department.Dept2, Department.Dept2.GetDescription()),
                new KeyValuePair<int, string>((int) Department.Dept3, Department.Dept3.GetDescription())
            };
        }

        public async Task Initialize()
        {
            await GetPersons();
            await ApplyFilter();
        }
        public async Task GetPersons()
        {
            AllPersons = await PersonsDataServices.GetAllPersons();
        }
        private Task ApplyFilter()
        {
            ValidPersons = new CollectionViewSource { Source = AllPersons };
            ValidPersons.View.Filter = PersonIdFilter;
            return Task.FromResult(true);
        }

        private static bool PersonIdFilter(object item)
        {
            var person = item as Person;
            return person?.Id % 2 == 0;
        }

        private async Task StartLongOperation()
        {
            await Task.Delay(3000);
            SelectNextRow();
        }

        private void SelectNextRow()
        {
            ValidPersons.View.MoveCurrentToNext();
        }

    }
}
