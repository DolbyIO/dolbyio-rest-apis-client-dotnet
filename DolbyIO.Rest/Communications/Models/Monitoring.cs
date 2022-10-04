using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models
{
    public abstract class AllElementsOptions
    {
        /// <summary>
        /// Gets or sets the beginning of the time range (in milliseconds that have elapsed since epoch).
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the end of the time range (in milliseconds that have elapsed since epoch).
        /// </summary>
        public int? To { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of elements to return per page. We recommend setting the proper value of this parameter to shorten the response time.
        /// </summary>
        public int? PageSize { get; set; }
    }

    public abstract class PagedOptions
    {
        /// <summary>
        /// Gets or sets the beginning of the time range (in milliseconds that have elapsed since epoch).
        /// </summary>
        public int? From { get; set; }

        /// <summary>
        /// Gets or sets the end of the time range (in milliseconds that have elapsed since epoch).
        /// </summary>
        public int? To { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of displayed results. We recommend setting the proper value of this parameter to shorten the response time.
        /// </summary>
        public int? Max { get; set; }

        /// <summary>
        /// Gets or sets when the results span multiple pages, use this option to navigate through pages. By default, only the max number of results is displayed.
        /// To see the next results, set the start parameter to the value of the next key returned in the previous response.
        /// </summary>
        public string Start { get; set; }
    }

    public abstract class PagedResponse
    {
        /// <summary>
        /// Gets or sets the token representing the first page of displayed results.
        /// Use this token as a value of the start parameter to return to the first page of the displayed results.
        /// </summary>
        [JsonProperty("first")]
        public string First { get; set; }

        /// <summary>
        /// Gets or sets the token representing the next page of displayed results.
        /// Use this token as a value of the start parameter to access the next page of results.
        /// </summary>
        [JsonProperty("next")]
        public string Next { get; set; }
    }
}
