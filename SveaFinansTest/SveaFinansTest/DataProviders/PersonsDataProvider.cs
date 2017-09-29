using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using SveaFinansTest.DTO;

namespace SveaFinansTest.DataProviders
{
    public static class PersonsDataProvider
    {
        public static Task<ObservableCollection<Person>> GetTestData()
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
