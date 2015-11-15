using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPIC_Core
{
    class Debugger
    {
        static Structure.STARTUPINFO sInfo = new Structure.STARTUPINFO();
        static Structure.PROCESS_INFORMATION pInfo = new Structure.PROCESS_INFORMATION();
        static Structure.SECURITY_ATTRIBUTES sa = new Structure.SECURITY_ATTRIBUTES();
        static Structure.DEBUG_EVENT DebugEvent = new Structure.DEBUG_EVENT();
        static Structure.EXCEPTION_DEBUG_INFO ExceptionInfo = new Structure.EXCEPTION_DEBUG_INFO();
        static Structure.EXIT_PROCESS_DEBUG_INFO ExitProcessInfo = new Structure.EXIT_PROCESS_DEBUG_INFO();
        static Structure.EXIT_THREAD_DEBUG_INFO ExitThreadInfo = new Structure.EXIT_THREAD_DEBUG_INFO();
        static Structure.CREATE_THREAD_DEBUG_INFO CreateThreadInfo = new Structure.CREATE_THREAD_DEBUG_INFO();
        static Structure.CREATE_PROCESS_DEBUG_INFO CreateProcessInfo = new Structure.CREATE_PROCESS_DEBUG_INFO();

        public static void HandleDebugEvent()
        {

        }
        public static void Init()
        {
            if (pInfo.hProcess != (IntPtr)0)
            {
                WinApi.CloseHandle(pInfo.hProcess);
                pInfo.hProcess = (IntPtr)0;
            }
            if (pInfo.hThread != (IntPtr)0)
            {
                WinApi.CloseHandle(pInfo.hThread);
                pInfo.hThread = (IntPtr)0;
            }
            sInfo.cb = 0;
            sInfo.lpReserved = "";
            sInfo.lpDesktop = "";
            sInfo.lpTitle = "";
            sInfo.dwX = 0;
            sInfo.dwY = 0;
            sInfo.dwXSize = 0;
            sInfo.dwYSize = 0;
            sInfo.dwXCountChars = 0;
            sInfo.dwYCountChars = 0;
            sInfo.dwFillAttribute = 0;
            sInfo.dwFlags = 0;
            sInfo.wShowWindow = 0;
            sInfo.cbReserved2 = 0;
            sInfo.lpReserved2 = (IntPtr)0;
            sInfo.hStdInput = (IntPtr)0;
            sInfo.hStdOutput = (IntPtr)0;
            sInfo.hStdError = (IntPtr)0;

            pInfo.hThread = (IntPtr)0;
            pInfo.dwProcessId = 0;
            pInfo.dwThreadId = 0;

            DebugEvent.dwDebugEventCode = 0;
            DebugEvent.dwProcessId = 0;
            DebugEvent.dwThreadId = 0;
        }
        public static int Start(String Path)
        {
            //WinApi.GetStartupInfo(sInfo);
            //隐藏窗口
            sInfo.cb = 0;
            sInfo.dwFlags = Constants.STARTF_USESHOWWINDOW;
            sInfo.wShowWindow = 0;
            //隐藏窗口
            Boolean rev = false;
            IntPtr nu = new IntPtr();//null
            rev = WinApi.CreateProcess(null, Path, ref sa, ref sa, false, Constants.DEBUG_PROCESS + Constants.DEBUG_ONLY_THIS_PROCESS, nu, null, ref sInfo, out pInfo);
            if (!rev)
            {
                Notifier.CreateToastNotification("创建进程失败");
                return -1;
            }
            Boolean Flag = false;//是否要跳出while循环？
            int a = 0;
            while(WinApi.WaitForDebugEvent(ref DebugEvent, int.MaxValue))
            {
                a++;
                Console.WriteLine("调试："+a.ToString());
                switch(DebugEvent.dwDebugEventCode)
                {
                    case 1://程序异常 Constants.EXCEPTION_DEBUG_EVENT
                        uint exitcode1 = new uint();
                        uint exitcode2 = new uint();
                        WinApi.GetExitCodeProcess(pInfo.hProcess, out exitcode1);
                        WinApi.GetExitCodeThread(pInfo.hThread, out exitcode2);
                        Console.WriteLine("返回非零 process:" + exitcode1.ToString()+" thread:"+exitcode2.ToString());
                        Flag = true;
                        break;
                    case 2://线程被创建
                        //Notifier.CreateToastNotification("线程创建：", pInfo.hThread.ToString());
                        Console.WriteLine("线程创建");
                        Console.WriteLine(pInfo.hThread.ToString());
                        break;
                    case 3://进程被创建
                        //Notifier.CreateToastNotification("进程创建：", pInfo.hProcess.ToString());
                        Console.WriteLine("进程创建");
                        Console.WriteLine(pInfo.hProcess.ToString());
                        break;
                    case 4://退出线程
                        //Notifier.CreateToastNotification("退出线程：");
                        Console.WriteLine("退出线程");
                        break;
                    case 5://退出进程
                        //Notifier.CreateToastNotification("退出进程：");
                        Console.WriteLine("退出进程");
                        const uint DBG_EXCEPTION_NOT_HANDLED = 0x80010001;
                        WinApi.ContinueDebugEvent(DebugEvent.dwProcessId, DebugEvent.dwThreadId, DBG_EXCEPTION_NOT_HANDLED);//#DBG_EXCEPTION_NOT_HANDLED = 2147549185
                        Flag = true;
                        break;
                }
                if (Flag)
                {
                    Init();
                    break;
                }
                WinApi.ContinueDebugEvent(DebugEvent.dwProcessId, DebugEvent.dwThreadId, 65538);//#DBG_CONTINUE = 65538
            }
            Console.WriteLine("准备关闭句柄");
            WinApi.CloseHandle(pInfo.hProcess);
            WinApi.CloseHandle(pInfo.hThread);
            Console.WriteLine("关闭句柄");
            return 0;
        }
    }

}
