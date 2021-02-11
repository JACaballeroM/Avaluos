using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceAvaluos;
using System.ComponentModel;
using System.ServiceModel;
using SIGAPred.Common.Seguridad;

/// <summary>
/// Modal para la búsqueda de peritos
/// </summary>
public partial class UserControls_ModalBuscarPeritos : System.Web.UI.UserControl
{

    #region Delegado y Evento
    /// <summary>
    /// Delegaod para confirmación
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Evento</param>
    public delegate void ConfirmClickHandler(object sender, CancelEventArgs e);

    /// <summary>
    /// Evento para confirmación
    /// </summary>
    public event ConfirmClickHandler ConfirmClick;

    #endregion

    #region Propiedades del control

    /// <summary>
    /// Propiedad que contiene el avalúo seleccionado
    /// </summary>
    public bool Seleccionado
    {
        get { return (bool)this.ViewState[Constantes.PAR_VIEWSTATE_SELECCIONADO]; }
    }

    /// <summary>
    /// Propiedad que contiene el número de registro
    /// </summary>
    public string NumeroRegistro
    {
        get
        {
            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_PERITOS))
            {
                if (gridViewPersonas.SelectedIndex > -1)
                    return gridViewPersonas.DataKeys[gridViewPersonas.SelectedIndex].Values[0].ToString();
            }
            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_SOCIEDADES))
            {
                if (gridViewSociedades.SelectedIndex > -1)
                    return gridViewSociedades.DataKeys[gridViewSociedades.SelectedIndex].Values[0].ToString();
            }
            return string.Empty;
        }
    }

    public string Nombre
    {
        get
        {
            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_PERITOS))
            {
                if (gridViewPersonas.SelectedIndex > -1)
                    return gridViewPersonas.Rows[gridViewPersonas.SelectedIndex].Cells[1].Text;
            }
            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_SOCIEDADES))
            {
                if (gridViewSociedades.SelectedIndex > -1)
                    return gridViewSociedades.Rows[gridViewSociedades.SelectedIndex].Cells[1].Text;
            }
            return string.Empty;
        }
    }
    private int tipoRegistro = -1;
    /// <summary>
    /// Indica si el valor corresponde a un perito (1) o a una sociedad(0)
    /// </summary>
    /// <value>
    /// El tipo de registro.
    /// </value>
    public int TipoRegistro 
    {
        get { return tipoRegistro; }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
   {
       try
       {
           if (!IsPostBack)
           {
               if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
               {
                   ddlOpcionBusqueda.Visible = false;
                   lblPerito.Visible = true;
                   lblBuscarPersonaModalFiltroTitulo.Text = "Búsqueda de peritos";
               }
           }
       }
       catch (FaultException<ServiceAvaluos.AvaluosException> cex)
       {
           string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
           MostrarMensajeInfoExcepcion(msj);
       }
       catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
       {
           string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
           MostrarMensajeInfoExcepcion(msj);
       }
       catch (Exception ex)
       {
           ExceptionPolicyWrapper.HandleException(ex);
           string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
           MostrarMensajeInfoExcepcion(msj);
       }
    }

    private void ToUpper()
    {
        txtPeritoApellidoMaterno.Text = txtPeritoApellidoMaterno.Text.ToUpper();
        txtPeritoApellidoPaterno.Text = txtPeritoApellidoPaterno.Text.ToUpper();
        txtPeritoNombre.Text = txtPeritoNombre.Text.ToUpper();
        txtPeritoRFC.Text = txtPeritoRFC.Text.ToUpper();
        txtNumeroRegistro.Text = txtNumeroRegistro.Text.ToUpper();
        txtPeritoCURP.Text = txtPeritoCURP.Text.ToUpper();
        txtPeritoIFE.Text = txtPeritoIFE.Text.ToUpper();
    }

    protected void btnBuscarPersonaModalFiltrar_Click(object sender, EventArgs e)
    {
        try
        {
            ToUpper();
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_PERITOS))
            {
                MostrarPanelPerito();
                gridViewPersonas.DataSourceID = odsPorBusquedaPerito.ID;
                gridViewPersonas.DataBind();
                gridViewPersonas.Visible = true;
            }

            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_SOCIEDADES))
            {
                MostrarPanelSociedad();
                gridViewSociedades.DataSourceID = odsPorBusquedaSociedad.ID;
                gridViewSociedades.DataBind();
                gridViewSociedades.Visible = true;
            }

            pnlBucarPersonasModalFiltros.Update();
            UpdatePanel1.Update();
        }
        catch (FaultException<ServiceAvaluos.AvaluosException> dex)
        {
            ModalInfoGuardar.TextoInformacion = dex.Detail.Descripcion;
            ModalInfoGuardar.TipoMensaje = true;

            extenderPnlGuardarModalOK_Extender.Show();
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            ModalInfoGuardar.TextoInformacion = ex.Message;
            ModalInfoGuardar.TipoMensaje = true;

            extenderPnlGuardarModalOK_Extender.Show();
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
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
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

    private void LaunchConfirmClickHandler(object sender, CancelEventArgs e)
    {
        if (ConfirmClick != null)
            ConfirmClick(sender, e);
    }

    protected void gridViewPersonas_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnAceptar.Enabled = true;
            this.ViewState[Constantes.PAR_VIEWSTATE_SELECCIONADO] = true;
            updatePanelBotones.Update();
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void gridViewPersonas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void gridViewPersonas_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void gridViewSociedades_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnAceptar.Enabled = true;
            this.ViewState[Constantes.PAR_VIEWSTATE_SELECCIONADO] = true;
            updatePanelBotones.Update();
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void gridViewSociedades_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void gridViewSociedades_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void btnBuscarPersonaModalCancelar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            this.ViewState[Constantes.PAR_VIEWSTATE_SELECCIONADO] = false;
            LaunchConfirmClickHandler(sender, new CancelEventArgs(true));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridViewPersonas.SelectedIndex > -1)
                this.ViewState[Constantes.PAR_VIEWSTATE_SELECCIONADO] = true;
            LaunchConfirmClickHandler(sender, new CancelEventArgs(true));
            LimpiarDatos();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        try
        {
            this.ViewState[Constantes.PAR_VIEWSTATE_SELECCIONADO] = false;
            LaunchConfirmClickHandler(sender, new CancelEventArgs(true));
            LimpiarDatos();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void ddlOpcionBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LimpiarDatosTexto();
            LimpiarDataSources();
            if (ddlOpcionBusqueda.SelectedValue.Equals(Constantes.DDLBUSQUEDA_PERITOS))
            {
                MostrarPanelPerito();
            }
            else
            {
                MostrarPanelSociedad();
            }
            LaunchConfirmClickHandler(sender, new CancelEventArgs(false));
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    #region [ TxtChanged ] 

    protected void txtPeritoRFC_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPeritoRFC.Text))
            {
                txtNumeroRegistro.Text = string.Empty;
                TextBox1.Text = txtPeritoRFC.Text;
            }
            else TextBox1.Text = string.Empty;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void txtPeritoCURP_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPeritoCURP.Text))
            {
                txtNumeroRegistro.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    protected void txtPeritoIFE_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtPeritoIFE.Text))
            {
                txtNumeroRegistro.Text = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION  + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    #endregion 

    private void MostrarPanelPerito()
    {
        this.lblPeritoApellidoPaterno.Visible = true;
        this.txtPeritoApellidoPaterno.Visible = true;
        this.lblPeritoApellidoMaterno.Visible = true;
        this.txtPeritoApellidoMaterno.Visible = true;
        tipoRegistro = 1;
        lblBuscarPersonaModalNombre.Text = "Nombre";
    }
    
    private void MostrarPanelSociedad()
    {
        this.lblPeritoApellidoPaterno.Visible = false;
        this.txtPeritoApellidoPaterno.Visible = false;
        this.lblPeritoApellidoMaterno.Visible = false;
        this.txtPeritoApellidoMaterno.Visible = false;
        txtPeritoApellidoPaterno.Text = string.Empty;
        txtPeritoApellidoMaterno.Text = string.Empty;

        tipoRegistro = 0;
        lblBuscarPersonaModalNombre.Text = "Razón Social";
    }

    #region LimpiarDatos

    private void LimpiarDatos()
    {
        LimpiarDatosTexto();

        //Visualizar el panel por defecto
        ddlOpcionBusqueda.SelectedValue = Constantes.DDLBUSQUEDA_PERITOS;
        MostrarPanelPerito();
        btnAceptar.Enabled = false;
        LimpiarDataSources();
    }

    private void LimpiarDataSources()
    {
        //Vaciar DataSources de los grids
        gridViewPersonas.DataSource = null;
        gridViewPersonas.DataBind();

        gridViewSociedades.DataSource = null;
        gridViewSociedades.DataBind();

        //Ocultar gridViews
        gridViewPersonas.Visible = false;
        gridViewSociedades.Visible = false;

        UpdatePanel1.Update();
    }

    private void LimpiarDatosTexto()
    {
        txtPeritoNombre.Text = string.Empty;
        txtNumeroRegistro.Text = string.Empty;
        txtPeritoApellidoMaterno.Text = string.Empty;
        txtPeritoApellidoPaterno.Text = string.Empty;
        txtPeritoCURP.Text = string.Empty;
        txtPeritoIFE.Text = string.Empty;
        txtPeritoRFC.Text = string.Empty;
    }

#endregion

}
