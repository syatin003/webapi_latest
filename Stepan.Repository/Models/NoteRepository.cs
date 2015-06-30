using Stepan.Common.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Stepan.Common;
namespace Stepan.Repository.Models
{
    public class NoteRepository : INoteRepository
    {
        StepanEktron_oldEntities db = new StepanEktron_oldEntities();
     
        public IEnumerable<NoteEx> GetNotes()
        {
            var _notes = db.Notes.ToList();
            return Transform(_notes);
        }

        public NoteEx GetNote(long id)
        {         
             var _note = db.Notes.Where(x => x.Note_Id == id).FirstOrDefault();
             return Transform(_note);           
        }

        public IEnumerable<NoteEx> SearchNote(string parameter)
        {
           var _notes = db.Notes
                    .Where(x => x.Note_Name.Contains(parameter) || x.Note_Content.Contains(parameter))
                    .ToList();

           return Transform(_notes);
        }

        public NoteEx Create()
        {
            return new NoteEx();
        }

        public NoteEx Add(NoteEx item)
        {
            try
            {
                Note _Note = new Note();
                _Note.Note_Name = item.Note_Name;
                _Note.Note_PageId = item.Note_PageId;
                _Note.Note_ProductId = item.Note_ProductId;
                _Note.Note_Content = item.Note_Content;
                _Note.Note_CreatedDate = DateTime.Now;
                _Note.Note_ModifiedDate = DateTime.Now;

                db.Notes.Add(_Note);
                db.SaveChanges();
                return item;
            }
            catch(Exception exp)
            {
                throw exp;
            }
        }

        public NoteEx Update(NoteEx note)
        {
            try
            {
                Note _Note = db.Notes.Find(note.Note_Id);
                _Note.Note_Name = note.Note_Name;
                _Note.Note_ProductId = note.Note_ProductId;
                _Note.Note_PageId = note.Note_PageId;
                _Note.Note_Content = note.Note_Content;
                _Note.Note_ModifiedDate = DateTime.Now;
                
                db.Entry(_Note).State = EntityState.Modified;
                db.SaveChanges();

                return Transform(_Note);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void Remove(long Id)
        {
            try
            {
                Note note = db.Notes.Find(Id);
                db.Notes.Remove(note);
                db.SaveChanges();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        private NoteEx Transform(Note note)
        {
            var _note = new NoteEx
            {
                Note_Content =note.Note_Content,
                Note_CreatedDate=note.Note_CreatedDate,
                Note_Id=note.Note_Id,
                Note_ModifiedDate=DateTime.Now,
                Note_Name=note.Note_Name,
                Note_PageId=note.Note_PageId,
                Note_ProductId=note.Note_ProductId
            };
            return _note;
        }

        private Note Transform(NoteEx note)
        {
            var _note = new Note
            {
                Note_Content = note.Note_Content,
                Note_CreatedDate = note.Note_CreatedDate,
                Note_Id = note.Note_Id,
                Note_ModifiedDate = DateTime.Now,
                Note_Name = note.Note_Name,
                Note_PageId = note.Note_PageId,
                Note_ProductId = note.Note_ProductId
            };
            return _note;
        }

        private IEnumerable<NoteEx> Transform(List<Note> Notes)
        {
            List<NoteEx> _Notes = new List<NoteEx>();

            foreach (Note Note in Notes)
            {
                _Notes.Add(Transform(Note));
            }

            return _Notes;
        }
    }
}