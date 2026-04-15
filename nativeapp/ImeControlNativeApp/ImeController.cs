
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

        private const uint IME_CONTROL = 0x0283;
        private const int IME_SETOPENSTATUS = 0x0006;

        public void setImeActiveStatus(String processName, Boolean imemode)
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
                        SendMessage(imeHandle, IME_CONTROL, IME_SETOPENSTATUS, imemode ? 1 : 0);
                        break;

                    }
                }
            }

        }

    }

}