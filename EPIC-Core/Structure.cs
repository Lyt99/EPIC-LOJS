using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EPIC_Core
{
    class Structure
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        }

        /*public struct DEBUG_EVENT
        {
            public int dwDebugEventCode;
            public int dwProcessId;
            public int dwThreadId;
            public struct u_
            {
                public EXCEPTION_DEBUG_INFO Exception;
                public CREATE_THREAD_DEBUG_INFO CreateThread;
                public CREATE_PROCESS_DEBUG_INFO CreateProcessInfo;
                public EXIT_THREAD_DEBUG_INFO ExitThread;
                public EXIT_PROCESS_DEBUG_INFO ExitProcess;
                public LOAD_DLL_DEBUG_INFO LoadDll;
                public UNLOAD_DLL_DEBUG_INFO UnloadDll;
                public OUTPUT_DEBUG_STRING_INFO DebugString;
                public RIP_INFO RipInfo;
            };
        }*/
        [StructLayout(LayoutKind.Explicit)]
        public struct DEBUG_EVENT
        {

            [FieldOffset(0)]
            public int dwDebugEventCode;

            [FieldOffset(4)]
            public int dwProcessId;

            [FieldOffset(8)]
            public int dwThreadId;

            [FieldOffset(12)]

            [StructLayout(LayoutKind.Explicit)]
            
            public struct u
            {

                [FieldOffset(0)]
                public EXCEPTION_DEBUG_INFO Exception;
                [FieldOffset(0)]
                public CREATE_THREAD_DEBUG_INFO CreateThread;
                [FieldOffset(0)]
                public CREATE_PROCESS_DEBUG_INFO CreateProcessInfo;
                [FieldOffset(0)]
                public EXIT_THREAD_DEBUG_INFO ExitThread;
                [FieldOffset(0)]
                public EXIT_PROCESS_DEBUG_INFO ExitProcess;
                [FieldOffset(0)]
                public LOAD_DLL_DEBUG_INFO LoadDll;
                [FieldOffset(0)]
                public UNLOAD_DLL_DEBUG_INFO UnloadDll;
                [FieldOffset(0)]
                public OUTPUT_DEBUG_STRING_INFO DebugString;
                [FieldOffset(0)]
                public RIP_INFO RipInfo;
            }
        };
        //[StructLayout(LayoutKind.Sequential)]
        public struct EXCEPTION_DEBUG_INFO
        {
            public EXCEPTION_RECORD ExceptionRecord;
            public uint dwFirstChance;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct EXCEPTION_RECORD
        {
            public uint ExceptionCode;
            public uint ExceptionFlags;
            public IntPtr ExceptionRecord;
            public IntPtr ExceptionAddress;
            public uint NumberParameters;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15, ArraySubType = UnmanagedType.U4)]
            public uint[] ExceptionInformation;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct CREATE_THREAD_DEBUG_INFO
        {
            public IntPtr hThread;
            public IntPtr lpThreadLocalBase;
            public PTHREAD_START_ROUTINE lpStartAddress;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct EXIT_PROCESS_DEBUG_INFO
        {
            public uint dwExitCode;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct EXIT_THREAD_DEBUG_INFO
        {
            public uint dwExitCode;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct CREATE_PROCESS_DEBUG_INFO
        {
            public IntPtr hFile;
            public IntPtr hProcess;
            public IntPtr hThread;
            public IntPtr lpBaseOfImage;
            public uint dwDebugInfoFileOffset;
            public uint nDebugInfoSize;
            public IntPtr lpThreadLocalBase;
            public PTHREAD_START_ROUTINE lpStartAddress;
            public IntPtr lpImageName;
            public ushort fUnicode;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct LOAD_DLL_DEBUG_INFO
        {
            public IntPtr hFile;
            public IntPtr lpBaseOfDll;
            public uint dwDebugInfoFileOffset;
            public uint nDebugInfoSize;
            public IntPtr lpImageName;
            public ushort fUnicode;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct UNLOAD_DLL_DEBUG_INFO
        {
            public IntPtr lpBaseOfDll;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct OUTPUT_DEBUG_STRING_INFO
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpDebugStringData;
            public ushort fUnicode;
            public ushort nDebugStringLength;
        }
        //[StructLayout(LayoutKind.Sequential)]
        public struct RIP_INFO
        {
            public uint dwError;
            public uint dwType;
        }
        public delegate uint PTHREAD_START_ROUTINE(IntPtr lpThreadParameter);






    }
}
