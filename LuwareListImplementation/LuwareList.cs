using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LuwareListImplementation 
{
    public interface ILuwareList<T> 
    {
        void Add(T element);
        void Clear();
        bool Remove(T element);
    }

    public class LuwareList<T> : ILuwareList<T>, IEnumerable<T> 
    {
        private T[] elements = new T[0];
        private int index;
        private int size;
        internal int version;
        public int Length => elements.Length;

        public IEnumerator<T> GetEnumerator() 
        {
            return new LuwareIEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() 
        {
            return GetEnumerator();
        }

        public void Add(T element) 
        {
            if (elements.Any()) 
            {
                ExpandSize();
            } 
            else 
            {
                elements = new T[1];
            }

            elements[index] = element;
            version++;
            size++;
            index++;
        }

        private void ExpandSize() 
        {
            var copy = new T[size + 1];
            Array.Copy(elements, copy, size);
            elements = copy;
        }

        public void Clear() 
        {
            elements = new T[0];
            version++;
        }

        public bool Remove(T element) 
        {
            for (int i = 0; i < elements.Length; i++) 
            {
                if (!elements[i].Equals(element)) continue;

                var (leftSide, rightSide) = Slice(i);
                elements = Join(leftSide, rightSide);

                version++;
                return true;
            }

            version++;
            return false;
        }

        private (T[] leftSide, T[] rightSide) Slice(int i) 
        {
            return new(elements.AsSpan().Slice(0, i).ToArray(), elements.AsSpan().Slice(i + 1, (elements.Length - 1) - i).ToArray());
        }

        private T[] Join(T[] leftSide, T[] rightSide) 
        {
            var result = new T[leftSide.Length + rightSide.Length];
            Array.Copy(leftSide, result, leftSide.Length);
            Array.Copy(rightSide, 0, result, leftSide.Length, rightSide.Length);
            return result;
        }

        public T ElementAt(int index) 
        {
            return elements[index];
        }
    }

    public class LuwareIEnumerator<T> : IEnumerator<T> 
    {
        private LuwareList<T> elements = new LuwareList<T>();
        private int previousVersion = 0;
        private int index;

        public LuwareIEnumerator(LuwareList<T> elements) 
        {
            previousVersion = elements.version;
            this.elements = elements;
            this.index = -1;
        }

        public bool MoveNext() 
        {
            EnsureCollectionWasNotModified();
            index++;

            return index < elements.Length;
        }

        public void Reset() 
        {
            index = 0;
        }

        public T Current 
        {
            get 
            {
                EnsureCollectionWasNotModified();
                return elements.ElementAt(index);
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose() {
            elements = null;
            index = 0;
        }

        private void EnsureCollectionWasNotModified() {
            if (previousVersion != elements.version)
                throw new InvalidOperationException("You can't change the list while iterating.");
        }
    }
}
