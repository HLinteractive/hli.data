// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.IAddRepository.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace HLI.Data.Interfaces.Repositories
{
    /// <summary>
    ///     Repository with Add method
    /// </summary>
    public interface IAddRepository<T>
        where T : class, new()
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Adds the specified object to context
        /// </summary>
        /// <param name="item">Item to add</param>
        void Add(T item);

        #endregion
    }
}