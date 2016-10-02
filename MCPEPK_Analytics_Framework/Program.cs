using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAF_Core;

namespace MCPEPK_Analytics_Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            PAFProxy proxy = new PAFProxy();

            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            PrintBanner();

            while (true)
            {
                Console.Write("paf > ");
                string command_line = Console.ReadLine();
                if (command_line.Trim() == "")
                    continue;

                proxy.command_map.dispatch(command_line);

                string[] command = command_line.Split(' ');
                if (command.Length > 0 && command[0] == "exit" || command[0] == "quit")
                    break;
            }
            Console.WriteLine();
            Console.WriteLine("Good bye!!");
            Console.ReadKey(false);
        }

        static void PrintBanner()
        {
            Console.WriteLine(@" /*******************************************************************************/");
            Console.WriteLine(@" /****  _____  _  __                       _       _   _                     ****/");
            Console.WriteLine(@" /**** |  __ \| |/ /     /\               | |     | | (_)                    ****/");
            Console.WriteLine(@" /**** | |__) | ' /     /  \   _ __   __ _| |_   _| |_ _  ___ ___            ****/");
            Console.WriteLine(@" /**** |  ___/|  <     / /\ \ | '_ \ / _` | | | | | __| |/ __/ __|           ****/");
            Console.WriteLine(@" /**** | |    | . \   / ____ \| | | | (_| | | |_| | |_| | (__\__ \           ****/");
            Console.WriteLine(@" /**** |_|    |_|\_\ /_/____\_\_| |_|\__,_|_|\__, |\__|_|\___|___/     _     ****/");
            Console.WriteLine(@" /****               |  ____|                 __/ |                   | |    ****/");
            Console.WriteLine(@" /****               | |__ _ __ __ _ _ __ ___|___/___      _____  _ __| | __ ****/");
            Console.WriteLine(@" /****               |  __| '__/ _` | '_ ` _ \ / _ \ \ /\ / / _ \| '__| |/ / ****/");
            Console.WriteLine(@" /****               | |  | | | (_| | | | | | |  __/\ V  V / (_) | |  |   <  ****/");
            Console.WriteLine(@" /****               |_|  |_|  \__,_|_| |_| |_|\___| \_/\_/ \___/|_|  |_|\_\ ****/");
            Console.WriteLine(@" /****                                                                       ****/");
            Console.WriteLine(@" /*******************************************************************************/");
            Console.WriteLine();
        }
    }
}
