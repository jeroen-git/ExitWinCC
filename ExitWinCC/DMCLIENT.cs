using System;
using System.Collections.Generic;
using System.Text;


using System.Runtime.InteropServices;

using OHIOAPI;
using DMDEFS;


namespace DMCLIENT
{

    public enum DM_OS_ENUM
    {
        DM_OS_UNKNOWN = 0,
        DM_OS_NT = 1,
        DM_OS_32S = 2,
        DM_OS_CHICAGO =3,

        DM_OS_2000 = 4,
        DM_OS_XP = 5,
        DM_OS_2003=6
    }

    public enum DM_SDMODE_ENUM
    {
        DM_SDMODE_SYSTEM = 0, // The operating system is shut down after closing WinCC.
        DM_SDMODE_WINCC = 1, // WinCC is shut down
        DM_SDMODE_LOGOFF = 2, //  After closing WinCC, the user is also logged off on operating system level.
        DM_SDMODE_REBOOT = 3 // The operating system is rebooted after the shutdown of WinCC.
    }

    #region C++ Structures
    public struct Rect
    {
        public UInt32 Left, Top, Right, Bottom;
    }
    #endregion

    #region WinCC Structures

    // Update cycles
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_CYCLE_INFO
    {
        public UInt32 dwCycleTime; // Time base of the update cycle

        [MarshalAs(UnmanagedType.U4)]
        public DM_CYCLE_ENUM/*UInt32*/ dwCycleIndex; // Specifies the sequence within the update cycle list.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CYCLE_NAME + 1))]
        public string szDescription;
    }

    // Data transfer channel
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_DATA_SERVICE
    {
        public UInt32 dwTeleType; // The parameter is reserved for future use and requires the default 0.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_SERVICE_NAME + 1))]
        public string szService; // Name of the data transfer channel. Identical with the name specified during the installation of the service (DMInstallDataService).

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_APP_NAME + 1))]
        public string szSendingApp; // Logical name of the sender. Identical with the application name specified in DMConnect.

        public UInt32 dwSendingMachine; // Index of the computer from which the package was sent (0 - 63). 
        public UInt32 dwDataSize; // Size of the data package in bytes: Data[0] ... byData[dwDataSize - 1]
        public IntPtr byData; // Pointer to the data.
    }

    // Tag group selection
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_GROUPFILTER
    {
        [MarshalAs(UnmanagedType.U4)]
        public DM_GROUPFILTER_FLAG/*UInt32 */ dwFlags;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1))]
        public string szConnectionName; // Name of the logical connection

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_UNIT_NAME + 1))]
        public string szUnitName; // The name of the channel unit is used as selection criterion.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szDriverName; // Name of the driver
    }

    // Specification of applications
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_SD_TARGET_APP
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_APP_NAME + 1))]
        public string szAppName; // Name of the application used in the call of DMConnect.
    }

    // Computer information
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_SD_TARGET_MACHINE
    {
        public bool fServer; // Specifies whether the computer is a server or a client.
        public bool fLocal; // Specifies whether the inquiring application runs on the local computer or on another configured computer in the network.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_COMPUTERNAME_LENGTH + 1))]
        public string szMachineName; // Computer name
    }

    // Computer list
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_MACHINE_TABLE
    {
        public long nNumMachines; // Number of computers in the project
        public long nLocalMachine; // Index of the entry of the local computer in the computer list.

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_OHIO_MACHINES), SafeArrayUserDefinedSubType = typeof(DM_SD_TARGET_MACHINE))]
        public DM_SD_TARGET_MACHINE[] tm; // Complete array of the DM_SD_TARGET_MACHINE type containing information on the computers involved in the project.
    }

    // Specification of data exchange partners
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_SEND_DATA_STRUCT
    {
        public bool fHighPriority; // 1:high priority , 0:normal priority

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_SERVICE_NAME + 1))]
        public string szService; // Name of the service to be used. 

        [MarshalAs(UnmanagedType.U4)]
        public DM_SD/*UInt32*/ dwTargetMachineFlags; // Specifies the applications to which data are to be sent using the DMSendApplicationData function.

        public UInt32 dwTargetMachines; // Number of completed structures in dmTargetMachine.

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_OHIO_MACHINES), SafeArrayUserDefinedSubType = typeof(DM_SD_TARGET_MACHINE))]
        public DM_SD_TARGET_MACHINE[] dmTargetMachine; // Pointer to the DM_SD_TARGET_MACHINE structure to specify the data transfer destination.

        public UInt32 dwTargetApps; // 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_OHIO_APPLICATIONS), SafeArrayUserDefinedSubType = typeof(DM_SD_TARGET_APP))]
        public DM_SD_TARGET_APP[] dmTargetApp; // The structures of the DM_SD_TARGET_APP type contain the names of the data transfer target applications.

        public UInt32 dwDataSize; // Number of completed structures in dmTargetApp.

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public Byte[] byData; // Array that contains the data to be sent.
    }

    // Dialog options
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_DLGOPTIONS
    {
        public UInt32 dwFlags; // dwFlags specifies the behavior of the dialog or dialog box.

        [MarshalAs(UnmanagedType.U4)]
        public Rect lprcPreference; // Pointer to a structure of the RECT type containing the dialog box size information. If lprcPreference = NULL, the dialog box is centered and has a predefined size.

        public IntPtr lpfnTestDropTarget; // The parameter is reserved for future use and requires the default NULL.
        public IntPtr lpfnDropTarget; // The parameter is reserved for future use and requires the default NULL.
        public IntPtr lpvUser; // Pointer to application-specific data.
    }

    // Tag type reference
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_TYPEREF
    {
        [MarshalAs(UnmanagedType.U4)]
        public /*UInt32*/ DM_VARTYPE dwType; // dwType specifies the tag type

        public UInt32 dwSize; // dwSize specifies the length of the data type (in bytes) on the OS.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_TYPE_NAME + 1))]
        public string szTypeName; // szTypeName specifies the name of the structure type for structure tags.
    }

    // Structured tag (structure for type description)
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_TYPE_UNION
    {
        [MarshalAs(UnmanagedType.Struct)]
        public DM_BITFIELD_DESCRIPTOR dmBitField;

        [MarshalAs(UnmanagedType.Struct)]
        public DM_ARRAY_DESCRIPTOR dmArray;

        [MarshalAs(UnmanagedType.Struct)]
        public DM_STRUCT_DESCRIPTOR dmStruct; // Structure is used to specify the number of tags belonging to a structure type.
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_BITFIELD_DESCRIPTOR
    {
        public UInt32 dwNumBits;
        public UInt32 dwBits;  
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_ARRAY_DESCRIPTOR
    {
        public UInt32 dwNumElements;
        public UInt32 dwASElemSize;
        public UInt32 dwASElemOffset;

        [MarshalAs(UnmanagedType.Struct)]
        public DM_FORMAT_INFO dmFormat;

        [MarshalAs(UnmanagedType.Struct)]
        public DM_SCALE_INFO dmScale;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_STRUCT_DESCRIPTOR
    {
        public UInt32 dwNumMembers;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_FORMAT_INFO
    {
        public UInt32 dwID;        
         
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst =(DM_LENGTH_DEFINES.MAX_DM_FORMAT_NAME + 1))]
        public string szName;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_SCALE_INFO
    {
        public UInt32 dwID;                                                               

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_SCALE_NAME + 1))]
        public string szName;                                   

        public UInt32 dwNumParams;    
    }

    // Tag specification
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARKEY
    {
        [MarshalAs(UnmanagedType.U4)]
        public /*UInt32*/DM_VARKEY_TYPE dwKeyType;
        // dwKeyType specifies whether the tag is to be addressed by a key ID or by its name:

        public UInt32 dwID;//Contains the key ID of the tag if dwKeyType has been set accordingly.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*129*/(DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1))]
        public String szName;//Contains the name of the tag if dwKeyType has been set accordingly.

        public IntPtr lpvUserData;// Pointer to application-specific data.
    }

    // Tag group information
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARGRP_DATA
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1))]
        public string szName; // Name of the tag group

        public UInt32 dwCreatorID; // The creator ID can be used to enter the creator of an object.
        public UInt32 dwVarNum; // Number of tags within the tag group
        public IntPtr lpvUserData; // Pointer to application-specific data.
    }

    // Tag group specification
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARGRPKEY
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME +1 ))]
        public string szName; // Name of the tag group

        public IntPtr lpcUserData; // Pointer to application-specific data.
    }

    // Tag limit values
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARLIMIT
    {
        public object dmMaxRange; // High limit of format conversion

        public object dmMinRange; // Low limit of format conversion

        public object dmMaxLimit; // High limit of tag.

        public object dmMinLimit; // Low limit of tag.
    }

    // with information on of a tag
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARIABLE_DATA
    {
        public DM_TYPEREF dmTypeRef; // The DM_TYPEREF structure contains tag type information.

        public DM_VARLIMIT dmVarLimit; // The DM_VARLIMIT structure contains tag limit information

        public object dmStart; // Start value of the tag. 

        public object dmDefault; // Default value of the tag.

        [MarshalAs(UnmanagedType.U4)]
        public DM_NOTIFY_VARAIABLE /*UInt32*/ dwNotify; // specifies for which events a log entry is generated

        [MarshalAs(UnmanagedType.U4)]
        public DM_FLAGS_VARIABLE/*UInt32*/ dwFlags; // Specifies how the substitute value is to be used.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1 ))]
        public string szSpecific;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1 ))]
        public string szGroup;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1 ))]
        public string szConnection;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES._MAX_PATH + 1 ))]
        public string szChannel;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_UNIT_NAME + 1 ))]
        public string szUnit;
    }

    // with information on of a tag
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARIABLE_DATA4
    {
        public DM_TYPEREF dmTypeRef; // The DM_TYPEREF structure contains tag type information.

        public DM_VARLIMIT dmVarLimit; // The DM_VARLIMIT structure contains tag limit information

        public object dmStart; // Start value of the tag. 

        public object dmDefault; // Default value of the tag.

        public DM_NOTIFY_VARAIABLE /*UInt32*/ dwNotify; // specifies for which events a log entry is generated
        public DM_FLAGS_VARIABLE/*UInt32*/ dwFlags; // Specifies how the substitute value is to be used.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1 ))]
        public string szSpecific;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1 ))]
        public string szGroup;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1 ))]
        public string szConnection;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES._MAX_PATH + 1 ))]
        public string szChannel;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES.MAX_DM_UNIT_NAME + 1 ))]
        public string szUnit;

        public MCP_VARIABLE_SCALES Scaling;

        public UInt32 dwASDataSize;
        public UInt32 dwOSDataSize;
        public DM_VARPROPERTY/*UInt32*/ dwVarProperty;
        public UInt32 dwFormat;
    }

    // Path and file names of configuration data
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_DIRECTORY_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*256*/(DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectDir; // Complete path of the project directory, e.g. "d:\WinCC\Project1"

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*256*/(DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectAppDir; // Complete path of the application subdirectory in the project directory, e.g.: "d:\WinCC\Project1\GraCS"

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*256*/(DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szGlobalLibDir; // Complete path of the project-independent library folder, e.g.: "d:\WinCC\aplib"

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*256*/(DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectLibDir; // Complete path of the project-specific library folder, e.g. "d:\WinCC\Project1\Library"

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*256*/(DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szLocalProjectAppDir; // Complete path of the application subdirectory in the the project directory on the local computer.
    }

    // Project information
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_PROJECT_INFO
    {
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectFile; // File name of the project, including path and extension.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_DSN_NAME + 1))]
        public string szDSNName; // Data source name of the database.

        public UInt32 dwDataLocale; // Code of the language used during configuration.
    }

    // Tag description
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_COMMON
    {
        [MarshalAs(UnmanagedType.U4)]
        public DM_VARTYPE /*UInt32*/ dwVarType; // specifies the tag type

        public UInt32 dwVarLength; // The length of the tag is only relevant for the DM_VARTYPE_TEXT_8 and DM_VARTYPE_TEXT_16 text tags.The text length is specified in characters (1...255).

        [MarshalAs(UnmanagedType.U4)]
        public DM_VARPROPERTY/*UInt32*/ dwVarProperty; // Specifies whether an internal or an external tag is involved

        public UInt32 dwFormat; // Number of the conversion routine to be used. For more information, refer to the "Conversion routines" section.
    }

    // Tag description extended
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_COMMON_EX
    {
        [MarshalAs(UnmanagedType.U4)]
        public DM_VARTYPE /*UInt32*/ dwVarType; // specifies the tag type

        public UInt32 dwCreatorID; // The creator ID can be used to enter the creator of an object. The values 0 - 10100 and 11000 - 11100 are reserved internally or for specific systems.
        public UInt32 dwVarLength; // The length of the tag is only relevant for the DM_VARTYPE_TEXT_8 and DM_VARTYPE_TEXT_16 text tags.The text length is specified in characters (1...255).

        [MarshalAs(UnmanagedType.U4)]
        public DM_VARPROPERTY/*UInt32*/ dwVarProperty; // Specifies whether an internal or an external tag is involved

        public UInt32 dwFormat; // Number of the conversion routine to be used. For more information, refer to the "Conversion routines" section.
        public UInt32 dwOSOffset; // The parameter is reserved for future use and requires the default 0.
        public UInt32 dwASOffset; // Offset in PLC buffer In WinCC V6 SP3 or higher, the bit-oriented ASOffset is handled internally in BYTE and not in WORD. ASOffset calculations for the GAPICreateType and GAPICreateType4 functions which still use a multiplier of 16 have to use a multiplier of 8 in the future.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst=255)]
        public string szStructTypeName; // Name of the structure type in which the entity is created
    }

    // Tag limit values
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_LIMITS
    {
        public double dTopLimit; // High limit value of the tag
        public double dBottomLimit; // Low limit value of the tag
        public double dStartValue; // Start value of the tag
        public double dSubstituteValue; // Substitute value of the tag
        public bool bTopLimit; // If this parameter is set, the substitute value is used when the value of the tag exceeds the value specified in dTopLimit.
        public bool bBottomLimit; // If this parameter is set, the substitute value is used when the value of the tag falls below the value specified in dBottomLimit.
        public bool bStartValue; // If this parameter is set, the substitute value is used as start value.
        public bool bConnectionErr; // If this parameter is set, the high limit value specified in dTopLimit is valid.
        public bool bTopLimitValid; // If this parameter is set, the high limit value specified in dTopLimit is valid.
        public bool bBottomLimitValid; // If this parameter is set, the low limit value specified in dBottomLimit is valid.
        public bool bStartValueValid; // If this parameter is set, the start value specified in dStartValue is valid.
        public bool bSubstValueValid; // If this parameter is set, the substitute value specified in dSubstitudeValue is valid.
    }

    // Tag limit values
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_LIMITS5
    {
        public object dTopLimit; // High limit value of the tag
        public object dBottomLimit; // Low limit value of the tag
        public object dStartValue; // Start value of the tag
        public object dSubstituteValue; // Substitute value of the tag
        public bool bTopLimit; // If this parameter is set, the substitute value is used when the value of the tag exceeds the value specified in dTopLimit.
        public bool bBottomLimit; // If this parameter is set, the substitute value is used when the value of the tag falls below the value specified in dBottomLimit.
        public bool bStartValue; // If this parameter is set, the substitute value is used as start value.
        public bool bConnectionErr; // If this parameter is set, the high limit value specified in dTopLimit is valid.
        public bool bTopLimitValid; // If this parameter is set, the high limit value specified in dTopLimit is valid.
        public bool bBottomLimitValid; // If this parameter is set, the low limit value specified in dBottomLimit is valid.
        public bool bStartValueValid; // If this parameter is set, the start value specified in dStartValue is valid.
        public bool bSubstValueValid; // If this parameter is set, the substitute value specified in dSubstitudeValue is valid.
    }

    // Tag limit values extended
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_LIMITS_EX
    {
        public double dTopLimit; // High limit value of the tag
        public double dBottomLimit; // Low limit value of the tag
        public double dStartValue; // Start value of the tag
        public double dSubstituteValue; // Substitute value of the tag

        [MarshalAs(UnmanagedType.U4)]
        public MCP_VARLIM_HAS/*UInt32*/ dwLimitFlags; // The LimitFlags parameter controls the validity of the default values and limits of a tag.

        public UInt32 dwTextBibStartText; // The parameter is only relevant for text tags. If you want to use a start-up value contained in the text library, the ID of that text should be entered here.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = 255)]
        public string szTextSubstitude; // The parameter is only relevant for text tags. If you want to use a default value contained in the text library, the ID of that text should be entered here.
    }

    // Handling of tag limit values
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_PROTOCOL
    {
        public bool bTopLimitErr; // A log entry is generated if the value of the tag violates the high limit.
        public bool bBottomLimitErr; // A log entry is generated if the value of the tag violates the low limit.
        public bool bTransformationErr; // A log entry is generated if a conversion error occurs.
        public bool bWriteErr; // A log entry is generated every time an illegal write access is attempted.
        public bool bWriteErrApplication; // A log entry is generated every time an illegal write access is attempted by the application.
        public bool bWriteErrProzess; // A log entry is generated every time an illegal write access is attempted by the process
    }

    // Handling of tag limit values extended
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_PROTOCOL_EX
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_VARPROT dwProtocolFlags; // The dwProtocolFlags parameter specifies the event which generates a log entry.
    }

    // 
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_VARIABLE_SCALES
    {
        [MarshalAs(UnmanagedType.U4)]
        public DM_VARSCALE/*UInt32*/ dwVarScaleFlags; 

        public double doMinProc; // Minimum value of the tag in the process
        public double doMaxProc; // Maximum value of the tag in the process
        public double doMinVar;  // Minimum value of the tag in WinCC
        public double doMaxVar;  // Maximum value of the tag in WinCC 
    }

    // Structured tag (structure for type description)
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_TYPE_DESCRIPTOR
    {
        [MarshalAs(UnmanagedType.Struct)]
        public DM_TYPEREF dmTypeRef; // Contains tag type information.
        [MarshalAs(UnmanagedType.Struct)]
        public DM_TYPE_UNION dmType; // Is used to specify the number of tags belonging to a structure type.
    }

    // Tag definition
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_NEWVARIABLE_DATA
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_NVAR_FLAG/*UInt32*/ dwFlags; // specifies how to edit the tag

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectFile; // Name of the project file, including path and extension.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 3))]
        public string szConnection; // Name of the logical connection allocated to the tag.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1))]
        public string szVarName; // Name of the new tag to be edited

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1))]
        public string szGroupName; // Name of the group the tag belongs to

        public MCP_VARIABLE_COMMON Common; // Name of the logical connection allocated to the tag.

        public MCP_VARIABLE_PROTOCOL Protocol; //  Containing instructions how to handle limit violations by the tag

        public MCP_VARIABLE_LIMITS Limits; // Containing the limits of the tag.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1))]
        public string szSpecific; // containing the limits of the tag.
    }

    // Tag definition
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode,Pack = 1)]
    public class MCP_NEWVARIABLE_DATA_4
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_NVAR_FLAG/*UInt32*/ dwFlags; // specifies how to edit the tag

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectFile; // Name of the project file, including path and extension.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 3))]
        public string szConnection; // Name of the logical connection allocated to the tag.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1))]
        public string szVarName; // Name of the new tag to be edited

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1))]
        public string szGroupName; // Name of the group the tag belongs to

        public MCP_VARIABLE_COMMON Common; // Name of the logical connection allocated to the tag.

        public MCP_VARIABLE_PROTOCOL Protocol; //  Containing instructions how to handle limit violations by the tag

        public MCP_VARIABLE_LIMITS Limits; // Containing the limits of the tag.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1))]
        public string szSpecific; // containing the limits of the tag.

        public MCP_VARIABLE_SCALES Scaling; // Containing the description of the tag scaling.
    }
       
        // Tag definition
    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode,Pack = 1)]
    public class MCP_NEWVARIABLE_DATA_5
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_NVAR_FLAG/*UInt32*/ dwFlags; // specifies how to edit the tag

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectFile; // Name of the project file, including path and extension.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 3))]
        public string szConnection; // Name of the logical connection allocated to the tag.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1))]
        public string szVarName; // Name of the new tag to be edited

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1))]
        public string szGroupName; // Name of the group the tag belongs to

        public MCP_VARIABLE_COMMON Common; // Name of the logical connection allocated to the tag.

        public MCP_VARIABLE_PROTOCOL Protocol; //  Containing instructions how to handle limit violations by the tag

        public MCP_VARIABLE_LIMITS5 Limits; // Containing the limits of the tag.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1))]
        public string szSpecific; // containing the limits of the tag.

        public MCP_VARIABLE_SCALES Scaling; // Containing the description of the tag scaling.
    }

    // Tag definition
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_NEWVARIABLE_DATA_EX
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_NVAR_FLAG /*UInt32*/ dwFlag; // specifies how to edit the tag
 
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1 ))]
        public string szProjectFile; // Name of the project file, including path and extension

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1 ))]
        public string szConnection; // Name of the logical connection allocated to the tag.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1 ))]
        public string szVarName; // Name of the new tag to be edited.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1 ))]
        public string szGroupName; // Name of the group the tag belongs to

        public MCP_VARIABLE_COMMON_EX Common; // Containing the description of the tag.

        public MCP_VARIABLE_PROTOCOL_EX Protocol; // Containing instructions how to handle limit violations by the tag.

        public MCP_VARIABLE_LIMITS_EX Limits; // Xontaining the limits of the tag

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1 ))]
        public string szSpecific; // Contains the address relation of the tag, e.g. data block and bytes in the block, etc.
    }

    // Tag definition
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_NEWVARIABLE_DATA_EX4
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_NVAR_FLAG /*UInt32*/ dwFlag; // specifies how to edit the tag
 
        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1 ))]
        public string szProjectFile; // Name of the project file, including path and extension

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1 ))]
        public string szConnection; // Name of the logical connection allocated to the tag.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_NAME + 1 ))]
        public string szVarName; // Name of the new tag to be edited.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_GROUP_NAME + 1 ))]
        public string szGroupName; // Name of the group the tag belongs to

        public MCP_VARIABLE_COMMON_EX Common; // Containing the description of the tag.

        public MCP_VARIABLE_PROTOCOL_EX Protocol; // Containing instructions how to handle limit violations by the tag.

        public MCP_VARIABLE_LIMITS_EX Limits; // Containing the limits of the tag

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst = (DM_LENGTH_DEFINES.MAX_DM_VAR_SPECIFIC + 1 ))]
        public string szSpecific; // Contains the address relation of the tag, e.g. data block and bytes in the block, etc.

        public MCP_VARIABLE_SCALES Scaling; // Containing the description of the tag scaling
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_DELETEVARIABLE_DATA
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_DVAR_FLAG /*UInt32*/ dwFlags; //specifies how to edit the tag.

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES._MAX_PATH +1))]
        public string szProjectFile;

        [MarshalAs(UnmanagedType.ByValTStr,SizeConst= (DM_LENGTH_DEFINES._MAX_PATH +1))]
        public string szVarName;
    }

    // Retrieve tag values
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VAR_UPDATE_STRUCT
    {
        public DM_TYPEREF dmTypeRef; // The DM_TYPEREF structure contains tag type information.

        public DM_VARKEY dmVarKey; // The DM_VARKEY structure is used to specify the tag to be edited.

        public object dmValue; // Value of the tag

        [MarshalAs(UnmanagedType.U4)]
        public DM_VARSTATE/*UInt32*/ dwState; // Indicates whether the value of the tag has been changed successfully or whether errors have occurred.
    }

    // 
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VARFILTER
    {
        [MarshalAs(UnmanagedType.U4)]
        public DM_VARFILTER_FLAGS dwFlags; // The parameter can be used to set a tag selection criterion.

        public UInt32 dwNumTypes; // Number of tag types specified in pdwTypes.

        [MarshalAs(UnmanagedType.U4)]
        public DM_VARTYPE pdwTypes; // Specifies the tag types to be used as selection criterion.

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszGroup; // Pointer to the name of a tag group. This name is to be used as selection criterion

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszName; // Pointer to the name of a tag. This name is to be used as selection criterion.

        [MarshalAs(UnmanagedType.LPWStr)]
        public string lpszConn; // Pointer to the name of a logical connection.
    }

    // Retrieve tag values
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_VAR_UPDATE_STRUCTEX
    {
        public DM_TYPEREF dmTypeRef; // The DM_TYPEREF structure contains tag type information.

        public DM_VARKEY dmVarKey; // The DM_VARKEY structure is used to specify the tag to be edited.

        public object dmValue; // Value of the tag

        [MarshalAs(UnmanagedType.U4)]
        public DM_VARSTATE/*UInt32*/ dwState; // Indicates whether the value of the tag has been changed successfully or whether errors have occurred.

        public UInt32 dwQualityCode; // Quality code of the tag value
    }

    // Connection data
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_CONNECTION_DATA
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 3))]
        public string szConnection; // Name of the logical connection

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_UNIT_NAME + 1))]
        public string szUnitName; // Name of the channel unit

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CON_COMMON + 1))]
        public string szCommon; // 

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CON_SPECIFIC + 1))]
        public string szSpecific; // szSpecific contains the address parameters of the connection, e.g. Ethernet address, slot number, etc.; for PLC-specific details refer to the Communications Manual.

        public UInt32 dwVarNum; // Number of allocated tags
    }

    // Enumerate connection data (filter structure)
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class DM_CONNKEY
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = /*256*/(DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1))]
        public string szName; // Name of the logical connection

        public IntPtr lpvUserData;// Pointer to application-specific data.
    }

    // Connection data of a logical connection
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_NEWCONNECTION_DATA
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_NCON_FLAG dwFlags; // Specifies how to edit connection.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectFile; // File name of the project, including path and extension

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_UNIT_NAME + 1))]
        public string szUnitName; // Name of the channel unit.

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1))]
        public string szConnection; // Name of the logical connection to be created

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CON_COMMON + 1))]
        public string szCommon; //

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CON_SPECIFIC + 1))]
        public string szSpecific; // Contains the address parameters of the connection.
    }

    // Delete a logical connection
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public class MCP_DELETECONNECTION_DATA
    {
        [MarshalAs(UnmanagedType.U4)]
        public MCP_DCON_FLAG dwFlags; // Specifies how to edit the logical connection

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES._MAX_PATH + 1))]
        public string szProjectFile; // File name of the project, including path and extension

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (DM_LENGTH_DEFINES.MAX_DM_CONNECTION_NAME + 1))]
        public string szConnection; // Name of the logical connection to be created
    }
    #endregion


    // EventArgs-Class and delegate for the notify event
    // DMNotifyEventArgs hold the notify data in protected form and handled as readonly properties
    public class DMNotifyEventArgs : EventArgs
    {
        // data members
        private readonly UInt32 dwNotifyClass;
        private readonly UInt32 dwNotifyCode;
        private readonly IntPtr lpbyData;
        private readonly UInt32 dwItems;
        private readonly IntPtr lpvUser;

        // init constructor to set the data
        public DMNotifyEventArgs(UInt32 dwNotifyClass,
                                 UInt32 dwNotifyCode,
                                 IntPtr lpbyData,
                                 UInt32 dwItems,
                                 IntPtr lpvUser)
        {
            this.dwNotifyClass = dwNotifyClass;
            this.dwNotifyCode = dwNotifyCode;
            this.lpbyData = IntPtr.Zero;
            this.lpvUser = lpvUser;
            this.dwItems = dwItems;
            if (0 != dwItems)
            {
                this.lpbyData = lpbyData;
            }
        }

        //------------------------------
        // properties to get the data's:

        public UInt32 NotifyClass
        {
            get { return dwNotifyClass; }
        }

        public UInt32 NotifyCode
        {
            get { return dwNotifyCode; }
        }

        public UInt32 Count
        {
            get { return dwItems; }
        }

        public IntPtr ByData
        {
            get { return lpbyData; }
        }

        public IntPtr VUser
        {
            get { return lpvUser; }
        }
    }

    // is used by the InitConstructor of the CDMClientWrapper to connect the notify event
    public delegate void DMNotifyEventHandler(object sender, DMNotifyEventArgs e);


    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DmEnumVarProc([MarshalAs(UnmanagedType.LPStruct)] DM_VARKEY lpdmVarKey,
                                       IntPtr lpvUser);


    #region Delegete Enumerate Functions

    // To make sure that your application can be notified of, for example, a new language selected, you must provide a callback function of the DM_NOTIFY_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DmNotifyProc(UInt32 dwNotifyClass, // identifies the notify message class
                                      UInt32 dwNotifyCode, // Notify code
                                      IntPtr lpbyData, // Pointer to the data provided within the DM_NOTIFY_CLASS_DATA class
                                      UInt32 dwItems, // Number of entries in lpbyData
                                      IntPtr lpvUser); // Pointer to application-specific data

    // In order to evaluate the projects enumerated by the system, you must provide a callback function of the DM_OPENED_PROJECTS_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumOpenedProjectsProc([MarshalAs(UnmanagedType.LPStruct)] DM_PROJECT_INFO lpInfo, // Pointer to a structure of the DM_PROJECT_INFO type containing the information on an open project.
                                       IntPtr lpvUser); // Pointer to application-specific data.

    // In order to evaluate the projects enumerated by the system, you must provide a callback function of the DM_OPENED_PROJECTS_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumVariableProc([MarshalAs(UnmanagedType.LPStruct)] DM_VARKEY lpdmVarKey, // Pointer to a structure of the DM_PROJECT_INFO type containing the information on an open project.
                                            [MarshalAs(UnmanagedType.LPStruct)] DM_VARIABLE_DATA lpdmVarData, // Pointer to a structure of the DM_VARIABLE_DATA containing the tag information.
                                            IntPtr lpvUser); // Pointer to application-specific data.

    // In order to evaluate the tag information enumerated by the system, you must provide a callback function of the DM_ENUM_VARIABLE_PROC4 type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumVariableProc4([MarshalAs(UnmanagedType.LPStruct)] DM_VARKEY lpdmVarKey, // Pointer to a structure of the DM_PROJECT_INFO type containing the information on an open project.
                                            [MarshalAs(UnmanagedType.LPStruct)] DM_VARIABLE_DATA4 lpdmVarData, // Pointer to a structure of the DM_VARIABLE_DATA containing the tag information.
                                            IntPtr lpvUser); // Pointer to application-specific data.

    // In order to evaluate the tag names enumerated by the system, you must provide a callback function of the DM_ENUM_TYPEMEMBERS_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumTypeMembersProc(string lpszMemberName, // Pointer to the name of the tag belonging to a structured tag.
                                                IntPtr lpvUser // Pointer to application-specific data. This pointer is made available again within the callback function.
                                                );

    // In order to evaluate the tag descriptions enumerated by the system, you must provide a callback function of the DM_ENUM_TYPEMEMBERS_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumTypeMembersProcEx([MarshalAs(UnmanagedType.LPStruct)]
                                                DM_VARKEY lpdmVarKey, // Pointer to the first structure of the DM_VARKEY type containing the keys (ID and name) of a tag.
                                                [MarshalAs(UnmanagedType.LPStruct)]
                                                MCP_NEWVARIABLE_DATA_EX lpdmVarDataEx, // Pointer to a structure of the MCP_NEWVARIABLE_DATA_EX type containing the description of a tag.
                                                IntPtr lpvUser // Pointer to application-specific data. This pointer is made available again within the callback function.
                                                );
    
    // In order to evaluate the tag descriptions enumerated by the system, you must provide a callback function of the DM_ENUM_TYPEMEMBERS_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumTypeMembersProcEx4([MarshalAs(UnmanagedType.LPStruct)]
                                                DM_VARKEY lpdmVarKey, // Pointer to the first structure of the DM_VARKEY type containing the keys (ID and name) of a tag.
                                                [MarshalAs(UnmanagedType.LPStruct)]
                                                MCP_NEWVARIABLE_DATA_EX4 lpdmVarDataEx, // Pointer to a structure of the MCP_NEWVARIABLE_DATA_EX4 type containing the description of a tag.
                                                IntPtr lpvUser // Pointer to application-specific data. This pointer is made available again within the callback function.
                                                );

    // In order to evaluate the tag types enumerated by the system, you must provide a callback function of the DM_ENUM_TYPES_PROC type.
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumTypesProc        ([MarshalAs(UnmanagedType.LPTStr)]    
                                                string lpszTypeName, // Pointer to the name of the tag belonging to a structured tag.
                                                UInt32 dwTypeID, // dwTypeID corresponds to the identifier assigned to the tag type by GAPICreateType.
                                                UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                IntPtr lpvUser // Pointer to application-specific data.
                                                );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate bool DMEnumConnectionProc   ([MarshalAs(UnmanagedType.LPStruct)]
                                                DM_CONNECTION_DATA lpdmConData, // Pointer to a structure of the DM_CONNECTION_DATA type which stores the data of a logical connection.
                                                IntPtr lpvUser // Pointer to application-specific data.
                                                );
    #endregion

    public class DMClient
    {
        // static event
        public static event DMNotifyEventHandler DMNotifyEv;

        // internal static members
        private static DmNotifyProc lpfnNotify_intern;
        private static bool bIsOwnConnected;

        public static string g_szProjectFile, g_szDSNName;

        /* InitConstructor 
         * set the notify event and clears the connect state
         * only one instance allowed
        */
        public DMClient(DMNotifyEventHandler notify)
        {
            bIsOwnConnected = false;
            lpfnNotify_intern = new DmNotifyProc(DMClient.DmNotifyProcIntern);
            DMNotifyEv += new DMNotifyEventHandler(notify);
        }

        protected static void OnDMNotify(DMNotifyEventArgs e)
        {
            if (null != DMNotifyEv)
            {
                DMNotifyEv(null, e);
            }
        }

        public static bool DmNotifyProcIntern(UInt32 dwNotifyClass,
                                        UInt32 dwNotifyCode,
                                        IntPtr lpbyData,
                                        UInt32 dwItems,
                                        IntPtr lpvUser)
        {
            bool bRet = false;

            if (bIsOwnConnected)
            {
                bRet = true;
                // do the special Notify handlings here like fire additional events or do other things
                DMNotifyEventArgs e = new DMNotifyEventArgs(dwNotifyClass, dwNotifyCode, lpbyData, dwItems, lpvUser);

                OnDMNotify(e);
            }

            return bRet;
        }
       
        #region Enumerate Functions 



        #endregion

        #region General API Functions

        /* Connects an application to the data manager. Only one DMConnect can be executed in a given application (process). Subsequent calls will return the DM_E_ALREADY_CONNECTED error.
         * Paramter(s)
         * - lpszAppName    ->  Pointer to the name of the calling application
         * - lpfnNotify     ->  Pointer to the notify function for administrative notify messages issued to the application by the data manager.
         * - lpvUser        ->  Pointer to application-specific data. The function does not evaluate this pointer, but makes it available again within the callback function.
         * - lpdmError      ->  Pointer to the data of the extended error message within the CMN_ERROR structure
         * Return:
         * - True   ->  Connection to data manager established
         * - False  ->  Error
        */
        [DllImport("dmclient.dll", EntryPoint = "DMConnectW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DMConnect([In] String lpszAppName,
                                             [In] DmNotifyProc lpfnNotify,
                                             [In] IntPtr lpvUser,
                                             [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                       CMN_ERROR lpdmError);

        /* Use this function to disconnect an application from the data manager.
         * Paramter(s)
         * - lpdmError      ->  Pointer to the data of the extended error message within the CMN_ERROR structure.
         * Return:
         * - True   ->  Connection interrupted
         * - False  ->  Error
        */
        [DllImport("dmclient.dll", EntryPoint = "DMDisConnectW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DMDisConnect([In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                          CMN_ERROR lpdmError);

        /* Use this function to find out if a connection to the data manager exists. This is e.g. recommended if you want to check if the DMConnect function call has been executed correctly.
         * Paramter(s)
         * - lpdmError          ->  Pointer to the data of the extended error message within the CMN_ERROR structure.
         * Return:
         * - True   ->  Connection exists
         * - False  ->  Error or connection does not exist.
        */
        [DllImport("dmclient.dll", EntryPoint = "DMGetConnectionStateW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMGetConnectionState([In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError);

        /* Use this function to exit WinCC.
         * Paramter(s)
         * Return:
        */
        [DllImport("dmclient.dll", EntryPoint = "DMExitWinCCW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMExitWinCC();

        /*Use this function to close WinCC. In addition, you can specify how you want the operating system to behave after the shutdown of WinCC.
         * Paramter(s)
         * - dwMode             ->  Shutdown mode.
         */
        [DllImport("dmclient.dll", EntryPoint = "DMExitWinCCExW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMExitWinCCEx([In] /*UInt32*/ DM_SDMODE_ENUM dwMode);

        #endregion

        #region Project Administration API Functions

        /* Opens the dialog box to create a new project. A project directory and subdirectories for each application are created for this purpose.
         * Paramter(s)
         * - hwndParent         ->  Handle to the window which is used as the parent window for the dialog.
         * - lpszProjectFile    ->  Pointer to the buffer which stores the name of the project file including path and extension; the buffer should have a size of at least _MAX_PATH characters.
         * - dwBufSize          ->  Size of the buffer passed in characters.
         * - lpdmError          ->  Pointer to the data of the extended error message within the CMN_ERROR structure.
         * Return:
         * - True   ->  Connection exists
         * - False  ->  Error or connection does not exist.
         */
        [DllImport("dmclient.dll", EntryPoint = "DMCreateNewProjectW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMCreateNewProject ([In] IntPtr hwndParent,
                                                     [In] string lpszProjectFile,
                                                     [In] UInt32 dwBufSize,
                                                     [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError);
        

        // Selects a project file using the standard selection dialog of the system. If the project has not yet been opened in the data manager, the information required for the project is loaded.
        [DllImport("dmclient.dll", EntryPoint = "DMOpenProjectW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DMOpenProject ([In] IntPtr hwndParent, // Handle to the window which is used as the parent window for the dialog.
                                                 [In] string lpszProjectFile,
                                                 [In] UInt32 dwBufSize,   
                                                 [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError);

        // Displays the Assign Parameters dialog box which contains the global project settings.
        [DllImport("dmclient.dll", EntryPoint = "DMEditProjectSettingsW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)] 
        private static extern bool DMEditProjectSettings ([In] IntPtr hwndParent, // Handle to the window which is used as the parent window for the dialog.
                                                         [In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                         [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure.

        // Calls the passed callback function for each project opened within the Control Center.
        [DllImport("dmclient.dll", EntryPoint = "DMEnumOpenedProjectsW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DMEnumOpenedProjects ([In, Out] UInt32 lpdwItems, // Pointer to a double word tag to store the number of open projects after the enumeration has been completed.
                                                        [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                             DMEnumOpenedProjectsProc lpfnEnum, // Pointer to your callback function which receives the project data.
                                                        [In] IntPtr lpvUser, // Pointer to application-specific data.
                                                         [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure.

        // Obtains information relevant to the project passed, such as specified path, data source name, computer configuration, etc.
        [DllImport("dmclient.dll", EntryPoint = "DMEnumOpenedProjectsW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern bool DMGetProjectInformation ([In] String lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                            [In, Out] DM_PROJECT_INFO lpProjectInfo, // Pointer to the DM_PROJECT_INFO structure to store the project information.
                                                            [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure.

        #endregion

        #region API Functions for editing tags

        // Obtains information on the objects linked with a tag, such as related tag group, channel unit, etc
        [DllImport("dmclient.dll", EntryPoint = "DMEnumVarDataW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMEnumVarData (   [In] /*[MarshalAs(UnmanagedType.LPWStr)]*/
                                                    string lpszProjectFile,
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    DM_VARKEY lpdmVarKey,
                                                    [In] UInt32 dwItems,
                                                    [In] /*[MarshalAs(UnmanagedType.FunctionPtr)]*/
                                                    DMEnumVariableProc lpfnEnum,
                                                    [In] IntPtr lpvUser,
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError
                                                    );

        // Obtains information on the objects linked with a tag, such as related tag group, channel unit, etc. It is distinguished from DMEnumVarData by its additional scaling data output.
        [DllImport("dmclient.dll", EntryPoint = "DMEnumVarData4W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMEnumVarData4 (  [In] string
                                                    lpszProjectFile,
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    DM_VARKEY lpdmVarKey,
                                                    [In] UInt32 dwItems,
                                                    [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                    DMEnumVariableProc4 lpfnEnum,
                                                    [In] IntPtr lpvUser,
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError
                                                    );

        [DllImport("dmclient.dll", EntryPoint = "DMEnumVariablesW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMEnumVariables([In] string lpszProjectFile,
                                                  [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    DM_VARFILTER lpdmVarFilter,
                                                  [In] 
                                                    DmEnumVarProc lpfnEnum,
                                                  IntPtr lpvUser,
                                                  [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError
                                                  );

        // Creates a new tag or verifies the existence of a tag.
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateNewVariableW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateNewVariable([In]  [MarshalAs(UnmanagedType.LPStruct)]
                                                                MCP_NEWVARIABLE_DATA pDtata, // Pointer to the MCP_NEWVARIABLE_DATA containing the tag data.
                                                            [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure. 
        
        // Creates a new tag or verifies the existence of a tag. This function is distinguished from GAPICreateNewVariable by the optional output of scaling data
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateNewVariable4W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateNewVariable4([In]  [MarshalAs(UnmanagedType.LPStruct)]
                                                                MCP_NEWVARIABLE_DATA_4 pDtata, // Pointer to the MCP_NEWVARIABLE_DATA containing the tag data.
                                                            [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure. 

        // Creates a new tag or verifies the existence of a tag. This function differs from GAPICreateNewVariable4 in two ways: the creator ID is specified and the start-up/default values can be specified for text tags also
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateNewVariable5W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateNewVariable5([In] UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                            [In]  [MarshalAs(UnmanagedType.LPStruct)]
                                                                MCP_NEWVARIABLE_DATA_5 pDtata, // Pointer to the MCP_NEWVARIABLE_DATA containing the tag data.
                                                            [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure. 
        
        // Creates a new tag or verifies the existence of a tag.
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateNewVariableEx4W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateNewVariableEx4([In] UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                            [In]  [MarshalAs(UnmanagedType.LPStruct)]
                                                                MCP_NEWVARIABLE_DATA_4 pDtata, // Pointer to the MCP_NEWVARIABLE_DATA containing the tag data.
                                                            [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure. 

        // Use this function to delete the tag specified by the MCP_DELETEVARIABLE_DATA.
        [DllImport("dmclient.dll", EntryPoint = "GAPIDeleteVariableW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPIDeleteVariable([In] [MarshalAs(UnmanagedType.LPStruct)]
                                                            MCP_DELETEVARIABLE_DATA pData, // Pointer to the MCP_DELETEVARIABLE_DATA structure containing the data of the tag you want to delete.
                                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                                 CMN_ERROR lpdmError); // Pointer to the data of the extended error message within the CMN_ERROR structure.

        #endregion
        
        #region API Functions for editing structured tags

        // Use this function to create a new type of structured tag.
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateTypeW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateType([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    DM_TYPE_DESCRIPTOR lpdmType, // Pointer to the DM_TYPE_DESCRIPTOR structure containing the description of the tag type.
                                                    [In] [MarshalAs(UnmanagedType.U4)]
                                                    MCP_NVAR_FLAG dwFlags, // Specifies how to edit the tag type
                                                    [In]
                                                    UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                    [In] [MarshalAs(UnmanagedType.LPArray)]
                                                    MCP_NEWVARIABLE_DATA_EX[] lpMemberdata, // Pointer to the first structure of the MCP_NEWVARIABLE_DATA_EX type containing the description of the tag type.
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                    );

        // Use this function to create a new type of structured tag.
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateTypeW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateTypeSS([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    DM_TYPE_DESCRIPTOR lpdmType, // Pointer to the DM_TYPE_DESCRIPTOR structure containing the description of the tag type.
                                                    [In] [MarshalAs(UnmanagedType.U4)]
                                                    MCP_NVAR_FLAG dwFlags, // Specifies how to edit the tag type
                                                    [In]
                                                    UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NEWVARIABLE_DATA_EX lpMemberdata, // Pointer to the first structure of the MCP_NEWVARIABLE_DATA_EX type containing the description of the tag type.
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                    ); 

        // Use this function to create a new type of structured tag. 
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateType4W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateType4([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    DM_TYPE_DESCRIPTOR lpdmType, // Pointer to the DM_TYPE_DESCRIPTOR structure containing the description of the tag type.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NVAR_FLAG dwFlags, // Specifies how to edit the tag type
                                                    [In] UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NEWVARIABLE_DATA_EX4 lpMemberdata, // Pointer to the first structure of the MCP_NEWVARIABLE_DATA_EX type containing the description of the tag type.
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                    ); 

        // Creates a new structured tag of a particular type created via GAPICreateType including the member data manipulated
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateTypInstanceW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateTypInstance([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NEWVARIABLE_DATA_EX lpdmTypeInstance, // Pointer to the data of the structured tag type within the MCP_NEWVARIABLE_DATA_EX structure.
                                                    [In] 
                                                    UInt32 dwNumMembers, // Number of tags. The number must be at least = 1. 
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NVAR_FLAG dwFlags, // Specifies how to edit the tag type
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NEWVARIABLE_DATA_EX lpMemberdata, // Pointer to the first structure of the MCP_NEWVARIABLE_DATA_EX type containing the description of the tag type.
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure
                                                    ); 

        // Creates a new structured tag of a particular type created via GAPICreateType4 including the member data manipulated.
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateTypInstance4W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPICreateTypInstance4([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NEWVARIABLE_DATA_EX4 lpdmTypeInstance, // Pointer to the data of the structured tag type within the MCP_NEWVARIABLE_DATA_EX structure.
                                                    [In] UInt32 dwNumMembers, // Number of tags. The number must be at least = 1. 
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NVAR_FLAG dwFlags, // Specifies how to edit the tag type
                                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                    MCP_NEWVARIABLE_DATA_EX4 lpMemberdata, // Pointer to the first structure of the MCP_NEWVARIABLE_DATA_EX type containing the description of the tag type.
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure
                                                    ); 

        // If you want to delete a structured tag type using this function, you must first delete all tags of that type by means of GAPIDeleteVariable.
        [DllImport("dmclient.dll", EntryPoint = "GAPIDeleteTypeW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPIDeleteType    ([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                    [In] [MarshalAs(UnmanagedType.U4)]
                                                    MCP_DVAR_FLAG dwFlags, // Specifies how to edit the tag type
                                                    [In] UInt32 dwCreatorID, // The creator ID can be used to enter the creator of an object.
                                                    [In] string lpszTypeName, // Pointer to the name of the tag type to be deleted
                                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                    CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                    ); 

        // This function returns the names of the tags belonging to a structured tag.
        [DllImport("dmclient.dll", EntryPoint = "GAPIEnumTypeMembersW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPIEnumTypeMembers([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                        [In] string lpszTypeName, // Name of the type of a structured tag whose tags you want to have enumerated.
                                                        [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                        DMEnumTypeMembersProc lpfnCallback, // Pointer to your callback function which is called for each tag.
                                                        IntPtr lpvUser, // Pointer to application-specific data.
                                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                        CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                        ); 

        // This function returns a description, including all defaults of the tags belonging to a structured tag; however no scaling data are returned.
        [DllImport("dmclient.dll", EntryPoint = "GAPIEnumTypeMembersExW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPIEnumTypeMembersEx ([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                        [In] string lpszTypeName, // Name of the type of a structured tag whose tags you want to have enumerated.
                                                        [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                        DMEnumTypeMembersProcEx lpfnCallback, // Pointer to your callback function which is called for each tag.
                                                        IntPtr lpvUser, // Pointer to application-specific data.
                                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                            CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                        ); 

        // This function returns a complete description, including all defaults of the tags belonging to a structured tag.
        [DllImport("dmclient.dll", EntryPoint = "GAPIEnumTypeMembersEx4W", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPIEnumTypeMembersEx4 ([In] string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                        [In] string lpszTypeName, // Name of the type of a structured tag whose tags you want to have enumerated.
                                                        [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                        DMEnumTypeMembersProcEx4 lpfnCallback, // Pointer to your callback function which is called for each tag.
                                                        IntPtr lpvUser, // Pointer to application-specific data.
                                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                        CMN_ERROR lpdmError // Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                        ); 

        // Use this function to enumerate the names and the ID numbers of the structured tag types configured.
        [DllImport("dmclient.dll", EntryPoint = "GAPIEnumTypesW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool GAPIEnumTypes       ([In] string lpszProjectFile,
                                                        [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                        DMEnumTypesProc lpfnCallback,
                                                        IntPtr lpvUser, // Pointer to application-specific data.
                                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                            CMN_ERROR lpdmError// Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                        );

        #endregion

        #region API Functions for editing connections

        // Obtain the data of alogical connection configured.
        [DllImport("dmclient.dll", EntryPoint = "DMEnumConnectionDataW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMEnumConnectionData (/*[In] [MarshalAs(UnmanagedType.LPTStr)]*/
                                                        string lpszProjectFile, // Pointer to the name of the project file, including path and extension.
                                                        [In] [MarshalAs(UnmanagedType.LPStruct)]
                                                        DM_CONNKEY lpdmConnKey, // Pointer to the first structure of the DM_CONNKEY type.
                                                        UInt32 dwItems, // Number of logical connections whose data you want to enumerate.
                                                        [In] [MarshalAs(UnmanagedType.FunctionPtr)]
                                                        DMEnumConnectionProc lpfnCallback, // Pointer to application-specific data.
                                                        IntPtr lpvUser, // Pointer to application-specific data. 
                                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                        CMN_ERROR lpdmError// Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                        ); 

        // Creates a new connection
        [DllImport("dmclient.dll", EntryPoint = "GAPICreateNewConnectionW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMGAPICreateNewConnection([In] [MarshalAs(UnmanagedType.LPStruct)]
                                                            MCP_NEWCONNECTION_DATA pData, // Containing the data of the logical connection to be created.
                                                         [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                            CMN_ERROR lpdmError// Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                         );

        // Delete an existing connection
        [DllImport("dmclient.dll", EntryPoint = "GAPIDeleteConnectionW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        public static extern bool DMGAPIDeleteConnection([In] [MarshalAs(UnmanagedType.LPStruct)]
                                                            MCP_DELETECONNECTION_DATA pData, // Containing the data of the logical connection to be deleted.
                                                         [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                            CMN_ERROR lpdmError// Pointer to the data of the extended error message within the CMN_ERROR structure.
                                                         );
        #endregion

        #region General Functions

        /* Establish connection to data manager. 
         * Paramter(s)
         * - lpszAppName    ->  Pointer to the name of the calling application
         * - lpdmError      ->  Pointer to the data of the extended error message within the CMN_ERROR structure
         * Return:
         * - True   ->  Connection to data manager established
         * - False  ->  Error
        */
        public bool Connect([In] String lpszAppName,
                           [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                      CMN_ERROR lpdmError)
        {
            bool bRet = false;

            if (!bIsOwnConnected)
            {
                bool bExtConnect = false;
                bExtConnect = DMGetConnectionState(lpdmError);
                if (!bExtConnect)
                {
                    bRet = DMConnect(lpszAppName,
                                     lpfnNotify_intern,
                                     IntPtr.Zero,
                                     lpdmError);
                    bIsOwnConnected = bRet;
                }
                else
                {
                    lpdmError.dwError1 = DM_ERROR_ENUM.DM_E_ALREADY_CONNECTED;
                    lpdmError.szErrorText = "is already connected from outside";
                    bRet = false;
                }
            }
            else
            {
                lpdmError.dwError1 = DM_ERROR_ENUM.DM_E_ALREADY_CONNECTED;
                lpdmError.szErrorText = "is already connected from DMClientWrapper";
                bRet = false;
            }
            ////WriteTextFile.Trace(bRet ? "DMConnect" : string.Format("Error in DMConnect: E1= {0:X} ; E2 = {1:X} ; {2}", lpdmError.dwError1, lpdmError.dwError2, lpdmError.szErrorText));
            return bRet;
        }

        // Retrieve connection to data manager
        public bool GetConnectionState()
        {
            CMN_ERROR err = new CMN_ERROR();
            string szAppName = "Kaizoku";
            string szText;
            bool bRet = DMGetConnectionState(err);
            if (!bRet) // not connected
            {
                err = new CMN_ERROR();
                bRet = DMConnect(szAppName, lpfnNotify_intern, IntPtr.Zero, err);
                if (!bRet)
                {
                    szText = string.Format("Error in DMConnect: E1= {0:X} ; E2= {1:X} ; {2}", err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = "DMConnect";
                }
                ////WriteTextFile.Trace(szText);
            }
            else // alredy connected
            {
                    szText = "DMGetConnectionState: OK";
                    //WriteTextFile.Trace(szText);
            }
            return bRet;
        }

        /* Terminate connection to data manager
         * Paramter(s)
         * - lpdmError      ->  Pointer to the data of the extended error message within the CMN_ERROR structure.
         * Return:
         * - True   ->  Connection interrupted
         * - False  ->  Error
        */
        public bool Disconnect([In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                         CMN_ERROR lpdmError)
        {
            bool bRet = false;

            if (bIsOwnConnected)
            {
                bRet = DMDisConnect(lpdmError);

                bIsOwnConnected = false;
            }
            else
            {
                lpdmError.dwError1 = DM_ERROR_ENUM.DM_E_NOT_CONNECTED;
                lpdmError.szErrorText = "not connected from DMClientWrapper";
                bRet = false;
            }
            //WriteTextFile.Trace(bRet ? "DMDisconnect" : string.Format("Error in DMDisconnect: E1= {0:X} ; E2 = {1:X} ; {2}", lpdmError.dwError1, lpdmError.dwError2, lpdmError.szErrorText));
            return bRet;
        }

        public bool Disconnect()
        {
            CMN_ERROR err = new CMN_ERROR();
            string szText;
            bool bRet =  DMDisConnect(err);
            if (!bRet)
	        {
                szText = string.Format("Error in DMConnect: E1={0:X} ; E2= {1:X} ; {2}", err.dwError1, err.dwError2, err.szErrorText);
	        }
            else
            {
                szText = string.Format("DMDisconnect");
            }
            //WriteTextFile.Trace(szText);
            return bRet;
        }
        #endregion

        #region Project Administration Functions

        // Open project via dialog
        public bool OpenProject(string szProjFile,
                                [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                        CMN_ERROR lpdmError)
        {
            bool bRet = false;

            string szText = "";
            CMN_ERROR err = new CMN_ERROR();

            if (!bIsOwnConnected)
            {
                lpdmError.dwError1 = DM_ERROR_ENUM.DM_E_NOT_CONNECTED;
                lpdmError.szErrorText = "not connected from DMClientWrapper";
                bRet = false;
            }
            else
            {
                bRet = DMOpenProject(IntPtr.Zero, szProjFile,255, err);
                if (!bRet)
                {
                    szText = string.Format("Error in DMOpenProject: E1= {0:X} ; E2= {1:X} ;{2}", err.dwError1, err.dwError2, err.szErrorText);
                }
                {
                    szText = string.Format("OpenProject ( {0} )", szProjFile);
                }
            }
            //WriteTextFile.Trace(szText);
            return bRet;
        }

        // Open Assign Parameters dialog box for project
        public bool EditProjectSettings(string szProjFile,
                                        [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                        CMN_ERROR lpdmError)
        {
            bool bRet = false;
            string szText = "";
            CMN_ERROR err = new CMN_ERROR();
            if (!bIsOwnConnected)
            {
                lpdmError.dwError1 = DM_ERROR_ENUM.DM_E_NOT_CONNECTED;
                lpdmError.szErrorText = "not connected from DMClientWrapper";
                bRet = false;
            }
            else
            {
                bRet = DMEditProjectSettings(IntPtr.Zero, szProjFile, err);
                if (!bRet)
                {
                    szText = string.Format("Error in DMEditProjectSettings: E1= {0:X} ; E2= {1:X} ;{2}", err.dwError1, err.dwError2, err.szErrorText);
                }
                {
                    szText = string.Format("DMEditProjectSettings ({0})", szProjFile);
                }
            }

            return bRet;
        }

        // Enumerate (opened) projects
        public bool EnumOpenedProjects()
        {            
            CMN_ERROR err = new CMN_ERROR();
            UInt32 dwItems = UInt32.MinValue;
            string szText = "";
            bool bRet = GetConnectionState();
            if (bRet)
            {
                bRet = DMEnumOpenedProjects(dwItems, EnumOpenedProjectsCallback, IntPtr.Zero, err);
                if (!bRet)
                {
                    szText = string.Format("Error in DMEnumOpenedProjects: E1= {0:X} ; E2= {1:X} ; {2}", err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = "DMEnumOpenedProjects";
                }
                //WriteTextFile.Trace(szText);
            }
            return bRet;
        }


        private bool EnumOpenedProjectsCallback(DM_PROJECT_INFO lpInfo, IntPtr lpvUser)
        {
            //WriteTextFile.Trace("**DMEnumOpenProjects**");
            g_szProjectFile = lpInfo.szProjectFile;
            g_szDSNName = lpInfo.szDSNName;
            //WriteTextFile.Trace(string.Format("ProjectFile = {0} ", lpInfo.szProjectFile));
            //WriteTextFile.Trace(string.Format("DSNName  = {0} ", lpInfo.szDSNName));
            //WriteTextFile.Trace(string.Format("DataLocale   = {0} ", lpInfo.dwDataLocale));
            //WriteTextFile.Trace("**********************");
            return true;
        }

        // Enumerate (opened) projects
        public bool EnumOpenedProjects([In, Out] UInt32 dwItems,
                               [In/*, Out*/] [MarshalAs(UnmanagedType.FunctionPtr)]
                                            DMEnumOpenedProjectsProc DMEnumOpenedProjectsCallback,
                               [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                        CMN_ERROR err)
        {
            string szText = "";
            err = new CMN_ERROR();
            bool bRet = GetConnectionState();
            if (!bRet)
            {
                bRet = DMEnumOpenedProjects(dwItems, DMEnumOpenedProjectsCallback, IntPtr.Zero, err);
                szText = string.Format("Error in DMEnumOpenedProjects: E1= {0:X} ; E2= {1:X}; {2}", err.dwError1, err.dwError2, err.szErrorText);
            }
            else
                szText = "DMEnumOpenedProjects";
            {
            }
            //WriteTextFile.Trace(szText);
            return bRet;
        }

        // Retrieve project information
        public bool GetProjectInformation([In, Out] DM_PROJECT_INFO lpInfo,
                                            [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                        CMN_ERROR err)
        {
            string szText = "";
            string szProjectFile;
            err = new CMN_ERROR();
            lpInfo = new DM_PROJECT_INFO();
            bool bRet = GetConnectionState();
            if (bRet)
            {
                EnumOpenedProjects(); // open the DM and set the g_szProjectFile
                szProjectFile = g_szProjectFile;
                bRet = DMGetProjectInformation(szProjectFile, lpInfo, err);
                if (!bRet)
                {
                    szText = string.Format("Error in DMGetProjectInformation: E1= {0:X} ; E2= {1:X} ; {2}", err.dwError1, err.dwError2, err.szErrorText);    
                }
                else
                {
                    //WriteTextFile.Trace("**DMGetProjectInformation**");
                    //WriteTextFile.Trace(string.Format("ProjectFile = {0} ", lpInfo.szProjectFile));
                    //WriteTextFile.Trace(string.Format("DSNName  = {0} ", lpInfo.szDSNName));
                    //WriteTextFile.Trace(string.Format("DataLocale   = {0} ", lpInfo.dwDataLocale));
                    //WriteTextFile.Trace("**********************");
                }
            }
            return bRet;
        }

        #endregion

        #region Functions for editing tags

        // Enumerate tag information
        public bool EnumVarData(DM_VARKEY lpdmVarKey, CMN_ERROR lpdmError)
        {
            lpdmError = new CMN_ERROR();
            bool bRet = EnumOpenedProjects();
            string szText;
            if (bRet)
            {
                /*ERRORS
                bRet = DMEnumVarData(g_szProjectFile, lpdmVarKey, 1, EnumVarDataCallback, IntPtr.Zero, lpdmError);*/
                if (!bRet)
                {
                    szText = string.Format("Error in GAPICreateNewVariable: E1= {0:X}; E2= {1:X} ; {2}", lpdmError.dwError1, lpdmError.dwError2, lpdmError.szErrorText);
                }
                else
                {
                    szText = "EnumVarData";
                }
                //WriteTextFile.Trace(szText);
            }
            return false;
        }

        public bool EnumVarDataCallback(DM_VARKEY lpdmVarKey, DM_VARIABLE_DATA lpdmVarData, IntPtr lpvUser)
        {
            //WriteTextFile.Trace("DMEnumVariableCallback");
            //WriteTextFile.Trace(string.Format("...szName={0} dmType={1}",lpdmVarKey.szName, lpdmVarData.dmTypeRef.dwType));
            //WriteTextFile.Trace(string.Format("\r\n"));
            return true;
        }

        // 
        public bool EnumVariables(DM_VARFILTER lpdmVarFilter, CMN_ERROR lpdmError)
        {
            lpdmError = new CMN_ERROR();
            bool bRet = EnumOpenedProjects();
            if (bRet)
	        {
                bRet = DMEnumVariables(g_szProjectFile, lpdmVarFilter, EnumVariablesCallback, IntPtr.Zero, lpdmError);
	        }
            return bRet;
        }
        public bool EnumVariablesCallback(DM_VARKEY lpdmVarKey, IntPtr lpvUser)
        {
            //WriteTextFile.Trace("EnumVariablesCallback");
            //WriteTextFile.Trace(string.Format("...szName={0}", lpdmVarKey.szName));
            return true;
        }

        // Create tag
        public bool CreateNewVariable([In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                        MCP_NEWVARIABLE_DATA NVarData,
                                          [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                     CMN_ERROR lpError)
        {
            lpError = new CMN_ERROR();
            string szText;
            bool bRet = EnumOpenedProjects(); // open the DM and set the g_szProjectFile
            if (bRet)
            {                
                NVarData.szProjectFile = g_szProjectFile/*@"C:\Users\Public\Documents\Siemens\WinCCProjects\test\test.mcp"*/;
                bRet = GAPICreateNewVariable(NVarData, lpError);
                if (!bRet)
                {
                    szText = string.Format("Error in CreateNewVariable: E1= {0:X}; E2= {1:X} ; {2}", lpError.dwError1, lpError.dwError2, lpError.szErrorText);
                }
                else
                {
                    szText = "CreateNewVariable";
                }
                //WriteTextFile.Trace(szText);
            }

            return bRet;
        }

        public bool CreateNewVariable4  ([In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                MCP_NEWVARIABLE_DATA_4 lpData,
                                                [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                     CMN_ERROR lpdmError)
        {
            lpdmError = new CMN_ERROR();
            string szText;
            bool bRet = EnumOpenedProjects(); // open the DM and set the g_szProjectFile
            if(bRet)
            {                
                lpData.szProjectFile = g_szProjectFile;
                bRet = GAPICreateNewVariable4(lpData, lpdmError);
                if (!bRet)
                {
                    szText = string.Format("Error in CreateNewVariable4: E1= {0:X}; E2= {1:X} ; {2}", lpdmError.dwError1, lpdmError.dwError2, lpdmError.szErrorText);
                }
                else
                {
                    szText = "CreateNewVariable4";
                }
                //WriteTextFile.Trace(szText);                
            }
            return false;
        }
        #endregion
        
        #region Fuction for editig structured tags

        public bool EnumTypes()
        {
            CMN_ERROR err= new CMN_ERROR();
            bool bRet = false;
            string szText;
            bRet = GetConnectionState();
            if (bRet)
            {
                EnumOpenedProjects();
                bRet = GAPIEnumTypes(g_szProjectFile, EnumTypeCallback, IntPtr.Zero, err);
                if (bRet)
                {
                    szText = string.Format("Error in EnumTypes: E1= {0} ; E2= {1} ; {2}", err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = string.Format("EnumTypes");
                }
            }
            return bRet;
        }

        private bool EnumTypeCallback(string szProjectName,
                                UInt32 dwTypeID,
                                UInt32 dwCreatorID,
                                IntPtr lpvUser
                                )
        {
            //WriteTextFile.Trace("**EnumTypeCallback**");
            //WriteTextFile.Trace(string.Format("TypeName = {0} TypeID = {1:X} CreatorID = {2}", szProjectName, dwTypeID, dwCreatorID));
            //WriteTextFile.Trace("**********************");
            return true;
        }
                                    

        // Create type of structured tag
        public bool CreateType      ([In] [MarshalAs(UnmanagedType.LPStruct)]
                                    DM_TYPE_DESCRIPTOR lpdmType,
                                    [In] [MarshalAs(UnmanagedType.U4)]
                                    MCP_NVAR_FLAG dwFlags,
                                    [In] [MarshalAs(UnmanagedType.LPStruct)]
                                    MCP_NEWVARIABLE_DATA_EX[] lpMemberdata,
                                    [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                     CMN_ERROR err
                                    )
        {
            bool bRet = EnumOpenedProjects(); // open the DM and set the g_szProjectFile
            string szText;
            if(bRet)
            {                
                bRet = GAPICreateType(g_szProjectFile,
                                        lpdmType,
                                        dwFlags,
                                        0,
                                        lpMemberdata,
                                        err);
                if (!bRet)
	            {
		            szText = string.Format("Error in CreateType: E1= {0:X} ; E2= {1:X} ; {2}",
                                            err.dwError1,err.dwError2,err.szErrorText);
	            }
                else
	            {
                    szText = string.Format("CreateType: {0}",bRet);
	            }
                //WriteTextFile.Trace(szText);
            }

            return bRet;
        }


        public bool CreateTypeSS([In] [MarshalAs(UnmanagedType.LPStruct)]
                                    DM_TYPE_DESCRIPTOR lpdmType,
                      [In] [MarshalAs(UnmanagedType.U4)]
                                    MCP_NVAR_FLAG dwFlags,
                      [In] [MarshalAs(UnmanagedType.LPStruct)]
                                    MCP_NEWVARIABLE_DATA_EX lpMemberdata,
                      [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                                     CMN_ERROR err
                      )
        {
            bool bRet = EnumOpenedProjects(); // open the DM and set the g_szProjectFile
            string szText;
            if (bRet)
            {
                lpMemberdata.szProjectFile = g_szProjectFile;
                bRet = GAPICreateTypeSS(g_szProjectFile,
                                        lpdmType,
                                        dwFlags,
                                        0,
                                        lpMemberdata,
                                        err);
                if (!bRet)
                {
                    szText = string.Format("Error in CreateType: E1= {0:X} ; E2= {1:X} ; {2}",
                                            err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = string.Format("CreateType: {0}", bRet);
                }
                //WriteTextFile.Trace(szText);
            }

            return bRet;
        }

        // Delete type of structured tag
        public bool DeleteType([In] [MarshalAs(UnmanagedType.U4)]
                                MCP_DVAR_FLAG dwFlags,
                              [In] string lpszTypeName,
                              [In, Out] [MarshalAs(UnmanagedType.LPStruct)]
                                CMN_ERROR lpdmError)
        {
            lpdmError = new CMN_ERROR();
            string szText;
            bool bRet = EnumOpenedProjects();
            if (bRet)
            {
                bRet = GAPIDeleteType(g_szProjectFile, dwFlags, 0, lpszTypeName, lpdmError);
                if (!bRet)
                {
                    szText = string.Format("Error in DeleteType: E1= {0:X} ; E2= {1:X} ; {2}",
                        lpdmError.dwError1, lpdmError.dwError2, lpdmError.szErrorText);
                }
                else
                {
                    szText = "DeleteType";
                }
                //WriteTextFile.Trace(szText);
            }
            return bRet;
        }

        #endregion

        #region Fucitons for editing conections

        // Enumerate connection data
        public bool EnumConenctionData()
        {
            CMN_ERROR err = new CMN_ERROR();
            DM_CONNKEY ConnKey = new DM_CONNKEY();
            UInt32 dwItems = UInt32.MinValue;
            string szText;
            bool bRet = GetConnectionState();
            if(bRet)
            {
                EnumOpenedProjects(); // open the DM and set the g_szProjectFile
                bRet = DMEnumConnectionData(g_szProjectFile,
                                            ConnKey,
                                            dwItems,
                                            EnumConnectionCallback,
                                            IntPtr.Zero,
                                            err
                                            );

                if (!bRet)
                {
                    szText = string.Format("Error in DMEnumConnectionData: E1= {0:X} ; E2= {1:X} ; {2}",
                                                err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = string.Format("DMEnumConnectionData");
                }
                //WriteTextFile.Trace(szText);
            }
            return bRet;
        }

        // Enumerate connection data (callback)
        private bool EnumConnectionCallback(DM_CONNECTION_DATA dmConData,
                                                IntPtr vUser
                                                )
        {
            //WriteTextFile.Trace(string.Format("DMEnumConnectionCallback"));
            //WriteTextFile.Trace(string.Format("...Connection = {0}", dmConData.szConnection));
            //WriteTextFile.Trace(string.Format("...UnitName = {0}", dmConData.szUnitName));
            //WriteTextFile.Trace(string.Format("...Common = {0}", dmConData.szCommon));
            //WriteTextFile.Trace(string.Format("...Specific = {0}", dmConData.szSpecific));
            //WriteTextFile.Trace(string.Format("...VarNum = {0}", dmConData.dwVarNum));
            return true;
        }

        // Create conenction
        public bool GAPICreateConnection    (MCP_NEWCONNECTION_DATA pData,
                                            CMN_ERROR err                                
            )
        {
            err = new CMN_ERROR();
            bool bRet = false;
            string szText;
            bRet = GetConnectionState();
            if (bRet)
	        {
	            EnumOpenedProjects();
                pData.szProjectFile = g_szProjectFile;
                bRet = DMGAPICreateNewConnection(pData, err);
                if (!bRet)
                {
                    szText = string.Format("Error in GAPICreateNewVariable: E1= {0:X} ; E2= {1:X} ; {2}",
                        err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = "GAPICreateConnection";
                }
                //WriteTextFile.Trace(szText);
	        }
            return bRet;
        }
        // Delete connection
        public bool GAPIDeleteConnection    (MCP_DELETECONNECTION_DATA pData,
                                            CMN_ERROR err
            )
        {
            string szText;
            err = new CMN_ERROR();
            bool bRet = GetConnectionState();
            if (bRet)
            {
                EnumOpenedProjects();
                pData.szProjectFile = g_szProjectFile;
                bRet = DMGAPIDeleteConnection(pData, err);
                if (!bRet)
                {
                    szText = string.Format("Error in DeleteConnection: E1= {0:X} ; E2= {1:X} ; {2}",
                                    err.dwError1, err.dwError2, err.szErrorText);
                }
                else
                {
                    szText = "DeleteConnection";
                }
                //WriteTextFile.Trace(szText);
            }
            return bRet;
        }
        #endregion
    }
}
