using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAF_Core;
using PAF_Core.module;
using PAF_Core.module_event;

namespace PAF_Core.command
{
    public class CommandMap
    {
        private Dictionary<Command, IModule> _commands = new Dictionary<Command, IModule>();

        public void register(Command command, IModule module)
        {
            if (!exists(command.getName()))
                _commands.Add(command, module);
            else
                throw new Exception("コマンドは既にregisterされています。");
        }

        public bool exists(string command)
        {
            foreach(var cmd in _commands)
                if (cmd.Key.getName().Equals(command))
                    return true;
            return false;
        }

        public IModule find_module(string command)
        {
            foreach (var cmd in _commands)
                if (cmd.Key.getName().Equals(command))
                    return cmd.Value;
            return null;
        }

        public Command find_command(string command)
        {
            foreach (var cmd in _commands)
                if (cmd.Key.getName().Equals(command))
                    return cmd.Key;
            return null;
        }

        public void dispatch(string command_line)
        {
            string[] command_line_split = command_line.Split(' ');
            if (command_line_split.Length == 0)
                return;
            string command = command_line_split[0];
            string[] args = command_line_split.Skip(1).ToArray();
            if (command == "exit" || command == "quit")
                return;
            if (!exists(command))
            {
                Logger.WriteLineError("Unknown command...");
                return;
            }
            var module = find_module(command);
            var command_info = find_command(command);
            
            if(!module.handleCommand(command_info, args))
            {
                Logger.WriteLine(command_info.getUsage());
            }
        }

        public void showCommandHelp()
        {
            foreach (var command in _commands)
            {
                Logger.Write(command.Key.getName() + ": ", ConsoleColor.Green);
                Logger.WriteLine(command.Key.getDescription());
            }
        }
    }
}
