using ID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.DAL.Contract
{
    public interface IUserDataManager
    {
        bool CreateUpdateUser(User user);
        bool DeleteUser(int userId, bool isSoftDelete);
        List<User> GetAllUser(int? userId);
    }
}
