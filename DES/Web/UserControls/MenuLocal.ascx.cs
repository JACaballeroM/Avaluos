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
using SIGAPred.Seguridad.Utilidades.ClaimTypes;
using SIGAPred.Common.Seguridad;
using System.ServiceModel;


/// <summary>
/// Menú local.
/// </summary>
public partial class UserControls_MenuLocal : System.Web.UI.UserControl
{
    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                GestorVisibilidadControles.ValidarControl(this.Controls, this.ID);
                //MostarReportes();
            }
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
}

    /// <summary>
    /// Pre-renderizado de la página.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            //Se establece el botón de cancelar para los modalpopupextenders
            mpeErrorTareas.CancelControlID = errorTareas.ClientIdCancelacion;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    private void MostarReportes()
    {
        
        if (Usuarios.tienePerfil(System.Configuration.ConfigurationManager.AppSettings["PerfilReportes"].ToString()))
        {
            TreeNode nodoReporte = new TreeNode("Informes");
            nodoReporte.Value = "Reportes";
            TreeNode nodoLink = new TreeNode("Investigación de mercado");
            nodoLink.Value = "Investigación de mercado";
            nodoLink.NavigateUrl = "~/InvMercado.aspx";
            nodoReporte.ChildNodes.Add(nodoLink);
            MenuLocal.Nodes.Add(nodoReporte);

        }
    }

    /// <summary>
    /// Mostrar mensaje de error asociado a una excepcion.
    /// </summary>
    /// <param name="mensaje">El mensaje que se quiere mostrar.</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }
}
