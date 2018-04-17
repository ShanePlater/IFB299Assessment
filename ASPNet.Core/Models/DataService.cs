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
                    Username = $"student_{i}",
                    FirstName = $"student_first {i}",
                    LastName = $"student_last {i}",
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
                    Username = $"teacher_{i}",
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
