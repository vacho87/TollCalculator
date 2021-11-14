using CommercialRegistration;

namespace TollCalc.CustomVehicleTypes
{
    /// <summary>
    /// Class CustomTruck. Matches the <see langword="CommercialRegistration.DeliveryTruck"/> type
    /// and privides functionality for calculating toll value for certain instance of <see langword="DeliveryTruck"/> class.
    /// </summary>
    public class CustomTruck
    {        
        private static readonly decimal truckNormalToll = 10m;        
        private int GrossWeightClass { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTruck" /> class.
        /// </summary>
        /// <param name="truck">The truck.</param>
        public CustomTruck(DeliveryTruck truck)
        {
            this.GrossWeightClass = truck.GrossWeightClass;
        }

        /// <summary>
        /// Calculates the toll.
        /// </summary>
        /// <returns>System.Decimal.</returns>
        public decimal CalcToll() => this.GrossWeightClass switch
        {
            < 3000 => truckNormalToll - 2m,
            > 5000 => truckNormalToll + 5m,
            _ => truckNormalToll,
        };
    }
}
