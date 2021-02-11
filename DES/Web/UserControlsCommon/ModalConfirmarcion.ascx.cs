using System;
using System.ComponentModel;


/// <summary>
/// Control de usuario para modales de confirmación
/// </summary>
public partial class UserControlsCommon_ModalConfirmacion : UserControlBase
{
    #region Eventos
    /// <summary>
    /// Delegado para controlar la confirmación
    /// </summary>
    public delegate void ConfirmClickHandler(object sender, CancelEventArgs e);
    /// <summary>
    /// Evento para controlar la confirmación
    /// </summary>
    public event ConfirmClickHandler ConfirmClick;


   
    #endregion

    #region Propiedades

    /// <summary>
    /// Almacena el texto de confirmación que aparece en el diálogo
    /// </summary>
    public string TextoConfirmacion
    {
        get
        {
            return lblTextoConfirmacion.Text;
        }

        set
        {
            lblTextoConfirmacion.Text = value;
        }
    }

    private bool cancelarVisible = true;
    /// <summary>
    /// Propiedad para indicar si el botón cancelar debe ser visible
    /// </summary>
    public bool CancelarVisible
    {
        get
        {
            return cancelarVisible;
        }

        set
        {
            cancelarVisible = value;
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
    /// Almacena el texto del link de confirmación que aparece en el diálogo
    /// </summary>
    public string TextoLinkConfirmacion
    {
        get
        {
            return lnkConfirmar.Text;
        }
        set
        {
            lnkConfirmar.Text = value;
        }
        
    }

    private bool aceptar;
    /// <summary>
    /// Propiedad Aceptar
    /// </summary>
    public bool Aceptar
    {
        get { return aceptar; }
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
        try
        {
            if (!IsPostBack)
            {
                //Actualizar visibilidad del link cancelar
                if (!cancelarVisible)
                {
                    this.lnkCancelar.Visible = false;
                    this.lnkCancelar.Enabled = false;
                }
                else
                {
                    this.lnkCancelar.Visible = true;
                    this.lnkCancelar.Enabled = true;
                }
            
              lblTextoConfirmacion.Text = TextoConfirmacion;
            }
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    #endregion

    #region Eventos Linkbutton

    protected void lnkConfirmar_Click(object sender, EventArgs e)
    {
        try
        {
            aceptar = true;
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void lnkCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            aceptar = false;
            LaunchConfirmClickHandler(sender, new CancelEventArgs(true));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

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

    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
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
