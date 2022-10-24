using CommanLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface INoteBL
    {
        public NoteEntity Addnote(NoteModel note, long UserId);
    }
}
