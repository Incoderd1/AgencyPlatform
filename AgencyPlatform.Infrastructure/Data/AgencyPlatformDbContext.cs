using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AgencyPlatform.Infrastructure.Data.Entities;

public partial class AgencyPlatformDbContext : DbContext
{
    public AgencyPlatformDbContext(DbContextOptions<AgencyPlatformDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<acciones_antifrauide> acciones_antifrauides { get; set; }

    public virtual DbSet<actividad_perfile> actividad_perfiles { get; set; }

    public virtual DbSet<agencia> agencias { get; set; }

    public virtual DbSet<cache_consulta> cache_consultas { get; set; }

    public virtual DbSet<cliente> clientes { get; set; }

    public virtual DbSet<config_sharding> config_shardings { get; set; }

    public virtual DbSet<configuracion_sistema> configuracion_sistemas { get; set; }

    public virtual DbSet<contactos_perfil> contactos_perfils { get; set; }

    public virtual DbSet<cupone> cupones { get; set; }

    public virtual DbSet<dispositivos_usuario> dispositivos_usuarios { get; set; }

    public virtual DbSet<estadisticas_rendimiento> estadisticas_rendimientos { get; set; }

    public virtual DbSet<feedback_interno> feedback_internos { get; set; }

    public virtual DbSet<historial_acceso> historial_accesos { get; set; }

    public virtual DbSet<historial_acceso_actual> historial_acceso_actuals { get; set; }

    public virtual DbSet<historial_acceso_archivo> historial_acceso_archivos { get; set; }

    public virtual DbSet<historial_acceso_pasado> historial_acceso_pasados { get; set; }

    public virtual DbSet<historial_configuracion> historial_configuracions { get; set; }

    public virtual DbSet<historial_verificacione> historial_verificaciones { get; set; }

    public virtual DbSet<imagenes_perfil> imagenes_perfils { get; set; }

    public virtual DbSet<logs_sistema> logs_sistemas { get; set; }

    public virtual DbSet<logs_sistema_actual> logs_sistema_actuals { get; set; }

    public virtual DbSet<logs_sistema_antiguo> logs_sistema_antiguos { get; set; }

    public virtual DbSet<logs_sistema_archivo> logs_sistema_archivos { get; set; }

    public virtual DbSet<logs_sistema_reciente> logs_sistema_recientes { get; set; }

    public virtual DbSet<membresias_vip> membresias_vips { get; set; }

    public virtual DbSet<metricas_perfil> metricas_perfils { get; set; }

    public virtual DbSet<perfile> perfiles { get; set; }

    public virtual DbSet<procesamiento_imagene> procesamiento_imagenes { get; set; }

    public virtual DbSet<punto> puntos { get; set; }

    public virtual DbSet<resumen_historico_visita> resumen_historico_visitas { get; set; }

    public virtual DbSet<suscripciones_vip> suscripciones_vips { get; set; }

    public virtual DbSet<usuario> usuarios { get; set; }

    public virtual DbSet<verificacione> verificaciones { get; set; }

    public virtual DbSet<visitas_perfil> visitas_perfils { get; set; }

    public virtual DbSet<visitas_perfil_actual> visitas_perfil_actuals { get; set; }

    public virtual DbSet<visitas_perfil_antiguo> visitas_perfil_antiguos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<acciones_antifrauide>(entity =>
        {
            entity.HasKey(e => e.id_accion).HasName("acciones_antifrauide_pkey");

            entity.ToTable("acciones_antifrauide");

            entity.HasIndex(e => new { e.tipo_entidad, e.id_entidad }, "idx_antifrauide_entidad");

            entity.HasIndex(e => e.estado, "idx_antifrauide_estado");

            entity.HasIndex(e => e.tipo_accion, "idx_antifrauide_tipo");

            entity.Property(e => e.estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activa'::character varying");
            entity.Property(e => e.evidencia).HasColumnType("jsonb");
            entity.Property(e => e.fecha_accion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_deteccion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_resolucion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_relacionada).HasMaxLength(45);
            entity.Property(e => e.tipo_accion).HasMaxLength(25);
            entity.Property(e => e.tipo_entidad).HasMaxLength(10);

            entity.HasOne(d => d.ejecutada_porNavigation).WithMany(p => p.AccionesAntifrauides)
                .HasForeignKey(d => d.ejecutada_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("acciones_antifrauide_ejecutada_por_fkey");
        });

        modelBuilder.Entity<actividad_perfile>(entity =>
        {
            entity.HasKey(e => e.id_actividad).HasName("actividad_perfiles_pkey");

            entity.HasIndex(e => new { e.fecha_inicio, e.fecha_fin }, "idx_actividad_fechas");

            entity.HasIndex(e => e.id_perfil, "idx_actividad_perfil");

            entity.HasIndex(e => e.tipo_actividad, "idx_actividad_tipo");

            entity.Property(e => e.fecha_fin).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_inicio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_registro).HasMaxLength(45);
            entity.Property(e => e.localizacion).HasMaxLength(100);
            entity.Property(e => e.tipo_actividad).HasMaxLength(15);

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.actividad_perfiles)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("actividad_perfiles_id_perfil_fkey");
        });

        modelBuilder.Entity<agencia>(entity =>
        {
            entity.HasKey(e => e.id_agencia).HasName("agencias_pkey");

            entity.HasIndex(e => e.estado, "idx_agencias_estado");

            entity.HasIndex(e => e.nombre_comercial, "idx_agencias_nombre");

            entity.HasIndex(e => new { e.pais, e.region, e.ciudad }, "idx_agencias_ubicacion");

            entity.HasIndex(e => e.verificada, "idx_agencias_verificada");

            entity.Property(e => e.ciudad).HasMaxLength(100);
            entity.Property(e => e.codigo_postal).HasMaxLength(20);
            entity.Property(e => e.direccion).HasMaxLength(200);
            entity.Property(e => e.documento_verificacion).HasMaxLength(255);
            entity.Property(e => e.email_contacto).HasMaxLength(100);
            entity.Property(e => e.estado)
                .HasMaxLength(30)
                .HasDefaultValueSql("'pendiente_verificacion'::character varying");
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_verificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.horario).HasColumnType("jsonb");
            entity.Property(e => e.logo_url).HasMaxLength(255);
            entity.Property(e => e.nif_cif).HasMaxLength(20);
            entity.Property(e => e.nombre_comercial).HasMaxLength(100);
            entity.Property(e => e.num_perfiles_activos).HasDefaultValue(0);
            entity.Property(e => e.pais).HasMaxLength(100);
            entity.Property(e => e.razon_social).HasMaxLength(150);
            entity.Property(e => e.region).HasMaxLength(100);
            entity.Property(e => e.sitio_web).HasMaxLength(200);
            entity.Property(e => e.telefono).HasMaxLength(20);
            entity.Property(e => e.verificada).HasDefaultValue(false);

            entity.HasOne(d => d.id_usuario_navigation).WithMany(p => p.agencia)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("agencias_id_usuario_fkey");
        });

        modelBuilder.Entity<cache_consulta>(entity =>
        {
            entity.HasKey(e => e.id_cache).HasName("cache_consultas_pkey");

            entity.HasIndex(e => e.clave_cache, "idx_cache_clave");

            entity.HasIndex(e => e.fecha_expiracion, "idx_cache_expiracion");

            entity.HasIndex(e => e.tipo_consulta, "idx_cache_tipo");

            entity.Property(e => e.clave_cache).HasMaxLength(255);
            entity.Property(e => e.datos).HasColumnType("jsonb");
            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_expiracion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.parametros_consulta).HasColumnType("jsonb");
            entity.Property(e => e.tipo_consulta).HasMaxLength(50);
            entity.Property(e => e.ultimo_uso).HasColumnType("timestamp without time zone");
            entity.Property(e => e.veces_utilizado).HasDefaultValue(0);
        });

        modelBuilder.Entity<cliente>(entity =>
        {
            entity.HasKey(e => e.id_cliente).HasName("clientes_pkey");

            entity.HasIndex(e => e.ultima_actividad, "idx_clientes_actividad");

            entity.HasIndex(e => e.edad, "idx_clientes_edad");

            entity.HasIndex(e => e.fidelidad_score, "idx_clientes_fidelidad");

            entity.HasIndex(e => e.puntos_acumulados, "idx_clientes_puntos");

            entity.HasIndex(e => new { e.es_vip, e.nivel_vip }, "idx_clientes_vip");

            entity.Property(e => e.edad).HasComputedColumnSql("calcular_edad(fecha_nacimiento)", true);
            entity.Property(e => e.es_vip).HasDefaultValue(false);
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fidelidad_score).HasDefaultValue(0);
            entity.Property(e => e.genero).HasMaxLength(20);
            entity.Property(e => e.intereses).HasColumnType("jsonb");
            entity.Property(e => e.nivel_vip).HasDefaultValue((short)0);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.num_logins).HasDefaultValue(0);
            entity.Property(e => e.origen_registro).HasMaxLength(50);
            entity.Property(e => e.preferencias).HasColumnType("jsonb");
            entity.Property(e => e.puntos_acumulados).HasDefaultValue(0);
            entity.Property(e => e.puntos_caducados).HasDefaultValue(0);
            entity.Property(e => e.puntos_gastados).HasDefaultValue(0);
            entity.Property(e => e.telefono).HasMaxLength(20);
            entity.Property(e => e.ubicacion_habitual).HasMaxLength(100);
            entity.Property(e => e.ultima_actividad).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.id_usuario_navigation).WithMany(p => p.clientes)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("clientes_id_usuario_fkey");
        });

        modelBuilder.Entity<config_sharding>(entity =>
        {
            entity.HasKey(e => e.id_config).HasName("config_sharding_pkey");

            entity.ToTable("config_sharding");

            entity.HasIndex(e => e.activo, "idx_sharding_activo");

            entity.HasIndex(e => e.tabla, "uq_config_sharding_tabla").IsUnique();

            entity.Property(e => e.activo).HasDefaultValue(true);
            entity.Property(e => e.campo_sharding).HasMaxLength(50);
            entity.Property(e => e.configuracion_shard).HasColumnType("jsonb");
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.metodo_sharding).HasMaxLength(20);
            entity.Property(e => e.numero_shards).HasDefaultValue(1);
            entity.Property(e => e.shard_actual).HasDefaultValue(0);
            entity.Property(e => e.tabla).HasMaxLength(50);
        });

        modelBuilder.Entity<configuracion_sistema>(entity =>
        {
            entity.HasKey(e => e.id_config).HasName("configuracion_sistema_pkey");

            entity.ToTable("configuracion_sistema");

            entity.HasIndex(e => e.clave, "configuracion_sistema_clave_key").IsUnique();

            entity.HasIndex(e => e.nivel_cache, "idx_config_cache");

            entity.HasIndex(e => e.clave, "idx_config_clave");

            entity.HasIndex(e => new { e.grupo, e.entorno }, "idx_config_grupo_entorno");

            entity.Property(e => e.clave).HasMaxLength(50);
            entity.Property(e => e.entorno)
                .HasMaxLength(15)
                .HasDefaultValueSql("'all'::character varying");
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.grupo)
                .HasMaxLength(50)
                .HasDefaultValueSql("'general'::character varying");
            entity.Property(e => e.modificable).HasDefaultValue(true);
            entity.Property(e => e.nivel_cache)
                .HasMaxLength(15)
                .HasDefaultValueSql("'medium_term'::character varying");
            entity.Property(e => e.tipo)
                .HasMaxLength(10)
                .HasDefaultValueSql("'text'::character varying");
            entity.Property(e => e.version).HasDefaultValue(1);

            entity.HasOne(d => d.actualizado_porNavigation).WithMany(p => p.configuracion_sistemas)
                .HasForeignKey(d => d.actualizado_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("configuracion_sistema_actualizado_por_fkey");
        });

        modelBuilder.Entity<contactos_perfil>(entity =>
        {
            entity.HasKey(e => e.id_contacto).HasName("contactos_perfil_pkey");

            entity.ToTable("contactos_perfil");

            entity.HasIndex(e => e.id_cliente, "idx_contactos_cliente");

            entity.HasIndex(e => e.estado, "idx_contactos_estado");

            entity.HasIndex(e => e.fecha_contacto, "idx_contactos_fecha");

            entity.HasIndex(e => e.id_perfil, "idx_contactos_perfil");

            entity.Property(e => e.email_contacto).HasMaxLength(100);
            entity.Property(e => e.estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.fecha_contacto)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_respuesta).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_contacto).HasMaxLength(45);
            entity.Property(e => e.telefono_contacto).HasMaxLength(20);
            entity.Property(e => e.tipo_contacto).HasMaxLength(20);

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.contactos_perfils)
                .HasForeignKey(d => d.id_cliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("contactos_perfil_id_cliente_fkey");

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.contactos_perfils)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("contactos_perfil_id_perfil_fkey");
        });

        modelBuilder.Entity<cupone>(entity =>
        {
            entity.HasKey(e => e.id_cupon).HasName("cupones_pkey");

            entity.HasIndex(e => e.codigo, "cupones_codigo_key").IsUnique();

            entity.HasIndex(e => e.codigo, "idx_cupones_codigo");

            entity.HasIndex(e => e.estado, "idx_cupones_estado");

            entity.HasIndex(e => new { e.fecha_inicio, e.fecha_fin }, "idx_cupones_fechas");

            entity.Property(e => e.aplica_a)
                .HasMaxLength(20)
                .HasDefaultValueSql("'todo'::character varying");
            entity.Property(e => e.codigo).HasMaxLength(20);
            entity.Property(e => e.estado)
                .HasMaxLength(15)
                .HasDefaultValueSql("'activo'::character varying");
            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_fin).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_inicio).HasColumnType("timestamp without time zone");
            entity.Property(e => e.maximo_descuento).HasPrecision(10, 2);
            entity.Property(e => e.minimo_compra).HasPrecision(10, 2);
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.tipo_descuento).HasMaxLength(20);
            entity.Property(e => e.usos_actuales).HasDefaultValue(0);
            entity.Property(e => e.usos_por_cliente).HasDefaultValue(1);
            entity.Property(e => e.valor).HasPrecision(10, 2);
        });

        modelBuilder.Entity<dispositivos_usuario>(entity =>
        {
            entity.HasKey(e => e.id_dispositivo).HasName("dispositivos_usuario_pkey");

            entity.ToTable("dispositivos_usuario");

            entity.HasIndex(e => e.estado, "idx_dispositivos_estado");

            entity.HasIndex(e => e.token_dispositivo, "idx_dispositivos_token");

            entity.HasIndex(e => e.id_usuario, "idx_dispositivos_usuario");

            entity.Property(e => e.estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activo'::character varying");
            entity.Property(e => e.fcm_token).HasMaxLength(255);
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_ultima_actividad).HasMaxLength(45);
            entity.Property(e => e.navegador).HasMaxLength(50);
            entity.Property(e => e.nombre_dispositivo).HasMaxLength(100);
            entity.Property(e => e.sistema_operativo).HasMaxLength(50);
            entity.Property(e => e.tipo_dispositivo).HasMaxLength(10);
            entity.Property(e => e.token_dispositivo).HasMaxLength(255);
            entity.Property(e => e.ultima_actividad)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.dispositivos_usuarios)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("dispositivos_usuario_id_usuario_fkey");
        });

        modelBuilder.Entity<estadisticas_rendimiento>(entity =>
        {
            entity.HasKey(e => e.id_estadistica).HasName("estadisticas_rendimiento_pkey");

            entity.ToTable("estadisticas_rendimiento");

            entity.HasIndex(e => new { e.fecha_registro, e.tipo_operacion }, "idx_estadisticas_fecha_tipo");

            entity.HasIndex(e => e.tabla_afectada, "idx_estadisticas_tabla");

            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.plan_ejecucion).HasColumnType("jsonb");
            entity.Property(e => e.tabla_afectada).HasMaxLength(50);
            entity.Property(e => e.tipo_operacion).HasMaxLength(50);
            entity.Property(e => e.utilizacion_indices).HasDefaultValue(false);
        });

        modelBuilder.Entity<feedback_interno>(entity =>
        {
            entity.HasKey(e => e.id_feedback).HasName("feedback_interno_pkey");

            entity.ToTable("feedback_interno");

            entity.HasIndex(e => e.id_cliente, "idx_feedback_cliente");

            entity.HasIndex(e => e.id_perfil, "idx_feedback_perfil");

            entity.HasIndex(e => e.puntuacion, "idx_feedback_puntuacion");

            entity.Property(e => e.estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.verificado).HasDefaultValue(false);
            entity.Property(e => e.visibilidad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'admin'::character varying");

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.feedback_internos)
                .HasForeignKey(d => d.id_cliente)
                .HasConstraintName("feedback_interno_id_cliente_fkey");

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.feedback_internos)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("feedback_interno_id_perfil_fkey");
        });

        modelBuilder.Entity<historial_acceso>(entity =>
        {
            entity.HasKey(e => e.id_historial).HasName("historial_acceso_pkey");

            entity.ToTable("historial_acceso");

            entity.HasIndex(e => e.fecha_acceso, "idx_historial_fecha");

            entity.HasIndex(e => e.tipo_evento, "idx_historial_tipo_evento");

            entity.HasIndex(e => e.id_usuario, "idx_historial_usuario");

            entity.Property(e => e.detalles).HasColumnType("jsonb");
            entity.Property(e => e.dispositivo_conocido).HasDefaultValue(false);
            entity.Property(e => e.fecha_acceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.localizacion_geografica).HasMaxLength(255);
            entity.Property(e => e.metodo_autenticacion).HasMaxLength(50);
            entity.Property(e => e.tipo_evento).HasMaxLength(20);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.historial_accesos)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("historial_acceso_id_usuario_fkey");
        });

        modelBuilder.Entity<historial_acceso_actual>(entity =>
        {
            entity.HasKey(e => new { e.id_historial, e.fecha_acceso }).HasName("historial_acceso_actual_pkey");

            entity.ToTable("historial_acceso_actual");

            entity.Property(e => e.id_historial).HasDefaultValueSql("nextval('historial_acceso_particionada_id_historial_seq'::regclass)");
            entity.Property(e => e.fecha_acceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.detalles).HasColumnType("jsonb");
            entity.Property(e => e.dispositivo_conocido).HasDefaultValue(false);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.localizacion_geografica).HasMaxLength(255);
            entity.Property(e => e.metodo_autenticacion).HasMaxLength(50);
            entity.Property(e => e.tipo_evento).HasMaxLength(20);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.historial_acceso_actuals)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("historial_acceso_particionada_id_usuario_fkey");
        });

        modelBuilder.Entity<historial_acceso_archivo>(entity =>
        {
            entity.HasKey(e => new { e.id_historial, e.fecha_acceso }).HasName("historial_acceso_archivo_pkey");

            entity.ToTable("historial_acceso_archivo");

            entity.Property(e => e.id_historial).HasDefaultValueSql("nextval('historial_acceso_particionada_id_historial_seq'::regclass)");
            entity.Property(e => e.fecha_acceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.detalles).HasColumnType("jsonb");
            entity.Property(e => e.dispositivo_conocido).HasDefaultValue(false);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.localizacion_geografica).HasMaxLength(255);
            entity.Property(e => e.metodo_autenticacion).HasMaxLength(50);
            entity.Property(e => e.tipo_evento).HasMaxLength(20);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.historial_acceso_archivos)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("historial_acceso_particionada_id_usuario_fkey");
        });

        modelBuilder.Entity<historial_acceso_pasado>(entity =>
        {
            entity.HasKey(e => new { e.id_historial, e.fecha_acceso }).HasName("historial_acceso_pasado_pkey");

            entity.ToTable("historial_acceso_pasado");

            entity.Property(e => e.id_historial).HasDefaultValueSql("nextval('historial_acceso_particionada_id_historial_seq'::regclass)");
            entity.Property(e => e.fecha_acceso)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.detalles).HasColumnType("jsonb");
            entity.Property(e => e.dispositivo_conocido).HasDefaultValue(false);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.localizacion_geografica).HasMaxLength(255);
            entity.Property(e => e.metodo_autenticacion).HasMaxLength(50);
            entity.Property(e => e.tipo_evento).HasMaxLength(20);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.historial_acceso_pasados)
                .HasForeignKey(d => d.id_usuario)
                .HasConstraintName("historial_acceso_particionada_id_usuario_fkey");
        });

        modelBuilder.Entity<historial_configuracion>(entity =>
        {
            entity.HasKey(e => e.id_historial).HasName("historial_configuracion_pkey");

            entity.ToTable("historial_configuracion");

            entity.HasIndex(e => e.id_config, "idx_historial_config");

            entity.HasIndex(e => e.fecha_cambio, "idx_historial_config_fecha");

            entity.Property(e => e.fecha_cambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_origen).HasMaxLength(45);

            entity.HasOne(d => d.id_configNavigation).WithMany(p => p.historial_configuracions)
                .HasForeignKey(d => d.id_config)
                .HasConstraintName("historial_configuracion_id_config_fkey");

            entity.HasOne(d => d.realizado_porNavigation).WithMany(p => p.historial_configuracions)
                .HasForeignKey(d => d.realizado_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("historial_configuracion_realizado_por_fkey");
        });

        modelBuilder.Entity<historial_verificacione>(entity =>
        {
            entity.HasKey(e => e.id_historial).HasName("historial_verificaciones_pkey");

            entity.HasIndex(e => e.id_verificacion, "idx_historial_verificacion");

            entity.HasIndex(e => e.fecha_cambio, "idx_historial_verificacion_fecha");

            entity.Property(e => e.datos_adicionales).HasColumnType("jsonb");
            entity.Property(e => e.estado_anterior).HasMaxLength(25);
            entity.Property(e => e.estado_nuevo).HasMaxLength(25);
            entity.Property(e => e.fecha_cambio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.id_verificacionNavigation).WithMany(p => p.historial_verificaciones)
                .HasForeignKey(d => d.id_verificacion)
                .HasConstraintName("historial_verificaciones_id_verificacion_fkey");

            entity.HasOne(d => d.realizado_porNavigation).WithMany(p => p.historial_verificaciones)
                .HasForeignKey(d => d.realizado_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("historial_verificaciones_realizado_por_fkey");
        });

        modelBuilder.Entity<imagenes_perfil>(entity =>
        {
            entity.HasKey(e => e.id_imagen).HasName("imagenes_perfil_pkey");

            entity.ToTable("imagenes_perfil");

            entity.HasIndex(e => new { e.estado, e.fecha_subida }, "idx_imagenes_estado_fecha");

            entity.HasIndex(e => e.hash_imagen, "idx_imagenes_hash");

            entity.HasIndex(e => new { e.id_perfil, e.orden }, "idx_imagenes_perfil_orden");

            entity.HasIndex(e => new { e.id_perfil, e.es_principal }, "idx_imagenes_principal");

            entity.HasIndex(e => e.uuid_imagen, "idx_imagenes_uuid");

            entity.Property(e => e.contenido_sensible).HasDefaultValue(false);
            entity.Property(e => e.descripcion).HasMaxLength(255);
            entity.Property(e => e.es_principal).HasDefaultValue(false);
            entity.Property(e => e.estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente_revision'::character varying");
            entity.Property(e => e.fecha_revision).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_subida)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.hash_imagen).HasMaxLength(64);
            entity.Property(e => e.metadata_exif).HasColumnType("jsonb");
            entity.Property(e => e.motivo_rechazo).HasMaxLength(255);
            entity.Property(e => e.nivel_blurring).HasDefaultValue((short)0);
            entity.Property(e => e.orden).HasDefaultValue((short)0);
            entity.Property(e => e.tags).HasColumnType("jsonb");
            entity.Property(e => e.tipo_mime).HasMaxLength(50);
            entity.Property(e => e.url_imagen).HasMaxLength(255);
            entity.Property(e => e.url_media).HasMaxLength(255);
            entity.Property(e => e.url_thumbnail).HasMaxLength(255);
            entity.Property(e => e.url_webp).HasMaxLength(255);
            entity.Property(e => e.uuid_imagen).HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.imagenes_perfils)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("imagenes_perfil_id_perfil_fkey");

            entity.HasOne(d => d.revisada_porNavigation).WithMany(p => p.imagenes_perfil_revisada_por_navigations)
                .HasForeignKey(d => d.revisada_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("imagenes_perfil_revisada_por_fkey");

            entity.HasOne(d => d.subida_porNavigation).WithMany(p => p.imagenes_perfil_subida_por_navigations)
                .HasForeignKey(d => d.subida_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("imagenes_perfil_subida_por_fkey");
        });

        modelBuilder.Entity<logs_sistema>(entity =>
        {
            entity.HasKey(e => e.id_log).HasName("logs_sistema_pkey");

            entity.ToTable("logs_sistema");

            entity.HasIndex(e => new { e.fecha_log, e.tipo }, "idx_logs_fecha_tipo");

            entity.HasIndex(e => e.hash_identificador, "idx_logs_hash");

            entity.HasIndex(e => new { e.modulo, e.submodulo }, "idx_logs_modulo_submodulo");

            entity.HasIndex(e => new { e.tipo, e.nivel_severidad }, "idx_logs_tipo_severidad");

            entity.Property(e => e.codigo_error).HasMaxLength(50);
            entity.Property(e => e.datos).HasColumnType("jsonb");
            entity.Property(e => e.fecha_log)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.hash_identificador).HasMaxLength(64);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.mensaje).HasMaxLength(1000);
            entity.Property(e => e.modulo).HasMaxLength(50);
            entity.Property(e => e.nivel_severidad).HasDefaultValue((short)1);
            entity.Property(e => e.submodulo).HasMaxLength(50);
            entity.Property(e => e.tipo).HasMaxLength(10);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.logs_sistemas)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_id_usuario_fkey");
        });

        modelBuilder.Entity<logs_sistema_actual>(entity =>
        {
            entity.HasKey(e => new { e.id_log, e.fecha_log }).HasName("logs_sistema_actual_pkey");

            entity.ToTable("logs_sistema_actual");

            entity.Property(e => e.id_log).HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)");
            entity.Property(e => e.fecha_log)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.codigo_error).HasMaxLength(50);
            entity.Property(e => e.datos).HasColumnType("jsonb");
            entity.Property(e => e.hash_identificador).HasMaxLength(64);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.mensaje).HasMaxLength(1000);
            entity.Property(e => e.modulo).HasMaxLength(50);
            entity.Property(e => e.nivel_severidad).HasDefaultValue((short)1);
            entity.Property(e => e.submodulo).HasMaxLength(50);
            entity.Property(e => e.tipo).HasMaxLength(10);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.logs_sistema_actuals)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<logs_sistema_antiguo>(entity =>
        {
            entity.HasKey(e => new { e.id_log, e.fecha_log }).HasName("logs_sistema_antiguo_pkey");

            entity.ToTable("logs_sistema_antiguo");

            entity.Property(e => e.id_log).HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)");
            entity.Property(e => e.fecha_log)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.codigo_error).HasMaxLength(50);
            entity.Property(e => e.datos).HasColumnType("jsonb");
            entity.Property(e => e.hash_identificador).HasMaxLength(64);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.mensaje).HasMaxLength(1000);
            entity.Property(e => e.modulo).HasMaxLength(50);
            entity.Property(e => e.nivel_severidad).HasDefaultValue((short)1);
            entity.Property(e => e.submodulo).HasMaxLength(50);
            entity.Property(e => e.tipo).HasMaxLength(10);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.logs_sistema_antiguos)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<logs_sistema_archivo>(entity =>
        {
            entity.HasKey(e => new { e.id_log, e.fecha_log }).HasName("logs_sistema_archivo_pkey");

            entity.ToTable("logs_sistema_archivo");

            entity.Property(e => e.id_log).HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)");
            entity.Property(e => e.fecha_log)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.codigo_error).HasMaxLength(50);
            entity.Property(e => e.datos).HasColumnType("jsonb");
            entity.Property(e => e.hash_identificador).HasMaxLength(64);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.mensaje).HasMaxLength(1000);
            entity.Property(e => e.modulo).HasMaxLength(50);
            entity.Property(e => e.nivel_severidad).HasDefaultValue((short)1);
            entity.Property(e => e.submodulo).HasMaxLength(50);
            entity.Property(e => e.tipo).HasMaxLength(10);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.logs_sistema_archivos)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<logs_sistema_reciente>(entity =>
        {
            entity.HasKey(e => new { e.id_log, e.fecha_log }).HasName("logs_sistema_reciente_pkey");

            entity.ToTable("logs_sistema_reciente");

            entity.Property(e => e.id_log).HasDefaultValueSql("nextval('logs_sistema_particionado_id_log_seq'::regclass)");
            entity.Property(e => e.fecha_log)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.codigo_error).HasMaxLength(50);
            entity.Property(e => e.datos).HasColumnType("jsonb");
            entity.Property(e => e.hash_identificador).HasMaxLength(64);
            entity.Property(e => e.ip).HasMaxLength(45);
            entity.Property(e => e.mensaje).HasMaxLength(1000);
            entity.Property(e => e.modulo).HasMaxLength(50);
            entity.Property(e => e.nivel_severidad).HasDefaultValue((short)1);
            entity.Property(e => e.submodulo).HasMaxLength(50);
            entity.Property(e => e.tipo).HasMaxLength(10);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_usuarioNavigation).WithMany(p => p.logs_sistema_recientes)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_sistema_particionado_id_usuario_fkey");
        });

        modelBuilder.Entity<membresias_vip>(entity =>
        {
            entity.HasKey(e => e.id_plan).HasName("membresias_vip_pkey");

            entity.ToTable("membresias_vip");

            entity.HasIndex(e => e.estado, "idx_membresias_estado");

            entity.Property(e => e.beneficios).HasColumnType("jsonb");
            entity.Property(e => e.descuentos_adicionales).HasDefaultValue((short)0);
            entity.Property(e => e.estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activo'::character varying");
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.nombre).HasMaxLength(100);
            entity.Property(e => e.precio_anual).HasPrecision(10, 2);
            entity.Property(e => e.precio_mensual).HasPrecision(10, 2);
            entity.Property(e => e.precio_trimestral).HasPrecision(10, 2);
            entity.Property(e => e.puntos_mensuales).HasDefaultValue(0);
            entity.Property(e => e.reduccion_anuncios).HasDefaultValue((short)0);
        });

        modelBuilder.Entity<metricas_perfil>(entity =>
        {
            entity.HasKey(e => e.id_metrica).HasName("metricas_perfil_pkey");

            entity.ToTable("metricas_perfil");

            entity.HasIndex(e => new { e.id_perfil, e.fecha }, "uq_metrica_perfil_fecha").IsUnique();

            entity.Property(e => e.contactos).HasDefaultValue(0);
            entity.Property(e => e.posicion_ranking).HasDefaultValue(0);
            entity.Property(e => e.tendencia)
                .HasMaxLength(10)
                .HasDefaultValueSql("'estable'::character varying");
            entity.Property(e => e.tiempo_promedio).HasPrecision(10, 2);
            entity.Property(e => e.visitas).HasDefaultValue(0);

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.metricas_perfils)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("metricas_perfil_id_perfil_fkey");
        });

        modelBuilder.Entity<perfile>(entity =>
        {
            entity.HasKey(e => e.id_perfil).HasName("perfiles_pkey");

            entity.HasIndex(e => e.destacado, "idx_perfiles_destacado");

            entity.HasIndex(e => e.estado, "idx_perfiles_estado");

            entity.HasIndex(e => e.genero, "idx_perfiles_genero");

            entity.HasIndex(e => e.es_independiente, "idx_perfiles_independiente");

            entity.HasIndex(e => e.nombre_perfil, "idx_perfiles_nombre");

            entity.HasIndex(e => e.nivel_popularidad, "idx_perfiles_popularidad");

            entity.HasIndex(e => new { e.ubicacion_ciudad, e.ubicacion_zona }, "idx_perfiles_ubicacion");

            entity.HasIndex(e => e.ultimo_online, "idx_perfiles_ultimo_online");

            entity.HasIndex(e => e.verificado, "idx_perfiles_verificado");

            entity.Property(e => e.color_cabello).HasMaxLength(20);
            entity.Property(e => e.color_ojos).HasMaxLength(20);
            entity.Property(e => e.destacado).HasDefaultValue(false);
            entity.Property(e => e.dispone_local).HasDefaultValue(false);
            entity.Property(e => e.disponibilidad).HasColumnType("jsonb");
            entity.Property(e => e.disponible_24h).HasDefaultValue(false);
            entity.Property(e => e.disponible_para)
                .HasMaxLength(20)
                .HasDefaultValueSql("'todos'::character varying");
            entity.Property(e => e.email_contacto).HasMaxLength(100);
            entity.Property(e => e.es_independiente).HasDefaultValue(false);
            entity.Property(e => e.estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_verificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.genero).HasMaxLength(10);
            entity.Property(e => e.hace_salidas).HasDefaultValue(false);
            entity.Property(e => e.idiomas).HasColumnType("jsonb");
            entity.Property(e => e.medidas).HasMaxLength(15);
            entity.Property(e => e.nacionalidad).HasMaxLength(50);
            entity.Property(e => e.nivel_popularidad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'bajo'::character varying");
            entity.Property(e => e.nombre_perfil).HasMaxLength(50);
            entity.Property(e => e.num_contactos).HasDefaultValue(0L);
            entity.Property(e => e.num_visitas).HasDefaultValue(0L);
            entity.Property(e => e.puntuacion_interna).HasPrecision(3, 2);
            entity.Property(e => e.servicios).HasColumnType("jsonb");
            entity.Property(e => e.tarifas).HasColumnType("jsonb");
            entity.Property(e => e.telefono_contacto).HasMaxLength(20);
            entity.Property(e => e.ubicacion_ciudad).HasMaxLength(100);
            entity.Property(e => e.ubicacion_zona).HasMaxLength(100);
            entity.Property(e => e.ultimo_online).HasColumnType("timestamp without time zone");
            entity.Property(e => e.verificado).HasDefaultValue(false);
            entity.Property(e => e.whatsapp).HasMaxLength(20);

            entity.HasOne(d => d.id_agencia_navigation).WithMany(p => p.perfile_id_agencia_navigations)
                .HasForeignKey(d => d.id_agencia)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("perfiles_id_agencia_fkey");

            entity.HasOne(d => d.id_usuario_navigation).WithMany(p => p.perfiles)
                .HasForeignKey(d => d.id_usuario)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("perfiles_id_usuario_fkey");

            entity.HasOne(d => d.quien_verifico_navigation).WithMany(p => p.perfile_quien_verifico_navigations)
                .HasForeignKey(d => d.quien_verifico)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("perfiles_quien_verifico_fkey");
        });

        modelBuilder.Entity<procesamiento_imagene>(entity =>
        {
            entity.HasKey(e => e.id_procesamiento).HasName("procesamiento_imagenes_pkey");

            entity.HasIndex(e => e.id_imagen, "idx_procesamiento_imagen");

            entity.HasIndex(e => e.tipo_procesamiento, "idx_procesamiento_tipo");

            entity.Property(e => e.fecha_procesamiento)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.parametros_procesamiento).HasColumnType("jsonb");
            entity.Property(e => e.procesado_por).HasMaxLength(50);
            entity.Property(e => e.tipo_procesamiento).HasMaxLength(20);
            entity.Property(e => e.url_resultado).HasMaxLength(255);

            entity.HasOne(d => d.id_imagenNavigation).WithMany(p => p.procesamiento_imagenes)
                .HasForeignKey(d => d.id_imagen)
                .HasConstraintName("procesamiento_imagenes_id_imagen_fkey");
        });

        modelBuilder.Entity<punto>(entity =>
        {
            entity.HasKey(e => e.id_punto).HasName("puntos_pkey");

            entity.HasIndex(e => e.id_cliente, "idx_puntos_cliente");

            entity.HasIndex(e => e.estado, "idx_puntos_estado");

            entity.HasIndex(e => e.fecha_expiracion, "idx_puntos_fecha_exp");

            entity.HasIndex(e => e.tipo_accion, "idx_puntos_tipo_accion");

            entity.Property(e => e.descripcion).HasMaxLength(255);
            entity.Property(e => e.estado)
                .HasMaxLength(10)
                .HasDefaultValueSql("'activo'::character varying");
            entity.Property(e => e.fecha_expiracion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_obtencion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.multiplicador)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("1.00");
            entity.Property(e => e.tipo_accion).HasMaxLength(30);
            entity.Property(e => e.tipo_referencia).HasMaxLength(50);

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.puntos)
                .HasForeignKey(d => d.id_cliente)
                .HasConstraintName("puntos_id_cliente_fkey");
        });

        modelBuilder.Entity<resumen_historico_visita>(entity =>
        {
            entity.HasKey(e => e.id_resumen).HasName("resumen_historico_visitas_pkey");

            entity.HasIndex(e => new { e.periodo_inicio, e.periodo_fin }, "idx_resumen_periodo");

            entity.HasIndex(e => new { e.id_perfil, e.periodo_inicio, e.periodo_fin }, "uq_resumen_perfil_periodo").IsUnique();

            entity.Property(e => e.fecha_creacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.tiempo_promedio).HasPrecision(10, 2);
            entity.Property(e => e.total_contactos).HasDefaultValue(0);
            entity.Property(e => e.total_visitas).HasDefaultValue(0);

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.resumen_historico_visita)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("resumen_historico_visitas_id_perfil_fkey");
        });

        modelBuilder.Entity<suscripciones_vip>(entity =>
        {
            entity.HasKey(e => e.id_suscripcion).HasName("suscripciones_vip_pkey");

            entity.ToTable("suscripciones_vip");

            entity.HasIndex(e => e.id_cliente, "idx_suscripciones_cliente");

            entity.HasIndex(e => new { e.estado, e.fecha_proximo_cargo }, "idx_suscripciones_cobro");

            entity.HasIndex(e => e.estado, "idx_suscripciones_estado");

            entity.HasIndex(e => new { e.fecha_inicio, e.fecha_fin, e.fecha_renovacion, e.fecha_proximo_cargo }, "idx_suscripciones_fechas");

            entity.HasIndex(e => new { e.gateway_pago, e.id_suscripcion_gateway }, "idx_suscripciones_gateway");

            entity.HasIndex(e => e.numero_suscripcion, "idx_suscripciones_numero");

            entity.HasIndex(e => e.numero_suscripcion, "suscripciones_vip_numero_suscripcion_key").IsUnique();

            entity.Property(e => e.auto_renovacion).HasDefaultValue(true);
            entity.Property(e => e.cupon_aplicado).HasMaxLength(50);
            entity.Property(e => e.datos_pago).HasColumnType("jsonb");
            entity.Property(e => e.efectiva_hasta).HasColumnType("timestamp without time zone");
            entity.Property(e => e.estado)
                .HasMaxLength(15)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.fecha_cancelacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_fin).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_inicio).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_proximo_cargo).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_renovacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_ultimo_intento).HasColumnType("timestamp without time zone");
            entity.Property(e => e.gateway_pago).HasMaxLength(50);
            entity.Property(e => e.ha_recibido_recordatorio).HasDefaultValue(false);
            entity.Property(e => e.id_cliente_gateway).HasMaxLength(100);
            entity.Property(e => e.id_suscripcion_gateway).HasMaxLength(100);
            entity.Property(e => e.id_transaccion_pago).HasMaxLength(100);
            entity.Property(e => e.impuestos)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00");
            entity.Property(e => e.intentos_cobro).HasDefaultValue(0);
            entity.Property(e => e.metodo_pago).HasMaxLength(15);
            entity.Property(e => e.moneda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'USD'::bpchar")
                .IsFixedLength();
            entity.Property(e => e.monto_base).HasPrecision(10, 2);
            entity.Property(e => e.monto_descuento)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0.00");
            entity.Property(e => e.monto_pagado).HasPrecision(10, 2);
            entity.Property(e => e.numero_suscripcion).HasMaxLength(50);
            entity.Property(e => e.origen_suscripcion)
                .HasMaxLength(15)
                .HasDefaultValueSql("'web'::character varying");
            entity.Property(e => e.referencia_pago).HasMaxLength(100);
            entity.Property(e => e.solicitada_por)
                .HasMaxLength(15)
                .HasDefaultValueSql("'cliente'::character varying");
            entity.Property(e => e.tipo_ciclo).HasMaxLength(10);

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.suscripciones_vips)
                .HasForeignKey(d => d.id_cliente)
                .HasConstraintName("suscripciones_vip_id_cliente_fkey");

            entity.HasOne(d => d.id_planNavigation).WithMany(p => p.suscripciones_vips)
                .HasForeignKey(d => d.id_plan)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("suscripciones_vip_id_plan_fkey");
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.id_usuario).HasName("usuarios_pkey");

            entity.HasIndex(e => new { e.ultima_actividad, e.estado }, "idx_actividad");

            entity.HasIndex(e => new { e.metodo_auth, e.auth_id }, "idx_auth");

            entity.HasIndex(e => e.email, "idx_email");

            entity.HasIndex(e => e.estado, "idx_estado");

            entity.HasIndex(e => e.tipo_usuario, "idx_tipo_usuario");

            entity.HasIndex(e => e.uuid, "idx_uuid");

            entity.HasIndex(e => e.email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.acepto_marketing).HasDefaultValue(false);
            entity.Property(e => e.auth_id).HasMaxLength(100);
            entity.Property(e => e.bloqueo_temporal).HasDefaultValue(false);
            entity.Property(e => e.cambio_contraseña_requerido).HasDefaultValue(false);
            entity.Property(e => e.contrasena).HasMaxLength(255);
            entity.Property(e => e.datos_eliminacion).HasColumnType("jsonb");
            entity.Property(e => e.email).HasMaxLength(100);
            entity.Property(e => e.estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.factor_2fa).HasDefaultValue(false);
            entity.Property(e => e.fecha_actualizacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_bloqueo).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_expiracion_token).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_registro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_suspension).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_ultimo_cambio_contraseña).HasColumnType("timestamp without time zone");
            entity.Property(e => e.intentos_fallidos).HasDefaultValue((short)0);
            entity.Property(e => e.ip_registro).HasMaxLength(45);
            entity.Property(e => e.metodo_auth)
                .HasMaxLength(20)
                .HasDefaultValueSql("'password'::character varying");
            entity.Property(e => e.permisos).HasColumnType("jsonb");
            entity.Property(e => e.salt).HasMaxLength(64);
            entity.Property(e => e.secreto_2fa).HasMaxLength(64);
            entity.Property(e => e.tipo_usuario).HasMaxLength(20);
            entity.Property(e => e.token_verificacion).HasMaxLength(100);
            entity.Property(e => e.ultima_actividad).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ultimo_ip).HasMaxLength(45);
            entity.Property(e => e.ultimo_login).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ultimo_terminos_aceptados).HasColumnType("timestamp without time zone");
            entity.Property(e => e.uuid).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.verificado_email).HasDefaultValue(false);
        });

        modelBuilder.Entity<verificacione>(entity =>
        {
            entity.HasKey(e => e.id_verificacion).HasName("verificaciones_pkey");

            entity.HasIndex(e => e.codigo_verificacion, "idx_verificaciones_codigo");

            entity.HasIndex(e => new { e.tipo_entidad, e.id_entidad }, "idx_verificaciones_entidad");

            entity.HasIndex(e => new { e.estado, e.prioridad }, "idx_verificaciones_estado_prioridad");

            entity.HasIndex(e => new { e.fecha_solicitud, e.valido_hasta }, "idx_verificaciones_fechas");

            entity.HasIndex(e => new { e.valido_hasta, e.renovacion_automatica }, "idx_verificaciones_renovacion");

            entity.HasIndex(e => e.codigo_verificacion, "verificaciones_codigo_verificacion_key").IsUnique();

            entity.Property(e => e.checklist_verificacion).HasColumnType("jsonb");
            entity.Property(e => e.codigo_verificacion).HasMaxLength(20);
            entity.Property(e => e.documentos_hash).HasColumnType("jsonb");
            entity.Property(e => e.documentos_url).HasColumnType("jsonb");
            entity.Property(e => e.estado)
                .HasMaxLength(25)
                .HasDefaultValueSql("'pendiente'::character varying");
            entity.Property(e => e.fecha_asignacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_solicitud)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_ultima_actualizacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.fecha_verificacion).HasColumnType("timestamp without time zone");
            entity.Property(e => e.historial_estados).HasColumnType("jsonb");
            entity.Property(e => e.metodo_pago).HasMaxLength(50);
            entity.Property(e => e.moneda)
                .HasMaxLength(3)
                .HasDefaultValueSql("'USD'::bpchar")
                .IsFixedLength();
            entity.Property(e => e.monto_pagado).HasPrecision(10, 2);
            entity.Property(e => e.nivel_verificacion).HasDefaultValue((short)1);
            entity.Property(e => e.origen_solicitud).HasMaxLength(50);
            entity.Property(e => e.pago_recibido).HasDefaultValue(false);
            entity.Property(e => e.prioridad)
                .HasMaxLength(10)
                .HasDefaultValueSql("'normal'::character varying");
            entity.Property(e => e.puntuacion_riesgo).HasPrecision(5, 2);
            entity.Property(e => e.recordatorio_enviado).HasDefaultValue(false);
            entity.Property(e => e.renovacion_automatica).HasDefaultValue(false);
            entity.Property(e => e.tipo_entidad).HasMaxLength(10);
            entity.Property(e => e.valido_desde).HasColumnType("timestamp without time zone");
            entity.Property(e => e.valido_hasta).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.verificado_porNavigation).WithMany(p => p.verificaciones)
                .HasForeignKey(d => d.verificado_por)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("verificaciones_verificado_por_fkey");
        });

        modelBuilder.Entity<visitas_perfil>(entity =>
        {
            entity.HasKey(e => e.id_visita).HasName("visitas_perfil_pkey");

            entity.ToTable("visitas_perfil");

            entity.HasIndex(e => e.id_cliente, "idx_visitas_cliente");

            entity.HasIndex(e => new { e.fecha_visita, e.dispositivo }, "idx_visitas_fecha_dispositivo");

            entity.HasIndex(e => new { e.id_perfil, e.fecha_visita }, "idx_visitas_perfil_fecha");

            entity.Property(e => e.dispositivo).HasMaxLength(10);
            entity.Property(e => e.fecha_visita)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.ip_visitante).HasMaxLength(45);
            entity.Property(e => e.origen).HasMaxLength(100);
            entity.Property(e => e.region_geografica).HasMaxLength(50);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.visitas_perfils)
                .HasForeignKey(d => d.id_cliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("visitas_perfil_id_cliente_fkey");

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.visitas_perfils)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("visitas_perfil_id_perfil_fkey");
        });

        modelBuilder.Entity<visitas_perfil_actual>(entity =>
        {
            entity.HasKey(e => new { e.id_visita, e.fecha_visita }).HasName("visitas_perfil_actual_pkey");

            entity.ToTable("visitas_perfil_actual");

            entity.Property(e => e.id_visita).HasDefaultValueSql("nextval('visitas_perfil_particionada_id_visita_seq'::regclass)");
            entity.Property(e => e.fecha_visita)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.dispositivo).HasMaxLength(10);
            entity.Property(e => e.ip_visitante).HasMaxLength(45);
            entity.Property(e => e.origen).HasMaxLength(100);
            entity.Property(e => e.region_geografica).HasMaxLength(50);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.visitas_perfil_actuals)
                .HasForeignKey(d => d.id_cliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("visitas_perfil_particionada_id_cliente_fkey");

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.visitas_perfil_actuals)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("visitas_perfil_particionada_id_perfil_fkey");
        });

        modelBuilder.Entity<visitas_perfil_antiguo>(entity =>
        {
            entity.HasKey(e => new { e.id_visita, e.fecha_visita }).HasName("visitas_perfil_antiguos_pkey");

            entity.Property(e => e.id_visita).HasDefaultValueSql("nextval('visitas_perfil_particionada_id_visita_seq'::regclass)");
            entity.Property(e => e.fecha_visita)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.dispositivo).HasMaxLength(10);
            entity.Property(e => e.ip_visitante).HasMaxLength(45);
            entity.Property(e => e.origen).HasMaxLength(100);
            entity.Property(e => e.region_geografica).HasMaxLength(50);
            entity.Property(e => e.user_agent).HasMaxLength(255);

            entity.HasOne(d => d.id_clienteNavigation).WithMany(p => p.visitas_perfil_antiguos)
                .HasForeignKey(d => d.id_cliente)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("visitas_perfil_particionada_id_cliente_fkey");

            entity.HasOne(d => d.id_perfilNavigation).WithMany(p => p.visitas_perfil_antiguos)
                .HasForeignKey(d => d.id_perfil)
                .HasConstraintName("visitas_perfil_particionada_id_perfil_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
