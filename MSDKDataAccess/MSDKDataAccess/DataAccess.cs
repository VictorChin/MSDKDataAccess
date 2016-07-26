using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSDKDataAccess
{
    class DataAccess
    {
        public string ConnectionString { get; set; }
        internal List<Student> GetStudents()
        { return new List<Student>(); }
        internal Student FindStudent(int ID) {
            return new Student();
        }
        internal void AddStudent(Student s)
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            using(SqlCommand cmd = new SqlCommand("insert into student(firstname,lastname,dob) values(@FirstName,@LastName,@dob)", conn))
            { cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = s.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 200).Value = s.LastName;
                cmd.Parameters.Add("@DOB", SqlDbType.Date, 200).Value = s._dob;
                cmd.ExecuteNonQuery();

            }
            
        }

    }
}
