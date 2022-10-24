using CommanLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    //public class NoteBL:INoteRL
    //{
    //    INoteRL NoteRL;
    //    public NoteBL(INoteRL NotesRL)
    //    {
            
    //        this.NoteRL = NotesRL; 


    //    }


        public NoteEntity Addnote(NoteModel note, long userid)
        {
            try
            {
                return this.NoteRL.Addnote(note, userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
