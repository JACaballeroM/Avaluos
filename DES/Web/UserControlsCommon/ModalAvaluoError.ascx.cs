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
using System.ComponentModel;
using ServiceAvaluos;
using System.ServiceModel;

/// <summary>
/// Control de usuario para modales de error
/// </summary>
public partial class UserControlsCommon_ModalAvaluoError : UserControlBase
{

    #region Delegado y Evento

    /// <summary>
    /// Delegadi para manejar las confirmaciones
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="e">Evento</param>
    public delegate void ConfirmClickHandler(object sender, CancelEventArgs e);
    /// <summary>
    /// Evento para manejar las confirmaciones
    /// </summary>
    public event ConfirmClickHandler ConfirmClick;

    private void LaunchConfirmClickHandler(object sender, CancelEventArgs e)
    {
        if (ConfirmClick != null)
        {
            ConfirmClick(sender, e);
        }
    }

    #endregion

    #region Propiedades
    /// <summary>
    /// Dataset donde se almacenan los errores de validación
    /// </summary>
    public DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable ErrorDT
    {
        get
        {
            if (this.ViewState[Constantes.PAR_VIEWSTATE_ERRORDT] == null)
                this.ViewState[Constantes.PAR_VIEWSTATE_ERRORDT] = new DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable();
            return (DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable)this.ViewState[Constantes.PAR_VIEWSTATE_ERRORDT];
        }
        set 
        {
            this.ViewState[Constantes.PAR_VIEWSTATE_ERRORDT] = value;
            CargarGrid();
        }
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gvErroresAvaluo.PageIndex = 0;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Carga del grid.
    /// </summary>
    private void CargarGrid()
    {
        gvErroresAvaluo.DataSource = ErrorDT;
        gvErroresAvaluo.DataBind();
    }

    /// <summary>
    /// Paginado del grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvErroresAvaluo_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvErroresAvaluo.PageIndex = e.NewPageIndex;
            CargarGrid();
            UpdatePanelGrid.Update();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Lanzamos el evento aceptar
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAceptar_Click(object sender, EventArgs e)
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
        System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString() + " MostrarMensajeInfoExcepcion : Exception: " + mensaje + "\n\r" );

        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }
}
