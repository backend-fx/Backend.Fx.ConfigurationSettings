using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime;

[UsedImplicitly]
public class LocalDateSerializer : NodaTimePatternSerializer<LocalDate>
{
    public LocalDateSerializer() : base(LocalDatePattern.FullRoundtrip) { }
}