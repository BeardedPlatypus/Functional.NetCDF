using System;
using System.Collections.Generic;
using System.Linq;
using BeardedPlatypus.Functional.NetCDF;
using NUnit.Framework;

namespace Functional.NetCDF.Samples.CSharp
{
    /// <summary>
    /// The <see cref="QueryTime"/> sample shows how to implement a simple NetCDF query
    /// to retrieve the time data, the start time and time steps, from a UGRID map file.
    ///
    /// It does so by implementing the <see cref="IQuery"/> interface while exposing the
    /// relevant data as properties.
    /// The query is executed within the actual test, and the data is compared with the
    /// expected data.
    /// </summary>
    [TestFixture]
    public class QueryTime
    {
        /// <summary>
        /// The <see cref="TimeQuery"/> implements the <see cref="IQuery"/> to obtain
        /// the Time component of a NetCDF UGRID file.
        /// It retrieves the start time and time steps.
        /// </summary>
        public class TimeQuery : IQuery
        {
            public DateTime StartTime { get; private set; }

            public IList<TimeSpan> TimeSteps { get; private set; }

            public void Execute(IRepository repository)
            {
                // Retrieve the id by finding the first component that defines a time component.
                // In the case of UGRID files, this is a variable that has a standard_name attribute
                // set to "time".
                var id = repository.RetrieveVariablesWithAttributeWithValue("standard_name", "time")
                                   .FirstOrDefault();

                // The time variable should always have a "units" attribute, that defines a string 
                // as "<time-quantity> since <date>", for example "seconds since 2021-06-14 00:00:00 +00:00"
                var units = repository.RetrieveAttribute<string>(id, "units");
                InterpretUnitsString(units, out var timeStep, out var startTime);
                StartTime = startTime;

                // The "time" variable is a 1D sequence of doubles, we will convert this to time spans
                // by using the <time-quantity> obtained from the units string.
                TimeSteps = repository.RetrieveVariableValue<double>(id)
                                     .Values
                                     .Select(GetToTimeStep(timeStep))
                                     .ToList();
            }

            private static void InterpretUnitsString(string units,
                                                     out string o,
                                                     out DateTime startTime)
            {
                string[] parts = units.Split(" since ", StringSplitOptions.RemoveEmptyEntries);

                // We assume the string is correctly formatted. In production code we might want to add
                // some more validation here.
                o = parts[0];
                startTime = DateTime.Parse(parts[0]);
            }

            private static Func<double, TimeSpan> GetToTimeStep(string timeStepSize) =>
                timeStepSize switch
                {
                    "seconds" => TimeSpan.FromSeconds,
                    "hours" => TimeSpan.FromHours,
                    "days" => TimeSpan.FromDays,
                    _ => throw new ArgumentOutOfRangeException(nameof(timeStepSize), timeStepSize, null)
                };
        }

        [Test]
        public void QueryTime_Sample()
        {
            // Execute the query
            var query = new TimeQuery();
            Service.Query("./example-data/map.nc", query);

            // Validate the results
            var expectedStartTime = DateTime.Parse("2021-06-14 00:00:00 +00:00");
            Assert.That(query.StartTime, Is.EqualTo(expectedStartTime));

            var expectedTimeSteps =
                Enumerable.Range(0, 73)
                          .Select(i => TimeSpan.FromSeconds(i * 1200.0))
                          .ToList();
            Assert.That(query.TimeSteps, Is.EqualTo(expectedTimeSteps));
        }
    }
}