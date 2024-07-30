using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime;

[UsedImplicitly]
public class OffsetDateSerializer : NodaTimePatternSerializer<OffsetDate>
{
    public OffsetDateSerializer() : base(OffsetDatePattern.FullRoundtrip) { }
}