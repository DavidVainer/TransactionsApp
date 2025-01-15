namespace TransactionsApp.Common.Services
{
    /// <summary>
    /// Generic interface for mapping object of type T to object of type K.
    /// </summary>
    /// <typeparam name="T">Object to map from.</typeparam>
    /// <typeparam name="K">Object to map to.</typeparam>
    public interface IMapper<T, K> where T : class where K : class
    {
        /// <summary>
        /// Maps object of type T to object of type K.
        /// </summary>
        /// <param name="source">Object to map from.</param>
        /// <returns>Mapped object.</returns>
        K Map(T source);
    }
}
