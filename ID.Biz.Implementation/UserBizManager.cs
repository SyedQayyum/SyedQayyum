using ID.Biz.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.ValueObjects;
using ID.DAL.Contract;
using Core.Common;
using ID.Model;
using AutoMapper;

namespace ID.Biz.Implementation
{
    public class UserBizManager : IUserBizManager
    {

        protected readonly IUserDataManager _userDataManager;

        public UserBizManager(IUserDataManager userDataManager)
        {
            _userDataManager = userDataManager;
            AppHelper.CreateMap<UserVO, User>();
        }

        public bool CreateUpdateUser(UserVO user)
        {
            return _userDataManager.CreateUpdateUser(Mapper.Map<UserVO, User>(user));
        }

        public bool DeleteUser(int userId, bool IsSoftDelete)
        {
            return _userDataManager.DeleteUser(userId, IsSoftDelete);
        }

        public List<UserVO> GetAllUser(int? UserId)
        {
            return Mapper.Map<List<User>, List<UserVO>>(_userDataManager.GetAllUser(UserId));
        }
    }
}
