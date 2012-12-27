using System;
using System.IO;
using System.Text;
using System.Threading;
using I2CLibrary;

namespace RaspPi
{
    class Program
    {
        static void Main(string[] args)
        {
            //using(var stream = new I2CStream(I2CChannel.Channel0, 0x2a))
            //{
            //    testCommand(stream, "L11", "LED1 on");
	//Thread.Sleep(2000);
          //      testCommand(stream, "L10", "LED1 off");
	//	Thread.Sleep(2000);
          //  }
	I2CMem.Send(0x2a, Encoding.ASCII.GetBytes("L11"), Encoding.ASCII.GetByteCount("L11"));

	//	I2CMem.Send(0x2a, Encoding.ASCII.GetBytes("L11"), Encoding.ASCII.GetByteCount("L11"));

		Thread.Sleep(2000);

	//	I2CMem.Send(0x2a, Encoding.ASCII.GetBytes("L10"), Encoding.ASCII.GetByteCount("L10"));

I2CMem.Send(0x2a, Encoding.ASCII.GetBytes("L10"), Encoding.ASCII.GetByteCount("L10"));

        }

        static void testCommand(Stream stream, string command, string action)
        {
            Console.Write("Switching {0} ... ", action);
            stream.Write(Encoding.ASCII.GetBytes(command.ToCharArray()), 0, Encoding.ASCII.GetByteCount(command));

            Thread.Sleep(100);

            var buffer = new byte[1];
            var readBytes = stream.Read(buffer, 0, 1);
            if (readBytes != 1)
            {
                Console.Write("Error: Received no data!");
            }
            if (buffer[0] == 1)
            {
                Console.WriteLine("OK!");
            }

        }
    }
}

