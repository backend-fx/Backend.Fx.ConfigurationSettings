using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime
{
    [UsedImplicitly]
    public class OffsetSerializer : NodaTimePatternSerializer<Offset>
    {
        public OffsetSerializer() : base(OffsetPattern.GeneralInvariant) { }
    }
}