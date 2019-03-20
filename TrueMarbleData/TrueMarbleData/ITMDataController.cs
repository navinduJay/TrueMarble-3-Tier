using System;
using System.ServiceModel;

namespace TrueMarbleData
{
    [ServiceContract]
    public interface ITMDataController 
    {

        [OperationContract]
        int GetTileWidth();

        [OperationContract]
        int GetTileHeight();

        [OperationContract]
        int GetNumTilesAcross(int zoom);

        [OperationContract]
        int GetNumTilesDown(int zoom);

        [OperationContract]
        byte[] LoadTile(int zoom, int x, int y);

    }
}
