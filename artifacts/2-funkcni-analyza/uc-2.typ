== Use Case 2: Odeslání commitů (Commit změn)

- *Název a identifikace:* Odeslání commitů do repozitáře
- *Aktér:* Uživatel
- *Cíl:* Uložit změny ve zdrojových souborech do historie verzí repozitáře.
- *Prekondice:*
  - Uživatel má právo zapisovat do repozitáře.
  - Repozitář existuje a obsahuje alespoň jednu větev.
- *Spouštěcí událost:* Uživatel provede změny v lokální kopii repozitáře a chce je uložit.

- *Hlavní scénář:*
  - Uživatel vybere soubory, které chce commitnout.
  - Zadá popis změny (commit message).
  - Potvrdí odeslání.
  - Systém ověří přístupová práva uživatele.
  - Systém vytvoří nový commit a propojí ho s předchozím.
  - Systém aktualizuje historii verzí dané větve.
  - Systém zobrazí potvrzení o úspěšném commitu.

- *Alternativní scénáře:*
  - Uživatel nemá oprávnění k zápisu → systém zobrazí chybu „Access denied“.
  - Došlo ke konfliktu s aktuální verzí → systém požádá o vyřešení konfliktu.

- *Postkondice:*
  - Nový commit je uložen v databázi.
  - Historie repozitáře je rozšířena o nový stav.

#figure(
  image("../plantuml-diagrams/create-commit-activity.png"),
  caption: [Use-case diagram - Odeslání commitů do repozitáře]
)
