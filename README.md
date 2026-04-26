 ECONEST — GESTOR DE CABAÑAS ECOTURÍSTICAS
  
  Descripcion del proyecto.
 
EcoNest es un sistema de gestión de reservas para hospedajes
ecoturísticos. Su objetivo principal es centralizar la
administración de cabañas, huéspedes, reservas, temporadas
y mantenimientos en una sola plataforma.
 
Funcionalidades principales:
  - Registrar y administrar cabañas con su estado y capacidad
  - Gestionar huéspedes y su historial de reservas
  - Crear reservas vinculando huésped, cabaña y temporada
  - Calcular costos automáticamente según la temporada activa
  - Registrar y controlar órdenes de mantenimiento
  - Listar servicios disponibles por cabaña o reserva
 
Objetivos:
  - Evitar conflictos de disponibilidad entre reservas
  - Mantener trazabilidad del estado de cada cabaña
  - Permitir escalar el sistema con nuevos módulos futuros
    (facturación, reportes, app móvil)
 
 

  1. DISEÑO PRELIMINAR — DIAGRAMA DE CLASES

<img width="1171" height="824" alt="DiagramClass-EcoNest drawio" src="https://github.com/user-attachments/assets/3841d2d4-1178-4b8d-9190-6c02c3d38e63" />

 
[BaseEntity] (abstracta)
  + Id: int
  + CreatedAt: DateTime
  + UpdatedAt: DateTime?
  + IsActive: bool
        |
        |── [Person] (abstracta)
        |     + Name: string
        |     + Surname: string
        |     + Email: string
        |     + PhoneNumber: string
        |           |
        |           └── [Guest]
        |                 + DocumentId: string
        |                 + Address: string
        |                 + Nationality: string
        |                 + Reservations: List<Reservation>
        |
        |── [Cabin]
        |     + Name: string
        |     + Location: string
        |     + Capacity: int
        |     + Description: string
        |     + State: CabinStatus
        |     + Reservations: List<Reservation>
        |     + Maintenances: List<Maintenance>
        |
        |── [Season]
        |     + Name: string
        |     + StartDate: DateTime
        |     + EndDate: DateTime
        |     + PriceMultiplier: decimal
        |     + Reservations: List<Reservation>
        |
        |── [Reservation]
        |     + CabinId: int
        |     + GuestId: int
        |     + SeasonId: int
        |     + CheckInDate: DateTime
        |     + CheckOutDate: DateTime
        |     + Status: ReservationStatus
        |     + CostPerNight: decimal
        |     + TotalCost: decimal
        |     + Cabin: Cabin
        |     + Guest: Guest
        |     + Season: Season
        |
        |── [Maintenance]
        |     + CabinId: int
        |     + Description: string
        |     + StartDate: DateTime
        |     + EndDate: DateTime
        |     + Status: MaintenanceStatus
        |     + Cabin: Cabin
        |
        |── [Service]
        |     + Name: string
        |     + Description: string
        |     + Price: decimal
        |
        |── [Payment]
        |     + ReservationId: int
        |     + PaymentDate: DateTime
        |     + Status: PaymentStatus
        |     + Amount: decimal
        |     + PaymentMethod: PaymentMethod
        |
        └── [Observation]
              + ReservationId: int
              + CabinId: int?
              + Comment: string
 
Enums:
  CabinStatus:        Available | Occupied | Maintenance
  ReservationStatus:  Reserved | Confirmed | Cancelled | Completed
  MaintenanceStatus:  Scheduled | InProgress | Completed
  PaymentStatus:      Pending | Completed | Failed
  PaymentMethod:      CreditCard | DebitCard | Cash | Online
 
Relaciones:
  Guest        1 ---> * Reservation
  Cabin        1 ---> * Reservation
  Cabin        1 ---> * Maintenance
  Season       1 ---> * Reservation
  Reservation  1 ---> * Observation
  Reservation  1 ---> * Payment
  Reservation  * ---> * Service  (tabla pivote: ReservationService)

  
