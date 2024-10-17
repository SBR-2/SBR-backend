using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SBRSystem_Data.Models;

namespace SBRSystem_Data.Context;

public partial class MySBRDbContext : DbContext
{
    public MySBRDbContext()
    {
    }

    public MySBRDbContext(DbContextOptions<MySBRDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BpmCategorium> BpmCategoria { get; set; }

    public virtual DbSet<BpmSubcategorium> BpmSubcategoria { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<ComentarioDocumento> ComentarioDocumentos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<Entidad> Entidads { get; set; }

    public virtual DbSet<Establecimiento> Establecimientos { get; set; }

    public virtual DbSet<EstadoFisico> EstadoFisicos { get; set; }

    public virtual DbSet<Factor> Factors { get; set; }

    public virtual DbSet<Ficha> Fichas { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<MatizRiesgo> MatizRiesgos { get; set; }

    public virtual DbSet<MercadoObjetivo> MercadoObjetivos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoEntidad> ProductoEntidads { get; set; }

    public virtual DbSet<Provincium> Provincia { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Relacion> Relacions { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    public virtual DbSet<Riesgo> Riesgos { get; set; }

    public virtual DbSet<RiesgoCategorium> RiesgoCategoria { get; set; }

    public virtual DbSet<RiesgoSubcategorium> RiesgoSubcategoria { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Solicitud> Solicituds { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Valor> Valors { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum("estado_proceso", new[] { "En proceso", "Rechazada", "Aceptada", "archivado" });

        modelBuilder.Entity<BpmCategorium>(entity =>
        {
            entity.HasKey(e => e.BpmCategoriaId).HasName("bpm_categoria_pkey");

            entity.ToTable("bpm_categoria");

            entity.HasIndex(e => e.Nombre, "bpm_categoria_unique").IsUnique();

            entity.Property(e => e.BpmCategoriaId)
                .HasDefaultValueSql("nextval('bpm_categoria_id_seq'::regclass)")
                .HasColumnName("bpm_categoria_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.GrupoId).HasColumnName("grupo_id");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");

            entity.HasOne(d => d.Grupo).WithMany(p => p.BpmCategoria)
                .HasForeignKey(d => d.GrupoId)
                .HasConstraintName("bpm_categoria_grupo_id_fk");
        });

        modelBuilder.Entity<BpmSubcategorium>(entity =>
        {
            entity.HasKey(e => e.BpmSubcategoriaId).HasName("bpm_subcategoria_pkey");

            entity.ToTable("bpm_subcategoria");

            entity.HasIndex(e => e.Nombre, "bpm_subcategoria_unique").IsUnique();

            entity.Property(e => e.BpmSubcategoriaId)
                .HasDefaultValueSql("nextval('bpm_subcategoria_id_seq'::regclass)")
                .HasColumnName("bpm_subcategoria_id");
            entity.Property(e => e.BpmCategoriaId).HasColumnName("bpm_categoria_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");

            entity.HasOne(d => d.BpmCategoria).WithMany(p => p.BpmSubcategoria)
                .HasForeignKey(d => d.BpmCategoriaId)
                .HasConstraintName("bpm_subcategoria_bpm_categoria_id_fk");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.ComentarioId).HasName("comentarios_pkey");

            entity.ToTable("comentarios");

            entity.Property(e => e.ComentarioId)
                .HasDefaultValueSql("nextval('comentario_seq'::regclass)")
                .HasColumnName("comentario_id");
            entity.Property(e => e.Detalle)
                .HasColumnType("character varying")
                .HasColumnName("detalle");
            entity.Property(e => e.FechaCumplimiento)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cumplimiento");
            entity.Property(e => e.FichaId).HasColumnName("ficha_id");
            entity.Property(e => e.Numero).HasColumnName("numero");

            entity.HasOne(d => d.Ficha).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.FichaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ficha_id");
        });

        modelBuilder.Entity<ComentarioDocumento>(entity =>
        {
            entity.HasKey(e => e.ComentarioDocumentoId).HasName("comentario_documento_pkey");

            entity.ToTable("comentario_documento");

            entity.Property(e => e.ComentarioDocumentoId)
                .HasDefaultValueSql("nextval('comentario_documento_seq'::regclass)")
                .HasColumnName("comentario_documento_id");
            entity.Property(e => e.DocumentoId).HasColumnName("documento_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Documento).WithMany(p => p.ComentarioDocumentos)
                .HasForeignKey(d => d.DocumentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("documento_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.ComentarioDocumentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuario_id");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.DocumentoId).HasName("documento_pkey");

            entity.ToTable("documento");

            entity.Property(e => e.DocumentoId)
                .HasDefaultValueSql("nextval('documento_id_seq'::regclass)")
                .HasColumnName("documento_id");
            entity.Property(e => e.Estado)
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Ruta)
                .HasColumnType("character varying")
                .HasColumnName("ruta");
            entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");
            entity.Property(e => e.TipoDocumentoId).HasColumnName("tipo_documento_id");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.SolicitudId)
                .HasConstraintName("documento_solicitud_id_fk");

            entity.HasOne(d => d.TipoDocumento).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.TipoDocumentoId)
                .HasConstraintName("documento_tipo_documento_id_fk");
        });

        modelBuilder.Entity<Entidad>(entity =>
        {
            entity.HasKey(e => e.EntidadId).HasName("entidad_pkey");

            entity.ToTable("entidad");

            entity.Property(e => e.EntidadId)
                .HasDefaultValueSql("nextval('entidad_id_seq'::regclass)")
                .HasColumnName("entidad_id");
            entity.Property(e => e.Cedula)
                .HasColumnType("character varying")
                .HasColumnName("cedula");
            entity.Property(e => e.Correo)
                .HasColumnType("character varying")
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasColumnType("character varying")
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Rnc)
                .HasColumnType("character varying")
                .HasColumnName("rnc");
            entity.Property(e => e.Telefono)
                .HasColumnType("character varying")
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Establecimiento>(entity =>
        {
            entity.HasKey(e => e.EstablecimientoId).HasName("establecimiento_pkey");

            entity.ToTable("establecimiento");

            entity.Property(e => e.EstablecimientoId)
                .HasDefaultValueSql("nextval('establecimiento_id_seq'::regclass)")
                .HasColumnName("establecimiento_id");
            entity.Property(e => e.CalUltimaInspeccion).HasColumnName("cal_ultima_inspeccion");
            entity.Property(e => e.Comercializacion)
                .HasColumnType("character varying")
                .HasColumnName("comercializacion");
            entity.Property(e => e.InicioOperaciones)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("inicio_operaciones");
            entity.Property(e => e.MercadoObjetivoId).HasColumnName("mercado_objetivo_id");
            entity.Property(e => e.MunicipioId).HasColumnName("municipio_id");
            entity.Property(e => e.NoSanitario)
                .HasColumnType("character varying")
                .HasColumnName("no_sanitario");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.NombreDigemaps)
                .HasColumnType("character varying")
                .HasColumnName("nombre_digemaps");
            entity.Property(e => e.NombreDps)
                .HasColumnType("character varying")
                .HasColumnName("nombre_dps");
            entity.Property(e => e.NumEmpleados).HasColumnName("num_empleados");
            entity.Property(e => e.NumProductosElaborados).HasColumnName("num_productos_elaborados");
            entity.Property(e => e.Numero)
                .HasColumnType("character varying")
                .HasColumnName("numero");
            entity.Property(e => e.ProduccionAnual).HasColumnName("produccion_anual");
            entity.Property(e => e.Rnc)
                .HasColumnType("character varying")
                .HasColumnName("rnc");
            entity.Property(e => e.Telefono)
                .HasColumnType("character varying")
                .HasColumnName("telefono");
            entity.Property(e => e.UltimaInspeccion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("ultima_inspeccion");
            entity.Property(e => e.VencimientoSanitario)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("vencimiento_sanitario");

            entity.HasOne(d => d.MercadoObjetivo).WithMany(p => p.Establecimientos)
                .HasForeignKey(d => d.MercadoObjetivoId)
                .HasConstraintName("mercado_objetivo_id");

            entity.HasOne(d => d.Municipio).WithMany(p => p.Establecimientos)
                .HasForeignKey(d => d.MunicipioId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("establecimiento_municipio_fk");
        });

        modelBuilder.Entity<EstadoFisico>(entity =>
        {
            entity.HasKey(e => e.EstadoFisicoId).HasName("estado_fisico_pkey");

            entity.ToTable("estado_fisico");

            entity.HasIndex(e => e.EstadoFisico1, "estado_fisico_unique").IsUnique();

            entity.Property(e => e.EstadoFisicoId)
                .HasDefaultValueSql("nextval('estado_fisico_id_seq'::regclass)")
                .HasColumnName("estado_fisico_id");
            entity.Property(e => e.EstadoFisico1)
                .HasColumnType("character varying")
                .HasColumnName("estado_fisico");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
        });

        modelBuilder.Entity<Factor>(entity =>
        {
            entity.HasKey(e => e.FactorId).HasName("factor_pkey");

            entity.ToTable("factor");

            entity.Property(e => e.FactorId)
                .HasDefaultValueSql("nextval('factor_id_seq'::regclass)")
                .HasColumnName("factor_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FactorNombre)
                .HasColumnType("character varying")
                .HasColumnName("factor_nombre");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Peso).HasColumnName("peso");
        });

        modelBuilder.Entity<Ficha>(entity =>
        {
            entity.HasKey(e => e.FichaId).HasName("ficha_pkey");

            entity.ToTable("ficha");

            entity.Property(e => e.FichaId)
                .HasDefaultValueSql("nextval('ficha_id_seq'::regclass)")
                .HasColumnName("ficha_id");
            entity.Property(e => e.AprobadorId).HasColumnName("aprobador_id");
            entity.Property(e => e.Calificacion).HasColumnName("calificacion");
            entity.Property(e => e.EstablecimientoId).HasColumnName("establecimiento_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.EvaluadorId).HasColumnName("evaluador_id");
            entity.Property(e => e.FechaAprobacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_aprobacion");
            entity.Property(e => e.FechaElaboracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_elaboracion");
            entity.Property(e => e.FechaRevision)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_revision");
            entity.Property(e => e.InspectorId).HasColumnName("inspector_id");
            entity.Property(e => e.Latitud)
                .HasColumnType("character varying")
                .HasColumnName("latitud");
            entity.Property(e => e.Longitud)
                .HasColumnType("character varying")
                .HasColumnName("longitud");
            entity.Property(e => e.MatizRiesgo).HasColumnName("matiz_riesgo");
            entity.Property(e => e.NombreDigemaps)
                .HasColumnType("character varying")
                .HasColumnName("nombre_digemaps");
            entity.Property(e => e.NombreDps)
                .HasColumnType("character varying")
                .HasColumnName("nombre_dps");
            entity.Property(e => e.RevisorId).HasColumnName("revisor_id");
            entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");

            entity.HasOne(d => d.Aprobador).WithMany(p => p.FichaAprobadors)
                .HasForeignKey(d => d.AprobadorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ficha_usuario_fk_2");

            entity.HasOne(d => d.Establecimiento).WithMany(p => p.Fichas)
                .HasForeignKey(d => d.EstablecimientoId)
                .HasConstraintName("ficha_establecimiento_id_fk");

            entity.HasOne(d => d.Evaluador).WithMany(p => p.FichaEvaluadors)
                .HasForeignKey(d => d.EvaluadorId)
                .HasConstraintName("ficha_usuario_fk3");

            entity.HasOne(d => d.Inspector).WithMany(p => p.FichaInspectors)
                .HasForeignKey(d => d.InspectorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ficha_usuario_fk");

            entity.HasOne(d => d.MatizRiesgoNavigation).WithMany(p => p.Fichas)
                .HasForeignKey(d => d.MatizRiesgo)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("matiz_riesgo_fk");

            entity.HasOne(d => d.Revisor).WithMany(p => p.FichaRevisors)
                .HasForeignKey(d => d.RevisorId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("ficha_usuario_fk_1");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.Fichas)
                .HasForeignKey(d => d.SolicitudId)
                .HasConstraintName("ficha_solicitud_id_fk");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.GrupoId).HasName("grupo_pkey");

            entity.ToTable("grupo");

            entity.Property(e => e.GrupoId)
                .HasDefaultValueSql("nextval('grupo_id_seq'::regclass)")
                .HasColumnName("grupo_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<MatizRiesgo>(entity =>
        {
            entity.HasKey(e => e.MatizRiesgoId).HasName("matiz_riesgo_id");

            entity.ToTable("matiz_riesgo",
                tb => tb.HasComment(
                    "Periodicidad en base al riesgo total en que frecuencia se tiene que evaluar nuevamente "));

            entity.Property(e => e.MatizRiesgoId)
                .HasDefaultValueSql("nextval('matiz_riesgo_seq'::regclass)")
                .HasColumnName("matiz_riesgo_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<MercadoObjetivo>(entity =>
        {
            entity.HasKey(e => e.MercadoObjetivoId).HasName("mercado_objetivo_pkey");

            entity.ToTable("mercado_objetivo");

            entity.Property(e => e.MercadoObjetivoId)
                .HasDefaultValueSql("nextval('mercado_objetivo_seq'::regclass)")
                .HasColumnName("mercado_objetivo_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.MercadoNombre)
                .HasColumnType("character varying")
                .HasColumnName("mercado_nombre");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.MunicipioId).HasName("municipio_pkey");

            entity.ToTable("municipio");

            entity.Property(e => e.MunicipioId)
                .HasDefaultValueSql("nextval('municipio_seq'::regclass)")
                .HasColumnName("municipio_id");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Municipio1)
                .HasColumnType("character varying")
                .HasColumnName("municipio");
            entity.Property(e => e.ProvinciaId).HasColumnName("provincia_id");

            entity.HasOne(d => d.Provincia).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.ProvinciaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("municipio_provincia_fk");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("opcion_pkey");

            entity.ToTable("opcion");

            entity.Property(e => e.OpcionId)
                .HasDefaultValueSql("nextval('opcion_id_seq'::regclass)")
                .HasColumnName("opcion_id");
            entity.Property(e => e.Detalle)
                .HasColumnType("character varying")
                .HasColumnName("detalle");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FactorId).HasColumnName("factor_id");
            entity.Property(e => e.Valor)
                .HasColumnType("character varying")
                .HasColumnName("valor");

            entity.HasOne(d => d.Factor).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.FactorId)
                .HasConstraintName("opcion_factor_id_fk");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.PreguntaId).HasName("pregunta_pkey");

            entity.ToTable("pregunta");

            entity.HasIndex(e => e.Nombre, "pregunta_unique").IsUnique();

            entity.Property(e => e.PreguntaId)
                .HasDefaultValueSql("nextval('pregunta_id_seq'::regclass)")
                .HasColumnName("pregunta_id");
            entity.Property(e => e.BpmSubcategoriaId).HasColumnName("bpm_subcategoria_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");

            entity.HasOne(d => d.BpmSubcategoria).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.BpmSubcategoriaId)
                .HasConstraintName("pregunta_bpm_subcategoria_id_fk");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("producto_pkey");

            entity.ToTable("producto");

            entity.Property(e => e.ProductoId)
                .HasDefaultValueSql("nextval('producto_id_seq'::regclass)")
                .HasColumnName("producto_id");
            entity.Property(e => e.EnvasePrimario)
                .HasColumnType("character varying")
                .HasColumnName("envase_primario");
            entity.Property(e => e.Estado)
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.EstadoFisicoId).HasColumnName("estado_fisico_id");
            entity.Property(e => e.Marca)
                .HasColumnType("character varying")
                .HasColumnName("marca");
            entity.Property(e => e.MaterialEmpaque)
                .HasColumnType("character varying")
                .HasColumnName("material_empaque");
            entity.Property(e => e.Nacional).HasColumnName("nacional");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.Origen)
                .HasColumnType("character varying")
                .HasColumnName("origen");
            entity.Property(e => e.Presentaciones)
                .HasColumnType("character varying")
                .HasColumnName("presentaciones");
            entity.Property(e => e.RiesgoSubcategoriaId).HasColumnName("riesgo_subcategoria_id");
            entity.Property(e => e.UnIngrediente).HasColumnName("un_ingrediente");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.EstadoFisico).WithMany(p => p.Productos)
                .HasForeignKey(d => d.EstadoFisicoId)
                .HasConstraintName("producto_estado_físico_id_fk");

            entity.HasOne(d => d.RiesgoSubcategoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.RiesgoSubcategoriaId)
                .HasConstraintName("producto_subcategoria_id_fk");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Productos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("producto_usuario_id_fk");
        });

        modelBuilder.Entity<ProductoEntidad>(entity =>
        {
            entity.HasKey(e => new { e.ProductoId, e.EntidadId }).HasName("producto_entidad_pkey");

            entity.ToTable("producto_entidad");

            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.EntidadId).HasColumnName("entidad_id");
            entity.Property(e => e.RelacionId).HasColumnName("relacion_id");

            entity.HasOne(d => d.Entidad).WithMany(p => p.ProductoEntidads)
                .HasForeignKey(d => d.EntidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("producto_entidad_entidad_id_fk");

            entity.HasOne(d => d.Producto).WithMany(p => p.ProductoEntidads)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("producto_entidad_producto_id_fk");

            entity.HasOne(d => d.Relacion).WithMany(p => p.ProductoEntidads)
                .HasForeignKey(d => d.RelacionId)
                .HasConstraintName("producto_entidad_relacion_id_fk");
        });

        modelBuilder.Entity<Provincium>(entity =>
        {
            entity.HasKey(e => e.ProvinciaId).HasName("provincia_pkey");

            entity.ToTable("provincia");

            entity.Property(e => e.ProvinciaId)
                .HasDefaultValueSql("nextval('provincia_seq'::regclass)")
                .HasColumnName("provincia_id");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.Provincia)
                .HasColumnType("character varying")
                .HasColumnName("provincia");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.RegistoId).HasName("registro_pkey");

            entity.ToTable("registro");

            entity.Property(e => e.RegistoId)
                .HasDefaultValueSql("nextval('registro_id_seq'::regclass)")
                .HasColumnName("registo_id");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.RutaArchivo)
                .HasColumnType("character varying")
                .HasColumnName("ruta_archivo");

            entity.HasOne(d => d.Producto).WithMany(p => p.Registros)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("registro_producto_id_fk");
        });

        modelBuilder.Entity<Relacion>(entity =>
        {
            entity.HasKey(e => e.RelacionId).HasName("relacion_pkey");

            entity.ToTable("relacion");

            entity.Property(e => e.RelacionId)
                .HasDefaultValueSql("nextval('relacion_id_seq'::regclass)")
                .HasColumnName("relacion_id");
            entity.Property(e => e.RelacionTipo)
                .HasColumnType("character varying")
                .HasColumnName("relacion_tipo");
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.RespuestaId).HasName("respuesta_pkey");

            entity.ToTable("respuesta");

            entity.Property(e => e.RespuestaId)
                .HasDefaultValueSql("nextval('respuesta_id_seq'::regclass)")
                .HasColumnName("respuesta_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FichaId).HasColumnName("ficha_id");
            entity.Property(e => e.Observacion)
                .HasColumnType("character varying")
                .HasColumnName("observacion");
            entity.Property(e => e.PreguntaId).HasColumnName("pregunta_id");
            entity.Property(e => e.ValorId).HasColumnName("valor_id");

            entity.HasOne(d => d.Ficha).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.FichaId)
                .HasConstraintName("respuesta_ficha_id_fk");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.PreguntaId)
                .HasConstraintName("respuesta_pregunta_id_fk");

            entity.HasOne(d => d.Valor).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.ValorId)
                .HasConstraintName("respuesta_valor_id_fk");
        });

        modelBuilder.Entity<Riesgo>(entity =>
        {
            entity.HasKey(e => e.RiesgoId).HasName("riesgo_pkey");

            entity.ToTable("riesgo");

            entity.HasIndex(e => e.Riesgo1, "riesgo_unique").IsUnique();

            entity.Property(e => e.RiesgoId)
                .HasDefaultValueSql("nextval('riesgo_id_seq'::regclass)")
                .HasColumnName("riesgo_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Riesgo1)
                .HasColumnType("character varying")
                .HasColumnName("riesgo");
        });

        modelBuilder.Entity<RiesgoCategorium>(entity =>
        {
            entity.HasKey(e => e.RiesgoCategoriaId).HasName("riesgo_categoria_pkey");

            entity.ToTable("riesgo_categoria");

            entity.Property(e => e.RiesgoCategoriaId)
                .HasDefaultValueSql("nextval('riesgo_categoria_id_seq'::regclass)")
                .HasColumnName("riesgo_categoria_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.RiesgoCategoria)
                .HasColumnType("character varying")
                .HasColumnName("riesgo_categoria");
        });

        modelBuilder.Entity<RiesgoSubcategorium>(entity =>
        {
            entity.HasKey(e => e.RiesgoSubcategoriaId).HasName("riesgo_subcategoria_pkey");

            entity.ToTable("riesgo_subcategoria");

            entity.Property(e => e.RiesgoSubcategoriaId)
                .HasDefaultValueSql("nextval('riesgo_subcategoria_id_seq'::regclass)")
                .HasColumnName("riesgo_subcategoria_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.RiesgoCategoriaId).HasColumnName("riesgo_categoria_id");
            entity.Property(e => e.RiesgoId).HasColumnName("riesgo_id");
            entity.Property(e => e.RiesgoSubcategoria)
                .HasColumnType("character varying")
                .HasColumnName("riesgo_subcategoria");

            entity.HasOne(d => d.RiesgoCategoria).WithMany(p => p.RiesgoSubcategoria)
                .HasForeignKey(d => d.RiesgoCategoriaId)
                .HasConstraintName("riesgo_subcategoria_categoria_id_fk");

            entity.HasOne(d => d.Riesgo).WithMany(p => p.RiesgoSubcategoria)
                .HasForeignKey(d => d.RiesgoId)
                .HasConstraintName("riesgo_subcategoria_riesgo_id_fk");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("rol_pkey");

            entity.ToTable("rol");

            entity.Property(e => e.RolId)
                .HasDefaultValueSql("nextval('rol_id_seq'::regclass)")
                .HasColumnType("character varying")
                .HasColumnName("rol_id");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Rol1)
                .HasColumnType("character varying")
                .HasColumnName("rol");
        });

        modelBuilder.Entity<Solicitud>(entity =>
        {
            entity.HasKey(e => e.SolicitudId).HasName("solicitud_pkey");

            entity.ToTable("solicitud");

            entity.Property(e => e.SolicitudId)
                .HasDefaultValueSql("nextval('solicitud_id_seq'::regclass)")
                .HasColumnName("solicitud_id");
            entity.Property(e => e.AcondicionadorDistinto).HasColumnName("acondicionador_distinto");
            entity.Property(e => e.EsExportado).HasColumnName("es_exportado");
            entity.Property(e => e.Estado)
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaRechazo)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_rechazo");
            entity.Property(e => e.Observaciones)
                .HasColumnType("character varying")
                .HasColumnName("observaciones");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.RiesgoTotal).HasColumnName("riesgo_total");
            entity.Property(e => e.TitularFabricante).HasColumnName("titular_fabricante");
            entity.Property(e => e.TitularRepresentacion).HasColumnName("titular_representacion");

            entity.HasOne(d => d.Producto).WithMany(p => p.Solicituds)
                .HasForeignKey(d => d.ProductoId)
                .HasConstraintName("solicitud_producto_id_fk");

            entity.HasMany(d => d.Opcions).WithMany(p => p.Solicituds)
                .UsingEntity<Dictionary<string, object>>(
                    "SolicitudOpcion",
                    r => r.HasOne<Opcion>().WithMany()
                        .HasForeignKey("OpcionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("solicitud_opcion_opcion_id_fk"),
                    l => l.HasOne<Solicitud>().WithMany()
                        .HasForeignKey("SolicitudId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("solicitud_opcion_solicitud_id_fk"),
                    j =>
                    {
                        j.HasKey("SolicitudId", "OpcionId").HasName("solicitud_opcion_pkey");
                        j.ToTable("solicitud_opcion");
                        j.IndexerProperty<int>("SolicitudId").HasColumnName("solicitud_id");
                        j.IndexerProperty<int>("OpcionId").HasColumnName("opcion_id");
                    });
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.TipoDocumentoId).HasName("tipo_documento_pkey");

            entity.ToTable("tipo_documento");

            entity.Property(e => e.TipoDocumentoId)
                .HasDefaultValueSql("nextval('tipo_documento_id_seq'::regclass)")
                .HasColumnName("tipo_documento_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.TipoDocumento1)
                .HasColumnType("character varying")
                .HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Correo, "usuario_unique").IsUnique();

            entity.Property(e => e.UsuarioId)
                .HasDefaultValueSql("nextval('usuario_id_seq'::regclass)")
                .HasColumnName("usuario_id");
            entity.Property(e => e.Correo)
                .HasColumnType("character varying")
                .HasColumnName("correo");
            entity.Property(e => e.EntidadId).HasColumnName("entidad_id");
            entity.Property(e => e.Estado)
                .HasColumnType("character varying")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Hash)
                .HasColumnType("character varying")
                .HasColumnName("hash");
            entity.Property(e => e.Nombre)
                .HasColumnType("character varying")
                .HasColumnName("nombre");
            entity.Property(e => e.RolId)
                .HasColumnType("character varying")
                .HasColumnName("rol_id");
            entity.Property(e => e.Salt)
                .HasColumnType("character varying")
                .HasColumnName("salt");

            entity.HasOne(d => d.Entidad).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.EntidadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entidad_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("usuario_rol_id_fk");
        });

        modelBuilder.Entity<Valor>(entity =>
        {
            entity.HasKey(e => e.ValorId).HasName("valor_pkey");

            entity.ToTable("valor");

            entity.Property(e => e.ValorId)
                .HasDefaultValueSql("nextval('valor_id_seq'::regclass)")
                .HasColumnName("valor_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("character varying")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NomenclaturaValor)
                .HasColumnType("character varying")
                .HasColumnName("nomenclatura_valor");
            entity.Property(e => e.Puntos)
                .HasPrecision(2, 1)
                .HasColumnName("puntos");
        });
        modelBuilder.HasSequence("bpm_categoria_id_seq");
        modelBuilder.HasSequence("bpm_subcategoria_id_seq");
        modelBuilder.HasSequence("comentario_documento_seq");
        modelBuilder.HasSequence("comentario_seq");
        modelBuilder.HasSequence("documento_id_seq");
        modelBuilder.HasSequence("entidad_id_seq");
        modelBuilder.HasSequence("establecimiento_id_seq");
        modelBuilder.HasSequence("estado_fisico_id_seq");
        modelBuilder.HasSequence("factor_id_seq");
        modelBuilder.HasSequence("ficha_id_seq");
        modelBuilder.HasSequence("grupo_id_seq");
        modelBuilder.HasSequence("matiz_riesgo_seq");
        modelBuilder.HasSequence("mercado_objetivo_seq");
        modelBuilder.HasSequence("municipio_seq");
        modelBuilder.HasSequence("opcion_id_seq");
        modelBuilder.HasSequence("pregunta_id_seq");
        modelBuilder.HasSequence("producto_id_seq");
        modelBuilder.HasSequence("provincia_seq");
        modelBuilder.HasSequence("registro_id_seq");
        modelBuilder.HasSequence("relacion_id_seq");
        modelBuilder.HasSequence("respuesta_id_seq");
        modelBuilder.HasSequence("riesgo_categoria_id_seq");
        modelBuilder.HasSequence("riesgo_id_seq");
        modelBuilder.HasSequence("riesgo_subcategoria_id_seq");
        modelBuilder.HasSequence("rol_id_seq");
        modelBuilder.HasSequence("solicitud_id_seq");
        modelBuilder.HasSequence("solicitud_opcion_id_seq");
        modelBuilder.HasSequence("tipo_documento_id_seq");
        modelBuilder.HasSequence("usuario_id_seq");
        modelBuilder.HasSequence("valor_id_seq");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}