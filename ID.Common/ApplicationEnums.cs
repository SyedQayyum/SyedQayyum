using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID.Common
{

    public enum ContactForEnum
    {
        General = 0,
        SurveyRequest = 1,
        Adverstisement = 2
    }

    public enum ManagementCategoryEnum
    {
        [Description("Investment")]
        Investment = 1,
        [Description("Income")]
        Income = 2,
        [Description("TimeSpent")]
        TimeSpent = 3
    }

}
