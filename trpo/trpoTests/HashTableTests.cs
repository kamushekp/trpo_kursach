using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using trpo.HashTable;

namespace trpoTests
{
    [TestFixture]
    public class HashTableTests
    {
        private int number = new Random().Next();

        [Test]
        public void Dict_OneElement()
        {
            var dict = new HashTable<int, string>(new Dictionary<int, string>()
            {
                {number, "111"},
            });

            dict[number].Should().Be("111");

            dict.Add(number * 2, "222");
            dict[number].Should().Be("111");
            dict[number * 2].Should().Be("222");

            dict.Undo();
            dict[number].Should().Be("111");
            Action act = () =>
            {
                var _ = dict[number * 2];
            };
            act.Should().Throw<KeyNotFoundException>();

            dict.Redo();
            dict[number].Should().Be("111");
            dict[number * 2].Should().Be("222");
        }

        [Test]
        public void Dict_TwoElements()
        {
            var dict = new HashTable<int, string>(new Dictionary<int, string>()
            {
                {number, "111"},
                {number + 2, "222"},
            });

            dict[number].Should().Be("111");
            dict[number + 2].Should().Be("222");

            dict.Add(number * 2, "333");

            dict[number].Should().Be("111");
            dict[number + 2].Should().Be("222");
            dict[number * 2].Should().Be("333");

            dict.Undo();

            dict[number].Should().Be("111");
            dict[number + 2].Should().Be("222");
            Action act = () =>
            {
                var _ = dict[number * 2];
            };
            act.Should().Throw<KeyNotFoundException>();

            dict.Redo();
            dict[number].Should().Be("111");
            dict[number + 2].Should().Be("222");
            dict[number * 2].Should().Be("333");

            dict.Add(number * 3, "444");

            dict[number].Should().Be("111");
            dict[number + 2].Should().Be("222");
            dict[number * 2].Should().Be("333");
            dict[number * 3].Should().Be("444");

            dict.Undo();
            dict.Undo();
            dict[number].Should().Be("111");
            dict[number + 2].Should().Be("222");
        }
    }
}