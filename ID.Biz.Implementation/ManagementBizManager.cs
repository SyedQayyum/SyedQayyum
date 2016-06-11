using ID.Biz.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.ValueObjects;
using Core.Common;
using ID.Model;
using ID.DAL.Contract;
using AutoMapper;
using ID.Common;
using System.Reflection;
using System.ComponentModel;

namespace ID.Biz.Implementation
{
    public class ManagementBizManager : IManagementBizManager
    {
        protected readonly IManagementDataManager _managementDataManager;

        public ManagementBizManager(IManagementDataManager managementDataManager)
        {
            _managementDataManager = managementDataManager;
            AppHelper.CreateMap<ManagementVO, Management>();
        }


        public bool CreateUpdateManagement(ManagementVO management)
        {
            return _managementDataManager.CreateUpdateManagement(Mapper.Map<ManagementVO, Management>(management));
        }

        public bool DeleteManagement(Int64 ManagementId)
        {
            return _managementDataManager.DeleteManagement(ManagementId);
        }

        public List<ManagementVO> GetAllManagement(int? ManagementId)
        {
            List<ManagementVO> ManagementList = Mapper.Map<List<Management>, List<ManagementVO>>(_managementDataManager.GetAllManagement(ManagementId));

            foreach (ManagementVO management in ManagementList)
            {
                management.MCategory = GetEnumDescription((ManagementCategoryEnum)management.ManagementCategoryId);
            }

            return ManagementList;
        }


        private string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
