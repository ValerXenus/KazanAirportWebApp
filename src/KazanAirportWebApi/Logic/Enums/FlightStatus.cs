using System.ComponentModel;

namespace KazanAirportWebApp.Logic.Enums
{
    /// <summary>
    /// Статусы полета
    /// </summary>
    public enum FlightStatus
    {
        [Description("Неактивен")]
        Disabled = 0,

        [Description("Ожидается регистрация")]
        WaitingForCheckIn = 1,

        [Description("Регистрация")]
        CheckIn = 2,

        [Description("Посадка поссажиров")]
        Boarding = 3,

        [Description("Взлет")]
        TakeOff = 4,

        [Description("В пути")]
        InFlight = 5,

        [Description("Посадка")]
        Landing = 6,

        [Description("Высадка пассажиров")]
        Unboarding = 7,

        [Description("Завершен")]
        Finished = 8,

        [Description("Задерживается")]
        Delayed = 9,

        [Description("Отменем")]
        Canceled = 10
    }
}