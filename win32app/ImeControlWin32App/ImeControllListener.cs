using System.Text.Json;
using ImeControl;
using NativeMessaging;
using NLog;

namespace NativeImeControl
{
    public class ImeControllListener : INativeMessagingEventListener
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        private ImeController controller;

        public ImeControllListener(String targetProcess)
        {
            controller = new ImeController(targetProcess);
        }
        public void messageReceived(string message)
        {
            log.Info("message received [" + message + "]");
            
            var doc = JsonDocument.Parse(message);
            var ime = doc.RootElement.TryGetProperty("ime-mode", out JsonElement imeMode);

            if ("on".Equals(imeMode.ToString()))
            {
                controller.setImeActiveStatus(true);
            }
            else if ("off".Equals(imeMode.ToString()))
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