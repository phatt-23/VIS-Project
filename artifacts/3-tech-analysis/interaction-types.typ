== Typy interakcí uživatelů se systémem a jejich náročnost

#figure(
  table(
    columns: (auto, auto, auto, auto),
    align: (left, left, left, left),
    table.header(
      [*Typ interakce*], 
      [*Popis*], 
      [*Odhad náročnosti*], 
      [*Frekvence*]
    ),

    [Zobrazení repozitáře], 
    [čtení metadat, seznam commitů a větví], 
    [nízká], 
    [častá],

    [Commit změn],
    [zápis nové verze do DB, kontrola konfliktů],
    [střední],
    [střední],

    [Vytvoření issue],
    [zápis záznamu, notifikace],
    [nízká],
    [střední],

    [Komentování],
    [zápis textu, aktualizace],
    [nízká],
    [častá],

    [Vytvoření repozitáře],
    [kontrola duplicity, inicializace DB záznamů],
    [střední],
    [méně častá],

    [Prohlížení historie commitů],
    [agregace dat, načtení diffů],
    [vysoká],
    [střední],

    [Vyhledávání],
    [fulltext v repozitářích a issue],
    [střední],
    [častá],
  ),
  caption: [Typy interakcí uživatelů]
)

