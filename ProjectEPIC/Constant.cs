using System;

namespace ProjectEPIC
{
    class Constant
    {
        /// <summary>
        /// 比较类型：忽略行尾空格
        /// </summary>
        public const int CT_IgnoreLineths = 1;
        /// <summary>
        /// 比较类型：不忽略行尾空格
        /// </summary>
        public const int CT_UnIgnoreLine = 2;
        /// <summary>
        /// 比较类型：自定义比较器
        /// </summary>
        public const int CT_CustomComparator = 3;
        /// <summary>
        /// 运行类型：编译源文件并且限制源文件调用非法函数
        /// </summary>
        public const int RT_SourceLimit = 1;
        /// <summary>
        /// 运行类型：编译源文件，对非法函数不做任何限制
        /// </summary>
        public const int RT_SourceWithoutLimit = 2;
        /// <summary>
        /// 运行类型：直接运行编译后的二进制文件
        /// </summary>
        public const int RT_Bin = 3;
    }
}
