= Funkční analýza - Use Case Model

== Hlavní aktéři

#figure(
  table(
    columns: (auto, auto),
    align: (left, left),
    table.header([Aktér], [Popis]),
    [Uživatel], 
      [Registrovaný uživatel systému, který může vytvářet repozitáře, provádět commity, spravovat issues a pull requesty.],
    [Návštěvník],
      [Nepřihlášený uživatel, který si může prohlížet veřejné repozitáře.],
    [Administrátor],
      [Správce systému, který dohlíží na chod aplikace, 
      spravuje uživatele a řeší incidenty.],
  )
)

== Use-case diagram

#image("../plantuml-diagrams/use-case-diagram.png")
#pagebreak()

#include "uc-1.typ"
#pagebreak()

#include "uc-2.typ"
#pagebreak()

#include "uc-3.typ"
#pagebreak()