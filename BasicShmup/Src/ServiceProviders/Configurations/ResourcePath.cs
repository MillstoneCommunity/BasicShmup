namespace BasicShmup.ServiceProviders.Configurations;

public record struct ResourcePath(string Path)
{
    public static implicit operator ResourcePath(string path) => new(path);
    public static implicit operator string(ResourcePath resourcePath) => resourcePath.Path;

    public ResourcePath Join(ResourcePath pathToAppend)
    {
        return System.IO.Path.Join(Path, pathToAppend);
    }

    public override string ToString()
    {
        return Path;
    }
}
