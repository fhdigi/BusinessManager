using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessManager.Models;
using BusinessManager.Services;

namespace BusinessManager.PageModels
{
    class ProjectListingPageModel : BasePageModel
    {
        public ObservableRangeCollection<Project> Projects { get; set; }

        public ProjectListingPageModel()
        {
            
        }
    }
}
