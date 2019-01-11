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

            CheckElementExisting(list, number);

            list.Insert(0, number * 2);

            CheckElementExisting(list, number * 2, number);

            list.Undo();

            CheckElementExisting(list, number);
            CheckRaising_AOORE_Exception(list, 1);

            list.Redo();
            CheckElementExisting(list, number * 2, number);
        }


        [Test]
        public void SomeElementsInsertionUndo()
        {
            var list = new DoublyLinkedList<int>(number);
            CheckElementExisting(list, number);

            var twice = number * 2;
            var triple = number * 3;

            list.Insert(0, twice);
            list.Insert(1, triple);
            CheckElementExisting(list, twice, triple, number);

            list.Undo();

            CheckElementExisting(list, twice, number);
            CheckRaising_AOORE_Exception(list, 2);


            list.Redo();
            CheckElementExisting(list, twice, triple, number);
        }

        private void CheckElementExisting(DoublyLinkedList<int> list, params int[] elems)
        {
            for (var index = 0; index < elems.Length; index++)
            {
                list[index].Should().Be(elems[index]);
            }
        }

        private void CheckRaising_AOORE_Exception(DoublyLinkedList<int> list, int incorrectIndex)
        {
            Action act = () => { var _ = list[incorrectIndex]; };
            act.Should().Throw<ArgumentOutOfRangeException>();
        }
    }
}