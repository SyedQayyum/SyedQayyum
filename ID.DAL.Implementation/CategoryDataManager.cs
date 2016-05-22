using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.DAL.Contract;
using ID.Model;
using System.Data.SqlClient;
using ID.DAL.Implementation.Shared;
using System.Data;
using Core.Common;

namespace ID.DAL.Implementation
{
    public class CategoryDataManager : ICategoryDataManager
    {
        public List<Category> GetAllCategory(int? CategoryId, bool? IsActive)
        {

            SqlConnection con = new IDDbContext().GetConnection();
            List<Category> objCategoryList = new List<Category>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                  CategoryId!=null? new SqlParameter("@categoryId",CategoryId) : new SqlParameter("@categoryId",DBNull.Value),
                  IsActive!=null? new SqlParameter("@isActive",IsActive) : new SqlParameter("@isActive",DBNull.Value)
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdCategories = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetSurveyCategory", Params);

            while (rdCategories.Read())
            {
                objCategoryList.Add(

                    new Category()
                    {
                        CategoryId = Convert.ToInt16(rdCategories["categoryId"].ToString()),
                        CategoryName = rdCategories["categoryName"].ToString(),
                        ParentCategory = rdCategories["parentCategory"] != DBNull.Value ? Convert.ToInt16(rdCategories["parentCategory"].ToString()) : (short)0,
                        CategoryOrder = Convert.ToInt16(rdCategories["order"].ToString()),
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

            return objCategoryList;
        }

        public bool CreateUpdateCategory(Category category)
        {
            bool IsSaved = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Category> objCategoryList = new List<Category>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@categoryId",category.CategoryId),
                   new SqlParameter("@categoryName",category.CategoryName),
                   new SqlParameter("@parentCategory",category.ParentCategory),
                   new SqlParameter("@order",category.CategoryOrder),
                   new SqlParameter("@createdBy",category.CreatedBy),
                   new SqlParameter("@isActive",category.IsActive)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_InsertUpdateSurveyCategory", Params);

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

        public bool DeleteCategory(int CategoryId, bool IsSoftDelete)
        {
            bool IsDeleted = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Category> objCategoryList = new List<Category>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@categoryId",CategoryId),
                   new SqlParameter("@IsSoftDelete",IsSoftDelete)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_DeleteSurveyCategory", Params);

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

        public bool SetCategoryActiveStatus(long CategoryId, bool ActiveStatus)
        {
            bool IsUpdated = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<Category> objCategoryList = new List<Category>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@categoryId",CategoryId),
                   new SqlParameter("@activeStatus",ActiveStatus)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_SetCategoryActiveStatus", Params);

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
