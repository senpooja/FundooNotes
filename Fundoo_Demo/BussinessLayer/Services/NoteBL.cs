using BussinessLayer.Interfaces;
using CommanLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL NoteRL;
        public NoteBL(INoteRL NoteRL)
        {

            this.NoteRL = NoteRL;
         }
        public NoteEntity AddNote(NoteModel notes, long userid)
        {
            try
            {


                return this.NoteRL.AddNote(notes, userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NoteEntity DeleteNote(long NoteId)
        {
            try
            {
                return NoteRL.DeleteNote(NoteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteModel UpdateNote(NoteModel noteModel, long noteId)
        {
            try
            {
                return NoteRL.UpdateNote(noteModel, noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Pinned(long NoteID, long userId)
        {
            try
            {
                return NoteRL.Pinned(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Trashed(long NoteID, long userId)
        {
            try
            {
                return NoteRL.Trashed(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Archieved(long NoteID, long userId)
        {
            try
            {
                return NoteRL.Archieved(NoteID, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity ColorNote(long NoteId, string color)
        {
            try
            {
                return NoteRL.ColorNote(NoteId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Imaged(long NoteID, long userId, IFormFile image)
        {
            try
            {
                return NoteRL.Imaged(NoteID, userId, image);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            try
            {
                return this.NoteRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserID(long userid)
        {
            try
            {
                return this.NoteRL.GetAllNotesbyuserID(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
