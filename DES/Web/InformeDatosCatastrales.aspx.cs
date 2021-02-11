using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using Microsoft.Reporting.WebForms;
using ServiceAvaluos;

/// <summary>
/// Clase encargada de gestionar el informe de datos catastrales
/// </summary>
public partial class InformeDatosCatastrales : PageBaseAvaluos
{

    /// <summary>
    /// Load de la página InformeConsultaEspecifica.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                //Ocultamos el report hasta que se agrege un DataSource
                CargarDatos();
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
    /// Carga los datos que se van a mostrar en el informe
    /// </summary>
    private void CargarDatos()
    {
        try
        {
            dsInvMercado ds = ArmarDataset();
            rpvDatosUnidadesCatastrales.LocalReport.DataSources.Clear();
            rpvDatosUnidadesCatastrales.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", (DataTable)ds.DatosInvMercado));
            rpvDatosUnidadesCatastrales.LocalReport.Refresh();
            rpvDatosUnidadesCatastrales.Visible = true;
        }
        catch (Exception ex)
        {
            //ExceptionPolicy.HandleException(ex, Constantes.POLITICASEGURIDAD);
            throw ex;
        }
    }

    private dsInvMercado ArmarDataset()
    {
        dsInvMercado ret = new dsInvMercado();
        if (SessionVariables.DsReporte != null)
        {
            foreach (DataRow row in SessionVariables.DsReporte.Tables[0].Rows)
            {
                dsInvMercado.DatosInvMercadoRow newRow = ret.DatosInvMercado.NewDatosInvMercadoRow();
                newRow.REGION = row["REGION"].ToString();
                newRow.MANZANA = row["MANZANA"].ToString();
                newRow.DELEGACION = row["DELEGACION"].ToString();
                newRow.UBICACION = row["UBICACION"].ToString();
                newRow.DESCRIPCION = row["DESCRIPCION"].ToString();
                newRow.TIPO = row["TIPO"].ToString();
                newRow.COLONIA = row["COLONIA"].ToString();
                newRow.PRECIOSOLICITADO = row["PRECIOSOLICITADO"].ToString();
                newRow.SUPERFICIE = row["SUPERFICIE"].ToString();
                newRow.VU = row["VALORUNITARIO"].ToString();
                ret.DatosInvMercado.AddDatosInvMercadoRow(newRow);
            }
            SessionVariables.Dispose();
        }

        return ret;
    }
     
   
    
    /// <summary>
    /// Nos redirige a la página referida (PAGINA ORIGEN)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnVolver_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/InvMercado.aspx");
            //RedirectUtil.BaseURL = Constantes.URL_DATOSGENERALES_V;
            //RedirectUtil.AddParameter(Constantes.PAR_IDSOLICITUD, HiddenIdSolicitud.Value);
            //RedirectUtil.AddParameter(Constantes.PAR_BANDEJAORIGEN, BandejaOrigen);
            //RedirectUtil.AddParameter(Constantes.REQUEST_FILTRO, FBusqueda.ObtenerStringFiltro());
            //RedirectUtil.AddParameter(Constantes.REQUEST_SORTEXP, SortExpression);
            //RedirectUtil.AddParameter(Constantes.REQUEST_SORTDIR, SortDirectionP);
            //RedirectUtil.Encrypted = true;
            //RedirectUtil.Go();
        }
        catch (System.Threading.ThreadAbortException)
        {
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

    #region Excepciones

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
            string msj = ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = "Error en la aplicación";
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }

    #endregion

}
