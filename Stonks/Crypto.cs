using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace Stonks
{
    public class Crypto
    {
        public string EncryptedApiKey { get; set; }

        [XmlIgnore]
        public string ApiKey
        {
            get => string.IsNullOrEmpty(EncryptedApiKey) ? null : Decrypt(EncryptedApiKey);
            set => EncryptedApiKey = string.IsNullOrEmpty(value) ? null : Encrypt(value);
        }
        private string Encrypt(string plain)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(plain);
                var enc = CryptProtect(bytes);
                return Convert.ToBase64String(enc);
            }
            catch
            {
                return null;
            }
        }

        private string Decrypt(string cipher)
        {
            try
            {
                var bytes = Convert.FromBase64String(cipher);
                var dec = CryptUnprotect(bytes);
                return Encoding.UTF8.GetString(dec);
            }
            catch
            {
                return null;
            }
        }

        // P/Invoke wrappers for DPAPI using CryptProtectData / CryptUnprotectData
        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CryptProtectData(ref DATA_BLOB pDataIn, string szDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

        [DllImport("crypt32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool CryptUnprotectData(ref DATA_BLOB pDataIn, StringBuilder pszDataDescr, IntPtr pOptionalEntropy, IntPtr pvReserved, IntPtr pPromptStruct, int dwFlags, ref DATA_BLOB pDataOut);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LocalFree(IntPtr hMem);

        [StructLayout(LayoutKind.Sequential)]
        private struct DATA_BLOB
        {
            public int cbData;
            public IntPtr pbData;

            public DATA_BLOB(byte[] data)
            {
                cbData = data.Length;
                pbData = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, pbData, data.Length);
            }
        }
        private static byte[] CryptProtect(byte[] data)
        {
            var inBlob = new DATA_BLOB(data);
            var outBlob = new DATA_BLOB();
            try
            {
                if (!CryptProtectData(ref inBlob, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, ref outBlob))
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }
                var result = new byte[outBlob.cbData];
                Marshal.Copy(outBlob.pbData, result, 0, outBlob.cbData);
                return result;
            }
            finally
            {
                if (inBlob.pbData != IntPtr.Zero) Marshal.FreeHGlobal(inBlob.pbData);
                if (outBlob.pbData != IntPtr.Zero) LocalFree(outBlob.pbData);
            }
        }
        private static byte[] CryptUnprotect(byte[] data)
        {
            var inBlob = new DATA_BLOB(data);
            var outBlob = new DATA_BLOB();
            try
            {
                if (!CryptUnprotectData(ref inBlob, null, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 0, ref outBlob))
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }
                var result = new byte[outBlob.cbData];
                Marshal.Copy(outBlob.pbData, result, 0, outBlob.cbData);
                return result;
            }
            finally
            {
                if (inBlob.pbData != IntPtr.Zero) Marshal.FreeHGlobal(inBlob.pbData);
                if (outBlob.pbData != IntPtr.Zero) LocalFree(outBlob.pbData);
            }
        }



    }
}
