using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Infraestructure.Identity;

namespace Seph.Principal.Infraestructure.Persistence
{
    public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options), IApplicationDbContext, IUnitOfWork
    {
        public DbSet<RefreshTokenSession> RefreshTokenSessions => Set<RefreshTokenSession>();
        public DbSet<Institucion> Instituciones => Set<Institucion>();
        public DbSet<Empleado> Empleados => Set<Empleado>();
        public DbSet<CatSexo> CatSexo => Set<CatSexo>();
        public DbSet<CatTipoPersonal> CatTipoPersonal => Set<CatTipoPersonal>();
        public DbSet<CatTipoContrato> CatTipoContratos => Set<CatTipoContrato>();
        public DbSet<CatArea> CatAreas => Set<CatArea>();
        public DbSet<CatPerfilAcademico> CatPerfilesAcademicos => Set<CatPerfilAcademico>();
        public DbSet<CatMunicipio> CatMunicipios => Set<CatMunicipio>();
        public DbSet<CatNivelAcademico> CatNivelAcademicos => Set<CatNivelAcademico>();
        public DbSet<CatDiscapacitado> CatDiscapacitados => Set<CatDiscapacitado>();
        public DbSet<CatInternet> CatInternets => Set<CatInternet>();
        public DbSet<CatMecanismoSeguimiento> CatMecanismos => Set<CatMecanismoSeguimiento>();
        public DbSet<CatSectorVinculado> CatSectores => Set<CatSectorVinculado>();

        public DbSet<MapUserPerfilAcademico> MapUserPerfilesAcademicos => Set<MapUserPerfilAcademico>();
        public DbSet<MapEmpleadoPerfilAcademico> MapEmpleadoPerfilesAcademicos => Set<MapEmpleadoPerfilAcademico>();
        public DbSet<HistorialContrato> HistorialContratos => Set<HistorialContrato>();
        // Catálogo de periodos disponibles para captura.
        public DbSet<CatPeriodo> CatPeriodos => Set<CatPeriodo>();
        // Catálogo de tipos de periodo.
        public DbSet<CatTipoPeriodo> CatTiposPeriodo => Set<CatTipoPeriodo>();
        // Relación entre institución y periodo habilitado.
        public DbSet<MapInstitucionPeriodo> MapInstitucionPeriodos => Set<MapInstitucionPeriodo>();
        // Reporte de matrícula registrado por institución y periodo.
        public DbSet<ReporteMatricula> ReporteMatriculas => Set<ReporteMatricula>();
        // Reporte de personal registrado por institución y periodo.
        public DbSet<ReportePersonal> ReportePersonales => Set<ReportePersonal>();
        // Reporte de infraestructura registrado por institución y periodo.
        public DbSet<ReporteInfraestructura> ReporteInfraestructuras => Set<ReporteInfraestructura>();
        public DbSet<ReporteVinculacion> ReporteVinculaciones => Set<ReporteVinculacion>();
        public DbSet<SectorVinculadoVinculacion> SectorVinculadoVinculaciones => Set<SectorVinculadoVinculacion>();
        public DbSet<EmailVerificationCode> EmailVerificationCodes => Set<EmailVerificationCode>();
        public DbSet<ReporteFinanza> ReporteFinanzas => Set<ReporteFinanza>();
        public DbSet<ProyectoFinanciado> ProyectoFinanciados => Set<ProyectoFinanciado>();
        // Catálogo de preguntas para el análisis estratégico.
        public DbSet<CatPreguntaAnalisis> CatPreguntasAnalisis => Set<CatPreguntaAnalisis>();
        // Reportes de análisis estratégico por institución y periodo.
        public DbSet<ReporteAnalisisEstrategico> ReporteAnalisisEstrategicos => Set<ReporteAnalisisEstrategico>();
        // Respuestas registradas dentro del análisis estratégico.
        public DbSet<RespuestaAnalisis> RespuestasAnalisis => Set<RespuestaAnalisis>();


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            builder.Entity<ApplicationRole>(b =>
            {
                b.Ignore("CreatedBy");
                b.Ignore("UpdatedBy");
                b.Ignore("CreatedAtUtc");
                b.Ignore("UpdatedAtUtc");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}