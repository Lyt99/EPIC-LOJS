using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPIC_Core
{
    static class Constants
    {
        /// <summary>
        /// 使用 dwXSize 和 dwYSize 成员
        /// </summary>
        public const int STARTF_USESIZE = 2;
        /// <summary>
        /// 使用 wShowWindow 成员
        /// </summary>
        public const int STARTF_USESHOWWINDOW = 1;
        /// <summary>
        /// 使用d w X 和d w Y 成员
        /// </summary>
        public const int STARTF_USEPOSITION = 4;
        /// <summary>
        /// 使用d w X C o u n t C h a r s 和dwYCount Chars 成员
        /// </summary>
        public const int STARTF_USECOUNTCHARS = 8;
        /// <summary>
        /// 使用d w F i l l A t t r i b u t e 成员
        /// </summary>
        public const int STARTF_USEFILLATTRIBUTE = 16;
        /// <summary>
        /// 使用h S t d I n p u t 、h S t d O u t p u t 和h S t d E r r o r 成员
        /// </summary>
        public const int STARTF_USESTDHANDLES = 256;
        /// <summary>
        /// 强制在x 8 6 计算机上运行的控制台应用程序以全屏幕方式启动运行
        /// </summary>
        public const int STARTFRUNFULLSCREEN = 32;
        public const int DEBUG_PROCESS = 1;
        public const int DEBUG_ONLY_THIS_PROCESS = 2;
        /// <summary>
        /// 程序企图读写一个不可访问的地址时引发的异常。例如企图读取0地址处的内存。
        /// </summary>
        public const uint EXCEPTION_ACCESS_VIOLATION = 0xC0000005;
        /// <summary>
        /// 数组访问越界时引发的异常
        /// </summary>
        public const uint EXCEPTION_ARRAY_BOUNDS_EXCEEDED = 0xC000008C;
        /// <summary>
        /// 触发断点时引发的异常
        /// </summary>
        public const uint EXCEPTION_BREAKPOINT = 0x80000003;
        /// <summary>
        /// 程序读取一个未经对齐的数据时引发的异常
        /// </summary>
        public const uint EXCEPTION_DATATYPE_MISALIGNMENT = 0x80000002;
        /// <summary>
        /// 如果浮点数操作的操作数是非正常的，则引发该异常。所谓非正常，即它的值太小以至于不能用标准格式表示出来
        /// </summary>
        public const uint EXCEPTION_FLT_DENORMAL_OPERAND = 0xC000008D;
        /// <summary>
        /// 浮点数除法的除数是0时引发该异常
        /// </summary>
        public const uint EXCEPTION_FLT_DIVIDE_BY_ZERO = 0xC000008E;
        /// <summary>
        /// 浮点数操作的结果不能精确表示成小数时引发该异常
        /// </summary>
        public const uint EXCEPTION_FLT_INEXACT_RESULT = 0xC000008F;
        /// <summary>
        /// 该异常表示不包括在这个表内的其它浮点数异常
        /// </summary>
        public const uint EXCEPTION_FLT_INVALID_OPERATION = 0xC0000090;
        /// <summary>
        /// 浮点数的指数超过所能表示的最大值时引发该异常
        /// </summary>
        public const uint EXCEPTION_FLT_OVERFLOW = 0xC0000091;
        /// <summary>
        /// 进行浮点数运算时栈发生溢出或下溢时引发该异常
        /// </summary>
        public const uint EXCEPTION_FLT_STACK_CHECK = 0xC0000092;
        /// <summary>
        /// 浮点数的指数小于所能表示的最小值时引发该异常
        /// </summary>
        public const uint EXCEPTION_FLT_UNDERFLOW = 0xC0000093;
        /// <summary>
        /// 程序企图执行一个无效的指令时引发该异常
        /// </summary>
        public const uint EXCEPTION_ILLEGAL_INSTRUCTION = 0xC000001D;
        /// <summary>
        /// 程序要访问的内存页不在物理内存中时引发的异常
        /// </summary>
        public const uint EXCEPTION_IN_PAGE_ERROR = 0xC0000006;
        /// <summary>
        /// 整数除法的除数是0时引发该异常
        /// </summary>
        public const uint EXCEPTION_INT_DIVIDE_BY_ZERO = 0xC0000094;
        /// <summary>
        /// 整数操作的结果溢出时引发该异常
        /// </summary>
        public const uint EXCEPTION_INT_OVERFLOW = 0xC0000095;
        /// <summary>
        /// 异常处理器返回一个无效的处理的时引发该异常
        /// </summary>
        public const uint EXCEPTION_INVALID_DISPOSITION = 0xC0000026;
        /// <summary>
        /// 发生一个不可继续执行的异常时，如果程序继续执行，则会引发该异常
        /// </summary>
        public const uint EXCEPTION_NONCONTINUABLE_EXCEPTION = 0xC0000025;
        /// <summary>
        /// 程序企图执行一条当前CPU模式不允许的指令时引发该异常
        /// </summary>
        public const uint EXCEPTION_PRIV_INSTRUCTION = 0xC0000096;
        /// <summary>
        /// 标志寄存器的TF位为1时，每执行一条指令就会引发该异常。主要用于单步调试
        /// </summary>
        public const uint EXCEPTION_SINGLE_STEP = 0x80000004;
        /// <summary>
        /// 栈溢出时引发该异常
        /// </summary>
        public const uint EXCEPTION_STACK_OVERFLOW = 0xC00000FD;
        /// <summary>
        /// 程序异常 应该进入异常处理阶段
        /// </summary>
        public const uint EXCEPTION_DEBUG_EVENT = 1;
    }
}
