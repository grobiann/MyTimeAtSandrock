using System.Net.Sockets;

namespace ServerCore
{
    public class ReceiveBuff
    {
        public int FreeSize => _buffer.Count - _writeIndex;
        public int DataSize => _writeIndex - _readIndex;

        // oooo[r]oooo[w]ooooooooo
        private readonly ArraySegment<byte> _buffer;
        private int _writeIndex;
        private int _readIndex;

        // TODO: Clean
        public ReceiveBuff(int size)
        {
            _buffer = new ArraySegment<byte>(new byte[size], 0, size);
            _readIndex = 0;
            _writeIndex = 0;
        }

        public ArraySegment<byte> GetWriteSegment()
        {
            return new ArraySegment<byte>(_buffer.Array, _writeIndex, FreeSize);
        }

        public ArraySegment<byte> GetDataSegment()
        {
            return new ArraySegment<byte>(_buffer.Array, _readIndex, DataSize);
        }

        public void OnWrite(int size)
        {
            _writeIndex += size;
        }

        public void OnRead(int count)
        {
            _readIndex += count;
        }
    }
}
