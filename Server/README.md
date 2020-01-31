# Migratie uitvoeren van de DB na aanpassen van het model
Commando's in de map data layer:
dotnet ef migrations add {variabele} -s ..\dotNETAcademyServer\ --context DatabaseContext
dotnet ef database update -s ..\dotNETAcademyServer\ --context DatabaseContext