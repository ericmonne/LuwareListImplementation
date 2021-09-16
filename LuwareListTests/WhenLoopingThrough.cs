using System;
using System.Collections.Generic;
using LuwareListImplementation;
using NUnit.Framework;

namespace LuwareListTests 
{
    [TestFixture]
    public class WhenLoopingThrough 
    {
        [Test]
        public void LoopsJustFine() 
        {
            var list = new LuwareList<int> { 1, 2 };
            var cSharpList = new List<int>();

            foreach (var i in list) 
            {
                cSharpList.Add(i);
            }

            CollectionAssert.AreEqual(list, cSharpList);
        }

        [Test]
        public void ThrowsIfCollectionIsModifiedWithAddition() 
        {
            Assert.That(() => 
            {
                var list = new LuwareList<int> { 1 };

                foreach (var i in list) 
                {
                    list.Add(i + 1);
                }

            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("You can't change the list while iterating."));
        }

        [Test]
        public void ThrowsIfCollectionIsModifiedWithRemoval() 
        {
            Assert.That(() => 
            {
                var list = new LuwareList<int> { 1 };

                foreach (var i in list) {
                    list.Remove(1);
                }

            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("You can't change the list while iterating."));
        }

        [Test]
        public void ThrowsIfCollectionIsModifiedByCleaningItUp() 
        {
            Assert.That(() => 
            {
                var list = new LuwareList<int> { 1 };
                
                foreach (var i in list) 
                {
                    list.Clear();
                }

            }, Throws.TypeOf<InvalidOperationException>().With.Message.EqualTo("You can't change the list while iterating."));
        }
    }
}
