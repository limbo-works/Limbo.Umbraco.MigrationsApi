@echo off
dotnet build src/Limbo.Umbraco.MigrationsApi --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:/nuget