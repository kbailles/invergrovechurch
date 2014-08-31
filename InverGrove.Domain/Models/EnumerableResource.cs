using System.Collections;
using System.Collections.Generic;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Models
{
    public abstract class EnumerableResource<T> : Resource, IList<T>
        where T : Resource
    {
        protected List<T> internalList = new List<T>();

        private bool isReadOnly;

        /// <summary>
        ///   Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns> A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection. </returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.internalList.GetEnumerator();
        }

        /// <summary>
        ///   Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns> An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection. </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.internalList.GetEnumerator();
        }

        /// <summary>
        ///   Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <param name="item"> The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is read-only.</exception>
        public void Add(T item)
        {
            this.internalList.Add(item);
        }

        /// <summary>
        ///   Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is read-only.</exception>
        public void Clear()
        {
            this.internalList.Clear();
        }

        /// <summary>
        ///   Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
        /// </summary>
        /// <param name="item"> The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
        /// <returns> true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" /> ; otherwise, false. </returns>
        public bool Contains(T item)
        {
            return this.internalList.Contains(item);
        }

        /// <summary>
        ///   Copies to.
        /// </summary>
        /// <param name="array"> The array. </param>
        /// <param name="arrayIndex"> Index of the array. </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///   Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <param name="item"> The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" /> . </param>
        /// <returns> true if <paramref name="item" /> was successfully removed from the <see
        ///    cref="T:System.Collections.Generic.ICollection`1" /> ; otherwise, false. This method also returns false if <paramref
        ///    name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" /> . </returns>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.ICollection`1" />
        ///   is read-only.</exception>
        public bool Remove(T item)
        {
            return this.internalList.Remove(item);
        }

        /// <summary>
        ///   Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> .
        /// </summary>
        /// <returns> The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" /> . </returns>
        public int Count
        {
            get { return this.internalList.Count; }
        }

        /// <summary>
        ///   Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
        /// </summary>
        /// <returns> true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false. </returns>
        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
            private set { this.isReadOnly = value; }
        }

        /// <summary>
        ///   Determines the index of a specific item in the <see cref="T:System.Collections.Generic.IList`1" /> .
        /// </summary>
        /// <param name="item"> The object to locate in the <see cref="T:System.Collections.Generic.IList`1" /> . </param>
        /// <returns> The index of <paramref name="item" /> if found in the list; otherwise, -1. </returns>
        public int IndexOf(T item)
        {
            return this.internalList.IndexOf(item);
        }

        /// <summary>
        ///   Inserts an item to the <see cref="T:System.Collections.Generic.IList`1" /> at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index at which <paramref name="item" /> should be inserted. </param>
        /// <param name="item"> The object to insert into the <see cref="T:System.Collections.Generic.IList`1" /> . </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" />
        ///   is not a valid index in the
        ///   <see cref="T:System.Collections.Generic.IList`1" />
        ///   .</exception>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.IList`1" />
        ///   is read-only.</exception>
        public void Insert(int index, T item)
        {
            this.internalList.Insert(index, item);
        }

        /// <summary>
        ///   Removes the <see cref="T:System.Collections.Generic.IList`1" /> item at the specified index.
        /// </summary>
        /// <param name="index"> The zero-based index of the item to remove. </param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" />
        ///   is not a valid index in the
        ///   <see cref="T:System.Collections.Generic.IList`1" />
        ///   .</exception>
        /// <exception cref="T:System.NotSupportedException">The
        ///   <see cref="T:System.Collections.Generic.IList`1" />
        ///   is read-only.</exception>
        public void RemoveAt(int index)
        {
            this.internalList.RemoveAt(index);
        }

        /// <summary>
        ///   Gets or sets the element at the specified index.
        /// </summary>
        /// <returns> The element at the specified index. </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///   <paramref name="index" />
        ///   is not a valid index in the
        ///   <see cref="T:System.Collections.Generic.IList`1" />
        ///   .</exception>
        /// <exception cref="T:System.NotSupportedException">The property is set and the
        ///   <see cref="T:System.Collections.Generic.IList`1" />
        ///   is read-only.</exception>
        public T this[int index]
        {
            get { return this.internalList[index]; }
            set { this.internalList[index] = value; }
        }
    }
}