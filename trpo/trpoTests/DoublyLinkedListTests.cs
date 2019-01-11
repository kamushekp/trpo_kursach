using System;
using FluentAssertions;
using NUnit.Framework;
using trpo.DoublyLinkedList;

namespace trpoTests
{
    [TestFixture]
    public class DoublyLinkedListTests
    {
        private int number = (new Random().Next());

        [Test]
        public void OneElement()
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
    }
}