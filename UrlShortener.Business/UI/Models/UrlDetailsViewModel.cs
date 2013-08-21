using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Business.UI.Models
{
    public class UrlDetailsViewModel
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public Uri Url { get; set; }

        [DisplayName("Request Count")]
        public long RequestCount { get; set; }

        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }

        [DisplayName("Last Request")]
        public DateTimeOffset LastRequest { get; set; }

        public string GetCreatedTimestamp()
        {
            return FormatTimestampForView(Created);
        }

        public string GetUpdatedTimestamp()
        {
            return FormatTimestampForView(Updated);
        }

        public string GetLastRequestTimestamp()
        {
            return FormatTimestampForView(LastRequest);
        }

        #region Private Methods

        private string FormatTimestampForView(DateTimeOffset timestamp)
        {
            if (timestamp > DateTimeOffset.MinValue) return timestamp.ToString("G");

            return "N/A";
        }

        #endregion Private Methods
    }
}
