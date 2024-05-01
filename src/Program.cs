using wallswitch;

namespace Wallswitch;

static class Program
{
    static void Main(string[] args)
    {
        
        Dictionary<string, WallpaperSet> sets = Config.GetSetsFromConfig();
        WallpaperSet set = sets[args[0]];

        Setter.SetWallpaper(set.PickRandomWallpaper());
    }
}