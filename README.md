# Getting started with unit testing

Example code for [my aligneddev.net article](https://www.aligneddev.net/blog/2018/getting-started-with-unit-testing-presentation-version/) and [SD Code Camp 2018](https://southdakotacodecamp.com/speakers/kevin-logan/).

## How do you start writing unit tests?

I hope to show an example at my presentation and people will walk out thinking "I can do that".

## Getting started - create the projects in master

* Get the [latest Vue template](https://github.com/MarkPieszak/AspNETCore-Vue-starter) before starting.
* `dotnet new -i aspnetcore-vuejs`
* in the source/web directory `dotnet new vuejs -n Web`
* in the source/web/web.tests directory `dotnet new mstest -n Web.Tests`
* in the source/api directory `dotnet new webapi -n Api`
* in the source/api/api.tests directory `dotnet new mstest -n Api.Tests`
* run `npm run dev` in source/web

## Tech Used

* Asp.Net MVC Core
* EF Core - may just be mocked data for now
* VueJs

## Branches

* master - has the latest running code
* emptyStart - after running the commands above with a few tweaks
* apiHttpCall - creating the API tests, merged to master with PR1
* inMemoryDatabase - EF using inMemoryDatabase for tests
* presentationStart - emptied out Weather classes and tests with snippet references

## "Business" Goals

Provide a way to track biking miles for purposes of reporting.

1. Reports on miles for the current year, past years, savings and expenses.
1. calculate savings based on car mileage and gas prices.

I currently have a Google Spreadsheet that I use to track all of this and see this as an opportunity to write some fun code.

For the purposes of this talk sample code, we'll keep it simple and not all the functionality will be implemented at this time.

## Models

### RideInfo

The ride information to save and returns. Mirrors the DB structure with EF Core.

## Scenarios/Specs that my "business" needs

### API

> used in my presentation
Given an API call
When asking for current temp
Then it calls the weather Api with the correct zip code

Examples
57105 | 52
00000 | 52 // TODO validate zipcodes

-- get current temp to fill in ride info
Given an API call
When asking for current temp
Then it calls the weather Api with the correct zip code

Given a new ride is submitted
When missing values (invalid)
Then it should return a 503 with a invalid message

Examples

Given a new ride is submitted
When all values filled in (valid)
Then it should persist to the data store with those values

Examples

### Web

### TypeScript tests

These haven't been implemented yet.

Given the new ride screen
When missing values (invalid)
And Save is clicked
Then it should show the validation message(s)

Examples

Given the new ride screen
When all values filled in (valid)
And Save is clicked
Then it should post to the persist the ride

Examples

Given the new ride screen
When all values filled in (valid)
And Save is clicked
And Post fails
Then it should show the correct error message

Given the new ride screen
When all values filled in (valid)
And Save is clicked
And Post succeeds
Then it should clear the values in the form
And Update totals (2nd test)

Examples

### MVC Tests

?? any needed with SPA approach??
[my article on "classic" MVC testing](https://www.aligneddev.net/blog/2018/unit-test-mvc/)