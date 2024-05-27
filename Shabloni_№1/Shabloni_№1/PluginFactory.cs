using System.Reflection;
using System.Runtime.InteropServices;
using Common;

public static class PluginFactory
{
    public static IPlugin GetPlugin(int number)
    {
        string[] pluginFiles = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Plugins\", "*.dll");

        if (number < 1 || number > pluginFiles.Length)
        {
            //Console.WriteLine("Invalid plugin number");
            return null;
        }

        string pluginFile = pluginFiles[number - 1];
        string pluginClassName = Path.GetFileNameWithoutExtension(pluginFile);

        Assembly assembly = Assembly.LoadFile(pluginFile);
        
        Type type = assembly.GetType(pluginClassName);        

        IPlugin plugin = (IPlugin)Activator.CreateInstance(type);

        return plugin;
    }
}