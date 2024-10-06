# Proyecto Loymark con Angular 18 y .NET 8

Este proyecto es una aplicación web desarrollada utilizando Angular 18 en el frontend y .NET 8 en el backend.

## Estructura del Proyecto

- **Frontend**: Implementado utilizando Angular 18, que proporciona una interfaz de usuario dinámica y receptiva.

- **Backend**: Desarrollado utilizando .NET 8, que maneja la lógica de negocio y la comunicación con la base de datos.

## Pruebas Unitarias e Integración

Se implementaron pruebas unitarias e integración utilizando XUnit. Estos dos tipos de prueba aseguran la calidad y el funcionamiento adecuado de la aplicación mediante:

- **Pruebas Unitarias**: Para la validación de la lógica de funciones y métodos individuales.

- **Pruebas de Integración**: Para garantizar el correcto funcionamiento de los controladores de la aplicación del lado del cliente.

## Gestión de la Base de Datos

Se utilizó **Entity Framework (EF)** para la gestión de la base de datos con un enfoque **Code First**.

### Estructura del Modelo de Datos

Se incluye un archivo `.sql` con la estructura del modelo de datos. El archivo proporcionado puede ser utilizado para crear una base de datos inicial y sus tablas sin datos semilla. La cadena de conexión a la base de datos utilizada es:

```bash
"Database": "Server=localhost\\SQLEXPRESS; Database=Loymark; Integrated Security=True; TrustServerCertificate=True;"
