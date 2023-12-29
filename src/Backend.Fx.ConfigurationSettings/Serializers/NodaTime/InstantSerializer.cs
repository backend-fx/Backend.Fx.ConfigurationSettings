using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime
{
    [UsedImplicitly]
    public class InstantSerializer : NodaTimePatternSerializer<Instant>
    {
        public InstantSerializer() : base(InstantPattern.ExtendedIso) { }
    }
}