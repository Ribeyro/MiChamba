using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyChamba.Models;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace MyChamba.Data;

public partial class MyChambaContext : DbContext
{
    public MyChambaContext()
    {
    }

    public MyChambaContext(DbContextOptions<MyChambaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<EntregasProyecto> EntregasProyectos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<EstudianteHabilidad> EstudianteHabilidads { get; set; }

    public virtual DbSet<EstudianteIdioma> EstudianteIdiomas { get; set; }

    public virtual DbSet<Habilidade> Habilidades { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<PerfilEstudiante> PerfilEstudiantes { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Recompensa> Recompensas { get; set; }

    public virtual DbSet<RecompensasCertificado> RecompensasCertificados { get; set; }

    public virtual DbSet<RecompensasEconomica> RecompensasEconomicas { get; set; }

    public virtual DbSet<Sector> Sectors { get; set; }

    public virtual DbSet<Solicitude> Solicitudes { get; set; }

    public virtual DbSet<TipoRecompensa> TipoRecompensas { get; set; }

    public virtual DbSet<TipoUsuario> TipoUsuarios { get; set; }

    public virtual DbSet<Universidad> Universidads { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("empresa");

            entity.HasIndex(e => e.IdSector, "fk_empresa_sector");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Direccion)
                .HasColumnType("text")
                .HasColumnName("direccion");
            entity.Property(e => e.IdSector).HasColumnName("id_sector");
            entity.Property(e => e.Logo)
                .HasColumnType("text")
                .HasColumnName("logo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Ruc)
                .HasMaxLength(15)
                .HasColumnName("ruc");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.IdSector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_empresa_sector");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Empresa)
                .HasForeignKey<Empresa>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("empresa_ibfk_1");
        });

        modelBuilder.Entity<EntregasProyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("entregas_proyectos");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.HasIndex(e => e.IdProyecto, "id_proyecto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.EstadoEvaluacion)
                .HasMaxLength(50)
                .HasColumnName("estado_evaluacion");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Link)
                .HasColumnType("text")
                .HasColumnName("link");
            entity.Property(e => e.Rendimiento)
                .HasColumnType("text")
                .HasColumnName("rendimiento");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.EntregasProyectos)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entregas_proyectos_ibfk_1");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.EntregasProyectos)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("entregas_proyectos_ibfk_2");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("estudiante");

            entity.HasIndex(e => e.IdCarrera, "fk_estudiante_carrera");

            entity.HasIndex(e => e.IdUniversidad, "fk_estudiante_universidad");

            entity.Property(e => e.IdUsuario)
                .ValueGeneratedNever()
                .HasColumnName("id_usuario");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.IdCarrera).HasColumnName("id_carrera");
            entity.Property(e => e.IdUniversidad).HasColumnName("id_universidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiante_carrera");

            entity.HasOne(d => d.IdUniversidadNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdUniversidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_estudiante_universidad");

            entity.HasOne(d => d.IdUsuarioNavigation).WithOne(p => p.Estudiante)
                .HasForeignKey<Estudiante>(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_ibfk_1");
        });

        modelBuilder.Entity<EstudianteHabilidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante_habilidad");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.HasIndex(e => e.IdHabilidad, "id_habilidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdHabilidad).HasColumnName("id_habilidad");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.EstudianteHabilidads)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_habilidad_ibfk_1");

            entity.HasOne(d => d.IdHabilidadNavigation).WithMany(p => p.EstudianteHabilidads)
                .HasForeignKey(d => d.IdHabilidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_habilidad_ibfk_2");
        });

        modelBuilder.Entity<EstudianteIdioma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estudiante_idioma");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.HasIndex(e => e.IdIdioma, "id_idioma");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdIdioma).HasColumnName("id_idioma");
            entity.Property(e => e.Nivel)
                .HasMaxLength(50)
                .HasColumnName("nivel");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.EstudianteIdiomas)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_idioma_ibfk_1");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.EstudianteIdiomas)
                .HasForeignKey(d => d.IdIdioma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("estudiante_idioma_ibfk_2");
        });

        modelBuilder.Entity<Habilidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("habilidades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("idiomas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Idioma1)
                .HasMaxLength(50)
                .HasColumnName("idioma");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("links");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.TipoLink)
                .HasMaxLength(50)
                .HasColumnName("tipo_link");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Links)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("links_ibfk_1");
        });

        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notificaciones");

            entity.HasIndex(e => e.IdReceptor, "id_receptor");

            entity.HasIndex(e => e.IdSolicitud, "id_solicitud");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaEnvio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_envio");
            entity.Property(e => e.IdReceptor).HasColumnName("id_receptor");
            entity.Property(e => e.IdSolicitud).HasColumnName("id_solicitud");
            entity.Property(e => e.Leido).HasColumnName("leido");
            entity.Property(e => e.Mensaje)
                .HasColumnType("text")
                .HasColumnName("mensaje");
            entity.Property(e => e.TipoMensaje)
                .HasMaxLength(50)
                .HasColumnName("tipo_mensaje");

            entity.HasOne(d => d.IdReceptorNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdReceptor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones_ibfk_2");

            entity.HasOne(d => d.IdSolicitudNavigation).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.IdSolicitud)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("notificaciones_ibfk_1");
        });

        modelBuilder.Entity<PerfilEstudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("PRIMARY");

            entity.ToTable("perfil_estudiante");

            entity.Property(e => e.IdEstudiante)
                .ValueGeneratedNever()
                .HasColumnName("id_estudiante");
            entity.Property(e => e.AcercaDe)
                .HasColumnType("text")
                .HasColumnName("acerca_de");
            entity.Property(e => e.Avatar)
                .HasColumnType("text")
                .HasColumnName("avatar");

            entity.HasOne(d => d.IdEstudianteNavigation).WithOne(p => p.PerfilEstudiante)
                .HasForeignKey<PerfilEstudiante>(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("perfil_estudiante_ibfk_1");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("proyectos");

            entity.HasIndex(e => e.IdEmpresa, "id_empresa");

            entity.HasIndex(e => e.IdTipoRecompensa, "id_tipo_recompensa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaLimite)
                .HasColumnType("datetime")
                .HasColumnName("fecha_limite");
            entity.Property(e => e.IdEmpresa).HasColumnName("id_empresa");
            entity.Property(e => e.IdTipoRecompensa).HasColumnName("id_tipo_recompensa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroParticipantes).HasColumnName("numero_participantes");

            entity.HasOne(d => d.IdEmpresaNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdEmpresa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proyectos_ibfk_1");

            entity.HasOne(d => d.IdTipoRecompensaNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdTipoRecompensa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("proyectos_ibfk_2");

            entity.HasMany(d => d.IdHabilidads).WithMany(p => p.IdProyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "RetoHabilidad",
                    r => r.HasOne<Habilidade>().WithMany()
                        .HasForeignKey("IdHabilidad")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("reto_habilidad_ibfk_2"),
                    l => l.HasOne<Proyecto>().WithMany()
                        .HasForeignKey("IdProyecto")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("reto_habilidad_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdProyecto", "IdHabilidad")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("reto_habilidad");
                        j.HasIndex(new[] { "IdHabilidad" }, "id_habilidad");
                        j.IndexerProperty<uint>("IdProyecto").HasColumnName("id_proyecto");
                        j.IndexerProperty<uint>("IdHabilidad").HasColumnName("id_habilidad");
                    });
        });

        modelBuilder.Entity<Recompensa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recompensa");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.HasIndex(e => e.IdProyecto, "id_proyecto");

            entity.HasIndex(e => e.IdTipoRecompensa, "id_tipo_recompensa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FechaAsignacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_asignacion");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdTipoRecompensa).HasColumnName("id_tipo_recompensa");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Recompensas)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recompensa_ibfk_3");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Recompensas)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recompensa_ibfk_1");

            entity.HasOne(d => d.IdTipoRecompensaNavigation).WithMany(p => p.Recompensas)
                .HasForeignKey(d => d.IdTipoRecompensa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recompensa_ibfk_2");
        });

        modelBuilder.Entity<RecompensasCertificado>(entity =>
        {
            entity.HasKey(e => e.IdRecompensa).HasName("PRIMARY");

            entity.ToTable("recompensas_certificados");

            entity.Property(e => e.IdRecompensa)
                .ValueGeneratedNever()
                .HasColumnName("id_recompensa");
            entity.Property(e => e.Archivo)
                .HasColumnType("text")
                .HasColumnName("archivo");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("datetime")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdRecompensaNavigation).WithOne(p => p.RecompensasCertificado)
                .HasForeignKey<RecompensasCertificado>(d => d.IdRecompensa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recompensas_certificados_ibfk_1");
        });

        modelBuilder.Entity<RecompensasEconomica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("recompensas_economicas");

            entity.HasIndex(e => e.IdRecompensa, "id_recompensa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.IdRecompensa).HasColumnName("id_recompensa");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.Monto)
                .HasPrecision(8, 2)
                .HasColumnName("monto");

            entity.HasOne(d => d.IdRecompensaNavigation).WithMany(p => p.RecompensasEconomicas)
                .HasForeignKey(d => d.IdRecompensa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("recompensas_economicas_ibfk_1");
        });

        modelBuilder.Entity<Sector>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sector");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Solicitude>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("solicitudes");

            entity.HasIndex(e => e.IdEstudiante, "id_estudiante");

            entity.HasIndex(e => e.IdProyecto, "id_proyecto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .HasColumnName("estado");
            entity.Property(e => e.FechaSolicitud)
                .HasColumnType("datetime")
                .HasColumnName("fecha_solicitud");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.ResumenHabilidades)
                .HasColumnType("text")
                .HasColumnName("resumen_habilidades");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.IdEstudiante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitudes_ibfk_1");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Solicitudes)
                .HasForeignKey(d => d.IdProyecto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("solicitudes_ibfk_2");
        });

        modelBuilder.Entity<TipoRecompensa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipo_recompensa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<TipoUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipo_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Universidad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("universidad");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.HasIndex(e => e.IdTipoUsuario, "id_tipo_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdTipoUsuario).HasColumnName("id_tipo_usuario");
            entity.Property(e => e.Password)
                .HasColumnType("text")
                .HasColumnName("password");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdTipoUsuarioNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTipoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
