using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Common.Service
{
    public interface INoteServices
    {
        IEnumerable<NoteEx> GetNotes();
      
        NoteEx GetNote(long id);
        
        NoteEx Create();

        NoteEx Add(NoteEx note);

        NoteEx Update(NoteEx note);

        void Remove(long id);

        IEnumerable<NoteEx> SearchNote(string parameter);
    }
}