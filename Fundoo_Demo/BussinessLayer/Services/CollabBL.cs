using BussinessLayer.Interfaces;
using CommanLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
 
    
        public class CollabBL : ICollabBL
        {
             public ICollabRL IcollabRL;

            public CollabBL(ICollabRL icollabRL)
            {
                this.IcollabRL = icollabRL;
            }
        public CollabResponseModel AddCollaborate(long notesId, long jwtUserId, CollaboratedModel model)
        {

            try
            {
                return IcollabRL.AddCollaborate(notesId, jwtUserId, model);
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

                IcollabRL.DeleteCollab(collab);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public CollaboratorEntity GetCollabWithId(long collabId)
        {

            try
            {
                return IcollabRL.GetCollabWithId(collabId);
            }
            catch (Exception)
            {

                throw;
            }

        }



        public IEnumerable<CollaboratorEntity> GetCollab(long userID)
        {
            try
            {
                return IcollabRL.GetCollab(userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    }
