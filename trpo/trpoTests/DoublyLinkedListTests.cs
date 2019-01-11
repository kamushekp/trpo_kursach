using System;
using FluentAssertions;
using NUnit.Framework;
using trpo.DoublyLinkedList;

namespace trpoTests
{
    [TestFixture]
    public class DoublyLinkedListTests
    {
        private int number = (new Random().Next(10));

        [Test]
        public void OneElementInsertionUndo()
        {
            var list = new DoublyLinkedList<int>(number);
            list[0].Should().Be(number);

            list.Insert(0, number * 2);
            list[0].Should().Be(number * 2);
            list[1].Should().Be(number);

            list.Undo();

            list[0].Should().Be(number);
            Action act = () => { var _ = list[1]; };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }


        [Test]
        public void SomeElementsInsertionUndo()
        {
            var list = new DoublyLinkedList<int>(number);
            list[0].Should().Be(number);

            var twice = number * 2;
            var triple = number * 3;

            list.Insert(0, twice);
            list.Insert(1, triple);

            list[0].Should().Be(twice);
            list[1].Should().Be(triple);
            list[2].Should().Be(number);

            list.Undo();

            list[0].Should().Be(twice);
            list[1].Should().Be(number);
            Action act = () => { var _ = list[2]; };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}