using System.Text.Json;
using ImeControl;
using NativeMessaging;
using NLog;

namespace NativeImeControl
{
    public class ImeControlerListener : INativeMessagingEventListener
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        private ImeController controller;

        public ImeControlerListener(String targetProcess)
        {
            controller = new ImeController(targetProcess);
        }
        public void messageReceived(string message)
        {
            log.Info("message received [" + message + "]");
            
            var doc = JsonDocument.Parse(message);
            doc.RootElement.TryGetProperty("imemode", out JsonElement imeMode);
            doc.RootElement.TryGetProperty("processName", out JsonElement processName);

            if ("on".Equals(imeMode.ToString()))
            {
                controller.setImeActiveStatus(processName.ToString(), true);
            }
            else if ("off".Equals(imeMode.ToString()))
            {
                controller.setImeActiveStatus(processName.ToString(), false);
            }
            else
            {
                log.Info("unknown message received. skip message [" + message + "].");
            }
        }
    }

}