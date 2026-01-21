using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ClarenceTodoist
{
    public partial class CreateWin : Form
    {
        public CreateWin()
        {
            InitializeComponent();
        }

        #region ConfirmBtn
        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string taskname = TaskName.Text;
            if(taskname == "")
            {
                MessageBox.Show("Please Enter the task name");
                return;
            }

            MysqlVisit.ExNonQuery($"INSERT INTO task(isDone, name) VALUES(0, '{taskname}')");
            MysqlVisit.CloseMysql();

            TaskDU_EvHandler.UpdateTask(this, new DUEventArgs("ConfirmBtn_Click from CreatWin"));
            this.Close();
        }
        #endregion
    }
}
