# CHANGELOG

Todos los cambios notables del proyecto están documentados en este archivo.

El formato está basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/).

---

## [2.2.0] - 2025-01-08

### ?? Enum de Categoría y FechaCreacion Automática

#### Added
- **Enum `Categoria`**: Nuevo tipo para garantizar valores finitos y controlados
  ```csharp
  public enum Categoria
  {
      Historia,
      Geografia,
      Ciencia,
      Arte,
      Entretenimiento,
    Deportes
  }
  ```

#### Changed
- **BREAKING CHANGE**: `Pregunta.Categoria` cambió de `string` a `enum Categoria`
  - Valores controlados y finitos
  - Type-safe: Error de compilación si se usa valor inválido
  - Mejor IntelliSense y autocompletado
  - No requiere validación - el compilador garantiza valores válidos

- **UI - FrmPreguntaDetalle**: ComboBox de categoría ahora carga desde el enum
  ```csharp
  // Antes
  cboCategoria.Items.AddRange(new[] { "Historia", "Geografia", ... });
  
  // Ahora
  cboCategoria.DataSource = Enum.GetValues(typeof(Categoria));
  ```

- **XmlLinqDataAccess**: Serialización/deserialización de enum
  ```csharp
  // Guardar: Categoria.ToString()
  new XElement("Categoria", p.Categoria.ToString())
  
  // Cargar: Enum.TryParse
  Categoria = Enum.TryParse<Categoria>(value, out var cat) 
      ? cat 
      : Categoria.Historia
  ```

#### Removed
- **UI - FrmJuegoABM**: Eliminado `DateTimePicker` del formulario
  - La fecha de creación ya no es editable por el usuario
  - Se establece automáticamente al crear el juego
 - Campo y label relacionados eliminados del diseñador
  - Simplifica la interfaz de usuario

- **Validaciones**: Removida validación de categoría en `JuegoService`
  - Ya no es necesaria porque el enum garantiza valores válidos
  - Reduce código innecesario

### Behavior

#### FechaCreacion Automática
```csharp
// Usuario NO especifica fecha - se establece automáticamente
var nuevoJuego = new Juego
{
    Nombre = "Mi Juego",
    Descripcion = "Descripción"
    // FechaCreacion = DateTime.Now (automático por default)
};
```

#### Categoria Type-Safe
```csharp
// ? Correcto - valores controlados
var pregunta = new Pregunta
{
    Categoria = Categoria.Historia  // IntelliSense sugiere valores
};

// ? Error de compilación
var pregunta2 = new Pregunta
{
    Categoria = "Histria"  // Typo - error en tiempo de compilación
};
```

### Rationale

#### ¿Por qué Enum para Categoria?
1. **Type Safety**: Error de compilación si se usa valor inválido
2. **IntelliSense**: IDE sugiere valores disponibles
3. **No requiere validación**: El compilador garantiza valores válidos
4. **Consistencia**: Mismo valor siempre (sin variaciones de mayúsculas, typos)
5. **Refactoring seguro**: Si cambia el nombre, el compilador avisa
6. **Mejor rendimiento**: Comparación de int en lugar de string

#### ¿Por qué FechaCreacion Automática?
1. **Integridad de Datos**: Fecha siempre coincide con el momento real de creación
2. **Simplicidad UX**: Usuario no debe pensar en la fecha
3. **Menos Errores**: Usuario no puede ingresar fecha incorrecta
4. **UI Más Limpia**: Un control menos en el formulario
5. **Lógica de Negocio**: La fecha es un metadato del sistema, no input del usuario

### Migration Guide

#### De string a Enum
```csharp
// Código viejo (v2.1.0)
var pregunta = new Pregunta
{
    Categoria = "Historia"  // string
};

// Código nuevo (v2.2.0)
var pregunta = new Pregunta
{
    Categoria = Categoria.Historia  // enum
};
```

#### Actualizar XMLs existentes
Los XMLs con categorías en string se parsean automáticamente:
```xml
<Categoria>Historia</Categoria>  <!-- Se convierte a Categoria.Historia -->
```

Si el valor no es válido, se usa default (Historia):
```xml
<Categoria>InvalidCategory</Categoria>  <!-- Se convierte a Categoria.Historia -->
```

---

## [2.1.0] - 2025-01-08

### ?? FechaCreacion Inmutable

#### Changed
- **BREAKING CHANGE**: `FechaCreacion` ahora es inmutable (init-only property)
  - Solo se puede establecer al crear un nuevo juego
  - No se puede modificar en actualizaciones
  - Tiene valor por defecto: `DateTime.Now`
  
```csharp
// Antes
public DateTime FechaCreacion { get; set; }

// Ahora
public DateTime FechaCreacion { get; init; } = DateTime.Now;
```

#### UI Changes
- **FrmJuegoABM**: DateTimePicker se deshabilita al editar juegos existentes
  - `dtpFechaCreacion.Enabled = false` cuando se selecciona un juego
- `dtpFechaCreacion.Enabled = true` solo para nuevos juegos
  - Al actualizar, se usa `with` expression que mantiene FechaCreacion original

#### Behavior
```csharp
// Crear nuevo juego
var nuevoJuego = new Juego
{
    Id = 0,
    Nombre = "Test",
    FechaCreacion = DateTime.Now  // Se puede establecer
};

// Actualizar juego existente
var juegoActualizado = juegoExistente with
{
    Nombre = "Nuevo nombre"
  // FechaCreacion se mantiene automáticamente (init-only)
};
```

### Rationale

**¿Por qué este cambio?**
- La fecha de creación es un metadato que no debería cambiar
- Representa cuándo se creó originalmente el juego
- Modificarla falsearía el historial
- Sigue principio de inmutabilidad para datos históricos

**Ventajas:**
- ? Mayor integridad de datos
- ? Previene modificaciones accidentales o maliciosas
- ? Más fácil auditar creación de juegos
- ? Consistente con inmutabilidad de IDs

---

## [2.0.0] - 2025-01-08

### ?? Características Principales

#### ? Records e Inmutabilidad
- **BREAKING CHANGE**: Convertidas todas las entidades del dominio a `record` types
  - `Juego`, `Pregunta`, `Opcion` ahora son records
  - Soportan `with` expressions para crear copias inmutables
  - Value equality por defecto
  - Mejor para programación funcional

#### ?? IDs Inmutables
- **BREAKING CHANGE**: Todos los IDs ahora usan `init` accessor
  - `public int Id { get; init; }` - Solo se puede asignar en construcción
  - No se pueden modificar después de la inicialización
  - Mayor seguridad y prevención de errores
  - Alineado con principios de inmutabilidad

#### ?? IDs Autoincrementales
- **ADDED**: Sistema de generación automática de IDs
  - IDs de juegos: autoincrementales basados en máximo existente
  - IDs de preguntas: autoincrementales globales (considerando todos los juegos)
  - IDs de opciones: autoincrementales por pregunta (1, 2, 3...)
  - Lógica centralizada en `JuegoRepository`

#### ?? Carga Bajo Demanda (No Caché)
- **BREAKING CHANGE**: Eliminada caché en memoria del repositorio
  - `JuegoRepository` ahora carga datos del archivo en cada operación
  - **Ventajas**:
    - Siempre datos actualizados
    - Menor uso de memoria
    - Código más simple
    - Sin problemas de sincronización
  - Cada operación persiste automáticamente
  - Método `Guardar()` mantenido por compatibilidad (ahora vacío)

#### ?? LINQ sobre Foreach
- **CHANGED**: Reemplazados loops tradicionales por LINQ
  - Validaciones ahora usan `FirstOrDefault`, `Any`, `All`
  - Transformaciones usan `Select` con expresiones lambda
  - Código más declarativo y funcional

### ?? Cambios en Archivos

#### Renamed
- **CAMBIOS_REALIZADOS.md** ? **CHANGELOG.md**
  - Nombre más estándar siguiendo convenciones
  - Formato mejorado con versionado semántico

#### Modified - Dominio (DOM/)
- `Juego.cs`: Convertido a record, Id con init, FechaCreacion con init y default
- `Pregunta.cs`: Convertido a record, Id con init, **Categoria ahora enum**
- `Opcion.cs`: Convertido a record, Id con init
- `Categoria.cs`: **Nuevo archivo - enum con categorías**

#### Modified - Repositorio (REPO/)
- `JuegoRepository.cs`:
  - Eliminada caché `_juegos` en memoria
  - Agregado `GenerarNuevoId()` con lógica autoincremental
  - Refactorizado `AsignarIdsPreguntasYOpciones()` con LINQ
  - IDs de preguntas consideran todos los juegos existentes
  - Cada operación CRUD persiste automáticamente

#### Modified - Servicio (SERV/)
- `JuegoService.cs`:
  - Removidas llamadas a `Guardar()` (ahora automático)
  - Validaciones refactorizadas con LINQ
  - **Removida validación de categoría (enum la garantiza)**

#### Modified - Context (CONTEXT/)
- `XmlLinqDataAccess.cs`:
  - **Serialización de enum Categoria a string**
  - **Deserialización con Enum.TryParse**
  - Fallback a Categoria.Historia si valor inválido

#### Modified - Presentación (UI/)
- `FrmJuegoABM.cs`:
  - **Eliminado uso de DateTimePicker**
  - FechaCreacion automática
  - UI simplificada
- `FrmJuegoABM.Designer.cs`:
  - **Removido DateTimePicker y label asociado**
  - Reorganizados controles
- `FrmPreguntaDetalle.cs`:
  - **ComboBox de categoría carga desde enum**
  - Cast de SelectedItem a Categoria

---

## Tipos de Cambios

- **ADDED**: Nuevas características
- **CHANGED**: Cambios en funcionalidad existente
- **DEPRECATED**: Características obsoletas
- **REMOVED**: Características eliminadas
- **FIXED**: Corrección de bugs
- **SECURITY**: Correcciones de seguridad
- **BREAKING CHANGE**: Cambios que rompen compatibilidad

---

## Próximas Versiones (Roadmap)

### [3.0.0] - Futuro
- [ ] Unit Tests con xUnit
- [ ] Logging con Serilog
- [ ] Validación de DTD automática
- [ ] Async/await para operaciones I/O
- [ ] Soporte para múltiples formatos (JSON, SQLite)
- [ ] Enum para Dificultad (similar a Categoria)

---

**Universidad Abierta Interamericana (UAI)**  
**Desarrollo y Arquitectura de Software - TP 5**
