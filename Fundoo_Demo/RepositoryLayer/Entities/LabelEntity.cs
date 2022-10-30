using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entities
{
    public class LabelEntity
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelID { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("note")]
        public long noteID { get; set; }
        [JsonIgnore]
        public virtual NoteEntity note { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }

    }
}
    

