
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using NLog;


namespace ImeControl
{


    public class ImeController
    {

        private Logger log = LogManager.GetCurrentClassLogger();
        private String processName;

        public ImeController(String processName)
        {
            this.processName = processName;
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("Imm32.dll")]
        private static extern int ImmGetDefaultIMEWnd(IntPtr hWnd);

        private const uint IME_CONTROL = 0x0283;
        private const int IME_SETOPENSTATUS = 0x0006;
        
        public void setImeActiveStatus(Boolean imemode)
        {
            var processes = Process.GetProcessesByName(processName);
            IntPtr target_hwnd = IntPtr.Zero;

            foreach (Process p in processes)
            {
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