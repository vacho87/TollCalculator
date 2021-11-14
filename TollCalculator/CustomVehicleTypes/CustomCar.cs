using ConsumerVehicleRegistration;

namespace TollCalc.CustomVehicleTypes
{
    /// <summary>
    /// Class CustomCar.Matches the <see langword="ConsumerVehicleRegistration.Car"/> type
    /// and privides functionality for calculating toll value for certain instance of <see langword="Car"/> class.
    /// </summary>
    public class CustomCar
    {
        private static readonly decimal carNormalToll = 2m;       
        private int Passengers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomCar" /> class.
        /// </summary>
        /// <param name="car">The car.</param>
        public CustomCar(Car car)
        {
            this.Passengers = car.Passengers;
        }

        /// <summary>
        /// Calculates the toll.
        /// </summary>
        /// <returns>System.Decimal.</returns>
        public decimal CalcToll() => this.Passengers switch
        {
            0 => carNormalToll + 0.50m,
            1 => carNormalToll,
            2 => carNormalToll - 0.50m,
            _ => carNormalToll - 1.00m,
        };
    }
}
