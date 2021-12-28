using System;
using System.Collections.Generic;
using System.Text;


using System.Runtime.InteropServices;
using DMDEFS;

namespace OHIOAPI
{
        // managed definition of CMNERROR
        [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode, Pack=1)]
        public class CMN_ERROR
        {
            [MarshalAs(UnmanagedType.U4)]
            public /*UInt32*/DM_ERROR_ENUM dwError1;

            public UInt32 dwError2;
            public UInt32 dwError3;
            public UInt32 dwError4;
            public UInt32 dwError5;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public String szErrorText;
        }
}
