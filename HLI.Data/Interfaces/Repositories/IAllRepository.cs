// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.IAllRepository.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace HLI.Data.Interfaces.Repositories
{
    /// <summary>
    ///     Repository allowing you to get all items from context
    /// </summary>
    public interface IAllRepository<T>
        where T : class, new()
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Retrive all items from context
        /// </summary>
        /// <returns>
        ///     <see cref="IEnumerable{T}" />
        /// </returns>
        IEnumerable<T> All();

        #endregion
    }
}