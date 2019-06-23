using System;
using System.Runtime.InteropServices;

namespace ClipboardCounter
{
    internal class ClipboardListener
    {
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

        
        internal const int WM_DRAWCLIPBOARD = 0x0308; // WM_DRAWCLIPBOARD message
        // Our variable that will hold the value to identify the next window in the clipboard viewer chain
        private static IntPtr _clipboardViewerNext;

        internal void Subscribe(IntPtr handle)
        {
            _clipboardViewerNext = SetClipboardViewer(handle);
        }

        internal void Unsubscribe(IntPtr handle)
        {
            // Removes our from the chain of clipboard viewers when the form closes.
            ChangeClipboardChain(handle, _clipboardViewerNext);        
        }
    }
}
