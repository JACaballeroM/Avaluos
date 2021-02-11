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
using SIGAPred.Common.Web;
using System.IO;


/// <summary>
/// Clase base, con métodos generales
/// </summary>
public class PageBase : System.Web.UI.Page
{
    #region RedirecUtil

    private Redirect redirect = new Redirect();
    protected Redirect RedirectUtil
    {
        get
        {
            if (redirect == null)
            {
                redirect = new Redirect();
                redirect.EndResponse = true;
            }
            return redirect;
        }
    }

    #endregion

    /// <summary>
    /// Propiedad para la gestión del filtro de búsqueda
    /// </summary>
    public FiltroBusqueda FBusqueda
    {
        get
        {
            if (this.ViewState[Constantes.PAR_VIEWSTATE_FILTROBUSQUEDA] == null)
                this.ViewState[Constantes.PAR_VIEWSTATE_FILTROBUSQUEDA] = new FiltroBusqueda();
            return (FiltroBusqueda)this.ViewState[Constantes.PAR_VIEWSTATE_FILTROBUSQUEDA];
        }
        set { this.ViewState[Constantes.PAR_VIEWSTATE_FILTROBUSQUEDA] = value; }
    }
}
