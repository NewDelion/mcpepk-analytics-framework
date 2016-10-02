using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Principal;
using System.Reflection;
using System.ComponentModel;
using PAF_Core.command;

namespace PAF_Core.module
{
    public class ModuleManager
    {
        List<IModule> _modules = new List<IModule>();
        
        public void LoadModules()
        {
            if (!Directory.Exists("plugin"))
                Directory.CreateDirectory("plugin");

            foreach(string filename in Directory.GetFiles("plugin", "*.dll"))
            {
                Assembly newAssembly = Assembly.LoadFile(Path.GetFullPath(filename));
                Type[] types = newAssembly.GetExportedTypes();
                foreach(Type type in types)
                {
                    try
                    {
                        if (!typeof(IModule).IsAssignableFrom(type)) continue;
                        var ctor = type.GetConstructor(Type.EmptyTypes);
                        if(ctor != null)
                        {
                            var module = (IModule)ctor.Invoke(null);
                            module.initModule(this, PAFProxy.instance);
                            _modules.Add(module);
                            Logger.WriteLineInfo(string.Format("プラグインを読み込みました：{0}", module.getName()));
                        }
                    }
                    catch(Exception ex)
                    {
                        Logger.WriteLineWarn(string.Format("プラグインの読み込み中にエラーが発生しました：{0}", type));
                        Logger.WriteLineError(ex.Message);
                    }
                }
            }
        }

        public void callEvent(PAF_Core.module_event.ModuleEvent ev)
        {
            foreach (var module in _modules)
            {
                try
                {
                    module.handleModuleEvent(ev);
                }
                catch (Exception ex)
                {
                    Logger.WriteLineWarn(string.Format("{0}の処理中にエラー：Module({1})", ev.GetType().Name, module.getName()));
                    Logger.WriteLineError(ex.Message);
                }
            }
        }

        
    }
}
