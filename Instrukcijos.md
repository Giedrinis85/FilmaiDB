# FilmaiDB
Filmai DB App

Trumpa instrukcija, kaip pasileisti programą:

1. Parsisiųsti visus FilmaiDB (https://github.com/Giedrinis85/FilmaiDB) failus.

2. Darbui su duombaze reikalingas SQL Server 2017 Express (https://go.microsoft.com/fwlink/?linkid=853017) ir SQL Server Management Studio (SSMS) (https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017).

3. Pasileisti SSMS programą ir prisijungti prie siūlomo serverio.

4. Sukurti naują duombazę "FilmaiDB" (dešiniu pelės klavišu spausti ant "Object Explorer" lange esančio "Databases", pasirinkti "New Database...", "Database name:" laukelyje įrašyti "FilmaiDB", spausti "OK").

5. Su SSMS atsidaryti ir execute'int keturis .sql failus iš FilmaiDB (svarbu, kad paskutinis būtų "Filmai.sql"). Susikurs reikalingos SQL lentelės.

6. Pasileisti "Command prompt" programą administratoriaus teisėmis.

7. Įvesti "dotnet --version" ir paspaust "Enter", norint pasitikrint, ar yra .NET Core SDK ir, jei yra, kokia versija. Jei gaunamas toks pranešimas "'dotnet' is not recognized as an internal or external command operable program or batch file", tada reikia įsidiegti naujausią .NET Core Runtime (https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.6-windows-hosting-bundle-installer).

8. Nusikopijuoti (Ctrl + C) kelią iki FilmaiDB.csproj failo, "Command prompt" lange surinkti "cd " (po žodžio "cd" tarpas būtinas) ir įklijuoti (Ctrl + V) tą kelią, paspaust "Enter".

9. Surinkti "dotnet run" komandą, paspausti "Enter". Tada ieškoti eilutės: "Now listening on: http://localhost:5000". Naršyklės lange įvesti http://localhost:5000 ir galima skmingai dirbti su programa.
