using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Model
{
    public class ProjectModel
    {
        public string Name { get; set; }
        public string Studio { get; set; }
        public string ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public DateTime ReleseDate { get; set; } = new DateTime(2000, 01, 01);
    }
}
