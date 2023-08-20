using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WhiteBoard.Services.Interfaces
{
    public interface ISendService
    {
        public void Send(InkCanvas inkCanvas, byte[] ImageBytes);
        public void SendToEmail(string Email, string Subject, string Name, string Message);
    }
}
