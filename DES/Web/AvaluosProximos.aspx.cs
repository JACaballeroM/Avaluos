using System;
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
using System.ServiceModel;
using System.Collections;
using System.Collections.Generic;
using SIGAPred.Common.Web;
using SIGAPred.Common.Extensions;
using SIGAPred.Common.Token;
using SIGAPred.Common.Seguridad;
using SIGAPred.Seguridad.Utilidades.ClaimTypes;
using Microsoft.IdentityModel.Claims;
using ServiceAvaluos;

/// <summary>
/// Página avalúos próximos.
/// </summary>
public partial class AvaluosProximos : PageBaseAvaluos
{
    #region PROPIEDADES
    /// <summary>
    /// Contiene el identificador del avalúo seleccionado.
    /// </summary>
    private int idAvaluoSeleccionado;
    #endregion 

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
                HiddenIdAvaluoProximo.Value = (SIGAPred.Common.Web.WebUtils.QueryString(Constantes.PAR_IDAVALUO)).Trim();
                IdAvaluo.Value = SIGAPred.Common.Web.WebUtils.QueryString(Constantes.PAR_IDAVALUO);
                lblCuenta.Text = SIGAPred.Common.Web.WebUtils.QueryString(Constantes.PAR_CUENTACAT);
                FBusqueda.RellenarObjetoFiltro(Request.QueryString[Constantes.REQUEST_FILTRO]);
                SortDirectionP = SIGAPred.Common.Web.WebUtils.QueryString(Constantes.REQUEST_SORTDIR);
                SortExpression = SIGAPred.Common.Web.WebUtils.QueryString(Constantes.REQUEST_SORTEXP);
                HiddenIdPersonaToken.Value = Usuarios.IdPersona();
                CargarSort();
                //if (Condiciones.Web(Constantes.FUN_PERITO))
                //{
                //    gridViewAvaluos.DataSourceID = odsAvaluosProximos_idPerito.ID;
                //}
                //else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                //{
                //    gridViewAvaluos.DataSourceID = odsAvaluosProximos_idSoci.ID;
                //}
                //else if (Condiciones.Web(Constantes.FUN_DICTAMENES))
                //{
                //    gridViewAvaluos.DataSourceID = odsAvaluosProximos.ID;
                //}
                //else if (Condiciones.Web(Constantes.FUN_FINANZAS))
                //{
                //    gridViewAvaluos.DataSourceID = odsAvaluosProximos.ID;//odsAvaluosProximosFinanzas.ID;
                //}

                if (Condiciones.Web(Constantes.FUN_DICTAMENES))
                {
                    gridViewAvaluos.DataSourceID = odsAvaluosProximos.ID;
                }
                else if (Condiciones.Web(Constantes.FUN_FINANZAS))
                {
                    gridViewAvaluos.DataSourceID = odsAvaluosProximos.ID;//odsAvaluosProximosFinanzas.ID;
                }
                else  if (Condiciones.Web(Constantes.FUN_PERITO))
                {
                    gridViewAvaluos.DataSourceID = odsAvaluosProximos_idPerito.ID;
                }
                else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                {
                    gridViewAvaluos.DataSourceID = odsAvaluosProximos_idSoci.ID;
                } 
            }
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
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }
    #endregion 

    #region GRIDVIEW
    /// <summary>
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos selected index cambio.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {   
            gridViewAvaluos_verinforme(null, null);
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
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos ordenación.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            SortExpression = e.SortExpression;
            SortDirectionP = e.SortDirection.ToString();
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
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos verinforme.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_verinforme(object sender, EventArgs e)
    {
        if (gridViewAvaluos.SelectedDataKey != null)
        {
            idAvaluoSeleccionado = gridViewAvaluos.SelectedDataKey.Value.ToString().Trim().ToInt();
            //Validar si el avalúo seleccionado tiene alamacenado el xml del avalúo
            bool tieneXml = ExisteXMLAsociado(idAvaluoSeleccionado);
            if (!tieneXml)
            {
                //mostrar mensaje indicando que no existe el xml
                string msj = Constantes.MSJ_ERROR_FALTANDATOSXML_NODETALLE;
                MostrarMensajeInfo(msj, true);
            }
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos row data bound.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex >= 0)
            {
                bool tieneXml = ExisteXMLAsociado((gridViewAvaluos.DataKeys[e.Row.RowIndex].Value).ToDecimal());
                if (tieneXml) //Si existe xml asociado habilitar el botón ver informe avalúo
                {
                    ((HyperLink)e.Row.FindControl(Constantes.BOTON_VERAVALUO_AVALUOSPROXIMOS)).Enabled = true;
                    ((HyperLink)e.Row.FindControl(Constantes.BOTON_VERAVALUO_AVALUOSPROXIMOS)).ImageUrl = Constantes.URL_IMG_VER_DETALLE;
                    ((HyperLink)e.Row.FindControl(Constantes.BOTON_VERAVALUO_AVALUOSPROXIMOS)).NavigateUrl = Constantes.URL_AVALUOS_INFORME + "?" + Constantes.PAR_IDAVALUO + "=" + gridViewAvaluos.DataKeys[e.Row.RowIndex].Value.ToString();
                }
                else//Si NO existe xml asociado DEShabilitar el botón ver informe avalúo
                {
                    ((HyperLink)e.Row.FindControl(Constantes.BOTON_VERAVALUO_AVALUOSPROXIMOS)).Enabled = false;
                    ((HyperLink)e.Row.FindControl(Constantes.BOTON_VERAVALUO_AVALUOSPROXIMOS)).ImageUrl = Constantes.URL_IMG_VER_DETALLE_P;
                }
            }
            //Actualizar visibilidad de las colunmas del gridview dependiendo del usuario logeado.
            if (Condiciones.Web(Constantes.FUN_PERITO))
            {
                gridViewAvaluos.Columns[7].Visible = false;
                gridViewAvaluos.Columns[8].Visible = false;
            }
            else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
            {
                gridViewAvaluos.Columns[7].Visible = true;
                gridViewAvaluos.Columns[8].Visible = false;
            }
            else if (Condiciones.Web(Constantes.FUN_DICTAMENES) || Condiciones.Web(Constantes.FUN_FINANZAS))
            {
                gridViewAvaluos.Columns[7].Visible = true;
                gridViewAvaluos.Columns[8].Visible = true;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox checkboxVIG = (CheckBox)e.Row.FindControl(Constantes.CHECKBOX_VIGENCIA);
                checkboxVIG.Checked = e.Row.ConvertGridViewRow<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>().IsVIGENTENull() ? false : true;
                checkboxVIG.Enabled = false;
            }
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
    /// Cargar sort.
    /// </summary>
    protected void CargarSort()
    {
        SortDirection direction;
        SortExpression = Request[Constantes.REQUEST_SORTEXP];
        SortDirectionP = Request[Constantes.REQUEST_SORTDIR];
        switch (SortDirectionP)
        {
            case Constantes.SORTDIRECTION_ASCENDING:
                direction = SortDirection.Ascending;
                break;
            case Constantes.SORTDIRECTION_DESCENDING:
                direction = SortDirection.Descending;
                break;
            default:
                direction = SortDirection.Ascending;
                break;
        }
        gridViewAvaluos.Sort(SortExpression, direction);
    }
    #endregion 

    #region MOSTRAR MENSAJES
    /// <summary>
    /// Mostrar mensaje información.
    /// </summary>
    /// <param name="mensaje">El/La mensaje.</param>
    /// <param name="tipoMensaje">Verdadero para tipo mensaje.</param>
    private void MostrarMensajeInfo(string mensaje, bool tipoMensaje)
    {
        ModalInfoToken.TextoInformacion = mensaje;
        ModalInfoToken.TipoMensaje = tipoMensaje;
        uppErrorTareas.Update();
        extenderPnlInfoTokenModal.Show();
        uppErrorTareas.Update();
    }

    /// <summary>
    /// Mostrar mensaje información excepcion.
    /// </summary>
    /// <param name="mensaje">El/La mensaje.</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();

    }
    #endregion 

    #region EVENTOS
    /// <summary>
    /// Mostramos la exception del token.
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
    /// Manejador de eventos. Llamado por btnVolver para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        try
        {
            RedirectUtil.BaseURL = Constantes.URL_BANDEJAENTRADA;
            RedirectUtil.AddParameter(Constantes.REQUEST_FILTRO, FBusqueda.ObtenerStringFiltro());
            RedirectUtil.AddParameter(Constantes.REQUEST_SORTEXP, SortExpression);
            RedirectUtil.AddParameter(Constantes.REQUEST_SORTDIR, SortDirectionP);
            RedirectUtil.Go();
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
    #endregion 
}
