using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Application.Filters
{
    public class RequestParameters
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public RequestParameters()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public RequestParameters(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}