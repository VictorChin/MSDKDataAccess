using System;

namespace MSDKDataAccess
{
    internal class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime _dob;
        public TimeSpan Age {
               get { return DateTime.Now - _dob; }
        }
        public int ID { get; set; }
    }
}