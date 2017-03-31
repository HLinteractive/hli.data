// ReSharper disable ClassNeverInstantiated.Global - JSON
namespace HLI.Data.Tests.Models
{
    /// <summary>
    ///     Result class for Google Places API call: https://developers.google.com/places/web-service/search
    /// </summary>
    public class GoogleLocation
    {
        #region Public Properties

        public object[] html_attributions { get; set; }

        public string next_page_token { get; set; }

        public Result[] results { get; set; }

        public string status { get; set; }

        #endregion

        public class Geometry
        {
            #region Public Properties

            public Location location { get; set; }

            public Viewport viewport { get; set; }

            #endregion
        }

        public class Location
        {
            #region Public Properties

            public float lat { get; set; }

            public float lng { get; set; }

            #endregion
        }

        public class Northeast
        {
            #region Public Properties

            public float lat { get; set; }

            public float lng { get; set; }

            #endregion
        }

        public class Opening_Hours
        {
            #region Public Properties

            public bool open_now { get; set; }

            public object[] weekday_text { get; set; }

            #endregion
        }

        public class Photo
        {
            #region Public Properties

            public int height { get; set; }

            public string[] html_attributions { get; set; }

            public string photo_reference { get; set; }

            public int width { get; set; }

            #endregion
        }

        public class Result
        {
            #region Public Properties

            public Geometry geometry { get; set; }

            public string icon { get; set; }

            public string id { get; set; }

            public string name { get; set; }

            public Opening_Hours opening_hours { get; set; }

            public Photo[] photos { get; set; }

            public string place_id { get; set; }

            public float rating { get; set; }

            public string reference { get; set; }

            public string scope { get; set; }

            public string[] types { get; set; }

            public string vicinity { get; set; }

            #endregion
        }

        public class Southwest
        {
            #region Public Properties

            public float lat { get; set; }

            public float lng { get; set; }

            #endregion
        }

        public class Viewport
        {
            #region Public Properties

            public Northeast northeast { get; set; }

            public Southwest southwest { get; set; }

            #endregion
        }
    }
}