using ID.Biz.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.ValueObjects;
using ID.DAL.Contract;
using ID.Model;
using Core.Common;
using AutoMapper;

namespace ID.Biz.Implementation
{
    public class OptionBizManager : IOptionBizManager
    {

        protected readonly IOptionDataManager _optionDataManager;

        public OptionBizManager(IOptionDataManager optionDataManager)
        {
            _optionDataManager = optionDataManager;
            AppHelper.CreateMap<OptionVO, Option>();
        }

        public bool CreateUpdateOption(OptionVO Option)
        {
            return _optionDataManager.CreateUpdateOption(Mapper.Map<OptionVO, Option>(Option));
        }

        public bool DeleteOption(int OptionId)
        {
            return _optionDataManager.DeleteOption(OptionId);
        }

        public List<OptionVO> GetAllOption(int? OptionId, bool? IsActive)
        {
            return Mapper.Map<List<Option>, List<OptionVO>>(_optionDataManager.GetAllOption(OptionId,IsActive));

        }

        public bool SetOptionActiveStatus(long OptionId, bool ActiveStatus)
        {
            return _optionDataManager.SetOptionActiveStatus(OptionId, ActiveStatus);
        }
    }
}
