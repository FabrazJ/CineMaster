# CineMaster
El proyecto está creado por los siguientes literales

ASP.NET V6.0 
ENTITY FRAMEWORK V6.0
HERRAMIENTAS 
SQL SERVER MANAGEMENT STUDIO
MICROSOFT ENTITY FRAMEWORK SQL SERVER
MICROSOFT ENTITY FRAMEWORK TOOLS


1. Desde .Net a ORM
(No fue nada fácil, al comienzo pensaba que sí, por lo que opté hacer dos partes, una Clase de Libreria, para implementarla en ASP.NET y ahí parti.)

3. Los Querys necesarios para la implementación.
Tomando en cuenta que el backend está más enfocado a que funcione bien, la parte del Front, es un diseño para ver el final de como sería.
4. Tienes Librerias, asignadas para su uso que tiene como dependencias el ASP.NET (Que es el Master, el proyecto Padre) para un mejor control

1. *Administrar Butaca (ButacaController y ButacaService):* Se implemento un controlador y un servicio para administrar las butacas en el cine. El controlador ofrece endpoints para realizar operaciones como la creación, actualización y eliminación de butacas, mientras que el servicio contiene la lógica de negocio correspondiente para ejecutar estas operaciones.

2. *Administrar Cartelera (CarteleraController y CarteleraService):* Desarrolle un controlador y un servicio para gestionar la cartelera de películas en el cine. Este componente proporciona endpoints para operaciones como la creación, actualización y eliminación de funciones de cine en la cartelera, así como la consulta de información sobre las funciones existentes.

3. *Obtener Reservas de Películas de Terror en un Rango de Fechas (ReservasController y ReservasService):* Cree un controlador y un servicio para obtener las reservas de películas de terror dentro de un rango de fechas especificado. Este componente permite a los clientes obtener información sobre las reservas de películas de terror que se realizaron en el cine durante un período específico.

4. *Obtener el Número de Butacas Disponibles y Ocupadas por Sala en la Cartelera del Día Actual (CarteleraController y CarteleraService):* Implementamos un endpoint para obtener el número de butacas disponibles y ocupadas por sala en la cartelera del día actual. Este componente utiliza un servicio para consultar la base de datos y calcular la disponibilidad de butacas en cada sala para el día actual.

6. Implemente un endpoint en un controlador llamado CarteleraController que se encarga de gestionar las solicitudes HTTP relacionadas con la cartelera de cine. Este endpoint está diseñado para proporcionar información sobre el número de butacas disponibles y ocupadas por sala en la cartelera del día actual. Para lograr esto, creamos un servicio llamado CarteleraService, que contiene la lógica de negocio necesaria para obtener esta información de la base de datos. Por lo que opté combinar los DTO (Objetos de Transferencia de Datos) para estructurar la información que se devolverá al cliente. El servicio accede a la base de datos utilizando Entity Framework Core para consultar las salas y las butacas asociadas a la cartelera del día actual, calculando el número de butacas disponibles y ocupadas en cada sala. Finalmente, el controlador responde a las solicitudes HTTP invocando el método correspondiente del servicio y devolviendo los datos obtenidos al cliente en formato JSON. Con esta implementación, los clientes pueden consultar fácilmente la disponibilidad de butacas en la cartelera del día actual antes de realizar una reserva.

