// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="HLI.Data.Tests.Integration.HttpBinPost.cs" company="HL Interactive">
// //   Copyright © HL Interactive, Stockholm, Sweden, 2015
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace HLI.Data.Tests.Models
{
    public class HttpBinPost
    {
        #region Public Properties

        public Args args { get; set; }

        public string data { get; set; }

        public Files files { get; set; }

        public Form form { get; set; }

        public Headers headers { get; set; }

        public object json { get; set; }

        public string origin { get; set; }

        public string url { get; set; }

        #endregion
    }

    public class Args
    {
    }

    public class Files
    {
    }

    public class Form
    {
    }

    public class Headers
    {
        #region Public Properties

        public string Accept { get; set; }

        public string AcceptEncoding { get; set; }

        public string AcceptLanguage { get; set; }

        public string CacheControl { get; set; }

        public string ContentLength { get; set; }

        public string Host { get; set; }

        public string Origin { get; set; }

        public string PostmanToken { get; set; }

        public string UserAgent { get; set; }

        #endregion
    }
}