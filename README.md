# 📚 BackEnd Monolítico – Registro de Materias de Estudiantes  
## Prueba Técnica – Interrapidísimo

Este proyecto corresponde al **BackEnd monolítico** desarrollado como parte de una **prueba técnica para la empresa Interrapidísimo**.  
La aplicación implementa el patrón **MVC (Model–View–Controller)** y está orientada al **registro de materias de estudiantes que se encuentren en sesión activa**.

---

## Descripción General

El sistema expone una **API REST** que permite:

- Registrar materias para estudiantes que se encuentren en sesión.
- Centralizar la lógica de negocio, controladores y acceso a datos en una única aplicación.

Este enfoque monolítico permite una solución más directa y sencilla, ideal para evaluar la correcta implementación de reglas de negocio y flujo de datos dentro de una arquitectura tradicional.

---

## Arquitectura

La solución está construida bajo un **enfoque monolítico utilizando el patrón MVC**, donde:

- **Models**  
  Representan las entidades del dominio y la lógica de acceso a datos.

- **Views**  
  (Si aplica) Encargadas de la representación visual del sistema, para este caso se usa un FrontEnd en Angular.

- **Controllers**  
  Gestionan las solicitudes HTTP, coordinan la lógica de negocio y devuelven las respuestas correspondientes.

En este proyecto, toda la aplicación se despliega como una sola unidad, compartiendo contexto, configuración y dependencias.

---

## Despliegue

El backend se encuentra **desplegado en una VPS Linux**, ejecutándose en un entorno de producción.

### Documentación Swagger

La documentación interactiva de la API está disponible en:

https://apiregistroestudiantes.joelflow.com/swagger/index.html

Desde Swagger es posible:
- Consultar los endpoints disponibles.
- Probar las operaciones directamente desde el navegador.
- Revisar los modelos de datos expuestos por la API.

---

## Tecnologías Utilizadas

- ASP.NET Core (.NET)
- Patrón MVC (Model–View–Controller)
- API REST
- Swagger / OpenAPI
- Despliegue en VPS Linux
- Control de versiones con Git

---

## Objetivo de la Prueba Técnica

El objetivo de este proyecto es evaluar:

- Implementación correcta de una arquitectura monolítica.
- Uso del patrón MVC en aplicaciones backend.
- Organización del código y claridad en el flujo de datos.
- Cumplimiento de reglas de negocio definidas.
- Capacidad de despliegue en entornos productivos reales.

---

## Autor

**Ing. Joel Baena**  
Backend Developer – .NET  

Proyecto desarrollado como prueba técnica para **Interrapidísimo**.
