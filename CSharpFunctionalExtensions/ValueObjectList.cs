using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFunctionalExtensions
{
    public class ValueObjectList<T> : ValueObject<ValueObjectList<T>>, IEnumerable<T>
        where T : ValueObject<T>, IComparable<T>
    {
        public List<T> Items { get; }

        public ValueObjectList(IEnumerable<T> items)
        {
            Items = items.ToList();
        }

        protected override bool EqualsCore(ValueObjectList<T> other)
        {
            return Items.OrderBy(x => x).SequenceEqual(other.Items.OrderBy(x => x));
        }

        protected override int GetHashCodeCore()
        {
            return Items.Count;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
