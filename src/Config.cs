using System.Diagnostics.CodeAnalysis;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace wallswitch;

class WallpaperSetInfo
{
    public string directory;
}

public static class Config
{
    public static Dictionary<string, WallpaperSet> GetSetsFromConfig()
    {
        string userHome = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string yaml = File.ReadAllText($"{userHome}/.local/share/wallswitch/config.yaml");

        IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .Build();

        var dict = deserializer.Deserialize<Dictionary<string, WallpaperSetInfo>>(yaml);

        Dictionary<string, WallpaperSet> sets = new();

        foreach (string name in dict.Keys)
            sets.Add(name, new WallpaperSet([dict[name].directory]));

        return sets;
    }
}
