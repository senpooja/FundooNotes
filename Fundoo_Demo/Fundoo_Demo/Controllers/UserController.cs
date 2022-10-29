using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult AddUser(UserRegistration userRegistration)
        {
            try
            {
                var reg = this.userBL.Registration(userRegistration);
                if (reg != null)

                {
                    // logger.Info("Registration successfull");
                    return this.Ok(new { Success = true, message = "Registration Sucessfull", Response = reg });
                }
                else
                {
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
                    // logger.Info("Registration successfull");
                    return this.Ok(new { Success = true, message = "Login Sucessfull", Data = reg });
                    
                }
                else
                {
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

                    return this.Ok(new { Success = true, message = "Token sent Sucessfully please check your mail" });
                }
                else
                {
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
                    return Ok(new { success = true, message = "Password Reset Successful" });
                }
                else
                {
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
