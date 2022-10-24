using CommanLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AddContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        public readonly Context context;
        private readonly IConfiguration Config;

        public NoteRL(Context context, IConfiguration Config)
        {
            this.context = context;
            this.Config = Config;
        }
        public NoteEntity Addnote(NoteModel notes, long userid)
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
                this.context.Note.Add(noteEntity);
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
    }
}