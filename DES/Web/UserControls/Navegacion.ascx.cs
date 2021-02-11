using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Control de usuario mapa de navegación.
/// </summary>
public partial class UserControls_Navegacion : System.Web.UI.UserControl
{
    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Mapa sitio web
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    /// <returns>
    /// Actualiza el mapa del sitio 
    /// </returns>
    private SiteMapNode SiteMap_SiteMapResolve(Object sender, SiteMapResolveEventArgs e)
    {
        SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
        SiteMapNode tempNode = currentNode;
        tempNode.Url = e.Context.Request.Url.PathAndQuery;
        return currentNode;
    }
}
