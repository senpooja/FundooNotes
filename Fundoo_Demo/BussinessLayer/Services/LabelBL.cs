using BussinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using CommanLayer.Models;

namespace BussinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
     

        ILabelRL ilabelRL;

        public LabelBL(ILabelRL ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }



        public LabelResponseModel CreateLable(long notesId, long jwtUserId, LabelModel model)
        {

            try
            {

                return ilabelRL.CreateLable(notesId, jwtUserId, model);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public void DeleteLable(LabelEntity lable, long jwtUserId)
        {

            try
            {
                ilabelRL.DeleteLable(lable, jwtUserId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<LabelEntity> GetAllLable(long jwtUserId)
        {

            try
            {

                return ilabelRL.GetAllLable(jwtUserId);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public LabelEntity GetLablesWithId(long lableId, long jwtUserId)
        {

            try
            {

                return ilabelRL.GetLablesWithId(lableId, jwtUserId);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public LabelResponseModel GetLableWithId(long lableId, long jwtUserId)
        {

            try
            {

                return ilabelRL.GetLableWithId(lableId, jwtUserId);

            }
            catch (Exception)
            {

                throw;
            }

        }

        public LabelResponseModel UpdateLable(LabelEntity updateLable, UpdateLableModel model, long jwtUserId)
        {

            try
            {

                return ilabelRL.UpdateLable(updateLable, model, jwtUserId);

            }
            catch (Exception)
            {

                throw;
            }

        }
        public IEnumerable<LabelEntity> GetlabelsUsingNoteid(long noteid, long userid)
        {
            try
            {
                return this.ilabelRL.GetlabelsUsingNoteid(noteid, userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return this.ilabelRL.RenameLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}