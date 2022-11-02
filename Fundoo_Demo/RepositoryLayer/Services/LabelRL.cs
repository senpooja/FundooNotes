using CommanLayer.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Context = RepositoryLayer.AddContext.Context;


namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {

       private readonly Context context;

        public LabelRL(Context context)
        {
            this.context = context;
        }




        public LabelResponseModel CreateLable(long notesId, long jwtUserId, LabelModel model)
        {
            try
            {
                
               var validNotesAndUser = this.context.Users.Where(e => e.UserId == jwtUserId);

                if (validNotesAndUser != null)
                {
                    LabelEntity label = new LabelEntity();

                    label.noteID = notesId;
                    label.UserId = jwtUserId;
                    label.LabelName = model.LabelName;

                    this.context.Add(label);
                    this.context.SaveChanges();

                    LabelResponseModel responseModel = new LabelResponseModel();

                    responseModel.LabelID = label.LabelID;
                    responseModel.NoteID = label.noteID;
                    responseModel.UserID = label.UserId;
                    responseModel.LabelName = label.LabelName;

                    return responseModel;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public IEnumerable<LabelEntity> GetAllLable(long jwtUserId)
        {
            try
            {

                var result = this.context.Lable.Where(x => x.UserId == jwtUserId);
                return result;

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public LabelResponseModel GetLableWithId(long lableId, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.Users.Where(e => e.UserId == jwtUserId);

                var response = this.context.Lable.FirstOrDefault(e => e.LabelID == lableId && e.UserId == jwtUserId);

                if (validUserId != null && response != null)
                {


                    LabelResponseModel model = new LabelResponseModel();

                    model.LabelID = response.LabelID;
                    model.NoteID = response.noteID;
                    model.UserID = response.UserId;
                    model.LabelName = response.LabelName;

                    return model;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public LabelEntity GetLablesWithId(long lableId, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.Users.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    return this.context.Lable.FirstOrDefault(e => e.LabelID == lableId);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public LabelResponseModel UpdateLable(LabelEntity updateLable, UpdateLableModel model, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.Users.Where(e => e.UserId == jwtUserId);

                var response = this.context.Lable.FirstOrDefault(e => e.LabelID == updateLable.LabelID);

                if (validUserId != null && response != null)
                {
                    updateLable.LabelName = model.LabelName;
                    updateLable.noteID = model.NoteID;

                    this.context.SaveChanges();


                    LabelResponseModel models = new LabelResponseModel();

                    models.LabelID = response.LabelID;
                    models.NoteID = response.noteID;
                    models.UserID = response.UserId;
                    models.LabelName = response.LabelName;

                    return models;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public void DeleteLable(LabelEntity lable, long jwtUserId)
        {
            try
            {
                var validUserId = this.context.Users.Where(e => e.UserId == jwtUserId);
                if (validUserId != null)
                {
                    this.context.Lable.Remove(lable);
                    this.context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<LabelEntity> GetlabelsUsingNoteid(long noteid, long userid)
        {
            return context.Lable.Where(e => e.noteID == noteid && e.UserId == userid).ToList();
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                var result = this.context.Lable.FirstOrDefault(x => x.UserId == userID && x.LabelName == labelName);
                if (result != null)
                {
                    context.Remove(result);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<LabelEntity> labels;
            labels = context.Lable.Where(x => x.UserId == userID && x.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var newlabel in labels)
                {
                    newlabel.LabelName = labelName;
                }
                context.SaveChanges();
                return labels;
            }
            return null;
        }
    }


}
