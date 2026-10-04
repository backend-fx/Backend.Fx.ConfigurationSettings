using Backend.Fx.ConfigurationSettings.Serializers.BCL;
using Xunit;

namespace Backend.Fx.ConfigurationSettings.Tests;

public class TheFloatingPointSerializers
{
    [Theory]
    [InlineData(0.1d)]
    [InlineData(0.1d + 0.2d)]
    [InlineData(double.MaxValue)]
    [InlineData(double.MinValue)]
    [InlineData(double.Epsilon)]
    [InlineData(-1234567890.123456789d)]
    public void DoubleRoundTrips(double value)
    {
        var serializer = new DoubleSerializer();

        var serialized = serializer.Serialize(value);
        var deserialized = serializer.Deserialize(serialized);

        Assert.Equal(value, deserialized);
    }

    [Theory]
    [InlineData(0.1f)]
    [InlineData(0.1f + 0.2f)]
    [InlineData(float.MaxValue)]
    [InlineData(float.MinValue)]
    [InlineData(float.Epsilon)]
    [InlineData(-12345.6789f)]
    public void FloatRoundTrips(float value)
    {
        var serializer = new FloatSerializer();

        var serialized = serializer.Serialize(value);
        var deserialized = serializer.Deserialize(serialized);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void SimpleDoubleSerializesToShortestForm()
    {
        var serializer = new DoubleSerializer();

        Assert.Equal("0.1", serializer.Serialize(0.1d));
    }
}
