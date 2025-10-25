using System.Collections.Generic;
using System.IO;
using System.Linq;
using Godot;

namespace BasicShmup.ServiceProviders.Configurations;

public class ConfigurationFinder
{
    private static readonly HashSet<string> IgnoredConfigurationDirectories = ["res://.godot/", "res://Src/"];
    private static readonly List<string> ConfigurationFileNameSuffixes = [".config.tres"];

    public IEnumerable<IConfiguration> GetConfigurations()
    {
        var resourcePaths = GetResourcePathIn("res://");

        foreach (var resourcePath in resourcePaths)
        {
            var resource = ResourceLoader.Load(resourcePath);
            if (resource is IConfiguration configuration)
                yield return configuration;
        }
    }

    private static IEnumerable<ResourcePath> GetResourcePathIn(string rootDirectory)
    {
        var directories = new Queue<ResourcePath>();
        directories.Enqueue(rootDirectory);

        while (directories.Count > 0)
        {
            var currentDirectory = directories.Dequeue();
            var subPaths = ResourceLoader.ListDirectory(currentDirectory);

            foreach (var subPath in subPaths)
            {
                var path = Path.Join(currentDirectory, subPath);

                if (IgnoredConfigurationDirectories.Contains(path))
                    continue;

                if (IsDirectory(subPath))
                    directories.Enqueue(path);

                if (HasAcceptedSuffix(path))
                    yield return path;
            }
        }
    }

    private static bool HasAcceptedSuffix(string path)
    {
        return ConfigurationFileNameSuffixes.Any(path.EndsWith);
    }

    private static bool IsDirectory(ResourcePath path) => path.Path.EndsWith('/');
}
