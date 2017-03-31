// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.IFindRepository.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace HLI.Data.Interfaces.Repositories
{
    /// <summary>
    ///     Repository allowing you to find a specific item
    /// </summary>
    public interface IFindRepository<T>
        where T : class, new()
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Find the specific item
        /// </summary>
        /// <param name="id">Id of item to find</param>
        /// <returns>The matching item</returns>
        T FindById(object id);

        #endregion
    }
}