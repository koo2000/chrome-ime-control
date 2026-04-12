using ImeControl;
using NativeMessaging;
using NLog;

namespace NativeImeControl
{
    public class ImeControllListener : INativeMessagingEventListener
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        private ImeController controller;

        public static readonly String ImeOnMessage = "ime on";
        public static readonly String ImeOffMessage = "ime off";

        public ImeControllListener(String targetProcess)
        {
            controller = new ImeController(targetProcess);
        }
        public void messageReceived(string message)
        {
            if (ImeOnMessage.Equals(message))
            {
                controller.setImeActiveStatus(true);
            }
            else if (ImeOffMessage.Equals(message))
            {
                controller.setImeActiveStatus(false);
            }
            else
            {
                log.Info("unknown message received. skip message [" + message + "].");
            }
        }
    }

}