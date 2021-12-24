namespace GetFlightsService.Common
{
    /// <summary>
    /// Режим чтения авиарейсов
    /// </summary>
    internal enum DirectionType
    {
        /// <summary>
        /// Не определен
        /// </summary>
        None = 0,

        /// <summary>
        /// Файл вылетов
        /// </summary>
        Departure = 1,

        /// <summary>
        /// Файл прилетов
        /// </summary>
        Arrival = 2
    }
}
