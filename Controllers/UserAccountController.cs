using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models.Data_Access;
using KazanAirportWebApp.Models.Join_Models;

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
                        "Insert Into dbo.Logins ([login], [passWord], email, userTypeId) Values (@login, @password, @email, @role)",
                        new SqlParameter("@login", user.login), 
                        new SqlParameter("@password", user.passWord),
                        new SqlParameter("@email", user.email),
                        new SqlParameter("@role", user.userTypeId));
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

                return loginsList.Count == 0 ? null : loginsList.First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Получение пользователя по Id
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetUserById")]
        public Logins GetUserById(int id)
        {
            try
            {
                List<Logins> loginsList;
                using (var db = new KazanAirportDbEntities())
                {
                    loginsList = db.Database.SqlQuery<Logins>("Select * From dbo.Logins Where id = @id",
                        new SqlParameter("@id", id)).ToList();
                }

                return loginsList.Count == 0 ? null : loginsList.First();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("UpdateUser")]
        public string UpdateUser(Logins user)
        {
            var dbUser = GetUserById(user.id);
            if (dbUser == null)
            {
                return "Ошибка. Пользователь не найден";
            }

            var existingDataValidation = ValidationLogic.ValidateExistingUserCompare(dbUser, user);
            if (!string.IsNullOrEmpty(existingDataValidation))
            {
                return existingDataValidation;
            }

            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand(
                        "Update dbo.Logins set[login] = @login, email = @email, userTypeId = @userTypeId where id = @id",
                        new SqlParameter("@login", user.login),
                        new SqlParameter("@email", user.email),
                        new SqlParameter("@userTypeId", user.userTypeId),
                        new SqlParameter("@id", user.id));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("GetUsersList")]
        public List<UserItem> GetUsersList()
        {
            try
            {
                List<UserItem> loginsList;
                using (var db = new KazanAirportDbEntities())
                {
                    loginsList = db.Database.SqlQuery<UserItem>("Select * From dbo.Logins as L " +
                                                      "join dbo.UserTypes as UT on L.userTypeId = UT.id").ToList();
                }

                return ProtectLogic.FilterPasswords(loginsList);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("RemoveUser")]
        public string RemoveUser(int id)
        {
            try
            {
                using (var db = new KazanAirportDbEntities())
                {
                    db.Database.ExecuteSqlCommand("Delete From dbo.Logins where id = @id",
                        new SqlParameter("@id", id));
                }

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}