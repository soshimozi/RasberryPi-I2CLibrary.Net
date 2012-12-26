namespace I2CLibrary
{
    interface IStreamProvider
    {
        void Flush();
        int Read(byte[] buffer, int offset, int count);
        void Write(byte[] buffer, int offset, int count);
        void Close();
    }
}
