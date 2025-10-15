== Odhad počtu současně pracujících uživatelů

- Systém cílí na menší vývojové týmy a jednotlivce, proto:
  - Běžná zátěž 50-100 současně aktivních uživatelů
  - Špičkové vytížení až 300 uživatelů (např. open-source komunita)
  - Neaktivní účty: až 10x více než aktivních uživatelů
- Vzhledem k asynchronní povaze práce s repozitářem (většina operací je krátká) je tento rozsah realistický pro běžný serverový deployment.