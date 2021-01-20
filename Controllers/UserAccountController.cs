using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
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
    }
}