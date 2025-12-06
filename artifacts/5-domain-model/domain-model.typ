= Domain Model

There are 3 main groups of classes:
- Entities
- Mappers
- Repositories

== Entities

Entities represent the model classes in the domain layer.
They are mostly the same with the models in the data layer, 
but they also include some other methods and have compound
attributes. For example a Commit model also contains Files.

== Mappers

They provide seamless mapping between models 
- from the data layer to the domain layer 
- from the domain layer to the data layer

They are used for abstracting the models internal workings.
The client is only ever exposed to the model from the domain layer
when working in the domain layer.

The opossite is true. The client is only exposed to the data models
when working in the data layer.

== Repositories / Services

These classes provide actions that we can do basic CRUD operations 
upon our models. These CRUD operations are translated into database
calls, that are not necessarily only trivial operations.
Some actions that these services provide deal with DB transactions
and insert multiple records at once. Again the client doesn't have
to worry about the inner workings of the DB, they just call upon the action
and it either succeeds or it fails and throws an error.

#pagebreak()

== Class Diagram

#figure(
  image("../assets/domain-layer-class-diagram.png"),
  caption: [Domain Model Class Diagram]
)

The classes filled red belong to the data layer.

#pagebreak()

== Sequence Diagram

Here's a sequence diagram for adding a commit.


#figure(
  image("../plantuml-diagrams/seq-diagram-add-commit.png"), 
  caption: [Sequence Diagram - Add Commit]
)

// #figure(
//   ```cs
//   Commit CommitRepository::AddCommit(Commit commit, List<File> files) {
//       try {
//           _connector.BeginTransaction();
//
//           var insertedRow = _commitDao.Insert(_commitMapper.MapToRow(commit));
//           foreach (var file in files)
//           {
//               file.CommitId = insertedRow.CommitId;
//               _fileDao.Insert(_fileMapper.MapToRow(file));
//           }
//           
//           _connector.CommitTransaction();
//           return _commitMapper.MapFromRow(insertedRow);
//       }
//       catch (Exception e) {
//           _connector.RollbackTransaction();
//           throw;
//       }
//   }
//   ```,
//   caption: [Code Implementation - Add Commit]
// )

