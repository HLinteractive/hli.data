// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.IRepository.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace HLI.Data.Interfaces.Repositories
{
    /// <summary>
    ///     A repository allowing standard operations against context
    /// </summary>
    public interface IRepository<T> : IAddRepository<T>, IAllRepository<T>, IFindRepository<T>, IRemoveRepository<T>
        where T : class, new()
    {
    }
}