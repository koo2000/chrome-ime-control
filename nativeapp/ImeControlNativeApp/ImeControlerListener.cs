using System.Runtime.ConstrainedExecution;
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

        public const String ImemodeOn = "imeOn";
        public const String ImemodeOff = "imeOff";
        public const String ImemodeHiragana = "hiragana";
        public const String ImemodeFullKatakana = "katakana";
        public const String ImemodeHalfwidthKatakana = "hankakukana";


        public void messageReceived(string message)
        {
            log.Info("message received [" + message + "]");

            var doc = JsonDocument.Parse(message);
            doc.RootElement.TryGetProperty("imeOn", out JsonElement imeOn);
            doc.RootElement.TryGetProperty("imemode", out JsonElement imemode);
            doc.RootElement.TryGetProperty("agent", out JsonElement agent);

            String processName;

            if (agent.ToString().Contains("Edg"))
            {
                processName = "msedge";
            }
            else if (agent.ToString().Contains("Chrome"))
            {
                processName = "chrome";
            }
            else
            {
                log.Info("unknown agent name. skip message[" + message + "].");
                return;
            }


            if ("true".Equals(imeOn.ToString()) || "on".Equals(imeOn.ToString()))
            {
                controller.setImeStatus(processName, true);
            }
            else if ("false".Equals(imeOn.ToString()) || "off".Equals(imeOn.ToString()))
            {
                controller.setImeStatus(processName, false);
            }
            else
            {
                log.Info("unknown imeOn option received. skip imeOn = [" + imeOn.ToString() + "].");
            }

            if (ImemodeHiragana.Equals(imemode.ToString()))
            {
                controller.setImeStatus(processName, true, ImeController.ImeModeHiragana);
            } else if (ImemodeFullKatakana.Equals(imemode.ToString())){
                controller.setImeStatus(processName, true, ImeController.ImeModeFullKatakana);
            } else if (ImemodeHalfwidthKatakana.Equals(imemode.ToString())){
                controller.setImeStatus(processName, true, ImeController.ImeModeHalfKatakana);
            } else if (ImemodeOn.Equals(imemode.ToString())){
                controller.setImeStatus(processName, true);
            } else if (ImemodeOff.Equals(imemode.ToString())){
                controller.setImeStatus(processName, false);
            } else
            {
                log.Info("unknown message received. skip message [" + message + "].");
            }
        }
    }

}