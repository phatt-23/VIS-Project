= Architecture

This project follows a very standard architecture.
It has 3 main components - Data, Domain and Presentation layers.

#figure(
  image("../plantuml-diagrams/component-diagram.png", width: 250pt),
  caption: [Component Diagram]
)

It will be deployed on some server running the instance of the application.
The same server could host the databases. 
If the applicatoin grows, the hosting of the databases could be delegated to another server.
Client devices are connected to the server running the application.

#figure(
  image("../plantuml-diagrams/deployment-architecture.png", width: 200pt),
  caption: [Deployment Diagram]
)


