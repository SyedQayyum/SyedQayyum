using ID.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Biz.Contract
{
    public interface ICategoryBizManager
    {
        List<CategoryVO> GetAllCategory(int? CategoryId,bool? IsActive);
        bool CreateUpdateCategory(CategoryVO category);
        bool DeleteCategory(int CategoryId, bool IsSoftDelete);
        bool SetCategoryActiveStatus(long CategoryId, bool ActiveStatus);
    }
}
