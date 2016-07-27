using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccess
    {
        public DataAccess(string connString)
        {
            this._conn = new SqlConnection(connString);
            this._conn.Open();
        }
        private SqlConnection _conn;
        public string ConnectionString { set { _conn = new SqlConnection(value);
                _conn.Open();
            } }
        public List<Student> GetStudents()
        {
            List<Student> allStudents = new List<Student>();//brand new list
            using (SqlCommand cmd = new SqlCommand("Select ID,FirstName,LastName,DOB from Student", _conn))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Student aStudent = new Student
                    {
                        ID = dr.GetInt32(0),
                        FirstName = dr.GetString(1),
                        LastName = dr.GetString(2),
                        _dob = dr.GetDateTime(3)
                    };
                    allStudents.Add(aStudent);
                 }
                dr.Close();
            }
                allStudents.Sort();
                return (allStudents);
        }
        public Student FindStudent(int ID) {
            using (SqlCommand cmd = new SqlCommand("Select ID,FirstName,LastName,DOB from Student Where ID = @ID", _conn))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value=ID;
                SqlDataReader dr =  cmd.ExecuteReader();
                if (dr.Read())
                { return new Student { ID = dr.GetInt32(0),
                    FirstName = dr.GetString(1), LastName = dr.GetString(2), _dob = dr.GetDateTime(3) };
                }
                else
                {
                    throw new Exception("Can't find student using id:" + ID);
                }
            }
                
        }

        public void DeleteStudent(int aStudentID)
        {
            using (SqlCommand cmd = new SqlCommand("delete from student where id =@ID", _conn))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = aStudentID;


                cmd.ExecuteNonQuery();
            }
        }
        public void AddStudent(Student s)
        {
            using(SqlCommand cmd = new SqlCommand("insert into student(firstname,lastname,dob) values(@FirstName,@LastName,@dob)", _conn))
            { cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 200).Value = s.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 200).Value = s.LastName;
                cmd.Parameters.Add("@DOB", SqlDbType.Date, 200).Value = s._dob;
              
                cmd.ExecuteNonQuery();
               

            }
            
        }

    }
}
