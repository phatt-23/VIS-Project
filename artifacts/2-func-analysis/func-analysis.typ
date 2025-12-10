= Functional Analysis – Use Case Model

== Main Actors

#figure(
  table(
  columns: (auto, auto),
  align: (left, left),
  table.header([Actor], [Description]),

  [User],
  [
    A registered user of the system who can create repositories, 
    make commits, manage issues, and pull requests.
  ],

  [Visitor],
  [
    A non-logged-in user who can browse public repositories.
  ],

  [Administrator],
  [
    A system administrator who oversees the operation 
    of the application, manages users, and handles incidents.
  ],
  )
)

#pagebreak()

== Use-Case Diagram

#figure(
  image("../plantuml-diagrams/use-case-diagram.png"),
  caption: [Use Case Diagram],
)
#pagebreak()

#include "uc-1.typ"
#pagebreak()

#include "uc-2.typ"
#pagebreak()

#include "uc-3.typ"
#pagebreak()

