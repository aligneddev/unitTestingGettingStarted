# Getting started with unit testing

Example code for https://www.aligneddev.net/blog/2018/getting-started-with-unit-testing/ and SD Code Camp 2018

How do you start writing unit tests?

I hope to show an example at my presentation and people will walk out thinking "I can do that".

Master branch is the base starting point.

The prep branch is my practice and preparation for the talk.

## Getting started - create the projects in master

* `dotnet new -i aspnetcore-vuejs`
  * [latest Vue template](https://github.com/MarkPieszak/AspNETCore-Vue-starter)
* in the source/web directory `dotnet new vuejs`
* in the source/web/web.tests directory `dotnet new mstest -n web.tests`
* in the source/api directory `dotnet new webapi`
* in the source/api/api.tests directory `dotnet new mstest -n api.tests`

## 