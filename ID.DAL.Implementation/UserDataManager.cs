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
    public class UserDataManager : IUserDataManager
    {
        public bool CreateUpdateUser(User user)
        {
            bool IsSaved = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<User> objUserList = new List<User>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@UserId",user.UserId),
                   new SqlParameter("@FirstName",user.FirstName),
                   new SqlParameter("@LastName",user.LastName),
                   new SqlParameter("@UserEmail",user.UserEmail),
                   new SqlParameter("@UserPassword",user.UserPassword),
                   new SqlParameter("@createdBy","Qayyum")
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_InsertUpdateUser", Params);

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

        public bool DeleteUser(int userId, bool isSoftDelete)
        {
            bool IsDeleted = false;
            SqlConnection con = new IDDbContext().GetConnection();
            List<User> objUserList = new List<User>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                   new SqlParameter("@UserId",userId),
                   new SqlParameter("@IsSoftDelete",isSoftDelete)
            };

            Params[0].Direction = ParameterDirection.Output;
            int RowAffected = SqlHelper.ExecuteNonQuery(con, CommandType.StoredProcedure, "USP_DeleteUser", Params);

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

        public List<User> GetAllUser(int? userId)
        {
            SqlConnection con = new IDDbContext().GetConnection();
            List<User> objUserList = new List<User>();
            int ReturnValue = 1;
            SqlParameter[] Params =
            {
                   new SqlParameter("@opReturnValue", SqlDbType.Int),
                  userId!=null? new SqlParameter("@UserId",userId) : new SqlParameter("@UserId",DBNull.Value)
            };

            Params[0].Direction = ParameterDirection.Output;
            SqlDataReader rdCategories = SqlHelper.ExecuteReader(con, CommandType.StoredProcedure, "USP_GetUser", Params);

            while (rdCategories.Read())
            {
                objUserList.Add(

                    new User()
                    {
                        UserId = Convert.ToInt16(rdCategories["UserId"].ToString()),
                        FirstName = rdCategories["FirstName"].ToString(),
                        LastName = rdCategories["LastName"].ToString(),
                        UserEmail = rdCategories["UserEmail"].ToString(),
                        UserPassword = rdCategories["UserPassword"].ToString()
                    });
            }
            rdCategories.Close();
            //ReturnValue = Convert.ToInt16(rdAdvertisement.Parameters["@EmpName"].Value.Value.ToString()); // Get Return value to check errors in DB
            con.Close();
            if (ReturnValue < 0)
            {
                return null;
            }

            return objUserList;
        }
    }
}
