using LiveryRegistration;

namespace TollCalc.CustomVehicleTypes
{
    /// <summary>
    /// Class CustomBus. Matches the <see langword="LiveryRegistration.Bus"/> type
    /// and privides functionality for calculating toll value for certain instance of <see langword="Bus"/> class.
    /// </summary>
    public class CustomBus
    {
        private static readonly decimal busNormalToll = 5m;        
        private int Capacity { get; set; }        
        private int Riders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomBus" /> class.
        /// </summary>
        /// <param name="bus">The bus.</param>
        public CustomBus(Bus bus)
        {
            this.Capacity = bus.Capacity;
            this.Riders = bus.Riders;
        }

        /// <summary>
        /// Calculates the toll.
        /// </summary>
        /// <returns>System.Decimal.</returns>
        public decimal CalcToll() => ((double)this.Riders / (double)this.Capacity) switch
        {
            < 0.5 => busNormalToll + 2m,
            > 0.9 => busNormalToll - 1m,
            _ => busNormalToll,
        };
    }
}
