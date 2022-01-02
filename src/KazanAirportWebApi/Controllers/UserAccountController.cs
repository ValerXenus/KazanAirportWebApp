using System;
using System.Collections.Generic;
using System.Linq;
using KazanAirportWebApi.DataAccess;
using KazanAirportWebApi.Logic;
using KazanAirportWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KazanAirportWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserAccountController : ControllerBase
    {
        /// <summary>
        /// Контекст БД
        /// </summary>
        private readonly KazanAirportDbContext _db;

        public UserAccountController(KazanAirportDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionName("AddNewUser")]
        public string AddNewUser(DbUser user)
        {
            var existingDataValidation = ValidationLogic.ValidateExistingUserData(_db, user);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();

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
        public DbUser LoginUser(DbUser user)
        {
            try
            {
                var currentUser = _db.Users.FirstOrDefault(x =>
                    x.UserLogin == user.UserLogin && x.UserPassword == user.UserPassword);

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
                var user = _db.Users.FirstOrDefault(x => x.Id == id);
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

            var existingDataValidation = ValidationLogic.ValidateExistingUserForEdit(_db, dbUser, user);
            if (!string.IsNullOrEmpty(existingDataValidation))
                return existingDataValidation;

            try
            {
                var currentUser = _db.Users.FirstOrDefault(x => x.Id == user.Id);
                if (currentUser == null)
                    return $"User with ID = {user.Id} wasn't found";

                currentUser.UserLogin = user.UserLogin;
                currentUser.Email = user.Email;
                currentUser.PassengerId = user.PassengerId;
                currentUser.UserTypeId = currentUser.UserTypeId;
                _db.SaveChanges();

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
        public List<DbUser> GetUsersList()
        {
            try
            {
                var users = _db.Users.ToList();

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
                var currentUser = _db.Users.FirstOrDefault(x => x.Id == id);
                if (currentUser == null)
                    return $"User with ID = {id} wasn't found";

                _db.Users.Remove(currentUser);
                _db.SaveChanges();

                return "Success";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }
    }
}