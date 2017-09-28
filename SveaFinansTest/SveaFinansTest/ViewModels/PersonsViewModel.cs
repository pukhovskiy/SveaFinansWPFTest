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
using SveaFinansTest.Enums;
using SveaFinansTest.Helpers;
using SveaFinansTest.Models;

namespace SveaFinansTest.ViewModels
{
    public class PersonsViewModel : ViewModelBase
    {
        private ObservableCollection<KeyValuePair<int, string>> _departments;
        private Person _currentPerson;

        internal ObservableCollection<Person> AllPersons { get; set; }

        private CollectionViewSource myVar;

        public CollectionViewSource ValidPersons
        {
            get { return myVar; }
            set
            {
                myVar = value;
                OnPropertyChanged();
            }
        }

        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set
            {
                if (Equals(_currentPerson, value)) return;
                _currentPerson = value;
                OnPropertyChanged();

            }
        }



        //there are many ways of providing enum values to combobox etc.
        //ObjectDataProvider, MarkupExtension, behaviors, via viewmodels 
        //there are situations where you might need to add language, configuration, filters etc
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

        private bool _isRunning;

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

        private RelayCommand _longOperationCommand;

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



        public PersonsViewModel()
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

        public async Task GetPersons()
        {
            AllPersons = await GetTestData();
        }

        private async Task StartLongOperation()
        {
            await Task.Delay(5000);
            SelectNextRow();
        }

        private void SelectNextRow()
        {
            ValidPersons.View.MoveCurrentToNext();
        }

        private static Task<ObservableCollection<Person>> GetTestData()
        {
            var listOfAllPersons = new ObservableCollection<Person>();
            for (var i = 1; i <= 100; i++)
            {
                listOfAllPersons.Add(new Person { Id = i, Name = "Person " + i, DateOfBirth = DateTime.Today.AddDays(-i), Address = "Address " + i });
            }
            return Task.FromResult(listOfAllPersons);
        }

    }
}
