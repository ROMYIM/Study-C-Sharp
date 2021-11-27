using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfSample;

namespace WcfProgram
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var baseAddress = new Uri("http://localhost:8733/WcfSample/");

            var host = new ServiceHost(typeof(CalculatorService), baseAddress);

            try
            {
                host.AddServiceEndpoint(typeof(ICalculator), new WSHttpBinding(), nameof(CalculatorService));

                var behavior = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true
                };
                
                host.Description.Behaviors.Add(behavior);

                if (host.State != CommunicationState.Opened)
                {
                    host.Open();
                }

                Console.WriteLine(host.State.ToString());
                Console.WriteLine("Press <Enter> to terminate the service.");

                if (Console.ReadLine() == "\n")
                    host.Close();
            }
            catch (CommunicationException e)
            {
                Console.WriteLine(e);
                host.Abort();
            }
        }
    }
}