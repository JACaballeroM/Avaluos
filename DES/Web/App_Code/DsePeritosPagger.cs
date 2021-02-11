using System;
using ServicePeritosSociedades;
using SIGAPred.Common.WCF;

/// <summary>
/// Clase para la gestión de  consultas sobre peritos/sociedades.
/// </summary>
public class DsePeritosPagger:PaggerBase
{
    /// <summary>
    /// Obtiene la tabla con las sociedades que corresponda en función de los criterios de búqueda.
    /// </summary>
    /// <param name="registro">registro.</param>
    /// <param name="nombre">nombre.</param>
    /// <param name="rfc">RFC.</param>
    /// <returns>
    /// tabla con las filas que cumplen los criterios de búsqueda.
    /// </returns>
    public DsePeritosSociedades.SociedadValuacionDataTable ObtenerBusquedaSociedades(string registro, string nombre, string rfc)
    {
        if (string.IsNullOrEmpty(registro))
        {
            registro = string.Empty;
        }
        if (string.IsNullOrEmpty(nombre))
        {
            nombre = string.Empty;
        }
        if (string.IsNullOrEmpty(rfc))
        {
            rfc = string.Empty;
        }
        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_PERITOS;
        }

        DsePeritosSociedades peritosSocDS = new DsePeritosSociedades();
        PeritosSociedadesClient clienteRCON = new PeritosSociedadesClient();

        try
        {
            if (string.IsNullOrEmpty(registro))
            {
                peritosSocDS = clienteRCON.GetSocValuacionByDatosPersonales(nombre, TipoFiltroLike.Contiene);
            }
            else
            {
                peritosSocDS = clienteRCON.GetSocValuacionByDatosIdentificativos(rfc, registro);
            }
        }
        finally
        {
            clienteRCON.Disconnect();
        }

        return peritosSocDS.SociedadValuacion; 
    }

    /// <summary>
    /// Obtiene la tabla con los peritos que corresponda en función de los criterios de búqueda.
    /// </summary>
    /// <param name="registro">registro.</param>
    /// <param name="nombre">nombre.</param>
    /// <param name="apellidoPaterno">apellido paterno.</param>
    /// <param name="apellidoMaterno">apellido materno.</param>
    /// <param name="rfc">RFC.</param>
    /// <param name="curp">CURP.</param>
    /// <param name="claveife">clave IFE.</param>
    /// <returns>
    /// tabla con las filas que cumplen los criterios de búsqueda.
    /// </returns>
    public DsePeritosSociedades.PeritoDataTable ObtenerBusquedaPeritos(string registro,string nombre,string apellidoPaterno,string apellidoMaterno,string rfc,string curp,string claveife)
    {
        if (string.IsNullOrEmpty(registro))
        {
            registro = string.Empty;
        }
        if (string.IsNullOrEmpty(nombre))
        {
            nombre = string.Empty;
        }
        if (string.IsNullOrEmpty(apellidoPaterno))
        {
            apellidoPaterno = string.Empty;
        }
        if (string.IsNullOrEmpty(apellidoMaterno))
        {
            apellidoMaterno = string.Empty;
        }
        if (string.IsNullOrEmpty(rfc))
        {
            rfc = string.Empty;
        }
        if (string.IsNullOrEmpty(curp))
        {
            curp = string.Empty;
        }
        if (string.IsNullOrEmpty(claveife))
        {
            claveife = string.Empty;
        }
        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_PERITOS;
        }

        DsePeritosSociedades peritosDS = new DsePeritosSociedades();
        PeritosSociedadesClient clienteRCON = new PeritosSociedadesClient();

        try
        {
            if (string.IsNullOrEmpty(registro))
            {
                peritosDS = clienteRCON.GetPeritosByDatosPersonales(nombre, TipoFiltroLike.Contiene, apellidoPaterno, TipoFiltroLike.Contiene, apellidoMaterno, TipoFiltroLike.Contiene);
            }
            else
            {
                peritosDS = clienteRCON.GetPeritosByDatosIdentificativos(rfc, curp, claveife, null, null, registro);
            }
        }
        finally
        {
            clienteRCON.Disconnect();
        }
        
        AnadirColunmaNombreCompleto(ref peritosDS);
        foreach (DsePeritosSociedades.PeritoRow rowPersonas in peritosDS.Perito.Rows)
        {
            if (rowPersonas.APELLIDOMATERNO == null)
            {
                rowPersonas.APELLIDOMATERNO = string.Empty;
            }
            if (rowPersonas.APELLIDOPATERNO == null)
            {
                rowPersonas.APELLIDOPATERNO = string.Empty;
            }
            if (rowPersonas.NOMBRE == null)
            {
                rowPersonas.NOMBRE = string.Empty;
            }
            rowPersonas[Constantes.NOMBRE_COMPLETO_COLUMN] = ComponerNombreCompleto(rowPersonas.NOMBRE, rowPersonas.APELLIDOPATERNO, rowPersonas.APELLIDOMATERNO);
        }

        return peritosDS.Perito;
    }

    /// <summary>
    /// Anadir colunma nombre completo al dataset de peritos
    /// </summary>
    /// <param name="dsPeritos">[in,out] El dataset peritos.</param>
    private void AnadirColunmaNombreCompleto(ref DsePeritosSociedades dsPeritos)
    {
        Type typeString = typeof(string);
        dsPeritos.Perito.Columns.Add(Constantes.NOMBRE_COMPLETO_COLUMN, typeString);
        dsPeritos.Perito.Columns[Constantes.NOMBRE_COMPLETO_COLUMN].MaxLength = dsPeritos.Perito.APELLIDOMATERNOColumn.MaxLength + dsPeritos.Perito.APELLIDOPATERNOColumn.MaxLength + dsPeritos.Perito.NOMBREColumn.MaxLength + 3;
    }

    /// <summary>
    /// Componer nombre completo.
    /// </summary>
    /// <param name="nombre">Nombre.</param>
    /// <param name="app1">Apellido Paterno.</param>
    /// <param name="app2">Apellido Materno.</param>
    /// <returns>
    /// Una cadena de texto que contiene el nombre completo, es decir nombre y apellidos
    /// </returns>
    private string ComponerNombreCompleto(string nombre, string app1, string app2)
    {
        string nombreCompleto = string.Empty;
        //nombreCompleto = apellido Paterno apellido Materno, Nombre
        nombreCompleto = app1 + Constantes.ESPACIO_BLANCO + app2 + ", " + nombre;
        return nombreCompleto;
    }

    
}
