using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WiFIMsg
{
    class WIn32Api
    {
        #region msg
        public const int USER = 0x0400;
        public const int UM_1 = USER + 1;
        #endregion
        #region api
        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        #endregion
    }
}
