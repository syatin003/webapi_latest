using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Common.Repository
{
    public interface INoteRepository : IRepository<NoteEx>
    {
        IEnumerable<NoteEx> GetNotes();

        NoteEx GetNote(long id);
        
        IEnumerable<NoteEx> SearchNote(string parameter);

    }
}