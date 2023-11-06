    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.ViewModel.Common
{

    public class PagingRequestBase :RequestBase
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
