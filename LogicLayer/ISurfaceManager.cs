using System;
namespace LogicLayer
{
    public interface ISurfaceManager
    {
        DataTransferObjects.Surface GetSurface(string userName, string splineName);
        System.Collections.Generic.List<string> ListSurfaces(string userName);
        int SaveSurface(DataTransferObjects.Surface toSave);
    }
}
