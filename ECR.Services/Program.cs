using System.ServiceProcess;
using ECR.Services.Server;

namespace ECR.Services
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static void Main()
        {
            var servicesToRun = new ServiceBase[] 
            { 
                new EcrServer()
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}

