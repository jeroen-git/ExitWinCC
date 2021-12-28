using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.InteropServices;

namespace DMDEFS
{

    //Definition of the constants for the data manager

        // string length and other constants
        public class DM_LENGTH_DEFINES
        {
            public const int
                MAX_DM_OHIO_MACHINES = 64,//Max. number of OHIO computers in a project
                MAX_DM_OHIO_APPLICATIONS = 32,//Max. number of local client applications 
                MAX_DM_UPDATE_CYCLES = 15,//Max. number of update cycles
                MAX_DM_SYSTEM_CYCLES = 10,//including 10 system cycles
                MAX_DM_USER_CYCLES = 5,//and 5 user cycles

                MAX_DM_VAR_NAME = 128,//Max. length of a tag name
                MAX_DM_TYPE_NAME = 128,//Max. length of a data type name
                MAX_DM_GROUP_NAME = 64,//Max. length of a tag group name
                MAX_DM_CYCLE_NAME = 64,//Max. length of a cycle time text
                MAX_DM_FORMAT_NAME = 64,//Max. length of a format instruction
                MAX_DM_SCALE_NAME = 64,//Max. length of a scaling parameter description
                MAX_DM_SCALE_PARAM_NAME = 64,//Max. length of a scaling parameter description
                MAX_DM_MEMBER_NAME = 128,//Max. length of composite type members
                MAX_DM_INFOTEXT_LEN = 255,//Max. length of an information text
                MAX_DM_SHIFT_NAME_LEN = 32,//Max. length of a shift name
                MAX_DM_SHIFTS = 16,//Max. number of shifts per day
                MAX_DM_SHIFT_HOLYDAYS = 30,//Max. number of holidays in the shift schedule
                MAX_DM_SHIFT_HOLYNAME = 64,//Max. length of a holiday name
                MAX_DM_SERVICE_NAME = 32,//Max. length of a service name
                MAX_DM_APP_NAME = 32,//Max. length of a log. application name
                MAX_DM_DSN_NAME = 32,//Max. length of the data source name of the database
                MAX_DM_UNIT_NAME = 65,//Max. length of a unit
                MAX_DM_CONNECTION_NAME = 32,//Max. length of a connection
                MAX_DM_VAR_SPECIFIC = 256,//Maximum length of the specific part of a tag in the GAPI
                MAX_DM_CON_SPECIFIC = 128,//Max. length of the specific part of a connection in the GAPI
                MAX_DM_CON_COMMON = 128,//Max. length of the specific part of a connection in the GAPI
                MAX_DM_VARTYPE_TEXT_LEN = 255,//Max. length of a text tag 

                MAX_COMPUTERNAME_LENGTH = 15, //Max. lenght of computer name

                _MAX_PATH    = 260;
        };

        // dmerror defines
        // The following error messages can be returned by the API functions in the CMN_ERROR error structure
        public enum DM_ERROR_ENUM
        {
            DM_E_OK = 0x00000000,//Without error
            DM_E_CANCEL = 0x00000001,//User has selected "Cancel" in a dialog box
            DM_E_FILE = 0x00000002,//File operation error
            DM_E_UPDATE = 0x00000003,//Project is currently updated
            DM_E_NO_RT_PRJ = 0x00000004,//No project in runtime mode
            DM_E_NOT_SUPPORTED = 0x00000005,//Requested service not available
            DM_E_ALREADY_CONNECTED = 0x00000006,//Connection to data manager already exists
            DM_E_NOT_CONNECTED = 0x00000007,//No connection to data manager
            DM_E_INVALID_TAID = 0x00000008,//Invalid transaction ID
            DM_E_INVALID_KEY = 0x00000009,//Tag not found
            DM_E_INVALID_TYPE = 0x0000000A,//Invalid tag type
            DM_E_MAX_LIMIT = 0x0000000B,//High limit of tag violated
            DM_E_MIN_LIMIT = 0x0000000C,//Low limit of tag violated
            DM_E_MAX_RANGE = 0x0000000D,//High limit of format conversion violated
            DM_E_MIN_RANGE = 0x0000000E,//Low limit of format conversion violated
            DM_E_ACCESS_FAULT = 0x0000000F,//Unauthorized write access to tag
            DM_E_TIMEOUT = 0x00000010,//Timeout error
            DM_E_ALREADY_EXIST = 0x00000011,//Object to be created already exists
            DM_E_PARAM = 0x00000012,//Invalid parameter
            DM_E_INV_PRJ = 0x00000013,//Specified project not found / loaded
            DM_E_UNKNOWN = 0x00000014,//Unknown error
            DM_E_OOM = 0x00000015,//Out of memory
            DM_E_NOT_CREATED = 0x00000016,//Project could not be created
            DM_E_MACHINE_NOT_FOUND = 0x00000017,//Computer not found
            DM_E_NO_INFO_FOUND = 0x00000018,//No start information found
            DM_E_INTERNAL = 0x00000019,//Internal processing error
            DM_E_INVALID_LOCALE = 0x0000001A,//Incorrect local ID
            DM_E_COMMUNICATION = 0x0000001B,//Incorrect local ID
            DM_E_DONT_EXIST = 0x0000001C,//Object does not exist e.g.: an attempt was made to create a tag while the specified connection did not exist
            DM_E_ALREADY_ACTIVATED = 0x0000001D,//Project is in runtime mode, e.g.: an attempt was made to activate a project that was already in runtime mode.
            DM_E_NO_OPEN_PROJECT = 0x0000001E,//No open project in the Control Center
            DM_E_ALREADY_DEACTIVATED = 0x0000001F,//Project is not in runtime mode
            DM_E_NO_RIGHTS = 0x00000020,//Incorrect CreatorID passed
            DM_E_NOT_DELETED = 0x00000021,//Object could not be deleted
            DM_E_LICENSE = 0x00000022,//Software protection: no license
            DM_E_LICENSE_LIMIT = 0x00000023,//Software protection: limit reached/exceeded
            DM_E_INVALID_OBJECTTYPE = 0x00000024,//Invalid object type
            DM_E_OP_REQUIERES_PRJEDITMODE = 0x00000025,//Project is in runtime mode, e.g.: the operation requires that the project is inactive.
            DM_E_INTERFACE = 0x00000026,//Internal error when accessing interfaces
            DM_E_UNIT_NOT_FOUND = 0x00000027,//Unit not found
            DM_E_CONNECTION_NOT_FOUND = 0x00000028,//Connection not found

            //Multiclient project default server interconnection
            DM_E_NODEFAULTSERVER = 0x00000101,//No default server configured (if "@default::" was entered as server prefix)
            DM_E_NOLOCALSERVER = 0x00000102,//No local server available (if "@local::" was entered as server prefix)
            DM_E_NOSERVER = 0x00000103,//No default server configured and no local server available
            DM_E_NOMC = 0x00000104,//No multi-client project (this error message is not used by the Control Center)
            DM_E_NOMCDEFAULTSERVER = 0x00000105,//No multi-client project, but "@default::" was entered as server prefix

            DM_E_SYS_ERROR = 0x10000000//If this bit is set in dwError1 of the CMN_ERROR error structure, dwError2 contains the system error code.
        };

        /*Notify message classes
        */

        // notify class defines
        public enum DM_NOTIFYCLASS_ENUM
        {
            NO_NOTIFY_CLASS = 0,
            DM_NOTIFY_CLASS_ERROR = 0x00000001,//Notify code contains error IDDM_NOTIFY_SHUTDOWN  
            DM_NOTIFY_CLASS_WARNING = 0x00000002,//Notify code contains warning
            DM_NOTIFY_CLASS_DATA = 0x00000003//Notify code contains data type
        };

        /*Notify codes
        */

        // notify code error defines
        public enum DM_NOTIFYCODE_ERROR_ENUM
        {
            NO_NOTIFY_CODE = 0,
            DM_NOTIFY_SHUTDOWN = 0x00000001,//Error class: Data manager is shut down
            DM_NOTIFY_PROCESSNET_ERROR = 0x00000002,//Error class: Error on process bus
            DM_NOTIFY_SYSNET_ERROR = 0x00000003//Error class: Error on system bus 
        };

        // notify code warning defines
        public enum DM_NOTIFYCODE_WARNING_ENUM
        {
            NO_NOTIFY_CODE = 0,
            DM_NOTIFY_QUEUE_50_PERCENT = 0x00000001,//Warning class: Application queue has 50% fill level
            DM_NOTIFY_QUEUE_60_PERCENT = 0x00000002,//Warning class: Application queue has 60% fill level
            DM_NOTIFY_QUEUE_70_PERCENT = 0x00000003,//Warning class: Application queue has 70% fill level
            DM_NOTIFY_QUEUE_80_PERCENT = 0x00000004,//Warning class: Application queue has 80% fill level
            DM_NOTIFY_QUEUE_90_PERCENT = 0x00000005,//Warning class: Application queue has 90% fill level
            DM_NOTIFY_QUEUE_OVERFLOW = 0x00000006,//Warning class: Application queue overflow

            DM_NOTIFY_CYCLES_CHANGED = 0x00000010,//Warning class => reread cycles
            DM_NOTIFY_MACHINES_CHANGED = 0x00000011,//Warning class, computer list
            DM_NOTIFY_PROJECT_OPENED = 0x00000012,//Warning class, project is loaded
            DM_NOTIFY_PROJECT_CLOSED = 0x00000013,//Warning class, project is closed
            DM_NOTIFY_SYSTEM_LOCALE = 0x00000014,//Set language (resources)
            DM_NOTIFY_DATA_LOCALE = 0x00000015,//Set language (configuration data)
            DM_NOTIFY_PROJECT_RUNTIME = 0x00000016,//Project activated in runtime mode
            DM_NOTIFY_PROJECT_EDIT = 0x00000017,//Runtime mode for project deactivated

            DM_NOTIFY_HOTKEY_CHANGE = 0x00000018,//Hotkeys have changed

            DM_NOTIFY_URSEL = 0x00000019,//
            DM_NOTIFY_RT_LIC = 0x00000020,//
            DM_NOTIFY_CS_LIC = 0x00000021,//

            DM_NOTIFY_SERVERDOWN = 0x00000022,//
            DM_NOTIFY_SERVERUP = 0x00000023,//

            DM_NOTIFY_BODO = 0x00000050,//
            DM_NOTIFY_BEGIN_PROJECT_EDIT = 0x00000051//
        };

        // notify code data defines
        public enum DM_NOTIFYCODE_DATA_ENUM
        {
            NO_NOTIFY_CODE = 0,
            DM_NOTIFY_APPLICATION_DATA = 0x00000001,//Data class: Data sent by application
            DM_NOTIFY_VARIABLE_DATA = 0x00000002//Data class: Tag data
        };

        // varkey type can be ored
        [FlagsAttribute]
        public enum DM_VARKEY_TYPE
        {
            NO_VARKEY = 0,
            DM_VARKEY_ID = 1,
            DM_VARKEY_NAME = 2
        };
        
         /* Tags
    */ 

        // Tag types
        public enum DM_VARTYPE
        {
            DM_VARTYPE_BIT = 1, // Binary tag                              
            DM_VARTYPE_SBYTE = 2,                                              
            DM_VARTYPE_BYTE = 3,                                        
            DM_VARTYPE_SWORD = 4,                                               
            DM_VARTYPE_WORD = 5,                                         
            DM_VARTYPE_SDWORD = 6,                                               
            DM_VARTYPE_DWORD = 7,                                          
            DM_VARTYPE_FLOAT = 8,                                             
            DM_VARTYPE_DOUBLE = 9,                                             
            DM_VARTYPE_TEXT_8 = 10,                                            
            DM_VARTYPE_TEXT_16 = 11,                                             
            DM_VARTYPE_RAW = 12,                         
            DM_VARTYPE_ARRAY = 13,                          
            DM_VARTYPE_STRUCT = 14,                             
            DM_VARTYPE_BITFIELD_8 = 15,                                   
            DM_VARTYPE_BITFIELD_16 = 16,                                    
            DM_VARTYPE_BITFIELD_32 = 17,                                    
            DM_VARTYPE_TEXTREF = 18
        }

        // Conversion routines for data type "signed 8-bit value" 
        public enum CONVERSION_S8Bit
        {
            CharToSignedByte = 0,
            CharToUnsignedByte = 1,
            CharToUnsignedWord = 2,
            CharToUnsignedDword = 3,
            CharToSignedWord = 4,
            CharToSignedDword = 5,
            CharToMSBByte = 6,
            CharToMSBWord = 7,
            CharToMSBDword = 8,
            CharToBCDByte = 9,
            CharToBCDWord = 10,
            CharToBCDDword = 11,
            CharToSignedBCDByte = 12,
            CharToSignedBCDWord = 13,
            CharToSignedBCDDword = 14,
            CharToExtSignedBCDByte = 15,
            CharToExtSignedBCDWord = 16,
            CharToExtSignedBCDDword = 17,
            CharToAikenByte = 18,
            CharToAikenWord = 19,
            CharToAikenDword = 20,
            CharToSignedAikenByte = 21,
            CharToSignedAikenWord = 22,
            CharToSignedAikenDword = 23,
            CharToExcessByte = 24,
            CharToExcessWord = 25,
            CharToExcessDword = 26,
            CharToSignedExcessByte = 27,
            CharToSignedExcessWord = 28,
            CharToSignedExcessDword = 29
        }

        // Conversion routines for data type "unsigned 8-bit value"
        public enum CONVERSION_U8Bit
        {
            ByteToUnsignedByte = 0,
            ByteToUnsignedWord = 1,
            ByteToUnsignedDword = 2,
            ByteToSignedByte = 3,
            ByteToSignedWord = 4,
            ByteToSignedDword = 5,
            ByteToBCDByte = 6,
            ByteToBCDWord = 7,
            ByteToBCDDword = 8,
            ByteToAikenByte = 9,
            ByteToAikenWord = 10,
            ByteToAikenDword = 11,
            ByteToExcessByte = 12,
            ByteToExcessWord = 13,
            ByteToExcessDword = 14
        }

        // Conversion routines for data type "signed 16-bit value"
        public enum CONVERSION_S16Bit
        {
            ShortToSignedWord = 0,
            ShortToUnsignedByte = 1,
            ShortToUnsignedWord = 2,
            ShortToUnsignedDword = 3,
            ShortToSignedByte = 4,
            ShortToSignedDword = 5,
            ShortToMSBByte = 6,
            ShortToMSBWord = 7,
            ShortToMSBDword = 8,
            ShortToBCDByte = 9,
            ShortToBCDWord = 10,
            ShortToBCDDword = 11,
            ShortToSignedBCDByte = 12,
            ShortToSignedBCDWord = 13,
            ShortToSignedBCDDword = 14,
            ShortToExtSignedBCDByte = 15,
            ShortToExtSignedBCDWord = 16,
            ShortToExtSignedBCDDword = 17,
            ShortToAikenByte = 18,
            ShortToAikenWord = 19,
            ShortToAikenDword = 20,
            ShortToSignedAikenByte = 21,
            ShortToSignedAikenWord = 22,
            ShortToSignedAikenDword = 23,
            ShortToExcessByte = 24,
            ShortToExcessWord = 25,
            ShortToExcessDword = 26,
            ShortToSignedExcessByte = 27,
            ShortToSignedExcessWord = 28,
            ShortToSignedExcessDword = 29
        }

        // Conversion routines for data type "unsigned 16-bit value"
        public enum CONVERSION_U16Bit
        {
            WordToUnsignedWord = 0,
            WordToUnsignedByte = 1,
            WordToUnsignedDword = 2,
            WordToSignedByte = 3,
            WordToSignedWord = 4,
            WordToSignedDword = 5,
            WordToBCDByte = 6,
            WordToBCDWord = 7,
            WordToBCDDword = 8,
            WordToAikenByte = 9,
            WordToAikenWord = 10,
            WordToAikenDword = 11,
            WordToExcessByte = 12,
            WordToExcessWord = 13,
            WordToExcessDword = 14,
            WordToSimaticBCDCounter = 15,
            WordToSimaticCounter = 16
        }

        // Conversion routines for data type "signed 32-bit value"
        public enum CONVERSION_S32Bit
        {
            LongToSignedDword = 0,
            LongToUnsignedByte = 1,
            LongToUnsignedWord = 2,
            LongToUnsignedDword = 3,
            LongToSignedByte = 4,
            LongToSignedWord = 5,
            LongToMSBByte = 6,
            LongToMSBWord = 7,
            LongToMSBDword = 8,
            LongToBCDByte = 9,
            LongToBCDWord = 10,
            LongToBCDDword = 11,
            LongToSignedBCDByte = 12,
            LongToSignedBCDWord = 13,
            LongToSignedBCDDword = 14,
            LongToExtSignedBCDByte = 15,
            LongToExtSignedBCDWord = 16,
            LongToExtSignedBCDDword = 17,
            LongToAikenByte = 18,
            LongToAikenWord = 19,
            LongToAikenDword = 20,
            LongToSignedAikenByte = 21,
            LongToSignedAikenWord = 22,
            LongToSignedAikenDword = 23,
            LongToExcessByte = 24,
            LongToExcessWord = 25,
            LongToExcessDword = 26,
            LongToSignedExcessByte = 27,
            LongToSignedExcessWord = 28,
            LongToSignedExcessDword = 29,
            LongToSimaticBCDTimer = 30,
            LongToSimaticTimer = 34
        }

        // Conversion routines for data type "unsigned 32-bit value"
        public enum CONVERSION_U32Bit
        {
            DwordToUnsignedDword = 0,
            DwordToUnsignedByte = 1,
            DwordToUnsignedWord = 2,
            DwordToSignedByte = 3,
            DwordToSignedWord = 4,
            DwordToSignedDword = 5,
            DwordToBCDByte = 6,
            DwordToBCDWord = 7,
            DwordToBCDDword = 8,
            DwordToAikenByte = 9,
            DwordToAikenWord = 10,
            DwordToAikenDword = 11,
            DwordToExcessByte = 12,
            DwordToExcessWord = 13,
            DwordToExcessDword = 14,
            DwordToSimaticBCDTimer = 15,
            DwordToSimaticTimer = 19,
        }

        // Conversion routines for data type "floating-point number 32-bit IEEE 754"
        public enum CONVERSION_F32Bit
        {
            FloatToFloat = 0,
            FloatToUnsignedByte = 1,
            FloatToUnsignedWord = 2,
            FloatToUnsignedDword = 3,
            FloatToSignedByte = 4,
            FloatToSignedWord = 5,
            FloatToSignedDword = 6,
            FloatToDouble = 7,
            FloatToMSBByte = 8,
            FloatToMSBWord = 9,
            FloatToMSBDword = 10,
            FloatToBCDByte = 11,
            FloatToBCDWord = 12,
            FloatToBCDDword = 13,
            FloatToSignedBCDByte = 14,
            FloatToSignedBCDWord = 15,
            FloatToSignedBCDDword = 16,
            FloatToExtSignedBCDByte = 17,
            FloatToExtSignedBCDWord = 18,
            FloatToExtSignedBCDDword = 19,
            FloatToAikenByte = 20,
            FloatToAikenWord = 21,
            FloatToAikenDword = 22,
            FloatToSignedAikenByte = 23,
            FloatToSignedAikenWord = 24,
            FloatToSignedAikenDword = 25,
            FloatToExcessByte = 26,
            FloatToExcessWord = 27,
            FloatToExcessDword = 28,
            FloatToSignedExcessByte = 29,
            FloatToSignedExcessWord = 30,
            FloatToSignedExcessDword = 31,
            FloatToSimaticBCDTimer = 32,
            FloatToS5Float = 36,
            FloatToSimaticTimer = 37,
        }

        // Conversion routines for data type "floating-point number 64-bit IEEE 754"
        public enum CONVERSION_F64Bit
        {
            DoubleToDouble = 0,
            DoubleToUnsignedByte = 1,
            DoubleToUnsignedWord = 2,
            DoubleToUnsignedDword = 3,
            DoubleToSignedByte = 4,
            DoubleToSignedWord = 5,
            DoubleToSignedDword = 6,
            DoubleToFloat = 7,
            DoubleToMSBByte = 8,
            DoubleToMSBWord = 9,
            DoubleToMSBDword = 10,
            DoubleToBCDByte = 11,
            DoubleToBCDWord = 12,
            DoubleToBCDDword = 13,
            DoubleToSignedBCDByte = 14,
            DoubleToSignedBCDWord = 15,
            DoubleToSignedBCDDword = 16,
            DoubleToExtSignedBCDByte = 17,
            DoubleToExtSignedBCDWord = 18,
            DoubleToExtSignedBCDDword = 19,
            DoubleToAikenByte = 20,
            DoubleToAikenWord = 21,
            DoubleToAikenDword = 22,
            DoubleToSignedAikenByte = 23,
            DoubleToSignedAikenWord = 24,
            DoubleToSignedAikenDword = 25,
            DoubleToExcessByte = 26,
            DoubleToExcessWord = 27,
            DoubleToExcessDword = 28,
            DoubleToSignedExcessByte = 29,
            DoubleToSignedExcessWord = 30,
            DoubleToSignedExcessDword = 31,
            DoubleToSimaticBCDTimer = 32,
            DoubleToS5Float = 36,
            DoubleToSimaticTimer = 37
        }

        // Update Cycles
        [FlagsAttribute]
        public enum DM_CYCLE_ENUM
        {
            DM_CYCLE_WHENCHANGED = 0,
            DM_CYCLE_250MS = 1,
            DM_CYCLE_500MS = 2,
            DM_CYCLE_1S = 3,
            DM_CYCLE_2S = 4,
            DM_CYCLE_5S = 5,
            DM_CYCLE_10S = 6,
            DM_CYCLE_1M = 7,
            DM_CYCLE_5M = 8,
            DM_CYCLE_10M = 9,
            DM_CYCLE_1H = 10,
            DM_CYCLE_USERCYCLE1 = 11,
            DM_CYCLE_USERCYCLE2 = 12,
            DM_CYCLE_USERCYCLE3 = 13,
            DM_CYCLE_USERCYCLE4 = 14,
            DM_CYCLE_USERCYCLE5 = 15
        }

        // Create Tag flag types
        [FlagsAttribute]
        public enum MCP_NVAR_FLAG
        {
            MCP_NVAR_FLAG_CREATE = 1, // Create tag
            MCP_NVAR_FLAG_MODIFY = 2, // Modify tag (do not use in RT. Tested)
            MCP_NVAR_FLAG_TEST = 3 // Checks if a tag exists.
        }

        // Tag selection
        public enum DM_VARFILTER_FLAGS
        {
            DM_VARFILTER_TYPE = 0x00000001,    
            DM_VARFILTER_GROUP= 0x00000002,    
            DM_VARFILTER_NAME = 0x00000004,    
            DM_VARFILTER_CONNECTION = 0x00000008,    
            DM_VARFILTER_NAME_WILDCARD = 0x00000010,	
            DM_VARFILTER_TYPENAME = 0x00000020,
            DM_VARFILTER_FAST_CALLBACK = 0x00010000,    
            DM_VARFILTER_LOCAL_ONLY = 0x00020000 
        }

        // Delete Tag falg types
        [FlagsAttribute]
        public enum MCP_DVAR_FLAG
        {
            MCP_DVAR_FLAG_DELETE = 0x00000001, // Delete tag: 
            MCP_DVAR_FLAG_TEST = 0x00000003 // Checks if a tag exists.
        }
        // Tag property
        [FlagsAttribute]
        public enum DM_VARPROPERTY
        {
            DM_INTERNAL_VAR = 2,// Internal tag
            DM_EXTERNAL_VAR = 4 // External tag
        }

        // Tag Limit flags
        [FlagsAttribute]
        public enum MCP_VARLIM_HAS
        {
            MCP_VARLIM_HAS_MIN_LIMIT = 0x00000008, // Tag has fixed low limit
            MCP_VARLIM_HAS_MAX_LIMIT = 0x00000010, // Tag has fixed high limit            
            MCP_VARLIM_HAS_DEFAULT_VALUE = 0x00000020, // Tag has substitute value        
            MCP_VARLIM_HAS_STARTUP_VALUE = 0x00000040, // Tag has start value        
            MCP_VARLIM_USE_DEFAULT_ON_STARTUP = 0x00000080, // The substitute value is to used on system start-up.
            MCP_VARLIM_USE_DEFAULT_ON_MAX = 0x00000100, // The substitute value is to be used when the high limit is violated.   
            MCP_VARLIM_USE_DEFAULT_ON_MIN = 0x00000200, // The substitute value is to be used when the low limit is violated.
            MCP_VARLIM_USE_DEFAULT_ON_COMM_ERROR = 0x00000400  // The substitute value is to be used when a communication error occurs.
        }

        //Create connection
        [FlagsAttribute]
        public enum MCP_NCON_FLAG
        {
            MCP_NCON_FLAG_CREATE = 1, // Create logical connection
            MCP_NCON_FLAG_MODIFY = 2, // Edit logical connection
            MCP_NCON_FLAG_TEST = 3, // Checks if a logical connection exists
        }

        //Delete connection
        [FlagsAttribute]
        public enum MCP_DCON_FLAG
        {
            MCP_DCON_FLAG_DELETE = 1, // Delete logical connection
            MCP_DCON_FLAG_TEST = 3, // Checks if a logical connection exists
        }

        //Protocol Flags
        [FlagsAttribute]
        public enum MCP_VARPROT
        {
            MCP_VARPROT_TOPLIMITERR = 0x00000001, // A log entry is generated if the value of the tag violates the high limit.
            MCP_VARPROT_BOTTEMLIMITERR = 0x00000002, // A log entry is generated if the value of the tag violates the low limit.
            MCP_VARPROT_TRANSFORMATIONERR = 0x00000004, // A log entry is generated if a conversion error occurs. 
            MCP_VARPROT_WRITEERR = 0x00000010, // A log entry is generated every time an illegal write access is attempted.  
            MCP_VARPROT_WRITEERRAPPLICATION = 0x00000020, // A log entry is generated every time an illegal write access is attempted by the application.
            MCP_VARPROT_WRITEERRPROCESS = 0x00000040 // A log entry is generated every time an illegal write access is attempted by the process.
        }

        // Target Machine Flahs
        [FlagsAttribute]
        public enum DM_SD
        {
            DM_SD_LOCAL = 0x00000001, // To local applications only
            DM_SD_ALL_MACHINES = 0x00000002, // To all computers in the project.
            DM_SD_ALL_SERVERS = 0x00000004, // To all servers in the project.
            DM_SD_ALL_CLIENTS = 0x00000008, // To all clients in the project.
            DM_SD_RELATED_MACHINES = 0x00000010, // To all clients using the same server as the local computer
            DM_SD_FIRST_SERVER = 0x00000020,
            DM_SD_PRIMARY_SERVER = 0x00000040,
            DM_SD_EXCEPT_LOCAL = 0x00000080 // To all computers, except local computers, which correspond to the address description.
        }

        // Varaible States
        [FlagsAttribute]
        public enum DM_VARSTATE
        {
            DM_VARSTATE_NOT_ESTABLISHED = (0x0001), // Connection to partner not established
            DM_VARSTATE_HANDSHAKE_ERROR = (0x0002), // Log error
            DM_VARSTATE_HARDWARE_ERROR = (0x0004), // Network module faulty
            DM_VARSTATE_MAX_LIMIT = (0x0008), // Configured high limit violated
            DM_VARSTATE_MIN_LIMIT = (0x0010), // Configured low limit violated
            DM_VARSTATE_MAX_RANGE = (0x0020), // High format limit violated
            DM_VARSTATE_MIN_RANGE = (0x0040), // Low format limit violated
            DM_VARSTATE_CONVERSION_ERROR = (0x0080), // Conversion error display (in connection with DM_VARSTATE_..._RANGE)
            DM_VARSTATE_STARTUP_VALUE = (0x0100), // Start-up value of the tag
            DM_VARSTATE_DEFAULT_VALUE = (0x0200), // Default value of the tag
            DM_VARSTATE_ADDRESS_ERROR = (0x0400), // Address error in channel
            DM_VARSTATE_INVALID_KEY = (0x0800), // Tag not found / not existing
            DM_VARSTATE_ACCESS_FAULT = (0x1000), // Access to tag not permitted
            DM_VARSTATE_TIMEOUT = (0x2000), // Timeout / no feedback from channel
            DM_VARSTATE_SERVERDOWN = (0x4000) // Server is down
        }

        // Scale Flags
        public enum DM_VARSCALE
        {
            DM_VARSCALE_NOSCALE = 0x00000000, // No scaling
            DM_VARSCALE_LINEAR = 0x00000001 // Linear scaling
        }

        // 
        public enum DM_NOTIFY_VARAIABLE
        {
            DM_NOTIFY_MAX_LIMIT = 0x00000001, // high limit is reached
            DM_NOTIFY_MIN_LIMIT = 0x00000002, // low limit is reached   
            DM_NOTIFY_FORMAT_ERROR = 0x00000004, // conversion error
            DM_NOTIFY_ACCESS_FAULT = 0x00000008, // illegal write access  
            DM_NOTIFY_APPLICATION_WRITE = 0x00000010, // illegal write access
            DM_NOTIFY_PROCESS_WRITE = 0x00000020  // write access by process  
        }

        public enum DM_FLAGS_VARIABLE
        {
            DM_HAS_MIN_LIMIT = 0x00000008, // Tag has fixed low limit   
            DM_HAS_MAX_LIMIT = 0x00000010, // Tag has fixed high limit
            DM_HAS_DEFAULT_VALUE = 0x00000020, // Tag has default value
            DM_HAS_STARTUP_VALUE = 0x00000040, // Tag has start-up value
            DM_USE_DEFAULT_ON_STARTUP = 0x00000080, // Substitute value on system start-up
            DM_USE_DEFAULT_ON_MAX = 0x00000100, // Enter substitute value when high limit is violated
            DM_USE_DEFAULT_ON_MIN = 0x00000200, // Enter substitute value when low limit is violated
            DM_USE_DEFAULT_ON_COMM_ERROR = 0x00000400 // Substitute value on communication error
        }

        public enum DM_GROUPFILTER_FLAG
        {
            DM_GROUPFILTER_DRIVER = 0x00000001, // The name of the driver is used as selection criterion.
            DM_GROUPFILTER_UNIT = 0x00000002, // The name of the channel unit is used as selection criterion.
            DM_GROUPFILTER_CONNECTION = 0x00000002 // The name of the logical connection is used as selection criterion.
        }
}
