﻿using CommanLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface ILabelBL
    {
        public LabelResponseModel CreateLable(long notesId, long jwtUserId, LabelModel model);
        public IEnumerable<LabelEntity> GetAllLable(long jwtUserId);
        public LabelResponseModel GetLableWithId(long lableId, long jwtUserId);
       public LabelEntity GetLablesWithId(long lableId, long jwtUserId);
        public LabelResponseModel UpdateLable(LabelEntity updateLable, UpdateLableModel model, long jwtUserId);
        public void DeleteLable(LabelEntity lable, long jwtUserId);
        public IEnumerable<LabelEntity> GetlabelsUsingNoteid(long noteid, long userid);

        public IEnumerable<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName);
     
    }
}

