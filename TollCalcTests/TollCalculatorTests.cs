using System;
using System.Collections.Generic;
using LiveryRegistration;
using NUnit.Framework;
using ConsumerVehicleRegistration;
using CommercialRegistration;

namespace TollCalc.Tests
{
    [TestFixture]
    public class TollCalculatorTests
    {
        private static readonly DateTime MorningRush = new (2019, 3, 4, 8, 0, 0);
        private static readonly DateTime DayTime = new (2019, 3, 6, 11, 30, 0);
        private static readonly DateTime EveningRush = new (2019, 3, 7, 17, 15, 0);
        private static readonly DateTime OverNight = new (2019, 3, 14, 03, 30, 0);
        private static readonly DateTime WeekendMorningRush = new (2019, 3, 16, 8, 30, 0);
        private static readonly DateTime WeekendDayTime = new (2019, 3, 17, 14, 30, 0);
        private static readonly DateTime WeekendEveningRush = new (2019, 3, 17, 18, 05, 0);
        private static readonly DateTime WeekendOverNight = new (2019, 3, 16, 01, 30, 0);

        public static IEnumerable<TestCaseData> TestCasesForCalculateTollTest_GetsVehiclesWhoseTypeMatchesRequired
        {
            get
            {
                yield return new TestCaseData(new Car { Passengers = 0 }, 2.5m);
                yield return new TestCaseData(new Car { Passengers = 1 }, 2m);
                yield return new TestCaseData(new Car { Passengers = 2 }, 1.5m);
                yield return new TestCaseData(new Car { Passengers = 3 }, 1m);
                yield return new TestCaseData(new Taxi { Fares = 0 }, 4m);
                yield return new TestCaseData(new Taxi { Fares = 1 }, 3.5m);
                yield return new TestCaseData(new Taxi { Fares = 2 }, 3m);
                yield return new TestCaseData(new Taxi { Fares = 4 }, 2.5m);
                yield return new TestCaseData(new Bus { Capacity = 20, Riders = 3 }, 7m);
                yield return new TestCaseData(new Bus { Capacity = 20, Riders = 10 }, 5m);
                yield return new TestCaseData(new Bus { Capacity = 20, Riders = 19 }, 4m);
                yield return new TestCaseData(new DeliveryTruck { GrossWeightClass = 2000 }, 8m);
                yield return new TestCaseData(new DeliveryTruck { GrossWeightClass = 4000 }, 10m);
                yield return new TestCaseData(new DeliveryTruck { GrossWeightClass = 6000 }, 15m);
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForCalculateTollTest_GetsUnknownTypeOfVehcle
        {
            get
            {
                yield return new TestCaseData(new Random());
                yield return new TestCaseData(new object());
                yield return new TestCaseData("Hello world!");
                yield return new TestCaseData(1);
                yield return new TestCaseData(true);                
            }
        }

        public static IEnumerable<TestCaseData> TestCasesForGetTimePeakPremiumTest
        {
            get
            {
                yield return new TestCaseData(MorningRush, true, 2m);
                yield return new TestCaseData(MorningRush, false, 1m);
                yield return new TestCaseData(DayTime, true, 1.5m);
                yield return new TestCaseData(DayTime, false, 1.5m);
                yield return new TestCaseData(EveningRush, true, 1m);
                yield return new TestCaseData(EveningRush, false, 2m);
                yield return new TestCaseData(OverNight, true, 0.75m);
                yield return new TestCaseData(OverNight, false, 0.75m);
                yield return new TestCaseData(WeekendMorningRush, true, 1m);
                yield return new TestCaseData(WeekendMorningRush, false, 1m);
                yield return new TestCaseData(WeekendDayTime, true, 1m);
                yield return new TestCaseData(WeekendDayTime, false, 1m);
                yield return new TestCaseData(WeekendEveningRush, true, 1m);
                yield return new TestCaseData(WeekendEveningRush, false, 1m);
                yield return new TestCaseData(WeekendOverNight, true, 1m);
                yield return new TestCaseData(WeekendOverNight, false, 1m);
            }
        }

        [TestCaseSource(nameof(TestCasesForCalculateTollTest_GetsVehiclesWhoseTypeMatchesRequired))]
        public void CalculateTollTest_GetsVehiclesWhoseTypeMatchesRequired_ReturnsResult(object vehicle, decimal toll)
        {
            Assert.AreEqual(toll, TollCalculator.CalculateToll(vehicle));
        }


        [TestCaseSource(nameof(TestCasesForCalculateTollTest_GetsUnknownTypeOfVehcle))]
        public void CalculateTollTest_GetsUnknownTypeOfVehcle_ThrowsArgumentException(object vehicle)
        {
            Assert.Throws<ArgumentException>(() => TollCalculator.CalculateToll(vehicle), "Unknown type of vehicle.", nameof(vehicle));
        }

        [TestCase(null)]
        public void CalculateTollTest_GetsNull_ThrowsArgumentNullException(object vehicle)
        {
            Assert.Throws<ArgumentNullException>(() => TollCalculator.CalculateToll(vehicle), nameof(vehicle));
        }

        [TestCaseSource(nameof(TestCasesForGetTimePeakPremiumTest))]
        public void GetTimePeakPremiumTest(DateTime timeOfTransit, bool intoTheCity, decimal expectedPremium)
        {
            Assert.AreEqual(expectedPremium, TollCalculator.GetTimePeakPremium(timeOfTransit, intoTheCity));
        }
    }
}