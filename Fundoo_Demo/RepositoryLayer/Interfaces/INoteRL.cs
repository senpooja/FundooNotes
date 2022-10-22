using CommanLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NoteModel notes, long userid);
    }
}
