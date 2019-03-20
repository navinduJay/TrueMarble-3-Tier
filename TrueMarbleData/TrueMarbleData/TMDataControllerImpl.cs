using System;
using System.ServiceModel;
using TrueMarbleLibrary;

namespace TrueMarbleData
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class TMDataControllerImpl : ITMDataController
    {

        // constructor
        TMDataControllerImpl()
        {
            Console.WriteLine("New client created!");
        }





        /*
         * 
         * 
         * if executing a method in TrueMarbleLibrary is 1(true | success) it goes to if condition otherwise if its 0(false | failure) it goes to else condition
        */

        // getting width using TrueMarbleLibrary
        public int GetTileWidth()
        {
            int width = 0;
            int height = 0;

            try
            {

                int isSuccessful = TrueMarbleLibrary.TrueMarble.GetTileSize(out width, out height);


                if(isSuccessful == 1)
                {
                    Console.WriteLine("GetTileWidth() method executed successfuly");
                }
                else if(isSuccessful == 0)
                {
                    throw new FaultException();
                }
            }
            catch (FaultException execption)
            {
                Console.WriteLine("Failed executing GetTileWidth() method" + execption);
            }

            return width;
        }


        // getting height using TrueMarbleLibrary
        public int GetTileHeight()
        {
            int height = 0;
            int width = 0;

            try
            {
                int isSuccessful = TrueMarbleLibrary.TrueMarble.GetTileSize(out width, out height);

                if(isSuccessful == 1)
                {
                    Console.WriteLine("GetTileHeight() method executed successfuly");
                }
                else if(isSuccessful == 0)
                {
                    throw new FaultException();
                }
            }
            catch(FaultException execption)
            {
                Console.WriteLine("Failed executing GetTileHeight() method" + execption);
            }

            return height;
        }



        // getting the number of tiles horizontally (across)
        public int GetNumTilesAcross(int zoom)
        {
            int xTilesCount = 0;
            int yTilesCount = 0;

            try
            {
                int isSuccessful = TrueMarbleLibrary.TrueMarble.GetNumTiles(zoom, out xTilesCount, out yTilesCount);

                if(isSuccessful == 1)
                {
                    Console.WriteLine("GetNumTilesAcross() method executed successfuly");
                }
                else if (isSuccessful == 0)
                {
                    throw new FaultException();
                }
            }
            catch (FaultException exception)
            {
                Console.WriteLine("Failed executing GetNumTilesAcross() method " + exception);

            }

            return xTilesCount;
        }


        // getting the number of tiles vertically (y range)
        public int GetNumTilesDown(int zoom)
        {
            int xTilesCount = 0;
            int yTilesCount = 0;

            try
            {
                int isSuccessful = TrueMarbleLibrary.TrueMarble.GetNumTiles(zoom, out xTilesCount, out yTilesCount);

                if(isSuccessful == 1)
                {
                    Console.WriteLine("GetNumTilesDown() method executed successfuly");
                }
                else if(isSuccessful == 0)
                {
                    throw new FaultException();
                }

            }
            catch (FaultException exception)
            {
                Console.WriteLine("Failed executing GetNumTilesDown() method" + exception);
            }

            return yTilesCount;
        }



        public byte[] LoadTile(int zoom, int x, int y)
        {
            int height = GetTileHeight();
            int width = GetTileWidth();
            int imageSize; // size of a JPG image
            int arraySize = height * width * 3;  // (buffer size)
            byte[] imagesArray; 

            // initialise byte array with the size of the buffer 
            imagesArray = new byte[arraySize];

            try
            {
                int isSuccessful = TrueMarbleLibrary.TrueMarble.GetTileImageAsRawJPG(zoom, x, y, out imagesArray, arraySize, out imageSize);

                if(isSuccessful != 1)
                {
                    throw new FaultException();
                }
                else if(isSuccessful == 0)
                {

                    Console.WriteLine("LoadTile() method executed successfuly");
                }

            }
            catch(FaultException exception)
            {
                Console.WriteLine("Failed executing LoadTile() method" + exception);
            }

            return imagesArray;

        }



        // destructor 
        ~TMDataControllerImpl()
        {
            Console.Write("Client destroyed");
        }
    }
}
