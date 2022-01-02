namespace DatasetConverter.Enums
{
    /// <summary>
    /// Время суток
    /// </summary>
    internal enum DayTime
    {
        /// <summary>
        /// Период 00:00 - 06:00
        /// </summary>
        From0To6 = 0,

        /// <summary>
        /// Период 06:00 - 12:00
        /// </summary>
        From6To12 = 1,

        /// <summary>
        /// Период 12:00 - 18:00
        /// </summary>
        From12To18 = 2,

        /// <summary>
        /// Период 18:00 - 00:00
        /// </summary>
        From18To0 = 3
    }
}
