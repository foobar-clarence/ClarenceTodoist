using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarenceTodoist
{
    public partial class TaskList : Form
    {
        Panel UserTaskList;
        const int UserTaskList_Height = 600;
        const int UserTaskList_Width = 650;
        const int TaskWidth = UserTaskList_Width - 50;
        const int TaskHeight = 15;

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TaskList());
        }

        public TaskList()
        {
            InitializeComponent();
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            #region UserTaskList
            UserTaskList = new Panel
            {
                Location = new Point(Width - UserTaskList_Width, Height - UserTaskList_Height),
                Size = new Size(UserTaskList_Width, UserTaskList_Height),
                BorderStyle = BorderStyle.None,

                Visible = true
            };

            this.Controls.Add(UserTaskList);
            #endregion

            #region Create_Button

            Button C_Task = new Button
            {
                Location = new Point(UserTaskList_Width - 150, UserTaskList_Height - 100),
                AutoSize = true,
                Text = "+"
            };

            C_Task.Click += new EventHandler(Create_Button_Click);

            UserTaskList.Controls.Add(C_Task);

            #endregion

            DisplayTask();
            TaskDU_EvHandler.DisplayUpdateRequest += new EventHandler(DURequestReceiver);
        }

        private void DURequestReceiver(object sender, EventArgs e)
        {
            DisplayTask();
        }

        #region Display_Task
        public void DisplayTask()
        {
            string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

            using (var connection = new MySqlConnection(connstr))
            {
                connection.Open();

                string Display = "SELECT * FROM TASK";
                using (var cmd = new MySqlCommand(Display, connection))
                {
                    var reader = cmd.ExecuteReader();

                    int counter = 0;
                    while (reader.Read())
                    {
                        string name = reader.GetString("name");
                        bool is_Done = reader.GetBoolean("isDone");

                        if (!is_Done)
                        {
                            Label label = new Label
                            {
                                Location = new Point(0, counter * TaskHeight),
                                Size = new Size(TaskWidth, TaskHeight),
                                Text = $"{name}"
                            };
                            UserTaskList.Controls.Add(label);
                            counter++;
                        }
                    }
                }
            }
        }
        #endregion

        #region Create_Button_Click
        private void Create_Button_Click(object sender, EventArgs e)
        {
            CreateWin createWin = new CreateWin();
            createWin.ShowDialog();
        }
        #endregion
    }
}
