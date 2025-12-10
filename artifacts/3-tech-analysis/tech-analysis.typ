= Analysis – Technical Requirements

== Conceptual Model of the Domain

=== Main Entities:

- *User* – represents a system user (owns an account, login credentials, role).
- *Repository* – a project space containing commits and issues.
- *Commit* – a record of a source code change, linked to the author.
- *Issue* – an item for tracking problems, bugs, or improvement proposals.
- *Comment* – a comment on an issue or commit.
- *File* – a file stored in a repository, with version history.

#figure(
  image("../assets/konceptualni-model.svg"),
  caption: [Conceptual model (entity model)],
)


#pagebreak()




== Estimated Sizes of Entities and Their Quantities

#table(
  columns: (auto, auto, auto, auto),
  table.header(
    [*Entity*],
    [*Estimated Number of Records*],
    [*Size of One Record*],
    [*Note*],
  ),
  [User],       [5000],     [\~2 KB], [basic profile],
  [Repository], [15000],    [\~3 KB], [metadata],
  [Commit],     [500000],   [\~5 KB], [message, reference to files],
  [Issue],      [50000],    [\~3 KB], [problem title, description, status],
  [Comment],    [200000],   [\~1 KB], [text, author, linked to issue/commit],
  [File],       [1000000],  [\~4 KB], [considering metadata only, not binary content],
)

- *Total estimated data size:* 6–8 GB in production (including indexes and metadata).
- \+ 1000000 Files that can be simple text files (small size), images (bigger files), zipped archives (huge)

#pagebreak()


== Estimated Number of Concurrent Users

- The system targets small development teams and individuals, therefore:
  - Normal load: 50–100 concurrently active users
  - Peak load: up to 300 users 
  - Inactive accounts: up to 10x more than active users

- Given the asynchronous nature of repository operations (most are short), 
  this range is realistic for a standard server deployment.
  
  
#pagebreak()



== Types of User Interactions with the System and Their Complexity

#figure(
  table(
    columns: (auto, auto, auto, auto),
    align: (left, left, left, left),
    table.header(
      [*Interaction Type*], 
      [*Description*], 
      [*Estimated Complexity*], 
      [*Frequency*]
    ),

    [Viewing Repository], 
    [reading metadata, list of commits and issues], 
    [low], 
    [frequent],

    [Committing Changes],
    [writing new version to DB],
    [medium],
    [medium],

    [Creating Issue],
    [writing record],
    [low],
    [medium],

    [Commenting],
    [writing text, updating],
    [low],
    [frequent],

    [Creating Repository],
    [checking for duplicates, initializing DB records],
    [medium],
    [less frequent],

    [Viewing Commit History],
    [data aggregation],
    [high],
    [medium],

    [Searching],
    [full-text search on various records in DB],
    [medium],
    [frequent],
  ),
  caption: [Types of user interactions]
)

#pagebreak()

== Initial System Layout and Platform Choices

=== Architecture:

- Multi-layer architecture with separation of concerns:
  - Data Layer:
    - Implementation using SQLite (development) or PostgreSQL (production)
    - Patterns used: Table Data Gateway, Data Connector, Unit of Work

  - Domain Layer:
    - System logic (repositories, commits, issues, ...) using .NET
    - Pattern: Service Layer, Mapper (interfacing Data layer)

  - Presentation Layer:
    - Web UI: ASP.NET Core MVC

#figure(
  table(
    columns: (auto, auto, auto),
    table.header(
      [*Layer*],
      [*Technology*],
      [*Platform*]
    ),

    [Data],   [SQLite / PostgreSQL],    [Linux],
    [Domain], [.NET C\# Class Library], [cross-platform],
    [Web UI], [ASP.NET Core MVC],       [Browser],
  ),
  caption: [Platform Targeting]
)

