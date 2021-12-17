namespace Захист
{
    internal static class HydrogencarbonateCorrection
    {
        internal static (double, double, double) CalculateHydrocard(double BE, double mass)
        {
            double hydrocarb = BE * mass * 0.3;
            double solVolume4 = hydrocarb / 0.496;
            double solVolume8_4 = hydrocarb / 0.9996;
            return (hydrocarb, solVolume4, solVolume8_4);
        }

    }
}
