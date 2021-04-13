using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_3Layer
{
    class CSDL_OOP
    {
        public delegate bool Compare(Student s1, Student s2);
        public static CSDL_OOP Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL_OOP();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static CSDL_OOP _Instance;
        public List<Student> getStudents(int classID, string name)
        {
            List<Student> ListStudent = cloneListStudent();
            ListStudent.Clear();
            foreach (DataRow dr in CSDL.Instance.DTSV.Rows)
            {
                if (classID == 0)
                {
                    if (name != "")
                    {
                        if (dr["Name"].ToString().Contains(name))
                            ListStudent.Add(loadStudent(dr));
                    }
                    else
                        ListStudent.Add(loadStudent(dr));
                }
                else
                {
                    if (name != "")
                    {
                        if (Convert.ToInt32(dr["ClassID"]) == classID && dr["Name"].ToString().Contains(name))
                            ListStudent.Add(loadStudent(dr));
                    }
                    else
                    {
                        if (Convert.ToInt32(dr["ClassID"]) == classID)
                            ListStudent.Add(loadStudent(dr));
                    }    
                }
            }
            return ListStudent;
        }
        private Student loadStudent(DataRow dr)
        {
            Student s = new Student();
            s.StudentID = dr["StudentID"].ToString();
            s.Name = dr["Name"].ToString();
            s.Gender = Convert.ToBoolean(dr["Gender"]);
            s.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            s.ClassID = Convert.ToInt32(dr["ClassID"]);
            return s;
        }
        private Class loadClass(DataRow dr)
        {
            Class c = new Class();
            c.ClassID = Convert.ToInt32(dr["ClassID"]);
            c.ClassName = dr["ClassName"].ToString();
            return c;
        }
        public bool setStudent(Student s)
        {
            List<Student> students = cloneListStudent();
            students.Add(s);
            syncCSDL(students);
            return true;
        }
        public Student getStudentByID(string studentID)
        {
            List<Student> ListStudent = cloneListStudent();
            Student s = new Student();
            int studentIndex = 0;
            foreach(var i in ListStudent)
            {
                if(ListStudent[studentIndex].StudentID == studentID)
                {
                    s = ListStudent[studentIndex];
                    break;
                }
                studentIndex++;
            }
            return s;
        }
        public bool setStudentByID(string studentID, Student s)
        {
            List<Student> students = cloneListStudent();
            int studentIndex = 0;
            foreach (var i in students)
            {
                if (students[studentIndex].StudentID == studentID)
                {
                    students[studentIndex] = s;
                    break;
                }
                studentIndex++;
            }
            syncCSDL(students);
            return true;
        }
        public bool deleteStudentByID(string studentID)
        {
            List<Student> students = cloneListStudent();
            foreach(var i in students)
            {
                if (i.StudentID == studentID)
                {
                    students.Remove(i);
                    break;
                }    
            }
            syncCSDL(students);
            return true;
        }
        public List<Student> sortListStudent(string property)
        {
            Compare cmp;
            List<Student> list = cloneListStudent();
            switch(property)
            {
                case "StudentID":
                    cmp = Student.cmpStudentID;
                    break;
                case "Name":
                    cmp = Student.cmpName;
                    break;
                case "DateOfBirth":
                    cmp = Student.cmpDateOfBirth;
                    break;
                case "Gender":
                    cmp = Student.cmpGender;
                    break;
                case "ClassID":
                    cmp = Student.cmpClassID;
                    break;
                default:
                    cmp = Student.cmpStudentID;
                    break;
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (cmp(list[i], list[j]))
                    {
                        Student temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }
        private void syncCSDL(List<Student> students)
        {
            CSDL.Instance.DTSV.Rows.Clear();
            foreach (var i in students)
            {
                DataRow Student = CSDL.Instance.DTSV.NewRow();
                Student["StudentID"] = i.StudentID;
                Student["Name"] = i.Name;
                Student["Gender"] = i.Gender;
                Student["DateOfBirth"] = i.DateOfBirth;
                Student["ClassID"] = i.ClassID;
                CSDL.Instance.DTSV.Rows.Add(Student);
            }
        }
        private List<Student> cloneListStudent()
        {
            List<Student> students = new List<Student>();
            foreach (DataRow row in CSDL.Instance.DTSV.Rows)
            {
                students.Add(loadStudent(row));
            }
            return students;
        }
        public bool isExist(string studentID)
        {
            List<Student> ListStudent = cloneListStudent();
            foreach(var i in ListStudent)
            {
                if (i.StudentID == studentID) return true;
            }
            return false;
        }
    }
}
