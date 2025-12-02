= Architecture

This project follows a very standard architecture.
It has 3 main components - Data, Domain and Presentation layers.

#figure(
  image("../plantuml-diagrams/component-diagram.png", width: 300pt),
  caption: [Component Diagram]
)

It will be deployed on some server running the instance of the application.
Another server will host the databases.
Client devices are connected to the server running the application.

#figure(
  image("../plantuml-diagrams/deployment-architecture.png", width: 200pt),
  caption: [Deployment Diagram]
)


