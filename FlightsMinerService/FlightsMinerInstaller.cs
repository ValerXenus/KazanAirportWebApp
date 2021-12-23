using System.ComponentModel;
using System.ServiceProcess;

namespace FlightsMinerService
{
    [RunInstaller(true)]
    public partial class FlightsMinerInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller _serviceInstaller;

        private ServiceProcessInstaller _processInstaller;

        public FlightsMinerInstaller()
        {
            InitializeComponent();
            _serviceInstaller = new ServiceInstaller();
            _processInstaller = new ServiceProcessInstaller();

            _processInstaller.Account = ServiceAccount.LocalSystem;
            _serviceInstaller.StartType = ServiceStartMode.Manual;
            _serviceInstaller.ServiceName = "GetFlightsService";
            Installers.Add(_processInstaller);
            Installers.Add(_serviceInstaller);
        }
    }
}
