using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Shared.Helpers
{
   public class FileSystemHelper
    {
        /// <summary>
        /// https://stackoverflow.com/a/221941
        /// https://stackoverflow.com/a/3621316
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>
        /// The binary and checksum of the file
        /// </returns>
        public static async Task<(byte[], string)> ReadFileAsync(string filePath)
        {
            var buffer = new byte[8 * 1024];
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (SHA1 hashAlg = new SHA1Managed())
            using (var memoryStream = new MemoryStream())
            {
                hashAlg.Initialize();
                int bytesReceived;
                while ((bytesReceived = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    hashAlg.TransformBlock(buffer, 0, bytesReceived, null, 0);
                    await memoryStream.WriteAsync(buffer, 0, bytesReceived);
                }

                hashAlg.TransformFinalBlock(new byte[0], 0, 0); //reset
                return (memoryStream.ToArray(), ChecksumToString(hashAlg.Hash));
            }
        }

        /// <summary>
        /// Calculate CheckSum
        /// https://stackoverflow.com/a/3621316
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static async Task<string> CheckSumAsync(byte[] bytes)
        {
            var buffer = new byte[8 * 1024];
            using (var memoryStream = new MemoryStream(bytes))
            using (SHA1 hashAlg = new SHA1Managed())
            {
                hashAlg.Initialize();
                int bytesReceived;
                while ((bytesReceived = await memoryStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    hashAlg.TransformBlock(buffer, 0, bytesReceived, null, 0);
                }

                hashAlg.TransformFinalBlock(new byte[0], 0, 0); //reset
                return ChecksumToString(hashAlg.Hash);
            }
        }

        public static async Task WriteFileAsync(byte[] byteArray, string filePath)
        {
            using (var contentStream = new MemoryStream(byteArray))
            using (var fileStream = File.Create(filePath))
            {
                var buffer = new byte[8 * 1024];
                int len;
                while ((len = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, len);
                }
                await fileStream.FlushAsync();
            }
        }

        public static void DeleteEvenIfReadOnly(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.SetAttributes(filePath, FileAttributes.Normal); // Might have been made read-only.
                File.Delete(filePath);
            }
        }

        public static void CreateDirectoryIfNotExist(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        /// <summary>
        /// Transforms a given hash into a string
        /// </summary>
        private static string ChecksumToString(byte[] hash)
        {
            if (hash == null || hash.Length == 0)
                return string.Empty;
            var formatted = new StringBuilder(2 * hash.Length);
            foreach (var b in hash)
            {
                formatted.AppendFormat("{0:X2}", b);
            }
            return formatted.ToString();
        }
    }
}
