using System.Diagnostics;

namespace Wallswitch;

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

            case "mate":
                SetWallpaperMATE(path);
                break;

            case "plasma":
                SetWallpaperPlasma(path);
                break;

            default:
                throw new Exception("Unsupported desktop environment");
        }
    }

    private static void SetWallpaperGNOME(string path)
    {
        Process process = new Process();
		process.StartInfo.FileName = "gsettings";
		process.StartInfo.Arguments = $"set org.gnome.desktop.background picture-uri \"file://{path}\"";
		process.StartInfo.UseShellExecute = false;
	
        process.Start();
        process.WaitForExit();

        process.StartInfo.Arguments = $"set org.gnome.desktop.background picture-uri-dark \"file://{path}\"";

        process.Start();
        process.WaitForExit();
    }

    private static void SetWallpaperMATE(string path)
    {
        Process process = new Process();
		process.StartInfo.FileName = "dconf";
		process.StartInfo.Arguments = $"write /org/mate/desktop/background/picture-filename \"'{path}'\"";
		process.StartInfo.UseShellExecute = false;
	
        process.Start();
        process.WaitForExit();
    }

    private static void SetWallpaperPlasma(string path)
    {
        Process process = new Process();
		process.StartInfo.FileName = "/usr/bin/qdbus";
		process.StartInfo.ArgumentList.Add("org.kde.plasmashell");
        process.StartInfo.ArgumentList.Add("/PlasmaShell");
        process.StartInfo.ArgumentList.Add("org.kde.PlasmaShell.evaluateScript");
        process.StartInfo.ArgumentList.Add($"var allDesktops = desktops();print (allDesktops);for (i=0;i<allDesktops.length;i++) {{d = allDesktops[i];d.wallpaperPlugin = \"org.kde.image\";d.currentConfigGroup = Array(\"Wallpaper\", \"org.kde.image\", \"General\");d.writeConfig(\"Image\", \"file://{path}\")}}");

		process.StartInfo.UseShellExecute = false;
        
        process.Start();
        process.WaitForExit();

        Console.WriteLine(process.ExitCode);
    }
}
