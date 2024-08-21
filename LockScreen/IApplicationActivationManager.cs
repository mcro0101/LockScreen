using System.Runtime.InteropServices;

namespace LockScreen
{
    public enum ActivateOptions
    {
        None = 0x0,
        //DesignMode = 0x1,
        //NoErrorUi = 0x2,
        //NoSplashScreen = 0x4,
    };

    [Guid("2e941141-7f97-4756-ba1d-9decde894a3d")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IApplicationActivationManager
    {
        int ActivateApplication(
            [In][MarshalAs(UnmanagedType.LPWStr)] string appUserModelId,
            [In][MarshalAs(UnmanagedType.LPWStr)] string arguments,
            [In][MarshalAs(UnmanagedType.U4)] ActivateOptions options);
    }

    [ComImport]
    [Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]
    public class ApplicationActivationManager
    {
    }
}
