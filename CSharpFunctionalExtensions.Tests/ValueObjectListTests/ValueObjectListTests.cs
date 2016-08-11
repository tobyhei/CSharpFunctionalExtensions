using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.ValueObjectListTests
{
    public class ValueObjectListTests
    {
        [Fact]
        public void Can_create_an_empty_value_object_list()
        {
            var valueObjectList = new ValueObjectList<MyValueObject>(new List<MyValueObject>());

            valueObjectList.Should().BeEmpty();
        }

        [Fact]
        public void Can_create_a_multiple_element_value_object_list()
        {
            var valueObjectList = ListFromTestValues(new[] { 5, 7, 6 });

            valueObjectList.Should().HaveCount(3);
        }

        [Fact]
        public void Two_sorted_equal_value_object_lists_are_equal()
        {
            var valueObjectList = ListFromTestValues(new[] { 5, 6, 7 });

            var valueObjectList2 = ListFromTestValues(new[] { 5, 6, 7 });

            valueObjectList.Should().BeEquivalentTo(valueObjectList2);
        }

        [Fact]
        public void Two_unsorted_equal_value_object_lists_are_equal()
        {
            var valueObjectList = ListFromTestValues(new[] { 5, 7, 6 });

            var valueObjectList2 = ListFromTestValues(new[] { 6, 5, 7 });

            valueObjectList.Should().BeEquivalentTo(valueObjectList2);
        }

        [Fact]
        public void Two_unequal_value_object_lists_are_not_equal()
        {
            var valueObjectList = ListFromTestValues(new[] { 5, 7, 6 });

            var valueObjectList2 = ListFromTestValues(new[] { 5, 6, 4 });

            valueObjectList.Should().NotBeEquivalentTo(valueObjectList2);
        }

        [Fact]
        public void Two_unequal_length_value_object_lists_are_not_equal()
        {
            var valueObjectList = ListFromTestValues(new[] {5, 7});

            var valueObjectList2 = ListFromTestValues(new[] { 5, 7, 4});

            valueObjectList.Should().NotBeEquivalentTo(valueObjectList2);
        }

        private ValueObjectList<MyValueObject> ListFromTestValues(IEnumerable<int> testValues)
        {
            return new ValueObjectList<MyValueObject>(testValues.Select(x => new MyValueObject(x)));
        }

        public class MyValueObject : ValueObject<MyValueObject>, IComparable<MyValueObject>
        {
            public int Value { get; }

            public MyValueObject(int value)
            {
                Value = value;
            }

            protected override bool EqualsCore(MyValueObject other)
            {
                return other.Value == Value;
            }

            protected override int GetHashCodeCore()
            {
                return Value.GetHashCode();
            }

            public int CompareTo(MyValueObject other)
            {
                return Value.CompareTo(other.Value);
            }
        }
    }
}
