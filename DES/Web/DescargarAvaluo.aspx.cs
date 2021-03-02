using System;
using System.ServiceModel;
using SIGAPred.Common.Web;
using SIGAPred.FuentesExternas.Avaluos.Services.Enum;
using System.ComponentModel;
using ServiceAvaluos;
using SIGAPred.Common.WCF;
using System.Web.UI.WebControls;
/// <summary>
/// Formulario para la descarga de avalúos.
/// </summary>
public partial class DescargarAvaluo : PageBaseAvaluos
{
    #region PAGE
    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Button lnkConfirmar = ((Button)ModalConfirmacion.FindControl("lnkConfirmar"));
                lnkConfirmar.Attributes.Add("OnClick", "ocultarModalConfirmar();");
                Button lnkCancelar = ((Button)ModalConfirmacion.FindControl("lnkCancelar"));
                lnkCancelar.Attributes.Add("OnClick", "ocultarModalConfirmar();");
                txtRegion.Text = string.Empty;
                txtManzana.Text = string.Empty;
                txtLote.Text = string.Empty;
                txtUnidadPrivativa.Text = string.Empty;

                string errorDescarga = WebUtils.QueryString(Constantes.PAR_ERROR);
                string msgToken = WebUtils.QueryString(Constantes.PAR_ERROR_MSG);
                if (!string.IsNullOrEmpty(errorDescarga))
                {
                    if (errorDescarga.CompareTo(Constantes.PAR_ERROR_CC_INMUEBLE) == 0)
                    {
                        ModalInfo.TextoInformacion = Constantes.MSJ_CUENTACAT_INVALIDA;
                        ModalInfo.TipoMensaje = true;
                        extenderPnlInfoModal.Show();
                    }
                    else if (errorDescarga.CompareTo(Constantes.PAR_ERROR_TOKEN) == 0)
                    {
                        if (!string.IsNullOrEmpty(msgToken))
                        {
                            string mensaje = msgToken;
                            ModalInfoToken.TextoInformacion = Constantes.MSJ_TOKEN_EXCEPTION + mensaje;
                            ModalInfoToken.TipoMensaje = true;
                            extenderPnlInfoTokenModal.Show();
                        }
                    }
                    else if (errorDescarga.CompareTo(Constantes.PAR_ERROR_USEREXCEPTION) == 0)
                    {
                        if (!string.IsNullOrEmpty(msgToken))
                        {
                            string mensaje = msgToken;
                            ModalInfoToken.TextoInformacion = mensaje ;
                            ModalInfoToken.TipoMensaje = true;
                            extenderPnlInfoTokenModal.Show();
                        }
                    }
                }
            }
        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInformacion(ex.Message);
        } 
        catch (FaultException<ServiceAvaluosNuevos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosInfoException> ciex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
            MostrarMensajeInformacion(msj);
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
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            //Se establece el botón de cancelar para los modalpopupextenders
            mpeErrorTareas.CancelControlID = errorTareas.ClientIdCancelacion;
            ModalConfirmacion.TextoConfirmacion = "El digito verficador no cumple la validación pero corresponde con el digito almacenado en BD. ¿Desea continuar con la descarga?";
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }
    #endregion 

    #region EVENTOS
    /// <summary>
    /// Mostrar la excepción del token.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void ModalInfoToken_Ok_Click(object sender, System.ComponentModel.CancelEventArgs e)
    {
        try
        {
            if (e.Cancel)
                extenderPnlInfoTokenModal.Hide();
            else
                extenderPnlInfoTokenModal.Show();
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosInfoException> ciex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por ModalInfo_Ok para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void ModalInfo_Ok_Click(object sender, System.ComponentModel.CancelEventArgs e)
    {
        try
        {
            if (e.Cancel)
                extenderPnlInfoModal.Hide();
            else
                extenderPnlInfoModal.Show();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por btnDescargaAvaluo para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnDescargaAvaluo_Click(object sender, EventArgs e)
    {
        try
        {
            //Verificar si existe la cuenta catastral
            if (verificarCuentaCat(txtRegion.Text, txtManzana.Text, txtLote.Text, txtUnidadPrivativa.Text))
            {
                //Verificar que el digito verificador introducido es correcto
                if (verificarDigito(txtRegion.Text, txtManzana.Text, txtLote.Text, txtUnidadPrivativa.Text, txtDigito.Text))
                {
                    RealizarDescargaAvaluo();
                }
                else
                {                    
                    AvaluosClient clienteAvaluos = new AvaluosClient();
                    try{
                        string digitoBD = clienteAvaluos.ObtenerDigitoVerificadorBD(txtRegion.Text, txtManzana.Text, txtLote.Text, txtUnidadPrivativa.Text);
                        if (digitoBD.Equals(txtDigito.Text))
                        {
                            ModalConfirmacion.CancelarVisible = true;

                          
                            confirmar_ModalPopupExtender.Show();
                        }
                        else
                          MostrarMensajeInformacion(Constantes.MSJ_DIGITO_VERIFICADOR_ERRONEO);
                    }
                    finally {  
                        clienteAvaluos.Disconnect();
                    }
                }
            }
            else
            {
                MostrarMensajeInformacion(Constantes.MSJ_CUENTACAT_INVALIDA);
            }
       }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInformacion(ex.Message);
        }
        catch(FaultException<ExcepcionCuentaNoExiste> ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInformacion(ex.Detail.Descripcion);
        } 
        catch (FaultException<ServiceAvaluosNuevos.AvaluosException> cex)
        {
            ExceptionPolicyWrapper.HandleException(cex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosInfoException> ciex)
        {
            ExceptionPolicyWrapper.HandleException(ciex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Realizar descarga del avalúo.
    /// </summary>
    private void RealizarDescargaAvaluo()
    {
        string cuentaCatastral = txtRegion.Text +
                                txtManzana.Text +
                                txtLote.Text +
                                txtUnidadPrivativa.Text + txtDigito.Text;

        RedirectUtil.BaseURL = Constantes.URL_DESCARGA_AVALUO_DETALLE;
        RedirectUtil.AddParameter(Constantes.PAR_CUENTACAT, cuentaCatastral);
        RedirectUtil.AddParameter(Constantes.PAR_TIPO_AVALUO, TipoAvaluo().ToString());
        RedirectUtil.Go();
    }
    #endregion 

    #region MOSTRAR MENSAJES
    /// <summary>
    /// Mostrar mensaje de error asociado a una excepción.
    /// </summary>
    /// <param name="mensaje">El mensaje a mostrar</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }

    /// <summary>
    /// Mostrar mensaje informativo de la aplicación
    /// </summary>
    /// <param name="mensaje">El mensaje a mostrar</param>
    private void MostrarMensajeInformacion(string mensaje)
    {
        ModalInfoToken.TextoInformacion = mensaje;
        ModalInfoToken.TipoMensaje = true;
        extenderPnlInfoTokenModal.Show();
        UpdatePanelBotonDescargar.Update();
    }
    #endregion 

    /// <summary>
    /// Manejador de eventos. Llamado por confirmar para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void confirmar_ConfirmClick(object sender, CancelEventArgs e)
    {
         if (!e.Cancel) //Sólo si se ha pulsado aceptar descargar el avalúo
        {
            RealizarDescargaAvaluo();
        }

    }
       

    /// <summary>
    /// Obtiene el/la tipo avalúo.
    /// </summary>
    /// <returns>
    /// El enmumerado asociado en función de si el tipo seleccionado es Catastral o Comercial
    /// </returns>
    protected int TipoAvaluo()
    {
        if (RBAvaluoCatastral.Checked)
            return (int)TiposAvaluoEnum.Catastral;

        else
            return (int)TiposAvaluoEnum.Comercial;
    }
}
