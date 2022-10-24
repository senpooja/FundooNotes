using BussinessLayer.Interfaces;
using CommanLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Fundoo_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class noteController : ControllerBase
    {
        INoteBL Notebl;
        public noteController(INoteBL Notebl)
        {
            this.Notebl = Notebl;
        }
        [HttpPost("Add")]
        public IActionResult Addnote(NoteModel addnote)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "userid"));
                var result = Notebl.Addnote(addnote, userid);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "NoteAdded Successfully", Response = result });


                }
                return this.BadRequest(new { Success = false, message = "NoteAdding is unsuccessfully", Response });

            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
