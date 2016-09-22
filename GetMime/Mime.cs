using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace GetMime
{
    public class Mime
    {
        private const int First265 = 256;

        private static int MimeSampleSize = 256;

        public static string Get(string inputFile)
        {
            var readAllBytes = File.ReadAllBytes(inputFile);
            using (var reader = new MemoryStream(readAllBytes))
            {
                return MimeFromBytes(reader, readAllBytes.Length);
            }
        }

        public static string MimeFromBytes(MemoryStream reader, int length)
        {
            var bufferLength = First265 > length ? length : First265;
            var buffer = new byte[bufferLength];
            reader.Read(buffer, 0, bufferLength);
            return GetMimeFromBytes(buffer);
        }

        [DllImport("urlmon.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = false)]
        private static extern int FindMimeFromData(
                    IntPtr pBC,
                    [MarshalAs(UnmanagedType.LPWStr)] string pwzUrl,
                    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I1, SizeParamIndex = 3)] byte[] pBuffer,
                    int cbSize,
                    [MarshalAs(UnmanagedType.LPWStr)]  string pwzMimeProposed,
                    int dwMimeFlags,
                    out IntPtr ppwzMimeOut,
                    int dwReserved);

        /// <summary>
        /// http://webandlife.blogspot.com.au/2012/11/google-is-your-alcoholic-friend.html
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static unsafe string GetMimeFromBytes(byte[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var mimeTypePointer = IntPtr.Zero;
            try
            {
                FindMimeFromData(IntPtr.Zero, null, data, MimeSampleSize, null, 0, out mimeTypePointer, 0);
                var mime = Marshal.PtrToStringUni(mimeTypePointer);
                return mime;
            }
            catch (AccessViolationException e)
            {
                Debug.WriteLine(e.ToString());
                throw;
            }
            finally
            {
                if (mimeTypePointer != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(mimeTypePointer);
                }
            }
        }
    }
}