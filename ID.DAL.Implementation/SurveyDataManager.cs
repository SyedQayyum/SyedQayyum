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
    public class SurveyDataManager : ISurveyDataManager
    {
        public Boolean CreateUpdateSurvey(Survey Survey)
        {
            SqlConnection con = new IDDbContext().GetConnection();
            Boolean IsCreated = false;
            int RowAffected = 0;
            List<Survey> objSurveyList = new List<Survey>();
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@surveyId",Survey.SurveyId),
                   new SqlParameter("@categoryId",Survey.CategoryId),
                   new SqlParameter("@surveyQuestion",Survey.SurveyQuestion),
                   new SqlParameter("@picturePath",Survey.PicturePath!=null ? Survey.PicturePath :""),
                   new SqlParameter("@surveyStartDate",Survey.StartDate),
                   new SqlParameter("@surveyCloseDate",Survey.CloseDate),
                   new SqlParameter("@surveyExpireDate",Survey.ExpireDate),
                   new SqlParameter("@surveyIsDeleted",Survey.IsDeleted),
                   new SqlParameter("@surveyIsActive",Survey.IsActive),
                    new SqlParameter("@createdBy","Qayyum")
            };

            Params[0].Direction = ParameterDirection.Output;
            using (SqlCommand objSqlCommand = new SqlCommand())
            {
                RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_InsertUpdateSurvey", Params);
                // detach the SqlParameters from the command object, so they can be used again.
                objSqlCommand.Parameters.Clear();
            }
            con.Close();
            if (RowAffected > 0)
            {
                IsCreated = true;
            }
            return IsCreated;
        }


        public bool DeleteSurvey(int SurveyId, bool IsSoftDelete)
        {
            throw new NotImplementedException();
        }

        public List<Survey> GetAllSurvey(long? SurveyId, int? CategoryId, Boolean? IsDeleted, Boolean? IsActive)
        {
            SqlConnection con = new IDDbContext().GetConnection();
            List<Survey> objSurveyList = new List<Survey>();
            int ReturnValue = 1;

            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@SurveyId",SurveyId) ,
                   new SqlParameter("@surveyCategoryId",CategoryId) ,
                   new SqlParameter("@isDeleted",IsDeleted),
                   new SqlParameter("@isActive",IsActive)
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdCategories = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetSurveyInformation", Params);

            while (rdCategories.Read())
            {
                objSurveyList.Add(

                    new Survey()
                    {

                        SurveyId = Convert.ToInt16(rdCategories["SurveyId"].ToString()),
                        CategoryId = Convert.ToInt16(rdCategories["CategoryId"].ToString()),
                        SurveyQuestion = rdCategories["SurveyQuestion"].ToString(),
                        PicturePath = rdCategories["PicturePath"].ToString(),
                        Rating = Convert.ToByte(rdCategories["SurveyRating"].ToString()),
                        CreatedDate =  Convert.ToDateTime(rdCategories["SurveyCreatedDate"].ToString()) ,
                        //CloseDate = rdCategories["SurveyCloseDate"] != null ? Convert.ToDateTime(rdCategories["SurveyCloseDate"].ToString()) : null,
                        //ExpireDate = rdCategories["SurveyExpireDate"] != null ?  Convert.ToDateTime(rdCategories["SurveyExpireDate"].ToString()) : null,
                        IsActive = Convert.ToBoolean(rdCategories["SurveyIsActive"].ToString()),
                        IsDeleted = Convert.ToBoolean(rdCategories["SurveyIsDeleted"].ToString()),
                        //StartDate = rdCategories["SurveyExpireDate"] != null ? Convert.ToDateTime(rdCategories["SurveyStartDate"].ToString()) : null,
                        CreatedBy = rdCategories["SurveyCreatedBy"].ToString()
                    });
            }
            rdCategories.Close();
            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return null;
            }

            return objSurveyList;
        }

        public long GetLastGeneratedSurveyId()
        {
            SqlConnection con = new IDDbContext().GetConnection();
            long SurveyId = 0;
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int)
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdCategories = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetSurveySurvey", Params);

            while (rdCategories.Read())
            {

                SurveyId = Convert.ToInt64(rdCategories["LastSurveyId"].ToString());
            }
            rdCategories.Close();
            con.Close();
            if (ReturnValue < 0)
            {
                return -1;
            }

            return SurveyId;
        }
    }
}
