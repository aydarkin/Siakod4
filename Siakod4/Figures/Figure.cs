using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siakod4.Figures
{
    public abstract class Figure
    {
        public bool Selected;


        public void Select()
        {
            Selected = true;
        }
        public void Deselect()
        {
            Selected = false;
        }

        public void RevertSelection()
        {
            Selected = !Selected;
        }

        public abstract void Paint(Graphics g);
        public abstract bool isPointInFigure(int x, int y);

    }
}
