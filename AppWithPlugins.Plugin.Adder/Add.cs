using AppWithPlugins.Plugins.Contracts;

using CommandLine;

namespace AppWithPlugins.Plugin.Adder
{
  [Verb("add", HelpText = "Adds two numbers and returns the sum")]
  public class Add : ICommand
  {
    [Option('a', "addends", Required = true, Separator = ' ', Default = new float[0])]
    public IList<float> Addends { get; set; } = new List<float>(){ 0f, 0f };

    public void Execute()
    {
      Console.WriteLine($"{Addends[0] + Addends[1]}");
    }
  }
}