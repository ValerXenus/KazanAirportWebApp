using System.ComponentModel;
using System.ServiceProcess;

namespace GetFlightsService
{
    [RunInstaller(true)]
    public partial class GetFlightsInstaller : System.Configuration.Install.Installer
    {
        private ServiceInstaller _serviceInstaller;

        private ServiceProcessInstaller _processInstaller;

        public GetFlightsInstaller()
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
