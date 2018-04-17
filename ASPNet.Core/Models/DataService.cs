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
            return new List<Student>();
        }

        public List<Teacher> getTeachers()
        {
            // code to use mysql connection and retrieve a list of all students
            return new List<Teacher>();
        }


    }
}
