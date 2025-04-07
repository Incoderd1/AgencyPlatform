using System;
using System.Collections.Generic;
using AgencyPlatform.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Data;

public partial class AgencyPlatformDbContext : DbContext
{
    public AgencyPlatformDbContext()
    {
    }

    public AgencyPlatformDbContext(DbContextOptions<AgencyPlatformDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccionesAntifrauide> AccionesAntifrauides { get; set; }

    public virtual DbSet<ActividadPerfile> ActividadPerfiles { get; set; }

    public virtual DbSet<Agencia> Agencias { get; set; }

    public virtual DbSet<CacheConsulta> CacheConsultas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ConfigSharding> ConfigShardings { get; set; }

    public virtual DbSet<ConfiguracionSistema> ConfiguracionSistemas { get; set; }

    public virtual DbSet<ContactosPerfil> ContactosPerfils { get; set; }

    public virtual DbSet<Cupone> Cupones { get; set; }

    public virtual DbSet<CuponesCliente> CuponesClientes { get; set; }

    public virtual DbSet<DispositivosUsuario> DispositivosUsuarios { get; set; }

    public virtual DbSet<EstadisticasRendimiento> EstadisticasRendimientos { get; set; }

    public virtual DbSet<FeedbackInterno> FeedbackInternos { get; set; }

    public virtual DbSet<HistorialAcceso> HistorialAccesos { get; set; }

    public virtual DbSet<HistorialAccesoActual> HistorialAccesoActuals { get; set; }

    public virtual DbSet<HistorialAccesoArchivo> HistorialAccesoArchivos { get; set; }

    public virtual DbSet<HistorialAccesoPasado> HistorialAccesoPasados { get; set; }

    public virtual DbSet<HistorialConfiguracion> HistorialConfiguracions { get; set; }

    public virtual DbSet<HistorialVerificacione> HistorialVerificaciones { get; set; }

    public virtual DbSet<ImagenesPerfil> ImagenesPerfils { get; set; }

    public virtual DbSet<LogsSistema> LogsSistemas { get; set; }

    public virtual DbSet<LogsSistemaActual> LogsSistemaActuals { get; set; }

    public virtual DbSet<LogsSistemaAntiguo> LogsSistemaAntiguos { get; set; }

    public virtual DbSet<LogsSistemaArchivo> LogsSistemaArchivos { get; set; }

    public virtual DbSet<LogsSistemaReciente> LogsSistemaRecientes { get; set; }

    public virtual DbSet<MembresiasVip> MembresiasVips { get; set; }

    public virtual DbSet<MetricasPerfil> MetricasPerfils { get; set; }

    public virtual DbSet<PaquetesCupone> PaquetesCupones { get; set; }

    public virtual DbSet<Perfile> Perfiles { get; set; }

    public virtual DbSet<ProcesamientoImagene> ProcesamientoImagenes { get; set; }

    public virtual DbSet<Punto> Puntos { get; set; }

    public virtual DbSet<ResumenHistoricoVisita> ResumenHistoricoVisitas { get; set; }

    public virtual DbSet<SuscripcionesVip> SuscripcionesVips { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Verificacione> Verificaciones { get; set; }

    public virtual DbSet<VisitasPerfil> VisitasPerfils { get; set; }

    public virtual DbSet<VisitasPerfilActual> VisitasPerfilActuals { get; set; }

    public virtual DbSet<VisitasPerfilAntiguo> VisitasPerfilAntiguos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AgenciaCitas;Username=postgres;Password=C1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("estado_cupon_cliente", new[] { "disponible", "usado", "expirado", "cancelado" });

        modelBuilder.Entity<AccionesAntifrauide>(entity =>
        {
            entity.HasKey(e => e.IdAccion).HasName("acciones_antifrauide_pkey");

            entity.ToTable("acciones_antifrauide");

            entity.HasIndex(e => new { e.TipoEntidad, e.IdEntidad }, "idx_antifrauide_entidad");

            entity.HasIndex(e => e.Estado, "idx_antifrauide_estado");

            entity.HasIndex(e => e.TipoAccion, "idx_antifrauide_tipo");

            entity.Property(e => e.IdAccion).HasColumnName("id_accion");
            entity.Property(e => e.EjecutadaPor).HasColumnName("ejecutada_por");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activa'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Evidencia)
                .HasColumnType("jsonb")
                .HasColumnName("evidencia");
            entity.Property(e => e.FechaAccion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_accion");
            entity.Property(e => e.FechaDeteccion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_deteccion");
            entity.Property(e => e.FechaResolucion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_resolucion");
            entity.Property(e => e.IdEntidad).HasColumnName("id_entidad");
            entity.Property(e => e.IpRelacionada)
                .HasMaxLength(45)
                .HasColumnName("ip_relacionada");
            entity.Property(e => e.Motivo).HasColumnName("motivo");
            entity.Property(e => e.TipoAccion)
                .HasMaxLength(25)
                .HasColumnName("tipo_accion");
            entity.Property(e => e.TipoEntidad)
                .HasMaxLength(10)
                .HasColumnName("tipo_entidad");

            entity.HasOne(d => d.EjecutadaPorNavigation).WithMany(p => p.AccionesAntifrauides)
                .HasForeignKey(d => d.EjecutadaPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("acciones_antifrauide_ejecutada_por_fkey");
        });

        modelBuilder.Entity<ActividadPerfile>(entity =>
        {
            entity.HasKey(e => e.IdActividad).HasName("actividad_perfiles_pkey");

            entity.ToTable("actividad_perfiles");

            entity.HasIndex(e => new { e.FechaInicio, e.FechaFin }, "idx_actividad_fechas");

            entity.HasIndex(e => e.IdPerfil, "idx_actividad_perfil");

            entity.HasIndex(e => e.TipoActividad, "idx_actividad_tipo");

            entity.Property(e => e.IdActividad).HasColumnName("id_actividad");
            entity.Property(e => e.FechaFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IpRegistro)
                .HasMaxLength(45)
                .HasColumnName("ip_registro");
            entity.Property(e => e.Localizacion)
                .HasMaxLength(100)
                .HasColumnName("localizacion");
            entity.Property(e => e.Notas).HasColumnName("notas");
            entity.Property(e => e.TipoActividad)
                .HasMaxLength(15)
                .HasColumnName("tipo_actividad");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.ActividadPerfiles)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("actividad_perfiles_id_perfil_fkey");
        });

        modelBuilder.Entity<Agencia>(entity =>
        {
            entity.HasKey(e => e.IdAgencia).HasName("agencias_pkey");

            entity.ToTable("agencias");

            entity.HasIndex(e => e.Estado, "idx_agencias_estado");

            entity.HasIndex(e => e.NombreComercial, "idx_agencias_nombre");

            entity.HasIndex(e => new { e.Pais, e.Region, e.Ciudad }, "idx_agencias_ubicacion");

            entity.HasIndex(e => e.Verificada, "idx_agencias_verificada");

            entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(100)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(20)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.DocumentoVerificacion)
                .HasMaxLength(255)
                .HasColumnName("documento_verificacion");
            entity.Property(e => e.EmailContacto)
                .HasMaxLength(100)
                .HasColumnName("email_contacto");
            entity.Property(e => e.Estado)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pendiente_verificacion'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FechaVerificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_verificacion");
            entity.Property(e => e.Horario)
                .HasColumnType("jsonb")
                .HasColumnName("horario");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .HasColumnName("logo_url");
            entity.Property(e => e.NifCif)
                .HasMaxLength(20)
                .HasColumnName("nif_cif");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(100)
                .HasColumnName("nombre_comercial");
            entity.Property(e => e.NumPerfilesActivos)
                .HasDefaultValue(0)
                .HasColumnName("num_perfiles_activos");
            entity.Property(e => e.Pais)
                .HasMaxLength(100)
                .HasColumnName("pais");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(150)
                .HasColumnName("razon_social");
            entity.Property(e => e.Region)
                .HasMaxLength(100)
                .HasColumnName("region");
            entity.Property(e => e.SitioWeb)
                .HasMaxLength(200)
                .HasColumnName("sitio_web");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.Verificada)
                .HasDefaultValue(false)
                .HasColumnName("verificada");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Agencia)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("agencias_id_usuario_fkey");
        });

        modelBuilder.Entity<CacheConsulta>(entity =>
        {
            entity.HasKey(e => e.IdCache).HasName("cache_consultas_pkey");

            entity.ToTable("cache_consultas");

            entity.HasIndex(e => e.ClaveCache, "idx_cache_clave");

            entity.HasIndex(e => e.FechaExpiracion, "idx_cache_expiracion");

            entity.HasIndex(e => e.TipoConsulta, "idx_cache_tipo");

            entity.Property(e => e.IdCache).HasColumnName("id_cache");
            entity.Property(e => e.ClaveCache)
                .HasMaxLength(255)
                .HasColumnName("clave_cache");
            entity.Property(e => e.Datos)
                .HasColumnType("jsonb")
                .HasColumnName("datos");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.ParametrosConsulta)
                .HasColumnType("jsonb")
                .HasColumnName("parametros_consulta");
            entity.Property(e => e.TiempoGeneracion).HasColumnName("tiempo_generacion");
            entity.Property(e => e.TipoConsulta)
                .HasMaxLength(50)
                .HasColumnName("tipo_consulta");
            entity.Property(e => e.UltimoUso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultimo_uso");
            entity.Property(e => e.VecesUtilizado)
                .HasDefaultValue(0)
                .HasColumnName("veces_utilizado");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.HasIndex(e => e.UltimaActividad, "idx_clientes_actividad");

            entity.HasIndex(e => e.Edad, "idx_clientes_edad");

            entity.HasIndex(e => e.FidelidadScore, "idx_clientes_fidelidad");

            entity.HasIndex(e => e.PuntosAcumulados, "idx_clientes_puntos");

            entity.HasIndex(e => new { e.EsVip, e.NivelVip }, "idx_clientes_vip");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Edad)
                .HasComputedColumnSql("calcular_edad(fecha_nacimiento)", true)
                .HasColumnName("edad");
            entity.Property(e => e.EsVip)
                .HasDefaultValue(false)
                .HasColumnName("es_vip");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaFinVip).HasColumnName("fecha_fin_vip");
            entity.Property(e => e.FechaInicioVip).HasColumnName("fecha_inicio_vip");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FidelidadScore)
                .HasDefaultValue(0)
                .HasColumnName("fidelidad_score");
            entity.Property(e => e.Genero)
                .HasMaxLength(20)
                .HasColumnName("genero");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Intereses)
                .HasColumnType("jsonb")
                .HasColumnName("intereses");
            entity.Property(e => e.NivelVip)
                .HasDefaultValue((short)0)
                .HasColumnName("nivel_vip");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.NumLogins)
                .HasDefaultValue(0)
                .HasColumnName("num_logins");
            entity.Property(e => e.OrigenRegistro)
                .HasMaxLength(50)
                .HasColumnName("origen_registro");
            entity.Property(e => e.Preferencias)
                .HasColumnType("jsonb")
                .HasColumnName("preferencias");
            entity.Property(e => e.PuntosAcumulados)
                .HasDefaultValue(0)
                .HasColumnName("puntos_acumulados");
            entity.Property(e => e.PuntosCaducados)
                .HasDefaultValue(0)
                .HasColumnName("puntos_caducados");
            entity.Property(e => e.PuntosGastados)
                .HasDefaultValue(0)
                .HasColumnName("puntos_gastados");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.UbicacionHabitual)
                .HasMaxLength(100)
                .HasColumnName("ubicacion_habitual");
            entity.Property(e => e.UltimaActividad)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultima_actividad");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("clientes_id_usuario_fkey");
        });

        modelBuilder.Entity<ConfigSharding>(entity =>
        {
            entity.HasKey(e => e.IdConfig).HasName("config_sharding_pkey");

            entity.ToTable("config_sharding");

            entity.HasIndex(e => e.Activo, "idx_sharding_activo");

            entity.HasIndex(e => e.Tabla, "uq_config_sharding_tabla").IsUnique();

            entity.Property(e => e.IdConfig).HasColumnName("id_config");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true)
                .HasColumnName("activo");
            entity.Property(e => e.CampoSharding)
                .HasMaxLength(50)
                .HasColumnName("campo_sharding");
            entity.Property(e => e.ConfiguracionShard)
                .HasColumnType("jsonb")
                .HasColumnName("configuracion_shard");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.MetodoSharding)
                .HasMaxLength(20)
                .HasColumnName("metodo_sharding");
            entity.Property(e => e.NumeroShards)
                .HasDefaultValue(1)
                .HasColumnName("numero_shards");
            entity.Property(e => e.ShardActual)
                .HasDefaultValue(0)
                .HasColumnName("shard_actual");
            entity.Property(e => e.Tabla)
                .HasMaxLength(50)
                .HasColumnName("tabla");
        });

        modelBuilder.Entity<ConfiguracionSistema>(entity =>
        {
            entity.HasKey(e => e.IdConfig).HasName("configuracion_sistema_pkey");

            entity.ToTable("configuracion_sistema");

            entity.HasIndex(e => e.Clave, "configuracion_sistema_clave_key").IsUnique();

            entity.HasIndex(e => e.NivelCache, "idx_config_cache");

            entity.HasIndex(e => e.Clave, "idx_config_clave");

            entity.HasIndex(e => new { e.Grupo, e.Entorno }, "idx_config_grupo_entorno");

            entity.Property(e => e.IdConfig).HasColumnName("id_config");
            entity.Property(e => e.ActualizadoPor).HasColumnName("actualizado_por");
            entity.Property(e => e.Clave)
                .HasMaxLength(50)
                .HasColumnName("clave");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Entorno)
                .HasMaxLength(15)
                .HasDefaultValueSql("'all'::character varying")
                .HasColumnName("entorno");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.Grupo)
                .HasMaxLength(50)
                .HasDefaultValueSql("'general'::character varying")
                .HasColumnName("grupo");
            entity.Property(e => e.Modificable)
                .HasDefaultValue(true)
                .HasColumnName("modificable");
            entity.Property(e => e.NivelCache)
                .HasMaxLength(15)
                .HasDefaultValueSql("'medium_term'::character varying")
                .HasColumnName("nivel_cache");
            entity.Property(e => e.TiempoExpiracionCache).HasColumnName("tiempo_expiracion_cache");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasDefaultValueSql("'text'::character varying")
                .HasColumnName("tipo");
            entity.Property(e => e.Valor).HasColumnName("valor");
            entity.Property(e => e.Version)
                .HasDefaultValue(1)
                .HasColumnName("version");

            entity.HasOne(d => d.ActualizadoPorNavigation).WithMany(p => p.ConfiguracionSistemas)
                .HasForeignKey(d => d.ActualizadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("configuracion_sistema_actualizado_por_fkey");
        });

        modelBuilder.Entity<ContactosPerfil>(entity =>
        {
            entity.HasKey(e => e.IdContacto).HasName("contactos_perfil_pkey");

            entity.ToTable("contactos_perfil");

            entity.HasIndex(e => e.IdCliente, "idx_contactos_cliente");

            entity.HasIndex(e => e.Estado, "idx_contactos_estado");

            entity.HasIndex(e => e.FechaContacto, "idx_contactos_fecha");

            entity.HasIndex(e => e.IdPerfil, "idx_contactos_perfil");

            entity.Property(e => e.IdContacto).HasColumnName("id_contacto");
            entity.Property(e => e.EmailContacto)
                .HasMaxLength(100)
                .HasColumnName("email_contacto");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaContacto)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_contacto");
            entity.Property(e => e.FechaRespuesta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_respuesta");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IpContacto)
                .HasMaxLength(45)
                .HasColumnName("ip_contacto");
            entity.Property(e => e.Mensaje).HasColumnName("mensaje");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(20)
                .HasColumnName("telefono_contacto");
            entity.Property(e => e.TipoContacto)
                .HasMaxLength(20)
                .HasColumnName("tipo_contacto");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.ContactosPerfils)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("contactos_perfil_id_cliente_fkey");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.ContactosPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("contactos_perfil_id_perfil_fkey");
        });

        modelBuilder.Entity<Cupone>(entity =>
        {
            entity.HasKey(e => e.IdCupon).HasName("cupones_pkey");

            entity.ToTable("cupones");

            entity.HasIndex(e => e.Codigo, "cupones_codigo_key").IsUnique();

            entity.HasIndex(e => e.Codigo, "idx_cupones_codigo");

            entity.HasIndex(e => e.Estado, "idx_cupones_estado");

            entity.HasIndex(e => new { e.FechaInicio, e.FechaFin }, "idx_cupones_fechas");

            entity.Property(e => e.IdCupon).HasColumnName("id_cupon");
            entity.Property(e => e.AplicaA)
                .HasMaxLength(20)
                .HasDefaultValueSql("'todo'::character varying")
                .HasColumnName("aplica_a");
            entity.Property(e => e.Codigo)
                .HasMaxLength(20)
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(15)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.MaximoDescuento)
                .HasPrecision(10, 2)
                .HasColumnName("maximo_descuento");
            entity.Property(e => e.MinimoCompra)
                .HasPrecision(10, 2)
                .HasColumnName("minimo_compra");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.RequierePuntos).HasColumnName("requiere_puntos");
            entity.Property(e => e.TipoDescuento)
                .HasMaxLength(20)
                .HasColumnName("tipo_descuento");
            entity.Property(e => e.UsosActuales)
                .HasDefaultValue(0)
                .HasColumnName("usos_actuales");
            entity.Property(e => e.UsosMaximos).HasColumnName("usos_maximos");
            entity.Property(e => e.UsosPorCliente)
                .HasDefaultValue(1)
                .HasColumnName("usos_por_cliente");
            entity.Property(e => e.Valor)
                .HasPrecision(10, 2)
                .HasColumnName("valor");
        });

        modelBuilder.Entity<CuponesCliente>(entity =>
        {
            entity.HasKey(e => e.IdCuponCliente).HasName("cupones_cliente_pkey");

            entity.ToTable("cupones_cliente");

            entity.HasIndex(e => e.IdCliente, "idx_cupones_cliente_cliente");

            entity.HasIndex(e => e.Estado, "idx_cupones_cliente_estado");

            entity.Property(e => e.IdCuponCliente).HasColumnName("id_cupon_cliente");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'disponible'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaAsignacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.FechaUso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_uso");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdCupon).HasColumnName("id_cupon");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_transaccion");
            entity.Property(e => e.PuntosCanjeados).HasColumnName("puntos_canjeados");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.CuponesClientes)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("cupones_cliente_id_cliente_fkey");

            entity.HasOne(d => d.IdCuponNavigation).WithMany(p => p.CuponesClientes)
                .HasForeignKey(d => d.IdCupon)
                .HasConstraintName("cupones_cliente_id_cupon_fkey");
        });

        modelBuilder.Entity<DispositivosUsuario>(entity =>
        {
            entity.HasKey(e => e.IdDispositivo).HasName("dispositivos_usuario_pkey");

            entity.ToTable("dispositivos_usuario");

            entity.HasIndex(e => e.Estado, "idx_dispositivos_estado");

            entity.HasIndex(e => e.TokenDispositivo, "idx_dispositivos_token");

            entity.HasIndex(e => e.IdUsuario, "idx_dispositivos_usuario");

            entity.Property(e => e.IdDispositivo).HasColumnName("id_dispositivo");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FcmToken)
                .HasMaxLength(255)
                .HasColumnName("fcm_token");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.IpUltimaActividad)
                .HasMaxLength(45)
                .HasColumnName("ip_ultima_actividad");
            entity.Property(e => e.Navegador)
                .HasMaxLength(50)
                .HasColumnName("navegador");
            entity.Property(e => e.NombreDispositivo)
                .HasMaxLength(100)
                .HasColumnName("nombre_dispositivo");
            entity.Property(e => e.SistemaOperativo)
                .HasMaxLength(50)
                .HasColumnName("sistema_operativo");
            entity.Property(e => e.TipoDispositivo)
                .HasMaxLength(10)
                .HasColumnName("tipo_dispositivo");
            entity.Property(e => e.TokenDispositivo)
                .HasMaxLength(255)
                .HasColumnName("token_dispositivo");
            entity.Property(e => e.UltimaActividad)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultima_actividad");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DispositivosUsuarios)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("dispositivos_usuario_id_usuario_fkey");
        });

        modelBuilder.Entity<EstadisticasRendimiento>(entity =>
        {
            entity.HasKey(e => e.IdEstadistica).HasName("estadisticas_rendimiento_pkey");

            entity.ToTable("estadisticas_rendimiento");

            entity.HasIndex(e => new { e.FechaRegistro, e.TipoOperacion }, "idx_estadisticas_fecha_tipo");

            entity.HasIndex(e => e.TablaAfectada, "idx_estadisticas_tabla");

            entity.Property(e => e.IdEstadistica).HasColumnName("id_estadistica");
            entity.Property(e => e.CantidadRegistros).HasColumnName("cantidad_registros");
            entity.Property(e => e.ConsultaEjecutada).HasColumnName("consulta_ejecutada");
            entity.Property(e => e.ConsumoMemoria).HasColumnName("consumo_memoria");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.PlanEjecucion)
                .HasColumnType("jsonb")
                .HasColumnName("plan_ejecucion");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(50)
                .HasColumnName("tabla_afectada");
            entity.Property(e => e.TiempoEjecucion).HasColumnName("tiempo_ejecucion");
            entity.Property(e => e.TipoOperacion)
                .HasMaxLength(50)
                .HasColumnName("tipo_operacion");
            entity.Property(e => e.UtilizacionIndices)
                .HasDefaultValue(false)
                .HasColumnName("utilizacion_indices");
        });

        modelBuilder.Entity<FeedbackInterno>(entity =>
        {
            entity.HasKey(e => e.IdFeedback).HasName("feedback_interno_pkey");

            entity.ToTable("feedback_interno");

            entity.HasIndex(e => e.IdCliente, "idx_feedback_cliente");

            entity.HasIndex(e => e.IdPerfil, "idx_feedback_perfil");

            entity.HasIndex(e => e.Puntuacion, "idx_feedback_puntuacion");

            entity.Property(e => e.IdFeedback).HasColumnName("id_feedback");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaExperiencia).HasColumnName("fecha_experiencia");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.Puntuacion).HasColumnName("puntuacion");
            entity.Property(e => e.Verificado)
                .HasDefaultValue(false)
                .HasColumnName("verificado");
            entity.Property(e => e.Visibilidad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'admin'::character varying")
                .HasColumnName("visibilidad");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.FeedbackInternos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("feedback_interno_id_cliente_fkey");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.FeedbackInternos)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("feedback_interno_id_perfil_fkey");
        });

        modelBuilder.Entity<HistorialAcceso>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("historial_acceso_pkey");

            entity.ToTable("historial_acceso");

            entity.HasIndex(e => e.FechaAcceso, "idx_historial_fecha");

            entity.HasIndex(e => e.TipoEvento, "idx_historial_tipo_evento");

            entity.HasIndex(e => e.IdUsuario, "idx_historial_usuario");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.Detalles)
                .HasColumnType("jsonb")
                .HasColumnName("detalles");
            entity.Property(e => e.DispositivoConocido)
                .HasDefaultValue(false)
                .HasColumnName("dispositivo_conocido");
            entity.Property(e => e.FechaAcceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_acceso");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.LocalizacionGeografica)
                .HasMaxLength(255)
                .HasColumnName("localizacion_geografica");
            entity.Property(e => e.MetodoAutenticacion)
                .HasMaxLength(50)
                .HasColumnName("metodo_autenticacion");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(20)
                .HasColumnName("tipo_evento");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialAccesos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("historial_acceso_id_usuario_fkey");
        });

        modelBuilder.Entity<HistorialAccesoActual>(entity =>
        {
            entity.HasKey(e => new { e.IdHistorial, e.FechaAcceso }).HasName("historial_acceso_actual_pkey");

            entity.ToTable("historial_acceso_actual");

            entity.Property(e => e.IdHistorial)
                .HasDefaultValueSql("nextval('historial_acceso_particionada_id_historial_seq'::regclass)")
                .HasColumnName("id_historial");
            entity.Property(e => e.FechaAcceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_acceso");
            entity.Property(e => e.Detalles)
                .HasColumnType("jsonb")
                .HasColumnName("detalles");
            entity.Property(e => e.DispositivoConocido)
                .HasDefaultValue(false)
                .HasColumnName("dispositivo_conocido");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.LocalizacionGeografica)
                .HasMaxLength(255)
                .HasColumnName("localizacion_geografica");
            entity.Property(e => e.MetodoAutenticacion)
                .HasMaxLength(50)
                .HasColumnName("metodo_autenticacion");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(20)
                .HasColumnName("tipo_evento");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialAccesoActuals)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("historial_acceso_particionada_id_usuario_fkey");
        });

        modelBuilder.Entity<HistorialAccesoArchivo>(entity =>
        {
            entity.HasKey(e => new { e.IdHistorial, e.FechaAcceso }).HasName("historial_acceso_archivo_pkey");

            entity.ToTable("historial_acceso_archivo");

            entity.Property(e => e.IdHistorial)
                .HasDefaultValueSql("nextval('historial_acceso_particionada_id_historial_seq'::regclass)")
                .HasColumnName("id_historial");
            entity.Property(e => e.FechaAcceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_acceso");
            entity.Property(e => e.Detalles)
                .HasColumnType("jsonb")
                .HasColumnName("detalles");
            entity.Property(e => e.DispositivoConocido)
                .HasDefaultValue(false)
                .HasColumnName("dispositivo_conocido");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.LocalizacionGeografica)
                .HasMaxLength(255)
                .HasColumnName("localizacion_geografica");
            entity.Property(e => e.MetodoAutenticacion)
                .HasMaxLength(50)
                .HasColumnName("metodo_autenticacion");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(20)
                .HasColumnName("tipo_evento");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialAccesoArchivos)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("historial_acceso_particionada_id_usuario_fkey");
        });

        modelBuilder.Entity<HistorialAccesoPasado>(entity =>
        {
            entity.HasKey(e => new { e.IdHistorial, e.FechaAcceso }).HasName("historial_acceso_pasado_pkey");

            entity.ToTable("historial_acceso_pasado");

            entity.Property(e => e.IdHistorial)
                .HasDefaultValueSql("nextval('historial_acceso_particionada_id_historial_seq'::regclass)")
                .HasColumnName("id_historial");
            entity.Property(e => e.FechaAcceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_acceso");
            entity.Property(e => e.Detalles)
                .HasColumnType("jsonb")
                .HasColumnName("detalles");
            entity.Property(e => e.DispositivoConocido)
                .HasDefaultValue(false)
                .HasColumnName("dispositivo_conocido");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.LocalizacionGeografica)
                .HasMaxLength(255)
                .HasColumnName("localizacion_geografica");
            entity.Property(e => e.MetodoAutenticacion)
                .HasMaxLength(50)
                .HasColumnName("metodo_autenticacion");
            entity.Property(e => e.TipoEvento)
                .HasMaxLength(20)
                .HasColumnName("tipo_evento");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.HistorialAccesoPasados)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("historial_acceso_particionada_id_usuario_fkey");
        });

        modelBuilder.Entity<HistorialConfiguracion>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("historial_configuracion_pkey");

            entity.ToTable("historial_configuracion");

            entity.HasIndex(e => e.IdConfig, "idx_historial_config");

            entity.HasIndex(e => e.FechaCambio, "idx_historial_config_fecha");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cambio");
            entity.Property(e => e.IdConfig).HasColumnName("id_config");
            entity.Property(e => e.IpOrigen)
                .HasMaxLength(45)
                .HasColumnName("ip_origen");
            entity.Property(e => e.Motivo).HasColumnName("motivo");
            entity.Property(e => e.RealizadoPor).HasColumnName("realizado_por");
            entity.Property(e => e.ValorAnterior).HasColumnName("valor_anterior");
            entity.Property(e => e.ValorNuevo).HasColumnName("valor_nuevo");

            entity.HasOne(d => d.IdConfigNavigation).WithMany(p => p.HistorialConfiguracions)
                .HasForeignKey(d => d.IdConfig)
                .HasConstraintName("historial_configuracion_id_config_fkey");

            entity.HasOne(d => d.RealizadoPorNavigation).WithMany(p => p.HistorialConfiguracions)
                .HasForeignKey(d => d.RealizadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("historial_configuracion_realizado_por_fkey");
        });

        modelBuilder.Entity<HistorialVerificacione>(entity =>
        {
            entity.HasKey(e => e.IdHistorial).HasName("historial_verificaciones_pkey");

            entity.ToTable("historial_verificaciones");

            entity.HasIndex(e => e.IdVerificacion, "idx_historial_verificacion");

            entity.HasIndex(e => e.FechaCambio, "idx_historial_verificacion_fecha");

            entity.Property(e => e.IdHistorial).HasColumnName("id_historial");
            entity.Property(e => e.Comentario).HasColumnName("comentario");
            entity.Property(e => e.DatosAdicionales)
                .HasColumnType("jsonb")
                .HasColumnName("datos_adicionales");
            entity.Property(e => e.EstadoAnterior)
                .HasMaxLength(25)
                .HasColumnName("estado_anterior");
            entity.Property(e => e.EstadoNuevo)
                .HasMaxLength(25)
                .HasColumnName("estado_nuevo");
            entity.Property(e => e.FechaCambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cambio");
            entity.Property(e => e.IdVerificacion).HasColumnName("id_verificacion");
            entity.Property(e => e.RealizadoPor).HasColumnName("realizado_por");
            entity.Property(e => e.TiempoEnEstado).HasColumnName("tiempo_en_estado");

            entity.HasOne(d => d.IdVerificacionNavigation).WithMany(p => p.HistorialVerificaciones)
                .HasForeignKey(d => d.IdVerificacion)
                .HasConstraintName("historial_verificaciones_id_verificacion_fkey");

            entity.HasOne(d => d.RealizadoPorNavigation).WithMany(p => p.HistorialVerificaciones)
                .HasForeignKey(d => d.RealizadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("historial_verificaciones_realizado_por_fkey");
        });

        modelBuilder.Entity<ImagenesPerfil>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("imagenes_perfil_pkey");

            entity.ToTable("imagenes_perfil");

            entity.HasIndex(e => new { e.Estado, e.FechaSubida }, "idx_imagenes_estado_fecha");

            entity.HasIndex(e => e.HashImagen, "idx_imagenes_hash");

            entity.HasIndex(e => new { e.IdPerfil, e.Orden }, "idx_imagenes_perfil_orden");

            entity.HasIndex(e => new { e.IdPerfil, e.EsPrincipal }, "idx_imagenes_principal");

            entity.HasIndex(e => e.UuidImagen, "idx_imagenes_uuid");

            entity.Property(e => e.IdImagen).HasColumnName("id_imagen");
            entity.Property(e => e.Alto).HasColumnName("alto");
            entity.Property(e => e.Ancho).HasColumnName("ancho");
            entity.Property(e => e.ContenidoSensible)
                .HasDefaultValue(false)
                .HasColumnName("contenido_sensible");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.EsPrincipal)
                .HasDefaultValue(false)
                .HasColumnName("es_principal");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente_revision'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRevision).HasColumnName("fecha_revision");
            entity.Property(e => e.FechaSubida)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("fecha_subida");
            entity.Property(e => e.HashImagen)
                .HasMaxLength(64)
                .HasColumnName("hash_imagen");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.MetadataExif)
                .HasColumnType("jsonb")
                .HasColumnName("metadata_exif");
            entity.Property(e => e.MotivoRechazo)
                .HasMaxLength(255)
                .HasColumnName("motivo_rechazo");
            entity.Property(e => e.NivelBlurring)
                .HasDefaultValue((short)0)
                .HasColumnName("nivel_blurring");
            entity.Property(e => e.Orden)
                .HasDefaultValue(0)
                .HasColumnName("orden");
            entity.Property(e => e.RevisadaPor).HasColumnName("revisada_por");
            entity.Property(e => e.SubidaPor).HasColumnName("subida_por");
            entity.Property(e => e.Tags)
                .HasColumnType("jsonb")
                .HasColumnName("tags");
            entity.Property(e => e.TamañoBytes).HasColumnName("tamaño_bytes");
            entity.Property(e => e.TipoMime)
                .HasMaxLength(50)
                .HasColumnName("tipo_mime");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(255)
                .HasColumnName("url_imagen");
            entity.Property(e => e.UrlMedia)
                .HasMaxLength(255)
                .HasColumnName("url_media");
            entity.Property(e => e.UrlThumbnail)
                .HasMaxLength(255)
                .HasColumnName("url_thumbnail");
            entity.Property(e => e.UrlWebp)
                .HasMaxLength(255)
                .HasColumnName("url_webp");
            entity.Property(e => e.UuidImagen)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("uuid_imagen");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.ImagenesPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("imagenes_perfil_id_perfil_fkey");

            entity.HasOne(d => d.RevisadaPorNavigation).WithMany(p => p.ImagenesPerfilRevisadaPorNavigations)
                .HasForeignKey(d => d.RevisadaPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("imagenes_perfil_revisada_por_fkey");

            entity.HasOne(d => d.SubidaPorNavigation).WithMany(p => p.ImagenesPerfilSubidaPorNavigations)
                .HasForeignKey(d => d.SubidaPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("imagenes_perfil_subida_por_fkey");
        });

        modelBuilder.Entity<LogsSistema>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("logs_sistema_pkey");

            entity.ToTable("logs_sistema");

            entity.HasIndex(e => new { e.FechaLog, e.Tipo }, "idx_logs_fecha_tipo");

            entity.HasIndex(e => e.HashIdentificador, "idx_logs_hash");

            entity.HasIndex(e => new { e.Modulo, e.Submodulo }, "idx_logs_modulo_submodulo");

            entity.HasIndex(e => new { e.Tipo, e.NivelSeveridad }, "idx_logs_tipo_severidad");

            entity.Property(e => e.IdLog).HasColumnName("id_log");
            entity.Property(e => e.CodigoError)
                .HasMaxLength(50)
                .HasColumnName("codigo_error");
            entity.Property(e => e.Datos)
                .HasColumnType("jsonb")
                .HasColumnName("datos");
            entity.Property(e => e.FechaLog)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_log");
            entity.Property(e => e.HashIdentificador)
                .HasMaxLength(64)
                .HasColumnName("hash_identificador");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.MemoriaUtilizada).HasColumnName("memoria_utilizada");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .HasColumnName("mensaje");
            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .HasColumnName("modulo");
            entity.Property(e => e.NivelSeveridad)
                .HasDefaultValue((short)1)
                .HasColumnName("nivel_severidad");
            entity.Property(e => e.Submodulo)
                .HasMaxLength(50)
                .HasColumnName("submodulo");
            entity.Property(e => e.TiempoProcesamiento).HasColumnName("tiempo_procesamiento");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogsSistemas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_id_usuario_fkey");
        });

        modelBuilder.Entity<LogsSistemaActual>(entity =>
        {
            entity.HasKey(e => new { e.IdLog, e.FechaLog }).HasName("logs_sistema_actual_pkey");

            entity.ToTable("logs_sistema_actual");

            entity.Property(e => e.IdLog)
                .HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)")
                .HasColumnName("id_log");
            entity.Property(e => e.FechaLog)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_log");
            entity.Property(e => e.CodigoError)
                .HasMaxLength(50)
                .HasColumnName("codigo_error");
            entity.Property(e => e.Datos)
                .HasColumnType("jsonb")
                .HasColumnName("datos");
            entity.Property(e => e.HashIdentificador)
                .HasMaxLength(64)
                .HasColumnName("hash_identificador");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.MemoriaUtilizada).HasColumnName("memoria_utilizada");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .HasColumnName("mensaje");
            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .HasColumnName("modulo");
            entity.Property(e => e.NivelSeveridad)
                .HasDefaultValue((short)1)
                .HasColumnName("nivel_severidad");
            entity.Property(e => e.Submodulo)
                .HasMaxLength(50)
                .HasColumnName("submodulo");
            entity.Property(e => e.TiempoProcesamiento).HasColumnName("tiempo_procesamiento");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogsSistemaActuals)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<LogsSistemaAntiguo>(entity =>
        {
            entity.HasKey(e => new { e.IdLog, e.FechaLog }).HasName("logs_sistema_antiguo_pkey");

            entity.ToTable("logs_sistema_antiguo");

            entity.Property(e => e.IdLog)
                .HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)")
                .HasColumnName("id_log");
            entity.Property(e => e.FechaLog)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_log");
            entity.Property(e => e.CodigoError)
                .HasMaxLength(50)
                .HasColumnName("codigo_error");
            entity.Property(e => e.Datos)
                .HasColumnType("jsonb")
                .HasColumnName("datos");
            entity.Property(e => e.HashIdentificador)
                .HasMaxLength(64)
                .HasColumnName("hash_identificador");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.MemoriaUtilizada).HasColumnName("memoria_utilizada");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .HasColumnName("mensaje");
            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .HasColumnName("modulo");
            entity.Property(e => e.NivelSeveridad)
                .HasDefaultValue((short)1)
                .HasColumnName("nivel_severidad");
            entity.Property(e => e.Submodulo)
                .HasMaxLength(50)
                .HasColumnName("submodulo");
            entity.Property(e => e.TiempoProcesamiento).HasColumnName("tiempo_procesamiento");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogsSistemaAntiguos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<LogsSistemaArchivo>(entity =>
        {
            entity.HasKey(e => new { e.IdLog, e.FechaLog }).HasName("logs_sistema_archivo_pkey");

            entity.ToTable("logs_sistema_archivo");

            entity.Property(e => e.IdLog)
                .HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)")
                .HasColumnName("id_log");
            entity.Property(e => e.FechaLog)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_log");
            entity.Property(e => e.CodigoError)
                .HasMaxLength(50)
                .HasColumnName("codigo_error");
            entity.Property(e => e.Datos)
                .HasColumnType("jsonb")
                .HasColumnName("datos");
            entity.Property(e => e.HashIdentificador)
                .HasMaxLength(64)
                .HasColumnName("hash_identificador");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.MemoriaUtilizada).HasColumnName("memoria_utilizada");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .HasColumnName("mensaje");
            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .HasColumnName("modulo");
            entity.Property(e => e.NivelSeveridad)
                .HasDefaultValue((short)1)
                .HasColumnName("nivel_severidad");
            entity.Property(e => e.Submodulo)
                .HasMaxLength(50)
                .HasColumnName("submodulo");
            entity.Property(e => e.TiempoProcesamiento).HasColumnName("tiempo_procesamiento");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogsSistemaArchivos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<LogsSistemaReciente>(entity =>
        {
            entity.HasKey(e => new { e.IdLog, e.FechaLog }).HasName("logs_sistema_reciente_pkey");

            entity.ToTable("logs_sistema_reciente");

            entity.Property(e => e.IdLog)
                .HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)")
                .HasColumnName("id_log");
            entity.Property(e => e.FechaLog)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_log");
            entity.Property(e => e.CodigoError)
                .HasMaxLength(50)
                .HasColumnName("codigo_error");
            entity.Property(e => e.Datos)
                .HasColumnType("jsonb")
                .HasColumnName("datos");
            entity.Property(e => e.HashIdentificador)
                .HasMaxLength(64)
                .HasColumnName("hash_identificador");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Ip)
                .HasMaxLength(45)
                .HasColumnName("ip");
            entity.Property(e => e.MemoriaUtilizada).HasColumnName("memoria_utilizada");
            entity.Property(e => e.Mensaje)
                .HasMaxLength(1000)
                .HasColumnName("mensaje");
            entity.Property(e => e.Modulo)
                .HasMaxLength(50)
                .HasColumnName("modulo");
            entity.Property(e => e.NivelSeveridad)
                .HasDefaultValue((short)1)
                .HasColumnName("nivel_severidad");
            entity.Property(e => e.Submodulo)
                .HasMaxLength(50)
                .HasColumnName("submodulo");
            entity.Property(e => e.TiempoProcesamiento).HasColumnName("tiempo_procesamiento");
            entity.Property(e => e.Tipo)
                .HasMaxLength(10)
                .HasColumnName("tipo");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.LogsSistemaRecientes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<MembresiasVip>(entity =>
        {
            entity.HasKey(e => e.IdPlan).HasName("membresias_vip_pkey");

            entity.ToTable("membresias_vip");

            entity.HasIndex(e => e.Estado, "idx_membresias_estado");

            entity.Property(e => e.IdPlan).HasColumnName("id_plan");
            entity.Property(e => e.Beneficios)
                .HasColumnType("jsonb")
                .HasColumnName("beneficios");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.DescuentosAdicionales)
                .HasDefaultValue((short)0)
                .HasColumnName("descuentos_adicionales");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioAnual)
                .HasPrecision(10, 2)
                .HasColumnName("precio_anual");
            entity.Property(e => e.PrecioMensual)
                .HasPrecision(10, 2)
                .HasColumnName("precio_mensual");
            entity.Property(e => e.PrecioTrimestral)
                .HasPrecision(10, 2)
                .HasColumnName("precio_trimestral");
            entity.Property(e => e.PuntosMensuales)
                .HasDefaultValue(0)
                .HasColumnName("puntos_mensuales");
            entity.Property(e => e.ReduccionAnuncios)
                .HasDefaultValue((short)0)
                .HasColumnName("reduccion_anuncios");
        });

        modelBuilder.Entity<MetricasPerfil>(entity =>
        {
            entity.HasKey(e => e.IdMetrica).HasName("metricas_perfil_pkey");

            entity.ToTable("metricas_perfil");

            entity.HasIndex(e => new { e.IdPerfil, e.Fecha }, "uq_metrica_perfil_fecha").IsUnique();

            entity.Property(e => e.IdMetrica).HasColumnName("id_metrica");
            entity.Property(e => e.Contactos)
                .HasDefaultValue(0)
                .HasColumnName("contactos");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.PosicionRanking)
                .HasDefaultValue(0)
                .HasColumnName("posicion_ranking");
            entity.Property(e => e.Tendencia)
                .HasMaxLength(10)
                .HasDefaultValueSql("'estable'::character varying")
                .HasColumnName("tendencia");
            entity.Property(e => e.TiempoPromedio)
                .HasPrecision(10, 2)
                .HasColumnName("tiempo_promedio");
            entity.Property(e => e.Visitas)
                .HasDefaultValue(0)
                .HasColumnName("visitas");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.MetricasPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("metricas_perfil_id_perfil_fkey");
        });

        modelBuilder.Entity<PaquetesCupone>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("paquetes_cupones_pkey");

            entity.ToTable("paquetes_cupones");

            entity.HasIndex(e => e.Estado, "idx_estado_paquete");

            entity.HasIndex(e => e.Precio, "idx_precio_paquete");

            entity.Property(e => e.IdPaquete).HasColumnName("id_paquete");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.ImagenUrl)
                .HasMaxLength(255)
                .HasColumnName("imagen_url");
            entity.Property(e => e.Incluye)
                .HasColumnType("jsonb")
                .HasColumnName("incluye");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.PuntosOtorgados)
                .HasDefaultValue(0)
                .HasColumnName("puntos_otorgados");
            entity.Property(e => e.SorteoIncluido)
                .HasDefaultValue(false)
                .HasColumnName("sorteo_incluido");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Ventas)
                .HasDefaultValue(0)
                .HasColumnName("ventas");
        });

        modelBuilder.Entity<Perfile>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("perfiles_pkey");

            entity.ToTable("perfiles");

            entity.HasIndex(e => e.Destacado, "idx_perfiles_destacado");

            entity.HasIndex(e => e.Estado, "idx_perfiles_estado");

            entity.HasIndex(e => e.Genero, "idx_perfiles_genero");

            entity.HasIndex(e => e.EsIndependiente, "idx_perfiles_independiente");

            entity.HasIndex(e => e.NombrePerfil, "idx_perfiles_nombre");

            entity.HasIndex(e => e.NivelPopularidad, "idx_perfiles_popularidad");

            entity.HasIndex(e => new { e.UbicacionCiudad, e.UbicacionZona }, "idx_perfiles_ubicacion");

            entity.HasIndex(e => e.UltimoOnline, "idx_perfiles_ultimo_online");

            entity.HasIndex(e => e.Verificado, "idx_perfiles_verificado");

            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.Altura).HasColumnName("altura");
            entity.Property(e => e.ColorCabello)
                .HasMaxLength(20)
                .HasColumnName("color_cabello");
            entity.Property(e => e.ColorOjos)
                .HasMaxLength(20)
                .HasColumnName("color_ojos");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Destacado)
                .HasDefaultValue(false)
                .HasColumnName("destacado");
            entity.Property(e => e.DisponeLocal)
                .HasDefaultValue(false)
                .HasColumnName("dispone_local");
            entity.Property(e => e.Disponibilidad)
                .HasColumnType("jsonb")
                .HasColumnName("disponibilidad");
            entity.Property(e => e.Disponible24h)
                .HasDefaultValue(false)
                .HasColumnName("disponible_24h");
            entity.Property(e => e.DisponiblePara)
                .HasMaxLength(20)
                .HasDefaultValueSql("'todos'::character varying")
                .HasColumnName("disponible_para");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.EmailContacto)
                .HasMaxLength(100)
                .HasColumnName("email_contacto");
            entity.Property(e => e.EsIndependiente)
                .HasDefaultValue(false)
                .HasColumnName("es_independiente");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaFinDestacado).HasColumnName("fecha_fin_destacado");
            entity.Property(e => e.FechaInicioDestacado).HasColumnName("fecha_inicio_destacado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FechaVerificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_verificacion");
            entity.Property(e => e.Genero)
                .HasMaxLength(10)
                .HasColumnName("genero");
            entity.Property(e => e.HaceSalidas)
                .HasDefaultValue(false)
                .HasColumnName("hace_salidas");
            entity.Property(e => e.IdAgencia).HasColumnName("id_agencia");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Idiomas)
                .HasColumnType("jsonb")
                .HasColumnName("idiomas");
            entity.Property(e => e.Medidas)
                .HasMaxLength(15)
                .HasColumnName("medidas");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.NivelPopularidad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'bajo'::character varying")
                .HasColumnName("nivel_popularidad");
            entity.Property(e => e.NombrePerfil)
                .HasMaxLength(50)
                .HasColumnName("nombre_perfil");
            entity.Property(e => e.NumContactos)
                .HasDefaultValue(0L)
                .HasColumnName("num_contactos");
            entity.Property(e => e.NumVisitas)
                .HasDefaultValue(0L)
                .HasColumnName("num_visitas");
            entity.Property(e => e.Peso).HasColumnName("peso");
            entity.Property(e => e.PuntuacionInterna)
                .HasPrecision(3, 2)
                .HasColumnName("puntuacion_interna");
            entity.Property(e => e.QuienVerifico).HasColumnName("quien_verifico");
            entity.Property(e => e.Servicios)
                .HasColumnType("jsonb")
                .HasColumnName("servicios");
            entity.Property(e => e.Tarifas)
                .HasColumnType("jsonb")
                .HasColumnName("tarifas");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(20)
                .HasColumnName("telefono_contacto");
            entity.Property(e => e.UbicacionCiudad)
                .HasMaxLength(100)
                .HasColumnName("ubicacion_ciudad");
            entity.Property(e => e.UbicacionZona)
                .HasMaxLength(100)
                .HasColumnName("ubicacion_zona");
            entity.Property(e => e.UltimoOnline)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultimo_online");
            entity.Property(e => e.Verificado)
                .HasDefaultValue(false)
                .HasColumnName("verificado");
            entity.Property(e => e.Whatsapp)
                .HasMaxLength(20)
                .HasColumnName("whatsapp");

            entity.HasOne(d => d.IdAgenciaNavigation).WithMany(p => p.PerfileIdAgenciaNavigations)
                .HasForeignKey(d => d.IdAgencia)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("perfiles_id_agencia_fkey");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Perfiles)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("perfiles_id_usuario_fkey");

            entity.HasOne(d => d.QuienVerificoNavigation).WithMany(p => p.PerfileQuienVerificoNavigations)
                .HasForeignKey(d => d.QuienVerifico)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("perfiles_quien_verifico_fkey");
        });

        modelBuilder.Entity<ProcesamientoImagene>(entity =>
        {
            entity.HasKey(e => e.IdProcesamiento).HasName("procesamiento_imagenes_pkey");

            entity.ToTable("procesamiento_imagenes");

            entity.HasIndex(e => e.IdImagen, "idx_procesamiento_imagen");

            entity.HasIndex(e => e.TipoProcesamiento, "idx_procesamiento_tipo");

            entity.Property(e => e.IdProcesamiento).HasColumnName("id_procesamiento");
            entity.Property(e => e.FechaProcesamiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_procesamiento");
            entity.Property(e => e.IdImagen).HasColumnName("id_imagen");
            entity.Property(e => e.ParametrosProcesamiento)
                .HasColumnType("jsonb")
                .HasColumnName("parametros_procesamiento");
            entity.Property(e => e.ProcesadoPor)
                .HasMaxLength(50)
                .HasColumnName("procesado_por");
            entity.Property(e => e.TipoProcesamiento)
                .HasMaxLength(20)
                .HasColumnName("tipo_procesamiento");
            entity.Property(e => e.UrlResultado)
                .HasMaxLength(255)
                .HasColumnName("url_resultado");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.ProcesamientoImagenes)
                .HasForeignKey(d => d.IdImagen)
                .HasConstraintName("procesamiento_imagenes_id_imagen_fkey");
        });

        modelBuilder.Entity<Punto>(entity =>
        {
            entity.HasKey(e => e.IdPunto).HasName("puntos_pkey");

            entity.ToTable("puntos");

            entity.HasIndex(e => e.IdCliente, "idx_puntos_cliente");

            entity.HasIndex(e => e.Estado, "idx_puntos_estado");

            entity.HasIndex(e => e.FechaExpiracion, "idx_puntos_fecha_exp");

            entity.HasIndex(e => e.TipoAccion, "idx_puntos_tipo_accion");

            entity.Property(e => e.IdPunto).HasColumnName("id_punto");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activo'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.FechaObtencion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_obtencion");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdReferencia).HasColumnName("id_referencia");
            entity.Property(e => e.Multiplicador)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("1.00")
                .HasColumnName("multiplicador");
            entity.Property(e => e.TipoAccion)
                .HasMaxLength(30)
                .HasColumnName("tipo_accion");
            entity.Property(e => e.TipoReferencia)
                .HasMaxLength(50)
                .HasColumnName("tipo_referencia");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Puntos)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("puntos_id_cliente_fkey");
        });

        modelBuilder.Entity<ResumenHistoricoVisita>(entity =>
        {
            entity.HasKey(e => e.IdResumen).HasName("resumen_historico_visitas_pkey");

            entity.ToTable("resumen_historico_visitas");

            entity.HasIndex(e => new { e.PeriodoInicio, e.PeriodoFin }, "idx_resumen_periodo");

            entity.HasIndex(e => new { e.IdPerfil, e.PeriodoInicio, e.PeriodoFin }, "uq_resumen_perfil_periodo").IsUnique();

            entity.Property(e => e.IdResumen).HasColumnName("id_resumen");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.PeriodoFin).HasColumnName("periodo_fin");
            entity.Property(e => e.PeriodoInicio).HasColumnName("periodo_inicio");
            entity.Property(e => e.TiempoPromedio)
                .HasPrecision(10, 2)
                .HasColumnName("tiempo_promedio");
            entity.Property(e => e.TotalContactos)
                .HasDefaultValue(0)
                .HasColumnName("total_contactos");
            entity.Property(e => e.TotalVisitas)
                .HasDefaultValue(0)
                .HasColumnName("total_visitas");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.ResumenHistoricoVisita)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("resumen_historico_visitas_id_perfil_fkey");
        });

        modelBuilder.Entity<SuscripcionesVip>(entity =>
        {
            entity.HasKey(e => e.IdSuscripcion).HasName("suscripciones_vip_pkey");

            entity.ToTable("suscripciones_vip");

            entity.HasIndex(e => e.IdCliente, "idx_suscripciones_cliente");

            entity.HasIndex(e => new { e.Estado, e.FechaProximoCargo }, "idx_suscripciones_cobro");

            entity.HasIndex(e => e.Estado, "idx_suscripciones_estado");

            entity.HasIndex(e => new { e.FechaInicio, e.FechaFin, e.FechaRenovacion, e.FechaProximoCargo }, "idx_suscripciones_fechas");

            entity.HasIndex(e => new { e.GatewayPago, e.IdSuscripcionGateway }, "idx_suscripciones_gateway");

            entity.HasIndex(e => e.NumeroSuscripcion, "idx_suscripciones_numero");

            entity.HasIndex(e => e.NumeroSuscripcion, "suscripciones_vip_numero_suscripcion_key").IsUnique();

            entity.Property(e => e.IdSuscripcion).HasColumnName("id_suscripcion");
            entity.Property(e => e.AutoRenovacion)
                .HasDefaultValue(true)
                .HasColumnName("auto_renovacion");
            entity.Property(e => e.CuponAplicado)
                .HasMaxLength(50)
                .HasColumnName("cupon_aplicado");
            entity.Property(e => e.DatosPago)
                .HasColumnType("jsonb")
                .HasColumnName("datos_pago");
            entity.Property(e => e.EfectivaHasta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("efectiva_hasta");
            entity.Property(e => e.Estado)
                .HasMaxLength(15)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCancelacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cancelacion");
            entity.Property(e => e.FechaFin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaProximoCargo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_proximo_cargo");
            entity.Property(e => e.FechaRenovacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_renovacion");
            entity.Property(e => e.FechaUltimoIntento)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ultimo_intento");
            entity.Property(e => e.GatewayPago)
                .HasMaxLength(50)
                .HasColumnName("gateway_pago");
            entity.Property(e => e.HaRecibidoRecordatorio)
                .HasDefaultValue(false)
                .HasColumnName("ha_recibido_recordatorio");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdClienteGateway)
                .HasMaxLength(100)
                .HasColumnName("id_cliente_gateway");
            entity.Property(e => e.IdPlan).HasColumnName("id_plan");
            entity.Property(e => e.IdSuscripcionGateway)
                .HasMaxLength(100)
                .HasColumnName("id_suscripcion_gateway");
            entity.Property(e => e.IdTransaccionPago)
                .HasMaxLength(100)
                .HasColumnName("id_transaccion_pago");
            entity.Property(e => e.Impuestos)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("impuestos");
            entity.Property(e => e.IntentosCobro)
                .HasDefaultValue(0)
                .HasColumnName("intentos_cobro");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(15)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'USD'::bpchar")
                .IsFixedLength()
                .HasColumnName("moneda");
            entity.Property(e => e.MontoBase)
                .HasPrecision(10, 2)
                .HasColumnName("monto_base");
            entity.Property(e => e.MontoDescuento)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00")
                .HasColumnName("monto_descuento");
            entity.Property(e => e.MontoPagado)
                .HasPrecision(10, 2)
                .HasColumnName("monto_pagado");
            entity.Property(e => e.MotivoCancelacion).HasColumnName("motivo_cancelacion");
            entity.Property(e => e.NotasInternas).HasColumnName("notas_internas");
            entity.Property(e => e.NumeroSuscripcion)
                .HasMaxLength(50)
                .HasColumnName("numero_suscripcion");
            entity.Property(e => e.OrigenSuscripcion)
                .HasMaxLength(15)
                .HasDefaultValueSql("'web'::character varying")
                .HasColumnName("origen_suscripcion");
            entity.Property(e => e.ReferenciaPago)
                .HasMaxLength(100)
                .HasColumnName("referencia_pago");
            entity.Property(e => e.SolicitadaPor)
                .HasMaxLength(15)
                .HasDefaultValueSql("'cliente'::character varying")
                .HasColumnName("solicitada_por");
            entity.Property(e => e.TipoCiclo)
                .HasMaxLength(10)
                .HasColumnName("tipo_ciclo");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.SuscripcionesVips)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("suscripciones_vip_id_cliente_fkey");

            entity.HasOne(d => d.IdPlanNavigation).WithMany(p => p.SuscripcionesVips)
                .HasForeignKey(d => d.IdPlan)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("suscripciones_vip_id_plan_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => new { e.UltimaActividad, e.Estado }, "idx_actividad");

            entity.HasIndex(e => new { e.MetodoAuth, e.AuthId }, "idx_auth");

            entity.HasIndex(e => e.Email, "idx_email");

            entity.HasIndex(e => e.Estado, "idx_estado");

            entity.HasIndex(e => e.TipoUsuario, "idx_tipo_usuario");

            entity.HasIndex(e => e.Uuid, "idx_uuid");

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.AceptoMarketing)
                .HasDefaultValue(false)
                .HasColumnName("acepto_marketing");
            entity.Property(e => e.AuthId)
                .HasMaxLength(100)
                .HasColumnName("auth_id");
            entity.Property(e => e.BloqueoTemporal)
                .HasDefaultValue(false)
                .HasColumnName("bloqueo_temporal");
            entity.Property(e => e.CambioContraseñaRequerido)
                .HasDefaultValue(false)
                .HasColumnName("cambio_contraseña_requerido");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.DatosEliminacion)
                .HasColumnType("jsonb")
                .HasColumnName("datos_eliminacion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Factor2fa)
                .HasDefaultValue(false)
                .HasColumnName("factor_2fa");
            entity.Property(e => e.FechaActualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaBloqueo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_bloqueo");
            entity.Property(e => e.FechaExpiracionToken)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion_token");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
            entity.Property(e => e.FechaSuspension)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_suspension");
            entity.Property(e => e.FechaUltimoCambioContraseña)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ultimo_cambio_contraseña");
            entity.Property(e => e.IntentosFallidos)
                .HasDefaultValue((short)0)
                .HasColumnName("intentos_fallidos");
            entity.Property(e => e.IpRegistro)
                .HasMaxLength(45)
                .HasColumnName("ip_registro");
            entity.Property(e => e.MetodoAuth)
                .HasMaxLength(20)
                .HasDefaultValueSql("'password'::character varying")
                .HasColumnName("metodo_auth");
            entity.Property(e => e.MotivoSuspension).HasColumnName("motivo_suspension");
            entity.Property(e => e.Permisos)
                .HasColumnType("jsonb")
                .HasColumnName("permisos");
            entity.Property(e => e.Salt)
                .HasMaxLength(64)
                .HasColumnName("salt");
            entity.Property(e => e.Secreto2fa)
                .HasMaxLength(64)
                .HasColumnName("secreto_2fa");
            entity.Property(e => e.TipoUsuario)
                .HasMaxLength(20)
                .HasColumnName("tipo_usuario");
            entity.Property(e => e.TokenVerificacion)
                .HasMaxLength(100)
                .HasColumnName("token_verificacion");
            entity.Property(e => e.UltimaActividad)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultima_actividad");
            entity.Property(e => e.UltimoIp)
                .HasMaxLength(45)
                .HasColumnName("ultimo_ip");
            entity.Property(e => e.UltimoLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultimo_login");
            entity.Property(e => e.UltimoTerminosAceptados)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultimo_terminos_aceptados");
            entity.Property(e => e.Uuid)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("uuid");
            entity.Property(e => e.VerificadoEmail)
                .HasDefaultValue(false)
                .HasColumnName("verificado_email");
        });

        modelBuilder.Entity<Verificacione>(entity =>
        {
            entity.HasKey(e => e.IdVerificacion).HasName("verificaciones_pkey");

            entity.ToTable("verificaciones");

            entity.HasIndex(e => e.CodigoVerificacion, "idx_verificaciones_codigo");

            entity.HasIndex(e => new { e.TipoEntidad, e.IdEntidad }, "idx_verificaciones_entidad");

            entity.HasIndex(e => new { e.Estado, e.Prioridad }, "idx_verificaciones_estado_prioridad");

            entity.HasIndex(e => new { e.FechaSolicitud, e.ValidoHasta }, "idx_verificaciones_fechas");

            entity.HasIndex(e => new { e.ValidoHasta, e.RenovacionAutomatica }, "idx_verificaciones_renovacion");

            entity.HasIndex(e => e.CodigoVerificacion, "verificaciones_codigo_verificacion_key").IsUnique();

            entity.Property(e => e.IdVerificacion).HasColumnName("id_verificacion");
            entity.Property(e => e.ChecklistVerificacion)
                .HasColumnType("jsonb")
                .HasColumnName("checklist_verificacion");
            entity.Property(e => e.CodigoVerificacion)
                .HasMaxLength(20)
                .HasColumnName("codigo_verificacion");
            entity.Property(e => e.DocumentosHash)
                .HasColumnType("jsonb")
                .HasColumnName("documentos_hash");
            entity.Property(e => e.DocumentosUrl)
                .HasColumnType("jsonb")
                .HasColumnName("documentos_url");
            entity.Property(e => e.Estado)
                .HasMaxLength(25)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.FechaCaducidadDocumentos).HasColumnName("fecha_caducidad_documentos");
            entity.Property(e => e.FechaSolicitud)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_solicitud");
            entity.Property(e => e.FechaUltimaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ultima_actualizacion");
            entity.Property(e => e.FechaVerificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_verificacion");
            entity.Property(e => e.HistorialEstados)
                .HasColumnType("jsonb")
                .HasColumnName("historial_estados");
            entity.Property(e => e.IdEntidad).HasColumnName("id_entidad");
            entity.Property(e => e.IdTransaccion).HasColumnName("id_transaccion");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Moneda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'USD'::bpchar")
                .IsFixedLength()
                .HasColumnName("moneda");
            entity.Property(e => e.MontoPagado)
                .HasPrecision(10, 2)
                .HasColumnName("monto_pagado");
            entity.Property(e => e.MotivoRechazo).HasColumnName("motivo_rechazo");
            entity.Property(e => e.NivelVerificacion)
                .HasDefaultValue((short)1)
                .HasColumnName("nivel_verificacion");
            entity.Property(e => e.NotasAdmin).HasColumnName("notas_admin");
            entity.Property(e => e.NotasInternas).HasColumnName("notas_internas");
            entity.Property(e => e.OrigenSolicitud)
                .HasMaxLength(50)
                .HasColumnName("origen_solicitud");
            entity.Property(e => e.PagoRecibido)
                .HasDefaultValue(false)
                .HasColumnName("pago_recibido");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'normal'::character varying")
                .HasColumnName("prioridad");
            entity.Property(e => e.PuntuacionRiesgo)
                .HasPrecision(5, 2)
                .HasColumnName("puntuacion_riesgo");
            entity.Property(e => e.RecordatorioEnviado)
                .HasDefaultValue(false)
                .HasColumnName("recordatorio_enviado");
            entity.Property(e => e.RenovacionAutomatica)
                .HasDefaultValue(false)
                .HasColumnName("renovacion_automatica");
            entity.Property(e => e.SugerenciasCorreccion).HasColumnName("sugerencias_correccion");
            entity.Property(e => e.TiempoProceso).HasColumnName("tiempo_proceso");
            entity.Property(e => e.TipoEntidad)
                .HasMaxLength(10)
                .HasColumnName("tipo_entidad");
            entity.Property(e => e.ValidoDesde)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valido_desde");
            entity.Property(e => e.ValidoHasta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valido_hasta");
            entity.Property(e => e.VerificadoPor).HasColumnName("verificado_por");

            entity.HasOne(d => d.VerificadoPorNavigation).WithMany(p => p.Verificaciones)
                .HasForeignKey(d => d.VerificadoPor)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("verificaciones_verificado_por_fkey");
        });

        modelBuilder.Entity<VisitasPerfil>(entity =>
        {
            entity.HasKey(e => e.IdVisita).HasName("visitas_perfil_pkey");

            entity.ToTable("visitas_perfil");

            entity.HasIndex(e => e.IdCliente, "idx_visitas_cliente");

            entity.HasIndex(e => new { e.FechaVisita, e.Dispositivo }, "idx_visitas_fecha_dispositivo");

            entity.HasIndex(e => new { e.IdPerfil, e.FechaVisita }, "idx_visitas_perfil_fecha");

            entity.Property(e => e.IdVisita).HasColumnName("id_visita");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(10)
                .HasColumnName("dispositivo");
            entity.Property(e => e.FechaVisita)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_visita");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IpVisitante)
                .HasMaxLength(45)
                .HasColumnName("ip_visitante");
            entity.Property(e => e.Origen)
                .HasMaxLength(100)
                .HasColumnName("origen");
            entity.Property(e => e.RegionGeografica)
                .HasMaxLength(50)
                .HasColumnName("region_geografica");
            entity.Property(e => e.TiempoVisita).HasColumnName("tiempo_visita");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.VisitasPerfils)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("visitas_perfil_id_cliente_fkey");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.VisitasPerfils)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("visitas_perfil_id_perfil_fkey");
        });

        modelBuilder.Entity<VisitasPerfilActual>(entity =>
        {
            entity.HasKey(e => new { e.IdVisita, e.FechaVisita }).HasName("visitas_perfil_actual_pkey");

            entity.ToTable("visitas_perfil_actual");

            entity.Property(e => e.IdVisita)
                .HasDefaultValueSql("nextval('visitas_perfil_particionada_id_visita_seq'::regclass)")
                .HasColumnName("id_visita");
            entity.Property(e => e.FechaVisita)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_visita");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(10)
                .HasColumnName("dispositivo");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IpVisitante)
                .HasMaxLength(45)
                .HasColumnName("ip_visitante");
            entity.Property(e => e.Origen)
                .HasMaxLength(100)
                .HasColumnName("origen");
            entity.Property(e => e.RegionGeografica)
                .HasMaxLength(50)
                .HasColumnName("region_geografica");
            entity.Property(e => e.TiempoVisita).HasColumnName("tiempo_visita");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.VisitasPerfilActuals)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("visitas_perfil_particionada_id_cliente_fkey");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.VisitasPerfilActuals)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("visitas_perfil_particionada_id_perfil_fkey");
        });

        modelBuilder.Entity<VisitasPerfilAntiguo>(entity =>
        {
            entity.HasKey(e => new { e.IdVisita, e.FechaVisita }).HasName("visitas_perfil_antiguos_pkey");

            entity.ToTable("visitas_perfil_antiguos");

            entity.Property(e => e.IdVisita)
                .HasDefaultValueSql("nextval('visitas_perfil_particionada_id_visita_seq'::regclass)")
                .HasColumnName("id_visita");
            entity.Property(e => e.FechaVisita)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_visita");
            entity.Property(e => e.Dispositivo)
                .HasMaxLength(10)
                .HasColumnName("dispositivo");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");
            entity.Property(e => e.IpVisitante)
                .HasMaxLength(45)
                .HasColumnName("ip_visitante");
            entity.Property(e => e.Origen)
                .HasMaxLength(100)
                .HasColumnName("origen");
            entity.Property(e => e.RegionGeografica)
                .HasMaxLength(50)
                .HasColumnName("region_geografica");
            entity.Property(e => e.TiempoVisita).HasColumnName("tiempo_visita");
            entity.Property(e => e.UserAgent)
                .HasMaxLength(255)
                .HasColumnName("user_agent");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.VisitasPerfilAntiguos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("visitas_perfil_particionada_id_cliente_fkey");

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.VisitasPerfilAntiguos)
                .HasForeignKey(d => d.IdPerfil)
                .HasConstraintName("visitas_perfil_particionada_id_perfil_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
