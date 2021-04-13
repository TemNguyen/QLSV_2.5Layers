using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_3Layer
{
    class Student
    {
        public string StudentID { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassID { get; set; }

        public static bool cmpStudentID(Student s1, Student s2)
        {
            if (String.Compare(s1.StudentID, s2.StudentID) > 0) return true;
            return false;
        }
        public static bool cmpName(Student s1, Student s2)
        {
            if (String.Compare(s1.Name, s2.Name) > 0) return true;
            return false;
        }
        public static bool cmpGender(Student s1, Student s2)
        {
            if (!s1.Gender && s2.Gender) return true;
            return false;
        }
        public static bool cmpDateOfBirth(Student s1, Student s2)
        {
            if (s1.DateOfBirth > s2.DateOfBirth) return true;
            return false;
        }
        public static bool cmpClassID(Student s1, Student s2)
        {
            if (s1.ClassID > s2.ClassID) return true;
            return false;
        }
    }
}
