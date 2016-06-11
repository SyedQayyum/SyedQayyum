using ID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.DAL.Contract
{
    public interface IManagementDataManager
    {
        List<Management> GetAllManagement(int? ManagementId);
        bool CreateUpdateManagement(Management management);
        bool DeleteManagement(Int64 ManagementId);
    }
}
