using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Biz.Contract
{
    public interface IOptionBizManager
    {
        List<OptionVO> GetAllOption(int? OptionId, bool? IsActive);
        bool CreateUpdateOption(OptionVO Option);
        bool DeleteOption(int OptionId);
        bool SetOptionActiveStatus(long OptionId, bool ActiveStatus);
    }
}
