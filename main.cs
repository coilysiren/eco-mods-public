using CommandLine;

namespace CLI
{
    public class Program
    {
        [Verb("BunWulfEducational")]
        public class BunWulfEducational { }

        [Verb("BunWulfStructural")]
        public class BunWulfStructural { }

        public static void Main(string[] args)
        {
            _ = Parser
                .Default.ParseArguments<BunWulfEducational, BunWulfStructural>(args)
                .WithParsed<BunWulfEducational>(opts => Console.WriteLine("Add command"))
                .WithParsed<BunWulfStructural>(opts => Console.WriteLine("Add command"));
        }
    }
}
