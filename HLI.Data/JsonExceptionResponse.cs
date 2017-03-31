// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.JsonExceptionResponse.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace HLI.Data
{
    /// <summary>
    ///     Represents a JSON response that occurs when there is an exception
    /// </summary>
    /// <example>
    ///     <code>
    ///     var json = await response.Content.ReadAsAsync&lt;JsonExceptionResponse&gt;();
    /// </code>
    /// </example>
    public class JsonExceptionResponse
    {
        #region Public Properties

        public string ExceptionMessage { get; set; }

        public string ExceptionType { get; set; }

        public string Message { get; set; }

        public string StackTrace { get; set; }

        #endregion

        #region Public Methods and Operators

        public override string ToString()
        {
            return
                $"{this.GetType().Name}: ExceptionMessage: {this.ExceptionMessage}, ExceptionType: {this.ExceptionType}, Message: {this.Message}, StackTrace: {this.StackTrace}";
        }

        #endregion
    }
}