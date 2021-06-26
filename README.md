# Functional.NetCDF
![GitHub](https://img.shields.io/github/license/BeardedPlatypus/Functional.NetCDF) [![Build Status](https://dev.azure.com/mwtegelaers/Functional.NetCDF/_apis/build/status/BeardedPlatypus.Functional.NetCDF?branchName=azure-pipelines)](https://dev.azure.com/mwtegelaers/Functional.NetCDF/_build/latest?definitionId=32&branchName=azure-pipelines)  
*This is a work-in-progress and will be extended in the near-future*

A simple .NET NetCDF4 wrapper for windows written in F# targeting NETStandard 2.0.

The current focus is on reading existing `.nc`files, and not so much creating new files.

## Example code

The following subsections show the implementation of how to query the time 
component from UGRID map file. More samples can be found in the Samples projects.

### F#
*The full code can be found [here]()*

```fsharp
```

### C#
*The full code can be found [here]()*

```csharp
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
```


## Dependencies

* [NetCDF 4.8.0](https://www.unidata.ucar.edu/software/netcdf/docs/index.html): native library for reading and writing NetCDF files
* [F#](https://fsharp.org/): all relevant wrapper code
