using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Generic;
using System.Linq;

namespace dataneo.Extensions.Benchmarks_4._8
{
    //[SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [SimpleJob(RuntimeMoniker.Net48)]
    // [SimpleJob(RuntimeMoniker.NetCoreApp21)]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.GitHub]
    [HtmlExporter]
    public class ToArrayBechmark
    {
        [Params(10, 10000, 1000000)]
        public static int Count { get; set; }
        [Benchmark]
        public void ToListTest()
            => GetCountInt(Count)
                .Select(s => s)
                .Where(w => w > 1)
                .ToList();

        [Benchmark]
        public void ToListWithPredictedCapacityTest()
           => GetCountInt(Count)
                .Select(s => s)
                .Where(w => w > 1)
                .ToList(Count);

        [Benchmark]
        public void ToArrayTest()
            => GetCountInt(Count)
                .Select(s => s)
                .Where(w => w > 1)
                .ToArray();

        [Benchmark]
        public void ToArrayWithPredictedCapacityTest()
            => GetCountInt(Count)
                .Select(s => s)
                .Where(w => w > 1)
                .ToArray(Count);

        private static IEnumerable<int> GetCountInt(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
            }
        }
    }
}
