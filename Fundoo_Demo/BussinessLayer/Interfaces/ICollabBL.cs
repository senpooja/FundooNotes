using CommanLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
   public interface ICollabBL
    {
        public CollabResponseModel AddCollaborate(long notesId, long jwtUserId, CollaboratedModel model);
        public void DeleteCollab(CollaboratorEntity collab);
        public CollaboratorEntity GetCollabWithId(long collabId);

        public IEnumerable<CollaboratorEntity> GetCollab(long userID);
    }
}
