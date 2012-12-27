using System.Runtime.InteropServices;

namespace I2CLibrary
{
    public static class I2CMem
    {
        public static void Send(int addr, byte [] buffer, int len)
        {
            if (bcm2835_i2c_mem_init() == 1)
	    {
            	try
            	{
                	bcm2835_i2c_mem_write(addr, buffer, len);
            	}
            	finally
            	{
                	bcm2835_i2c_mem_close();
            	}
	     }
        }

        public static void Receive(int addr, byte[] buffer, int len)
        {
            if (bcm2835_i2c_mem_init() == 1) {
            	try
            	{
                	bcm2835_i2c_mem_read(addr, buffer, len);
            	}
            	finally
            	{
                	bcm2835_i2c_mem_close();
            	}
	    }
        }

        #region Imported functions

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_mem_init")]
        private static extern int bcm2835_i2c_mem_init();

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_mem_close")]
        private static extern int bcm2835_i2c_mem_close();

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_mem_write")]
        private static extern int bcm2835_i2c_mem_write(int addr, byte[] pbuf, int len);

        [DllImport("libi2c.so", EntryPoint = "bcm2835_i2c_mem_read")]
        private static extern int bcm2835_i2c_mem_read(int addr, byte[] pbuf, int len);

        #endregion
    }
}

