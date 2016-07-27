using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System.Linq;

namespace DAT_TEST
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void AddStudent_Should_Increase_Student_Count_By_One()
            {

            //Arrange
            DataAccess da = new DataAccess("Data Source=msdk.database.windows.net;Initial Catalog=MSDKLAB;Integrated Security=False;User ID=chinzilla;Password=Pa$$w0rd12345;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //Act
            int beforeAdding = da.GetStudents().Count;
            da.AddStudent(new Student { FirstName = "Dummy", LastName = "Dummy", _dob = new DateTime(1933, 10, 10) });
            int afterAdding = da.GetStudents().Count;
            //Assert
            Assert.AreEqual(beforeAdding + 1, afterAdding);
        }
        [TestMethod]
        public void DeleteStudent_Should_Remove_Student_From_List()
        {
            //Arrange
            DataAccess da = new DataAccess("Data Source=msdk.database.windows.net;Initial Catalog=MSDKLAB;Integrated Security=False;User ID=chinzilla;Password=Pa$$w0rd12345;Connect Timeout=15;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            int aStudentID = da.GetStudents().First<Student>().ID;
            //Act
            da.DeleteStudent(aStudentID);
            //Assert
            try
            {
                da.FindStudent(aStudentID);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Can't find student using id:"+aStudentID);
            }
        }
    }
}
