using System;

namespace MSDKDataAccess
{
    internal class Student
    {
        public override string ToString()
        {
            return String.Format("Hej! I am {1}, {0} {1} and {2} years old", this.FirstName, this.LastName, this.Age.Days/365);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime _dob;
        public TimeSpan Age {
               get { return DateTime.Now - _dob; }
        }
        public int ID { get; set; }
    }
}