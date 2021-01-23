using System;

namespace KazanAirportWebApp.Models.Join_Models
{
    public class UserItem
    {
        public int id { get; set; }

        public string login { get; set; }

        public string passWord { get; set; }

        public string email { get; set; }

        public Nullable<int> passengerId { get; set; }

        public int userTypeId { get; set; }

        public string typeName { get; set; }
    }
}