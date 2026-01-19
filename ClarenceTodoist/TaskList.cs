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
        const int TaskGap = 20;

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
                Location = new Point(this.Width - 200, this.Height - 150),
                AutoSize = true,
                Text = "+"
            };

            C_Task.Click += new EventHandler(Create_Button_Click);

            this.Controls.Add(C_Task);
            C_Task.BringToFront();

            #endregion

            TaskDisplayUpdate();
            TaskDU_EvHandler.DisplayUpdateRequest += new EventHandler(DURequestReceiver);
        }

        private void DURequestReceiver(object sender, EventArgs e)
        {
            TaskDisplayUpdate();
        }

        #region Display_Task
        public void TaskDisplayUpdate()
        {
            if(UserTaskList.Controls.Count > 0)
                Clear_UserTasklist();

            string connstr = "server=127.0.0.1;port=3306;user=root;password=CLarenGc5416;database=clatodoist";

            using (var connection = new MySqlConnection(connstr))
            {
                connection.Open();

                string Display = "SELECT * FROM task";
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
                                Location = new Point(0, counter * (TaskHeight + TaskGap)),
                                Size = new Size(TaskWidth, TaskHeight),
                            };
                            label.Text = name;

                            Console.WriteLine(name + "\n");
                            label.Tag = reader.GetInt32("ID");
                            label.Click += new EventHandler(TaskClick);
                            UserTaskList.Controls.Add(label);
                            counter++;
                        }
                    }
                }
            }
        }
        #endregion

        #region TaskListClear
        private void Clear_UserTasklist()
        {
            int taskCount = UserTaskList.Controls.Count;
            for(int i = 0; i < taskCount; i++)
            {
                Control control = UserTaskList.Controls[0];
                UserTaskList.Controls.Remove(UserTaskList.Controls[0]);
                if(control != null)
                    control.Dispose();
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

        #region Task_Click

        private void TaskClick(object sender, EventArgs e)
        {
            Label label = sender as Label;
            int TagValue = -1;
            if(label != null)
            {
                if(label.Tag is int)
                    TagValue = (int)label.Tag;
            }

            if(TagValue == -1)
            {
                MessageBox.Show("Error tag of the label");
                return;
            }

            ReviseWin reviseWin = new ReviseWin(TagValue, false); // id, isDone
            reviseWin.ShowDialog();
        }

        #endregion
    }
}
