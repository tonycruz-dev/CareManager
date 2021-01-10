using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EntityHelpers
{
    public class JobRequestParams
    {
        private const int _maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > _maxPageSize) ? _maxPageSize : value; }
        }

        public string UserId { get; set; }
        public int AgencyId { get; set; }
        public string Sort { get; set; }
        [JsonSchemaDate]
        public DateTime?[] DateRange { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public string OrderBy { get; set; }
        private string _search;
        public string Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
