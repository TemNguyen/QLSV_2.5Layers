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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            setCBBClass();
            setCBBSort();
        }

        public void setCBBClass()
        {
            cbb_Class.Items.Add(new CBBItems
            {
                Text = "All",
                Value = 0
            });
            foreach(DataRow sv in CSDL.Instance.DTLSH.Rows)
            {
                cbb_Class.Items.Add(new CBBItems
                {
                    Text = sv["ClassName"].ToString(),
                    Value = Convert.ToInt32(sv["ClassID"])
                });
            }
            cbb_Class.SelectedIndex = 0;
        }
        public void setCBBSort()
        {
            int count = 0;
            foreach(DataColumn i in CSDL.Instance.DTSV.Columns)
            {
                cbb_Sort.Items.Add(new CBBItems
                {
                    Text = i.ColumnName,
                    Value = count++
                });
            }
            cbb_Sort.SelectedIndex = 0;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            refreshDatagrid();
        }
        private void cbb_Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshDatagrid();
        }
        private void btn_Show_Click(object sender, EventArgs e)
        {
            cbb_Class.SelectedIndex = 0;
            textBox1.Text = "";
            cbb_Sort.SelectedIndex = 0;
            refreshDatagrid();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            string studentID = "";
            int classID = 0;
            f2.Sender(studentID, classID);
            f2.reload = refreshDatagrid;
            f2.Show();
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            int classID = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ClassID"].Value);
            string studentID = dataGridView1.CurrentRow.Cells["StudentID"].Value.ToString(); 
            f2.Sender(studentID, classID);
            f2.reload = refreshDatagrid;
            f2.Show();
        }
        private void btn_Del_Click(object sender, EventArgs e)
        {
            DialogResult mess = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (mess)
            {
                case DialogResult.Yes:
                    string studentID = dataGridView1.CurrentRow.Cells["StudentID"].Value.ToString();
                    CSDL_OOP.Instance.deleteStudentByID(studentID);
                    refreshDatagrid();
                    break;
                case DialogResult.No:
                    break;
            }
        }
        private void btn_Sort_Click(object sender, EventArgs e)
        {
            string property = ((CBBItems)cbb_Sort.SelectedItem).Text;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = CSDL_OOP.Instance.sortListStudent(property);
        }
        public void refreshDatagrid()
        {
            int classID = ((CBBItems)cbb_Class.SelectedItem).Value;
            string name = textBox1.Text;
            dataGridView1.DataSource = null;  //Debug out of index
            dataGridView1.DataSource = CSDL_OOP.Instance.getStudents(classID, name);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
