using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteBoard.Model;

namespace WhiteBoard.Messages
{
    public class UserMessage : MessageBase
    {
        public UsersModel User { get; set; }
    }
}
