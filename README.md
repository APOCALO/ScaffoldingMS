
# 馃寪 Proyecto Scaffold API .NET 9 para Microservicios

![Plataforma](https://img.shields.io/badge/platform-.NET%208-blueviolet)
![Licencia](https://img.shields.io/badge/license-MIT-green)
![Arquitectura limpia](https://img.shields.io/badge/architecture-clean-blue)
![DDD](https://img.shields.io/badge/pattern-DDD-orange)
![MediatR](https://img.shields.io/badge/tool-MediatR-red)

> **Proyecto de andamiaje de API eficiente para crear microservicios con .NET 9, aprovechando la arquitectura limpia, el dise帽o orientado al dominio (DDD) y MediatR**. Este proyecto proporciona una base altamente extensible para crear servicios robustos y escalables teniendo en cuenta las mejores pr谩cticas. 馃幆

---

## 馃幆 Visi贸n general

Este repositorio sirve de andamiaje para crear microservicios .NET eficientes. Dise帽ado teniendo en cuenta la modularidad y la mantenibilidad, proporciona un s贸lido punto de partida para aplicaciones de nivel empresarial. El proyecto se basa en:

- 馃彈 **Arquitectura limpia**: Fomenta la separaci贸n de preocupaciones, lo que facilita las pruebas, el mantenimiento y la ampliaci贸n.
- 馃摝 **Dise帽o orientado al dominio (DDD)**: Ayuda a estructurar la l贸gica empresarial compleja con capas espec铆ficas del dominio.
- 馃摗 **MediatR**: Facilita la comunicaci贸n entre componentes utilizando el patr贸n mediador para un c贸digo m谩s limpio y desacoplado.

---

## 馃洜 Caracter铆sticas

- **Estructura modular**: Proporciona capas organizadas para dominio, aplicaci贸n e infraestructura.
- **Altamente extensible**: A帽ade f谩cilmente nuevas funciones y componentes sin afectar a la estructura central.
- **Rendimiento eficiente**: C贸digo base optimizado para aplicaciones de microservicios con capacidad de respuesta.
- **Mejores pr谩cticas**: Sigue las mejores pr谩cticas de .NET y de arquitectura para un c贸digo limpio y mantenible.

---

## 馃殌 Primeros pasos

### Requisitos previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (u otra base de datos compatible)
- [Docker](https://www.docker.com/products/docker-desktop) (opcional para el desarrollo en contenedores)

### Instalaci贸n

1. **Clonar el repositorio**:
   ```bash
   git clone https://github.com/APOCALO/ScaffoldingMS.git
   cd ScaffoldingMS
   ```

2. **Instalar dependencias**:
   - Restore the dependencies by running:
     ```bash
     dotnet restore
     ```

3. **Configurar la base de datos**:
   - Actualiza `appsettings.json` con tu cadena de conexi贸n a la base de datos.

4. **Ejecutar la aplicaci贸n**:
   ```bash
   dotnet run
   ```

---

## 馃摉 Utilizaci贸n

Este scaffold est谩 dise帽ado para apoyar el r谩pido desarrollo de APIs. Puede empezar a crear nuevas funciones defini茅ndolas en las capas **Dominio** y **Aplicaci贸n**, y exponi茅ndolas despu茅s a trav茅s de la capa **API**.

- **A帽adir nuevos Endpoints**: Definir nuevos endpoints a帽adiendo DTOs de petici贸n/respuesta en `Application` y handlers con MediatR.
- **Ampliar el dominio**: Organizar la l贸gica de negocio dentro de la capa `Domain` para la reutilizaci贸n y la coherencia.

Consulte la [Documentaci贸n de la API](#) 馃搫 para obtener una lista detallada de los puntos finales disponibles y ejemplos de uso.

---

## 馃搨 Estructura del proyecto

```
馃搧 YourProjectName
鈹溾攢鈹� 馃搧 Web.Api              # Contiene la capa API
鈹溾攢鈹� 馃搧 Application          # Capa de aplicaci贸n con l贸gica de negocio, DTOs y handlers MediatR
鈹溾攢鈹� 馃搧 Domain               # Capa de dominio para entidades, interfaces y l贸gica central
鈹斺攢鈹� 馃搧 Infrastructure       # Capa de infraestructura para acceso a datos y servicios externos
```

---

## 馃鈥嶐煠濃�嶐煣?Contribuci贸n

Las contribuciones son bienvenidas. Si desea contribuir a este proyecto, por favor:

1. Fork del repositorio.
2. Crear una nueva rama (`feature/YourFeature`).
3. Commit de los cambios..
4. Reliaza push a la rama y crea un Pull Request.

Por favor, lea nuestras [Directrices de contribuci贸n](CONTRIBUTING.md) para m谩s detalles. 馃檶

---

## 馃搫 Licencia

Este proyecto est谩 licenciado bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para m谩s detalles.

---

## 馃専 Agradecimientos

Un agradecimiento especial a la comunidad .NET y a los colaboradores que mejoran continuamente el ecosistema. 馃檹

---

> Hecho con 鉂わ笍 por [Apocalo](https://github.com/APOCALO)
    