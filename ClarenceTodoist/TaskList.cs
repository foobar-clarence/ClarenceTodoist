using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarenceTodoist
{
    public partial class TaskList : Form
    {
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

        }
    }
}
