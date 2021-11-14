using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;
using System.Diagnostics.CodeAnalysis;
using TollCalc.CustomVehicleTypes;

namespace TollCalc
{
    /// <summary>
    /// Class TollCalculator.
    /// </summary>
    public class TollCalculator
    {        
        /// <summary>
        /// Calculates the toll.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal CalculateToll(object vehicle)
        {
            if (vehicle is null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            return vehicle switch
            {
                Car car => new CustomCar(car).CalcToll(),
                Taxi taxi => new CustomTaxi(taxi).CalcToll(),
                Bus bus => new CustomBus(bus).CalcToll(),
                DeliveryTruck truck => new CustomTruck(truck).CalcToll(),
                _ => throw new ArgumentException("Unknown type of vehicle.", nameof(vehicle))
            };
        }

        /// <summary>
        /// Gets the time peak premium.
        /// </summary>
        /// <param name="timeOfTransit">The time of transit.</param>
        /// <param name="intoTheCity">The into the city.</param>
        /// <returns>System.Decimal.</returns>
        public static decimal GetTimePeakPremium(DateTime timeOfTransit, bool intoTheCity) =>
                    (IsWeekDay(timeOfTransit), GetTimeBamd(timeOfTransit), intoTheCity) switch
                    {
                        (false, _, _) => 1m,
                        (true, TimeBand.Daytime, _) => 1.5m,
                        (true, TimeBand.Overnight, _) => 0.75m,
                        (true, TimeBand.MorningRush, true) => 2m,
                        (true, TimeBand.EveningRush, false) => 2m,
                        _ => 1m,
                    };

        /// <summary>
        /// Determines whether [is week day] [the specified time of transit].
        /// </summary>
        /// <param name="timeOfTransit">The time of transit.</param>
        /// <returns>System.Boolean.</returns>
        private static bool IsWeekDay(DateTime timeOfTransit)
        {
            return timeOfTransit.DayOfWeek switch
            {
                DayOfWeek.Sunday => false,
                DayOfWeek.Saturday => false,
                _ => true,
            };
        }

        /// <summary>
        /// Gets the time bamd.
        /// </summary>
        /// <param name="timeOfTransit">The time of transit.</param>
        /// <returns>TollCalc.TimeBand.</returns>
        private static TimeBand GetTimeBamd(DateTime timeOfTransit)
        {
            return timeOfTransit.Hour switch
            {
                >= 6 and <= 9 => TimeBand.MorningRush,
                > 9 and < 16 => TimeBand.Daytime,
                >= 16 and < 19 => TimeBand.EveningRush,
                _ => TimeBand.Overnight,
            };
        }
    }
}