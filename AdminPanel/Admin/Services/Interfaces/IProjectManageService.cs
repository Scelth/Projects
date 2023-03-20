using Admin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Services.Interfaces
{
    interface IProjectManageService
    {
        void Add(ProjectModel projectModel);
        ProjectModel GetProject(string name, string releaseDate);
        bool CheckExists(string name, string releaseDate);
    }
}