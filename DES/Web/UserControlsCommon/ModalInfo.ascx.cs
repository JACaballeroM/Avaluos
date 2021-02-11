using System;
using System.ComponentModel;

/// <summary>
/// Control de usuario para modales informativas
/// </summary>
public partial class UserControlsCommon_ModalInfo : System.Web.UI.UserControl
{

    #region Eventos

    /// <summary>
    /// Manejador del delagado confirmación
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Envento</param>
    public delegate void ConfirmClickHandler(object sender, CancelEventArgs e);
    
    /// <summary>
    /// Manejador del evento confirmación
    /// </summary>
    public event ConfirmClickHandler ConfirmClick;

    #endregion

    #region Propiedades

    /// <summary>
    /// Propiedad que indica el tipo de mensaje
    /// </summary>
    public bool TipoMensaje
    {
        get { return Convert.ToBoolean(ViewState[Constantes.PAR_VIEWSTATE_TIPOMSJ]); }
        set { ViewState[Constantes.PAR_VIEWSTATE_TIPOMSJ] = value; }
    }

    /// <summary>
    /// Retorna el clientid del botón de cancelar, para evitar el refresco en el cliente
    /// </summary>
    public string ClientIdCancelacion
    {
        get
        {
            return btnCancelar.ClientID;
        }
    }

    /// <summary>
    /// Almacena el texto de confirmación que aparece en el diálogo
    /// </summary>
    public string TextoInformacion
    {
        get
        {
            return lblTextoInformacion.Text;
        }
        set
        {
            lblTextoInformacion.Text = value;
        }
    }

    /// <summary>
    /// Almacena el texto del título que aparece en el diálogo
    /// </summary>
    public string TextoTitulo
    {
        get
        {
            return lblTextoTitulo.Text;
        }
        set
        {
            lblTextoTitulo.Text = value;
        }
    }

    /// <summary>
    /// Almacena el texto del link de cancelar que aparece en el diálogo
    /// </summary>
    public string TextoLinkCancelar
    {
        get
        {
            return btnOk.Text;
        }
        set
        {
            btnOk.Text = value;
        }
    }

    /// <summary>
    /// Activa o desactiva el título del diálogo
    /// </summary>
    public bool VisibleTitulo
    {
        set
        {
            trCabecera.Visible = value;
        }
        get
        {
            return trCabecera.Visible;
        }
    }

    #endregion

    #region Eventos Página

    protected void Page_Load(object sender, EventArgs e)
    {


    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        { 
            //Se establece el botón de cancelar para los modalpopupextenders
            PnlTextoInformacion.Update();
            mpeErrorTareas.CancelControlID = errorTareas.ClientIdCancelacion;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }

    #endregion

    #region Eventos Linkbutton

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            LaunchConfirmClickHandler(sender, new CancelEventArgs(true));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    #endregion

    #region Métodos Privados

    private void LaunchConfirmClickHandler(object sender, CancelEventArgs e)
    {
        if (ConfirmClick != null)
        {
            ConfirmClick(sender, e);
        }
    }
    #endregion
}
