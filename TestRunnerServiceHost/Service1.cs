using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using TestRunnerServiceLibrary;

namespace TestRunnerServiceHost
{
    public partial class Service1 : ServiceBase
    {
        private ServiceHost ServiceHost { get; set; }

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (ServiceHost != null)
            {
                ServiceHost.Close();
            }

            string strAdrHTTP = "http://localhost:8733/Design_Time_Addresses/TestRunnerService/";

            Uri[] adrbase = { new Uri(strAdrHTTP) };
            ServiceHost = new ServiceHost(typeof(TestRunnerService), adrbase);

            ServiceMetadataBehavior mBehave = new ServiceMetadataBehavior();
            ServiceHost.Description.Behaviors.Add(mBehave);

            WSHttpBinding httpb = new WSHttpBinding();
            ServiceHost.AddServiceEndpoint(typeof(TestRunnerService), httpb, strAdrHTTP);
            ServiceHost.AddServiceEndpoint(typeof(IMetadataExchange),
            MetadataExchangeBindings.CreateMexHttpBinding(), "mex");

            ServiceHost.Open();
        }

        protected override void OnStop()
        {
            if (ServiceHost != null)
            {
                ServiceHost.Close();
                ServiceHost = null;
            }
        }
    }
}
