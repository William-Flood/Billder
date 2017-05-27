using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class SurfaceAccessor
    {
        public static int SaveSurface(Surface toSave)
        {
            int results = 0;
            var conn = DataConnection.getDBConnection();
            var cmdText = @"bsp_save_spline";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@USERNAME", toSave.Username);
            cmd.Parameters.AddWithValue("@SPLINENAME", toSave.SplineName);
            cmd.Parameters.AddWithValue("@COLOR", toSave.Color);
            cmd.Parameters.AddWithValue("@X_ONE", toSave.Verticies[0].X);
            cmd.Parameters.AddWithValue("@Y_ONE", toSave.Verticies[0].Y);
            cmd.Parameters.AddWithValue("@Z_ONE", toSave.Verticies[0].Z);
            cmd.Parameters.AddWithValue("@A_ONE_ONE", toSave.Verticies[0].AOne);
            cmd.Parameters.AddWithValue("@B_ONE_ONE", toSave.Verticies[0].BOne);
            cmd.Parameters.AddWithValue("@C_ONE_ONE", toSave.Verticies[0].COne);
            cmd.Parameters.AddWithValue("@A_ONE_TWO", toSave.Verticies[0].ATwo);
            cmd.Parameters.AddWithValue("@B_ONE_TWO", toSave.Verticies[0].BTwo);
            cmd.Parameters.AddWithValue("@C_ONE_TWO", toSave.Verticies[0].CTwo);
            cmd.Parameters.AddWithValue("@X_TWO", toSave.Verticies[1].X);
            cmd.Parameters.AddWithValue("@Y_TWO", toSave.Verticies[1].Y);
            cmd.Parameters.AddWithValue("@Z_TWO", toSave.Verticies[1].Z);
            cmd.Parameters.AddWithValue("@A_TWO_ONE", toSave.Verticies[1].AOne);
            cmd.Parameters.AddWithValue("@B_TWO_ONE", toSave.Verticies[1].BOne);
            cmd.Parameters.AddWithValue("@C_TWO_ONE", toSave.Verticies[1].COne);
            cmd.Parameters.AddWithValue("@A_TWO_TWO", toSave.Verticies[1].ATwo);
            cmd.Parameters.AddWithValue("@B_TWO_TWO", toSave.Verticies[1].BTwo);
            cmd.Parameters.AddWithValue("@C_TWO_TWO", toSave.Verticies[1].CTwo);
            cmd.Parameters.AddWithValue("@X_THREE", toSave.Verticies[2].X);
            cmd.Parameters.AddWithValue("@Y_THREE", toSave.Verticies[2].Y);
            cmd.Parameters.AddWithValue("@Z_THREE", toSave.Verticies[2].Z);
            cmd.Parameters.AddWithValue("@A_THREE_ONE", toSave.Verticies[2].AOne);
            cmd.Parameters.AddWithValue("@B_THREE_ONE", toSave.Verticies[2].BOne);
            cmd.Parameters.AddWithValue("@C_THREE_ONE", toSave.Verticies[2].COne);
            cmd.Parameters.AddWithValue("@A_THREE_TWO", toSave.Verticies[2].ATwo);
            cmd.Parameters.AddWithValue("@B_THREE_TWO", toSave.Verticies[2].BTwo);
            cmd.Parameters.AddWithValue("@C_THREE_TWO", toSave.Verticies[2].CTwo);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                results = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               throw ex;
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        public static List<String> SurfaceList(String userName)
        {
            var results = new List<String>();
            var conn = DataConnection.getDBConnection();
            var cmdText = @"bsp_retrieve_user_splines";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.Parameters.AddWithValue("@USERNAME", userName);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    results.Add(reader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        public static Surface GetSurface(String userName, String splineName)
        {
            var results = new Surface();
            var conn_1 = DataConnection.getDBConnection();
            var cmd_1Text = @"bsp_retrieve_spline_color";
            var cmd_1 = new SqlCommand(cmd_1Text, conn_1);
            cmd_1.Parameters.AddWithValue("@USERNAME", userName);
            cmd_1.Parameters.AddWithValue("@SPLINENAME", splineName);
            cmd_1.CommandType = CommandType.StoredProcedure;
            try
            {
                conn_1.Open();
                var reader = cmd_1.ExecuteReader();
                while (reader.Read())
                {
                    results.Color = reader.GetString(0);
                    results.Username = userName;
                    results.SplineName = splineName;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn_1.Close();
            }
            var conn_2 = DataConnection.getDBConnection();
            var cmd_2Text = @"bsp_retrieve_spline_points";
            var cmd_2 = new SqlCommand(cmd_2Text, conn_2);
            cmd_2.Parameters.AddWithValue("@USERNAME", userName);
            cmd_2.Parameters.AddWithValue("@SPLINENAME", splineName);
            cmd_2.CommandType = CommandType.StoredProcedure;
            results.Verticies = new List<Vertex>();
            try
            {
                conn_2.Open();
                var reader = cmd_2.ExecuteReader();
                while (reader.Read())
                {
                    var newVertex = new Vertex()
                    {
                        PointIndex = reader.GetString(0),
                        X = reader.GetInt32(1),
                        Y = reader.GetInt32(2),
                        Z = reader.GetInt32(3),
                        AOne = reader.GetInt32(4),
                        BOne = reader.GetInt32(5),
                        COne = reader.GetInt32(6),
                        ATwo = reader.GetInt32(7),
                        BTwo = reader.GetInt32(8),
                        CTwo = reader.GetInt32(9)
                    };
                    results.Verticies.Add(newVertex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn_2.Close();
            }
            return results;
        }
    }
}
