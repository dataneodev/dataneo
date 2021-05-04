using BenchmarkDotNet.Running;

namespace dataneo.Extensions.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ToArrayBechmark>();
        }
    }
}
