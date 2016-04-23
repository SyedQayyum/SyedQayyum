using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ID.Biz.Contract;
using ID.ValueObjects;
using Core.Common;
using ID.Model;
using AutoMapper;
using ID.DAL.Contract;

namespace ID.Biz.Implementation
{
   public class CategoryBizManager : ICategoryBizManager
    {
        protected readonly ICategoryDataManager _categoryDataManager;

        public CategoryBizManager(ICategoryDataManager categoryDataManager)
        {
            _categoryDataManager = categoryDataManager;
            AppHelper.CreateMap<CategoryVO, Category>();
        }

        public List<CategoryVO> GetAllCategory(int? CategoryId)
        {
            List<CategoryVO> objCategoryList = Mapper.Map<List<Category>, List<CategoryVO>>(_categoryDataManager.GetAllCategory(CategoryId));
            foreach(CategoryVO Category in objCategoryList)
            {
                string ParentName = objCategoryList.Where(x => x.CategoryId == Category.ParentCategory).Select(x => x.CategoryName).FirstOrDefault();
                Category.ParentCategoryName = string.IsNullOrEmpty(ParentName) ? "N/A": ParentName;
            }
            return objCategoryList;
        }

        public bool CreateUpdateCategory(CategoryVO category)
        {
            return _categoryDataManager.CreateUpdateCategory(Mapper.Map<CategoryVO, Category>(category));
        }

         public bool DeleteCategory(int CategoryId, bool IsSoftDelete)
        {
            return _categoryDataManager.DeleteCategory(CategoryId, IsSoftDelete);
        }
    }
}
