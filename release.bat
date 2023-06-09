@echo off
dotnet build src/Limbo.Umbraco.MigrationsApi --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget