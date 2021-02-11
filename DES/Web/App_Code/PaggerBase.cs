using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Clase base para la gestión de paginación.
/// </summary>
public class PaggerBase
{
    /// <summary>
    /// El número total de filas.
    /// </summary>
    protected int rowsTotal = -1;
    /// <summary>
    ///El indice.
    /// </summary>
    protected int indice = -1;
    /// <summary>
    ///Tamaño de la página
    /// </summary>
    protected int pageSize = -1;
    /// <summary>
    ///Criterio de ordenación
    /// </summary>
    protected string SortExpression = string.Empty;

    /// <summary>
    /// Guarda las propiedades de la página.
    /// </summary>
    /// <param name="indice">El indice de la página.</param>
    /// <param name="pageSize">Tamaño de la página.</param>
    /// <param name="SortExpression">Criterio de ordenación.</param>
    protected void SavePaggerSettings(int indice, int pageSize, string SortExpression)
    {
        if (indice > 0)
            this.indice = indice / pageSize + 1;
        else
            this.indice = 1;

        this.pageSize = pageSize;
        this.SortExpression = SortExpression;
    }

    /// <summary>
    /// Obtiene el número total de filas
    /// </summary>
    /// <returns>
    /// El número total de filas
    /// </returns>
    protected int GetRowCount()
    {
        return this.rowsTotal;
    }
}
