using ID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.DAL.Contract
{
    public interface IOptionDataManager
    {
        List<Option> GetAllOption(long? OptionId, bool? IsActive);
        bool CreateUpdateOption(Option Option);
        bool DeleteOption(long OptionId);
        bool SetOptionActiveStatus(long optionId, bool activeStatus);
    }
}
