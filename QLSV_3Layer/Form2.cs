using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3Layer
{
    public partial class Form2 : Form
    {
        public delegate void MyDel(string studentID, int classID);
        public delegate void reloadData();
        public reloadData reload;
        public MyDel Sender;

        string studentID = "";
        int classID = 0;

        private void getDel(string _studentID, int _classID)
        {
            this.studentID = _studentID;
            this.classID = _classID;
        }
        public Form2()
        {
            Sender = new MyDel(getDel);
            InitializeComponent();
            setCbb_Class();
            rbtn_Male.Checked = true;
        }
        private void setCbb_Class()
        {  
            foreach (DataRow s in CSDL.Instance.DTLSH.Rows)
            {
                cbb_Class.Items.Add(new CBBItems
                {
                    Text = s["ClassName"].ToString(),
                    Value = Convert.ToInt32(s["ClassID"])
                });
            }
            cbb_Class.SelectedIndex = 0;
        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (studentID == "")
            {
                if (tb_StudentID.Text == "" || tb_Name.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đủ thông tin!", "Chú ý!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (CSDL_OOP.Instance.isExist(tb_StudentID.Text))
                {
                    MessageBox.Show("StudentID = " + tb_StudentID.Text + " đã tồn tại!", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }    
                CSDL_OOP.Instance.setStudent(getStudent());
            }         
            else
                CSDL_OOP.Instance.setStudentByID(studentID, getStudent());
            reload();
            this.Dispose();
        }
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            reload();
            this.Dispose();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (studentID != "")
            {
                tb_StudentID.Enabled = false;
                setStudent();
            }
        }
        private Student getStudent()
        {
            Student s = new Student(); 
            s.StudentID = tb_StudentID.Text;
            s.Name = tb_Name.Text;
            if (rbtn_Female.Checked) s.Gender = false;
            else s.Gender = true;
            s.DateOfBirth = Convert.ToDateTime(dateTimePicker1.Value);
            s.ClassID = ((CBBItems)cbb_Class.SelectedItem).Value;
            return s;
        }
        private void setStudent()
        {
            Student s = new Student();
            s = CSDL_OOP.Instance.getStudentByID(studentID);
            tb_StudentID.Text = s.StudentID;
            tb_Name.Text = s.Name;
            if (s.Gender == true) rbtn_Male.Checked = true;
            else rbtn_Female.Checked = true;
            dateTimePicker1.Value = s.DateOfBirth;
            cbb_Class.SelectedIndex = classID - 1;
        }
    }
}
