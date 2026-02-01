using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ClarenceTodoist
{
    public partial class ReviseWin : Form
    {
        string TaskName;
        bool IsDown;
        int TaskID;
        public ReviseWin(int id, bool done)
        {
            TaskID = id;
            IsDown = done;
            InitializeComponent();
        }

        private void ReviseWin_Load(object sender, EventArgs e)
        {
            using (var reader = MysqlVisit.GetReader($"SELECT * FROM task WHERE ID = {TaskID}"))
            {
                while (reader.Read())
                {
                    TaskName = reader.GetString("name");
                }
            }
            MysqlVisit.CloseMysql();

            nameRevised.Text = TaskName;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string newName = nameRevised.Text;
            if (newName != "")
            {
                if (newName != TaskName)
                {
                    MysqlVisit.ExNonQuery($"UPDATE task SET name='{newName}' WHERE ID = {TaskID}");
                    MysqlVisit.CloseMysql();
                }
                TaskDU_EvHandler.UpdateTask(this, new DUEventArgs("ConfirmBtn from ReviseWin"));
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter the task name");
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            MysqlVisit.ExNonQuery($"DELETE from task WHERE ID = {TaskID}");
            MysqlVisit.CloseMysql();

            TaskDU_EvHandler.UpdateTask(this, new DUEventArgs("ConfirmBtn from ReviseWin"));
            this.Close();
        }
    }
}
