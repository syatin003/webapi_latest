using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Common
{
    public class CreateNote
    {
        public string Note_Name { get; set; }
        public long Note_ProductId { get; set; }
        public long Note_PageId { get; set; }
        public string Note_Content { get; set; }
    }
}