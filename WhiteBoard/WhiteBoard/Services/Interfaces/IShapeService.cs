using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBoard.Services.Interfaces
{
    internal interface IShapeService
    {
        public void DrawPen();
        public void DrawSquare();
        public void DrawCircle();
        public void DrawTriangle();
    }
}