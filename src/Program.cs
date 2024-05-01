using wallswitch;

namespace Wallswitch;

static class Program
{
    static void Main(string[] args)
    {
        WallpaperSet set = new(["/home/pt/wallpapers/other"]);

        Setter.SetWallpaper(set.PickRandomWallpaper());
    }
}