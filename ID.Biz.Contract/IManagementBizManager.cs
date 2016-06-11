using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Biz.Contract
{
   public interface IManagementBizManager
    {
        List<ManagementVO> GetAllManagement(int? ManagementId);
        bool CreateUpdateManagement(ManagementVO management);
        bool DeleteManagement(Int64 ManagementId);
    }
}
