namespace Test

{
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using NativeMessaging;
    using NUnit.Framework.Interfaces;


    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public class TestListener : INativeMessagingEventListener
        {
            private readonly List<String> messages = new List<string>();

            public List<string> Messages => messages;

            public void messageReceived(string message)
            {
                messages.Add(message);
                Console.Out.WriteLine("message receive [" + message + "]");
            }
        }
        [Test]
        public void TestInStream()
        {

            TestListener testListener = new TestListener();

            byte[] data1 = Encoding.UTF8.GetBytes("message01 dayo");
            byte[] data2 = Encoding.UTF8.GetBytes("message02 dayo dayo.");
            byte[] dataLen1 = BitConverter.GetBytes((Int32)data1.Length);
            byte[] dataLen2 = BitConverter.GetBytes((Int32)data2.Length);

            MemoryStream inStream = new MemoryStream();
            inStream.Write(dataLen1);
            inStream.Write(data1);
            inStream.Write(dataLen2);
            inStream.Write(data2);
            inStream.Seek(0, SeekOrigin.Begin);

            NativeMessagingConnection con = new NativeMessagingConnection(inStream, null);
            con.AddListener(testListener);

            con.startRead();


            Thread.Sleep(1000);


            Assert.That("message01 dayo", Is.EqualTo(testListener.Messages[0]));
            Assert.That("message02 dayo dayo.", Is.EqualTo(testListener.Messages[1]));
        }
    }
}