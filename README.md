# Poradnia
1. Coppy project to visual studio.
2. Download Docker Desktop
3. Pull command copied docker pull mcr.microsoft.com/mssql/server
4. run command: docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1qaz@WSX" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
5. Opent projerct in visual studio
6. Open Package Manager Console
7. Set defoult project: Persistanece
8. run: update-database
9. run project on IIS Express
