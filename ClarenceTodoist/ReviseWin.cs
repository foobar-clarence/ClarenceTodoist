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
            string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

            using (var connection = new MySqlConnection(connstr))
            {
                connection.Open();

                string GetTaskInfo = $"SELECT * FROM task WHERE ID = {TaskID}";
                using (var cmd = new MySqlCommand(GetTaskInfo, connection))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TaskName = reader.GetString("name");
                    }
                }
            }

            nameRevised.Text = TaskName;
        }

        private void ConfirmBtn_Click(object sender, EventArgs e)
        {
            string newName = nameRevised.Text;
            if (newName != "" && newName != TaskName)
            {
                string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

                using (var connection = new MySqlConnection(connstr))
                {
                    connection.Open();

                    string ChangeTaskName = $"UPDATE task SET name='{newName}' WHERE ID = {TaskID}";
                    using (var cmd = new MySqlCommand(ChangeTaskName, connection))
                    {
                        int row_affected = cmd.ExecuteNonQuery();
                    }
                }
            }
            TaskDU_EvHandler.UpdateTask(this, new DUEventArgs("ConfirmBtn from ReviseWin"));
            this.Close();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

            using (var connection = new MySqlConnection(connstr))
            {
                connection.Open();

                string DeleteTask = $"DELETE from task WHERE ID = {TaskID}";
                using (var cmd = new MySqlCommand(DeleteTask, connection))
                {
                    int row_affected = cmd.ExecuteNonQuery();
                }
            }
            TaskDU_EvHandler.UpdateTask(this, new DUEventArgs("ConfirmBtn from ReviseWin"));
            this.Close();
        }
    }
}
