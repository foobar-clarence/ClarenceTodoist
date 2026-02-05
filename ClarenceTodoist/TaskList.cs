using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarenceTodoist
{
    public partial class TaskList : Form
    {
        Panel StatusBar;
        const int StatusBar_Width = 250;

        Panel UserTaskList;

        const int TaskListGap_Top = 100;
        const int TaskWidth = 450;
        const int TaskHeight = 15;
        const int TaskGap_Top = 15;
        const int TaskGap_Bottom = 9;
        const int GaplineThickness = 1;

        const int SizeOfTaskCreateButton = 25;
        const int GapOfTaskCreateButton = 15;

        const int DoneBtnSize = 25;

        public TaskList()
        {
            InitializeComponent();
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            #region StatusBar

            StatusBar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(StatusBar_Width, Height),
                BorderStyle = BorderStyle.None,
                BackColor = Color.FromArgb(227, 250, 255)
            };
            this.Controls.Add(StatusBar);

            #endregion

            #region UserTaskList

            int UserTaskList_Width = Width - StatusBar_Width;
            int UserTaskList_Height = Height;
            UserTaskList = new Panel
            {
                Location = new Point(StatusBar_Width, 0),
                Size = new Size(UserTaskList_Width, UserTaskList_Height),
                BorderStyle = BorderStyle.None,
                BackColor = Color.White
            };

            this.Controls.Add(UserTaskList);
            #endregion

            TaskDisplayUpdate();
            TaskDU_EvHandler.DisplayUpdateRequest += new EventHandler(DURequestReceiver);
        }

        private void DURequestReceiver(object sender, EventArgs e)
        {
            TaskDisplayUpdate();
        }

        #region Display_Task

        //更新用户任务界面的显示情况
        public void TaskDisplayUpdate()
        {
            if(UserTaskList.Controls.Count > 0)
                Clear_UserTasklist();


            using (var reader = MysqlVisit.GetReader("SELECT * FROM task"))
            {
                Point startPoint = new Point((UserTaskList.Width - TaskWidth) / 2, TaskListGap_Top);
                int TaskOffest_Y = 0;

                while (reader.Read())
                {
                    #region ParametersGetting
                    string name = reader.GetString("name");
                    bool is_Done = reader.GetBoolean("isDone");
                    #endregion

                    if (!is_Done)
                    {
                        // 任务显示：任务完成按钮 + label文本 + 灰色横线间隔(美观考虑)
                        TaskOffest_Y += TaskGap_Top;
                        #region DoneTaskBtn

                        int ButtonX = startPoint.X;
                        int ButtonY = startPoint.Y + TaskOffest_Y;
                        DoneTaskBtn btn = new DoneTaskBtn(UserTaskList, ButtonX, ButtonY, DoneBtnSize); 

                        #endregion

                        #region LabelDrawing
                        //Label x坐标空出任务完成按钮位置
                        int LabelX = startPoint.X + DoneBtnSize + 10;
                        int LabelY = startPoint.Y + TaskOffest_Y;
                        Label label = new Label
                        {
                            Location = new Point(LabelX, LabelY),
                            AutoSize = true,
                            Font = new Font("微软雅黑", 9f),
                            Text = name,
                            UseCompatibleTextRendering = true,
                            Width = TaskWidth,
                        };

                        label.AutoEllipsis = true;
                        label.Tag = reader.GetInt32("ID");
                        label.Click += new EventHandler(TaskClick);
                        UserTaskList.Controls.Add(label);

                        TaskOffest_Y += label.Height;
                        #endregion

                        #region GapLineDrawing

                        // 线条与文字间隔设置
                        TaskOffest_Y += TaskGap_Bottom;

                        int GL_X = LabelX;
                        int GL_Y = startPoint.Y + TaskOffest_Y;
                        Panel Gapline = new Panel { 
                            Location = new Point (GL_X, GL_Y),
                            Size = new Size (TaskWidth, GaplineThickness),
                            BackColor = Color.FromArgb(160, 160, 160),
                            BorderStyle = BorderStyle.None,
                        };
                        UserTaskList.Controls.Add(Gapline);
                        Gapline.BringToFront();

                        #endregion
                    }
                }
                MysqlVisit.CloseMysql();

                // 创建按钮与任务显示下用以添加新任务
                TaskOffest_Y += GapOfTaskCreateButton;
                UserTaskList.Controls.Add(Create_CreateTaskButton(startPoint.X, startPoint.Y + TaskOffest_Y));
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

        private Button Create_CreateTaskButton(int X, int Y)
        {
            //C_Task即创建任务按钮的基本配置
            Button C_Task = new Button
            {
                Location = new Point(X, Y),
                Size = new Size(SizeOfTaskCreateButton, SizeOfTaskCreateButton),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                BackgroundImageLayout = ImageLayout.Zoom,
                BackgroundImage = Image.FromFile("../../Images/TaskList/PlusSign.png"),
                Text = ""
            };
            //去边缘，设置悬停颜色，绑定点击事件
            C_Task.FlatAppearance.BorderSize = 0;
            C_Task.FlatAppearance.MouseOverBackColor = Color.Gray;
            C_Task.Click += new EventHandler(Create_Button_Click);

            //改变C_Task按钮形状为圆形
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            int CircleDia = C_Task.Width;
            path.AddEllipse(0, 0, CircleDia, CircleDia);
            C_Task.Region = new Region(path);

            this.Controls.Add(C_Task);
            C_Task.BringToFront();

            //返回按钮控件
            return C_Task;
        }
    }
}
