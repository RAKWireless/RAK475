//=============================================================================
// DETAILS : This class implement the Windows hook mechanism.
//           From MSDN, Dino Esposito.
//
//-----------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace CodeProject.Win32API.Hook
{
    ///////////////////////////////////////////////////////////////////////
    #region Class HookEventArgs

    /// Class used for hook event arguments.
    public class HookEventArgs : EventArgs
    {
        /// Event code parameter.
        public int code;
        /// wParam parameter.
        public IntPtr wParam;
        /// lParam parameter.
        public IntPtr lParam;

        internal HookEventArgs(int code, IntPtr wParam, IntPtr lParam)
        {
            this.code = code;
            this.wParam = wParam;
            this.lParam = lParam;
        }
    }

    #endregion

    ///////////////////////////////////////////////////////////////////////
    #region Enum HookType

    /// Hook Types.
    public enum HookType : int
    {
        /// <value>0</value>
        WH_JOURNALRECORD = 0,
        /// <value>1</value>
        WH_JOURNALPLAYBACK = 1,
        /// <value>2</value>
        WH_KEYBOARD = 2,
        /// <value>3</value>
        WH_GETMESSAGE = 3,
        /// <value>4</value>
        WH_CALLWNDPROC = 4,
        /// <value>5</value>
        WH_CBT = 5,
        /// <value>6</value>
        WH_SYSMSGFILTER = 6,
        /// <value>7</value>
        WH_MOUSE = 7,
        /// <value>8</value>
        WH_HARDWARE = 8,
        /// <value>9</value>
        WH_DEBUG = 9,
        /// <value>10</value>
        WH_SHELL = 10,
        /// <value>11</value>
        WH_FOREGROUNDIDLE = 11,
        /// <value>12</value>
        WH_CALLWNDPROCRET = 12,
        /// <value>13</value>
        WH_KEYBOARD_LL = 13,
        /// <value>14</value>
        WH_MOUSE_LL = 14
    }
    #endregion

    ///////////////////////////////////////////////////////////////////////
    #region Class WindowsHook

    /// <summary>
    /// Class to expose the windows hook mechanism.
    /// </summary>
    public class WindowsHook
    {
        /// <summary>
        /// Hook delegate method.
        /// </summary>
        public delegate int HookProc(int code, IntPtr wParam, IntPtr lParam);

        // internal properties
        internal IntPtr hHook = IntPtr.Zero;
        //internal IntPtr hHook2 = IntPtr.Zero;
        internal HookProc filterFunc = null;
        internal HookType hookType;

        /// <summary>
        /// Hook delegate method.
        /// </summary>
        public delegate void HookEventHandler(object sender, HookEventArgs e);

        /// <summary>
        /// Hook invoke event.
        /// </summary>
        public event HookEventHandler HookInvoke;

        internal void OnHookInvoke(HookEventArgs e)
        {
            if (HookInvoke != null)
                HookInvoke(this, e);
        }

        /// <summary>
        /// Construct a HookType hook.
        /// </summary>
        /// <param name="hook">Hook type.</param>
        public WindowsHook(HookType hook)
        {
            hookType = hook;
            filterFunc = new HookProc(this.CoreHookProc);
        }
        /// <summary>
        /// Construct a HookType hook giving a hook filter delegate method.
        /// </summary>
        /// <param name="hook">Hook type</param>
        /// <param name="func">Hook filter event.</param>
        public WindowsHook(HookType hook, HookProc func)
        {
            hookType = hook;
            filterFunc = func;
        }

        // default hook filter function
        internal int CoreHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0) { return CallNextHookEx(hHook, code, wParam, lParam); }

            // let clients determine what to do
            HookEventArgs e = new HookEventArgs(code, wParam, lParam);
            OnHookInvoke(e);

            // yield to the next hook in the chain
            return CallNextHookEx(hHook, code, wParam, lParam);
        }

        /// <summary>
        /// Install the hook. 
        /// </summary>
        public void Install()
        {
            hHook = SetWindowsHookEx(hookType, filterFunc, IntPtr.Zero, (int)AppDomain.GetCurrentThreadId());
            //Tony Update 2011.03.12
            ///hHook = SetWindowsHookEx(hookType, filterFunc, IntPtr.Zero, (int)Thread.CurrentThread.ManagedThreadId);
        }


        /// <summary>
        /// Uninstall the hook.
        /// </summary>
        public void Uninstall()
        {
            if (hHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(hHook);
                hHook = IntPtr.Zero;
            }
        }

        #region Win32 Imports

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowsHookEx(HookType code, HookProc func, IntPtr hInstance, int threadID);

        [DllImport("user32.dll")]
        internal static extern int UnhookWindowsHookEx(IntPtr hhook);

        [DllImport("user32.dll")]
        internal static extern int CallNextHookEx(IntPtr hhook, int code, IntPtr wParam, IntPtr lParam);



        #region Tony 2011.03.12

        ////[DllImport("kernel32.dll")]
        ////public static extern int GetCurrentThreadId();


        ////[DllImport("kernel32.dll", EntryPoint = "GetCurrentThreadId")]
        ////internal static extern int GetCurrentThreadId();

        ////////[DllImport("User32.dll", EntryPoint = "FindWindow")]
        ////////private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        ////////[DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        ////////private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        ////////[DllImport("User32.dll", EntryPoint = "SendMessage")]
        ////////private static extern long SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        ////////[DllImport("User32.dll", EntryPoint = "FindWindow")]
        ////////private static extern long UnhookWindowsHookEx(long hHook);

        ////////[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        ////////private static extern long GetWindowLong(long hwnd, long nIndex);



        ////////[DllImport("User32.dll", EntryPoint = "SetWindowsHookEx")]
        ////////private static extern long SetWindowsHookEx(long idHook, long lpfn, long hmod, long dwThreadId);

        ////////[DllImport("User32.dll", EntryPoint = "SetWindowPos")]
        ////////private static extern long SetWindowPos(long hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        ////////[DllImport("User32.dll", EntryPoint = "SetWindowsHookEx")]
        ////////private static extern long GetWindowRect(long hwnd, RECT lpRect);
        #endregion
        #endregion
    }
    #endregion
}
