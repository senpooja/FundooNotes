using CommanLayer.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.Extensions.Configuration;
using Polly;
using RepositoryLayer.AddContext;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Context = RepositoryLayer.AddContext.Context;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {
        public readonly Context context;
        public readonly IConfiguration Iconfiguration;
        public CollabRL(Context context, IConfiguration Iconfigration)
        {
            this.context = context;
            this.Iconfiguration = Iconfigration;
        }
        public CollabResponseModel AddCollaborate(long notesId, long jwtUserId, CollaboratedModel model)
        {
            try
            {
                var validNotesAndUser = this.context.Users.Where(e => e.UserId == jwtUserId);
                CollaboratorEntity collaborate = new CollaboratorEntity();

                collaborate.NoteID = notesId;
                collaborate.userid = jwtUserId;
                collaborate.CollaboratedEmail = model.Collaborated_Email;

                context.Add(collaborate);
                context.SaveChanges();

                CollabResponseModel responseModel = new CollabResponseModel();

                responseModel.CollaboratorID = collaborate.CollaboratorID;
                responseModel.noteID = collaborate.NoteID;
               // responseModel.UserId = collaborate.userid;
                responseModel.CollaboratedEmail = collaborate.CollaboratedEmail;

                return responseModel;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteCollab(CollaboratorEntity collab)
        {
            try
            {

                this.context.CollaboratorTable.Remove(collab);
                this.context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public IEnumerable<CollaboratorEntity> GetAllByNoteID(long noteID)
        {
            return context.CollaboratorTable.Where(n => n.NoteID == noteID).ToList();
        }
       
        }
    }

