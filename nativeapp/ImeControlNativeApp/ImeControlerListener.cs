using System.Text.Json;
using ImeControl;
using NativeMessaging;
using NLog;

namespace NativeImeControl
{
    public class ImeControlerListener : INativeMessagingEventListener
    {
        private Logger log = LogManager.GetCurrentClassLogger();

        private ImeController controller = new ImeController();

        public void messageReceived(string message)
        {
            log.Info("message received [" + message + "]");
            
            var doc = JsonDocument.Parse(message);
            doc.RootElement.TryGetProperty("imemode", out JsonElement imeMode);
            doc.RootElement.TryGetProperty("agent", out JsonElement agent);

            String processName;

            if (agent.ToString().Contains("Edg"))
            {
                processName = "msedge";
            } else if (agent.ToString().Contains("Chrome"))
            {
                processName = "chrome";
            } else
            {
                log.Info("unknown agent name. skip message[" + message + "].");
                return;
            }


            if ("on".Equals(imeMode.ToString()))
            {
                controller.setImeActiveStatus(processName, true);
            }
            else if ("off".Equals(imeMode.ToString()))
            {
                controller.setImeActiveStatus(processName, false);
            }
            else
            {
                log.Info("unknown message received. skip message [" + message + "].");
            }
        }
    }

}