using System;
using System.IO;
using System.Text;

namespace HexDump
{
    class Program
    {
        static void Main(string[] args)
        {
            var position = 0;
            //using (var reader = new StreamReader("textdata.txt"))
            using (Stream input = File.OpenRead(args[0]))
            {
                var buffer = new byte[16];
                int bytesRead;
                //while (!reader.EndOfStream)
                while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    // Read up to the next 16 bytes from the file in to a byte array
                    //var buffer = new char[16];                          // Create a 16 character array buffer
                    //var bytesRead = reader.ReadBlock(buffer, 0, 16);    // Stream reads chars from textdata.txt into buffer starting at index 0 with count of 16
                    //var bytesRead = input.Read(buffer, 0, buffer.Length);    // Stream reads chars from textdata.txt into buffer starting at index 0 with count of 16

                    // Write the position (or offset) in hex, followed by a colon and space
                    Console.Write("{0:x4}: ", position);
                    position += bytesRead;

                    // Write the hex value of each character in the byte arrary
                    for (var i = 0; i < 16; i++)
                    {
                        if (i < bytesRead)
                            Console.Write("{0:x2} ", (byte)buffer[i]);
                        else
                            Console.Write("   ");
                        if (i == 7) Console.Write("-- ");
                        if (buffer[i] < 0x20 || buffer[i] > 0x7f) buffer[i] = (byte)'.';
                    }

                    // Write the actual characters in the byte array
                    //var bufferContents = new string(buffer);
                    var bufferContents = Encoding.UTF8.GetString(buffer);
                    Console.WriteLine("   {0}", bufferContents.Substring(0, bytesRead));
                }
            }
        }
    }
}
