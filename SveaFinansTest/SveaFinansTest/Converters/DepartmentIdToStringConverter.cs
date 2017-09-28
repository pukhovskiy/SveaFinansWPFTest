using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SveaFinansTest.Enums;
using SveaFinansTest.Helpers;

namespace SveaFinansTest.Converters
{
    public class DepartmentIdToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "--";

            int departmentId;
            var isId = int.TryParse(value.ToString(), out departmentId);
            if (!isId) return "--";
            var department = (Department)Enum.ToObject(typeof(Department), departmentId);
            var description = department.GetDescription();
            return description ?? department.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
