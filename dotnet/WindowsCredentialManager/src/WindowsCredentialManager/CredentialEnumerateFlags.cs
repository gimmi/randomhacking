using System;

namespace WindowsCredentialManager
{
    [Flags]
    internal enum CredentialEnumerateFlags
    {
        None = 0,
        AllCredentials = 0x1
    }
}