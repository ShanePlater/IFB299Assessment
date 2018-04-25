using System.Collections.Generic;
using GMS.Data.Models;
using MySql.Data.MySqlClient;

namespace GMS.Data
{
    
    public class DataService
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DataService()
        {
            // Code to initialise mysql connection
        }
        private void Initialize()
        {
            server = "localhost";
            database = "mikamusicstudio";
            uid = "root";
            password = "password";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        //MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                       // MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
               // MessageBox.Show(ex.Message);
                return false;
            }
        }

        public List<Student> getStudents()
        {
            // code to use mysql connection and retrieve a list of all students

            //Sample students
            var students = new List<Student>();
            for (char i = 'a'; i < 'k'; i++)
            {
                var student = new Student()
                {
                    FirstName = $"student_first {i}",
                    LastName = $"student_last {i}",
                    Email = $"student_{i}@abc.com"
                };
                students.Add(student);
            }

            return students;
        }

        public List<Teacher> getTeachers()
        {
            // code to use mysql connection and retrieve a list of all students

            //Sample teachers
            var teachers = new List<Teacher>();
            for (char i = 'a'; i < 'k'; i++)
            {
                var teacher = new Teacher()
                {
                    UserName = $"teacher_{i}",
                    FirstName = $"teacher_first {i}",
                    LastName = $"teacher_last {i}",
                    Email = $"teacher_{i}@abc.com"
                };
                teachers.Add(teacher);
            }
            return teachers;
        }


    }
}
