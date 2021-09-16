using LuwareListImplementation;
using NUnit.Framework;
using System.Linq;

namespace LuwareListTests 
{
    [TestFixture]
    public class WhenAdding 
    {
        [Test]
        public void AddsASingleItem() 
        {
            var list = new LuwareList<int>();

            list.Add(1);

            Assert.That(list.Count(), Is.EqualTo(1));
            Assert.That(list.First(), Is.EqualTo(1));
        }        
        
        [Test]
        public void AddsMultiple() 
        {
            var list = new LuwareList<int>();

            list.Add(1);
            list.Add(2);

            Assert.That(list.Count(), Is.EqualTo(2));
            CollectionAssert.AreEqual(new [] { 1, 2 }, list.ToArray());
        }
    }
}
