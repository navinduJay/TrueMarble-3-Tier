using System;
using System.ServiceModel;
using TrueMarbleData;

namespace TrueMarbleBiz
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    internal class TMBizControllerImpl : ITMBizController
    {
       /*creating a data tier object because biz tier is required to access data tier, therefore through m_data it can access the functions of data tier */
         private ITMDataController m_data;

        public TMBizControllerImpl()
        {
            Console.WriteLine("A new client has connected!");
            try {

                ChannelFactory<ITMDataController> trueMarbleChannelFactory;
                NetTcpBinding tcpBinding = new NetTcpBinding();

                tcpBinding.MaxReceivedMessageSize = System.Int32.MaxValue;
                tcpBinding.ReaderQuotas.MaxArrayLength = System.Int32.MaxValue;

                trueMarbleChannelFactory = new ChannelFactory<ITMDataController>(tcpBinding, "net.tcp://localhost:50001/TMData");  
                m_data = trueMarbleChannelFactory.CreateChannel();


            } catch (FaultException exception) {

                Console.WriteLine("Connection faiure! " + exception);


            }


        }


        public int GetNumTilesAcross(int zoom)
        {
            int tilesAcross = this.m_data.GetNumTilesAcross(zoom);

            if (tilesAcross != 1) {
                throw new FaultException();
            } else {

                Console.WriteLine(" GetNumTilesAcross() Successfully executed");

            }

                return tilesAcross;
        }


        public int GetNumTilesDown(int zoom)
        {
          
               int tilesDown = this.m_data.GetNumTilesDown(zoom);

                if(tilesDown != 1) {
                    throw new FaultException();
                } else {

                    Console.WriteLine(" GetNumTilesDown() Successfully executed");
                }

            return tilesDown;
        }


        public byte[] LoadTile(int zoom, int x, int y)
        {

            byte[] buff = null;
            buff = this.m_data.LoadTile(zoom, x, y);
            return buff;

        }


    

        // destructor
        ~TMBizControllerImpl()
        {
            Console.WriteLine("Client disconnected!");
        }

    }
}
