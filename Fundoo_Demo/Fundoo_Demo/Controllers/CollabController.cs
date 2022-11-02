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
    public class CollabController : ControllerBase
    {
        private readonly ICollabBL icollaboratorBL;
        private readonly ILogger<UserController> _logger;
        public readonly Context context;
       
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;

        public CollabController(ICollabBL icollaboratorBL, ILogger<UserController> _logger, IMemoryCache memoryCache, IDistributedCache distributedCache, Context context)
        {
            this.icollaboratorBL = icollaboratorBL;
            this._logger = _logger;
            this.context = context;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
    
        }


        [HttpPost]
        [Route("Add")]
        public IActionResult AddCollaborate(long notesId, CollaboratedModel model)
        {
            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

            if (jwtUserId == 0 && notesId == 0)
            {
                _logger.LogInformation("Email Missing For Collaboration");
                return BadRequest(new { Success = false, message = "Email Missing For Collaboration" });
            }

            _logger.LogInformation("Collaboration Successfull ");
            CollabResponseModel collaborate = icollaboratorBL.AddCollaborate(notesId, jwtUserId, model);
            return Ok(new { Success = true, message = "Collaboration Successfull ", collaborate });
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteCollaborate(long collabId)
        {

            long jwtUserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);

            CollaboratorEntity collabid = (CollaboratorEntity)icollaboratorBL.GetAllByNoteID(collabId);
            if (collabid == null)
            {
                _logger.LogInformation("No Collaboration Found");
                return BadRequest(new { Success = false, message = "No Collaboration Found" });
            }
            _logger.LogInformation("Collaborated Email Removed");
            icollaboratorBL.DeleteCollab(collabid);
            return Ok(new { Success = true, message = "Collaborated Email Removed" });
        }




        [HttpGet("ByNoteId")]
        public IEnumerable<CollaboratorEntity> GetAllByNoteID(long NoteID)
        {
            try
            {
                _logger.LogInformation("successfull retrive label by note id ");
                return icollaboratorBL.GetAllByNoteID(NoteID);
            }
            catch (Exception)
            {
                _logger.LogInformation("unccessfull");
                throw;
            }
        }
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllCollabUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollaboratorEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollaboratorEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = await context.CollaboratorTable.ToListAsync();
               // CollabList = (List<NoteEntity>)icollaboratorBL.GetAllNotes();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }
}
