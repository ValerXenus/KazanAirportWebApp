using System.ServiceProcess;

namespace GetFlightsService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FlightsService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
