
namespace QMPT.Data.ModelRestrictions.Devices
{
    public static class DeviceRestrictions
    {
        public const int numberMaxLength = 50;
        public const int numberMinLength = 1;

        public const int measurementRangeMaxLength = 50;
        public const int measurementRangeMInLength = 1;

        public const int permissibleSystematicErrorMaxMaxLength = 50;
        public const int permissibleSystematicErrorMaxMinLength = 1;

        public const int admissibleRandomErrorsMaxMaxLength = 1000;
        
        public const int admissibleRandomErrorsMaxKeyMaxLength = 50;
        public const int admissibleRandomErrorsMaxKeyMinLength = 1;

        public const int admissibleRandomErrorsMaxValueMaxLength = 50;
        public const int admissibleRandomErrorsMaxValueMinLength = 1;

        public const int magnetometerReadingsVariationMaxLength = 50;
        public const int magnetometerReadingsVariationMinLength = 1;

        public const int gradientResistenceMaxLength = 50;
        public const int gradientResistenceMinLength = 1;

        public const int signalAmplitudeMaxLength = 50;
        public const int signalAmplitudeMinLength = 1;

        public const int relaxtionTimeMaxLength = 50;
        public const int relaxtionTimeMinLength = 1;

        public const int optimalCycleMaxLength = 50;
        public const int optimalCycleMinLength = 1;
    }
}
