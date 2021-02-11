using System;
using ServiceAvaluos;
using SIGAPred.Common.WCF;

/// <summary>
/// Clase para la gestión de consultas sobre avalúos.
/// </summary>
public class DseAvaluosConsultaPager : PaggerBase
{
    #region NumTotal

    /// <summary>
    /// Número total de filas obtenidas por búsqueda por proximidad.
    /// </summary>
    /// <param name="idAvaluo">identificador del avalúo.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">indice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// Entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasProximidad(int idAvaluo, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por proximidad perito.
    /// </summary>
    /// <param name="idAvaluo">identificador del avalúo.</param>
    /// <param name="idPerito">identificador perito.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasProximidadPerito(int idAvaluo, int idPerito, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por proximidad sociedad.
    /// </summary>
    /// <param name="idAvaluo">identificador del avalúo.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasProximidadSociedad(int idAvaluo, int idSociedad, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por fecha/estado.
    /// </summary>
    /// <param name="fechaInicio">fecha inicio.</param>
    /// <param name="fechaFin">fecha fin.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasFechaEstado(DateTime fechaInicio, DateTime fechaFin, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por fecha/estado/perito.
    /// </summary>
    /// <param name="fechaInicio">fecha inicio.</param>
    /// <param name="fechaFin">fecha fin.</param>
    /// <param name="idPerito">identificador perito.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasFechaPeritoVigenciaEstado(DateTime fechaInicio, DateTime fechaFin, int idPerito, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por fecha/estado/vigencia.
    /// </summary>
    /// <param name="numValuo">número avalúo.</param>
    /// <param name="idPersona">identificador persona.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasNumValuoPeritoEstadoVig(string numValuo, decimal idPersona, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por Número avalúo/Estado/vigencia.
    /// </summary>
    /// <param name="numValuo">número avalúo.</param>
    /// <param name="registroPerito">resgistro perito.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasNumValuoEstadoVig(string numValuo, string registroPerito, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por Número avalúo/Estado/vigencia.
    /// </summary>
    /// <param name="cuentaCatastral">cuenta catastral.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasCuentaVigenciaEstado(string cuentaCatastral, int codestado, string vigente, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por Cuenta catastral/Estado/vigencia.
    /// </summary>
    /// <param name="cuentaCatastral">cuenta catastral.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasCuentaCatastralVigEstado(string cuentaCatastral, decimal idSociedad, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por  Cuenta catastral/Perito/Estado/vigencia.
    /// </summary>
    /// <param name="cuentaCatastral">cuenta catastral.</param>
    /// <param name="idPerito">identificador perito.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasCuentaCatastralPeritoVigEstado(string cuentaCatastral, string idPerito, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }
   
    /// <summary>
    /// Número total de filas por búsqueda por Fecha/Sociedad/Estado/vigencia.
    /// </summary>
    /// <param name="fechaInicio">fecha inicio.</param>
    /// <param name="fechaFin">fecha fin.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasFechaSociedad_EstadoVig(DateTime fechaInicio, DateTime fechaFin, int idSociedad, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }
   
    /// <summary>
    /// Número total de filas por búsqueda por Número avalúo/Sociedad/Estado/vigencia.
    /// </summary>
    /// <param name="numValuo">número avalúo.</param>
    /// <param name="registroPerito">registro perito.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalFilasNumValuoSociedadEstadoVig(string numValuo, string registroPerito, decimal idSociedad, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por identificador avalúo/Estado/vigencia.
    /// </summary>
    /// <param name="idValuo">identificador del avalúo.</param>
    /// <param name="estado">estado.</param>
    /// <param name="vigencia">vigencia.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalObtenerAvaluoPorIdAvaluoEstadoVigencia(string idValuo, int estado, string vigencia, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por identificador avalúo/Estado/vigencia/Idpersona.
    /// </summary>
    /// <param name="idValuo">identificador del avalúo.</param>
    /// <param name="estado">estado.</param>
    /// <param name="vigencia">vigencia.</param>
    /// <param name="idpersonaperito">identificador persona perito.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalObtenerAvaluoPorIdAvaluoEstadoVigencia2(string idValuo, int estado, string vigencia, int idpersonaperito, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    /// <summary>
    /// Número total de filas por búsqueda por identificador avalúo/sociedad/Estado/vigencia.
    /// </summary>
    /// <param name="idValuo">identificador del avalúo.</param>
    /// <param name="estado">estado.</param>
    /// <param name="vigencia">vigencia.</param>
    /// <param name="idpersonasoci">identificador persona sociedad.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// entero que indica el número de filas devueltas.
    /// </returns>
    public int NumTotalObtenerAvaluoPorIdAvaluoSociEstadoVigencia(string idValuo, int estado, string vigencia, int idpersonasoci, int pageSize, int indice, string SortExpression)
    {
        return GetRowCount();
    }

    #endregion 

    #region ObtenerAvaluos
    
    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta con los avalúos próximos a un avalúo.
    /// </summary>
    /// <param name="idAvaluo">identificador del avalúo.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorProximidad(int idAvaluo, int codestado, int pageSize, int indice,string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorProximidad(idAvaluo, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos por fecha y estado.
    /// </summary>
    /// <param name="fechaInicio">fecha inicio.</param>
    /// <param name="fechaFin">fecha fin.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorFechaEstado(DateTime fechaInicio, DateTime fechaFin, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorFechaEstado(fechaInicio, fechaFin, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos por fecha, perito, vigencia
    /// y estado.
    /// </summary>
    /// <param name="fechaInicio">fecha inicio.</param>
    /// <param name="fechaFin">fecha fin.</param>
    /// <param name="idPerito">identificador perito.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorFechaPeritoVigenciaEstado(DateTime fechaInicio, DateTime fechaFin, int idPerito, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorFechaPeritoVigenciaEstado(fechaInicio, fechaFin, idPerito, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos por fecha, sociedad, estado
    /// y vigencia.
    /// </summary>
    /// <param name="fechaInicio">fecha inicio.</param>
    /// <param name="fechaFin">fecha fin.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorFechaSociedad_EstadoVig(DateTime fechaInicio, DateTime fechaFin, int idSociedad, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorFechaSoci_EstadoVig(fechaInicio, fechaFin, idSociedad, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos por número avalúo, sociedad,
    /// estado y vigencia.
    /// </summary>
    /// <param name="numValuo">número avalúo.</param>
    /// <param name="registroPerito">registro perito.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorNumValuoSociedad_EstadoVig(string numValuo, string registroPerito, decimal idSociedad,string vigente,int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorNumAvaluoSoci_EstadoVig(numValuo, registroPerito, idSociedad, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos por número avalúo, vigencia
    /// y estado.
    /// </summary>
    /// <param name="numValuo">número avalúo.</param>
    /// <param name="registroPerito">registro perito.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorNumValuoVigEstado(string numValuo, string registroPerito, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorNumValuoVigEstado(numValuo, registroPerito, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos por cuenta catastral,
    /// sociedad, estado y vigencia.
    /// </summary>
    /// <param name="cuentaCatastral">cuenta catastral.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralSociedadEstadoVigencia(string cuentaCatastral, decimal idSociedad, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorCuentaSociEstadoVig(cuentaCatastral, idSociedad, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);  
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta a partir de los parámetros identificador avalúo, estado
    /// y vigencia.
    /// </summary>
    /// <param name="idValuo">identificador avalúo.</param>
    /// <param name="estado">estado.</param>
    /// <param name="vigencia">vigencia.</param>
    /// <param name="idpersonaperito">identificador persona perito.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluoPorIdAvaluoEstadoVigencia(string idValuo, int estado, string vigencia, int idpersonaperito, int pageSize, int indice, string SortExpression) //AÃ±adido tarea 2.2.8
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluoPorIdAvaluoEstadoVigencia(idValuo, estado, vigencia, idpersonaperito, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta a partir de los parámetros identificador avalúo, estado,
    /// vigencia.
    /// </summary>
    /// <param name="idValuo">identificador avalúo.</param>
    /// <param name="estado">estado.</param>
    /// <param name="vigencia">vigencia.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluoPorIdAvaluoEstadoVigenciaTodosPeritosSoci(string idValuo, int estado, string vigencia, int pageSize, int indice, string SortExpression) //AÃ±adido tarea 2.2.8
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluoPorVigEstTodosPeritosSoci(idValuo, estado, vigencia, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta según identificador avalúo, sociedad, estado y vigencia.
    /// </summary>
    /// <param name="idValuo">identificador avalúo.</param>
    /// <param name="estado">estado.</param>
    /// <param name="vigencia">vigencia.</param>
    /// <param name="idpersonasoci">identificador persona sociedad.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluoPorIdAvaluoSociEstadoVigencia(string idValuo, int estado, string vigencia, int idpersonasoci, int pageSize, int indice,string SortExpression) //AÃ±adido tarea 2.2.8
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluoPorIdAvaluos_Soci_Vigencia_Estado(idValuo, estado, vigencia, idpersonasoci, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);  
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta a partir de número de avalúo, perito, estado y vigencia.
    /// </summary>
    /// <param name="numValuo">número avalúo.</param>
    /// <param name="idPersona">identificador persona.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorNumValuoPerito_EstadoVig(string numValuo, decimal idPersona, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorNumValuoPerito_EstadoVig(numValuo, idPersona, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta a partir de la cuenta catastral, la vigencia y el estado.
    /// </summary>
    /// <param name="cuentaCatastral">cuenta catastral.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorCuentaVigenciaEstado(string cuentaCatastral, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            DseAvaluoConsulta dse = clienteAvaluos.ObtenerAvaluosPorCuentaVigenciaEstado(cuentaCatastral, codestado, vigente, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
            return clienteAvaluos.ObtenerAvaluosPorCuentaVigenciaEstado(cuentaCatastral, codestado, vigente, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta a partir de la cuenta catastral, el perito, la vigencia
    /// y el estado.
    /// </summary>
    /// <param name="cuentaCatastral">cuenta catastral.</param>
    /// <param name="idPerito">identificador perito.</param>
    /// <param name="vigente">vigente.</param>
    /// <param name="codestado">código estado.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralPeritoVigenciaEstado(string cuentaCatastral, string idPerito, string vigente, int codestado, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorCuentaCatastralPeritoVigenciaEstado(cuentaCatastral, idPerito, vigente, codestado, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta para la búsqueda de avalúos próximos por identidicador
    /// avalúo e identificador perito.
    /// </summary>
    /// <param name="idAvaluo">identificador del avalúo.</param>
    /// <param name="idPerito">identificador perito.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorProximidadPerito(int idAvaluo, int idPerito, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorProximidadPerito(idAvaluo, idPerito, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    /// <summary>
    /// Obtiene el dataset DseAvaluoConsulta  para la búsqueda de avalúos próximos por identidicador
    /// avalúo e identificador sociedad.
    /// </summary>
    /// <param name="idAvaluo">identificador del avalúo.</param>
    /// <param name="idSociedad">identificador sociedad.</param>
    /// <param name="pageSize">tamaño página.</param>
    /// <param name="indice">índice.</param>
    /// <param name="SortExpression">criterio de ordenación.</param>
    /// <returns>
    /// obtiene un dataset con los avalúos encontrados según los criterios.
    /// </returns>
    public DseAvaluoConsulta ObtenerAvaluosPorProximidadSociedad(int idAvaluo, int idSociedad, int pageSize, int indice, string SortExpression)
    {
        SavePaggerSettings(indice, pageSize, SortExpression);

        if (string.IsNullOrEmpty(SortExpression))
        {
            SortExpression = Constantes.SORTDIRECTION_DEFAULT_FECHAPRES;
        }

        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerAvaluosPorProximidadSociedad(idAvaluo, idSociedad, base.pageSize, base.indice, ref base.rowsTotal, SortExpression);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }

    #endregion 
}
