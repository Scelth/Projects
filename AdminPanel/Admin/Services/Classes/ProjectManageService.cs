using Admin.Model;
using Admin.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Services.Classes
{
    internal class ProjectManageService : IProjectManageService
    {

        private List<ProjectModel> Projects = new();
        private readonly ISerializeService _serializeService;

        public ProjectManageService(ISerializeService service)
        {
            _serializeService = service;
        }

        private string CheckLength()
        {
            var res = File.ReadAllText("data.json");
            return res;
        }

        public void Add(ProjectModel projectModel)
        {
            var check = CheckLength();

            using FileStream fs = new("data.json", FileMode.Truncate);
            using StreamReader sr = new(fs);
            using StreamWriter sw = new(fs);

            var json = sr.ReadToEnd();

            if (check.Length > 1)
            {
                Projects = _serializeService.Deserialize<List<ProjectModel>>(check);
            }
            Projects.Add(projectModel);
            json = _serializeService.Serialize<List<ProjectModel>>(Projects);
            sw.Write(json);

        }

        private ProjectModel? DownloadData(string name, string releaseDate)
        {
            using FileStream fs = new("data.json", FileMode.Open);
            using StreamReader sr = new(fs);

            Projects = _serializeService.Deserialize<List<ProjectModel>>(sr.ReadToEnd());

            var result = Projects.Find(x => x.Name == name && x.ReleaseDate == releaseDate);

            return result;
        }

        public bool CheckExists(string name, string releaseDate)
        {
            var project = DownloadData(name, releaseDate);

            if (project != null)
            {
                return true;
            }

            return false;
        }

        public ProjectModel GetProject(string name, string releaseDate)
        {
            var project = DownloadData(name, releaseDate);

            if (project != null)
            {
                return project;
            }

            throw new NullReferenceException();
        }
    }
}