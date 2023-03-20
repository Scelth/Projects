using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    public class ProjectModel
    {
        [JsonIgnore]
        public Search[] Search { get; set; }
        public string Name { get; set; }
        public string Studio { get; set; }
        public string ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public DateTime ReleseDate { get; set; }
        public string Category { get; set; }
    }

    public class Search
    {
        public string Name { get; set; }
    }
}