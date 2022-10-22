using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.AddContext
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public object User { get; internal set; }
        public DbSet<NoteEntity> Note { get; set; }



    }
}