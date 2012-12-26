using System;
using System.Runtime.InteropServices;

namespace I2CLibrary
{
    class I2CStreamProvider : IStreamProvider
    {
        public static I2CStreamProvider CreateProvider(I2CChannel channel, int address)
        {
            return new I2CStreamProvider((int)channel, address);
        }

        private readonly int _deviceHandle;

        private I2CStreamProvider(int channel, int address)
        {
            _deviceHandle = i2c_connect(channel, address);
        }

        public void Flush()
        {
        }

        public int Read(byte[] buffer, int offset, int length)
        {
            var temp = new byte[length];

            var readCount = i2c_read(_deviceHandle, temp, length);
            if (readCount > 0)
            {
                Array.Copy(temp, 0, buffer, offset, readCount);
            }

            return readCount;
        }

        public void Write(byte[] buffer, int offset, int length)
        {
            var temp = new byte[length];
            Array.Copy(buffer, offset, temp, 0, length);
            i2c_write(_deviceHandle, temp, length);
        }

        public void Close()
        {
            i2c_close(_deviceHandle);
        }

        #region Imported functions

        [DllImport("libi2c.so", EntryPoint = "i2c_connect")]
        private static extern int i2c_connect(int channel, int address);

        [DllImport("libi2c.so", EntryPoint = "i2c_close")]
        private static extern void i2c_close(int handle);

        [DllImport("libi2c.so", EntryPoint = "i2c_write")]
        private static extern int i2c_write(int handle, byte[] pbuf, int len);

        [DllImport("libi2c.so", EntryPoint = "i2c_read")]
        private static extern int i2c_read(int handle, byte[] pbuf, int len);

        #endregion
    }
}
