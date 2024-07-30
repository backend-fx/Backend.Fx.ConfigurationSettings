using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime;

[UsedImplicitly]
public class OffsetDateTimeSerializer : NodaTimePatternSerializer<OffsetDateTime>
{
    public OffsetDateTimeSerializer() : base(OffsetDateTimePattern.FullRoundtrip) { }
}