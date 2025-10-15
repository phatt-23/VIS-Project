== Use Case 1: Vytvoření repozitáře

- *Název a identifikace:* Vytvoření repozitáře

- *Aktér:* Uživatel

- *Cíl:* Založit nový repozitář, ve kterém může uživatel ukládat a spravovat zdrojové kódy.

- *Prekondice:* 
  - Uživatel je přihlášený do systému.
  - Název repozitáře není v účtu uživatele již použit.

- *Spouštěcí událost:* Uživatel klikne na tlačítko „Nový repozitář“.

- *Hlavní scénář:* 
  - Systém zobrazí formulář pro vytvoření repozitáře.
  - Uživatel zadá název, volitelný popis a nastaví viditelnost (veřejný/soukromý).
  - Uživatel potvrdí vytvoření.
  - Systém ověří, že zadané údaje jsou platné.
  - Systém vytvoří nový repozitář, včetně počáteční větve (např. main).
  - Systém zobrazí stránku nově vytvořeného repozitáře.

- *Alternativní scénář:* Název je neplatný nebo již existuje - systém zobrazí chybové hlášení a umožní opravu.

- *Postkondice:*
  - V databázi vznikne nový záznam o repozitáři.
  - Uživatel se stává vlastníkem repozitáře.

#figure(
  image("../plantuml-diagrams/create-repo-activity.png"),
  caption: [Use-case diagram - Vytvoření repozitáře]
)