// Link Settings
#show link: set text(fill: rgb(0, 0, 100)) // make links blue
#show link: underline // underline links

// Heading Settings
#set heading(numbering: "1.")

// Raw Blocks
#show raw: set text(font: "Hack Nerd Font", size: 8pt)
#show raw.where(block: true): it => block(
  inset: 8pt,
  radius: 5pt,
  text(it),
  stroke: (
    left: 2pt + luma(230),
  )
)
#show raw.where(block: false): box.with(
  fill: luma(240),
  inset: (x: 3pt, y: 0pt),
  outset: (y: 3pt),
  radius: 2pt,
)

// Font and Language
#set text(
  // lang: "cs",
  font: "Latin Modern Sans", 
  size: 11pt,
)

// Paper Settings
#set page(paper: "a4")

// TITLE PAGE begin
#let student_fullname = "Phat Tran Dai"
#let student_identifier = "TRA0163"
#let title = "Vývoj informačních systémů"

#set document(
  title: title,
  author: student_fullname + " (" + student_identifier + ")",
  date: auto,
)

#set page(
  margin: 1.2in
)

#align(top + center)[
  Vysoká škola báňská - Technická univerzita Ostrava \
  Fakulta elektrotechniky a informatiky
]

#align(center + horizon)[
  #text(size: 34pt, weight: "bold", [
    #title
  ])
  #v(0.2em)
  #text(size: 24pt, [
    MiniGitHub
  ])
]

#v(40pt)

#align(bottom + left)[
  Jméno: #student_fullname \
  Osobní číslo: #student_identifier
  #h(1fr)  
  Datum: #datetime.today().display("[day]. [month]. [year]")
]

// TITLE PAGE end
#pagebreak()

// Paragraph Settings
#set par(
  // justify: true,
  // first-line-indent: 1em,
  linebreaks: "optimized",
)

// Text margins
// #set block(spacing: 2em)
// #set par(leading: 0.8em)

// Start the Page Counter
#counter(page).update(1)

// OUTLINE BEGIN
#show outline.entry.where(
  level: 1
): it => {
  v(12pt, weak: true)
  strong(it)
}

#outline(indent: auto,
  title: box(
    inset: (bottom: 0.8em),
    text[Content],
  ),
)
// OUTLINE END
#pagebreak()

#show regex("\b\w\b\s"): it => [#it.text.first()~]

#include "1-vize/vize.typ"
#pagebreak()

#include "2-funkcni-analyza/funkcni-analyza.typ"
#pagebreak()

#include "3-tech-analyza/tech-analyza.typ"
#pagebreak()

#include "4-skica/skica.typ"
#pagebreak()

#include "5-domen-model/domen-model.typ"
#pagebreak()

#include "6-architecture/architecture.typ"
