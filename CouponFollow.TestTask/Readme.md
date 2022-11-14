# CouponFollow test task

This is a repository meant for a recruitment task.
It contains the outlined tasks coded in C# using Playwright and NUnit.

## Building
In Visul studio, simply build the project via F6.
For the command line, use 
	dotnet build
This will pull the dependencies and compile

## Running tests
**Before** running the tests, find the <em>playwright.ps1</em> in your **bin** folder file and run it with the install param
	bin/Debug/netX/playwright.ps1 install

From there, in Visual Studio you can run the tests from the test explorer.
For the command line, both commands work
	dotnet test
	dotnet vstest
