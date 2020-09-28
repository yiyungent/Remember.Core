using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class PageModel<T>
    {
        public IQueryable<T> Data { get; set; }

        public int TotalCount { get; set; }
    }
}
