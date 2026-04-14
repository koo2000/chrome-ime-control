
using System.Text;
using NativeImeControl;

using NativeMessaging;
namespace Tests
{




    public class ImeControllListenerTests
    {


        [Test]
        public void integrationTestImeOn()
        {
            sendMessageWithListener("\"ime-mode\" : \"on\", \"agent\" : \"\"");
        }

        [Test]
        public void integrationTestImeOff()
        {
            sendMessageWithListener("\"ime-mode\" : \"off\"");
        }

        private static void sendMessageWithListener(string message)
        {
            ImeControlerListener listener = new ImeControlerListener();


            NativeMessagingConnection connection = new NativeMessagingConnection(Console.OpenStandardInput(), Console.OpenStandardOutput());
            connection.AddListener(new ImeControlerListener());
            connection.startRead();


            byte[] data1 = Encoding.UTF8.GetBytes(message);
            byte[] dataLen1 = BitConverter.GetBytes((Int32)data1.Length);

            MemoryStream inStream = new MemoryStream();
            inStream.Write(dataLen1);
            inStream.Write(data1);
            inStream.Seek(0, SeekOrigin.Begin);

            NativeMessagingConnection con = new NativeMessagingConnection(inStream, null);
            con.AddListener(listener);

            con.startRead();

            Thread.Sleep(100);
        }
    }
}
