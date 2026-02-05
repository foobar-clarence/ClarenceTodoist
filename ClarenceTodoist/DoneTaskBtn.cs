using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarenceTodoist
{
    internal class DoneTaskBtn
    {
        PictureBox Tick;
        const int Tick_OfsX = 5;
        const int Tick_OfsY = 5;
        const int TickSize = 15;
        

        Button DoneBtn;

        public DoneTaskBtn(Panel Blank, int X, int Y, int DoneBtnSize) {
            Tick = new PictureBox { 
                Image = Image.FromFile("../../Images/TaskList/TaskDoneTick.png"),
                Location = new Point(X + Tick_OfsX, Y + Tick_OfsY),
                Size = new Size(TickSize, TickSize),
                SizeMode = PictureBoxSizeMode.Zoom,
                Visible = false
            };
            DoneBtn = new Button
            {
                Size = new Size(DoneBtnSize, DoneBtnSize),
                Location = new Point(X, Y),
                BackgroundImage = Image.FromFile("../../Images/TaskList/TaskDoneCircle.png"),
                BackgroundImageLayout = ImageLayout.Zoom,
                FlatStyle = FlatStyle.Flat,
            };
            
            DoneBtn.FlatAppearance.MouseOverBackColor = Color.White;
            DoneBtn.FlatAppearance.MouseDownBackColor = Color.White;
            DoneBtn.FlatAppearance.BorderSize = 0;
            DoneBtn.MouseEnter += OnMouseEnter;
            DoneBtn.MouseLeave += OnMouseLeave;

            Blank.Controls.Add(DoneBtn);
            Blank.Controls.Add(Tick);
            Tick.BringToFront();
        }

        private void OnMouseEnter(object s, EventArgs e)
        {
            Tick.Visible = true;
            DoneBtn.BackColor = Color.White;
        }

        private void OnMouseLeave(object s, EventArgs e)
        {
            Tick.Visible = false;
        }

        public void Dispose()
        {
            if (DoneBtn != null)
            {
                DoneBtn.MouseLeave -= OnMouseLeave;
                DoneBtn.MouseEnter -= OnMouseEnter;
                DoneBtn.Dispose();
            }

            if(Tick != null)
            {
                Tick.Dispose();
            }
        }
    }
}
