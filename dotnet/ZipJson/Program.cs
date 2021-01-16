using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ZipJson
{
    class Program
    {
        static void Main()
        {
            var message = BuildMessage();
            
            Console.WriteLine(message.Length);
            
            using var decompressedStream = new MemoryStream();
            using var decompressionStream = new DeflateStream(new MemoryStream(message), CompressionMode.Decompress);
            decompressionStream.CopyTo(decompressedStream);

            var json = Encoding.UTF8.GetString(decompressedStream.GetBuffer(), 0, (int) decompressedStream.Length);
            Console.Out.WriteLine(json.Length);
            Console.Out.WriteLine(json);
        }

        private static byte[] BuildMessage()
        {
            using var memoryStream = new MemoryStream();
            using var deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress);

            foreach (var _ in Enumerable.Range(0, 10))
            {
                WriteJson(deflateStream);
                deflateStream.WriteByte(10);
                deflateStream.Flush();
                Console.WriteLine(memoryStream.Length);
            }

            return memoryStream.ToArray();
        }

        private static void WriteJson(Stream stream)
        {
            using var writer = new Utf8JsonWriter(stream);
            writer.WriteStartObject();
            writer.WriteNumber("t", 123);
            writer.WriteString("n", "456");
            writer.WriteEndObject();
        }
    }
}
