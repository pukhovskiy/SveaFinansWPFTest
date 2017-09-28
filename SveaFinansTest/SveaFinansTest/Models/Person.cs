using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SveaFinansTest.Common;

namespace SveaFinansTest.Models
{
    public class Person : BusinessObject
    {
        private int _id;
        private string _name;
        private DateTime _dateOfBirth;
        private string _address;
        private int? _departmentId;

        public int Id
        {
            get { return _id; }
            set
            {
                if (Equals(_id, value)) return;
                _id = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value)) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (Equals(_dateOfBirth, value)) return;
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                if (Equals(_address, value)) return;
                _address = value;
                OnPropertyChanged();
            }
        }

        public int? DepartmentId
        {
            get { return _departmentId; }
            set
            {
                if (Equals(_departmentId, value)) return;
                _departmentId = value;
                OnPropertyChanged();
            }
        }


    }
}
