using BussinessLayer.Interfaces;
using CommanLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Fundoo_Demo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class noteController : ControllerBase
    {
        INoteBL Notebl;
        public noteController(INoteBL Notebl)
        {
            this.Notebl = Notebl;
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
                    return Ok(new { success = true, message = "Note added successfully", Response = result });
                }

                return BadRequest(new { success = false, message = "Note not added", });
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
                    return this.Ok(new { Success = true, message = "Notes Deleted Successfully" });
                }
                else
                {
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
                    return this.Ok(new { Success = true, message = "Note updated successfully", data = notesModel });
                }
                else
                {
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
                    return this.Ok(new { Success = true, message = "Note Pinned Successfully", data = result });
                }
                else if (result == false)
                {
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
                    return this.Ok(new { Success = true, message = "Trashed Successfully", data = result });
                }
                else if (result == false)
                {
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
        [Route("Color")]
        public IActionResult ColourNote(long NoteId, string color)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var colors = Notebl.ColorNote(NoteId, color);
                if (colors != null)
                {
                    return Ok(new { Success = true, message = "Added Colour Successfully", data = colors });
                }
                else
                {
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
                    return Ok(new { Status = true, Message = "Image Uploaded Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new { Status = true, Message = "Image Uploaded Unsuccessfully", Data = result });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
