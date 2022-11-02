using BussinessLayer.Interfaces;
using CommanLayer.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Context = RepositoryLayer.AddContext.Context;

namespace Fundoo_Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class noteController : ControllerBase
    {
        INoteBL Notebl;
        
        public readonly Context context;
        private readonly IConfiguration Config;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<UserController> _logger;
        public noteController(INoteBL Notebl, Context context, IMemoryCache memoryCache, IDistributedCache distributedCache, IConfiguration Config, ILogger<UserController> _logger)
        {
            this.Notebl = Notebl;
            this.context = context;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.Config = Config;
            this._logger = _logger;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNote(NoteModel noteModel)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notebl.AddNote(noteModel, userID);

                if (result != null)
                {
                    _logger.LogInformation("Note added successfully");
                    return Ok(new { success = true, message = "Note added successfully", Response = result });
                }
                else
                {
                    _logger.LogInformation("Note not added");
                    return BadRequest(new { success = false, message = "Note not added", });
                }
            }

            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long NoteId)
        {
            try
            {
                var delete = Notebl.DeleteNote(NoteId);
                if (delete != null)
                {
                    _logger.LogInformation("Notes Deleted Successfully");
                    return this.Ok(new { Success = true, message = "Notes Deleted Successfully" });
                }
                else
                {
                    _logger.LogInformation("Notes Deleted Unsuccessful");
                    return this.BadRequest(new { Success = false, message = "Notes Deleted Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateNotesOfUser(NoteModel notesModel, long noteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                NoteModel notes = this.Notebl.UpdateNote(notesModel, noteId);
                if (notes != null)
                {
                    _logger.LogInformation("Note updated successfully");
                    return this.Ok(new { Success = true, message = "Note updated successfully", data = notesModel });
                }
                else
                {
                    _logger.LogInformation("No Such Note Found");
                    return this.BadRequest(new { Success = false, message = "No Such Note Found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long NoteId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notebl.Pinned(NoteId, userID);
                if (result == true)
                {
                    _logger.LogInformation("Note Pinned Successfully");
                    return this.Ok(new { Success = true, message = "Note Pinned Successfully", data = result });
                }
                else if (result == false)
                {
                    _logger.LogInformation("Unpinned");
                    return this.Ok(new { Success = true, message = "Unpinned", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note Pinned Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long NotesId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notebl.Trashed(NotesId, userID);
                if (result == true)
                {
                    _logger.LogInformation("Trashed Successfully");
                    return this.Ok(new { Success = true, message = "Trashed Successfully", data = result });
                }
                else if (result == false)
                {
                    _logger.LogInformation("Untrashed");
                    return this.Ok(new { Success = true, message = "Untrashed", data = result });
                }

                else
                {
                    return this.BadRequest(new { Success = false, message = "Trashed unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
        [Route("Archive")]
        public IActionResult ArchiveNote(long NotesId)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notebl.Trashed(NotesId, userID);
                if (result == true)
                {
                    _logger.LogInformation("Archive Successfully");
                    return this.Ok(new { Success = true, message = "Archive Successfully", data = result });
                }
                else if (result == false)
                {
                    _logger.LogInformation("UnArchive");
                    return this.Ok(new { Success = true, message = "UnArchive", data = result });
                }

                else
                {
                    return this.BadRequest(new { Success = false, message = "Archive unsuccessfull" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPut]
        [Route("Color")]
        public IActionResult ColourNote(long NoteId, string color)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var colors = Notebl.ColorNote(NoteId, color);
                if (colors != null)
                {
                    _logger.LogInformation("Added Colour Successfully");
                    return Ok(new { Success = true, message = "Added Colour Successfully", data = colors });
                }
                else
                {
                    _logger.LogInformation("Added Colour UnSuccessfully");
                    return BadRequest(new { Success = false, message = "Added Colour Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut]
        [Route("Image")]
        public IActionResult Imaged(long noteId, IFormFile image)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = Notebl.Imaged(noteId, userID, image);
                if (result != null)
                {
                    _logger.LogInformation("Image Uploaded Successfully");
                    return Ok(new { Status = true, Message = "Image Uploaded Successfully", Data = result });
                }
                else
                {
                    _logger.LogInformation("Image Uploaded Unsuccessfully");
                    return BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessfully", Data = result });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("NotesByUserID")]
        public IEnumerable<NoteEntity> GetAllNotesbyuserID(long userid)
        {
            try
            {
                return Notebl.GetAllNotesbyuserID(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("AllNotes")]
        public IEnumerable<NoteEntity> GetAllNote()
        {
            try
            {
                _logger.LogInformation("read all notes");
                return Notebl.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")] 
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NoteList"; 
            string serializedCustomerList; 
            var NoteList = new List<NoteEntity>(); 
            var redisNoteList = await distributedCache.GetAsync(cacheKey);
            if (redisNoteList != null)
            {
                _logger.LogInformation("rediscaching successfull");
                serializedCustomerList = Encoding.UTF8.GetString(redisNoteList);
                NoteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedCustomerList); 
            }
            else 
            {
                _logger.LogInformation("rediscaching Unsuccessfully");
                //NoteList = await context.Notes.ToListAsync();
                NoteList = (List<NoteEntity>)Notebl.GetAllNotes();
                serializedCustomerList = JsonConvert.SerializeObject(NoteList); 
                redisNoteList = Encoding.UTF8.GetBytes(serializedCustomerList); 
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddMinutes(10)).SetSlidingExpiration(TimeSpan.FromMinutes(2)); 
                await distributedCache.SetAsync(cacheKey, redisNoteList, options); 
            }
            return Ok(NoteList); 
        }
      //  </list<NoteEntity></NoteEntity></IActionResult>
    }
}
