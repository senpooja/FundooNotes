using CommanLayer.Models;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AddContext;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Aes = System.Runtime.Intrinsics.X86.Aes;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public readonly Context context;
        public readonly IConfiguration Iconfiguration;
        public UserRL(Context context,IConfiguration Iconfigration)
        {
            this.context = context;
            this.Iconfiguration = Iconfigration;
        }
        public UserEntity Registration(UserRegistration User)
        {
            try
            {
                UserEntity entity = new UserEntity();
                entity.FirstName = User.Firstname;
                entity.LastName = User.Lastname;
                entity.Email = User.Email;
               entity.Password = User.Password;
               //entity.Password = Encrypt(User.Password);
                this.context.Users.Add(entity);
                int Result = this.context.SaveChanges();
                if (Result > 0)
                {
                    return entity;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public string Login(UserLogin userLogin)
        {
            try
            {
                UserEntity entity = new UserEntity();
                 entity = this.context.Users.FirstOrDefault(x => x.Email == userLogin.Email);



                //var Email = entity.Email;

               var id = entity.Password;
                //string password = Decrypt(id);
                string password = id;
                
                var UserID = entity.UserId;
                if (password == userLogin.Password && entity != null)

                {
                    var token = TokenBTID(entity.Email,UserID);
                    return token;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
       


        //method to jwt token
        public string TokenBTID(string email,long userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
        
            var key = Encoding.ASCII.GetBytes(Iconfiguration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("userID", userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }




        public static string EncryptPassword(string Password)
        {
            try
            {
                if (Password == null)
                {
                    return null;
                }
                else
                {
                    byte[] x = Encoding.ASCII.GetBytes(Password);
                    string encryptedpass = Convert.ToBase64String(x);
                    return encryptedpass;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Decrypt(string Password)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(Password);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public string ForgetPassword(string EmailId)
        {
            try
            {
                var Result = this.context.Users.FirstOrDefault(x => x.Email == EmailId);
                if (Result != null)
                {
                    var Token = TokenBTID(EmailId, Result.UserId);
                    new MSMQ().sendData2Queue(Token);
                    return Token;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    var Result = context.Users.FirstOrDefault(x => x.Email == email);
                    Result.Password = password;
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {

                throw;
            }

        }


    }


}