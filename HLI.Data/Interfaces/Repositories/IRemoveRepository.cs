// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.IRemoveRepository.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace HLI.Data.Interfaces.Repositories
{
    /// <summary>
    ///     Repository with Add method
    /// </summary>
    public interface IRemoveRepository<T>
        where T : class, new()
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Removes the specified object from context
        /// </summary>
        /// <param name="item">Item to remove</param>
        void Remove(T item);

        #endregion
    }
}