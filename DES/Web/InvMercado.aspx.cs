using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
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
            if (!IsPostBack)
            {

                CargarCombos();
                AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
                txtRegion.Focus();

            }
            else
            {
                //_busquedaActual = (TipoFiltroBusqueda)(System.Convert.ToInt32(hidBusquedaActual.Value));
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
    protected void CargarCombos()
    {
        CargarComboTipo();
        CargarComboDelegaciones();
        CargarComboColonia(-1);
    }


    protected void CargarComboTipo()
    {
        ddlTipo.Items.Clear();
        CatalogoTipos tipos = new CatalogoTipos();
        foreach (var tipo in tipos.Tipos)
        {
            ddlTipo.Items.Add(new System.Web.UI.WebControls.ListItem(tipo.descripcion, tipo.clave));
        }
    }


    protected void CargarComboDelegaciones()
    {
        ServiceCatastral.ConsultaCatastralServiceClient clienteCas = new ServiceCatastral.ConsultaCatastralServiceClient();
        try
        {
            if (! (ddlDelegacion.Items.Count > 0) )
            {
                ServiceCatastral.DseDelegacionColonia.DelegacionDataTable delegaciones = clienteCas.GetDelegaciones();
                foreach (var del in delegaciones)
                {
                    ddlDelegacion.Items.Add(new System.Web.UI.WebControls.ListItem(del.NOMBRE, del.IDDELEGACION.ToString()));
                }
                CargarOpcionDefault(ddlDelegacion, "-Seleccione una delegación-");
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
        finally
        {
            clienteCas.Close();
        }
    }

    protected void ddlDelegacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CargarComboColonia(Convert.ToDecimal(ddlDelegacion.SelectedValue.ToString()));
        
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

    protected void CargarComboColonia(decimal idDelegacion)
    {
        ddlColonia.Items.Clear();
        ServiceRCON.RegistroContribuyentesClient cliente = new RegistroContribuyentesClient();
        try
        {
            if (idDelegacion != -1)
            {
                DseColoniaAsentamiento dsecol = cliente.GetColAsentByDelegacion(idDelegacion, string.Empty);
                foreach (DataRow row in dsecol.Tables[0].Rows)
                {
                    if (!row["IDDELEGACION"].ToString().Equals("-1"))
                    {
                        ddlColonia.Items.Add(new System.Web.UI.WebControls.ListItem(row["DESCRIPCION"].ToString(), row["CODIGO"].ToString()));
                    }
                }
                ddlColonia.Enabled = true;
            }
            else
            {
                ddlColonia.Enabled = false;
            }
            CargarOpcionDefault(ddlColonia, "-Seleccione una colonia-");
            
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
        finally
        {
            cliente.Close();
        }
    }

    protected void CargarOpcionDefault(DropDownList ddl, string Mensaje)
    {
        ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(Mensaje, "-1"));
        ddl.SelectedValue = "-1";
    }
    #endregion

    #region OTROS METODOS

    /// <summary>
    /// Busqueda inicial.
    /// </summary>
    private void BusquedaInicial()
    {
        
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
        
    }

    /// <summary>
    /// Inicializar controles.
    /// </summary>
    private void InicializarControles()
    {
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
        return true;
    }

    /// <summary>
    /// Actualizar URL infome.
    /// </summary>
    private void ActualizarUrlInfome()
    {
       
    }

    /// <summary>
    /// Actualizar URL acuse.
    /// </summary>
    private void ActualizarUrlAcuse()
    {
        
    }

    /// <summary>
    /// Actualizar filtro busqueda.
    /// </summary>
    private void ActualizarFiltroBusqueda()
    {
       
    }

    /// <summary>
    /// Asignar valores busqueda.
    /// </summary>
    private void AsignarValoresBusqueda()
    {
       
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
       
    }

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s  a la busqueda por
    /// numAvaluo.
    /// </summary>
    private void DesactivarNumAvaluo()
    {
        
    }

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s  a la busqueda por número
    /// único de avalúo.
    /// </summary>
    private void DesactivarIdAvaluo()
    {
       
    }

    /// <summary>
    /// Selecciona la opción Todos en el radiobutton vigencia.
    /// </summary>
    private void SeleccionarVigenciaTodos()
    {
       
    }

    /// <summary>
    /// Selecciona la opción Todos en el radiobutton vigencia.
    /// </summary>
    private void SeleccionarVigenciaVigente()
    {
       
    }

    /// <summary>
    /// Oculta, vacia y desactiva todos los campos y validaciones asociad@s a la busqueda por cuenta
    /// catastral.
    /// </summary>
    private void DesactivarCuentaCatastral()
    {
       
    }

    /// <summary>
    /// Seleccionar la opción todos en el combo de estados.
    /// </summary>
    private void SeleccionarEstadoTodos()
    {
      
    }

    /// <summary>
    /// Seleccionar la opción enviado notario en el combo de estados.
    /// </summary>
    private void SeleccionarEstadoEnviadoNotario()
    {
       
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
       
    }

    #endregion

    #region SELECCIONAR CRITERIO BUSQUEDA

    /// <summary>
    /// Seleccionar busqueda fecha.
    /// </summary>
    private void SeleccionarBusquedaFecha()
    {
       
    }

    /// <summary>
    /// Seleccionar busqueda cuenta.
    /// </summary>
    private void SeleccionarBusquedaCuenta()
    {
       
    }

    /// <summary>
    /// Seleccionar busqueda identificador avalúo.
    /// </summary>
    private void SeleccionarBusquedaIdAvaluo()
    {
       
    }

    /// <summary>
    /// Seleccionar busqueda número avalúo.
    /// </summary>
    private void SeleccionarBusquedaNumAvaluo()
    {
        
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
       
    }

    /// <summary>
    /// Asigna el criterio de ordenación al gridview avalúo (SortExpression + SortDirection)
    /// </summary>
    protected void CargarSort()
    {
        //try
        //{
        //    SortDirection direction;
        //    SortExpression = Request[Constantes.REQUEST_SORTEXP];
        //    SortDirectionP = Request[Constantes.REQUEST_SORTDIR];
        //    switch (SortDirectionP)
        //    {
        //        case Constantes.SORTDIRECTION_ASCENDING:
        //            direction = SortDirection.Ascending;
        //            break;
        //        case Constantes.SORTDIRECTION_DESCENDING:
        //            direction = SortDirection.Descending;
        //            break;
        //        default:
        //            direction = SortDirection.Ascending;
        //            break;
        //    }
        //    gridViewAvaluos.Sort(SortExpression, direction);
        //}
        //catch (Exception ex)
        //{
        //    ExceptionPolicyWrapper.HandleException(ex);
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
    }

    /// <summary>
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos row data bound.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //try
        //{
            //Ocultar/mostrar colunmas del gridview dependiendo del perfil del usuario
        //    if (Condiciones.Web(Constantes.FUN_DICTAMENES) || Condiciones.Web(Constantes.FUN_FINANZAS))
        //    {
        //        gridViewAvaluos.Columns[1].Visible = false;
        //        gridViewAvaluos.Columns[7].Visible = true;
        //        gridViewAvaluos.Columns[8].Visible = true;
        //    }
        //    else if (Condiciones.Web(Constantes.FUN_PERITO))
        //    {
        //        gridViewAvaluos.Columns[7].Visible = false;
        //        gridViewAvaluos.Columns[8].Visible = false;

        //    }
        //    else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
        //    {
        //        gridViewAvaluos.Columns[7].Visible = true;
        //        gridViewAvaluos.Columns[8].Visible = false;
        //    }
            

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        CheckBox checkboxVIG = (CheckBox)e.Row.FindControl(Constantes.CHECKBOX_VIGENCIA);
        //        checkboxVIG.Checked = e.Row.ConvertGridViewRow<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>().IsVIGENTENull() ? false : true;
        //        checkboxVIG.Enabled = false;
        //        if (((e.Row.Cells[10].Text).Trim()).Equals(Constantes.PAR_XML_AV__COMERCIAL))
        //        {
        //            e.Row.Cells[10].Text = Constantes.PAR_AVALUO_COMERCIAL_SHORT;
        //        }
        //        if (((e.Row.Cells[10].Text).Trim()).Equals(Constantes.PAR_XML_AV_CATASTRAL))
        //        {
        //            e.Row.Cells[10].Text = Constantes.PAR_AVALUO_CATASTRAL_SHORT;
        //        }
        //        if (e.Row.RowIndex == 0)
        //        {
        //            DataRowView t = (DataRowView)e.Row.DataItem;
        //            lblCount.Text = String.Format(Constantes.MSJ_NUN_AVALUOS_ENCONTRADOS, t["ROWS_TOTAL"]);
        //        }
        //    }
        //    if (e.Row.RowType == DataControlRowType.EmptyDataRow)
        //    {
        //        lblCount.Text = string.Empty;
        //    }
        //}
        //catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        //{
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
        //catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
        //{
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
        //catch (Exception ex)
        //{
        //    ExceptionPolicyWrapper.HandleException(ex);
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
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
            //SortExpression = e.SortExpression;
            //SortDirectionP = e.SortDirection.ToString();
            //EnabledButton(false, true);
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
        //try
        //{
           
        //    string numeroNotario = gridViewAvaluos.SelectedDataKey[Constantes.COL_NUMNOTARIO].ToString().Trim();
        //    string codEstado = gridViewAvaluos.SelectedDataKey[Constantes.COL_CODESTADOAVALUO].ToString().Trim();
        //    string codTipoTramite = gridViewAvaluos.SelectedDataKey[Constantes.COL_CODTIPOTRAMITE].ToString().Trim();
           

        //    ActualizarUrlAcuse();
        //    ActualizarUrlInfome();
        //}
        //catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        //{
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
        //catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
        //{
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
        //catch (Exception ex)
        //{
        //    ExceptionPolicyWrapper.HandleException(ex);
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
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
            gridViewAvaluos.PageIndex = e.NewPageIndex;
            LoadGrid();
            
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
        //for (int i = 0; i < gridViewAvaluos.Rows.Count; i++)
        //{
        //    if (((gridViewAvaluos.Rows[i].Cells[9].Text).Trim()).Equals(Constantes.PAR_XML_AV__COMERCIAL))
        //    {
        //        gridViewAvaluos.Rows[i].Cells[9].Text = Constantes.PAR_AVALUO_COMERCIAL_SHORT;
        //    }
        //    if (((gridViewAvaluos.Rows[i].Cells[9].Text).Trim()).Equals(Constantes.PAR_XML_AV_CATASTRAL))
        //    {
        //        gridViewAvaluos.Rows[i].Cells[9].Text = Constantes.PAR_AVALUO_CATASTRAL_SHORT;
        //    }
        //}
    }

    /// <summary>
    /// Manejador de eventos. Llamado por gridViewAvaluos para eventos pre render.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void gridViewAvaluos_PreRender(object sender, EventArgs e)
    {
        //try
        //{
        //    gridViewAvaluos_formatearColTipo();
        //}
        //catch (Exception ex)
        //{
        //    ExceptionPolicyWrapper.HandleException(ex);
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
    }

    #endregion

    #region BOTONES

    //#region Activar/Desactivar/Ocultar botones

    ///// <summary>
    ///// Activar botón acuse recibo.
    ///// </summary>
    //private void activarBtnAcuseRecibo()
    //{
    //    HlinkGenerarAcuse.Enabled = true;
    //    HlinkGenerarAcuse.ImageUrl = Constantes.URL_INFORME;
    //}

    ///// <summary>
    ///// Desactivar botón acuse recibo.
    ///// </summary>
    //private void desactivarBtnAcuseRecibo()
    //{
    //    HlinkGenerarAcuse.Enabled = false;
    //    HlinkGenerarAcuse.ImageUrl = Constantes.URL_INFORME_P;
    //}

    ///// <summary>
    ///// Desactivar botón cambiar estado.
    ///// </summary>
    //private void desactivarBtnCambiarEstado()
    //{
    //    btnCambiarEstado.Enabled = false;
    //    btnCambiarEstado.ImageUrl = Constantes.URL_IMG_CAMBIAR_ESTADO_P;
    //}

    ///// <summary>
    ///// Activar botón cambiar estado.
    ///// </summary>
    //private void activarBtnCambiarEstado()
    //{
    //    btnCambiarEstado.Enabled = true;
    //    btnCambiarEstado.ImageUrl = Constantes.URL_IMG_CAMBIAR_ESTADO;
    //}

    ///// <summary>
    ///// Activar botón avaluos proximos.
    ///// </summary>
    //private void activarBtnAvaluosProximos()
    //{
    //    btnVerAvaluosProximos.Enabled = true;
    //    btnVerAvaluosProximos.ImageUrl = Constantes.URL_IMG_AVALUOS_PROXIMOS;
    //}

    /// <summary>
    /// Activar botón perito.
    /// </summary>
    private void activarBtnPerito()
    {
       
    }

    /// <summary>
    /// Ocultar botón acuse recibo.
    /// </summary>
    private void ocultarBtnAcuseRecibo()
    {
        
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

    /// <summary>
    /// Desactivar botón notario.
    /// </summary>
    private void desactivarBtnNotario()
    {
        //btnNotario.Enabled = false;
        //btnNotario.ImageUrl = Constantes.URL_IMG_USER_P;
    }

    /// <summary>
    /// Activar botón notario.
    /// </summary>
    private void activarBtnNotario()
    {
        //btnNotario.Enabled = true;
        //btnNotario.ImageUrl = Constantes.URL_IMG_USER;
    }

    #endregion

    /// <summary>
    /// Enabled button.
    /// </summary>
    /// <param name="estado">Verdadero para estado.</param>
    /// <param name="limpiarSeleccion">Verdadero para limpiar selección.</param>
    protected void EnabledButton(bool estado, bool limpiarSeleccion)
    {
        //try
        //{
        //    btnCambiarEstado.Enabled = estado;
        //    HlinkInforme.Enabled = estado;
        //    btnNotario.Enabled = estado;
        //    btnVerAvaluosProximos.Enabled = estado;
        //    HlinkGenerarAcuse.Enabled = estado;
        //    HlinkInforme.Enabled = estado;
        //    if (estado)
        //    {
        //        activarBtnCambiarEstado();
        //        activarBtnAcuseRecibo();
        //        activarBtnNotario();
        //        activarBtnAvaluosProximos();
        //        HlinkInforme.ImageUrl = Constantes.URL_IMG_VER_DETALLE;
        //    }
        //    else
        //    {
        //        desactivarBtnCambiarEstado();
        //        desactivarBtnAcuseRecibo();
        //        desactivarBtnNotario();
        //        btnVerAvaluosProximos.ImageUrl = Constantes.URL_IMG_AVALUOS_PROXIMOS_P;
        //        //#24
        //        HlinkInforme.ImageUrl = Constantes.URL_IMG_VER_DETALLE_P;

        //        if (limpiarSeleccion)
        //        {
        //            gridViewAvaluos.SelectedIndex = -1;
        //        }
        //    }
        //}
        //catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        //{
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
        //catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
        //{
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
        //catch (Exception ex)
        //{
        //    ExceptionPolicyWrapper.HandleException(ex);
        //    string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
        //    MostrarMensajeInfoExcepcion(msj);
        //}
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
        ////booleano que indica si el estado del avalúo es recibido
        //bool EstadoRecibido = (codEstado == ((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Recibido).ToString());

        //if (!tieneXml) //Si el avalúo No tiene xml asociado
        //{
        //    EnabledButton(false, false);
        //    activarBtnAvaluosProximos();
        //    if (codEstado != (((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.EnviadoNotario).ToString()) && (codEstado != ((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Cancelado).ToString()))
        //    {
        //        activarBtnCambiarEstado();
        //    }
        //}
        //else //Si el avalúo Sí tiene xml asociado
        //{
        //    if (gridViewAvaluos.SelectedIndex > -1)
        //    {
        //        EnabledButton(true, false);
        //    }
        //    else
        //    {
        //        EnabledButton(false, true);
        //    }

        //    if (!string.IsNullOrEmpty(numeroNotario) || !EstadoRecibido)
        //    {
        //        desactivarBtnNotario();
        //    }
        //    else
        //    {
        //        activarBtnNotario();
        //    }

        //    if (EstadoRecibido)
        //    {
        //        activarBtnCambiarEstado();
        //    }
        //    else
        //    {
        //        desactivarBtnCambiarEstado();
        //    }

        //    if (codEstado == ((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.EstadosAvaluoEnum.Cancelado).ToString())
        //    {
        //        desactivarBtnAcuseRecibo();
        //    }
        //}
        //if (codTipoTramite == (((int)SIGAPred.FuentesExternas.Avaluos.Services.Enum.TiposAvaluoEnum.Catastral).ToString()))
        //{
        //    desactivarBtnNotario();
        //}
        //UpdatePanelBotonesGridView.Update();
    }

    //#endregion

    #region EVENTOS

    /// <summary>
    /// Manejador de eventos. Llamado por rbBusquedaGroup para eventos checked cambio.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void rbBusquedaGroup_CheckedChanged(object sender, EventArgs e)
    {
       
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
            //Response.Redirect("~/InvMercado.aspx");
            CargarCombos();
            txtRegion.Text = string.Empty;
            txtManzana.Text = string.Empty;
            txtCuenta.Text = string.Empty;
            btnWord.Visible = false;
            ddlDelegacion.SelectedValue = "-1";
            LoadGrid();
            AsignarFechasPorDefecto(txtFechaIni, txtFechaFin);
            txtRegion.Focus();
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
        LoadGrid();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }

    protected void btnWord_Click(object sender, ImageClickEventArgs e)
    {
        //HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.ClearHeaders();
        //HttpContext.Current.Response.ContentType = "application/pdf";
        //HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //LoadGrid();
        //gridViewAvaluos.AllowPaging = false;
        //gridViewAvaluos.DataBind();
        //gridViewAvaluos.RenderControl(hw);
        //StringReader sr = new StringReader(sw.ToString());
        //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //pdfDoc.Open();
        //htmlparser.Parse(sr);
        //pdfDoc.Close();
        //HttpContext.Current.Response.Write(pdfDoc);
        //Response.Flush();
        //Response.End();  

        Response.Redirect("~/InformeDatosCatastrales.aspx");
    }
    private void LoadGrid()
    {
         ServiceAvaluos.AvaluosClient cliente = new AvaluosClient();
        try
        {
            decimal idDelegacion = decimal.Parse(ddlDelegacion.SelectedValue);

            decimal idColonia = decimal.Parse(ddlColonia.SelectedValue);
                
            DataSet ds = cliente.GetInvestigacionMercado(
                txtRegion.Text,
                txtManzana.Text,
                ddlTipo.SelectedValue,
                idDelegacion,
                idColonia,
                txtFechaIni.Text,
                txtFechaFin.Text
                );

            if (ds.Tables.Count>0)
            {
                    SessionVariables.DsReporte = ds;
                    gridViewAvaluos.DataSource = ds;
                    gridViewAvaluos.DataBind();
                    btnWord.Visible = gridViewAvaluos.Rows.Count > 0;
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
        finally
        {
            cliente.Close();
        }
    }

    /// <summary>
    /// Manejador de eventos. Llamado por modalEstado para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void modalEstado_ConfirmClick(object sender, CancelEventArgs e)
    {
       
    }

    /// <summary>
    /// Manejador de eventos. Llamado por buscarNotario para eventos confirm click.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void buscarNotario_ConfirmClick(object sender, CancelEventArgs e)
    {
       
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
       
    }

    #endregion


    #region FILTRO

    /// <summary>
    /// Cargar campos filtro.
    /// </summary>
    protected void CargarCamposFiltro()
    {
       
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
