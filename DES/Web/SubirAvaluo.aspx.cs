using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using ServiceAvaluosNuevos;
using ServiceAvaluosAnterior;
using SIGAPred.Common.Extensions;
using SIGAPred.Common.Seguridad;
using SIGAPred.Common.WCF;

/// <summary>
/// Formulario para subir avalúos.
/// </summary>
public partial class SubirAvaluo : PageBaseAvaluos
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
            //La opción de avalúo especial solamente debe estar disponible para los usuarios del personal técnico
            if (!(Condiciones.Web(Constantes.FUN_FINANZAS)))
            {
                MostrarRbAvEspecial(false);
                MostrarRbAvNormal(true);
            }
            if (Condiciones.Web(Constantes.FUN_FINANZAS)) //Los usuarios de la secretaria de finanzas solo pueden subir avalúos especiales
            {
                MostrarRbAvEspecial(true);
                MostrarRbAvNormal(false);
            }
            if (!IsPostBack)
            {
                UpdatePanelSubir.Update();
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "SubirAvaluo", "function ShowWait() " +
            "{" +
                "if ($get('" + fileAvaluoXML.ClientID + "').value.length > 0)" +
                "{" +
                "    $get('" + UpdateProgressSubir.ClientID + "').style.display = 'block';" +
                "}" +
            "}", true);
        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInformativo(Constantes.MSJ_TOKEN_EXCEPTION + ex.Message, true);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion + Environment.NewLine + cex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosInfoException> ciex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion + Environment.NewLine + ciex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
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
            mpeErrorTareas.CancelControlID = errorTareas.ClientIdCancelacion;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }
    #endregion

    #region MOSTRAR MENSAJES
    /// <summary>
    /// Mostrar mensaje de error asociado a una excepcion.
    /// </summary>
    /// <param name="mensaje">El mensaje que se quiere mostrar.</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString() + " MostrarMensajeInfoExcepcion : Exception: " + mensaje + "\n\r");

        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }

    /// <summary>
    /// Mostrar modal error.
    /// </summary>
    /// <param name="info">La información que se quiere mostrar.</param>
    /// <param name="tipoMensaje">Verdadero para tipo mensaje.</param>
    private void MostrarModalError(string info, bool tipoMensaje)
    {
        ModalErrorInfo.TextoInformacion = info;
        ModalErrorInfo.TipoMensaje = tipoMensaje;
        ModalPopupExtenderError.Show();
    }

    /// <summary>
    /// Mostrar mensaje informativo que indica que un avalúo ha sido registrado con exito.
    /// </summary>
    private void MostrarMensajeAvRegistrado()
    {
        string numUnico = ViewState[Constantes.PAR_VIEWSTATE_NUMUNICOREGISTRADO].ToString();
        lblTextoInformacion.Text = Constantes.MSJ_SUBIRAVALUO_NORMAL_REGISTRADOCORRECTAMENTE + Constantes.MSJ_NUM_UNICO + numUnico;
        btnGuardar_ModalPopupExtenderRegistrado.Show();
        uppMensajeExito.Update();

    }

    /// <summary>
    /// Mostrar mensaje informativo.
    /// </summary>
    /// <param name="info">La información que se quiere mostrar.</param>
    /// <param name="tipoMensaje">Verdadero para tipo mensaje.</param>
    private void MostrarMensajeInformativo(string info, bool tipoMensaje)
    {
        ModalInfoToken.TextoInformacion = info;
        ModalInfoToken.TipoMensaje = tipoMensaje;
        extenderPnlInfoTokenModal.Show();
    }

    #endregion

    #region VIEWSTATE
    /// <summary>
    /// Limpiar el view state que contiene el documento XML.
    /// </summary>
    private void LimpiarViewStateDocumentoXML()
    {
        ViewState[Constantes.PAR_VIEWSTATE_DOCXML] = null;
    }

    /// <summary>
    /// Guardar el view state que contiene el documento XML.
    /// </summary>
    /// <param name="documentoXML">El documento XML a guardar.</param>
    private void GuardarViewstateDocumentoXML(byte[] documentoXML)
    {
        ViewState.Add(Constantes.PAR_VIEWSTATE_DOCXML, documentoXML);
    }

    /// <summary>
    /// Obtiene el/la recuperar viewstate documento XML.
    /// </summary>
    /// <returns>
    /// Contenido del documento xml almacenado en el viewstate
    /// </returns>
    private byte[] RecuperarViewstateDocumentoXML()
    {
        byte[] documentoXML = (byte[])ViewState[Constantes.PAR_VIEWSTATE_DOCXML];
        return documentoXML;
    }
    #endregion

    #region ACCESO DATOS XML
    /// <summary>
    /// Realiza una busqueda de elementos a partir de un elemento raiz.
    /// </summary>
    /// <param name="root">Elemento en el que se realizara la busqueda.</param>
    /// <param name="id">Valor del atributo ID por el que se raliza la busqueda.</param>
    /// <returns>
    /// Los elementos encontrados con ese id.
    /// </returns>
    private IEnumerable<XElement> XmlSearchById(XElement root, string id)
    {
        IEnumerable<XElement> queryConsulta = from campo in root.Descendants()
                                              where (string)campo.Attribute(Constantes.XML_IDENTIFICADOR_ELEMENTOS) == id
                                              select campo;
        return queryConsulta;
    }

    /// <summary>
    /// Realiza una busqueda de elementos a partir de varios elementos.
    /// </summary>
    /// <param name="rootN">Elementos en los que se realizara la busqueda.</param>
    /// <param name="id">Valor del atributo ID por el que se raliza la busqueda.</param>
    /// <returns>
    /// Los elementos encontrados con ese id.
    /// </returns>
    private IEnumerable<XElement> XmlSearchById(IEnumerable<XElement> rootN, string id)
    {
        IEnumerable<XElement> queryConsulta = from campo in rootN.Descendants()
                                              where (string)campo.Attribute(Constantes.XML_IDENTIFICADOR_ELEMENTOS) == id
                                              select campo;
        return queryConsulta;
    }
    #endregion

    #region EVENTOS

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable erroresValidacion = null;
            byte[] documentoXMLComprimido = null;
            byte[] documentoXML = null;
            string error = string.Empty;

            if (fileAvaluoXML.HasFile)
            {
                int tamanioFichero = fileAvaluoXML.FileBytes.Length;
                bool validadoTamanioFichero = true;

                ServiceAvaluosNuevos.AvaluosClient clienteAvaluosTamanio = new ServiceAvaluosNuevos.AvaluosClient();

                try
                {
                    validadoTamanioFichero = clienteAvaluosTamanio.ValidarTamanioFichero(tamanioFichero);
                }
                finally
                {
                    clienteAvaluosTamanio.Disconnect();
                }

                if (validadoTamanioFichero)
                {
                    bool esXMLValido = false;
                    bool errorAlDescomprimir = false;
                    XmlDocument xmlAvaluo = new XmlDocument();

                    if (fileAvaluoXML.FileName.EndsWith(Constantes.XML_FILE_EXTENSION))
                    {
                    documentoXML = fileAvaluoXML.FileBytes;
                    }
                    else
                    {
                        try
                        {
                            documentoXML = SIGAPred.Common.Compresion.Compresion.DescomprimirCualquierFormato(fileAvaluoXML.FileBytes);
                        }
                        catch (Exception)
                        {
                            errorAlDescomprimir = true;
                        }
                    }

                    if (!errorAlDescomprimir)
                    {
                        try
                        {
                            xmlAvaluo.Load(new MemoryStream(documentoXML));
                            esXMLValido = true;
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message;
                            esXMLValido = false;
                        }
                
                        if (esXMLValido)
                        {
                            documentoXMLComprimido = SIGAPred.Common.Compresion.Compresion.Comprimir(documentoXML, SIGAPred.Common.Compresion.Compresion.TipoFichero.DocumentoTexto);
                            XElement xmlAnio = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                            DateTime fechaAvaluo = DateTime.Parse(XmlSearchById(xmlAnio, "a.2").ToStringXElement());
                            if (fechaAvaluo.Year > 2020)
                            {
                                if (rbNormal.Checked)
                                {

                                    //Paso 1: Todas validaciones menos VUS
                                    if (Condiciones.Web(Constantes.FUN_PERITO))
                                    {
                                        ServiceAvaluosNuevos.AvaluosClient clienteAvaluos = new ServiceAvaluosNuevos.AvaluosClient();

                                        try
                                        {
                                            erroresValidacion = clienteAvaluos.EsValidoAvaluo(documentoXMLComprimido, Convert.ToInt32(Usuarios.IdPersona()), true);
                                        }
                                        finally
                                        {
                                            clienteAvaluos.Disconnect();
                                        }
                                    }

                                    if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                                    {
                                        ServiceAvaluosNuevos.AvaluosClient clienteAvaluos = new ServiceAvaluosNuevos.AvaluosClient();

                                        try
                                        {
                                            erroresValidacion = clienteAvaluos.EsValidoAvaluo(documentoXMLComprimido, Convert.ToInt32(Usuarios.IdPersona()), false);
                                        }
                                        finally
                                        {
                                            clienteAvaluos.Disconnect();
                                        }
                                    }


                                    int id = 1000;

                                    //JACM Se agrega validacion e21n17

                                    XElement xmlVAL = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                                    bool esComercial = (Decimal)xmlVAL.Descendants((XName)"Comercial").Count<XElement>() > 0M;
                                    // Boolean checkb7 = false;
                                    try
                                    {
                                        var rowVAL = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%Lista esperada de elementos posibles: ''TipoDeInmueble%'").FirstOrDefault();
                                        rowVAL["DESCRIPCION"] = "b.7 - El contenido del elemento 'Antecedentes' está incompleto. Lista esperada de elementos posibles: 'TipoDeInmueble'.";
                                    }
                                    catch (Exception exe)
                                    {
                                        
                                    }


                                    try
                                    {
                                        var rowVALe5del = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%e.5 %'").FirstOrDefault();
                                        rowVALe5del.Delete();
                                    }
                                    catch (Exception exe) { }

                                    try
                                    {
                                        var rowVALe5ndel = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%VidaUtilRemanentePonderadaDelInmueble%'").FirstOrDefault();
                                        rowVALe5ndel.Delete();
                                    }
                                    catch (Exception exe) { }

                                    string vidas = "";
                                    // VidaMinimaRemanentePonderadaDelInmueble
                                    try
                                    {
                                        vidas = XmlSearchById(xmlVAL, "e.5").FirstOrDefault().Name.ToString();

                                        if (!vidas.Equals("VidaMinimaRemanentePonderadaDelInmueble"))
                                        {
                                            var rowVALe5 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                            rowVALe5["IDERROR"] = id;
                                            rowVALe5["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                            rowVALe5["DESCRIPCION"] = "e.5 - El elemento 'VidaMinimaRemanentePonderadaDelInmueble' está incompleto. Lista esperada de elementos posibles: 'VidaMinimaRemanentePonderadaDelInmueble'.";
                                            erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALe5);
                                            id++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        var rowVALe5 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                        rowVALe5["IDERROR"] = id;
                                        rowVALe5["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                        rowVALe5["DESCRIPCION"] = "e.6 - El elemento 'PorcentSuperfUltimNivelRespectoAnterior' contiene un elemento no válido '"+vidas+ "'. Lista esperada de elementos posibles: 'PorcentSuperfUltimNivelRespectoAnterior'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALe5);
                                        id++;
                                    }
                                       



                                    try
                                    {
                                        if (XmlSearchById(xmlVAL, "e.2.4").IsFull())
                                        {

                                            var rowVALe24 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                            rowVALe24["IDERROR"] = id;
                                            rowVALe24["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                            rowVALe24["DESCRIPCION"] = "e.2.4 - El contenido del elemento 'ValorTotalDeLasConstruccionesProIndiviso' es inválido. Lista esperada de elementos posibles: 'ConstruccionesComunes'.";
                                            erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALe24);
                                            id++;
                                        }
                                    }
                                    catch (Exception ex) { }




                                    try { string b7 = XmlSearchById(xmlVAL, "b.7").ToStringXElement(); }
                                    catch (Exception ex)
                                    {
                                       
                                        var rowVALb7 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                        rowVALb7["IDERROR"] = id;
                                        rowVALb7["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                        rowVALb7["DESCRIPCION"] = "b.7 - El contenido del elemento 'Antecedentes' está incompleto. Lista esperada de elementos posibles: 'TipoDeInmueble'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb7);
                                        id++;
                                    }

                                    try { string b7 = XmlSearchById(xmlVAL, "b.7").ToStringXElement(); }
                                    catch (Exception ex)
                                    {

                                        var rowVALb7 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                        rowVALb7["IDERROR"] = id;
                                        rowVALb7["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                        rowVALb7["DESCRIPCION"] = "b.7 - El contenido del elemento 'Antecedentes' está incompleto. Lista esperada de elementos posibles: 'TipoDeInmueble'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb7);
                                        id++;
                                    }

                                    try
                                    {
                                        var rowVALe24del = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%e.2.4 %'").FirstOrDefault();
                                        rowVALe24del.Delete();
                                    }
                                    catch (Exception exe) {}

                                    try
                                    {
                                        var rowVALe24ndel = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%ValorTotalDeLasConstruccionesProIndiviso%'").FirstOrDefault();
                                        rowVALe24ndel.Delete();
                                    }
                                    catch (Exception exe) { }

                                    try { 
                                        if (XmlSearchById(xmlVAL, "e.2.4").IsFull()) {

                                            var rowVALe24 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                            rowVALe24["IDERROR"] = id;
                                            rowVALe24["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                            rowVALe24["DESCRIPCION"] = "e.2.4 - El contenido del elemento 'ValorTotalDeLasConstruccionesProIndiviso' es inválido. Lista esperada de elementos posibles: 'ConstruccionesComunes'.";
                                            erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALe24);
                                            id++;
                                        } 
                                    }
                                    catch (Exception ex) { }


                                    if (esComercial)
                                    {
                                        Boolean g111 = false;
                                        try
                                        {
                                            var rowVAL = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%Lista esperada de elementos posibles: ''Consideraciones%'").FirstOrDefault();
                                            rowVAL["DESCRIPCION"] = "g.1.1 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'Consideraciones'.";
                                            g111 = true;
                                        }

                                        catch (Exception exe)
                                        {

                                        }
                                        try { string g111ele = XmlSearchById(xmlVAL, "g.1.1").ToStringXElement(); }
                                        catch (Exception ex)
                                        {
                                            if (!g111)
                                            {
                                                var rowVALg11 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALg11["IDERROR"] = id;
                                                rowVALg11["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALg11["DESCRIPCION"] = "g.1.1 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaEXPOSICIONDEMOTIVOS'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALg11);
                                                id++;
                                            }
                                            g111 = false;
                                        }

                                        Boolean g112 = false;
                                        try
                                        {
                                            var rowVAL = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%Lista esperada de elementos posibles: ''MemoriaTecnicaEXPOSICIONDEMOTIVOS%'").FirstOrDefault();
                                            rowVAL["DESCRIPCION"] = "g.1.2 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaEXPOSICIONDEMOTIVOS'.";
                                            g112 = true;
                                        }

                                        catch (Exception exe)
                                        {

                                        }
                                        try { string g111ele = XmlSearchById(xmlVAL, "g.1.2").ToStringXElement(); }
                                        catch (Exception ex)
                                        {
                                            if (!g112)
                                            {
                                                var rowVALg11 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALg11["IDERROR"] = id;
                                                rowVALg11["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALg11["DESCRIPCION"] = "g.1.2 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaDESGLOSEDEINFORMACION'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALg11);
                                                id++;
                                            }
                                            g112 = false;
                                        }

                                        Boolean g113 = false;
                                        try
                                        {
                                            var rowVAL = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%Lista esperada de elementos posibles: ''MemoriaTecnicaDESGLOSEDEINFORMACION% '").FirstOrDefault();
                                            rowVAL["DESCRIPCION"] = "g.1.3 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaDESGLOSEDEINFORMACION'.";
                                            g113 = true;
                                        }

                                        catch (Exception exe)
                                        {

                                        }
                                        try { string g111ele = XmlSearchById(xmlVAL, "g.1.3").ToStringXElement(); }
                                        catch (Exception ex)
                                        {
                                            if (!g113)
                                            {
                                                var rowVALg11 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALg11["IDERROR"] = id;
                                                rowVALg11["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALg11["DESCRIPCION"] = "g.1.3 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'Consideraciones'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALg11);
                                                id++;
                                            }
                                            g113 = false;
                                        }

                                        Boolean g114 = false;
                                        try
                                        {
                                            var rowVAL = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%Lista esperada de elementos posibles: ''MemoriaTecnicaDESCRIPCIONDECALCULOSREALIZADOS%'").FirstOrDefault();
                                            rowVAL["DESCRIPCION"] = "g.1.4 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaDESCRIPCIONDECALCULOSREALIZADOS'.";
                                            g114 = true;
                                        }

                                        catch (Exception exe)
                                        {

                                        }
                                        try { string g114ele = XmlSearchById(xmlVAL, "g.1.4").ToStringXElement(); }
                                        catch (Exception ex)
                                        {
                                            if (!g114)
                                            {
                                                var rowVALg14 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALg14["IDERROR"] = id;
                                                rowVALg14["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALg14["DESCRIPCION"] = "g.1.4 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaDESCRIPCIONDECALCULOSREALIZADOS'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALg14);
                                                id++;
                                            }
                                            g114 = false;
                                        }

                                        Boolean g115 = false;
                                        try
                                        {
                                            var rowVAL = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%Lista esperada de elementos posibles: ''MemoriaTecnicaOTROSPARASUSTENTO%'").FirstOrDefault();
                                            rowVAL["DESCRIPCION"] = "g.1.5 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaOTROSPARASUSTENTO'.";
                                            g115 = true;
                                        }

                                        catch (Exception exe)
                                        {

                                        }
                                        try { string g115ele = XmlSearchById(xmlVAL, "g.1.5").ToStringXElement(); }
                                        catch (Exception ex)
                                        {
                                            if (!g115)
                                            {
                                                var rowVALg15 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALg15["IDERROR"] = id;
                                                rowVALg15["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALg15["DESCRIPCION"] = "g.1.5 - El contenido del elemento 'ConsideracionesPreviasAlAvaluo' está incompleto. Lista esperada de elementos posibles: 'MemoriaTecnicaOTROSPARASUSTENTO'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALg15);
                                                id++;
                                            }
                                            g115 = false;
                                        }


                                    }


                                    if (esComercial)
                                    {
                                        try
                                        {
                                            var rowVAL222 = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.2.2.2%'").FirstOrDefault();
                                            rowVAL222.Delete();
                                            var rowVAL422 = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.4.2.2%'").FirstOrDefault();
                                            rowVAL422.Delete();
                                        }
                                        catch (Exception ex) { }

                                        string h122 = "";
                                        string h1352 = "";
                                        string h222 = "";
                                        string h422 = "";

                                        try {

                                            

                                            var terrenos = XmlSearchById(xmlVAL, "h.1");
                                            foreach (XElement terreno in terrenos)
                                            {
                                                var tipoterreno = XmlSearchById(terreno, "h.1.1").FirstOrDefault().Name.ToString();
                                                if (tipoterreno == "TerrenosResiduales")
                                                {
                                                    try
                                                    {
                                                        var rowVAL122del = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.1.2.2%'").FirstOrDefault();
                                                        rowVAL122del.Delete();
                                                        var rowVAL1352del = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.1.3.5.2%'").FirstOrDefault();
                                                        rowVAL1352del.Delete();
                                                    }
                                                    catch (Exception ex) { }

                                                    try { h122 = XmlSearchById(terreno, "h.1.2.2").FirstOrDefault().Name.ToString(); }
                                                    catch (Exception ex)
                                                    {
                                                        var rowVAL122 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL122["IDERROR"] = id;
                                                        rowVAL122["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL122["DESCRIPCION"] = "h.1.2.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' está incompleto. Lista esperada de elementos posibles: 'ValorUnitarioDeTierraHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL122);
                                                        id++;
                                                    }

                                                    try
                                                    {
                                                        h1352 = XmlSearchById(terreno, "h.1.3.5.2").FirstOrDefault().Name.ToString();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        var rowVAL1352 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL1352["IDERROR"] = id;
                                                        rowVAL1352["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL1352["DESCRIPCION"] = "h.1.3.5.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' está incompleto. Lista esperada de elementos posibles: 'ValorUnitarioHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL1352);
                                                        id++;
                                                    }

                                                    if (!h122.Equals("ValorUnitarioDeTierraHomologadoPromedio"))
                                                    {
                                                        var rowVAL122 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL122["IDERROR"] = id;
                                                        rowVAL122["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL122["DESCRIPCION"] = "h.1.2.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' tiene un elemento secundario '" + h122
                                                            + "' no válido. Lista esperada de elementos posibles: 'ValorUnitarioDeTierraHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL122);
                                                        id++;
                                                    }

                                                    if (!h1352.Equals("ValorUnitarioHomologadoPromedio"))
                                                    {
                                                        var rowVAL1352 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL1352["IDERROR"] = id;
                                                        rowVAL1352["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL1352["DESCRIPCION"] = "h.1.3.5.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' tiene un elemento secundario '" + h1352
                                                            + "' no válido. Lista esperada de elementos posibles: 'ValorUnitarioHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL1352);
                                                        id++;
                                                    }

                                                }
                                            }
                                                try
                                                {
                                                var terrenosh = XmlSearchById(xmlVAL, "h.2");
                                                foreach (XElement terreno in terrenosh)
                                                {
                                                    try
                                                    {
                                                        h222 = XmlSearchById(terreno, "h.2.2.2").FirstOrDefault().Name.ToString();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        var rowVAL222 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL222["IDERROR"] = id;
                                                        rowVAL222["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL222["DESCRIPCION"] = "h.2.2.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' está incompleto. Lista esperada de elementos posibles: 'ValorUnitarioHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL222);
                                                        id++;
                                                    }
                                                }
                                                var terrenosh4 = XmlSearchById(xmlVAL, "h.4");
                                                foreach (XElement terreno in terrenosh4)
                                                {
                                                    try
                                                    {
                                                        h422 = XmlSearchById(terreno, "h.4.2.2").FirstOrDefault().Name.ToString();
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        var rowVAL422 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL422["IDERROR"] = id;
                                                        rowVAL422["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL422["DESCRIPCION"] = "h.4.2.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' está incompleto. Lista esperada de elementos posibles: 'ValorUnitarioHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL422);
                                                        id++;
                                                    }




                                                    if (!h222.Equals("ValorUnitarioHomologadoPromedio"))
                                                    {
                                                        var rowVAL222 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL222["IDERROR"] = id;
                                                        rowVAL222["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL222["DESCRIPCION"] = "h.2.2.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' tiene un elemento secundario '" + h222
                                                            + "' no válido. Lista esperada de elementos posibles: 'ValorUnitarioHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL222);
                                                        id++;
                                                    }

                                                    if (!h422.Equals("ValorUnitarioHomologadoPromedio"))
                                                    {
                                                        var rowVAL422 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL422["IDERROR"] = id;
                                                        rowVAL422["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL422["DESCRIPCION"] = "h.4.2.2 - El elemento 'ConclusionesHomologacionConstruccionesEnVenta' tiene un elemento secundario '" + h422
                                                            + "' no válido. Lista esperada de elementos posibles: 'ValorUnitarioHomologadoPromedio'.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL422);
                                                        id++;
                                                    }
                                                }

                                        }
                                        catch (Exception ex) { }
                                               


                                            
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                    }



                                    try
                                    {


                                        if (!esComercial)
                                        {

                                            try
                                            {
                                                var rowVAL17del = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%e.2.1.n.17%'").FirstOrDefault();
                                                rowVAL17del.Delete();
                                                var rowVAL517del = (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%e.2.5.n.17%'").FirstOrDefault();
                                                rowVAL517del.Delete();
                                            }
                                            catch (Exception ex) { }


                                            var rowVAL517 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();

                                            var e2 = XmlSearchById(xmlVAL, "e.2.5");
                                            foreach (XElement element in e2)
                                            {
                                                //IEnumerable<XElement> xmlCuentaCat = null;
                                                string uso = XmlSearchById(element, "e.2.5.n.2").ToStringXElement();
                                                string clase = XmlSearchById(element, "e.2.5.n.6").ToStringXElement();
                                                string dep = XmlSearchById(element, "e.2.5.n.17").ToStringXElement();

                                                if ((uso == "P" || uso == "PE" || uso == "PC" || uso == "J") && clase == "U" && dep != "1")
                                                {
                                                    rowVAL517["IDERROR"] = id;
                                                    rowVAL517["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL517["DESCRIPCION"] = "e.2.5.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL517);
                                                    id++;
                                                }
                                                else
                                                {

                                                    decimal e_2_5_n_7 = XmlSearchById(element, "e.2.5.n.17").ToDecimalXElement();

                                                    if (e_2_5_n_7 > 50M) //Se topa el valor de e_2_1_n_7 a 50
                                                        e_2_5_n_7 = 50M;

                                                    if (!(dep.ToDecimal().ToRound2() == ((100M - (e_2_5_n_7 * 0.8M)) / 100M).ToRound2()))
                                                    {
                                                        var rowVAL18 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL18["IDERROR"] = id;
                                                        rowVAL18["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL18["DESCRIPCION"] = "e.2.5.n.17 Error de calculo.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL18);
                                                        id++;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex) { }


                                    try
                                    {


                                        if (!esComercial)
                                        {
                                            var rowVAL17 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                            var e2 = XmlSearchById(xmlVAL, "e.2.1");

                                            foreach (XElement element in e2)
                                            {
                                                //IEnumerable<XElement> xmlCuentaCat = null;
                                                string uso = XmlSearchById(element, "e.2.1.n.2").ToStringXElement();
                                                string clase = XmlSearchById(element, "e.2.1.n.6").ToStringXElement();
                                                string dep = XmlSearchById(element, "e.2.1.n.17").ToStringXElement();
                                                
                                                if ((uso == "P" || uso == "PE" || uso == "PC" || uso == "J") && clase == "U" && dep != "1")
                                                {
                                                    rowVAL17["IDERROR"] = id;
                                                    rowVAL17["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL17["DESCRIPCION"] = "e.2.1.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL17);
                                                    id++;
                                                }
                                                else
                                                {

                                                    decimal e_2_1_n_7 = XmlSearchById(element, "e.2.1.n.7").ToDecimalXElement();

                                                    if (e_2_1_n_7 > 50M) //Se topa el valor de e_2_1_n_7 a 50
                                                        e_2_1_n_7 = 50M;

                                                    if (!(dep.ToDecimal().ToRound2() == ((100M - (e_2_1_n_7 * 0.8M)) / 100M).ToRound2()))
                                                    {
                                                        var rowVAL18 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL18["IDERROR"] = id;
                                                        rowVAL18["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL18["DESCRIPCION"] = "e.2.1.n.17 Error de calculo.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL18);
                                                        id++;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex) { }



                                    if (erroresValidacion.Any())
                                    {
                                        ServiceAvaluosNuevos.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable newIntentoDT = new ServiceAvaluosNuevos.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable();
                                        ServiceAvaluosNuevos.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PRow intentoFallidoRow = newIntentoDT.NewFEXAVA_INTENTOFALLIDO_PRow();

                                        //Obtenemos los datos del Avalúo recibido
                                        IEnumerable<XElement> indentificacion = null;
                                        XElement data = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                                        // a - Identificacion.
                                        indentificacion = XmlSearchById(data, "a");
                                        if (indentificacion.IsFull())
                                        {
                                            IEnumerable<XElement> numeroAvaluo = null;
                                            numeroAvaluo = XmlSearchById(indentificacion, "a.1");
                                            if (numeroAvaluo.IsFull())
                                            {
                                                if ((numeroAvaluo.ToStringXElement()).Trim().Length > 30) //30 es la longitud máxima en BD, si ´la longitud mayor de 30 al insertar en BD dará error
                                                    intentoFallidoRow.NUMEROAVALUO = ((numeroAvaluo.ToStringXElement()).Trim()).Substring(0, 27) + "...";
                                                else
                                                    intentoFallidoRow.NUMEROAVALUO = ((numeroAvaluo.ToStringXElement()).Trim());
                                            }
                                            //Dependiendo del rol hacer uno u otro
                                            if (Condiciones.Web(Constantes.FUN_PERITO))
                                            {
                                                //Clave valuador
                                                intentoFallidoRow.IDPERSONAPERITO = Convert.ToInt32(Usuarios.IdPersona());
                                            }
                                            else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                                            {
                                                //Clave sociedad
                                                intentoFallidoRow.IDPERSONASOCIEDAD = Convert.ToInt32(Usuarios.IdPersona());
                                            }
                                        }

                                        intentoFallidoRow.FECHAINTENTOSUBIDA = System.DateTime.Now;
                                        intentoFallidoRow.ERRORES = string.Empty;

                                        foreach (ServiceAvaluosNuevos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow rowError in erroresValidacion)
                                        {
                                            intentoFallidoRow.ERRORES += Environment.NewLine + string.Empty + rowError.IDERROR.ToString() + "- " + rowError.TIPOERROR.ToString() + Constantes.SIMBOLO_DOSPUNTOS + rowError.DESCRIPCION.ToString();
                                        }
                                        try
                                        {
                                            newIntentoDT.Rows.Add(intentoFallidoRow);

                                            ServiceAvaluosNuevosAvaluosClient clienteAvaluos = new ServiceAvaluosNuevosAvaluosClient();

                                            try
                                            {
                                                clienteAvaluos.RegistrarIntentoFallido(newIntentoDT);
                                            }
                                            finally
                                            {
                                                clienteAvaluos.Disconnect();
                                            }

                                            ModalAvaluoError.ErrorDT = erroresValidacion;
                                            btnGuardar_ModalPopupExtender.Show();
                                        }
                                        catch (ArgumentException ex)
                                        {
                                            string mensaje = ex.Message + Environment.NewLine + ex.StackTrace;
                                            MostrarMensajeInfoExcepcion(Constantes.MSJ_REGISTRAR_INTENTOFALLIDO + Constantes.SIMBOLO_DOSPUNTOS + mensaje);
                                        }
                                    }
                                    else
                                    {
                                        //Paso 2: Validar ValorUnitarioSuelo (VUS) 
                                        IEnumerable<XElement> valorUnitarioSuelo = null;
                                        XElement xml = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                                        if (!xmlAvaluo.SelectSingleNode(Constantes.NODO_RAIZ).FirstChild.Name.Equals(Constantes.PAR_XML_AV_CATASTRAL))
                                        {
                                            //Region 
                                            string region = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.1");
                                            if (valorUnitarioSuelo.IsFull())
                                                region = valorUnitarioSuelo.ToStringXElement();

                                            //Manzana
                                            string manzana = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.2");
                                            if (valorUnitarioSuelo.IsFull())
                                                manzana = valorUnitarioSuelo.ToStringXElement();

                                            //Lote
                                            string lote = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.3");
                                            if (valorUnitarioSuelo.IsFull())
                                                lote = valorUnitarioSuelo.ToStringXElement();

                                            //Unidad
                                            string unidad = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.4");
                                            if (valorUnitarioSuelo.IsFull())
                                                unidad = valorUnitarioSuelo.ToStringXElement();

                                            //AreaValor 
                                            string areaValor = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "d.5.1.n.8");
                                            if (valorUnitarioSuelo.IsFull())
                                                areaValor = valorUnitarioSuelo.ToStringXElement();

                                            //Valor unitario suelo
                                            decimal valorUnitario;
                                            valorUnitarioSuelo = XmlSearchById(xml, "h.1.4");
                                            if (valorUnitarioSuelo.IsFull())
                                            {
                                                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX", false);
                                                valorUnitario = Convert.ToDecimal(valorUnitarioSuelo.ToStringXElement(), culture);

                                                ServiceAvaluosNuevos.AvaluosClient clienteAvaluos = new ServiceAvaluosNuevos.AvaluosClient();

                                                try
                                                {
                                                    erroresValidacion = clienteAvaluos.ValidarXmlValorUnitarioSuelo(region, manzana, lote, unidad, areaValor, valorUnitario);
                                                }
                                                finally
                                                {
                                                    clienteAvaluos.Disconnect();
                                                }



                                                //No hay valores sufientes para validar
                                                if (erroresValidacion.Any())
                                                {
                                                    if (erroresValidacion[0].TIPOERROR.Equals(Constantes.ERROR_VUS_COD_SINVALORES))
                                                    {
                                                        ModalConfirmacion.CancelarVisible = false;
                                                        string str = erroresValidacion[0].DESCRIPCION.ToString();
                                                        ModalConfirmacion.TextoConfirmacion = str.Replace(Environment.NewLine, string.Empty); //Eliminar los saltos de línea con los que llega el msj
                                                        GuardarViewstateDocumentoXML(documentoXMLComprimido);
                                                        confirmar_ModalPopupExtender.Show();
                                                        //EL registro del avalúo se lanza desde el evento de la opción aceptar de la modal confirmación

                                                    }
                                                    //// VUS fuera de rango
                                                    else if (erroresValidacion[0].TIPOERROR.Equals(Constantes.ERROR_VUS_COD_FUERADERANGO))
                                                    {
                                                        ModalConfirmacion.CancelarVisible = true;
                                                        string str = erroresValidacion[0].DESCRIPCION;
                                                        ModalConfirmacion.TextoConfirmacion = str.Replace(Environment.NewLine, string.Empty); //Eliminar los saltos de línea con los que llega el msj
                                                        GuardarViewstateDocumentoXML(documentoXMLComprimido);
                                                        confirmar_ModalPopupExtender.Show();
                                                    }
                                                }
                                                //Paso 3: Si se han pasado todas las validaciones registrar el avalúo 
                                                else //No hay errores de validación 
                                                {
                                                    RealizarRegistroAvaluo(documentoXMLComprimido);
                                                    MostrarMensajeAvRegistrado();
                                                }
                                            }
                                            else //No hay errores de validación 
                                            {
                                                RealizarRegistroAvaluo(documentoXMLComprimido);
                                                MostrarMensajeAvRegistrado();
                                            }
                                        }
                                        else //No hay errores de validación 
                                        {
                                            RealizarRegistroAvaluo(documentoXMLComprimido);
                                            MostrarMensajeAvRegistrado();
                                        }
                                    }
                                }
                                else //Es avalúos especial
                                {
                                    string[] cuentaCat = new string[4];
                                    XElement xml = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;

                                    IEnumerable<XElement> xmlCuentaCat = null;


                                    xmlCuentaCat = XmlSearchById(xml, "b.3.10");

                                    ServiceAvaluosNuevos.AvaluosClient clienteAvaluos = new ServiceAvaluosNuevos.AvaluosClient();

                                    try
                                    {
                                        clienteAvaluos.RegistrarAvaluoEspecial(documentoXML);
                                    }
                                    finally
                                    {
                                        clienteAvaluos.Disconnect();
                                    }

                                    lblTextoInformacion.Text = Constantes.MSJ_SUBIRAVALUO_REGISTRADOCORRECTAMENTE;
                                    btnGuardar_ModalPopupExtenderRegistrado.Show();
                                    uppMensajeExito.Update();
                                }
                            }
                            else 
                            {
                                if (rbNormal.Checked)
                                {
                                    ServiceAvaluosAnterior.DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable erroresValidacionAnterior = null;
                                    //Paso 1: Todas validaciones menos VUS
                                    if (Condiciones.Web(Constantes.FUN_PERITO))
                                    {
                                        ServiceAvaluosAnterior.AvaluosClient clienteAvaluos = new ServiceAvaluosAnterior.AvaluosClient();
                                        //ServiceAvaluosAnterior clienteAvaluos = new ServiceAvaluosAnterior.AvaluosAnteriorClient();

                                        try
                                        {
                                            erroresValidacionAnterior = clienteAvaluos.EsValidoAvaluo(documentoXMLComprimido, Convert.ToInt32(Usuarios.IdPersona()), true);

                                        }
                                        finally
                                        {
                                            clienteAvaluos.Disconnect();
                                        }
                                    }

                                    if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                                    {
                                        ServiceAvaluosAnterior.AvaluosClient clienteAvaluos = new ServiceAvaluosAnterior.AvaluosClient();

                                        try
                                        {
                                            erroresValidacionAnterior = clienteAvaluos.EsValidoAvaluo(documentoXMLComprimido, Convert.ToInt32(Usuarios.IdPersona()), false);
                                        }
                                        finally
                                        {
                                            clienteAvaluos.Disconnect();
                                        }
                                    }

                                    if (erroresValidacionAnterior.Any())
                                    {
                                        ServiceAvaluosAnterior.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable newIntentoDT = new ServiceAvaluosAnterior.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable();
                                        ServiceAvaluosAnterior.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PRow intentoFallidoRow = newIntentoDT.NewFEXAVA_INTENTOFALLIDO_PRow();

                                        //Obtenemos los datos del Avalúo recibido
                                        IEnumerable<XElement> indentificacion = null;
                                        XElement data = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                                        // a - Identificacion.
                                        indentificacion = XmlSearchById(data, "a");
                                        if (indentificacion.IsFull())
                                        {
                                            IEnumerable<XElement> numeroAvaluo = null;
                                            numeroAvaluo = XmlSearchById(indentificacion, "a.1");
                                            if (numeroAvaluo.IsFull())
                                            {
                                                if ((numeroAvaluo.ToStringXElement()).Trim().Length > 30) //30 es la longitud máxima en BD, si ´la longitud mayor de 30 al insertar en BD dará error
                                                    intentoFallidoRow.NUMEROAVALUO = ((numeroAvaluo.ToStringXElement()).Trim()).Substring(0, 27) + "...";
                                                else
                                                    intentoFallidoRow.NUMEROAVALUO = ((numeroAvaluo.ToStringXElement()).Trim());
                                            }
                                            //Dependiendo del rol hacer uno u otro
                                            if (Condiciones.Web(Constantes.FUN_PERITO))
                                            {
                                                //Clave valuador
                                                intentoFallidoRow.IDPERSONAPERITO = Convert.ToInt32(Usuarios.IdPersona());
                                            }
                                            else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
                                            {
                                                //Clave sociedad
                                                intentoFallidoRow.IDPERSONASOCIEDAD = Convert.ToInt32(Usuarios.IdPersona());
                                            }
                                        }

                                        intentoFallidoRow.FECHAINTENTOSUBIDA = System.DateTime.Now;
                                        intentoFallidoRow.ERRORES = string.Empty;
                                        erroresValidacion = new DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable();
                                        foreach (ServiceAvaluosAnterior.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow rowError in erroresValidacionAnterior)
                                        {
                                            intentoFallidoRow.ERRORES += Environment.NewLine + string.Empty + rowError.IDERROR.ToString() + "- " + rowError.TIPOERROR.ToString() + Constantes.SIMBOLO_DOSPUNTOS + rowError.DESCRIPCION.ToString();
                                            
                                            var row = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                            row["IDERROR"] = rowError.IDERROR.ToString();
                                            row["TIPOERROR"] = rowError.TIPOERROR.ToString();
                                            row["DESCRIPCION"] = rowError.DESCRIPCION.ToString();
                                            erroresValidacion.AddERROR_VALIDACION_AVALUORow(row);
                                        }
                                        try
                                        {
                                            newIntentoDT.Rows.Add(intentoFallidoRow);

                                            ServiceAvaluosAnterior.AvaluosClient clienteAvaluos = new ServiceAvaluosAnterior.AvaluosClient();

                                            try
                                            {
                                                clienteAvaluos.RegistrarIntentoFallido(newIntentoDT);
                                            }
                                            finally
                                            {
                                                clienteAvaluos.Disconnect();
                                            }
                                           
                                            ModalAvaluoError.ErrorDT = erroresValidacion;
                                            btnGuardar_ModalPopupExtender.Show();
                                        }
                                        catch (ArgumentException ex)
                                        {
                                            string mensaje = ex.Message;
                                            MostrarMensajeInfoExcepcion(Constantes.MSJ_REGISTRAR_INTENTOFALLIDO + Constantes.SIMBOLO_DOSPUNTOS + mensaje);
                                        }
                                    }
                                    else
                                    {
                                        //Paso 2: Validar ValorUnitarioSuelo (VUS) 
                                        IEnumerable<XElement> valorUnitarioSuelo = null;
                                        XElement xml = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                                        if (!xmlAvaluo.SelectSingleNode(Constantes.NODO_RAIZ).FirstChild.Name.Equals(Constantes.PAR_XML_AV_CATASTRAL))
                                        {
                                            //Region 
                                            string region = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.1");
                                            if (valorUnitarioSuelo.IsFull())
                                                region = valorUnitarioSuelo.ToStringXElement();

                                            //Manzana
                                            string manzana = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.2");
                                            if (valorUnitarioSuelo.IsFull())
                                                manzana = valorUnitarioSuelo.ToStringXElement();

                                            //Lote
                                            string lote = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.3");
                                            if (valorUnitarioSuelo.IsFull())
                                                lote = valorUnitarioSuelo.ToStringXElement();

                                            //Unidad
                                            string unidad = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "b.3.10.4");
                                            if (valorUnitarioSuelo.IsFull())
                                                unidad = valorUnitarioSuelo.ToStringXElement();

                                            //AreaValor 
                                            string areaValor = string.Empty;
                                            valorUnitarioSuelo = XmlSearchById(xml, "d.5.1.n.8");
                                            if (valorUnitarioSuelo.IsFull())
                                                areaValor = valorUnitarioSuelo.ToStringXElement();

                                            //Valor unitario suelo
                                            decimal valorUnitario;
                                            valorUnitarioSuelo = XmlSearchById(xml, "h.1.4");
                                            if (valorUnitarioSuelo.IsFull())
                                            {
                                                System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("es-MX", false);
                                                valorUnitario = Convert.ToDecimal(valorUnitarioSuelo.ToStringXElement(), culture);

                                                ServiceAvaluosAnterior.AvaluosClient clienteAvaluos = new ServiceAvaluosAnterior.AvaluosClient();

                                                try
                                                {
                                                    erroresValidacionAnterior = clienteAvaluos.ValidarXmlValorUnitarioSuelo(region, manzana, lote, unidad, areaValor, valorUnitario);
                                                }
                                                finally
                                                {
                                                    clienteAvaluos.Disconnect();
                                                }

                                                //No hay valores sufientes para validar
                                                if (erroresValidacionAnterior.Any())
                                                {
                                                    if (erroresValidacionAnterior[0].TIPOERROR.Equals(Constantes.ERROR_VUS_COD_SINVALORES))
                                                    {
                                                        ModalConfirmacion.CancelarVisible = false;
                                                        string str = erroresValidacionAnterior[0].DESCRIPCION.ToString();
                                                        ModalConfirmacion.TextoConfirmacion = str.Replace(Environment.NewLine, string.Empty); //Eliminar los saltos de línea con los que llega el msj
                                                        GuardarViewstateDocumentoXML(documentoXMLComprimido);
                                                        confirmar_ModalPopupExtender.Show();
                                                        //EL registro del avalúo se lanza desde el evento de la opción aceptar de la modal confirmación

                                                    }
                                                    //// VUS fuera de rango
                                                    else if (erroresValidacionAnterior[0].TIPOERROR.Equals(Constantes.ERROR_VUS_COD_FUERADERANGO))
                                                    {
                                                        ModalConfirmacion.CancelarVisible = true;
                                                        string str = erroresValidacionAnterior[0].DESCRIPCION;
                                                        ModalConfirmacion.TextoConfirmacion = str.Replace(Environment.NewLine, string.Empty); //Eliminar los saltos de línea con los que llega el msj
                                                        GuardarViewstateDocumentoXML(documentoXMLComprimido);
                                                        confirmar_ModalPopupExtender.Show();
                                                    }
                                                }
                                                //Paso 3: Si se han pasado todas las validaciones registrar el avalúo 
                                                else //No hay errores de validación 
                                                {
                                                    RealizarRegistroAvaluo(documentoXMLComprimido);
                                                    MostrarMensajeAvRegistrado();
                                                }
                                            }
                                            else //No hay errores de validación 
                                            {
                                                RealizarRegistroAvaluo(documentoXMLComprimido);
                                                MostrarMensajeAvRegistrado();
                                            }
                                        }
                                        else //No hay errores de validación 
                                        {
                                            RealizarRegistroAvaluo(documentoXMLComprimido);
                                            MostrarMensajeAvRegistrado();
                                        }
                                    }
                                }
                                else //Es avalúos especial
                                {
                                    string[] cuentaCat = new string[4];
                                    XElement xml = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;

                                    IEnumerable<XElement> xmlCuentaCat = null;


                                    xmlCuentaCat = XmlSearchById(xml, "b.3.10");

                                    ServiceAvaluosAnterior.AvaluosClient clienteAvaluos = new ServiceAvaluosAnterior.AvaluosClient();

                                    try
                                    {
                                        clienteAvaluos.RegistrarAvaluoEspecial(documentoXML);
                                    }
                                    finally
                                    {
                                        clienteAvaluos.Disconnect();
                                    }

                                    lblTextoInformacion.Text = Constantes.MSJ_SUBIRAVALUO_REGISTRADOCORRECTAMENTE;
                                    btnGuardar_ModalPopupExtenderRegistrado.Show();
                                    uppMensajeExito.Update();
                                }

                            }
                            
                        }

                        else
                        {
                            MostrarMensajeInformativo(string.Format("{0}: <br> {1} <br><br> Error: <br> {2}", Constantes.MSJ_XML_ERROR, "El documento introducido no es un XML o no está bien formado.", error), true);
                        }

                    }
                    else
                    {
                        //MostrarMensajeInformativo(Constantes.MSJ_ERROR_NO_ES_DOCUMENTO_XML, true);
                        MostrarMensajeInformativo("Error en el fichero: <br> Si ha introducido un fichero comprimido, compruebe que no tenga contraseña y que, dentro de éste, sólo existe un fichero XML de avalúo, sin otros ficheros ni carpetas.", true);
                    }
                }
                else
                {
                    MostrarMensajeInformativo(Constantes.MSJ_TAMANIO_FICHERO, true);
                }
            }
            else
            {
                MostrarMensajeInformativo(Constantes.MSJ_FICHERO_VACIO, true);
            }
        }
        catch (UserFailedException ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            MostrarMensajeInformativo(Constantes.MSJ_USUARIONOEXISTE_EXECP, false);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosException> cex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion + Environment.NewLine + cex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosInfoException> ciex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion + Environment.NewLine + ciex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FileUploadExtension.FileUploadExtensionException fuex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + fuex.Message + Environment.NewLine + fuex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            LimpiarViewStateDocumentoXML();
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }


    /// <summary>
    /// Ocultamos o mostramos el popup de la la validacion del avaluo.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void ModalAvaluoError_Ok_Click(object sender, CancelEventArgs e)
    {
        try
        {
            btnGuardar_ModalPopupExtender.Hide();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Ocultamos o mostramos el popup de la la validacion del avaluo.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void ModalErrorInfo_Ok_Click(object sender, CancelEventArgs e)
    {
        try
        {
            ModalPopupExtenderError.Hide();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
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
            extenderPnlInfoTokenModal.Hide();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Oculta el panel informativo del registro del avaluo.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void lnkOk_Click(object sender, EventArgs e)
    {
        try
        {
            btnGuardar_ModalPopupExtenderRegistrado.Hide();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

    /// <summary>
    /// Oculta el panel de confirmación.
    /// </summary>
    /// <param name="sender">Origen</param>
    /// <param name="e">Evento</param>
    protected void confirmar_ConfirmClick(object sender, CancelEventArgs e)
    {
        try
        {
            confirmar_ModalPopupExtender.Hide();
            if (!e.Cancel) //Sólo si se ha pulsado aceptar registrar el avalúo
            {
                byte[] documentoXML = RecuperarViewstateDocumentoXML();
                RealizarRegistroAvaluo(documentoXML);
                string numUnico = ViewState[Constantes.PAR_VIEWSTATE_NUMUNICOREGISTRADO].ToString();
                lblTextoInformacion.Text = Constantes.MSJ_SUBIRAVALUO_NORMAL_REGISTRADOCORRECTAMENTE + Constantes.MSJ_NUM_UNICO + numUnico;
                btnGuardar_ModalPopupExtenderRegistrado.Show();
                uppMensajeExito.Update();
            }
            else
            {
                LimpiarViewStateDocumentoXML();
            }
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosException> cex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion + Environment.NewLine + cex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluosNuevos.AvaluosInfoException> ciex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion + Environment.NewLine + ciex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            LimpiarViewStateDocumentoXML();
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
    }
    #endregion

    /// <summary>
    /// Realizar registro avalúo.
    /// </summary>
    /// <param name="documentoXML">El documento XML que representa el avalúo que se quere registrar.</param>
    private void RealizarRegistroAvaluo(byte[] documentoXML)
    {
        if (ModalConfirmacion.Visible)
        {
            confirmar_ModalPopupExtender.Hide();
        }

        string numUnico;
        ServiceAvaluosNuevos.AvaluosClient clienteAvaluos = new ServiceAvaluosNuevos.AvaluosClient();

        try
        {
            numUnico = clienteAvaluos.RegistrarAvaluo(documentoXML, Usuarios.IdPersona());
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }

        ViewState.Add(Constantes.PAR_VIEWSTATE_NUMUNICOREGISTRADO, numUnico);
        LimpiarViewStateDocumentoXML();
    }

    /// <summary>
    /// Mostrar rb av especial.
    /// </summary>
    /// <param name="mostrar">Verdadero si se quiere mostrar la opción avalúo especial y false en caso contrario.</param>
    private void MostrarRbAvEspecial(bool mostrar)
    {
        if (mostrar)
        {
            this.rbEspecial.Visible = true;
            this.rbEspecial.Enabled = true;
            this.rbEspecial.Checked = true;
        }
        else
        {
            this.rbEspecial.Visible = false;
            this.rbEspecial.Enabled = false;
            this.rbEspecial.Checked = false;
        }
    }

    /// <summary>
    /// Mostrar rb av normal.
    /// </summary>
    /// <param name="mostrar">Verdadero si se quiere mostrar la opción avalúo normal y false en caso contrario.</param>
    private void MostrarRbAvNormal(bool mostrar)
    {
        if (mostrar)
        {
            this.rbNormal.Visible = true;
            this.rbNormal.Enabled = true;
            this.rbNormal.Checked = true;
        }
        else
        {
            this.rbNormal.Visible = false;
            this.rbNormal.Enabled = false;
            this.rbNormal.Checked = false;
        }
    }

    /// <summary>
    /// Obtener cuenta catastral del XML.
    /// </summary>
    /// <param name="xmlCuentaCat">Apartado del xml donde aparece la cuenta catatral.</param>
    /// <returns>
    /// La cuenta catastal desglosada y almacenada en un array
    /// </returns>
    private string[] ObtenerCuentaCatXml(IEnumerable<XElement> xmlCuentaCat)
    {
        string[] cuentaCat = new string[4];
        IEnumerable<XElement> resul = null;

        //Region 
        string region = string.Empty;
        resul = XmlSearchById(xmlCuentaCat, "b.3.10.1");
        if (resul.IsFull())
            region = resul.ToStringXElement();

        if (!string.IsNullOrEmpty(region))
            cuentaCat[0] = region;

        //Manzana
        string manzana = string.Empty;
        resul = XmlSearchById(xmlCuentaCat, "b.3.10.2");
        if (resul.IsFull())
            manzana = resul.ToStringXElement();

        if (!string.IsNullOrEmpty(manzana))
            cuentaCat[1] = manzana;

        //Lote
        string lote = string.Empty;
        resul = XmlSearchById(xmlCuentaCat, "b.3.10.3");
        if (resul.IsFull())
            lote = resul.ToStringXElement();

        if (!string.IsNullOrEmpty(lote))
            cuentaCat[2] = lote;

        //Unidad
        string unidad = string.Empty;
        resul = XmlSearchById(xmlCuentaCat, "b.3.10.4");
        if (resul.IsFull())
            unidad = resul.ToStringXElement();

        if (!string.IsNullOrEmpty(unidad))
            cuentaCat[3] = unidad;

        return cuentaCat;
    }
}


