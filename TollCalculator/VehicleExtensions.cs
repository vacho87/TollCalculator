using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

namespace TollCalc
{
    public static class VehicleExtensions
    {
        private const decimal carNormalToll = 2m;
        private const decimal taxiNormalToll = 3.5m;
        private const decimal busNormalToll = 5m;
        private const decimal truckNormalToll = 10m;


        public static decimal CalcToll(this Car car) => car.Passengers switch
        {
            0 => carNormalToll + 0.50m,
            1 => carNormalToll,
            2 => carNormalToll - 0.50m,
            _ => carNormalToll - 1.00m,
        };

        public static decimal CalcToll(this Taxi taxi) => taxi.Fares switch
        {
            0 => taxiNormalToll + 0.50m,
            1 => taxiNormalToll,
            2 => taxiNormalToll - 0.50m,
            _ => taxiNormalToll - 1.00m,
        };

        public static decimal CalcToll(this Bus bus) => ((double)bus.Riders / (double)bus.Capacity) switch
        {
            < 0.5 => busNormalToll + 2m,
            > 0.9 => busNormalToll -1m,            
            _ => busNormalToll,
        };

        public static decimal CalcToll(this DeliveryTruck truck) => truck.GrossWeightClass switch
        {
            < 3000 => truckNormalToll - 2m,
            > 5000 => truckNormalToll + 5m,
            _ => truckNormalToll,
        };
    }
}
