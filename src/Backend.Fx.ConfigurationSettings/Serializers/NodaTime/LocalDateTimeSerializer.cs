using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime
{
    [UsedImplicitly]
    public class LocalDateTimeSerializer : NodaTimePatternSerializer<LocalDateTime>
    {
        public LocalDateTimeSerializer() : base(LocalDateTimePattern.BclRoundtrip) { }
    }
}