using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.ValueObjects;

namespace ID.Biz.Contract
{
    public interface IUserBizManager
    {
        List<UserVO> GetAllUser(int? UserId);
        bool CreateUpdateUser(UserVO user);
        bool DeleteUser(int userId, bool IsSoftDelete);
    }
}
