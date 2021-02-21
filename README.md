# Bet Roulette
 Apuestas en el juego de la ruleta. es un juego donde puedes elegir un número del 0-36 o 37-38 que representan el color rojo y negro respectivamente. apostar una cantidad de   dinero
y probar que tan buena es tu suerte.
# Configuración de aplicación

### Usar AWS Service en el aplicativo.
El aplicativo usa como acceso a datos redis cache y el manejo de log atravéz de cloudwach ambos servicios alojados en AWS. Cuando se trabaje de forma local (development) el aplicativo omite el uso de cloudwatch y trabaja con una versión de redis local.Si se desea usar tales servicios   se tiene que tener los siguientes requisitos.
- Crear redis en aws y obtener cadena de conexión.
- Crear secret manager nombre de key ConnectionStrings__MyTestApp, ahi almacenaremos la cadena de conexión de redis.
- Asignar ASPNETCORE_ENVIRONMENT = Production en dockerfile
- Tener las credenciales de AWS en la carpeta "%UserProfile%\.aws"

### Ejecutar la aplicación con docker.
 
- en la consola de comandos ir a la carpeta donde se encuentra el proyecto (.csproject) => CD Masiv.BetApi
- docker-compose build
- docker-compose up
### Hacer uso de la api
- adjuntar a postam el archivo Roullette.postman_collection.json
### Otros proyectos desarrollados por mi.

- Jira TrackingGit: https://youtu.be/kA4mBo4oRlE
- BundleAnalyzer : https://youtu.be/lD7vOq4bb00
- CoverageAll Javascript : https://youtu.be/id0Zy4v1pw8




