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
        List<CategoryVO> GetAllCategory(int? CategoryId);
        bool CreateUpdateCategory(CategoryVO category);
        bool DeleteCategory(int CategoryId, bool IsSoftDelete);
    }
}
