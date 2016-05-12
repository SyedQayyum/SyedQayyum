using ID.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.DAL.Contract
{
   public interface ICategoryDataManager
    {
        List<Category> GetAllCategory(int? CategoryId, bool? IsActive);
        bool CreateUpdateCategory(Category category);
        bool DeleteCategory(int CategoryId, bool IsSoftDelete);
        bool SetCategoryActiveStatus(long CategoryId, bool ActiveStatus);
    }
}
