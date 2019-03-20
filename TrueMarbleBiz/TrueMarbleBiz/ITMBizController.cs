using System;
using System.ServiceModel;
using TrueMarbleData;

namespace TrueMarbleBiz
{
    [ServiceContract]
    public interface ITMBizController
    {

        [OperationContract]
        int GetNumTilesAcross(int zoom);

        [OperationContract]
        int GetNumTilesDown(int zoom);

        [OperationContract]
        byte[] LoadTile(int zoom, int x, int y);

        [OperationContract]
        bool VerifyTiles();


    }
}
