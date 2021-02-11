using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;


/// <summary>
/// Clase para la gestión del filtro de búsqueda
/// </summary>
[Serializable]
public class FiltroBusqueda
{
    private char CaracSeparadorCamposFiltro = Constantes.CARAC_SEPARADOR_CAMPOSFILTRO;
    protected enum TipoFiltroBusqueda
    {
        Fecha = 0,
        CuentaCatastral = 1,
        IdAvaluo = 2,
        NumeroAvaluo = 3
    }

    /// <summary>
    /// Propiedad Filtro Busqueda
    /// </summary>
    public FiltroBusqueda()
    {
    }

    #region [ Campos filtro ]

    private string sortExpression;
   /// <summary>
   /// Propiedad criterio de ordenación
   /// </summary>
    public string SortExpression
    {
        get
        {
            return sortExpression;
        }
        set
        {
            sortExpression = value;
        }
    }


    private int indice;
    /// <summary>
    /// Propiedad indice
    /// </summary>
    public int Indice
    {
        get
        {
            return indice;
        }
        set
        {
            indice = value;
        }
    }

    private int pageSize;
    /// <summary>
    /// Propiedad tamaño de página
    /// </summary>
    public int PageSize
    {
        get
        {
            return pageSize;
        }
        set
        {
            pageSize = value;
        }
    }

    private string unidadPrivativa;
    /// <summary>
    /// Propiedad unidad privativa
    /// </summary>
    public string UnidadPrivativa
    {
        get
        {
            return unidadPrivativa;
        }
        set
        {
            unidadPrivativa = value;
        }
    }

    private string lote;
    /// <summary>
    /// Propiedad lote
    /// </summary>
    public string Lote
    {
        get
        {
            return lote;
        }
        set
        {
            lote = value;
        }
    }

    private string manzana;
    /// <summary>
    /// Propiedad manzana
    /// </summary>
    public string Manazana
    {
        get
        {
            return manzana;
        }
        set
        {
            manzana = value;
        }
    }

    private string region;
    /// <summary>
    /// Propiedad región
    /// </summary>
    public string Region
    {
        get
        {
            return region;
        }
        set
        {
            region = value;
        }
    }

    private string fechaIni;
    /// <summary>
    /// PRopiedad fecha inicio
    /// </summary>
    public string FechaIni
    {
        get
        {
            return fechaIni;
        }
        set
        {
            fechaIni = value;
        }
    }

    private string fechaFin;
    /// <summary>
    /// Propiedad fecha fin
    /// </summary>
    public string FechaFin
    {
        get
        {
            return fechaFin;
        }
        set
        {
            fechaFin = value;
        }
    }

    private string idAvaluo;
    /// <summary>
    /// Propiedad identificador del avalúo
    /// </summary>
    public string Idavaluo
    {
        get
        {
            return idAvaluo;
        }
        set
        {
            idAvaluo = value;
        }
    }

    private string numAvaluo;
    /// <summary>
    /// Propiedad número de avalúo
    /// </summary>
    public string NumAvaluo
    {
        get
        {
            return numAvaluo;
        }
        set
        {
            numAvaluo = value;
        }
    }

    private string codEstado;
   /// <summary>
   /// Propiedad código estado (recibido, cancelado, enviado a notario o todos)
   /// </summary>
    public string CodEstado
    {
        get
        {
            return codEstado;
        }
        set
        {
            codEstado = value;
        }
    }

    private string vigencia;
    /// <summary>
    /// Propiedad código estado (vigente, no vigente o todos)
    /// </summary>
    public string Vigencia
    {
        get
        {
            return vigencia;
        }
        set
        {
            vigencia = value;
        }
    }

    private string tipo;
    /// <summary>
    /// Propiedad tipo de búsqueda seleccionada, los posibles tipos de búsqueda son:
    /// por fecha,
    /// por número avalúo, 
    /// por número único avalúo,
    /// por cuenta catastral
    /// </summary>
    public string Tipo
    {
        get
        {
            return tipo;
        }
        set
        {
            tipo = value;
        }
    }

    #endregion

    /// <summary>
    /// Propiedad EsFecha
    /// </summary>
    /// <returns>True si el tipo de búsqueda es por fecha, false en otro caso</returns>
    public bool EsFecha()
    {
        if (tipo == TipoFiltroBusqueda.Fecha.ToString())
            return true;
        else
            return false;
    }

    /// <summary>
    /// Propiedad EsCuenta
    /// </summary>
    /// <returns>True si el tipo de búsqueda es por cuenta catastral, false en otro caso</returns>
    public bool EsCuenta()
    {
        if (tipo == TipoFiltroBusqueda.CuentaCatastral.ToString())
            return true;
        else
            return false;
    }

    /// <summary>
    /// Propiedad EsIdavaluo
    /// </summary>
    /// <returns>True si el tipo de búsqueda es por número único de avalúo, false en otro caso</returns>
    public bool EsIdavaluo()
    {
        if (tipo == TipoFiltroBusqueda.IdAvaluo.ToString())
            return true;
        else
            return false;
    }

    /// <summary>
    /// Propiedad EsNumAvaluo
    /// </summary>
    /// <returns>True si el tipo de búsqueda es por número de avalúo, false en otro caso</returns>
    public bool EsNumAvaluo()
    {
        if (tipo == TipoFiltroBusqueda.NumeroAvaluo.ToString())
            return true;
        else
            return false;
    }

    /// <summary>
    /// Compone el string del filtro que contiene los valores de todas las propiedades que se quieren guardar
    /// </summary>
    /// <returns></returns>
    public string ObtenerStringFiltro()
    {
        return fechaIni + CaracSeparadorCamposFiltro + fechaFin + CaracSeparadorCamposFiltro + region + CaracSeparadorCamposFiltro + manzana + CaracSeparadorCamposFiltro + lote + CaracSeparadorCamposFiltro + unidadPrivativa + CaracSeparadorCamposFiltro + idAvaluo + CaracSeparadorCamposFiltro + numAvaluo + CaracSeparadorCamposFiltro + vigencia + CaracSeparadorCamposFiltro + codEstado + CaracSeparadorCamposFiltro + pageSize + CaracSeparadorCamposFiltro + indice + CaracSeparadorCamposFiltro + sortExpression + CaracSeparadorCamposFiltro + tipo;
    }

    /// <summary>
    /// Asigna a las propiedades su valor correspondiente a partir del string filtro
    /// </summary>
    /// <param name="stringFiltro">string Filtro</param>
    public void RellenarObjetoFiltro(string stringFiltro)
    {
        string[] camposFiltro = stringFiltro.Split(CaracSeparadorCamposFiltro);

        fechaIni = camposFiltro[0];
        fechaFin = camposFiltro[1];
        region = camposFiltro[2];
        manzana = camposFiltro[3];
        lote = camposFiltro[4];
        unidadPrivativa = camposFiltro[5];
        idAvaluo = camposFiltro[6];
        numAvaluo = camposFiltro[7];
        vigencia = camposFiltro[8];
        codEstado = camposFiltro[9];

        pageSize = Convert.ToInt32(camposFiltro[10]);
        indice = Convert.ToInt32(camposFiltro[11]);
        sortExpression = camposFiltro[12];

        tipo = camposFiltro[13];
    }
}
