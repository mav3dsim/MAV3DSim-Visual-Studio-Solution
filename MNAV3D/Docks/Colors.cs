using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MAV3DSim.Docks
{
    class Colors : ProfessionalColorTable 
    {
        Color color = Color.FromArgb(0, 122, 204);
        public override Color MenuItemSelected
        {
            get { return color; }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return color; }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return color; }
        }

        
        

        public override Color MenuItemBorder
        {
            get { return Color.Red; }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get { return color; }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get { return color; }
        }

        public override Color MenuBorder
        {
            get { return color; }
        }
    }
}
