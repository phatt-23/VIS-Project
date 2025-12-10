= Architecture

== Components

This project follows a very standard architecture.
It has 3 main components - Data, Domain and Presentation layers.

#figure(
  image("../plantuml-diagrams/component-diagram.png", width: 250pt),
  caption: [Component Diagram]
)

The data layer depends on a database for it is its data source.
The data source is provided by a database and used by the data connector.
Data connector serves as an abstraction over the raw data source and it itself
provides a data access.
Table data gateways use this data access and provide these interfaces - persistence of data entities and querying for data entities.
These interfaces are used by services in the domain layer.
Services provide the domain logic interface.
This interface is used by the presentation layer.

#pagebreak()

== Deployment

This whole application will by deployed on two computational nodes.
One is hosting the database and the other is running an instance of our application.
This application connects to the node hosting the database.
This deployment scheme distibutes the workload thus reduces computaional load on individual nodes.
Client devices connect to the application via a browser.

#figure(
  image("../plantuml-diagrams/deployment-architecture.png", width: 200pt),
  caption: [Deployment Diagram]
)


