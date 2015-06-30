using Stepan.Common;
using Stepan.Common.Repository;
using Stepan.Common.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stepan.Service
{
    public class NoteService : INoteServices
    {
        readonly INoteRepository repo;
        public NoteService(INoteRepository repo)
        {
            this.repo = repo;
        }
        public IEnumerable<NoteEx> GetNotes()
        {
            return repo.GetNotes();
        }
        public NoteEx GetNote(long id)
        {
            return repo.GetNote(id);
        }

        public IEnumerable<NoteEx> SearchNote(string parameter)
        {
            return repo.SearchNote(parameter);
        }


        public NoteEx Create()
        {
          return repo.Create();
        }

        public NoteEx Add(NoteEx note)
        {
            return repo.Add(note);
        }

        public NoteEx Update(NoteEx note)
        {
            return repo.Update(note);
        }

        public void Remove(long Id)
        {
            repo.Remove(Id);
        }
    }
}