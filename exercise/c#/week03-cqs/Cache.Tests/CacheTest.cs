using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace Cache.Tests;
[TestSubject(typeof(Cache))]
public sealed class CacheTest
{
    private readonly Cache _cache = new();

    [Fact]
    public void Cache_Should_Insert_Value_If_Not_Exists()
    {
        var key = "testKey";
        var value = 42;

        _cache.TryAdd(key, value);

        int retrievedValue = _cache.GetOrDefault(key);
        retrievedValue.Should().Be(value);
    }

    [Fact]
    public void Cache_Should_Return_Default_If_Key_Does_Not_Exist()
    {
        var key = "nonExistentKey";

        int retrievedValue = _cache.GetOrDefault(key);

        retrievedValue.Should().Be(default);
    }
}