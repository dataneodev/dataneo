using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using dataneo.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace dataneo.Extensions.Benchmarks
{
    [SimpleJob(RuntimeMoniker.Net48, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.NetCoreApp50)]
    [MemoryDiagnoser]
    public class ToArrayBechmark
    {
        private static int[] _arr = GetCountInt(100000).ToArray();
        //[Params(100000)]
        public int Count { get; set; }

        public IEnumerable<int> Items => _arr
                                        .Where(w => w > 1)
                                        .Select(s => s);

        [Benchmark]
        public void ToListTest() => Items.ToList();
        [Benchmark]
        public void ToListWithPredictedCapacityTest() => Items.ToList(Count);

        [Benchmark]
        public void ToArrayTest() => Items.ToArray();

        [Benchmark]
        public void ToArrayWithPredictedCapacityTest() => Items.ToArray(Count);

        private static IEnumerable<int> GetCountInt(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
            }
        }
    }
}
