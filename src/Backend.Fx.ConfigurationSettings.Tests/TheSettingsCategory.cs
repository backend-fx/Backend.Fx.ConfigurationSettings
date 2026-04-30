using Backend.Fx.ConfigurationSettings.Tests.Dummy;
using FakeItEasy;
using NodaTime;
using Xunit;

namespace Backend.Fx.ConfigurationSettings.Tests;

public class TheSettingsCategory
{
    private readonly ISettingRepository _repository = A.Fake<ISettingRepository>();
    private readonly DummyCategory _sut;

    public TheSettingsCategory()
    {
        _sut = new DummyCategory(_repository, new SettingSerializerFactory());
    }

    [Fact]
    public void WritesSerializedStringToRepository()
    {
        _sut.MyStringSetting = "hello";

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyStringSetting", "hello"))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void WritesNullStringToRepository()
    {
        _sut.MyStringSetting = null;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyStringSetting", null))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void WritesSerializedNullableIntegerToRepository()
    {
        _sut.MyNullableIntegerSetting = 7;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyNullableIntegerSetting", "7"))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void WritesNullNullableIntegerToRepository()
    {
        _sut.MyNullableIntegerSetting = null;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyNullableIntegerSetting", null))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void ReadsStringFromRepository()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyStringSetting")).Returns("world");

        Assert.Equal("world", _sut.MyStringSetting);
    }

    [Fact]
    public void ReadsNullStringFromRepository()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyStringSetting")).Returns(null);

        Assert.Null(_sut.MyStringSetting);
    }

    [Fact]
    public void ReadsIntegerFromRepository()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyIntegerSetting")).Returns("99");

        Assert.Equal(99, _sut.MyIntegerSetting);
    }

    [Fact]
    public void ReturnsDefaultIntegerWhenNotConfigured()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyIntegerSetting")).Returns(null);

        Assert.Equal(0, _sut.MyIntegerSetting);
    }

    [Fact]
    public void ReadsNullableIntegerFromRepository()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyNullableIntegerSetting")).Returns("5");

        Assert.Equal(5, _sut.MyNullableIntegerSetting);
    }

    [Fact]
    public void ReturnsNullForNullableIntegerWhenNotConfigured()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyNullableIntegerSetting")).Returns(null);

        Assert.Null(_sut.MyNullableIntegerSetting);
    }

    [Fact]
    public void WritesSerializedInstantToRepository()
    {
        var instant = Instant.FromUtc(2024, 6, 15, 12, 0, 0);
        _sut.MyNullableInstantSetting = instant;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyNullableInstantSetting", "2024-06-15T12:00:00Z"))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void WritesNullInstantToRepository()
    {
        _sut.MyNullableInstantSetting = null;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyNullableInstantSetting", null))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void ReadsInstantFromRepository()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyNullableInstantSetting")).Returns("2024-06-15T12:00:00Z");

        Assert.Equal(Instant.FromUtc(2024, 6, 15, 12, 0, 0), _sut.MyNullableInstantSetting);
    }

    [Fact]
    public void ReturnsNullInstantWhenNotConfigured()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyNullableInstantSetting")).Returns(null);

        Assert.Null(_sut.MyNullableInstantSetting);
    }

    [Fact]
    public void WritesSerializedLocalDateToRepository()
    {
        var date = new LocalDate(2024, 3, 20);
        _sut.MyLocalDateSetting = date;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyLocalDateSetting", "2024-03-20"))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void WritesNullLocalDateToRepository()
    {
        _sut.MyNullableLocalDateSetting = null;

        A.CallTo(() => _repository.WriteSerializedValue("Dummy", "MyNullableLocalDateSetting", null))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public void ReadsLocalDateFromRepository()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyLocalDateSetting")).Returns("2024-03-20");

        Assert.Equal(new LocalDate(2024, 3, 20), _sut.MyLocalDateSetting);
    }

    [Fact]
    public void ReturnsNullLocalDateWhenNotConfigured()
    {
        A.CallTo(() => _repository.GetSerializedValue("Dummy", "MyNullableLocalDateSetting")).Returns(null);

        Assert.Null(_sut.MyNullableLocalDateSetting);
    }
}