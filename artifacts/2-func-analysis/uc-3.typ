== Use Case 3: Creating an Issue

- *Name and Identification:* Creating an Issue
- *Actor:* User (Contributor or Maintainer)
- *Goal:* Report a problem, bug, or enhancement proposal in the repository so it can be tracked and resolved.

- *Preconditions:*
  - The repository exists.
  - The user has permission to create issues in the repository.

- *Triggering Event:* The user discovers a bug or has an improvement idea and clicks “New Issue”.

- *Main Scenario:*
  - The user opens the repository page.
  - Clicks “New Issue”.
  - The system displays the issue creation form.
  - The user fills in the title, description (and optionally adds labels or assigns an assignee, not implemented).
  - The user confirms the creation of the issue.
  - The system validates the inputs (e.g., forbidden characters, title length).
  - The system stores the issue in the database with the default status *Open*.
  - The system displays the newly created issue in the list.

- *Alternative Scenarios:*
  - The input contains invalid or empty values → the system displays an error message and allows correction.
  - A database write error occurs → the system displays the error “Failed to save issue”.

- *Postconditions:*
  - The new issue is visible in the list of all issues.
  - Users can comment on it or change its status (e.g., Closed).

#figure(
  image("../plantuml-diagrams/create-issue-activity.png", width: 300pt),
  caption: [Use-case diagram – Creating an issue]
)

