== Konceptuální model domény

=== Hlavní entity:

- *User* - reprezentuje uživatele systému (vlastní účet, přihlašovací údaje, role).
- *Repository* - projektový prostor obsahující commity a issue.
- *Commit* - záznam o změně zdrojového kódu, propojený s autorem. 
- *Issue* - položka pro sledování problémů, chyb nebo návrhů.
- *Comment* - komentář k issue nebo commitu.
- *File* - soubor uložený v repozitáři, s historií verzí.

#figure(
  image("../assets/konceptualni-model.svg"),
  caption: [Konceptualní model (je to entitní model)],
)