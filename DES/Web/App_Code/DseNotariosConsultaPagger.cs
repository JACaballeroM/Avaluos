using ServiceAvaluos;
using SIGAPred.Common.WCF;

/// <summary>
/// Clase para la gestión de consultas sobre notarios
/// </summary>
public class DseNotariosConsultaPagger : PaggerBase
{
    /// <summary>
    /// Número total de filas según notarios
    /// </summary>
    /// <param name="numero">número</param>
    /// <param name="nombre">nombre</param>
    /// <param name="apellidoPaterno">apellido paterno</param>
    /// <param name="apellidoMaterno">apellido materno</param>
    /// <param name="rfc">RFC</param>
    /// <param name="curp">CURP</param>
    /// <param name="ife">IFE</param>
    /// <param name="pageSize">tamaño página</param>
    /// <param name="indice">índice</param>
    /// <param name="SortExpression">criterio de ordenación</param>
    /// <returns>entero que indica el número de filas devueltas</returns>
    public int NumTotalFilasNotarios(decimal? numero, string nombre, string apellidoPaterno, string apellidoMaterno, string rfc, string curp, string ife, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Tabla con los notarios obtenidos según los criterios de búsqueda
    /// </summary>
    /// <param name="numero">número</param>
    /// <param name="nombre">nombre</param>
    /// <param name="apellidoPaterno">apellido paterno</param>
    /// <param name="apellidoMaterno">apellido materno</param>
    /// <param name="rfc">RFC</param>
    /// <param name="curp">CURP</param>
    /// <param name="ife">IFE</param>
    /// <param name="pageSize">tamaño página</param>
    /// <param name="indice">índice</param>
    /// <param name="SortExpression">criterio de ordenación</param>
    /// <returns>tabla con las filas que cumplen los criterios de búsqueda</returns>
    public DseNotarios.FEXAVA_NOTARIOSDataTable ObtenerNotariosPorBusqueda(decimal? numero, string nombre, string apellidoPaterno, string apellidoMaterno, string rfc, string curp, string ife, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = "NUMERO DESC";
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
        if (string.IsNullOrEmpty(ife))
        {
            ife = string.Empty;
        }

        DseNotarios dse = null;
        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            dse = clienteAvaluos.BusquedaNotarios(numero, nombre, apellidoPaterno, apellidoMaterno, rfc, curp, ife, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }

        foreach (DseNotarios.FEXAVA_NOTARIOSRow rowPersonas in dse.FEXAVA_NOTARIOS.Rows)
        {
            if (!rowPersonas.IsNOMBREAPELLIDOSNull())
            {
                if (rowPersonas.NOMBREAPELLIDOS.Trim().Length > 100)
                {
                    rowPersonas.NOMBREAPELLIDOS = string.Concat(rowPersonas.NOMBREAPELLIDOS.Trim().Substring(0, 100), Constantes.SIMBOLO_TRESPUNTOS);
                }
            }
            if (!rowPersonas.IsRFCNull())
            {
                if (rowPersonas.RFC.Trim().Length > 8)
                {
                    rowPersonas.RFC = string.Concat(rowPersonas.RFC.Trim().Substring(0, 8), Constantes.SIMBOLO_TRESPUNTOS);
                }
            }
            if (!rowPersonas.IsCURPNull())
            {
                if (rowPersonas.CURP.Trim().Length > 8)
                {
                    rowPersonas.CURP = string.Concat(rowPersonas.CURP.Trim().Substring(0, 8), Constantes.SIMBOLO_TRESPUNTOS);
                }
            }
            if (!rowPersonas.IsCLAVEIFENull())
            {
                if (rowPersonas.CLAVEIFE.Trim().Length > 8)
                {
                    rowPersonas.CLAVEIFE = string.Concat(rowPersonas.CLAVEIFE.Trim().Substring(0, 8), Constantes.SIMBOLO_TRESPUNTOS);
                }
            }
        }

        return dse.FEXAVA_NOTARIOS;
    }
}






