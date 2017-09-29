using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SveaFinansTest.DataProviders;
using SveaFinansTest.Models;

namespace SveaFinansTest.Services
{
    public static class PersonsDataServices
    {
        private static Person MapToBusiness(DTO.Person person)
        {
            return new Person
            {
                Id = person.Id,
                Name = person.Name,
                DateOfBirth = person.DateOfBirth,
                Address = person.Address,
                DepartmentId = person.DepartmentId
            };
        }

        public static async Task<ObservableCollection<Person>> GetAllPersons()
        {
            var persons = await PersonsDataProvider.GetTestData();
            return new ObservableCollection<Person>(persons.Select(MapToBusiness));
        }
    }
}
