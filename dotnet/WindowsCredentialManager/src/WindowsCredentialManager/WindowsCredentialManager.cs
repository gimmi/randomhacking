using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsCredentialManager
{
    public class WindowsCredentialManager
    {
        public void AddOrUpdate(string targetName, string userName, string secret)
        {
            if (string.IsNullOrWhiteSpace(targetName))
            {
                throw new ArgumentException("Argument cannot be empty or white space.", nameof(targetName));
            }

            var existingCredPtr = IntPtr.Zero;
            var credBlob = IntPtr.Zero;

            try
            {
                byte[] secretBytes = Encoding.Unicode.GetBytes(secret);
                credBlob = Marshal.AllocHGlobal(secretBytes.Length);
                Marshal.Copy(secretBytes, 0, credBlob, secretBytes.Length);

                var newCred = new Win32Credential {
                    Type = CredentialType.Generic,
                    TargetName = targetName,
                    CredentialBlobSize = secretBytes.Length,
                    CredentialBlob = credBlob,
                    Persist = CredentialPersist.LocalMachine,
                    UserName = userName
                };

                var success = Advapi32.CredWrite(ref newCred, 0);
                var result = Win32Error.GetLastError(success);
                Win32Error.ThrowIfError(result);
            }
            finally
            {
                if (credBlob != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(credBlob);
                }

                if (existingCredPtr != IntPtr.Zero)
                {
                    Advapi32.CredFree(existingCredPtr);
                }
            }
        }

        public bool TryRemove(string targetName)
        {
            if (!TryGet(targetName, out _, out _))
            {
                return false;
            }
            
            var success = Advapi32.CredDelete(targetName, CredentialType.Generic, 0);
            var result = Win32Error.GetLastError(success);
            Win32Error.ThrowIfError(result);
            return true;
        }

        public bool TryGet(string targetName, out string username, out string password)
        {
            var credList = IntPtr.Zero;

            try
            {
                var success = Advapi32.CredEnumerate(null, CredentialEnumerateFlags.AllCredentials, out var count, out credList);
                var result = Win32Error.GetLastError(success);
                Win32Error.ThrowIfError(result);

                var ptrSize = Marshal.SizeOf<IntPtr>();
                for (var i = 0; i < count; i++)
                {
                    var credPtr = Marshal.ReadIntPtr(credList, i * ptrSize);
                    var credential = Marshal.PtrToStructure<Win32Credential>(credPtr);

                    if (StringComparer.Ordinal.Equals(targetName, TrimLegacyPrefix(credential.TargetName)))
                    {
                        username = credential.UserName;
                        if (credential.CredentialBlobSize != 0 && credential.CredentialBlob != IntPtr.Zero)
                        {
                            var passwordBytes = InteropUtils.ToByteArray(credential.CredentialBlob, credential.CredentialBlobSize);
                            password = Encoding.Unicode.GetString(passwordBytes);
                        }
                        else
                        {
                            password = default;
                        }

                        return true;
                    }
                }
                username = default;
                password = default;
                return false;
            }
            finally
            {
                if (credList != IntPtr.Zero)
                {
                    Advapi32.CredFree(credList);
                }
            }
        }

        private static string TrimLegacyPrefix(string str)
        {
            var prefix = "LegacyGeneric:target=";
            var first = str.IndexOf(prefix, StringComparison.Ordinal);
            return first == -1 ? str : str.Substring(first + prefix.Length, str.Length - first - prefix.Length);
        }
    }
}