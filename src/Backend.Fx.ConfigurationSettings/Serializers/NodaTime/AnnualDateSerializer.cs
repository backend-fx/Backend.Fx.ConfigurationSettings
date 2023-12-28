using JetBrains.Annotations;
using NodaTime;
using NodaTime.Text;

namespace Backend.Fx.ConfigurationSettings.Serializers.NodaTime
{
    [UsedImplicitly]
    public class AnnualDateSerializer : NodaTimePatternSerializer<AnnualDate>
    {
        public AnnualDateSerializer() : base(AnnualDatePattern.Iso) { }
    }
}