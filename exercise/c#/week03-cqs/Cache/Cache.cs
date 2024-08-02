namespace Cache;
public sealed class Cache : Dictionary<string, int>
{
    public int GetOrDefault(string key) => TryGetValue(key, out int value) ? value : default;
}