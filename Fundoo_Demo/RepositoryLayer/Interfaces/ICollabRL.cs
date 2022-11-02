using CommanLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
   public interface ICollabRL
    {
        public CollabResponseModel AddCollaborate(long notesId, long jwtUserId, CollaboratedModel model);
        public void DeleteCollab(CollaboratorEntity collab);
      //  public CollaboratorEntity GetCollabWithId(long collabId);

        public IEnumerable<CollaboratorEntity> GetAllByNoteID(long noteID);
    }
}
