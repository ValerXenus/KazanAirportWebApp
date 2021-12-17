using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using KazanAirportWebApp.DataAccess;
using KazanAirportWebApp.Logic;
using KazanAirportWebApp.Models;

namespace KazanAirportWebApp.Controllers
{
    public class UserAccountController : ApiController
    {
        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewUser")]
        public string AddNewUser(DbUser user)
        {
            var existingDataValidation = ValidationLogic.ValidateExistingUserData(user);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                using var db = new KazanAirportDbContext();

                user.UserPassword = ProtectLogic.GetMd5Hash(user.UserPassword);
                db.Users.Add(user);
                db.SaveChanges();

                return $"User {user.UserLogin} added successfully.";
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
        public DbUser LoginUser(DbUser user)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var hashedPassword = ProtectLogic.GetMd5Hash(user.UserPassword);
                var currentUser = db.Users.FirstOrDefault(x =>
                    x.UserLogin == user.UserLogin && x.UserPassword == hashedPassword);

                return currentUser;
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
        public DbUser GetUserById(int id)
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                return user;
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
        public string UpdateUser(DbUser user)
        {
            var dbUser = GetUserById(user.Id);
            if (dbUser == null)
                return "Ошибка. Пользователь не найден";

            var existingDataValidation = ValidationLogic.ValidateExistingUserForEdit(dbUser, user);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                using var db = new KazanAirportDbContext();
                var currentUser = db.Users.FirstOrDefault(x => x.Id == user.Id);
                if (currentUser == null)
                    return $"User with ID = {user.Id} wasn't found";

                currentUser.UserLogin = user.UserLogin;
                currentUser.Email = user.Email;
                currentUser.PassengerId = user.PassengerId;
                currentUser.UserTypeId = currentUser.UserTypeId;
                db.SaveChanges();

                return $"User {user.UserLogin} has been updated";
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
        public List<DbUser> GetUsersList()
        {
            try
            {
                using var db = new KazanAirportDbContext();
                var users = db.Users.ToList();

                return ProtectLogic.FilterPasswords(users);
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
                using var db = new KazanAirportDbContext();
                var currentUser = db.Users.FirstOrDefault(x => x.Id == id);
                if (currentUser == null)
                    return $"User with ID = {id} wasn't found";

                db.Users.Remove(currentUser);
                db.SaveChanges();

                return $"User {currentUser.UserLogin} deleted successfully";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}