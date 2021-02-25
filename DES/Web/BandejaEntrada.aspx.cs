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

/// <summary>
/// Página bandeja de entrada.
/// </summary>
public partial class BandejaEntrada : PageBaseAvaluos
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

    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HiddenTipoRegistro.Value = "1";
            if (!IsPostBack)
            {
                if (PreviousPage != null)
                {
                    string name = PreviousPage.GetType().Name;
                    switch (name)
                    {
                        case "home_aspx":
                            GestorVisibilidadControles.ValidarControl(this.Controls, Request.Path);
                            CargarComboCodEstadosAvaluo();
                            BusquedaInicial();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    CargarSort();
                    if ((Request[Constantes.REQUEST_FILTRO] != null))
                    {
                        FBusqueda.RellenarObjetoFiltro(Request[Constantes.REQUEST_FILTRO]);
                        CargarCamposFiltro();
                        ObtenerValorVigencia();
                        EnabledButton(false, true);
                        if (FBusqueda.EsFecha())
                        {
                            actualizarTxtPerito(TipoFiltroBusqueda.Fecha);
                            SeleccionarBusquedaFecha();
                            RealizarBusqueda();
                        }
                        else if (FBusqueda.EsCuenta())
                        {
                            actualizarTxtPerito(TipoFiltroBusqueda.CuentaCatastral);
                            SeleccionarBusquedaCuenta();
                            RealizarBusqueda();
                        }
                        else if (FBusqueda.EsNumAvaluo())
                        {
                            actualizarTxtPerito(TipoFiltroBusqueda.NumeroAvaluo);
                            SeleccionarBusquedaNumAvaluo();
                            RealizarBusqueda();
                        }
                        else if (FBusqueda.EsIdavaluo())
                        {
                            actualizarTxtPerito(TipoFiltroBusqueda.IdAvaluo);
                            SeleccionarBusquedaIdAvaluo();
                            RealizarBusqueda();
                        }
                    }
                    else
                    {
                        GestorVisibilidadControles.ValidarControl(this.Controls, Request.Path);
                        CargarComboCodEstadosAvaluo();
                        BusquedaInicial();
                    }
                }
                hidBusquedaActual.Value = ((int)_busquedaActual).ToString();



            }
            else
            {
                _busquedaActual = (TipoFiltroBusqueda)(System.Convert.ToInt32(hidBusquedaActual.Value));
            }

            UpdatePanelBotonesGridView.Update();
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
    protected void CargarComboCodEstadosAvaluo()
    {
        try
        {
            
            DseAvaluosCatConsulta.FEXAVA_CATESTADOSAVALUODataTable catEstadosAvaluoDT = ApplicationCache.DseCatalogos.FEXAVA_CATESTADOSAVALUO;
            var newEstados = from c in catEstadosAvaluoDT where c.CODESTADOAVALUO == Constantes.CODESTADO_RECIBIDO || c.CODESTADOAVALUO == Constantes.CODESTADO_CANCELADO || c.CODESTADOAVALUO == Constantes.CODESTADO_ENVIADONOTARIO select new { Descripcion = c.DESCRIPCION, Codigo = c.CODESTADOAVALUO };
            if (catEstadosAvaluoDT.Any())
            {
                this.ddlEstado.DataSource = newEstados;
                this.ddlEstado.DataTextField = Constantes.DDLESTADO_DESCRIPCION;
                this.ddlEstado.DataValueField = Constantes.DDLESTADO_COD;
                this.ddlEstado.DataBind();
                this.ddlEstado.Items.Add(new ListItem(Constantes.DDLESTADO_TEXT_TODOS, Constantes.DDLESTADO_VALUE_TODOS));
              

            
            }
            //if (!Condiciones.Web(Constantes.FUN_FINANZAS))
            SeleccionarEstadoTodos();
            //else
            //    SeleccionarEstadoEnviadoNotario();
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

    #region OTROS METODOS

    /// <summary>
    /// Busqueda inicial.
    /// </summary>
    private void BusquedaInicial()
    {
        AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
        HiddenVigente.Value = Constantes.PAR_VIGENTE;
        HiddenNumUnico.Value = string.Empty;
        SeleccionarBusquedaFecha();
        RealizarBusqueda();
        ActualizarFiltroBusqueda();
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
                this.lblNumPerito.Visible = false;
            }

            if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
            {
                if (tipoFiltroBusqueda == TipoFiltroBusqueda.NumeroAvaluo)
                {
                    this.textNumeroPerito.Enabled = true;
                    this.textNumeroPerito.Visible = true;
                    this.lblNumPerito.Visible = true;
                }
                else
                {
                    this.textNumeroPerito.Enabled = false;
                    this.textNumeroPerito.Visible = true;
                    this.lblNumPerito.Visible = true;
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
        if (Condiciones.Web(Constantes.FUN_PERITO))
        {
            this.textNumeroPerito.Visible = false;
            this.lblNumPerito.Visible = false;
            this.lblNumPerito.Enabled = false;
            this.lblNumPerSoci.Visible = false;
            this.btnPeritos.Visible = false;
            this.btnPeritos.Enabled = false;
        }

        if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
        {
            this.lblNumPerito.Enabled = true;
            this.lblNumPerSoci.Visible = false;
            this.btnPeritos.Visible = true;
            this.btnPeritos.Enabled = true;
        }

        if (Condiciones.Web(Constantes.FUN_DICTAMENES))
        {
            // Deshabilitar y ocultar a las opciones a las que no puede acceder el perfil dictamenes
            this.btnCambiarEstado.Visible = false;
            this.btnCambiarEstado.Enabled = false;

            this.btnNotario.Visible = false;
            this.btnNotario.Enabled = false;

            ocultarBtnAcuseRecibo();

            this.lblNumPerito.Visible = false;
            this.lblNumPerSoci.Visible = true;

            this.btnPeritos.Visible = true;
            this.btnPeritos.Enabled = true;
        }

        if (Condiciones.Web(Constantes.FUN_FINANZAS))
        {
            // Deshabilitar y ocultar a las opciones a las que no puede acceder el perfil dictamenes
            this.btnCambiarEstado.Visible = false;
            this.btnCambiarEstado.Enabled = false;

            this.btnNotario.Visible = false;
            this.btnNotario.Enabled = false;

            ocultarBtnAcuseRecibo();

            //Dejar seleccionado el estado enviado notario y deshabilita y ocultar el control
            //this.ddlEstado.SelectedValue = Constantes.CODESTADO_ENVIADONOTARIO.ToString();
            //this.ddlEstado.Visible = false;
            //this.ddlEstado.Enabled = false;
            //this.lblEstado.Visible = false;

            this.lblNumPerito.Visible = false;
            this.lblNumPerSoci.Visible = true;

            this.btnPeritos.Visible = true;
            this.btnPeritos.Enabled = true;
        }

        UpdatePanelBotonesGridView.Update();
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

            switch (tipoFiltroBusqueda)
            {
                //Asiganr al grid el origen de datos que corresponda según el caso
                case TipoFiltroBusqueda.Fecha:
                    actualizarTxtPerito(TipoFiltroBusqueda.Fecha);

                    if (Condiciones.Web(Constantes.FUN_FINANZAS))
                    {
                        gridViewAvaluos.DataSourceID = odsPorFechaDictamenes.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_PERITO))
                    {
                        gridViewAvaluos.DataSourceID = odsPorFecha.ID;

                    }
                    else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                    {
                        gridViewAvaluos.DataSourceID = odsPorFechaSoci.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_DICTAMENES))
                    {
                        gridViewAvaluos.DataSourceID = odsPorFechaDictamenes.ID;
                    }
                    
                    break;
                case TipoFiltroBusqueda.NumeroAvaluo:
                    actualizarTxtPerito(TipoFiltroBusqueda.NumeroAvaluo);
                    if (Condiciones.Web(Constantes.FUN_FINANZAS))
                    {
                        gridViewAvaluos.DataSourceID = odsPorNumAvaluoDictamenes.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_PERITO))
                    {
                        gridViewAvaluos.DataSourceID = odsPorNumAvaluo.ID;

                    }
                    else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                    {
                        gridViewAvaluos.DataSourceID = odsPorNumAvaluoSoci.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_DICTAMENES))
                    {
                        gridViewAvaluos.DataSourceID = odsPorNumAvaluoDictamenes.ID;
                    }
                    
                    break;
                case TipoFiltroBusqueda.CuentaCatastral:
                    actualizarTxtPerito(TipoFiltroBusqueda.CuentaCatastral);
                    txtCuenta.Text = ComponerCuenta(txtRegion.Text, txtManzana.Text, txtLote.Text, txtUnidadPrivativa.Text);
                    if (Condiciones.Web(Constantes.FUN_FINANZAS))
                    {
                        gridViewAvaluos.DataSourceID = odsCuentaDictamenes.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_PERITO))
                    {
                        gridViewAvaluos.DataSourceID = odsCuentaCatastral.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                    {
                        gridViewAvaluos.DataSourceID = odsPorCuentaSoci.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_DICTAMENES))
                    {
                        gridViewAvaluos.DataSourceID = odsCuentaDictamenes.ID;
                    }
                    
                    break;
                case TipoFiltroBusqueda.IdAvaluo:
                    if (Condiciones.Web(Constantes.FUN_FINANZAS))
                    {
                        gridViewAvaluos.DataSourceID = odsPorIdAvaluoDictamenes.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_PERITO))
                    {
                        gridViewAvaluos.DataSourceID = odsPorIdAvaluo.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                    {
                        gridViewAvaluos.DataSourceID = odsPorIdAvaluoSoci.ID;
                    }
                    else if (Condiciones.Web(Constantes.FUN_DICTAMENES))
                    {
                        gridViewAvaluos.DataSourceID = odsPorIdAvaluoDictamenes.ID;
                    }
                    
                    break;
            }
            BindarGridAvaluos();

            if (gridViewAvaluos.Rows.Count > 0)
            {
                this.gridViewAvaluos.AllowSorting = true;
                //Los datakey son "IDAVALUO,CUENTACATASTRAL,NUMERO_NOTARIO"
                string listaInmuebles = string.Empty;
                foreach (GridViewRow row in gridViewAvaluos.Rows)
                {
                    string[] ccat = row.Cells[2].Text.Split('-');
                    listaInmuebles += ccat[0].ToString() + ccat[1].ToString() + ccat[2].ToString() + ccat[3].ToString() + ",";
                }
                //Eliminar la última coma, probar sin quitarla
                listaInmuebles = listaInmuebles.Substring(0, listaInmuebles.Length - 1);

                MarcajesClient clienteMarcajes = new MarcajesClient();
                DSMarcarSituaciones.MARCAJESCUENTASDataTable mcuentasDT = null;

                try
                {
                    mcuentasDT = clienteMarcajes.ObtenerMarcajesCuentasPorLista(listaInmuebles);
                }
                finally
                {
                    clienteMarcajes.Disconnect();
                }

                if (mcuentasDT.Any())
                {
                    CheckBox cbRow;
                    foreach (DSMarcarSituaciones.MARCAJESCUENTASRow mcuentasR in mcuentasDT)
                    {
                        if (!mcuentasR.IsCODMARCAJEMOTIVONull())
                        {
                            if (mcuentasR.CODMARCAJEMOTIVO == Constantes.PAR_VAL_IDMARCAJECUENTA_DICTAMINANDO)
                            {
                                string region = mcuentasR.REGION.ToString();
                                string manzana = mcuentasR.MANZANA.ToString();
                                string lote = mcuentasR.LOTE.ToString();
                                string unidadPrivativa = mcuentasR.UNIDADPRIVATIVA.ToString();

                                foreach (GridViewRow row in gridViewAvaluos.Rows)
                                {
                                    if (row.Cells[2].Text == ComponerCuenta(region, manzana, lote, unidadPrivativa))
                                    {
                                        //obtenemos la referencia al control checkbox
                                        cbRow = ((CheckBox)row.FindControl("RowCheckBox"));
                                        cbRow.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            this.gridViewAvaluos.AllowSorting = true;
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
    /// Comprueba si el estado seleccionado es todos.
    /// </summary>
    /// <returns>
    /// True si el estado seleccionado en el combo ddlEstado es todos y false en el resto de casos.
    /// </returns>
    private bool EstadoSeleccionadoEsTodos()
    {
        return (this.ddlEstado.SelectedValue.Equals(Constantes.DDLESTADO_VALUE_TODOS));
    }

    /// <summary>
    /// Actualizar URL infome.
    /// </summary>
    private void ActualizarUrlInfome()
    {
        RedirectUtil.BaseURL = Constantes.URL_AVALUOS_INFORME;
        RedirectUtil.AddParameter(Constantes.PAR_NUMUNIAVALUO_INF, (HiddenNumUnico.Value).Trim());
        //RedirectUtil.AddParameter(Constantes.PAR_IDAVALUO, (HiddenIdAvaluo.Value).Trim());
        //RedirectUtil.AddParameter(Constantes.PAR_PAGINAORIGEN, Constantes.URL_BANDEJAENTRADA);
        HlinkInforme.NavigateUrl = RedirectUtil.ToString();
    }

    /// <summary>
    /// Actualizar URL acuse.
    /// </summary>
    private void ActualizarUrlAcuse()
    {
        RedirectUtil.BaseURL = Constantes.URL_DESCARGA_ACUSE_AVALUO;
        RedirectUtil.AddParameter(Constantes.PAR_IDAVALUO, (HiddenIdAvaluo.Value).Trim());
        RedirectUtil.AddParameter(Constantes.PAR_NUMUNIAVALUO, (HiddenNumUnico.Value).Trim());
        System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r"
                   + DateTime.Now.ToString() + " " + "BandejaEntrada ActualizarUrlAcuse : URL_DESCARGA_ACUSE_AVALUO: " + Constantes.URL_DESCARGA_ACUSE_AVALUO + 
                   " | " + (HiddenIdAvaluo.Value).Trim() +
                   " | " + (HiddenNumUnico.Value).Trim() +
                   " | " + RedirectUtil.ToString() +
                   "\n\r");
        HlinkGenerarAcuse.NavigateUrl = RedirectUtil.ToString();
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
        FBusqueda.Idavaluo = txtIdAvaluo.Text.Trim();
        FBusqueda.NumAvaluo = textNumeroAvaluo.Text.Trim();
        FBusqueda.CodEstado = ddlEstado.SelectedValue.ToString();
        FBusqueda.Tipo = _busquedaActual.ToString();
        FBusqueda.Vigencia = HiddenVigente.Value.ToString();
    }

    /// <summary>
    /// Asignar valores busqueda.
    /// </summary>
    private void AsignarValoresBusqueda()
    {
        //Fecha
        HiddenFechaFin.Value = txtFechaFin.Text;
        HiddenFechaIni.Value = txtFechaIni.Text;

        //Num Avalúo
        HiddenNumAvaluo.Value = textNumeroAvaluo.Text.Trim();

        //IdAvaluo
        //Eliminar los ceros a la izquierda
        if (txtIdAvaluo.Text.Length > 0)
        {
            string numStr = (txtIdAvaluo.Text.Trim()).Substring(11, (txtIdAvaluo.Text.Trim().Length - 11));
            int num = numStr.ToInt();
            txtIdAvaluo.Text = (txtIdAvaluo.Text).Replace(numStr, num.ToString());
            // Fin eliminar ceros a la izquierda 
        }

        HiddenNumUnicoAv.Value = txtIdAvaluo.Text.Trim();

        //Cuenta catastral
        HiddenCuentaCatastral.Value = ComponerCuenta(txtRegion.Text, txtManzana.Text, txtLote.Text, txtUnidadPrivativa.Text);

        //Registro perito sociedad
        HiddenReg_PerSoci.Value = textNumeroPerito.Text.Trim();

        //Valores comunes  (Estado y Vigencia)
        HiddenEstado.Value = ddlEstado.SelectedValue.ToString();
        Vigencia = HiddenVigente.Value;
    }

    /// <summary>
    /// Asigna al campo HiddenVigente el valor que corresponda según el radiobutton seleccionado
    /// (vigente, no vigente o todos)
    /// </summary>
    private void ObtenerValorVigencia()
    {
        if (this.RadioButtonVigente.Checked)
        {
            HiddenVigente.Value = Constantes.PAR_VIGENTE;
        }
        else if (this.RadioButtonNoVigente.Checked)
        {
            HiddenVigente.Value = Constantes.PAR_NO_VIGENTE;
        }
        else if (this.RadioButtonTodos.Checked)
        {
            HiddenVigente.Value = Constantes.PAR_VIGENCIA_TODOS;
        }
    }

    /// <summary>
    /// Realizar busqueda.
    /// </summary>
    private void RealizarBusqueda()
    {
        txtIdAvaluo.Text = txtIdAvaluo.Text.Trim();
        ObtenerValorVigencia();
        EnabledButton(false, true);
        CargarGridView();
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
        textNumeroAvaluo.Text = string.Empty;
        textNumeroPerito.Text = string.Empty;

        textNumeroAvaluo.Enabled = false;
        textNumeroPerito.Enabled = false;

        textNumeroAvaluo.Text = string.Empty;
        textNumeroPerito.Text = string.Empty;
        desactivarBtnPerito();

        rfvNumeroAvaluo.Enabled = false;
        revNumeroPerito.Enabled = false;
    }

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s  a la busqueda por número
    /// único de avalúo.
    /// </summary>
    private void DesactivarIdAvaluo()
    {
        txtIdAvaluo.Enabled = false;
        txtIdAvaluo.Text = string.Empty;
        rfvIdAvaluo.Enabled = false; // añadido 2.2.8

        revIdAvaluo.Enabled = false;
    }

    /// <summary>
    /// Selecciona la opción Todos en el radiobutton vigencia.
    /// </summary>
    private void SeleccionarVigenciaTodos()
    {
        RadioButtonNoVigente.Checked = false;
        RadioButtonVigente.Checked = false;
        RadioButtonTodos.Checked = true;
    }

    /// <summary>
    /// Selecciona la opción Todos en el radiobutton vigencia.
    /// </summary>
    private void SeleccionarVigenciaVigente()
    {
        RadioButtonNoVigente.Checked = false;
        RadioButtonVigente.Checked = true;
        RadioButtonTodos.Checked = false;
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

    /// <summary>
    /// Seleccionar la opción todos en el combo de estados.
    /// </summary>
    private void SeleccionarEstadoTodos()
    {
        ddlEstado.ClearSelection();
        ddlEstado.SelectedValue = Constantes.DDLESTADO_VALUE_TODOS;
    }

    /// <summary>
    /// Seleccionar la opción enviado notario en el combo de estados.
    /// </summary>
    private void SeleccionarEstadoEnviadoNotario()
    {
        ddlEstado.ClearSelection();
        ddlEstado.SelectedValue = Constantes.DDLESTADO_VALUE_ENVIADONOTARIO;
    }

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s a cualquier opción de
    /// busqueda y selecciona estado = TODOS y vigencia = TODOS.
    /// </summary>
    private void DesactivarTodo()
    {
        //FECHAS
        DesactivarFechas();

        //Nº AVALÚO/PERITO/IDAVALUO
        DesactivarNumAvaluo();

        //IDAVALUO
        DesactivarIdAvaluo();

        //REGIÓN/MANZANA/LOTE/UNIDAD PRIVATIVA (CUENTA CATASTRAL)
        DesactivarCuentaCatastral();

        //ESTADO
        //if (!Condiciones.Web(Constantes.FUN_FINANZAS)) //Los usuairos de la secretaria de finanzas y dictaminadores solo pueden ver avalúos enviados a notario
        //{
        SeleccionarEstadoTodos();
        //}
        //else
        //    SeleccionarEstadoEnviadoNotario();

        //VIGENCIA
        SeleccionarVigenciaVigente();
    }

    /// <summary>
    /// Restricciones filtro.
    /// </summary>
    /// <param name="busquedaFiltro">El tipo filtro de búsqueda seleccionado.</param>
    protected void RestriccionesFiltro(TipoFiltroBusqueda busquedaFiltro)
    {
        try
        {
            // Vaciar y ocultar todos los campos y desactivar todas las restricciones
            DesactivarTodo();

            //En función del tipo de busqueda activar solo las restricciones asociadas al tipo de busqueda
            switch (busquedaFiltro)
            {
                case TipoFiltroBusqueda.IdAvaluo:
                    txtIdAvaluo.Enabled = true;
                    rfvIdAvaluo.Enabled = true;
                    revIdAvaluo.Enabled = true;
                    break;
                case TipoFiltroBusqueda.CuentaCatastral:
                    txtRegion.Enabled = true;
                    txtManzana.Enabled = true;
                    txtLote.Enabled = true;
                    txtUnidadPrivativa.Enabled = true;

                    txtUnidadPrivativa.Attributes.Add("onblur", "javascript:if (this.value.length!=0){document.getElementById('" + rfvLote.ClientID + "').enabled=true;rellenar(this, this.value, 3);}else {document.getElementById('" + rfvLote.ClientID + "').enabled=false;}");

                    rfvRegion.Enabled = true;
                    rfvRegionExp.Enabled = true;
                    rfvRegionExp.Visible = true;
                    rfvManzanaExp.Enabled = true;
                    rfvManzanaExp.Visible = true;
                    rfvManzana.Enabled = true;
                    rfvLoteExp.Enabled = true;
                    rfvLoteExp.Visible = true;
                    rfvUnidadPrivativaExp.Enabled = true;
                    //rfvLote.Enabled = true;
                    //rfvUnidadPrivativa.Enabled = true;
                    break;
                case TipoFiltroBusqueda.Fecha:
                    txtFechaFin.Enabled = true;
                    txtFechaIni.Enabled = true;

                    AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);

                    rfvFechaInicial.Enabled = true;
                    rfvFechaFin.Enabled = true;
                    cvFechaInicial.Enabled = true;
                    cvFechaFin.Enabled = true;
                    cvRangofechas.Enabled = true;

                    btnFechaFin.Enabled = true;
                    btnFechaIni.Enabled = true;
                    break;
                case TipoFiltroBusqueda.NumeroAvaluo:
                    textNumeroAvaluo.Enabled = true;
                    if (Condiciones.Web(Constantes.FUN_PERITO))
                    {
                        textNumeroPerito.Enabled = false;
                        btnPeritos.Visible = false;
                    }
                    else
                    {
                        textNumeroPerito.Enabled = true;
                    }

                    rfvNumeroAvaluo.Enabled = true;
                    if (Condiciones.Web(Constantes.FUN_FINANZAS) || Condiciones.Web(Constantes.FUN_DICTAMENES))
                    {
                        textNumeroPerito.Enabled = true;
                        textNumeroPerito.Visible = true;  // campo perito  debe ser visible
                        lblNumPerito.Visible = false;
                        lblNumPerSoci.Visible = true;
                        activarBtnPerito();
                    }
                    else if (Condiciones.Web(Constantes.FUN_PERITO))
                    {
                        textNumeroPerito.Visible = false;  // campo perito no debe ser visible
                        lblNumPerito.Visible = false;
                        ocultarBtnPerito();
                        textNumeroPerito.Text = Usuarios.IdPersona();
                    }
                    else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                    {
                        textNumeroPerito.Enabled = true;
                        textNumeroPerito.Visible = true;  // campo perito  debe ser visible
                        lblNumPerito.Visible = true;
                        lblNumPerSoci.Visible = false;
                        activarBtnPerito();
                    }//Fin añadido añadido 2.2.8	Búsqueda por estado (#69)
                    break;
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

    #endregion

    #region SELECCIONAR CRITERIO BUSQUEDA

    /// <summary>
    /// Seleccionar busqueda fecha.
    /// </summary>
    private void SeleccionarBusquedaFecha()
    {
        rbIdAvaluo.Checked = false;
        rbCuenta.Checked = false;
        rbNumeroAvaluo.Checked = false;
        rbFechas.Checked = true;
    }

    /// <summary>
    /// Seleccionar busqueda cuenta.
    /// </summary>
    private void SeleccionarBusquedaCuenta()
    {
        rbIdAvaluo.Checked = false;
        rbCuenta.Checked = true;
        rbNumeroAvaluo.Checked = false;
        rbFechas.Checked = false;
    }

    /// <summary>
    /// Seleccionar busqueda identificador avalúo.
    /// </summary>
    private void SeleccionarBusquedaIdAvaluo()
    {
        rbIdAvaluo.Checked = true;
        rbCuenta.Checked = false;
        rbNumeroAvaluo.Checked = false;
        rbFechas.Checked = false;
    }

    /// <summary>
    /// Seleccionar busqueda número avalúo.
    /// </summary>
    private void SeleccionarBusquedaNumAvaluo()
    {
        rbIdAvaluo.Checked = false;
        rbCuenta.Checked = false;
        rbNumeroAvaluo.Checked = true;
        rbFechas.Checked = false;
    }

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

    /// <summary>
    /// Cargar grid view.
    /// </summary>
    protected void CargarGridView()
    {
        try
        {
            if (rbNumeroAvaluo.Checked)
            {
                CargarPagina(TipoFiltroBusqueda.NumeroAvaluo);
            }
            else if (rbFechas.Checked)
            {
                CargarPagina(TipoFiltroBusqueda.Fecha);
            }
            else if (rbIdAvaluo.Checked)
            {
                CargarPagina(TipoFiltroBusqueda.IdAvaluo);
            }
            else if (rbCuenta.Checked)
            {
                CargarPagina(TipoFiltroBusqueda.CuentaCatastral);
            }
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
            //Ocultar/mostrar colunmas del gridview dependiendo del perfil del usuario
            if (Condiciones.Web(Constantes.FUN_DICTAMENES) || Condiciones.Web(Constantes.FUN_FINANZAS))
            {
                gridViewAvaluos.Columns[1].Visible = false;
                gridViewAvaluos.Columns[7].Visible = true;
                gridViewAvaluos.Columns[8].Visible = true;
            }
            else if (Condiciones.Web(Constantes.FUN_PERITO))
            {
                gridViewAvaluos.Columns[7].Visible = false;
                gridViewAvaluos.Columns[8].Visible = false;

            }
            else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
            {
                gridViewAvaluos.Columns[7].Visible = true;
                gridViewAvaluos.Columns[8].Visible = false;
            }
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox checkboxVIG = (CheckBox)e.Row.FindControl(Constantes.CHECKBOX_VIGENCIA);
                checkboxVIG.Checked = e.Row.ConvertGridViewRow<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>().IsVIGENTENull() ? false : true;
                checkboxVIG.Enabled = false;
                if (((e.Row.Cells[10].Text).Trim()).Equals(Constantes.PAR_XML_AV__COMERCIAL))
                {
                    e.Row.Cells[10].Text = Constantes.PAR_AVALUO_COMERCIAL_SHORT;
                }
                if (((e.Row.Cells[10].Text).Trim()).Equals(Constantes.PAR_XML_AV_CATASTRAL))
                {
                    e.Row.Cells[10].Text = Constantes.PAR_AVALUO_CATASTRAL_SHORT;
                }
                if (e.Row.RowIndex == 0)
                {
                    DataRowView t = (DataRowView)e.Row.DataItem;
                    lblCount.Text = String.Format(Constantes.MSJ_NUN_AVALUOS_ENCONTRADOS, t["ROWS_TOTAL"]);
                }
            }
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                lblCount.Text = string.Empty;
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
            EnabledButton(false, true);
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
            HiddenNumUnico.Value = gridViewAvaluos.SelectedDataKey[Constantes.COL_NUMUNICO].ToString().Trim();
            HiddenIdAvaluo.Value = gridViewAvaluos.SelectedDataKey[Constantes.COL_IDAVALUO].ToString().Trim();
            string numeroNotario = gridViewAvaluos.SelectedDataKey[Constantes.COL_NUMNOTARIO].ToString().Trim();
            string codEstado = gridViewAvaluos.SelectedDataKey[Constantes.COL_CODESTADOAVALUO].ToString().Trim();
            string codTipoTramite = gridViewAvaluos.SelectedDataKey[Constantes.COL_CODTIPOTRAMITE].ToString().Trim();
            //Validar si el avalúo seleccionado tiene alamacenado el xml del avalúo
            bool tieneXml = ExisteXMLAsociado(HiddenIdAvaluo.Value.ToDecimal());
            if (!tieneXml)
            {
                string msj = Constantes.MSJ_ERROR_FALTANDATOSXML;
                MostrarMensajeInfo(msj, true);
            }

            //Habilitar los botones que hacen referencia a una fila seleccionada del gridview
            HabilitarDeshabilitarBotones(codEstado, numeroNotario, tieneXml, codTipoTramite);

            //Actualizar parámetros de las URLs
            ActualizarUrlAcuse();
            ActualizarUrlInfome();
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
            gridViewAvaluos.SelectedIndex = -1;

            EnabledButton(false, true);
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
    /// Activar botón acuse recibo.
    /// </summary>
    private void activarBtnAcuseRecibo()
    {
        HlinkGenerarAcuse.Enabled = true;
        HlinkGenerarAcuse.ImageUrl = Constantes.URL_INFORME;
    }

    /// <summary>
    /// Desactivar botón acuse recibo.
    /// </summary>
    private void desactivarBtnAcuseRecibo()
    {
        HlinkGenerarAcuse.Enabled = false;
        HlinkGenerarAcuse.ImageUrl = Constantes.URL_INFORME_P;
    }

    /// <summary>
    /// Desactivar botón cambiar estado.
    /// </summary>
    private void desactivarBtnCambiarEstado()
    {
        btnCambiarEstado.Enabled = false;
        btnCambiarEstado.ImageUrl = Constantes.URL_IMG_CAMBIAR_ESTADO_P;
    }

    /// <summary>
    /// Activar botón cambiar estado.
    /// </summary>
    private void activarBtnCambiarEstado()
    {
        btnCambiarEstado.Enabled = true;
        btnCambiarEstado.ImageUrl = Constantes.URL_IMG_CAMBIAR_ESTADO;
    }

    /// <summary>
    /// Activar botón avaluos proximos.
    /// </summary>
    private void activarBtnAvaluosProximos()
    {
        btnVerAvaluosProximos.Enabled = true;
        btnVerAvaluosProximos.ImageUrl = Constantes.URL_IMG_AVALUOS_PROXIMOS;
    }

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
    /// Ocultar botón acuse recibo.
    /// </summary>
    private void ocultarBtnAcuseRecibo()
    {
        this.HlinkGenerarAcuse.Visible = false;
        this.HlinkGenerarAcuse.Enabled = false;
    }

    /// <summary>
    /// Ocultar botón perito.
    /// </summary>
    private void ocultarBtnPerito()
    {
        btnPeritos.Enabled = false;
        btnPeritos.Visible = false;
    }

    /// <summary>
    /// Desactivar botón perito.
    /// </summary>
    private void desactivarBtnPerito()
    {
        btnPeritos.Enabled = false;
        btnPeritos.ImageUrl = Constantes.URL_IMG_USER_P;
    }

    /// <summary>
    /// Desactivar botón notario.
    /// </summary>
    private void desactivarBtnNotario()
    {
        btnNotario.Enabled = false;
        btnNotario.ImageUrl = Constantes.URL_IMG_USER_P;
    }

    /// <summary>
    /// Activar botón notario.
    /// </summary>
    private void activarBtnNotario()
    {
        btnNotario.Enabled = true;
        btnNotario.ImageUrl = Constantes.URL_IMG_USER;
    }

    #endregion

    /// <summary>
    /// Enabled button.
    /// </summary>
    /// <param name="estado">Verdadero para estado.</param>
    /// <param name="limpiarSeleccion">Verdadero para limpiar selección.</param>
    protected void EnabledButton(bool estado, bool limpiarSeleccion)
    {
        try
        {
            btnCambiarEstado.Enabled = estado;
            HlinkInforme.Enabled = estado;
            btnNotario.Enabled = estado;
            btnVerAvaluosProximos.Enabled = estado;
            HlinkGenerarAcuse.Enabled = estado;
            HlinkInforme.Enabled = estado;
            if (estado)
            {
                activarBtnCambiarEstado();
                activarBtnAcuseRecibo();
                activarBtnNotario();
                activarBtnAvaluosProximos();
                HlinkInforme.ImageUrl = Constantes.URL_IMG_VER_DETALLE;
            }
            else
            {
                desactivarBtnCambiarEstado();
                desactivarBtnAcuseRecibo();
                desactivarBtnNotario();
                btnVerAvaluosProximos.ImageUrl = Constantes.URL_IMG_AVALUOS_PROXIMOS_P;
                //#24
                HlinkInforme.ImageUrl = Constantes.URL_IMG_VER_DETALLE_P;

                if (limpiarSeleccion)
                {
                    gridViewAvaluos.SelectedIndex = -1;
                }
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
    /// Habilitar deshabilitar botones.
    /// </summary>
    /// <param name="codEstado">El código estado.</param>
    /// <param name="numeroNotario">El número notario.</param>
    /// <param name="tieneXml">Verdadero si tiene XML.</param>
    /// <param name="codTipoTramite">El código tipo trámite.</param>
    private void HabilitarDeshabilitarBotones(string codEstado, string numeroNotario, bool tieneXml, string codTipoTramite)
    {
        //booleano que indica si el estado del avalúo es recibido
        bool EstadoRecibido = (codEstado == ((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Recibido).ToString());

        if (!tieneXml) //Si el avalúo No tiene xml asociado
        {
            EnabledButton(false, false);
            activarBtnAvaluosProximos();
            if (codEstado != (((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.EnviadoNotario).ToString()) && (codEstado != ((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Cancelado).ToString()))
            {
                activarBtnCambiarEstado();
            }
        }
        else //Si el avalúo Sí tiene xml asociado
        {
            if (gridViewAvaluos.SelectedIndex > -1)
            {
                EnabledButton(true, false);
            }
            else
            {
                EnabledButton(false, true);
            }

            if (!string.IsNullOrEmpty(numeroNotario) || !EstadoRecibido)
            {
                desactivarBtnNotario();
            }
            else
            {
                activarBtnNotario();
            }

            if (EstadoRecibido)
            {
                activarBtnCambiarEstado();
            }
            else
            {
                desactivarBtnCambiarEstado();
            }

            if (codEstado == ((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Cancelado).ToString())
            {
                desactivarBtnAcuseRecibo();
            }
        }
        if (codTipoTramite == (((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.TiposAvaluoEnum.Catastral).ToString()))
        {
            desactivarBtnNotario();
        }
        UpdatePanelBotonesGridView.Update();
    }

    #endregion

    #region EVENTOS

    /// <summary>
    /// Manejador de eventos. Llamado por rbBusquedaGroup para eventos checked cambio.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void rbBusquedaGroup_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (sender == rbFechas)
            {
                RestriccionesFiltro(TipoFiltroBusqueda.Fecha);
            }
            else if (sender == rbNumeroAvaluo)
            {
                RestriccionesFiltro(TipoFiltroBusqueda.NumeroAvaluo);
            }
            else if (sender == rbCuenta)
            {
                RestriccionesFiltro(TipoFiltroBusqueda.CuentaCatastral);
            }
            else if (sender == rbIdAvaluo)
            {
                RestriccionesFiltro(TipoFiltroBusqueda.IdAvaluo);
            }
            //  EnabledButton(false, true);
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

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
    /// Manejador de eventos. Llamado por btnAvaluosProximos para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnAvaluosProximos_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            RedirectUtil.BaseURL = Constantes.URL_AVALUOS_PROXIMOS;
            RedirectUtil.AddParameter(Constantes.PAR_IDAVALUO, (HiddenIdAvaluo.Value).Trim());
            RedirectUtil.AddParameter(Constantes.PAR_CUENTACAT, gridViewAvaluos.SelectedDataKey[Constantes.COL_CUENTACATASTRAL].ToString().Trim());
            RedirectUtil.AddParameter(Constantes.REQUEST_FILTRO, FBusqueda.ObtenerStringFiltro());
            RedirectUtil.AddParameter(Constantes.REQUEST_SORTEXP, SortExpression.ToString());
            RedirectUtil.AddParameter(Constantes.REQUEST_SORTDIR, SortDirectionP.ToString());
            RedirectUtil.Go();
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
    /// Manejador de eventos. Llamado por btnNotario para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnNotario_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            btnNotario_ModalPopupExtender.Show();
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
            SeleccionarBusquedaFecha();
            RestriccionesFiltro(TipoFiltroBusqueda.Fecha);
            HiddenFechaFin.Value = string.Empty;
            HiddenFechaIni.Value = string.Empty;
            gridViewAvaluos.Sort(String.Empty, SortDirection.Ascending);
            RealizarBusqueda();
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

    /// <summary>
    /// Manejador de eventos. Llamado por btnBuscar para eventos click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void btnBuscar_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            AsignarValoresBusqueda();
            RealizarBusqueda();
            ActualizarFiltroBusqueda();
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
                int codEstado = ModalEstado1.CodNuevoEstadoAvaluo;
                if (codEstado == (int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Cancelado)
                { //si está cancelado deshabilitar busqueda notario
                    desactivarBtnNotario();
                    desactivarBtnAcuseRecibo();
                }

                AvaluosClient clienteAvaluos = new AvaluosClient();

                try
                {
                    clienteAvaluos.CambiarEstadoAvaluo(Convert.ToDecimal(HiddenIdAvaluo.Value), codEstado);
                }
                finally
                {
                    clienteAvaluos.Disconnect();
                }

                CargarGridView();
                desactivarBtnCambiarEstado();
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
    /// Manejador de eventos. Llamado por buscarNotario para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void buscarNotario_ConfirmClick(object sender, CancelEventArgs e)
    {
        try
        {
            if (!e.Cancel)
            {
                btnNotario_ModalPopupExtender.Show();
            }
            else
            {
                btnNotario_ModalPopupExtender.Hide();
                if (ModalBuscarNotarios1.Seleccionado)
                {
                    DseAvaluoConsulta dsAvaluo = new DseAvaluoConsulta();
                    AvaluosClient clienteAvaluos = new AvaluosClient();

                    try
                    {
                        clienteAvaluos.AsignarNotarioAvaluo(Convert.ToDecimal((HiddenIdAvaluo.Value).Trim()), ModalBuscarNotarios1.NumeroIdentificadorNotario);
                        dsAvaluo = clienteAvaluos.ObtenerAvaluo(Convert.ToInt32((HiddenIdAvaluo.Value).Trim()));
                    }
                    finally
                    {
                        clienteAvaluos.Disconnect();
                    }

                    #region Envío Correo

                    bool hayError = true;
                    string mensajeError = string.Empty;
                    if (!dsAvaluo.FEXAVA_AVALUO_V[0].IsNUMERO_NOTARIONull())
                    {
                        string tipoPersona = string.Empty;
                        string valor = string.Empty;
                        string cuentaCatastral = string.Empty;
                        if (!dsAvaluo.FEXAVA_AVALUO_V[0].IsCUENTACATASTRALNull())
                        {
                            cuentaCatastral = dsAvaluo.FEXAVA_AVALUO_V[0].CUENTACATASTRAL.ToString();
                        }

                        if (dsAvaluo.FEXAVA_AVALUO_V[0].IsVALORCATASTRALNull())
                        {
                            valor = Constantes.PAR_XML_AV__COMERCIAL;
                        }
                        else
                        {
                            valor = Constantes.PAR_XML_AV_CATASTRAL;
                        }

                        string registro = string.Empty;
                        if (Condiciones.Web(Constantes.FUN_PERITO))
                        {
                            registro = dsAvaluo.FEXAVA_AVALUO_V[0].REGISTRO_PERITO.ToString();
                            if (!dsAvaluo.FEXAVA_AVALUO_V[0].IsNOMBRE_PERITONull())
                                tipoPersona = "El perito " + dsAvaluo.FEXAVA_AVALUO_V[0].NOMBRE_PERITO.ToString();
                        }
                        else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                        {
                            if (!dsAvaluo.FEXAVA_AVALUO_V[0].IsREGISTRO_SOCIEDADNull())
                            {
                                registro = dsAvaluo.FEXAVA_AVALUO_V[0].REGISTRO_SOCIEDAD.ToString();
                            }
                            else
                            {
                                registro = dsAvaluo.FEXAVA_AVALUO_V[0].REGISTRO_PERITO.ToString();
                            }

                            if (!dsAvaluo.FEXAVA_AVALUO_V[0].IsNOMBRE_SOCIEDADNull())
                            {
                                tipoPersona = "La sociedad '" + dsAvaluo.FEXAVA_AVALUO_V[0].NOMBRE_SOCIEDAD.ToString() + "' cuyo perito es " + dsAvaluo.FEXAVA_AVALUO_V[0].NOMBRE_PERITO.ToString();
                            }
                            else
                            {
                                tipoPersona = "La sociedad cuyo perito es " + dsAvaluo.FEXAVA_AVALUO_V[0].NOMBRE_PERITO.ToString();
                            }
                        }

                        DseInfoNotarios dsNotarios = null;
                        RegistroContribuyentesClient clienteRCON = new RegistroContribuyentesClient();

                        try
                        {
                            dsNotarios = clienteRCON.GetInfoNotario(dsAvaluo.FEXAVA_AVALUO_V[0].IDPERSONANOTARIO, false);
                        }
                        finally
                        {
                            clienteRCON.Disconnect();
                        }

                        if (dsNotarios.Notario.Any())
                        {
                            string nombre = string.Empty;
                            string numAva = string.Empty;
                            string email = string.Empty;

                            if (!dsNotarios.Notario[0].IsNOMBRENull())
                            {
                                nombre = dsNotarios.Notario[0].NOMBRE + Constantes.ESPACIO_BLANCO;
                            }

                            if (!dsNotarios.Notario[0].IsAPELLIDOPATERNONull())
                            {
                                nombre += dsNotarios.Notario[0].APELLIDOPATERNO + Constantes.ESPACIO_BLANCO;
                            }

                            if (!dsNotarios.Notario[0].IsAPELLIDOMATERNONull())
                            {
                                nombre += dsNotarios.Notario[0].APELLIDOMATERNO;
                            }

                            if (!dsNotarios.Notario[0].IsEMAILNull())
                            {
                                email = dsNotarios.Notario[0].EMAIL;
                            }

                            numAva = dsAvaluo.FEXAVA_AVALUO_V[0].NUMEROAVALUO;

                            if (email != string.Empty)
                            {
                                ListDictionary lstReemplazos = new ListDictionary();
                                lstReemplazos.Add(Constantes.PAR_ENVNOT_NOMBRECOMPLETO, nombre);
                                lstReemplazos.Add(Constantes.PAR_ENVNOT_TIPOPERSONA, tipoPersona);
                                lstReemplazos.Add(Constantes.PAR_ENVNOT_REGISTRO, registro);
                                lstReemplazos.Add(Constantes.PAR_ENVNOT_VALOR, valor);
                                lstReemplazos.Add(Constantes.PAR_ENVNOT_CUENTACAT, cuentaCatastral);
                                lstReemplazos.Add(Constantes.PAR_ENVNOT_FECHA, System.DateTime.Now.ToString());
                                string rutaTemplate = ConfigurationSettings.AppSettings[Constantes.PAR_ENVNOT_RUTA_TEMPLATE] + Constantes.PAR_ENVNOT_FICHERO_TEMPLATE;

                                SIGAPred.Common.Email.EmailUtils.DatosEmail datos = new SIGAPred.Common.Email.EmailUtils.DatosEmail();
                                datos.From = ConfigurationSettings.AppSettings[Constantes.PAR_ENVNOT_EMAIL_ORIGEN];
                                datos.To = email;
                                datos.CC = string.Empty;
                                datos.Subject = Constantes.PAR_ENVNOT_SUBJECT + numAva;
                                if (SIGAPred.Common.Email.EmailUtils.SendEmail(rutaTemplate, lstReemplazos, datos, out mensajeError))
                                {
                                    hayError = false;
                                    string msj = Constantes.PAR_ENVNOT_CONFIRMACION_ENVIO;
                                    MostrarMensajeInfo(msj, true);
                                }

                                datos.Subject = String.Format("MAIL INFO: {0}", datos.Subject);
                                string[] mails = ConfigurationSettings.AppSettings["MailsNotificacionesFinanzas"].ToString().Split(',');
                                foreach (string mail in mails)
                                {
                                    datos.To = mail;
                                    if (SIGAPred.Common.Email.EmailUtils.SendEmail(rutaTemplate, lstReemplazos, datos, out mensajeError))
                                    {
                                        hayError = false;
                                    }    
                                }

                            }
                            else
                            {
                                mensajeError = Constantes.PAR_ENVNOT_ERROR_NOEMAIL;
                            }
                        }
                    }
                    else
                    {
                        hayError = false;
                    }
                    if (hayError)
                    {
                        string msj = string.Empty;
                        if (!string.IsNullOrEmpty(mensajeError))
                        {
                            msj = Constantes.PAR_ENVNOT_ERROR_OTROS + mensajeError;
                        }
                        else
                        {
                            msj = Constantes.PAR_ENVNOT_ERROR_DESCONOCIDO;
                        }
                        MostrarMensajeInfo(msj, true);
                    }

                    #endregion

                    CargarGridView();
                    desactivarBtnNotario();
                    desactivarBtnCambiarEstado();
                    desactivarBtnAcuseRecibo();
                }
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
                        textNumeroPerito.Text = ModalBuscarPeritos1.NumeroRegistro.ToString().Trim();
                    }
                    HiddenTipoRegistro.Value = ModalBuscarPeritos1.TipoRegistro.ToString().Trim();
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

    /// <summary>
    /// Cargar campos filtro.
    /// </summary>
    protected void CargarCamposFiltro()
    {
        try
        {
            CargarComboCodEstadosAvaluo();
            if (FBusqueda.EsFecha())
            {
                RestriccionesFiltro(TipoFiltroBusqueda.Fecha);
            }
            else if (FBusqueda.EsCuenta())
            {
                RestriccionesFiltro(TipoFiltroBusqueda.CuentaCatastral);
            }
            else if (FBusqueda.EsIdavaluo())
            {
                RestriccionesFiltro(TipoFiltroBusqueda.IdAvaluo);
            }
            else if (FBusqueda.EsNumAvaluo())
            {
                RestriccionesFiltro(TipoFiltroBusqueda.NumeroAvaluo);
            }

            rbFechas.Checked = FBusqueda.EsFecha();
            rbCuenta.Checked = FBusqueda.EsCuenta();
            rbIdAvaluo.Checked = FBusqueda.EsIdavaluo();
            rbNumeroAvaluo.Checked = FBusqueda.EsNumAvaluo();

            switch (FBusqueda.Vigencia)
            {
                case Constantes.PAR_VIGENCIA_TODOS:
                    RadioButtonTodos.Checked = true;
                    RadioButtonVigente.Checked = false;
                    RadioButtonNoVigente.Checked = false;
                    break;
                case Constantes.PAR_VIGENTE:
                    RadioButtonTodos.Checked = false;
                    RadioButtonVigente.Checked = true;
                    RadioButtonNoVigente.Checked = false;
                    break;
                case Constantes.PAR_NO_VIGENTE:
                    RadioButtonTodos.Checked = false;
                    RadioButtonVigente.Checked = false;
                    RadioButtonNoVigente.Checked = true;
                    break;
            }

            txtRegion.Text = FBusqueda.Region;
            txtLote.Text = FBusqueda.Lote;
            txtManzana.Text = FBusqueda.Manazana;
            txtUnidadPrivativa.Text = FBusqueda.UnidadPrivativa;
            txtFechaIni.Text = FBusqueda.FechaIni;
            txtFechaFin.Text = FBusqueda.FechaFin;
            txtIdAvaluo.Text = FBusqueda.Idavaluo;
            textNumeroAvaluo.Text = FBusqueda.NumAvaluo;
            ddlEstado.SelectedValue = FBusqueda.CodEstado;
            AsignarValoresBusqueda();
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
