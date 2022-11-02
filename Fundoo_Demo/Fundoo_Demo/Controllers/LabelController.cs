using BussinessLayer.Interfaces;
using CommanLayer.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    
        public class LabelController : ControllerBase
        {

            ILabelBL ilabelBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public readonly Context context;
        private readonly IConfiguration Config;
        private readonly ILogger<UserController> _logger;
        public LabelController(ILabelBL ilabelBL, IMemoryCache memoryCache, IDistributedCache distributedCache, Context context, IConfiguration Config, ILogger<UserController> _logger)
            {
                this.ilabelBL = ilabelBL;
                this.context = context;
                this.memoryCache = memoryCache;
                this.distributedCache = distributedCache;
                this.Config = Config;
                this._logger = _logger;
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
                        _logger.LogInformation("Name Missing For Label");
                        return BadRequest(new { Success = false, message = "Name Missing For Label" });
                    }
                    else
                    {
                        _logger.LogInformation("Label create successfull");
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
                          _logger.LogInformation("Retrived All labels");
                          return Ok(new { Success = true, message = "Retrived All labels ", result });

                    }
                    else
                    {
                         _logger.LogInformation("no labelin database");
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

                    _logger.LogInformation("Retrived Lable");
                    return Ok(new { Success = true, message = "Retrived Lable ", result });

                    }
                    else
                    {
                    _logger.LogInformation("No Lable With Particular LableId");
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
                    _logger.LogInformation("Lable Updated Sucessfully");
                    LabelResponseModel lable = ilabelBL.UpdateLable(updateLable, model, jwtUserId);

                        return Ok(new { Success = true, message = "Lable Updated Sucessfully", lable });

                    }
                    else
                    {
                    _logger.LogInformation("No Notes Found With NotesId");
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
                    _logger.LogInformation("Label Removed");

                    ilabelBL.DeleteLable(lable, jwtUserId);
                        return Ok(new { Success = true, message = "Label Removed" });

                    }
                    else
                    {
                    _logger.LogInformation("No Label Found");
                    return BadRequest(new { Success = false, message = "No Label Found" });

                    }



                }
                catch (Exception)
                {

                    throw;
                }

            }
        [HttpGet("ByNoteId")]
        public IEnumerable<LabelEntity> GetlabelsUsingNoteid(long noteid)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                return ilabelBL.GetlabelsUsingNoteid(noteid, userID);
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        [HttpPut("Rename")]
        public IActionResult RenameLabel(string lableName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var result = ilabelBL.RenameLabel(userID, lableName, newLabelName);
                if (result != null)
                {
                    _logger.LogInformation("Label renamed successfully");
                    return this.Ok(new { success = true, message = "Label renamed successfully", Response = result });
                }
                else
                {
                    _logger.LogInformation("Unable to rename");
                    return this.BadRequest(new { success = false, message = "Unable to rename" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
               // LabelList = await context.Lable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }


    }
    }