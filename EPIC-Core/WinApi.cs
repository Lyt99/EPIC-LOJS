using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EPIC_Core
{
    class WinApi
    {
        [DllImport("Kernel32", CharSet = CharSet.Auto)]
        internal static extern void GetStartupInfo([In, Out] Structure.STARTUPINFO lpStartupInfo);
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            ref Structure.SECURITY_ATTRIBUTES lpProcessAttributes,
            ref Structure.SECURITY_ATTRIBUTES lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            [In] ref Structure.STARTUPINFO lpStartupInfo,
            out Structure.PROCESS_INFORMATION lpProcessInformation);
        [DllImport("Kernel32.dll")]
        public static extern bool CloseHandle(System.IntPtr hObject);
        [DllImport("kernel32.dll", EntryPoint = "WaitForDebugEvent")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WaitForDebugEvent([In] ref Structure.DEBUG_EVENT lpDebugEvent, uint dwMilliseconds);
        [DllImport("kernel32.dll")]
        static extern bool DebugActiveProcess(uint dwProcessId);
        [DllImport("kernel32.dll")]
        static extern bool ContinueDebugEvent(uint dwProcessId, uint dwThreadId, uint dwContinueStatus);
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DebugActiveProcessStop([In] int Pid);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int ContinueDebugEvent(int dwProcessId, int dwThreadId, uint dwContinueStatus);
        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static public extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);
        [DllImport("kernel32.dll")]
        static public extern bool GetExitCodeThread(IntPtr hThread, out uint lpExitCode);
    }
}
