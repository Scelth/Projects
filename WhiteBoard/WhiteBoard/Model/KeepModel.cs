using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBoard.Model
{
    public class KeepModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public UsersModel User { get; set; }
    }
}
