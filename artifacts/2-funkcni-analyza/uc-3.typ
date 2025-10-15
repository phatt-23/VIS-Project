== Use Case 3: Vytvoření Issue

- *Název a identifikace:* Vytvoření issue
- *Aktér:* Uživatel (Contributor nebo Maintainer)
- *Cíl:* Nahlásit problém, chybu nebo návrh na vylepšení v repozitáři, aby mohl být sledován a vyřešen.

- *Prekondice:*
  - Repozitář existuje.
  - Uživatel má oprávnění vytvářet issue v daném repozitáři.

- *Spouštěcí událost:* Uživatel zjistí chybu nebo má návrh na vylepšení a klikne na „New Issue“.

- *Hlavní scénář:*
  - Uživatel otevře stránku repozitáře.
  - Klikne na „New Issue“.
  - Systém zobrazí formulář pro vytvoření issue.
  - Uživatel vyplní název, popis a případně přidá štítky (labels) nebo přiřadí řešitele (assignee).
  - Uživatel potvrdí vytvoření issue.
  - Systém ověří vstupy (např. nepovolené znaky, délku názvu).
  - Systém uloží issue do databáze s výchozím stavem Open.
  - Systém zobrazí nově vytvořenou issue v seznamu.

- *Alternativní scénáře:*
  - Zadání obsahuje neplatné nebo prázdné hodnoty → systém zobrazí chybové hlášení a umožní opravu.
  - Došlo k chybě při zápisu do databáze → systém zobrazí chybové hlášení „Nepodařilo se uložit issue“.

- *Postkondice:*
  - Nové issue je viditelné v seznamu všech issues.
  - Uživatelé mohou komentovat nebo měnit její stav (např. Closed).


#figure(
  image("../plantuml-diagrams/create-issue-activity.png"),
  caption: [Use-case diagram - Vytvoření issue]
)