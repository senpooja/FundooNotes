using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommanLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Services.Account;
using RepositoryLayer.AddContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Account = CloudinaryDotNet.Account;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        public readonly Context context;
        private readonly IConfiguration Config;
       private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NoteRL(Context context, IConfiguration Config, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.context = context;
            this.Config = Config;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        public NoteEntity AddNote(NoteModel notes, long userid)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = notes.Title;
                noteEntity.Note = notes.Note;
                noteEntity.Color = notes.Color;
                noteEntity.Image = notes.Image;
                noteEntity.IsArchive = notes.IsArchive;
                noteEntity.IsPin = notes.IsPin;
                noteEntity.IsTrash = notes.IsTrash;
                noteEntity.userid = userid;
                // noteEntity.Created = notes.Created;
                this.context.Notes.Add(noteEntity);
                int result = this.context.SaveChanges();

                if (result > 0)
                {
                    return noteEntity;
                }
                return null;

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
                var deleteNote = context.Notes.Where(x => x.NoteID == NoteId).FirstOrDefault();
                if (deleteNote != null)
                {
                    context.Notes.Remove(deleteNote);
                    context.SaveChanges();
                    return deleteNote;
                }

                return null;


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
                var update = context.Notes.Where(x => x.NoteID == noteId).FirstOrDefault();
                if (update != null)

                {
                    update.Title = noteModel.Title;
                    update.Note = noteModel.Note;
                    update.IsArchive = noteModel.IsArchive;
                    update.Color = noteModel.Color;
                    update.Image = noteModel.Image;
                    update.IsPin = noteModel.IsPin;
                    update.IsTrash = noteModel.IsTrash;
                    context.Notes.Update(update);
                    context.SaveChanges();
                    return noteModel;


                }
                return null;



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
                var result = context.Notes.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();

                result.IsPin = !result.IsPin;
                context.SaveChanges();
                return result.IsPin;

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
                var result = context.Notes.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();

                result.IsTrash = !result.IsTrash;
                context.SaveChanges();
                return result.IsTrash;

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
                var result = context.Notes.Where(r => r.userid == userId && r.NoteID == NoteID).FirstOrDefault();
                result.IsArchive = !result.IsArchive;
                context.SaveChanges();
                return result.IsArchive;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity ColorNote(long NoteId, string color)
        {
            var result = context.Notes.Where(r => r.NoteID == NoteId).FirstOrDefault();
            if (result != null)
            {

                result.Color = color;
                context.Notes.Update(result);
                context.SaveChanges();
                return result;

            }
            else
            {
                return null;
            }
        }
        public string Imaged(long NoteID, long userId, IFormFile image)
        {
            try
            {
                var result = context.Notes.Where(x => x.userid == userId && x.NoteID == NoteID).FirstOrDefault();
                if (result != null)
                {
                    Account account = new Account(
                        "dcsij276d",        // CLOUD_NAME,API_KEY,API_SECRET
                         "196186383952518",
                         "2AxVfJ1_VD8N__j6wZ9-BxJRikw"
                        );

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, image.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParameters);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = image.FileName;
                    // result.Image = imagePath;
                    context.SaveChanges();
                    return "Image Upload Successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            return context.Notes.ToList();
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserID(long userid)
        {
            return context.Notes.Where(n => n.userid == userid).ToList();
        }

    }
}