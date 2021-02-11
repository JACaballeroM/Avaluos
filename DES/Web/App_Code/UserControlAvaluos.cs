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
using ServiceAvaluos;

/// <summary>
/// Clase para User Control Avaluos.
/// </summary>
public class UserControlAvaluos:UserControlBase
{
    #region Servicio WCF

    /// <summary>
    /// El cliente avalúo.
    /// </summary>
    private AvaluosClient clienteAvaluo = null;
    /// <summary>
    /// Obtiene el cliente avalúo.
    /// </summary>
    /// <value>
    /// El cliente avalúo.
    /// </value>
    protected AvaluosClient ClienteAvaluo
    {
        get
        {
            if (clienteAvaluo == null)
            {
                clienteAvaluo = new AvaluosClient();
            }
            return clienteAvaluo;
        }
    }

    /// <summary>
    /// Lanza el evento unload
    /// </summary>
    /// <param name="e">Información del evento para enviar al manejador del evento registrado.</param>
    protected override void OnUnload(EventArgs e)
    {
        if (clienteAvaluo != null)
        {
            clienteAvaluo.Close();
        }
        base.OnUnload(e);
    }

    /// <summary>
    /// Manejador de eventos. Llamado por Page para eventos dispose.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento para enviar al manejador del evento registrado.</param>
    private void Page_Dispose(object sender, System.EventArgs e)
    {
        if (clienteAvaluo != null) clienteAvaluo.Close();
    }

    #endregion
}
