using System.Reflection;

using AppWithPlugins.Plugins.Contracts;

namespace AppWithPlugins.Cli
{
  public class PluginManager
  {
    public static IEnumerable<(ICommand command, Type type)> GetCommands(string pluginsRootDirectory)
    {
      IEnumerable<DirectoryInfo> pluginsDirectories = Directory.EnumerateDirectories(pluginsRootDirectory).Select(p => new DirectoryInfo(p));
      IEnumerable<string> pluginsPath = pluginsDirectories.SelectMany(d => d.GetFiles("*.dll").Select(f => f.FullName));

      RemoveContractConflicts(pluginsPath);

      IEnumerable<(ICommand command, Type type)> commands = pluginsPath.SelectMany(pluginPath =>
      {
        Assembly pluginAssembly = LoadPlugin(pluginPath);
        return CreateCommands(pluginAssembly);
      });

      return commands;
    }

    private static void RemoveContractConflicts(IEnumerable<string> pluginsDirectories)
    {
      string contractAssemblyName = Path.GetFileName(typeof(ICommand).Assembly.Location);
      string rootContractAssemblyName = Path.Combine(Environment.CurrentDirectory, contractAssemblyName);

      if(contractAssemblyName != null)
      {
        pluginsDirectories.Where(x =>
          !rootContractAssemblyName.Equals(x)
          && Path.GetFileName(x).Equals(contractAssemblyName)          
        ).ToList().ForEach(File.Delete);
      }
    }

    private static Assembly LoadPlugin(string pluginPath)
    {
      PluginLoadContext loadContext = new PluginLoadContext(pluginPath);
      return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginPath)));
    }

    private static IEnumerable<(ICommand command, Type type)> CreateCommands(Assembly assembly)
    {
      List<(ICommand command, Type type)> result = new();

      foreach(Type type in assembly.GetTypes())
      {
        if(typeof(ICommand).IsAssignableFrom(type))
        {
          ICommand? plugin = null;
          try
          {
            plugin = Activator.CreateInstance(type) as ICommand;
          } catch(Exception) { /* do nothing */ }

          if(plugin != null)
          {
            result.Add((plugin, type));
          }
        }
      }

      if(result.Count == 0)
      {
        string availableTypes = string.Join(",", assembly.GetTypes().Select(t => t.FullName));
        throw new ApplicationException(
            $"Can't find any type which implements ICommand in {assembly} from {assembly.Location}.\n" +
            $"Available types: {availableTypes}");
      }

      return result;
    }
  }
}
