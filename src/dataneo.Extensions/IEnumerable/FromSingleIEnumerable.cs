using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataneo.Extensions
{
    public static class FromSingleIEnumerable
    {
        public static FromSingleIEnumerable<TSource> Get<TSource>(TSource value) => new FromSingleIEnumerable<TSource>(value);
    }

    public readonly struct FromSingleIEnumerable<T> : IEnumerable<T>
    {
        private readonly FromSingleEnumerator<T> _fromSingleEnumerator;

        public FromSingleIEnumerable(T value)
        {
            this._fromSingleEnumerator = new FromSingleEnumerator<T>(value);
        }

        public IEnumerator<T> GetEnumerator() => _fromSingleEnumerator;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private struct FromSingleEnumerator<Tin> : IEnumerator<Tin>
        {
            private readonly Tin _value;
            private readonly bool _valueSet;
            private bool firstIncrement;

            public FromSingleEnumerator(Tin value)
            {
                this._value = value;
                this._valueSet = true;
                this.firstIncrement = false;
            }

            public Tin Current => this._value;
            object IEnumerator.Current => this._value;

            public void Dispose() { }

            public bool MoveNext()
            {
                if (this.firstIncrement || !this._valueSet)
                    return false;

                this.firstIncrement = true;
                return true;
            }

            public void Reset()
            {
                firstIncrement = false;
            }
        }
    }
}
