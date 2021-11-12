using NUnit.Framework;
using TollCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumerVehicleRegistration;
using CommercialRegistration;
using LiveryRegistration;

namespace TollCalc.Tests
{
    [TestFixture]
    public class TollCalculatorTests
    {
        private readonly TollCalculator calculator = new();

        public static IEnumerable<TestCaseData> TestCasesForCalculateTollTest_GetsVehiclesWhoseTypeMatchesRequired
        {
            get
            {
                yield return new TestCaseData(new Car { Passengers = 0 }, 2.5m);
                yield return new TestCaseData(new Car { Passengers = 1 }, 2m);
                yield return new TestCaseData(new Car { Passengers = 2 }, 1.5m);
                yield return new TestCaseData(new Car { Passengers = 3 }, 1m);
                yield return new TestCaseData(new Taxi { Fares = 0 }, 4.5m);
                yield return new TestCaseData(new Taxi { Fares = 1 }, 3.5m);
                yield return new TestCaseData(new Taxi { Fares = 2 }, 3m);
                yield return new TestCaseData(new Taxi { Fares = 4 }, 2.5m);
                yield return new TestCaseData(new Bus { Capacity = 20, Riders = 3 }, 7m);
                yield return new TestCaseData(new Bus { Capacity = 20, Riders = 10 }, 5m);
                yield return new TestCaseData(new Bus { Capacity = 20, Riders = 19 }, 4m);                
                yield return new TestCaseData(new DeliveryTruck { GrossWeightClass = 2000}, 8m);
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

        [TestCaseSource(typeof(TollCalculatorTests), nameof(TestCasesForCalculateTollTest_GetsVehiclesWhoseTypeMatchesRequired))]
        public void CalculateTollTest_GetsVehiclesWhoseTypeMatchesRequired_ReturnsResult(object vehicle, decimal toll)
        {
            Assert.AreEqual(toll, calculator.CalculateToll(vehicle));
        }


        [TestCaseSource(typeof(TollCalculatorTests), nameof(TestCasesForCalculateTollTest_GetsUnknownTypeOfVehcle))]
        public void CalculateTollTest_GetsUnknownTypeOfVehcle_ThrowsArgumentException(object vehicle)
        {
            Assert.Throws<ArgumentException>(() => calculator.CalculateToll(vehicle), "Unknown type of vehicle.", nameof(vehicle));
        }

        [TestCase(null)]
        public void CalculateTollTest_GetsNull_ThrowsArgumentNullException(object vehicle)
        {
            Assert.Throws<ArgumentNullException>(() => calculator.CalculateToll(vehicle), nameof(vehicle));
        }
    }
}