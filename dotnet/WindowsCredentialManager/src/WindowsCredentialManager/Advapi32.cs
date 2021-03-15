using System;
using System.Runtime.InteropServices;

namespace WindowsCredentialManager
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/wincred/
    internal static class Advapi32
    {
        private const string LibraryName = "advapi32.dll";

        [DllImport(LibraryName, EntryPoint = "CredReadW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredRead(
            string target,
            CredentialType type,
            int reserved,
            out IntPtr credential);

        [DllImport(LibraryName, EntryPoint = "CredWriteW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredWrite(
            ref Win32Credential credential,
            int flags);

        [DllImport(LibraryName, EntryPoint = "CredDeleteW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredDelete(
            string target,
            CredentialType type,
            int flags);

        [DllImport(LibraryName, EntryPoint = "CredFree", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern void CredFree(
            IntPtr credential);

        [DllImport(LibraryName, EntryPoint = "CredEnumerateW", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CredEnumerate(
            string filter,
            CredentialEnumerateFlags flags,
            out int count,
            out IntPtr credentialsList);
    }
}