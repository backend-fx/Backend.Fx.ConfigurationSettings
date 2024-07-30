using Backend.Fx.ConfigurationSettings.Serializers.NodaTime;
using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.BCL;

[UsedImplicitly]
public class DurationSerializer : NodaTimePatternSerializer<Duration>
{
    public DurationSerializer() : base(DurationPattern.Roundtrip) { }
}