using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDKDataAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccess da = new DataAccess { ConnectionString = "Data Source=msdk.database.windows.net;Initial Catalog=MSDKLAB;Integrated Security=False;User ID=chinzilla;Password=********;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" };
            Student s = new Student() { FirstName = "Victor", LastName = "Chin", _dob = new DateTime(1975, 07, 20) };
            da.AddStudent(s);
        }
    }
}
