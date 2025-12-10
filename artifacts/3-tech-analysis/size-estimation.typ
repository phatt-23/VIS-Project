== Odhad velikostí entit a jejich množství

#table(
  columns: (auto, auto, auto, auto),
  table.header(
    [*Entita*],
    [*Odhadovaný počet záznamů*],
    [*Velikost jednoho záznamu*],
    [*Poznámka*],
  ),
  [User], [5000], [\~2 KB], [základní profil + hash hesla],
  [Repository], [15000], [\~3 KB], [metadata + nastavení přístupu],
  [Commit], [500000], [~5 KB], [hash, message, reference na předka],
  [Issue], [50000], [~3 KB], [popis problému, status, priorita],
  [Comment], [200000], [~1 KB], [text, autor, vazba na issue/commit],
  [File], [1000000], [~4 KB], [metadata, nikoliv binární obsah],
)

- *Celkový odhad datové velikosti:* 6-8 GB v produkčním provozu (včetně indexů a metadat).
