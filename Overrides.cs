using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redberry
{
    public class menu_strip_renderer : ToolStripProfessionalRenderer
    {
        public menu_strip_renderer() : base(new MyColors()) { }
    }
    public class MyColors : ProfessionalColorTable
    {
        public override Color MenuItemSelected
        {
            get { return Color.White; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.White; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.White; }
        }
        public override Color MenuItemBorder
        {
            get { return Color.LightGray; }
        }
    }
}
