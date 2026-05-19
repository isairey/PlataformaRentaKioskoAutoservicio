<div align="center">

<img width="220" src="https://cdn-icons-png.flaticon.com/512/3081/3081559.png" />

# 🛠️ 2C Rentals — Equipment Rental System

### Plataforma de renta de equipos con kiosco autoservicio y panel administrativo ⚡

<p align="center">
  <b>2C Rentals</b> es un sistema de escritorio desarrollado con VB.NET y Windows Forms para la administración de renta de equipos, reservas, inventario y operaciones administrativas en tiempo real.
</p>

<p align="center">
  <img src="https://img.shields.io/badge/VB.NET-Windows_App-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
  <img src="https://img.shields.io/badge/Windows_Forms-Desktop_UI-0078D6?style=for-the-badge&logo=windows&logoColor=white">
  <img src="https://img.shields.io/badge/MySQL-Database-4479A1?style=for-the-badge&logo=mysql&logoColor=white">
  <img src="https://img.shields.io/badge/.NET-10.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white">
</p>

<p align="center">
  <a href="#-acerca-del-proyecto">Acerca</a> •
  <a href="#-características">Características</a> •
  <a href="#-tecnologías-utilizadas">Tecnologías</a> •
  <a href="#-instalación">Instalación</a> •
  <a href="#-vista-previa">Vista previa</a>
</p>

</div>

---

# 🌌 Acerca del proyecto

**2C Rentals** es un sistema moderno de administración de renta de equipos diseñado para pequeñas empresas que necesitan automatizar reservas, control de inventario y administración de clientes.

El proyecto fue construido utilizando:

- 🖥️ Windows Forms
- ⚡ VB.NET
- 🗄️ MySQL
- 🔐 Seguridad SHA-256
- 📦 Arquitectura por capas
- 🛠️ Sistema CRUD administrativo

El sistema incluye:

- 🛒 Kiosco autoservicio para clientes
- 👨‍💼 Portal administrativo protegido
- 📋 Gestión completa de inventario
- 📅 Reservas automáticas
- 🔄 Restauración de stock
- 🔐 Autenticación segura

---

# ✨ Características

## 🛒 Kiosco autoservicio

- 📦 Navegación de equipos
- 🧾 Carrito de renta
- 📅 Selección de días
- ⚡ Reservas rápidas
- 👤 Registro de clientes
- 🖥️ Interfaz táctil amigable

---

## 👨‍💼 Panel administrativo

- 📊 Dashboard administrativo
- 📋 Gestión de reservas
- 🔄 Retorno y cancelación
- 📈 Estadísticas en tiempo real
- 📦 Administración de inventario
- 🛠️ CRUD completo de equipos

---

## 🔐 Seguridad

- 🔑 Autenticación administrativa
- 🔒 Contraseñas SHA-256
- 🛡️ Protección de accesos
- ⚡ Validaciones seguras
- 🗄️ Consultas SQL parametrizadas

---

## 📦 Gestión de inventario

- ➕ Registro de equipos
- 📋 Edición de productos
- ❌ Soft delete
- 📊 Control de stock
- 💰 Tarifas por día
- 📅 Disponibilidad dinámica

---

# 👨‍💻 Módulos del sistema

## 🛒 Customer Kiosk Module

Módulo de autoservicio para clientes.

### Funcionalidades:

- 📦 Explorar equipos
- ➕ Agregar al carrito
- 📅 Configurar días de renta
- 🧾 Checkout
- 🔖 Generación automática de booking code

---

## 👨‍💼 Admin Dashboard Module

Módulo administrativo principal.

### Funcionalidades:

- 📊 Estadísticas generales
- 📋 Gestión de reservas
- 🔄 Retorno de equipos
- ❌ Cancelaciones
- ⚡ Actualización de estados

---

## 📦 Equipment Management Module

Módulo de inventario.

### Funcionalidades:

- ➕ Agregar equipos
- ✏️ Editar equipos
- 🗑️ Desactivar productos
- 📊 Gestión de stock
- 💰 Tarifas dinámicas

---

## 🔐 Authentication Module

Sistema de autenticación.

### Funcionalidades:

- 🔑 Login administrativo
- 🔒 Hash SHA-256
- 🛡️ Validación segura
- 👨‍💼 Gestión de administradores

---

# 🛠️ Tecnologías utilizadas

## ⚙️ Backend / Desktop

<p>
  <img src="https://skillicons.dev/icons?i=dotnet,windows" />
</p>

- VB.NET
- .NET 10
- Windows Forms
- Arquitectura por capas
- Desktop Development

---

## 🗄️ Base de datos

<p>
  <img src="https://skillicons.dev/icons?i=mysql" />
</p>

- MySQL 8+
- Persistencia relacional
- Transacciones SQL
- Gestión de inventario
- Relaciones FK

---

## 🔐 Seguridad

<p>
  <img src="https://skillicons.dev/icons?i=github" />
</p>

- SHA-256 Hashing
- Validación segura
- SQL parametrizado
- Protección de credenciales

---

## 🧰 Herramientas

<p>
  <img src="https://skillicons.dev/icons?i=git,github,vscode,visualstudio" />
</p>

- Git
- GitHub
- Visual Studio
- NuGet
- Windows SDK

---

# 📂 Estructura del proyecto

```bash
PlataformaRentaKioskoAutoservicio/
│
├── ERS.slnx
├── ERS/
│   ├── App.config
│   ├── setup_database.sql
│   ├── DBConnection.vb
│   ├── RentalManager.vb
│   ├── AdminManager.vb
│   ├── HashHelper.vb
│   ├── EquipmentItem.vb
│   ├── CartItem.vb
│   │
│   ├── FrmKiosk.vb
│   ├── FrmCheckout.vb
│   ├── FrmConfirmation.vb
│   │
│   ├── FrmAdminLogin.vb
│   ├── FrmAdminDashboard.vb
│   ├── FrmManageEquipment.vb
│   │
│   └── My Project/
│
├── README.md
└── LICENSE
```

---

# 🏗️ Arquitectura del sistema

El proyecto utiliza una arquitectura por capas donde:

- 🖥️ Las interfaces WinForms manejan la UI
- ⚡ Los servicios administran lógica de negocio
- 🗄️ MySQL almacena los datos
- 🔐 HashHelper protege credenciales
- 📦 Los modelos representan entidades

---

# 🔄 Flujo del sistema

## 🛒 Reserva de cliente

```text
Cliente → Kiosco → Carrito → Checkout → MySQL → Confirmación
```

---

## 👨‍💼 Operación administrativa

```text
Admin → Login → Dashboard → Gestión → Actualización MySQL
```

---

# ⚡ Instalación

## 📋 Requisitos

- Windows 10/11
- .NET SDK 10+
- MySQL 8+
- Visual Studio
- Git

---

# 🚀 Configuración del proyecto

## 1️⃣ Clonar repositorio

```bash
git clone https://github.com/isairey/PlataformaRentaKioskoAutoservicio.git
```

---

## 2️⃣ Entrar al proyecto

```bash
cd PlataformaRentaKioskoAutoservicio
```

---

## 3️⃣ Crear base de datos

```bash
mysql -u YOUR_MYSQL_USER -p < ERS/setup_database.sql
```

---

## 4️⃣ Configurar App.config

```xml
<connectionStrings>
  <add name="TwoCRentals"
       connectionString="Server=localhost;Database=twoc_rentals_db;Uid=YOUR_USER;Pwd=YOUR_PASSWORD;"
       providerName="MySql.Data.MySqlClient" />
</connectionStrings>
```

---

## 5️⃣ Restaurar paquetes

```bash
dotnet restore
```

---

## 6️⃣ Ejecutar proyecto

```bash
dotnet run --project ERS/ERS.vbproj
```

---

# 📊 Funcionalidades principales

## 📦 Inventario

- Gestión de equipos
- Disponibilidad dinámica
- Tarifas por día
- Soft delete

---

## 📅 Reservas

- Booking automático
- Control de fechas
- Generación de códigos
- Transacciones atómicas

---

## 🔐 Seguridad

- Login seguro
- SHA-256
- SQL parametrizado
- Protección administrativa

---

## 📈 Dashboard

- Reservas activas
- Equipos vencidos
- Estadísticas
- Gestión rápida

---

# 🗄️ Base de datos

## 📦 Tablas principales

```text
customers
equipment
rentals
rental_details
admins
```

---

## 🔄 Estados de renta

```text
Active → Overdue → Returned / Cancelled
```

---

# 👨‍💼 Credenciales por defecto

| Campo | Valor |
|------|------|
| Usuario | `admin` |
| Password | Configurado en `setup_database.sql` |

⚠️ Se recomienda cambiar la contraseña después del primer inicio de sesión.

---

# 🧪 Testing y validación

## 🔍 Casos probados

- ✔️ Reservas simultáneas
- ✔️ Restauración de stock
- ✔️ Login administrativo
- ✔️ Gestión CRUD
- ✔️ Validación SQL
- ✔️ Control de disponibilidad

---

# 📸 Vista previa

## 🖥️ Interfaces del sistema

<div align="center">

### 🛒 Kiosco de renta
![Kiosk](https://images.unsplash.com/photo-1556740749-887f6717d7e4?q=80&w=1200)

### 📦 Gestión de equipos
![Inventory](https://images.unsplash.com/photo-1517048676732-d65bc937f952?q=80&w=1200)

### 👨‍💼 Dashboard administrativo
![Dashboard](https://images.unsplash.com/photo-1551288049-bebda4e38f71?q=80&w=1200)

### 🗄️ Base de datos y sistema
![Database](https://images.unsplash.com/photo-1544383835-bda2bc66a55d?q=80&w=1200)

</div>

---

# 🧠 Objetivos del proyecto

## 🎯 Aprendizaje y desarrollo

- Desarrollo desktop moderno
- Arquitectura por capas
- Integración MySQL
- Gestión de inventarios
- Sistemas CRUD
- Seguridad en aplicaciones
- Diseño WinForms profesional

---

# 🚧 Roadmap

## 🔮 Próximas mejoras

- ☁️ Sincronización cloud
- 📱 Aplicación móvil
- 💳 Sistema de pagos
- 📊 Reportes PDF
- 📧 Notificaciones email
- 🌐 API REST
- 🤖 Recomendaciones inteligentes

---

# 🤝 Contribuciones

Las contribuciones son bienvenidas ❤️

## Cómo contribuir

1. Fork del proyecto

```bash
git checkout -b feature/nueva-funcionalidad
```

2. Commit

```bash
git commit -m "✨ Nueva funcionalidad"
```

3. Push

```bash
git push origin feature/nueva-funcionalidad
```

4. Pull Request 🚀

---

# 👨‍💻 Desarrollador

<div align="center">

## Isai Reyes — Full Stack Developer

Desarrollador apasionado por aplicaciones de escritorio, sistemas administrativos y arquitecturas modernas 🚀

</div>

---

# 🌟 Apoya el proyecto

⭐ Dale una estrella  
🍴 Haz fork  
📢 Comparte el proyecto

---

# 📜 Licencia

Proyecto orientado al aprendizaje y desarrollo de sistemas administrativos modernos con VB.NET y MySQL.

---

<div align="center">

### 🛠️ 2C Rentals — gestión moderna de renta de equipos ⚡

</div>
