using CommanLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface INoteBL
    {
        public NoteEntity AddNote(NoteModel notes, long userid);
        public NoteEntity DeleteNote(long NoteId);
        public NoteModel UpdateNote(NoteModel noteModel, long noteId);
        public bool Pinned(long NoteID, long userId);
        public bool Trashed(long NoteID, long userId);
        public bool Archieved(long NoteID, long userId);
        public NoteEntity ColorNote(long NoteId, string color);
        public string Imaged(long NoteID, long userId, IFormFile image);
        public IEnumerable<NoteEntity> GetAllNotes();
        public IEnumerable<NoteEntity> GetAllNotesbyuserID(long userid);


    }
}
