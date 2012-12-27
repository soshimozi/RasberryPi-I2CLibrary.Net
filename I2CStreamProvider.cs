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
            _deviceHandle = bcm2835_i2c_file_open(channel, address);
        }

        public void Flush()
        {
        }

        public int Read(byte[] buffer, int offset, int length)
        {
            var temp = new byte[length];

            var readCount = bcm2835_i2c_file_read(_deviceHandle, temp, length);
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
            bcm2835_i2c_file_write(_deviceHandle, temp, length);
        }

        public void Close()
        {
            bcm2835_i2c_file_close(_deviceHandle);
        }

        #region Imported functions

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_file_open")]
        private static extern int bcm2835_i2c_file_open(int channel, int address);

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_file_close")]
        private static extern void bcm2835_i2c_file_close(int handle);

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_file_write")]
        private static extern int bcm2835_i2c_file_write(int handle, byte[] pbuf, int len);

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_file_read")]
        private static extern int bcm2835_i2c_file_read(int handle, byte[] pbuf, int len);

        #endregion
    }
}
