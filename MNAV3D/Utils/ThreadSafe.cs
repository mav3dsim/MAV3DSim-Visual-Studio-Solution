using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms;

namespace MAV3DSim
{
    class ThreadSafe
    {
        private delegate void SetControlPropertyThreadSafeDelegate(Control control, string propertyName, object propertyValue);
        private delegate void SetGMapControlOverlayPropertyThreadSafeDelegate(GMapControl control, string propertyName, int index,object propertyValue);
        private delegate object GetControlPropertyThreadSafeDelegate(Control control, string propertyName);
        private delegate object InvokeControlMethodThreadSafeDelegate(Control control, string methodName, object propertyValue);
        private delegate object AddControlMethodThreadSafeDelegate(Control control, string methodName, object propertyValue);
        private delegate void SetStatusStripPropertyThreadSafeDelegate(StatusStrip statusStrip, ToolStripItem controlName, string propertyName, object propertyValue);
       
        public ThreadSafe()
        {

        }

        public void SetControlPropertyThreadSafe(Control control, string propertyName, object propertyValue)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new SetControlPropertyThreadSafeDelegate(SetControlPropertyThreadSafe), new object[] { control, propertyName, propertyValue });
                }
                else
                {
                    control.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control, new object[] { propertyValue });
                }
            }
            catch (Exception e)
            {

            }
        }

        public void SetGMapControlOverlayPropertyThreadSafe(GMapControl control, string propertyName, int index, object propertyValue)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new SetGMapControlOverlayPropertyThreadSafeDelegate(SetGMapControlOverlayPropertyThreadSafe), new object[] { control, propertyName, index, propertyValue });
                }
                else
                {
                    control.Overlays[index].Routes[index].GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, control.Overlays[index].Routes[index], new object[] { propertyValue });
                }
            }
            catch (Exception e)
            {

            }
        }

        
        public object GetControlPropertyThreadSafe(Control control, string propertyName)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke(new GetControlPropertyThreadSafeDelegate(GetControlPropertyThreadSafe), new object[] { control, propertyName });
            }
            else
            {
                return control.GetType().InvokeMember(propertyName, BindingFlags.GetProperty, null, control, null);
            }
        }

        public object InvokeControlMethodThreadSafe(Control control, string methodName, object propertyValue)
        {
            if (control.InvokeRequired)
            {
                return control.Invoke(new InvokeControlMethodThreadSafeDelegate(InvokeControlMethodThreadSafe), new object[] { control, methodName, propertyValue });
            }
            else
            {
                return control.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, control, new object[] {propertyValue});
            }
        }

        public object InvokeContainerControlMethodThreadSafe(Control control, string methodName, object propertyValue)
        {
            
            if (control.InvokeRequired)
            {
                return control.Invoke(new AddControlMethodThreadSafeDelegate(InvokeContainerControlMethodThreadSafe), new object[] { control, methodName, propertyValue });
            }
            else
            {
                if (propertyValue==null)
                    return control.Controls.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, control.Controls, new object[] {  });
                else
                    return control.Controls.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, control.Controls, new object[] { propertyValue });
            }
        }

        public void SetStatusStripPropertyThreadSafe(StatusStrip statusStrip,ToolStripItem controlName, string propertyName, object propertyValue)
        {
            try
            {
                if (statusStrip.InvokeRequired)
                {
                    statusStrip.Invoke(new SetStatusStripPropertyThreadSafeDelegate(SetStatusStripPropertyThreadSafe), new object[] { statusStrip, controlName, propertyName, propertyValue });
                }
                else
                {
                    controlName.GetType().InvokeMember(propertyName, BindingFlags.SetProperty, null, controlName, new object[] { propertyValue });
                }
            }
            catch (Exception e)
            {

            }
        }



        
    }
}
