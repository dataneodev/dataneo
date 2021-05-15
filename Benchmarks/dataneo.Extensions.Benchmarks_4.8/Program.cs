using BenchmarkDotNet.Running;

namespace dataneo.Extensions.Benchmarks_4._8
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ToArrayBechmark>();
        }
    }
}
