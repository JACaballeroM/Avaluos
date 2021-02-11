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
using ServiceAvaluos;
using SIGAPred.Common.Web;
using SIGAPred.Common.Extensions;
using System.ComponentModel;
using SIGAPred.FuentesExternas.Avaluos.Services.Enum;
using System.ServiceModel;

/// <summary>
/// Deatlle del avalúo.
/// </summary>
public partial class UserControls_DetalleAvaluo : UserControlAvaluos
{
    /// <summary>
    /// Propiedad que indica si se deben mostrar los botones de las operaciones.
    /// </summary>
    /// <value>
    /// True si se deben mostrar los botones de operaciones, false en otro caso.
    /// </value>
    public bool VerBotonesOperaciones
    {
        get
        {
            return PanelBotones.Visible;
        }
        set
        {
            PanelBotones.Visible = value;
        }
    }

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
                HiddenIdAvaluo.Value = (WebUtils.QueryString(Constantes.PAR_IDAVALUO)).Trim();
                HiddenNumUniAv.Value = (WebUtils.QueryString(Constantes.PAR_NUMUNIAVALUO)).Trim();
                CargarControl();
            }
        }
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
    /// Cargar control.
    /// </summary>
    protected void CargarControl()
    {
        DseAvaluoConsulta dse = new DseAvaluoConsulta();
        dse = ClienteAvaluo.ObtenerAvaluo(((HiddenIdAvaluo.Value).Trim()).ToInt());
        if (dse.FEXAVA_AVALUO_V.Count == 1)
        {
            if(!dse.FEXAVA_AVALUO_V[0].IsTIPONull())
                lblTipoDato.Text = dse.FEXAVA_AVALUO_V[0].TIPO;
            if (!dse.FEXAVA_AVALUO_V[0].IsESTADONull())
                lblEstadoDato.Text = dse.FEXAVA_AVALUO_V[0].ESTADO;
            if (!dse.FEXAVA_AVALUO_V[0].IsFECHA_DOCDIGITALNull())
                lblFechaDato.Text = dse.FEXAVA_AVALUO_V[0].FECHA_DOCDIGITAL.ToShortDateString();
            if (!dse.FEXAVA_AVALUO_V[0].IsVALORCOMERCIALNull())
                lblValorComercialDato.Text = dse.FEXAVA_AVALUO_V[0].VALORCOMERCIAL.ToCurrency();
            if (!dse.FEXAVA_AVALUO_V[0].IsVALORCATASTRALNull())
                lblValorCatastralDato.Text = dse.FEXAVA_AVALUO_V[0].VALORCATASTRAL.ToCurrency();
            if (!dse.FEXAVA_AVALUO_V[0].IsVALORREFERIDONull())
                lblValorReferidoDato.Text = dse.FEXAVA_AVALUO_V[0].VALORREFERIDO.ToCurrency();
            if (!dse.FEXAVA_AVALUO_V[0].IsNOMBRE_NOTARIONull())
                lblNotarioDato.Text = dse.FEXAVA_AVALUO_V[0].NOMBRE_NOTARIO;
            if (!dse.FEXAVA_AVALUO_V[0].IsVIGENTENull())
                lblVigenteDato.Text = dse.FEXAVA_AVALUO_V[0].VIGENTE;          
            lblObjetoDato.Text = dse.FEXAVA_AVALUO_V[0].OBJETO;
            lblPropositoDato.Text = dse.FEXAVA_AVALUO_V[0].PROPOSITO;
            VerLinks(dse.FEXAVA_AVALUO_V[0].CODESTADOAVALUO);              
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por modalEstado para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void modalEstado_ConfirmClick(object sender, CancelEventArgs e)
    {
        try
        {
            if (!e.Cancel)
            {
                int codEstado = ModalEstado1.CodNuevoEstadoAvaluo;
                ClienteAvaluo.CambiarEstadoAvaluo(Convert.ToDecimal((HiddenIdAvaluo.Value).Trim()), codEstado);
                CargarControl();
            }
        }
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
    /// Ver links.
    /// </summary>
    /// <param name="codEstado">El código estado.</param>
    protected void VerLinks(decimal codEstado)
    {
        try
        {
            if (codEstado == (decimal)EstadosAvaluoEnum.PendienteValidacion)
            {
                lnkAceptado.Visible = false;
                lnkEnValidacion.Visible = true;
                lnkRechazado.Visible = false;
            }
            else if (codEstado == (decimal)EstadosAvaluoEnum.EnValidacion)
            {
                lnkAceptado.Visible = true;
                lnkEnValidacion.Visible = false;
                lnkRechazado.Visible = true;
            }
            else 
            {
                lnkAceptado.Visible = false;
                lnkEnValidacion.Visible = false;
                lnkRechazado.Visible = false;
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
    /// Manejador de eventos. Llamado por lnkEnValidacion para eventos click.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void lnkEnValidacion_Click(object sender, EventArgs e)
    {
        try
        {
            ClienteAvaluo.CambiarEstadoAvaluo(Convert.ToDecimal((HiddenIdAvaluo.Value).Trim()), Convert.ToDecimal(EstadosAvaluoEnum.Aceptado));
            CargarControl();
        }
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
    /// Manejador de eventos. Llamado por lnkAceptado para eventos click.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void lnkAceptado_Click(object sender, EventArgs e)
    {
        try
        {
            ClienteAvaluo.CambiarEstadoAvaluo(Convert.ToDecimal((HiddenIdAvaluo.Value).Trim()), Convert.ToDecimal(EstadosAvaluoEnum.Aceptado));
            CargarControl();
        }
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
    /// Manejador de eventos. Llamado por lnkRechazado para eventos click.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void lnkRechazado_Click(object sender, EventArgs e)
    {
        try
        {
            ClienteAvaluo.CambiarEstadoAvaluo(Convert.ToDecimal((HiddenIdAvaluo.Value).Trim()), Convert.ToDecimal(EstadosAvaluoEnum.Rechazado));
            CargarControl();
        }
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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

    /// <summary>
    /// Mostrar mensaje de error asociado a una excepcion.
    /// </summary>
    /// <param name="mensaje">El mensaje de error que se quiere mostrar.</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }
}
