using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNet.Core.Models
{
    
    public class DataService
    {
        public DataService()
        {
            // Code to initialise mysql connection
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
                    Username = $"student {i}",
                    FirstName = $"student first {i}",
                    LastName = $"student last {i}",
                    Email = $"student{i}@abc.com"
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
                    Username = $"a{i}",
                    FirstName = $"aa{i}",
                    LastName = $"bb{i}",
                    Email = $"aa{i}@abc.com"
                };
                teachers.Add(teacher);
            }
            return teachers;
        }


    }
}
