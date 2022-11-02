using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Security.Claims;

namespace Fundoo_Demo.Controllers
{
  //  [Authorize]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;

        private readonly ILogger<UserController> _logger;
        public UserController(IUserBL userBL, ILogger<UserController> _logger)
        {
           
            this.userBL = userBL;
            this._logger = _logger;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistration userRegistration)
        {
            try
            {
                var reg = this.userBL.Registration(userRegistration);
                if (reg != null)

                {
                     _logger.LogInformation("Registration Sucessfull");
                    return this.Ok(new { Success = true, message = "Registration Sucessfull", Response = reg });
                }
                else
                {
                    _logger.LogInformation("Registration Unsucessfull");
                    return this.BadRequest(new { Success = false, message = "Registration Unsucessfull" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var reg = this.userBL.Login(userLogin);
                if (reg != null)

                {
                   _logger.LogInformation("User exists in Db");
                    return this.Ok(new { Success = true, message = "Login Sucessfull", Data = reg });
                    
                }
                else
                {
                    _logger.LogInformation("User does not exists in Db");
                    return this.BadRequest(new { Success = false, message = "login Unsucessfull" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
     

        [HttpPost("ForgetPassWord")]
        public IActionResult ForgetPassword(string EmailId)
        {
            try
            {

                var reg = this.userBL.ForgetPassword(EmailId);
                if (reg != null)

                {
                    _logger.LogInformation("Token sent Sucessfully please check your mail");
                    return this.Ok(new { Success = true, message = "Token sent Sucessfully please check your mail" });
                }
                else
                {
                    _logger.LogInformation("unable to send token to mail");
                    return this.BadRequest(new { Success = false, message = "unable to send token to mail" });
                }
            }
            catch (Exception ex)
            {

                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [Authorize]
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var Email = User.FindFirst(ClaimTypes.Email).Value.ToString();
               

                if (userBL.ResetPassword(Email, password, confirmPassword))
                {
                    _logger.LogInformation("assword Reset Successful");
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
                    _logger.LogInformation("Password Reset Unsuccessful");
                    return BadRequest(new { success = false, message = "Password Reset Unsuccessful" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }




    }
}
