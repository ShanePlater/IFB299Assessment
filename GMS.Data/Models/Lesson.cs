using System;

namespace GMS.Data.Models
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
