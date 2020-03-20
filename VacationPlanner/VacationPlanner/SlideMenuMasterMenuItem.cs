using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationPlanner
{

    public class SlideMenuMasterMenuItem
    {
        public SlideMenuMasterMenuItem()
        {
            TargetType = typeof(SlideMenuMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}