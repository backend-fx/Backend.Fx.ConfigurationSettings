using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime;

[UsedImplicitly]
public class LocalTimeSerializer : NodaTimePatternSerializer<LocalTime>
{
    public LocalTimeSerializer() : base(LocalTimePattern.ExtendedIso) { }
}