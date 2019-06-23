using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardCounter
{
    public delegate void FireNotification(string notification);
    class TextProcessor
    {
        FireNotification notificatior;
        public void SetNotificationTool(FireNotification notificatior)
        {
            this.notificatior = notificatior;
        }

        public int CountChars(string text)
        {
            return text.Length;
        }
    }
}
