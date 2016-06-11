using ID.DAL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.Model;
using System.Data.SqlClient;
using ID.DAL.Implementation.Shared;
using System.Data;
using Core.Common;

namespace ID.DAL.Implementation
{
    public class ManagementDataManager : IManagementDataManager
    {
        public bool CreateUpdateManagement(Management management)
        {
            bool IsSaved = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Management> objManagementList = new List<Management>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@Id",management.ManagementId),
                   new SqlParameter("@Comment",management.Comment),
                   new SqlParameter("@ManageCategory",management.ManagementCategoryId),
                   new SqlParameter("@RupeeOrMinutes",management.RupeeOrMinute)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_InsertUpdateManagement", Params);

            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return false;
            }
            if (RowAffected > 0)
            {
                IsSaved = true;
            }

            return IsSaved;
        }

        public bool DeleteManagement(Int64 ManagementId)
        {
            bool IsDeleted = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Management> objManagementList = new List<Management>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@Id",ManagementId)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_DeleteManagement", Params);

            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return false;
            }
            if (RowAffected > 0)
            {
                IsDeleted = true;
            }

            return IsDeleted;
        }

        public List<Management> GetAllManagement(int? ManagementId)
        {
            SqlConnection con = new IDDbContext().GetConnection();
            List<Management> objManagementList = new List<Management>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                  ManagementId !=null ? new SqlParameter("@Id",ManagementId) : new SqlParameter("@Id",DBNull.Value)
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdManagement = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetSurveyManagement", Params);

            while (rdManagement.Read())
            {
                objManagementList.Add(

                    new Management()
                    {
                        ManagementId = Convert.ToInt64(rdManagement["Id"].ToString()),
                        Comment = rdManagement["Comment"].ToString(),
                        ManagementCategoryId = Convert.ToInt16(rdManagement["ManageCategory"].ToString()),
                        RupeeOrMinute = Convert.ToInt16(rdManagement["RupeeOrMinutes"].ToString()),
                    });
            }
            rdManagement.Close();
            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return null;
            }

            return objManagementList;
        }
    }
}
