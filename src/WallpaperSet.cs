namespace wallswitch;

public class WallpaperSet
{
    Random random = new();

    private List<string> _wallpaperImagePaths = new();

    public WallpaperSet(string[] paths)
    {
        foreach (string path in paths)
        {
            foreach (string imagePath in Directory.GetFiles(path))
            {
                _wallpaperImagePaths.Add(imagePath);
            }            
        }
    }

    public string PickRandomWallpaper()
    {
        return _wallpaperImagePaths[random.Next(0, _wallpaperImagePaths.Count - 1)];
    }
}
