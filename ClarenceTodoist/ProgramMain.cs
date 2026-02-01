
using System.Windows.Forms;

namespace ClarenceTodoist
{
    internal class ProgramMain
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TaskList());
        }
    }
}
