using System.Diagnostics;

namespace wallswitch;

public static class Setter
{
    public static void SetWallpaper(string path)
    {
        string desktopEnvironment = Environment.GetEnvironmentVariable("DESKTOP_SESSION");

        switch (desktopEnvironment)
        {
            case "gnome":
                SetWallpaperGNOME(path);
                break;

            default:
                throw new Exception("Unsupported desktop environment");
        }
    }

    private static void SetWallpaperGNOME(string path)
    {
        Process process = new Process();
		process.StartInfo.FileName = "gsettings";
		process.StartInfo.Arguments = $"set org.gnome.desktop.background picture-uri file://{path}";
		process.StartInfo.UseShellExecute = false;
	
        process.Start();
        process.WaitForExit();

        process.StartInfo.Arguments = $"set org.gnome.desktop.background picture-uri-dark file://{path}";

        process.Start();
        process.WaitForExit();
    }
}
