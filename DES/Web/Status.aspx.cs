using System;
using Microsoft.IdentityModel.Configuration;

/// <summary>
/// Clase para la gestión del registro de usuarios.
/// </summary>
public partial class Status : System.Web.UI.Page
{
    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        var _homeRealm = MicrosoftIdentityModelSection.DefaultServiceElement.FederatedAuthentication.WSFederation.HomeRealm;
        Response.Write("<script>if (self == top || self.name !== 'frameToken') { window.top.location.href = '" + _homeRealm + "'}</script>");
    }
}