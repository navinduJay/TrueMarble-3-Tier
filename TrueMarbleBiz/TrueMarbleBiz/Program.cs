using System;
using System.ServiceModel;
using TrueMarbleData;

namespace TrueMarbleBiz
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            try {


                ServiceHost host;
                NetTcpBinding tcpBinding = new NetTcpBinding();


                // setup the port for external clients
                host = new ServiceHost(typeof(TMBizControllerImpl));
                host.AddServiceEndpoint(typeof(ITMBizController), tcpBinding, "net.tcp://localhost:50002/TMBiz");

                host.Open();

                Console.WriteLine("Press any key to terminate the program");

                Console.ReadLine();

                host.Close();

            }
          
            catch (System.Net.Sockets.SocketException exception)
            {

                Console.WriteLine("Connection  failure! " + exception);
            }



        }
    }
}
