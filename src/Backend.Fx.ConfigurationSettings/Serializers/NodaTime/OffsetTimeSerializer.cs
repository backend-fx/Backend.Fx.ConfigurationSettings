using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime
{
    [UsedImplicitly]
    public class OffsetTimeSerializer : NodaTimePatternSerializer<OffsetTime>
    {
        public OffsetTimeSerializer() : base(OffsetTimePattern.ExtendedIso) { }
    }
}