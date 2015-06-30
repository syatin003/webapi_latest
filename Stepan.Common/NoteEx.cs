using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Common
{
    public class NoteEx
    {
        public long Note_Id { get; set; }
        public string Note_Name { get; set; }
        public long Note_ProductId { get; set; }
        public long Note_PageId { get; set; }
        public string Note_Content { get; set; }
        public DateTime Note_CreatedDate { get; set; }
        public Nullable<DateTime> Note_ModifiedDate { get; set; }
    }
}