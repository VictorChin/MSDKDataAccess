using System;

namespace MSDKDataAccess
{
    internal class Student : IComparable<Student>
    {
        public override string ToString()
        {
            return String.Format("Hej! I am {1}, {0} {1} and {2} years old", this.FirstName, this.LastName, this.Age.Days/365);
        }

        public int CompareTo(Student other)
        {
            if (this._dob.CompareTo(other._dob) == 0)
            { return this.ID.CompareTo(other.ID); }
            else { return this._dob.CompareTo(other._dob); }
           
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