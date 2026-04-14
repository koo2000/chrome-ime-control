
using System.ComponentModel;
using System.Text;

namespace NativeMessaging
{
    public class NativeMessagingConnection
    {
        private Stream ist;
        private Stream ost;
        private Encoding enc;
        private List<INativeMessagingEventListener> listeners = new List<INativeMessagingEventListener>();

        public NativeMessagingConnection(Stream ist, Stream ost) : this(ist, ost, Encoding.UTF8)
        {
            
        }
        public NativeMessagingConnection(Stream ist, Stream ost, Encoding enc)
        {
            this.ist = ist;
            this.ost = ost;
            this.enc = enc;
        }

        public void AddListener(INativeMessagingEventListener listener)
        {
            listeners.Add(listener);
        }

        public void DeleteListener(INativeMessagingEventListener listener)
        {
            listeners.Remove(listener);
        }

        public void startRead()
        {
            var _context = new ReadContext();
            _context.reset(ReadState.LenReading, 4);
            ist.BeginRead(_context.Buf, _context.ReadLen, _context.ByteLen, processInput, _context);
        }

        public void processInput(IAsyncResult result)
        {
            var _context = result.AsyncState as ReadContext;

            if (_context is null)
            {
                throw new ArgumentNullException("context is null");
            }
            int _readBytes = ist.EndRead(result);

            _context.ReadLen += _readBytes;
            if (_context.ReadLen < _context.ByteLen)
            {
                // 読み込み途中なので読み込み継続
                ist.BeginRead(_context.Buf, _context.ReadLen, _context.ByteLen, processInput, _context);
                return;
            }

            if (_context.State == ReadState.LenReading)
            {
                _context.ByteLen = BitConverter.ToInt32(_context.Buf);

                if (_context.ByteLen > 10000000 || _context.ByteLen < 0)
                {
                    // マイナスや10MB以上のメッセージは怪しいので無理やり終わらせる。
                    throw new ArgumentException("message context length is minus or too large ["+ _context.ByteLen + "]. Abort Process.");
                }

                _context.reset(ReadState.MessageReading, _context.ByteLen);
                ist.BeginRead(_context.Buf, _context.ReadLen, _context.ByteLen, processInput, _context);
            }
            else
            {
                String _message = enc.GetString(_context.Buf);
                listeners.ForEach(listener => listener.messageReceived(_message));

                _context.reset(ReadState.LenReading, 4);

                ist.BeginRead(_context.Buf, _context.ReadLen, _context.ByteLen, processInput, _context);
            }
        }

    }

    public interface INativeMessagingEventListener
    {
        void messageReceived(String message);
    }

    public class ReadContext
    {
        private ReadState state;
        private byte[] buf;
        private int byteLen;
        private int readLen;

        public ReadState State { get => state; set => state = value; }
        public byte[] Buf { get => buf; set => buf = value; }
        public int ByteLen { get => byteLen; set => byteLen = value; }
        public int ReadLen { get => readLen; set => readLen = value; }

        public void reset(ReadState state, int byteLen)
        {
            this.state = state;
            this.byteLen = byteLen;
            this.buf = new byte[byteLen];
            this.readLen = 0;
        }
    }

    public enum ReadState
    {
        LenReading,
        MessageReading
    }
}