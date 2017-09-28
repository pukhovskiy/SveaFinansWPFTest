using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SveaFinansTest.Enums
{
    public enum Department
    {
        [Description("Department 1")]
        Dept1 = 1,
        [Description("Department 2")]
        Dept2 = 2,
        [Description("Department 3")]
        Dept3 = 3
    }
}
