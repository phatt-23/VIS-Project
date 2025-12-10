== První představa o rozložení systému a volba platforem

=== Architektura:

- Vícevrstvá architektura s oddělením zodpovědností:
  - Data Layer:
    - Implementace pomocí SQLite (vývoj) nebo PostgreSQL (produkce).
    - Využití patternů: Data Mapper, Identity Map, Unit of Work.

  - Domain Layer:
    - Logika systému (repozitáře, commity, issue) v čistém C\#.
    - Pattern: Domain Model + Transaction Script.

  - Presentation Layer:
    - WebUI: ASP.NET Core MVC
    - Desktop: Avalonia UI

=== Platformy:
#figure(
  table(
    columns: (auto, auto, auto),
    table.header(
      [*Vrstva*],
      [*Technologie*],
      [*Platforma*]
    ),

    [Data], [SQLite / PostgreSQL], [Linux / Docker],
    [Domain], [.NET C\# Class Library], [cross-platform],
    [Web UI], [ASP.NET Core MVC],	[Browser],
    [Desktop], [UI	Avalonia],	[Linux / Windows],
  )
)