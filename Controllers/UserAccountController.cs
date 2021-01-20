using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.Data_Access;

namespace KazanAirportWebApp.Controllers
{
    public class UserAccountController : ApiController
    {
        /// <summary>
        /// Using as a sample SQL query in EF
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetUserTypes")]
        public HttpResponseMessage GetUserTypes()
        {
            List<UserTypes> types;
            using (var db = new KazanAirportDbEntities())
            {
                types = db.UserTypes.SqlQuery("Select * from dbo.UserTypes").ToList();
            }

            return Request.CreateResponse(HttpStatusCode.OK, types);
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewUser")]
        public string AddNewUser(Logins user)
        {
            var existingDataValidation = ValidationLogic.ValidateExistingUserData(user);
            if (!string.IsNullOrEmpty(existingDataValidation))
            {
                return existingDataValidation;
            }

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Insert Into dbo.Logins ([login], [passWord], email, userTypeId) Values (@login, @password, @email, 2)",
                        new SqlParameter("@login", user.login), 
                        new SqlParameter("@password", user.passWord),
                        new SqlParameter("@email", user.email));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("LoginUser")]
        public Logins LoginUser(Logins user)
        {
            try
            {
                List<Logins> loginsList;
                using (var db = new KazanAirportDbEntities())
                { 
                    loginsList = db.Database.SqlQuery<Logins>("Select * From dbo.Logins Where ([login] = @login) and ([passWord] = @password)",
                        new SqlParameter("@login", user.login),
                        new SqlParameter("@password", user.passWord)).ToList();
                }

                return loginsList.Count == 0 ? null : loginsList[0];
            }
            catch
            {
                return null;
            }
        }
    }
}