using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAF_Core.command
{
    public class Command
    {
        private string name;
        private string description;
        private string usage;

        public Command(string name, string description, string usage)
        {
            this.name = name;
            this.description = description;
            this.usage = usage;
        }

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }

        public string getUsage()
        {
            return usage;
        }


    }
}
