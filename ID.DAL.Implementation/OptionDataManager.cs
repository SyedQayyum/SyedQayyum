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
    public class OptionDataManager : IOptionDataManager
    {
        public bool CreateUpdateOption(Option Option)
        {
            bool IsSaved = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Option> objOptionList = new List<Option>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@OptionId",Option.OptionId),
                   new SqlParameter("@OptionName",Option.OptionName),
                   new SqlParameter("@createdBy",Option.CreatedBy),
                   new SqlParameter("@isActive",Option.IsActive)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_InsertUpdateOptions", Params);

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

        public bool DeleteOption(long OptionId)
        {
            bool IsDeleted = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Option> objOptionList = new List<Option>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@optionId",OptionId)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_DeleteOption", Params);

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

        public List<Option> GetAllOption(long? OptionId, bool? IsActive)
        {
            SqlConnection con = new IDDbContext().GetConnection();
            List<Option> objOptionList = new List<Option>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                  OptionId!=null? new SqlParameter("@optionId",OptionId) : new SqlParameter("@optionId",DBNull.Value),
                  IsActive!=null? new SqlParameter("@isActive",IsActive) : new SqlParameter("@isActive",DBNull.Value)
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdCategories = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetOptions", Params);

            while (rdCategories.Read())
            {
                objOptionList.Add(

                    new Option()
                    {
                        OptionId = Convert.ToInt64(rdCategories["OptionId"].ToString()),
                        OptionName = rdCategories["OptionName"].ToString(),
                        IsActive = Convert.ToBoolean(rdCategories["isActive"].ToString())

                    });
            }
            rdCategories.Close();
            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return null;
            }

            return objOptionList;
        }

        public bool SetOptionActiveStatus(long OptionId, bool ActiveStatus)
        {
            bool IsUpdated = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Option> objOptionList = new List<Option>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@optionId",OptionId),
                   new SqlParameter("@activeStatus",ActiveStatus)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_SetOptionActiveStatus", Params);

            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return false;
            }
            if (RowAffected > 0)
            {
                IsUpdated = true;
            }

            return IsUpdated;
        }
    }
}
