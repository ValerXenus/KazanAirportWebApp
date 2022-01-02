using System;
using System.ComponentModel;
using System.Linq;

namespace KazanAirportWebApi.Logic
{
    /// <summary>
    /// Дополнительный функционал для работы с enum
    /// </summary>
    public class EnumTranslator
    {
        /// <summary>
        /// Get description of the enum value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            var info = value.GetType().GetField(value.ToString());

            if (info?.GetCustomAttributes(typeof(DescriptionAttribute), false) 
                    is DescriptionAttribute[] attributes && attributes.Any())
                return attributes.First().Description;

            return value.ToString();
        }
    }
}