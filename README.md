# SiempreSeCaeElSistema
Airline

Para usar correctamente debe configurar la base de datos y realizar los/las Migrations (Se entiende que tiene los paquetes NuGet requeridos instalados):

Para configurar ConnectionString (Texto para conectar con la BD y todo lo relacionado). appsettings.json => "ConnectionStrings"; Aquí puede crear un nuevo ConnectionString o bien puede editar el ya creado con la configuración de su servidor.

Realizar los/las Migrations. Tools => NuGet Package Manager => Package Manager Console; Ya en la consola debe escribir: Add-Migration "NombreMigration" Con esto agregaremos el/la Migration a nuestro proyecto. Luego, debemos actualizar la base de datos a partir del Migration ya creado para ello escribimos: Update-Database

Listo.
