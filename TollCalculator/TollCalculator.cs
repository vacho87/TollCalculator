using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

namespace TollCalc
{
    public class TollCalculator
    {
        public decimal CalculateToll(object vehicle) =>
        vehicle switch
        {
            Car => 2.00m,
            Taxi => 3.50m,
            Bus => 5.00m,
            DeliveryTruck => 10.00m,
            { } => throw new ArgumentException(message: "Not a known vehicle type", paramName: nameof(vehicle)),
            null => throw new ArgumentNullException(nameof(vehicle))
        };
    }
}