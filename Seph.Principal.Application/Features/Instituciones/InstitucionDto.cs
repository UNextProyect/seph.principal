namespace Seph.Principal.Application.Features.Instituciones
{
    /// <summary>
    /// este es un record inmutable que se encarga de transportar los datos de la institucion, 
    /// se utiliza para evitar problemas de concurrencia y para mejorar el rendimiento, 
    /// ya que los records son inmutables y se pueden compartir entre hilos sin problemas de sincronización.
    /// </summary>
    /// <param name="Id">es el identificador de la institución</param>
    /// <param name="StrValor">es el valor de la institución</param>
    /// <param name="StrDescripcion">es la descripción de la institución</param>
    public sealed record InstitucionDto(
     int Id,
     string StrValor,
     string StrDescripcion
 );
}
