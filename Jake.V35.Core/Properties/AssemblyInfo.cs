using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Jake.V35.Core")]
[assembly: AssemblyDescription("支持异步操作，日志模块")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Fingard")]
[assembly: AssemblyProduct("Jake.V35.Core")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("c5548826-d422-4f16-a9db-a6e4cb73035d")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]


//2018.1.12 1.1.0.0 优化日志写入性能，避免频繁开关文件。
//2018.1.24 1.1.1.0 增加日志Id，方便查找同一个文件的不同数据。
//          1.1.1.2 修复没到时间就释放日志对象的问题
//          1.1.4.0 目录变了需要切日志。
//          1.1.4.5 一些bug修复，关闭时写入所有日志
//          1.1.5.0 增加Start方法启动日志服务
[assembly: AssemblyVersion("1.1.5.0")]
[assembly: AssemblyFileVersion("1.1.5.0")]
