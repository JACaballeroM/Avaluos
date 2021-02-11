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
using ServiceAvaluos;
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
            DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable erroresValidacion = null;
            byte[] documentoXMLComprimido = null;
            byte[] documentoXML = null;
            string error = string.Empty;

            if (fileAvaluoXML.HasFile)
            {
                int tamanioFichero = fileAvaluoXML.FileBytes.Length;
                bool validadoTamanioFichero = false;

                AvaluosClient clienteAvaluosTamanio = new AvaluosClient();

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

                            if (rbNormal.Checked)
                            {
                                //Paso 1: Todas validaciones menos VUS
                                if (Condiciones.Web(Constantes.FUN_PERITO))
                                {
                                    AvaluosClient clienteAvaluos = new AvaluosClient();

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
                                    AvaluosClient clienteAvaluos = new AvaluosClient();

                                    try
                                    {
                                        erroresValidacion = clienteAvaluos.EsValidoAvaluo(documentoXMLComprimido, Convert.ToInt32(Usuarios.IdPersona()), false);
                                    }
                                    finally
                                    {
                                        clienteAvaluos.Disconnect();
                                    }
                                }

                                if (erroresValidacion.Any())
                                {
                                    DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable newIntentoDT = new DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable();
                                    DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PRow intentoFallidoRow = newIntentoDT.NewFEXAVA_INTENTOFALLIDO_PRow();

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

                                    foreach (DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow rowError in erroresValidacion)
                                    {
                                        intentoFallidoRow.ERRORES += Environment.NewLine + string.Empty + rowError.IDERROR.ToString() + "- " + rowError.TIPOERROR.ToString() + Constantes.SIMBOLO_DOSPUNTOS + rowError.DESCRIPCION.ToString();
                                    }
                                    try
                                    {
                                        newIntentoDT.Rows.Add(intentoFallidoRow);

                                        AvaluosClient clienteAvaluos = new AvaluosClient();

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

                                            AvaluosClient clienteAvaluos = new AvaluosClient();

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

                                AvaluosClient clienteAvaluos = new AvaluosClient();

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
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FileUploadExtension.FileUploadExtensionException fuex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + fuex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            LimpiarViewStateDocumentoXML();
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
        {
            LimpiarViewStateDocumentoXML();
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (Exception ex)
        {
            LimpiarViewStateDocumentoXML();
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
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
        AvaluosClient clienteAvaluos = new AvaluosClient();

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


