
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Management;
using System.Text;
using NLog;
using System.Runtime.Versioning;
using System.ComponentModel;


namespace ImeControl
{
    [SupportedOSPlatform("windows")]
    public class ImeController
    {

        private Logger log = LogManager.GetCurrentClassLogger();
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("Imm32.dll")]
        private static extern int ImmGetDefaultIMEWnd(IntPtr hWnd);

        // The following constants can be found at the URL below.
        // https://github.com/mhammond/pywin32/issues/2441
        private const uint IME_CONTROL = 0x0283;
        // IMN_SETCONVERSIONMODE、IMC_SETOPENSTATUS はimm.h にないが動く（Windows CEのimm.hにはある）
        private const int IMC_SETCONVERSIONMODE = 0x0002;
        private const int IMC_SETOPENSTATUS = 0x0006;
        // The following constants can be found at the URL below.
        // https://github.com/xtuml/packaging/blob/master/features/org.xtuml.bp.MinGW/win32.all/MinGW/include/imm.h
        public const int IME_CMODE_NATIVE = 1;
        public const int IME_CMODE_CHINESE = IME_CMODE_NATIVE;
        public const int IME_CMODE_HANGEUL = IME_CMODE_NATIVE;
        public const int IME_CMODE_HANGUL = IME_CMODE_NATIVE;
        public const int IME_CMODE_JAPANESE = IME_CMODE_NATIVE;
        public const int IME_CMODE_KATAKANA = 2;
        public const int IME_CMODE_LANGUAGE = 3;
        public const int IME_CMODE_FULLSHAPE = 8;
        public const int IME_CMODE_ROMAN = 16;
        public const int IME_CMODE_CHARCODE = 32;
        public const int IME_CMODE_HANJACONVERT = 64;

        // The following constants japanese setting.
        public const int ImeModeHiragana = IME_CMODE_JAPANESE |  IME_CMODE_FULLSHAPE;
        public const int ImeModeFullKatakana = IME_CMODE_JAPANESE | IME_CMODE_KATAKANA | IME_CMODE_FULLSHAPE;
        public const int ImeModeHalfKatakana = IME_CMODE_JAPANESE | IME_CMODE_KATAKANA;

        public void setImeStatus(String processName, Boolean imeOn,int imeMode = -1)
        {
            String queryString = String.Format("SELECT * FROM Win32_Process Where Name like '{0}.exe'", processName);
            // String queryString = String.Format("SELECT * FROM Win32_Process ");
            var query = new ManagementObjectSearcher(
                queryString
                );
            // var query = new ManagementObjectSearcher(@"SELECT * FROM Win32_Process ");
            var result = query.Get();
            var processes = Process.GetProcessesByName(processName);
            IntPtr target_hwnd = IntPtr.Zero;

            int cnt = 0;
            foreach (ManagementObject o in result)
            {
                Object[] user = new Object[2];
                o.InvokeMethod("GetOwner", user);
                String userName = (String)user[0];

                // find only current user process
                if (userName != null && userName.Length > 0)
                {
                    var id = o["ProcessId"];
                    Process p = Process.GetProcessById(Convert.ToInt32(id));

                    IntPtr mainWindowHandle = p.MainWindowHandle;
                    if (mainWindowHandle != 0)
                    {
                        log.Debug("process [" + processName + "] found. pid = " + p.Id + "]");
                        IntPtr imeHandle = ImmGetDefaultIMEWnd(mainWindowHandle);
                        SendMessage(imeHandle, IME_CONTROL, IMC_SETOPENSTATUS, imeOn ? 1 : 0);
                        if (imeOn && imeMode >= 0)
                        {
                            SendMessage(imeHandle, IME_CONTROL, IMC_SETCONVERSIONMODE, imeMode);
                        }

                        break;

                    }
                }


            }

        }

    }

}