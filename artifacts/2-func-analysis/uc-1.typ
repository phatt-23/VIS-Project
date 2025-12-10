== Use Case 1: Creating a Repository

- *Name and Identification:* Creating a Repository

- *Actor:* User

- *Goal:* Create a new repository where the user can store and manage source code.

- *Preconditions:* 
  - The user is logged into the system.
  - The repository name is not already used in the user’s account.

- *Triggering Event:* The user clicks the "New Repository" button.

- *Main Scenario:* 
  - The system displays a form for creating a repository.
  - The user enters a name, an optional description, and sets the visibility (public/private).
  - The user confirms the creation.
  - The system verifies that the entered information is valid.
    - The repository name is unique within the user’s repositories.
  - The system creates a new repository.
  - The system displays the page of the newly created repository.

- *Alternative Scenario:* The name is invalid or already exists - the system displays an error message and allows correction.

- *Postconditions:*
  - A new repository record is created in the database.
  - The user becomes the owner of the repository.



#figure(
  image("../plantuml-diagrams/create-repo-activity.png", width: 250pt),
  caption: [Use-case diagram – Creating a Repository]
)

