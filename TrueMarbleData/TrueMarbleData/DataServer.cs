using System;
using System.ServiceModel;

namespace TrueMarbleData
{
    class DataServer
    {
        public static void Main(string[] args)
        {

            try {

                Console.WriteLine("Server started...");

                ServiceHost host;
                NetTcpBinding tcpBinding = new NetTcpBinding();

                tcpBinding.MaxReceivedMessageSize = System.Int32.MaxValue;
                tcpBinding.ReaderQuotas.MaxArrayLength = System.Int32.MaxValue;



                host = new ServiceHost(typeof(TMDataControllerImpl));
                host.AddServiceEndpoint(typeof(ITMDataController), tcpBinding, "net.tcp://localhost:50001/TMData");

                // opening the connection will allow clients to be able to connect to the server
                host.Open(); 

                // prevents program from terminating
                Console.ReadLine();


                // getting rid of the server class
                host.Close();



            } 
            catch(FaultException exception) {

                Console.WriteLine("Server cannot be started, something went wrong!" + exception);
            }
           


        }
    }
}
