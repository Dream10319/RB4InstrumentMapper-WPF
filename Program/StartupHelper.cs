using Microsoft.Win32;
using System.Reflection;

public class StartupHelper
{
    private const string StartupKeyPath = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";
    private const string AppName = "RB4InstrumentMapper";  // Change this to your app's name

    public static void EnableAutoStart()
    {
        string exePath = Assembly.GetExecutingAssembly().Location + " --startup";

        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKeyPath, writable: true))
        {
            key?.SetValue(AppName, exePath);
        }
    }

    public static void DisableAutoStart()
    {
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKeyPath, writable: true))
        {
            if (key?.GetValue(AppName) != null)
            {
                key.DeleteValue(AppName);
            }
        }
    }

    public static bool IsAutoStartEnabled()
    {
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKeyPath))
        {
            return key?.GetValue(AppName) != null;
        }
    }
}
