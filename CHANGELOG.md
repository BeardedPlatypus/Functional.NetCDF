# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.1.2]
### Added
- Added `RetrieveVariableID` method to `IRepository` to retrieve the id
  associated with a variable name

### Fixed
- Bug failing to retrieve correct attributes in the UPM package due to strings being trimmed

## [0.1.1]
### Added
- Added `RetrieveVariableName` method to `IRepository` to retrieve the name  
  associated with a `VariableID`

## [0.1.0]
### Added
- Added initial reading of NetCDF variables and their attributes from NetCDF files
   - Add the `IQuery` interface to define NetCDF queries
   - Add the `Service` to run the queries on a given NetCDF file
   - Add the `IRepository` to query an opened NetCDF file within a query
- Set up the NuGet package structure
