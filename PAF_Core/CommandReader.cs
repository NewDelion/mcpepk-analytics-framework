using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core
{
    public class CommandReader
    {
        private bool running = true;

        public CommandReader()
        {
            
        }

        public void run()
        {
            while (this.running)
            {
                string line = Console.ReadLine();
                Logger.Write("paf > ");

            }
        }
    }
}
