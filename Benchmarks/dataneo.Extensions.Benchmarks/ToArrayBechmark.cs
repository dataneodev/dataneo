using BenchmarkDotNet.Attributes;
using dataneo.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace dataneo.Extensions.Benchmarks
{
    [MemoryDiagnoser]
    public class ToArrayBechmark
    {
        [Params(100000)]
        public int Count { get; set; }

        public IEnumerable<int> Items => GetCountInt(Count);

        [Benchmark]
        public void ToListTest() => Items.ToList();
        [Benchmark]
        public void ToListWithPredictedCapacityTest() => Items.ToList(Count);

        [Benchmark]
        public void ToArrayTest() => Items.ToArray();

        [Benchmark]
        public void ToArrayWithPredictedCapacityTest() => Items.ToArray(Count);

        private IEnumerable<int> GetCountInt(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return i;
            }
        }
    }
}
