using AppWithPlugins.Plugins.Contracts;

using CommandLine;

namespace AppWithPlugins.Cli
{
  public class Program
  {
    static void Main(string[] args)
    {
      IEnumerable<(ICommand command, Type type)> commands = PluginManager.GetCommands(Path.Combine(Environment.CurrentDirectory, "Plugins"));      
      Type[] commandTypes = commands.Select(c => c.type).ToArray();
      
      Parser.Default
        .ParseArguments(args, commandTypes)
        .WithParsed<ICommand>(t => t.Execute());
    }
  }
}