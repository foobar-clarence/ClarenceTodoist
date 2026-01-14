using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClarenceTodoist
{
    public class DUEventArgs : EventArgs
    {
        public DateTime requestTime { get; set; }
        public string requestFunc { get; set; }

        public DUEventArgs(string Func = "Unknown function")
        {
            requestTime = DateTime.Now;
            requestFunc = Func;
        }
    }

    public static class TaskDU_EvHandler
    {
        public static event EventHandler DisplayUpdateRequest;
        
        public static void UpdateTask(object sender, DUEventArgs eventargs)
        {
            if(DisplayUpdateRequest != null)
            {
                DisplayUpdateRequest.Invoke(sender, eventargs);
            }

            Console.WriteLine($"Function {eventargs.requestFunc} required for task update at {eventargs.requestTime}");
            //MessageBox.Show($"Function {eventargs.requestFunc} required for task update at {eventargs.requestTime}");
        }
    }
}
