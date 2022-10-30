using BussinessLayer.Interfaces;
using CommanLayer.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
        public class LabelController : ControllerBase
        {

            ILabelBL ilabelBL;

            public LabelController(ILabelBL ilabelBL)
            {
                this.ilabelBL = ilabelBL;
            }


            [HttpPost]
            [Route("Create")]
            public IActionResult CreateLable(long notesId, LabelModel model)
            {

                try
                {


                    long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                    if (jwtUserId == 0 && notesId == 0)
                    {
                        return BadRequest(new { Success = false, message = "Name Missing For Label" });
                    }
                    else
                    {

                        LabelResponseModel lable = ilabelBL.CreateLable(notesId, jwtUserId, model);

                        return Ok(new { Success = true, message = "Label Created", lable });

                    }


                }
                catch (Exception)
                {

                    throw;
                }


            }


            [HttpGet]
            [Route("ReadAll")]
            public IActionResult GetAllLabel()
            {

                try
                {

                    long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

                    var result = ilabelBL.GetAllLable(jwtUserId);

                    if (result != null)
                    {
                        return Ok(new { Success = true, message = "Retrived All labels ", result });

                    }
                    else
                    {
                        return BadRequest(new { Success = false, message = "No label in database " });

                    }



                }
                catch (Exception)
                {

                    throw;
                }

            }


            [HttpGet]
            [Route("GetLableById")]
            public IActionResult GetLableWithId(long lableId)
            {

                try
                {

                    long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    var result = ilabelBL.GetLableWithId(lableId, jwtUserId);

                    if (result != null)
                    {

                        return Ok(new { Success = true, message = "Retrived Lable ", result });

                    }
                    else
                    {
                        return NotFound(new { Success = false, message = "No Lable With Particular LableId " });

                    }


                }
                catch (Exception)
                {

                    throw;
                }

            }


            [HttpPut]
            [Route("Update")]
            public IActionResult UpdateLabel(long lableId, UpdateLableModel model)
            {
                try
                {

                    long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    LabelEntity updateLable = ilabelBL.GetLablesWithId(lableId, jwtUserId);


                    if (updateLable != null)
                    {
                        LabelResponseModel lable = ilabelBL.UpdateLable(updateLable, model, jwtUserId);

                        return Ok(new { Success = true, message = "Lable Updated Sucessfully", lable });

                    }
                    else
                    {
                        return BadRequest(new { Success = false, message = "No Notes Found With NotesId" });

                    }



                }
                catch (Exception)
                {

                    throw;
                }

            }


            [HttpDelete]
            [Route("Delete")]
            public IActionResult DeleteLabel(long lableId)
            {

                try
                {

                    long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                    LabelEntity lable = ilabelBL.GetLablesWithId(lableId, jwtUserId);
                    if (lable != null)
                    {

                        ilabelBL.DeleteLable(lable, jwtUserId);
                        return Ok(new { Success = true, message = "Label Removed" });

                    }
                    else
                    {
                        return BadRequest(new { Success = false, message = "No Label Found" });

                    }



                }
                catch (Exception)
                {

                    throw;
                }

            }



        }
    }