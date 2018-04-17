using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNet.Core.Models
{
    public class Lesson
    {
        public Teacher teacher;

        public Student student;

        public DateTime dateTime;

        public int Cost;

        public enum LessonStatus { Complete, Cancelled, Missed };

        public LessonStatus Status;
    }
}
