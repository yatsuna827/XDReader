using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace XDReader
{
    static class NativeMethods
    {
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, out WindowRect lpRect);

        [DllImport("user32.dll")]
        private static extern int MoveWindow(IntPtr hwnd, int x, int y, int nWidth, int nHeight, int bRepaint);

        private delegate bool EnumWindowsDelegate(IntPtr hWnd, IntPtr lparam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool EnumWindows(EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private extern static bool EnumChildWindows(IntPtr hWnd, EnumWindowsDelegate lpEnumFunc, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd,
            StringBuilder lpString, int nMaxCount);

        private const int SRCCOPY = 0xCC0020;
        private const int CAPTUREBLT = 1073741824;

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hwnd, IntPtr hdc);
        public static Bitmap CaptureScreen(int x, int y, int height, int width)
        {
            var disDC = GetDC(IntPtr.Zero);
            var bmp = new Bitmap(width, height);
            var g = Graphics.FromImage(bmp);
            var hDC = g.GetHdc();

            //Bitmapに画像をコピーする
            BitBlt(hDC, 0, 0, bmp.Width, bmp.Height, disDC, x, y, SRCCOPY);

            //解放
            g.ReleaseHdc(hDC);
            g.Dispose();
            ReleaseDC(IntPtr.Zero, disDC);

            return bmp;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct WindowRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
        public static WindowRect GetWindowRect(IntPtr handle)
        {
            GetWindowRect(handle, out WindowRect windowRect);
            return windowRect;
        }

        public static void SetWindowSize(IntPtr handle, int h, int w)
        {
            var rect = GetWindowRect(handle);
            MoveWindow(handle, rect.left, rect.top, h, w, 1);
        }

        public static void ShowWindowNames()
        {
            EnumWindows((hWnd, i) => 
            {
                int textLen = GetWindowTextLength(hWnd);
                if (0 < textLen)
                {
                    //ウィンドウのタイトルを取得する
                    StringBuilder tsb = new StringBuilder(textLen + 1);
                    GetWindowText(hWnd, tsb, tsb.Capacity);

                    //結果を表示する
                    if (tsb.ToString() == "ウィンドウ プロジェクター (ソース) - 映像キャプチャデバイス") MessageBox.Show(tsb.ToString());
                }

                //すべてのウィンドウを列挙する
                return true;
            }, 
            IntPtr.Zero);
        }

        public static IntPtr[] GetHandlesByWindowName(string name)
        {
            var list = new List<IntPtr>();
            EnumWindows((hWnd, i) =>
            {
                int textLen = GetWindowTextLength(hWnd);
                if (textLen >= name.Length)
                {
                    StringBuilder tsb = new StringBuilder(textLen + 1);
                    GetWindowText(hWnd, tsb, tsb.Capacity);

                    if (tsb.ToString().Contains(name)) list.Add(hWnd);
                }

                return true;
            },
            IntPtr.Zero);
            return list.ToArray();
        }
    }

}
