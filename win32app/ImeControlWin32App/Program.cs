using NativeMessaging;
using ImeControl;
using NLog;
using NativeImeControl;
using System.Runtime.CompilerServices;



Logger log = LogManager.GetCurrentClassLogger();
log.Info("start NativeMessagingConnection");

NativeMessagingConnection connection = new NativeMessagingConnection(Console.OpenStandardInput(), Console.OpenStandardOutput());
connection.AddListener(new ImeControllListener("chrome"));
connection.startRead();

while(true)
{
    // TODO どうもこれだとプロセスが残ってしまう。（まぁそういうものな気もするが、内部で例外を検知したら終了するようにはしたい。）
    Thread.Sleep(6000);
    log.Info("process alive");
}



// Console.WriteLine("Hello, World!");
