using DataAccess;
using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class SurfaceManager : ISurfaceManager
    {
        public int SaveSurface (Surface toSave)
        {
            try
            {
                return SurfaceAccessor.SaveSurface(toSave);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Internal error: ", ex);
            }
        }

        public List<String> ListSurfaces(String userName)
        {
            try
            {
                return SurfaceAccessor.SurfaceList(userName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Internal error: ", ex);
            }
        }

        public Surface GetSurface(String userName, String splineName)
        {
            try
            {
                return SurfaceAccessor.GetSurface(userName, splineName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Internal error: ", ex);
            }
        }
    }
}
