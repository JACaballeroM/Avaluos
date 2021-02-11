using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceAvaluos;
using ServiceMarcarSituaciones;
using ServiceRCON;
using SIGAPred.Common.Extensions;
using SIGAPred.Common.Seguridad;
using SIGAPred.Common.WCF;
using System.Text;
using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.Reporting.WebForms;
using MyExtentions;
using System.Web;

/// <summary>
/// Página bandeja de entrada.
/// </summary>
public partial class CuentasDuplicadas : PageBaseAvaluos
{
   
    /// <summary>
    /// Contiene el criterio de vigencia seleccionado.
    /// </summary>
    private string Vigencia;

    #region PROPIEDADES

    /// <summary>
    /// Conitene la busqueda actual.
    /// </summary>
    protected TipoFiltroBusqueda _busquedaActual;

    protected string region { get { return txtRegion.Text; } set { txtRegion.Text = value; } }
    protected string manzana { get { return txtManzana.Text; } set { txtManzana.Text = value; } }
    protected string lote { get { return txtLote.Text; } set { txtLote.Text = value; } }
    protected string unidad { get { return txtUnidadPrivativa.Text; } set { txtUnidadPrivativa.Text = value; } }
    protected string registroPerito { get { return textNumeroPerito.Text.Equals(NO_SEL)? string.Empty:textNumeroPerito.Text; } set { textNumeroPerito.Text = string.IsNullOrEmpty(value) ? NO_SEL : value; } }
    protected string nombrePerito { get { return textNombre.Text.Equals(NO_SEL) ? string.Empty : textNombre.Text.ToUpper(); } set { textNombre.Text = string.IsNullOrEmpty(value) ? NO_SEL : value; } }
    protected DateTime fechaInicio { 
        get {
            try
            {
                return Convert.ToDateTime(txtFechaIni.Text); 
            }
             catch
            {
                AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
                return Convert.ToDateTime(txtFechaIni.Text); 
            }
        } 
    }
    protected const string NO_SEL = "No seleccionado"; 
    protected const string DS = "DataSetCuentas";
    protected DateTime fechaFin
    {
        get
        {
            try
            {
                return Convert.ToDateTime(txtFechaFin.Text);
            }
            catch
            {
                AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
                return Convert.ToDateTime(txtFechaFin.Text);
            }
        }
    }

    /// <summary>
    /// Valores que representan el TipoFiltroBusqueda.
    /// </summary>
    protected enum TipoFiltroBusqueda
    {
        /// <summary>
        /// .
        /// </summary>
        Fecha = 0,
        /// <summary>
        /// .
        /// </summary>
        NumeroAvaluo = 1,
        /// <summary>
        /// .
        /// </summary>
        Estado = 2,
        /// <summary>
        /// .
        /// </summary>
        CuentaCatastral = 3,
        /// <summary>
        /// .
        /// </summary>
        IdAvaluo = 4
    }

    #endregion

    #region PAGE

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

    protected void LoadItemsDdl()
    {
        ddlDowanload.Items.Add("PDF");
        ddlDowanload.Items.Add("EXCEL");
    }

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
                AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
                LoadItemsDdl();
                
            }
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnDownload);
            UpdatePanelFiltro.Update();

            //UpdatePanelBotonesGridView.Update();
        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInfo(ex.Message, true);
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

    #endregion

    #region CARGAR COMBOS

    /// <summary>
    /// Cargar combo código estados avalúo.
    /// </summary>
   

    #endregion

    #region OTROS METODOS

    /// <summary>
    /// Busqueda inicial.
    /// </summary>
    private void BusquedaInicial()
    {
        AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
        //RealizarBusqueda();
        //ActualizarFiltroBusqueda();
    }


    /// <summary>
    /// Asignar fechas por defecto.
    /// </summary>
    /// <param name="txtFechaInicio">El control texto fecha inicio.</param>
    /// <param name="txtFechaFin">El control texto fecha fin.</param>
    protected void AsignarFechasPorDefecto(TextBox txtFechaInicio, TextBox txtFechaFin)
    {
        try
        {
            txtFechaInicio.Text = DateTime.Now.AddDays(-30).ToShortDateString();
            txtFechaFin.Text = DateTime.Now.ToShortDateString();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Actualizar texto perito.
    /// </summary>
    /// <param name="tipoFiltroBusqueda">Tipo del filtro busqueda selecionado</param>
    protected void actualizarTxtPerito(TipoFiltroBusqueda tipoFiltroBusqueda)
    {
        try
        {
            if (Condiciones.Web(Constantes.FUN_PERITO))
            {
                this.textNumeroPerito.Enabled = false;
                this.textNumeroPerito.Visible = false;
            }

            if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
            {
                if (tipoFiltroBusqueda == TipoFiltroBusqueda.NumeroAvaluo)
                {
                    this.textNumeroPerito.Enabled = true;
                    this.textNumeroPerito.Visible = true;
                }
                else
                {
                    this.textNumeroPerito.Enabled = false;
                    this.textNumeroPerito.Visible = true;
                }
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
    /// Inicializar controles.
    /// </summary>
    private void InicializarControles()
    {
       
    }

    /// <summary>
    /// Cargar pagina.
    /// </summary>
    /// <param name="tipoFiltroBusqueda">El/La tipo filtro busqueda.</param>
    protected void CargarPagina(TipoFiltroBusqueda tipoFiltroBusqueda)
    {
        try
        {
            string IdPersona = Usuarios.IdPersona();
            HiddenIdPersonaToken.Value = IdPersona;
            InicializarControles();

            _busquedaActual = tipoFiltroBusqueda;
            FBusqueda.Tipo = tipoFiltroBusqueda.ToString();

           
            UpdatePanelGridBuscador.Update();
        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInfo(ex.Message, true);
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
    /// Actualizar filtro busqueda.
    /// </summary>
    private void ActualizarFiltroBusqueda()
    {
        FBusqueda.FechaIni = string.Empty;
        FBusqueda.FechaFin = string.Empty;
        FBusqueda.Lote = string.Empty;
        FBusqueda.Manazana = string.Empty;
        FBusqueda.Region = string.Empty;
        FBusqueda.UnidadPrivativa = string.Empty;
        FBusqueda.Idavaluo = string.Empty;
        FBusqueda.NumAvaluo = string.Empty;
        FBusqueda.CodEstado = string.Empty;
        FBusqueda.Tipo = string.Empty;
        FBusqueda.Vigencia = string.Empty;

        FBusqueda.FechaIni = txtFechaIni.Text;
        FBusqueda.FechaFin = txtFechaFin.Text;
        FBusqueda.Lote = txtLote.Text;
        FBusqueda.Manazana = txtManzana.Text;
        FBusqueda.Region = txtRegion.Text;
        FBusqueda.UnidadPrivativa = txtUnidadPrivativa.Text;
       
    }

    /// <summary>
    /// Asignar valores busqueda.
    /// </summary>
    private void AsignarValoresBusqueda()
    {
        //Fecha
       
    }

    /// <summary>
    /// Asigna al campo HiddenVigente el valor que corresponda según el radiobutton seleccionado
    /// (vigente, no vigente o todos)
    /// </summary>
    private void ObtenerValorVigencia()
    {
       
    }

    /// <summary>
    /// Realizar busqueda.
    /// </summary>
    private void RealizarBusqueda()
    {
        AvaluosClient cliente = new AvaluosClient();
        DataSet ds = cliente.GetCuentasDuplicadas(fechaInicio, fechaFin, region, manzana, lote, unidad, registroPerito, false);
        CargarGridView(ds);
    }

    #endregion

    #region SELECCIONAR CRITERIO BUSQUEDA

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s  a la busqueda por por
    /// fecha.
    /// </summary>
    private void DesactivarFechas()
    {
        txtFechaIni.Text = string.Empty;
        txtFechaFin.Text = string.Empty;

        txtFechaFin.Enabled = false;
        txtFechaIni.Enabled = false;

        rfvFechaFin.Enabled = false;
        rfvFechaInicial.Enabled = false;

        btnFechaFin.Enabled = false;
        btnFechaIni.Enabled = false;

        cvFechaInicial.Enabled = false;
        cvFechaFin.Enabled = false;
        cvRangofechas.Enabled = false;
    }

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s  a la busqueda por
    /// numAvaluo.
    /// </summary>
    private void DesactivarNumAvaluo()
    {
        textNumeroPerito.Text = string.Empty;

        textNumeroPerito.Enabled = false;

        textNumeroPerito.Text = string.Empty;
        desactivarBtnPerito();

        revNumeroPerito.Enabled = false;
    }

   
    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s a la busqueda por cuenta
    /// catastral.
    /// </summary>
    private void DesactivarCuentaCatastral()
    {
        txtRegion.Text = string.Empty;
        txtManzana.Text = string.Empty;
        txtLote.Text = string.Empty;
        txtUnidadPrivativa.Text = string.Empty;

        txtRegion.Enabled = false;
        txtManzana.Enabled = false;
        txtLote.Enabled = false;
        txtUnidadPrivativa.Enabled = false;

        rfvRegion.Enabled = false;
        rfvRegionExp.Enabled = false;
        rfvManzanaExp.Enabled = false;
        rfvManzana.Enabled = false;
        rfvLote.Enabled = false;
        rfvLoteExp.Enabled = false;
        rfvUnidadPrivativa.Enabled = false;
        rfvUnidadPrivativaExp.Enabled = false;
    }

    
   

   

    #endregion

    #region SELECCIONAR CRITERIO BUSQUEDA

   

    #endregion

    #region GRIDVIEW

    /// <summary>
    /// Bindar grid avaluos.
    /// </summary>
    private void BindarGridAvaluos()
    {
        gridViewAvaluos.PageIndex = 0;
        gridViewAvaluos.DataBind();
    }


    protected void btnDownload_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AvaluosClient cliente = new AvaluosClient();
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streams;
            string mimeType, encoding, fileNameExtension, reportType = ddlDowanload.SelectedItem.Text;
            DataSet ds = cliente.GetCuentasDuplicadas(fechaInicio, fechaFin, region, manzana, lote, unidad, registroPerito, true);
            //Creamos el origen de datos
            LocalReport rpvCuentas = new LocalReport();

            //Seleccionamos la dirección del reporte y el origen de los datos
            rpvCuentas.ReportPath = @"ReportDesign\CtasDuplicadas.rdlc";
            ReportDataSource rdsDataCtas = new ReportDataSource("DataSet1", ds.Tables[0]);

            //Añadimos la validacion si es reporte de cuenta Empadronadas o NO empadornadas

            //Añadimos el reportDataSource al reportViewer
            rpvCuentas.DataSources.Add(rdsDataCtas);

            //Creamos el render
            byte[] renderedBytes = rpvCuentas.Render( reportType, null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

            //Response.AddHeader("content-disposition", "attachment; filename=" + NombreReporte + fileNameExtension);
            //Response.AddHeader("Content-Length", renderedBytes.Length.ToString());

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.ContentType = mimeType;
            Response.AddHeader("Content-Disposition", "attachment;filename=ReporteDuplicadas."+fileNameExtension);
            Response.BinaryWrite(renderedBytes);
            try
            {
                Response.Flush();
                Response.Close();
            }
            catch  { }

            //Response.BinaryWrite(renderedBytes);
            //Response.End();
        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInfo(ex.Message, true);
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
    /// Cargar grid view.
    /// </summary>
    protected void CargarGridView(DataSet ds)
    {
        try
        {
            ViewState[DS] = ds;
            gridViewAvaluos.DataSource = ds;
            //gridViewAvaluos.
            gridViewAvaluos.DataBind();
            if (gridViewAvaluos.Rows.Count > 0)
            {
                divDownload.Visible = true;
                decimal total = ds.Tables[0].Rows[0].ToDecimalValue("ROWS_TOTAL");
                if (total > 100)
                {
                    lblCount.Text = string.Format("{0} registros encontrados sólo se muestran 100, en el reporte aparecerán completos.", total);
                }
                else
                {
                    lblCount.Text = string.Format("{0} registros encontrados.", total);
                }

            }
            else
            {
                divDownload.Visible = false;
                lblCount.Text = string.Empty;
            }

            UpdatePanelGridBuscador.Update();

        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInfo(ex.Message, true);
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
    /// Asigna el criterio de ordenación al gridview avalúo (SortExpression + SortDirection)
    /// </summary>
    protected void CargarSort()
    {
        try
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
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
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
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos selected index cambio.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            
            string numeroNotario = gridViewAvaluos.SelectedDataKey[Constantes.COL_NUMNOTARIO].ToString().Trim();
            string codEstado = gridViewAvaluos.SelectedDataKey[Constantes.COL_CODESTADOAVALUO].ToString().Trim();
            string codTipoTramite = gridViewAvaluos.SelectedDataKey[Constantes.COL_CODTIPOTRAMITE].ToString().Trim();
            //Validar si el avalúo seleccionado tiene alamacenado el xml del avalúo
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
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos page index changing.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gridViewAvaluos.DataSource = ViewState[DS];
            gridViewAvaluos.PageIndex = e.NewPageIndex;
            gridViewAvaluos.DataBind();
            UpdatePanelGridBuscador.Update();


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
    /// Sustituye las descripciones del tipo avalúo (Catastral/Comercial)
    /// por sus descripciones acortadas (Cat/Com)
    /// </summary>
    private void gridViewAvaluos_formatearColTipo()
    {
        for (int i = 0; i < gridViewAvaluos.Rows.Count; i++)
        {
            if (((gridViewAvaluos.Rows[i].Cells[9].Text).Trim()).Equals(Constantes.PAR_XML_AV__COMERCIAL))
            {
                gridViewAvaluos.Rows[i].Cells[9].Text = Constantes.PAR_AVALUO_COMERCIAL_SHORT;
            }
            if (((gridViewAvaluos.Rows[i].Cells[9].Text).Trim()).Equals(Constantes.PAR_XML_AV_CATASTRAL))
            {
                gridViewAvaluos.Rows[i].Cells[9].Text = Constantes.PAR_AVALUO_CATASTRAL_SHORT;
            }
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos pre render.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_PreRender(object sender, EventArgs e)
    {
        try
        {
            gridViewAvaluos_formatearColTipo();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    #endregion

    #region BOTONES

    #region Activar/Desactivar/Ocultar botones

   

    /// <summary>
    /// Activar botón perito.
    /// </summary>
    private void activarBtnPerito()
    {
        btnPeritos.Visible = true;
        btnPeritos.Enabled = true;
        btnPeritos.ImageUrl = Constantes.URL_IMG_USER;
    }


    /// <summary>
    /// Ocultar botón perito.
    /// </summary>
    private void ocultarBtnPerito()
    {
       
    }

    /// <summary>
    /// Desactivar botón perito.
    /// </summary>
    private void desactivarBtnPerito()
    {
        
    }

   

   

    #endregion

   

   

    #endregion

    #region EVENTOS

    

    /// <summary>
    /// Manejador de eventos. Llamado por btnNotarioHidden para eventos value cambio.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnNotarioHidden_ValueChanged(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Manejador de eventos. Llamado por btnPeritos para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnPeritos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnPeritos_ModalPopupExtender.Show();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

   

    

    /// <summary>
    /// Borra los criterios de búsqueda y los establezce a los valores por defecto.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnLimpiar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ClearPage();
            UpdatePanelFiltro.Update();
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

    private void ClearPage()
    {
        region = string.Empty;
        manzana = string.Empty;
        lote = string.Empty;
        unidad = string.Empty;
        AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
        registroPerito = string.Empty;
        nombrePerito = string.Empty;
        lblTextoError.Text = string.Empty;
            
    }

    public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
    {
        return Math.Abs((fechaDesde.Month - fechaHasta.Month) + 12 * (fechaDesde.Year - fechaHasta.Year));
    }

    public bool EsCuentaValida()
    {
        bool valido = true;

        if (!string.IsNullOrEmpty(region) || !string.IsNullOrEmpty(manzana) || !string.IsNullOrEmpty(lote) || !string.IsNullOrEmpty(unidad))
        {
            if (!string.IsNullOrEmpty(region) && !string.IsNullOrEmpty(manzana) && !string.IsNullOrEmpty(lote) && !string.IsNullOrEmpty(unidad))
                valido = true;
            else
                valido = false;
        }
        return valido;
    }

    private bool EsFiltroValido(out string mensaje)
    {
        bool valido = true;
        mensaje = string.Empty;
        StringBuilder sb = new StringBuilder();
        if (CalcularMesesDeDiferencia(fechaInicio, fechaFin) > 6)
        {
            sb.AppendLine("* El rango de fechas debe ser menor a 6 meses");
            valido = false;
        }
        if (!EsCuentaValida())
        {
            sb.AppendLine("* La cuenta catastral debe ser completa");
            valido = false;
        }
        mensaje = sb.ToString();
        return valido;
    }

    /// <summary>
    /// Manejador de eventos. Llamado por btnBuscar para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string mensaje = string.Empty;
            if (EsFiltroValido(out mensaje))
            {
                lblTextoError.Text = string.Empty;
                RealizarBusqueda();

                //continua
            }
            else
            {
                lblTextoError.Text = string.Empty;
                lblTextoError.Text = mensaje;  
            }
            UpdatePanelFiltro.Update();
            //AsignarValoresBusqueda();
            //ActualizarFiltroBusqueda();
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
    /// Manejador de eventos. Llamado por modalEstado para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void modalEstado_ConfirmClick(object sender, CancelEventArgs e)
    {
        try
        {
            if (!e.Cancel)
            {
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
    /// Manejador de eventos. Llamado por RadioButtonTodos para eventos checked cambio.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void RadioButtonTodos_CheckedChanged(object sender, EventArgs e)
    {

    }

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
            {
                extenderPnlInfoTokenModal.Hide();
            }
            else
            {
                extenderPnlInfoTokenModal.Show();
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
    /// Manejador de eventos. Llamado por buscarPerito para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void buscarPerito_ConfirmClick(object sender, CancelEventArgs e)
    {
        try
        {
            if (!e.Cancel)
            {
                btnPeritos_ModalPopupExtender.Show();
            }
            else
            {
                btnPeritos_ModalPopupExtender.Hide();

                if (ModalBuscarPeritos1.Seleccionado)
                {
                    if (!string.IsNullOrEmpty(ModalBuscarPeritos1.NumeroRegistro))
                    {
                        registroPerito = ModalBuscarPeritos1.NumeroRegistro.ToString().Trim();
                        nombrePerito = ModalBuscarPeritos1.Nombre;
                    }
                }
                UpdatePanelFiltro.Update();
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


    #region FILTRO

   

    #endregion

    #region MOSTRAR MESAJES

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

    /// <summary>
    /// Mostrar mensaje información.
    /// </summary>
    /// <param name="mensaje">El/La mensaje.</param>
    /// <param name="tipoMensaje">Verdadero para tipo mensaje.</param>
    private void MostrarMensajeInfo(string mensaje, bool tipoMensaje)
    {
        ModalInfoToken.TextoInformacion = mensaje;
        ModalInfoToken.TipoMensaje = tipoMensaje;
        extenderPnlInfoTokenModal.Show();
    }

    #endregion
}
