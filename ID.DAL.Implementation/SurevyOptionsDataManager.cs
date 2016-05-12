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
    public class SurevyOptionsDataManager : ISurevyOptionsDataManager
    {
        public bool CreateUpdateSurveyOption(long Id, long OptionId, long SurveyId)
        {
            bool IsSaved = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Category> objCategoryList = new List<Category>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@Id",Id),
                   new SqlParameter("@OptionId",OptionId),
                   new SqlParameter("@SurveyId",SurveyId)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_InsertUpdateSurveyOptions", Params);

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

        public bool DeleteOption(long? Id, long SurveyId)
        {
            bool IsDeleted = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Category> objCategoryList = new List<Category>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                  Id!=null? new SqlParameter("@Id",Id) : new SqlParameter("@Id",DBNull.Value),
                   new SqlParameter("@SurveyId",SurveyId)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_DeleteSurveyOptions", Params);

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

        public List<Option> GetAllSurveyOption(long SurveyId)
        {
            SqlConnection con = new IDDbContext().GetConnection();
            List<Option> objOptionList = new List<Option>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@surveyId",SurveyId) ,
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdCategories = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetSurveyOptions", Params);

            while (rdCategories.Read())
            {
                objOptionList.Add(

                    new Option()
                    {
                        OptionId = Convert.ToInt64(rdCategories["OptionId"].ToString()),
                        OptionName = rdCategories["OptionName"].ToString(),

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
    }
}
