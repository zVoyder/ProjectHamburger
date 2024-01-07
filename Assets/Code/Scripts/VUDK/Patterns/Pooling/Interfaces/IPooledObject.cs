namespace VUDK.Patterns.Pooling.Interfaces
{
    using VUDK.Patterns.Pooling;

    public interface IPooledObject
    {
        public Pool RelatedPool { get; }

        /// <summary>
        /// Associates the Object with a <see cref="Pool"/>.
        /// </summary>
        /// <param name="associatedPool"></param>
        public void AssociatePool(Pool associatedPool);

        /// <summary>
        /// Disposes the object and returns it to the <see cref="Pool"/>.
        /// </summary>
        public void Dispose();

        /// <summary>
        /// Called on disposed, clears the object from its alterations.
        /// </summary>
        public void Clear();
    }
}