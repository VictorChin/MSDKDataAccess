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
            DataAccess da = new DataAccess { ConnectionString = "Data Source=msdk.database.windows.net;Initial Catalog=MSDKLAB;Integrated Security=False;User ID=chinzilla;Password=Pa$$w0rd12345;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" };
            #region dont look here
            //Student s = new Student() { FirstName = "Victor", LastName = "Chin", _dob = new DateTime(1975, 07, 20) };
            //da.AddStudent(s);
            //Console.WriteLine("Please enter student ID");
            //int input;

            //try
            //{
            //    if (int.TryParse(Console.ReadLine(), out input))
            //    {
            //        Student s = da.FindStudent(input);
            //        Console.WriteLine(s);
            //    }

            //}
            //catch (Exception)
            //{

            //    Console.WriteLine("Bad Student ID, please try again");
            //} 
            #endregion
            var allStudents = da.GetStudents();
            //var q = from s in allStudents
            //        orderby s.Age
            //        select s;
            foreach (var item in allStudents)
            {
                Console.WriteLine(item);
            }
            Console.ReadLine();
        }
    }
}
