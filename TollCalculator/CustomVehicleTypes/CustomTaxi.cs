using LiveryRegistration;

namespace TollCalc.CustomVehicleTypes
{
    /// <summary>
    /// Class CustomTaxi. Matches the <see langword="LiveryRegistration.Taxi"/> type
    /// and privides functionality for calculating toll value for certain instance of <see langword="Taxi"/> class.
    /// </summary>
    public class CustomTaxi
    {
        private static readonly decimal taxiNormalToll = 3.5m;        
        private int Fares { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTaxi" /> class.
        /// </summary>
        /// <param name="taxi">The taxi.</param>
        public CustomTaxi(Taxi taxi)
        {
            this.Fares = taxi.Fares;
        }

        /// <summary>
        /// Calculates the toll.
        /// </summary>
        /// <returns>System.Decimal.</returns>
        public decimal CalcToll() => this.Fares switch
        {
            0 => taxiNormalToll + 0.50m,
            1 => taxiNormalToll,
            2 => taxiNormalToll - 0.50m,
            _ => taxiNormalToll - 1.00m,
        };
    }
}
