# TP 5 - Desarrollo y Arquitectura de Software
## ABM de Juegos de Preguntados en XML

### ?? Descripción
Sistema de gestión (ABM - Alta, Baja, Modificación) para juegos de Preguntados almacenados en formato XML. 

El proyecto implementa una **arquitectura de N capas limpia (Clean Architecture)** siguiendo los principios **SOLID**, **DRY**, **YAGNI**, **KISS** y las mejores prácticas de **Clean Code**, utilizando **Dependency Injection** y características modernas de C# 12.

---

## ??? Arquitectura Limpia (Clean Architecture)

El proyecto está organizado siguiendo Clean Architecture, donde cada capa tiene una responsabilidad clara y las dependencias fluyen hacia el centro (dominio):

```
???????????????????????????????????????????????
?    Presentation (UaiDasTp5Ej4)     ?
?    - UI/FrmJuegoABM, FrmPreguntaDetalle  ?
???????????????????????????????????????????????
           ? depende de
???????????????????????????????????????????????
?    Application (APP)   ?
?    - DependencyInjection      ?
???????????????????????????????????????????????
         ? depende de
??????????????????????????????????????????????
? UI  ? Service   ? Repository     ?
? Helpers  ? (SERV)? (REPO) ?
??????????????????????????????????????????????
     ?       ?     ?
??????????????????????????????????????????????
?    Data Context (CONTEXT)  ?
?    - XmlLinqDataAccess (LINQ to XML) ?
??????????????????????????????????????????????
       ? depende de
???????????????????????????????????????????????
?    Abstractions (ABS)  ?
?    - Interfaces (contratos)   ?
???????????????????????????????????????????????
 ? depende de
???????????????????????????????????????????????
?    Domain (DOM) - NÚCLEO      ?
? - Records: Juego, Pregunta, Opcion  ?
?    - IDs inmutables (init-only) ?
?    - FechaCreacion inmutable    ?
?    - Sin dependencias externas   ?
???????????????????????????????????????????????
```

### ?? Estructura del Proyecto

```
UaiDasTp5Ej4/
?
??? DOM/       # ? Capa de Dominio (núcleo)
?   ??? Juego.cs     # Record - Entidad principal
?   ??? Pregunta.cs     # Record - Entidad pregunta
?   ??? Opcion.cs      # Record - Entidad opción
?
??? ABS/ # ?? Capa de Abstracciones
?   ??? IJuegoRepository.cs # Contrato de repositorio
?   ??? IJuegoService.cs    # Contrato de servicio
?   ??? IXmlDataAccess.cs   # Contrato de acceso a datos
?
??? CONTEXT/ # ?? Capa de Contexto/Datos
? ??? XmlLinqDataAccess.cs  # Implementación LINQ to XML
?
??? REPO/        # ?? Capa de Repositorio
?   ??? JuegoRepository.cs  # Repository Pattern + carga bajo demanda
?
??? SERV/    # ?? Capa de Servicios
?   ??? JuegoService.cs # Lógica de negocio + validaciones con LINQ
?
??? UI/        # ?? Helpers de UI
?   ??? MessageHelper.cs    # Mensajes al usuario
?   ??? DisplayExtensions.cs# Formateo de entidades
?
??? APP/  # ?? Capa de Aplicación
?   ??? DependencyInjection.cs  # Configuración de DI
?
??? UaiDasTp5Ej4/      # ??? Capa de Presentación
?   ??? Program.cs      # Entry point + configuración
?   ??? UI/         # Formularios
?   ?   ??? FrmJuegoABM.cs      # Formulario principal ABM
?   ?   ??? FrmJuegoABM.Designer.cs
?   ?   ??? FrmPreguntaDetalle.cs   # Formulario de pregunta
?   ?   ??? FrmPreguntaDetalle.Designer.cs
?   ??? Data/ # ?? Archivos XML y DTD
?   ?   ??? Juegos.dtd      # Definición del esquema
?   ?   ??? JuegosEjemplo.xml   # Datos de ejemplo
?   ??? Docs/          # ?? Documentación técnica
?       ??? Arquitectura.puml   # Diagrama de arquitectura
?       ??? Flujo_Operaciones.puml  # Diagrama de secuencia
?
??? README.md   # Este archivo
??? CHANGELOG.md    # Historial de cambios
```

---

## ?? Principios Aplicados

### ?? SOLID

#### **S - Single Responsibility Principle (SRP)**
Cada clase tiene una única razón para cambiar:
- `JuegoRepository`: Solo gestiona persistencia de datos (carga bajo demanda)
- `JuegoService`: Solo contiene lógica de negocio y validaciones (usando LINQ)
- `XmlLinqDataAccess`: Solo maneja I/O de XML con LINQ
- `FrmJuegoABM`: Solo gestiona la interfaz de usuario del ABM
- `FrmPreguntaDetalle`: Solo gestiona la interfaz de creación de preguntas
- `MessageHelper`: Solo muestra mensajes al usuario

#### **O - Open/Closed Principle (OCP)**
Abierto para extensión, cerrado para modificación:
- Se pueden agregar nuevas implementaciones de `IXmlDataAccess` sin modificar código existente
- Los servicios trabajan con interfaces, no con implementaciones concretas
- Records permiten crear copias inmutables con `with` expression

#### **L - Liskov Substitution Principle (LSP)**
Las implementaciones son sustituibles:
- Cualquier implementación de `IJuegoService` puede usarse sin cambiar el comportamiento
- Las implementaciones de `IXmlDataAccess` son completamente intercambiables

#### **I - Interface Segregation Principle (ISP)**
Interfaces específicas y cohesivas:
- `IJuegoRepository`: Solo operaciones de repositorio
- `IJuegoService`: Solo operaciones de negocio
- `IXmlDataAccess`: Solo operaciones de XML
- Ningún cliente depende de métodos que no usa

#### **D - Dependency Inversion Principle (DIP)**
Depende de abstracciones, no de concreciones:
- Todas las capas dependen de interfaces (ABS)
- La configuración de dependencias está centralizada en `DependencyInjection`
- Facilita testing mediante mocking

### ?? Clean Code Principles

#### **Records e Inmutabilidad**
```csharp
// ? Records con propiedades inmutables
public record Juego
{
    public int Id { get; init; }  // Solo inicialización
    public DateTime FechaCreacion { get; init; } = DateTime.Now;  // Inmutable con default
    public string Nombre { get; set; } = string.Empty;
    // ...
}

// ? With expressions para crear copias (respetando inmutabilidad)
var juegoActualizado = juegoExistente with { Nombre = "Nuevo nombre" };
// FechaCreacion se mantiene automáticamente
```

#### **Propiedades Inmutables**
- **Id**: Solo se puede asignar en construcción/inicialización
- **FechaCreacion**: Solo se puede asignar al crear, tiene valor por defecto `DateTime.Now`
- Al actualizar un juego, estas propiedades se mantienen automáticamente

**Ventajas de la inmutabilidad:**
- Previene modificaciones accidentales
- Thread-safe por diseño
- Más fácil de razonar sobre el código
- Mejor para debugging

#### **IDs Autoincrementales**
```csharp
// ? Generación automática de IDs
private static int GenerarNuevoId(List<Juego> juegos) => 
    juegos.Any() ? juegos.Max(j => j.Id) + 1 : 1;

// ? IDs de preguntas basados en máximo global
var maxPreguntaId = juegosExistentes
    .SelectMany(j => j.Preguntas)
    .Select(p => p.Id)
    .DefaultIfEmpty(0)
    .Max();
```

#### **Carga Bajo Demanda**
```csharp
// ? No mantener caché en memoria
public IEnumerable<Juego> ObtenerTodos() => 
    xmlDataAccess.CargarJuegos(rutaArchivo);

// ? Persistencia automática en cada operación
public void Agregar(Juego juego)
{
    var juegos = ObtenerTodos().ToList();
    // ... lógica ...
    xmlDataAccess.GuardarJuegos(juegos, rutaArchivo);
}
```

#### **LINQ en lugar de foreach**
```csharp
// ? Validaciones con LINQ
var preguntaSinTexto = juego.Preguntas
    .FirstOrDefault(p => string.IsNullOrWhiteSpace(p.Texto));

// ? Transformaciones con LINQ
return preguntas.Select((pregunta, index) =>
{
    var nuevaPregunta = pregunta.Id == 0
     ? pregunta with { Id = maxPreguntaId + index + 1 }
        : pregunta;
 return nuevaPregunta;
}).ToList();
```

#### **Constructores Primarios (C# 12)**
```csharp
// ? Constructor primario - más conciso
public class JuegoService(IJuegoRepository juegoRepository) : IJuegoService
{
    public IEnumerable<Juego> ObtenerTodosLosJuegos() => 
        juegoRepository.ObtenerTodos();
}
```

#### **Expression-Bodied Members**
```csharp
// ? Para métodos simples
public IEnumerable<Juego> ObtenerTodos() => 
    xmlDataAccess.CargarJuegos(rutaArchivo);
    
public Juego? ObtenerPorId(int id) => 
    ObtenerTodos().FirstOrDefault(j => j.Id == id);
```

#### **Null Safety (C# 8+)**
```csharp
// ? Uso de nullable reference types
public Juego? ObtenerPorId(int id) // Indica que puede retornar null
private Juego? _juegoSeleccionado;  // Field nullable
```

### ?? DRY (Don't Repeat Yourself)
- `MessageHelper`: Centraliza todos los mensajes al usuario
- `DisplayExtensions`: Reutiliza formateo de entidades
- `JuegoService`: Centraliza validaciones usando LINQ
- `JuegoRepository`: Un solo punto de acceso a datos con lógica de IDs

### ?? YAGNI (You Aren't Gonna Need It)
- Solo funcionalidad requerida para el ABM
- Sin características especulativas
- Una sola implementación XML (LINQ to XML)
- Sin caché innecesaria (carga bajo demanda)

### ?? KISS (Keep It Simple, Stupid)
- Código simple y directo
- Records para inmutabilidad
- LINQ para operaciones funcionales
- Carga bajo demanda (sin complejidad de caché)
- Una sola implementación XML

---

## ?? Tecnologías

- **.NET 8**
- **C# 12** (Records, init-only properties, with expressions, constructores primarios, collection expressions)
- **Windows Forms**
- **LINQ to XML** (XDocument)
- **Microsoft.Extensions.DependencyInjection**

---

## ?? Características Técnicas Avanzadas

### Records e Inmutabilidad
Los records proporcionan:
- **Value equality**: Comparación por valor, no por referencia
- **With expressions**: Crear copias con propiedades modificadas
- **Init-only setters**: Propiedades que no pueden cambiar después de la inicialización
- **Sintaxis concisa**: Menos código boilerplate

### Propiedades Inmutables en Entidades

#### Juego
```csharp
public record Juego
{
    public int Id { get; init; }  // Inmutable - asignado por el repositorio
    public DateTime FechaCreacion { get; init; } = DateTime.Now;  // Inmutable - se establece al crear
    public string Nombre { get; set; } = string.Empty;  // Mutable
    public string Descripcion { get; set; } = string.Empty;  // Mutable
    // ...
}
```

**Comportamiento:**
- **Crear**: Se establece FechaCreacion = DateTime.Now automáticamente
- **Actualizar**: FechaCreacion se mantiene (no se puede cambiar)
- **UI**: DateTimePicker se deshabilita al editar juegos existentes

#### Pregunta y Opcion
```csharp
public record Pregunta
{
    public int Id { get; init; }  // Inmutable
    // ... propiedades mutables
}

public record Opcion
{
    public int Id { get; init; }  // Inmutable
    // ... propiedades mutables
}
```

### IDs Inmutables y Autoincrementales
```csharp
// IDs solo se pueden asignar en construcción o inicialización
public record Juego
{
    public int Id { get; init; }  // Inmutable después de construcción
}

// Generación automática en el repositorio
var nuevoJuego = juego with 
{ 
    Id = GenerarNuevoId(juegos),
    Preguntas = AsignarIdsPreguntasYOpciones(...)
};
```

### Carga Bajo Demanda (No Caché)
**Ventajas:**
- ? Siempre datos actualizados del archivo
- ? Menor uso de memoria
- ? No hay sincronización entre caché y archivo
- ? Simplicidad del código

**Cuándo usar:**
- Archivos pequeños a medianos (< 10MB)
- Operaciones no frecuentes
- Cuando la consistencia es más importante que la velocidad

### LINQ sobre Foreach
**Ventajas de LINQ:**
- ? Más declarativo y legible
- ? Menos propenso a errores
- ? Composable y reutilizable
- ? Lazy evaluation donde apropiado

```csharp
// ? Foreach tradicional
var resultado = new List<Pregunta>();
foreach (var pregunta in preguntas)
{
    if (pregunta.Id == 0)
    {
  pregunta.Id = idCounter++;
    }
    resultado.Add(pregunta);
}

// ? LINQ moderno
var resultado = preguntas
    .Select((pregunta, index) => pregunta.Id == 0
        ? pregunta with { Id = startId + index }
     : pregunta)
.ToList();
```

---

## ?? Dependency Injection

### Configuración
```csharp
var serviceProvider = DependencyInjection.ConfigurarServicios(rutaArchivo);
```

### Servicios registrados
- `IXmlDataAccess` ? `XmlLinqDataAccess` (LINQ to XML)
- `IJuegoRepository` ? `JuegoRepository` (carga bajo demanda)
- `IJuegoService` ? `JuegoService` (validaciones con LINQ)

---

## ? Validaciones Implementadas

### Juego
- ? Nombre: Requerido, mínimo 3 caracteres
- ? Descripción: Requerida
- ? Fecha de creación: **Automática e inmutable** - se establece al crear
- ? Cantidad de preguntas: Calculada automáticamente
- ? ID: Generado automáticamente, inmutable

### Pregunta
- ? Texto: Requerido
- ? Categoría: Requerida (Historia, Geografia, Ciencia, Arte, Entretenimiento, Deportes)
- ? Dificultad: Requerida (Fácil, Media, Difícil)
- ? Opciones: Mínimo 2 requeridas, todas con texto
- ? Respuesta correcta: Debe corresponder a una opción válida
- ? ID: Generado automáticamente, inmutable

### Opción
- ? Texto: Requerido
- ? ID: Generado automáticamente, inmutable

---

## ?? Patrones de Diseño Utilizados

### **Repository Pattern con Carga Bajo Demanda**
`JuegoRepository` proporciona:
- Carga bajo demanda del archivo XML
- Generación automática de IDs autoincrementales
- Operaciones CRUD con persistencia automática
- Sin caché en memoria para mayor simplicidad

### **Service Layer Pattern con LINQ**
`JuegoService` encapsula lógica de negocio:
- Validaciones centralizadas usando LINQ
- Separación de presentación y datos
- Reglas de negocio consistentes y funcionales

### **Dependency Injection Pattern**
Configurado en `APP/DependencyInjection`:
- Inversión de control
- Facilita testing
- Desacoplamiento de componentes

### **Immutable Domain Pattern**
Usando records de C# 12:
- IDs inmutables (init-only)
- FechaCreacion inmutable con valor por defecto
- With expressions para actualizaciones
- Value equality
- Thread-safe por diseño

---

## ?? Diagramas

### Diagrama de Arquitectura
Ver: `UaiDasTp5Ej4/Docs/Arquitectura.puml`

### Diagrama de Flujo de Operaciones
Ver: `UaiDasTp5Ej4/Docs/Flujo_Operaciones.puml`

**Nota**: Los archivos `.puml` se pueden visualizar con:
- [PlantUML](https://plantuml.com/)
- Extensión VS Code: "PlantUML"
- Online: [PlantText](https://www.planttext.com/)

---

## ?? Características Técnicas de C# 12

### Records
```csharp
// Sintaxis concisa con comportamiento inmutable
public record Juego
{
    public int Id { get; init; }  // Init-only
    public DateTime FechaCreacion { get; init; } = DateTime.Now;  // Init-only con default
    public string Nombre { get; set; } = string.Empty;
}
```

### With Expressions
```csharp
// Crear copias con propiedades modificadas (las init-only se mantienen)
var juegoActualizado = juegoOriginal with { Nombre = "Nuevo nombre" };
// Id y FechaCreacion se copian automáticamente sin cambios
```

### Init-Only Properties
```csharp
// Propiedades que solo se pueden establecer en inicialización
public int Id { get; init; }  // Solo en construcción o inicializador de objeto
public DateTime FechaCreacion { get; init; } = DateTime.Now;  // Con valor por defecto
```

### Constructores Primarios
```csharp
// Sintaxis concisa y clara
public class JuegoService(IJuegoRepository juegoRepository) : IJuegoService
{
    // Parámetro disponible en toda la clase
}
```

### Collection Expressions
```csharp
// Sintaxis moderna para colecciones vacías
public List<Pregunta> Preguntas { get; set; } = [];
return preguntas ?? [];
```

---

## ?? Resumen de Cumplimiento

? ABM completo en XML  
? Arquitectura de N Capas (Clean Architecture)  
? .NET 8 + Windows Forms + C# 12
? Principios SOLID aplicados  
? Clean Code (records, init-only, with expressions, LINQ)  
? DRY, YAGNI, KISS  
? Dependency Injection  
? LINQ to XML  
? DTD y validaciones  
? **Records con IDs inmutables (init-only)** ?  
? **FechaCreacion inmutable con valor por defecto** ?  
? **IDs autoincrementales** ?  
? **Carga bajo demanda (no caché)** ?  
? **LINQ en lugar de foreach** ?  
? Documentación consolidada  
? Diagramas PlantUML  
? Entidades separadas  
? Formularios en carpeta UI  
? Archivos XML y DTD organizados  

**? Proyecto completamente moderno con C# 12 e inmutabilidad total ?**

---

## ????? Autor

**Trabajo Práctico 5** - Desarrollo y Arquitectura de Software  
**Universidad Abierta Interamericana (UAI)**  
**Clase 8**

---

## ?? Licencia

Proyecto académico - Universidad Abierta Interamericana
