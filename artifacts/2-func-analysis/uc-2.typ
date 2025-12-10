== Use Case 2: Submitting Commits (Commit Changes)

- *Name and Identification:* Submitting commits to the repository
- *Actor:* User
- *Goal:* Save changes in source files to the repository's version history.
- *Preconditions:*
  - The user has write access to the repository.
  - The repository exists and contains at least one branch.
- *Triggering Event:* The user makes changes in a local copy of the repository and wants to save them.

- *Main Scenario:*
  - The user selects the files they want to commit.
  - They enter a description of the change (commit message).
  - They confirm the submission.
  - The system verifies the user's access rights.
  - The system creates a new commit and links it to the previous one.
  - The system updates the version history of the branch.
  - The system displays a confirmation of a successful commit.

- *Alternative Scenarios:*
  - The user does not have write permissions $->$ the system displays the error "Access denied".
  - A conflict with the current version occurred $->$ the system requests conflict resolution.

- *Postconditions:*
  - The new commit is saved in the database.
  - The repository history is extended with the new state.

#figure(
  image("../plantuml-diagrams/create-commit-activity.png", width: 300pt),
  caption: [Use-case diagram — Submitting commits to the repository]
)

