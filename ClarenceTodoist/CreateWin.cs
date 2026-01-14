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
            string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

            using(var connection = new MySqlConnection(connstr))
            {
                connection.Open();

                if (TaskName.Text != "")
                {
                    string Insert = $"INSERT INTO task(isDone, name) VALUES(0, '{TaskName.Text}')";
                    using (var cmd = new MySqlCommand(Insert, connection))
                    {
                        int row_affect = cmd.ExecuteNonQuery();
                    }
                }

                this.Close();
                DU_request("ConfirmBtn_Click from CreatWin");
            }
        }

        protected void DU_request(string Func)
        {   
            TaskDU_EvHandler.UpdateTask(this, new DUEventArgs(Func));

        }
        #endregion
    }
}
