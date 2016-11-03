using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAF_Core.module;
using PAF_Core.module_event;

namespace PAF_Core.command
{
    public class DefaultCommandHandler : PAF_Core.module.IModule
    {
        public string getName()
        {
            return "DefaultCommandHandler";
        }

        public string getVersion()
        {
            return "1.0.0";
        }

        public bool handleCommand(Command command, string[] args)
        {
            if (command.getName().Equals("help"))
                proxy.command_map.showCommandHelp();
            else if (command.getName().Equals("target-ip"))
            {
                if (args.Length == 0)
                {
                    if (proxy.IP_Client == null)
                        Logger.WriteLineWarn("クライアントのIPは設定されていません。");
                    else
                        Logger.WriteLineInfo(string.Format("IP：{0}", proxy.IP_Client.ToString()));
                }
                else
                {
                    string ip_string = args[0];
                    System.Net.IPAddress ip = null;
                    if(System.Net.IPAddress.TryParse(ip_string, out ip))
                    {
                        proxy.IP_Client = ip;
                        Logger.WriteLine("クライアントのIPを設定しました。", ConsoleColor.Green);
                        Logger.WriteLine(string.Format("IP：{0}", proxy.IP_Client.ToString()));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (command.getName().Equals("server-ip"))
            {
                if (args.Length == 0)
                {
                    if (proxy.Address_Server == null)
                        Logger.WriteLineWarn("サーバのIPは設定されていません。");
                    else
                        Logger.WriteLineInfo(string.Format("IP：{0}", proxy.Address_Server.Address.ToString()));
                }
                else
                {
                    string ip_string = args[0];
                    System.Net.IPAddress ip = null;
                    if (System.Net.IPAddress.TryParse(ip_string, out ip))
                    {
                        if (proxy.Address_Server == null)
                            proxy.Address_Server = new System.Net.IPEndPoint(ip, 0);
                        else
                            proxy.Address_Server.Address = ip;
                        Logger.WriteLine("サーバのIPを設定しました。", ConsoleColor.Green);
                        Logger.WriteLine(string.Format("IP：{0}", proxy.Address_Server.Address.ToString()));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (command.getName().Equals("server-port"))
            {
                if (args.Length == 0)
                {
                    if (proxy.Address_Server == null)
                        Logger.WriteLineWarn("サーバのポートは設定されていません。");
                    else
                        Logger.WriteLineInfo(string.Format("Port：{0}", proxy.Address_Server.Port));
                }
                else
                {
                    string port_string = args[0];
                    int port = 19132;
                    if(int.TryParse(port_string, out port))
                    {
                        if (proxy.Address_Server == null)
                            proxy.Address_Server = new System.Net.IPEndPoint(System.Net.IPAddress.Loopback, port);
                        else
                            proxy.Address_Server.Port = port;
                        Logger.WriteLine("サーバのポートを設定しました。", ConsoleColor.Green);
                        Logger.WriteLine(string.Format("Port：{0}", proxy.Address_Server.Port));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (command.getName().Equals("port-to-client"))
            {
                if (args.Length == 0)
                    Logger.WriteLineInfo(string.Format("Port：{0}", proxy.ProxyToClientPort));
                else
                {
                    string port_string = args[0];
                    int port = 19133;
                    if (int.TryParse(port_string, out port))
                    {
                        proxy.ProxyToClientPort = port;
                        Logger.WriteLine("プロキシのポートを設定しました。", ConsoleColor.Green);
                        Logger.WriteLine(string.Format("Port：{0}", proxy.ProxyToClientPort));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (command.getName().Equals("port-to-server"))
            {
                if (args.Length == 0)
                    Logger.WriteLineInfo(string.Format("Port：{0}", proxy.ProxyToServerPort));
                else
                {
                    string port_string = args[0];
                    int port = 19133;
                    if (int.TryParse(port_string, out port))
                    {
                        proxy.ProxyToServerPort = port;
                        Logger.WriteLine("プロキシのポートを設定しました。", ConsoleColor.Green);
                        Logger.WriteLine(string.Format("Port：{0}", proxy.ProxyToServerPort));
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (command.getName().Equals("run"))
            {
                proxy.Start();
            }
            else if (command.getName().Equals("stop"))
            {
                proxy.Stop();
            }
            return true;
        }

        public void handleModuleEvent(ModuleEvent ev) { }

        private PAFProxy proxy = null;
        public void initModule(ModuleManager manager, PAFProxy proxy)
        {
            this.proxy = proxy;

            proxy.command_map.register(new Command("help", "ヘルプを表示します", "help"), this);
            proxy.command_map.register(new Command("target-ip", "クライアントのIPを取得・設定します", "target-ip [IP]"), this);
            proxy.command_map.register(new Command("server-ip", "サーバのIPを取得・設定します", "server-ip [IP]"), this);
            proxy.command_map.register(new Command("server-port", "サーバのポートを取得・設定します", "server-port [Port]"), this);
            proxy.command_map.register(new Command("port-to-client", "プロキシのポートを取得・設定します", "port-to-client [Port]"), this);
            proxy.command_map.register(new Command("port-to-server", "プロキシのポートを取得・設定します", "port-to-server [Port]"), this);
            proxy.command_map.register(new Command("run", "プロキシを起動します", "run"), this);
            proxy.command_map.register(new Command("stop", "プロキシを停止します", "stop"), this);
        }
    }
}
