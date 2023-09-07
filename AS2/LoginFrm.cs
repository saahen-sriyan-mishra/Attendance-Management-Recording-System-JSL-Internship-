using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AS2
{
    public partial class LoginFrm : Form
    {
        public bool loginFlag { get; set; } 
        public int UserID { get; set; }
        public LoginFrm()
        {
            InitializeComponent();
            loginFlag = false;
        }

        private void LoginFrm_Load(object sender, EventArgs e)
        {


        }

        private void metroButtonLogin_Click(object sender, EventArgs e)
        {
            user1DataSet1TableAdapters.UsersTableAdapter userAda = new DataSet1TableAdapters.UsersTableAdapter();
            DataTable dt = userAda.GetDataByUserAndPass(metroTextBoxUser.Text, metroTextBoxPass.Text);

            if(dt.Rows.Count > 0 ) 
            {
                //valid
                MessageBox.Show("login ok");
                UserID = int.Parse(dt.Rows[0]["UserID"].ToString());
                loginFlag = true;
            }

            else 
            {
                // not valid
                MessageBox.Show("access denied");
                loginFlag = false;
            }

            Close();
        }

        private void metroTextBoxUser_Click(object sender, EventArgs e)
        {

        }
    }
}
