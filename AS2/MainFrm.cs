using AS2.DataSet1TableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AS2
{
    public partial class MainFrm : MetroFramework.Forms.MetroForm
    {
        public int loggedIn { get; set; }

        public int UserID { get; set; }
        public MainFrm()
        {
            InitializeComponent();
            loggedIn = 0;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.AttendanceREcordsTBL' table. You can move, or remove it, as needed.
            // this.attendanceREcordsTBLTableAdapter.Fill(this.dataSet1.AttendanceREcordsTBL);
            // TODO: This line of code loads data into the 'dataSet1.ClassesTBL' table. You can move, or remove it, as needed.


        }

        private void MainFrm_Activated(object sender, EventArgs e)
        {
            if(loggedIn == 0)
            {
                //open login Form
                LoginFrm newLogin = new LoginFrm();
                newLogin.ShowDialog();

                if (newLogin.loginFlag == false)
                {
                    Close();
                }
                else
                {
                    UserID = newLogin.UserID;
                    statLblUser.Text = UserID.ToString();
                    loggedIn = 1;

                    //filter the add class to loggedin user only
                    this.classesTBLTableAdapter.Fill(this.dataSet1.ClassesTBL);
                    classesTBLBindingSource.Filter = "UserID = '" + UserID.ToString() + "'";
                }

            }
           
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            FrmAddClass Addclass = new FrmAddClass();
            Addclass.UserID = UserID;
            Addclass.ShowDialog();
        }
        
        private void metroButton4_Click(object sender, EventArgs e)
        {
            StudentsFrm students = new StudentsFrm();
            students.ClassName = metroComboBox1.Text;
            students.ClassID =(int)metroComboBox1.SelectedValue;
            students.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void metroButtonGet_Click(object sender, EventArgs e)
        {
            //check if record exists,  if yes load them for edit and if not create a record for each student and load for edit
            AttendanceREcordsTBLTableAdapter ada = new AttendanceREcordsTBLTableAdapter();
            DataTable dt = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

            if (dt.Rows.Count > 0)
            {
                //we have records so we can edit

                DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;


            }


            else
            {
                //create a record for each student
                //get the class students list
                StudentsTBLTableAdapter students_adapter = new StudentsTBLTableAdapter();

                DataTable dt_Students = students_adapter.GetDataByClassID((int)metroComboBox1.SelectedValue);

                foreach (DataRow row in dt_Students.Rows)
                {
                    //insert a new record for this student

                    ada.InsertQuery((int)row[0], (int)metroComboBox1.SelectedValue, dateTimePicker1.Text,"", row[1].ToString(), metroComboBox1.Text);


                
                }
                //record is avaliable, we can edit

                DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
                dataGridView1.DataSource = dt_new;

            }


                
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            //exist record check
            AttendanceREcordsTBLTableAdapter ada = new AttendanceREcordsTBLTableAdapter();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //for nul object editing if statement
                if (row.Cells[1].Value != null)
                {
                                   // [ ] id for cell no in  combo box
                    ada.UpdateQuery(row.Cells[1].Value.ToString(), row.Cells[0].Value.ToString(), (int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

                }

            }
            //save the edit
            DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
            dataGridView1.DataSource = dt_new;

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            //exist record check
            AttendanceREcordsTBLTableAdapter ada = new AttendanceREcordsTBLTableAdapter();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                //for nul object editing if statement
                if (row.Cells[1].Value != null)
                {                   //for clear empty string
                    ada.UpdateQuery("", row.Cells[0].Value.ToString(), (int)metroComboBox1.SelectedValue, dateTimePicker1.Text);

                }

            }
            //save the edit
            DataTable dt_new = ada.GetDataBy((int)metroComboBox1.SelectedValue, dateTimePicker1.Text);
            dataGridView1.DataSource = dt_new;

        }

      



        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            //RegisterFrm reg = new RegisterFrm();
            RegisterFrm registerFrm = new RegisterFrm();
            registerFrm.ShowDialog();
        }
    }
}
