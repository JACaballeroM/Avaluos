using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using ServiceAvaluos;
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
        catch (FaultException<ServiceAvaluos.AvaluosException> cex)
        {
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion + Environment.NewLine + cex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
            ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable erroresValidacion = null;
            byte[] documentoXMLComprimido = null;
            byte[] documentoXML = null;
            byte[] documentoXML1 = null;
            string error = string.Empty;

            if (fileAvaluoXML.HasFile)
            {
                int tamanioFichero = fileAvaluoXML.FileBytes.Length;
                bool validadoTamanioFichero = true;

                ServiceAvaluos.AvaluosClient clienteAvaluosTamanio = new ServiceAvaluos.AvaluosClient();

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
                        documentoXML1= fileAvaluoXML.FileBytes;
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
                        XElement xmlAnio = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                        DateTime fechaAvaluo = System.DateTime.Now;
                        try
                        {
                            fechaAvaluo = DateTime.Parse(XmlSearchById(xmlAnio, "a.2").ToStringXElement());
                        }
                        catch (Exception ex)
                        {
                            error = Constantes.ERR_FECHA_AVALUO;
                            esXMLValido = false;
                        }
                        if (esXMLValido)
                        {
                            documentoXMLComprimido = SIGAPred.Common.Compresion.Compresion.Comprimir(documentoXML, SIGAPred.Common.Compresion.Compresion.TipoFichero.DocumentoTexto);
                            
                            if (fechaAvaluo.Year > 2020)
                            {
                                if (rbNormal.Checked)
                                {

                                    //Paso 1: Todas validaciones menos VUS
                                    if (Condiciones.Web(Constantes.FUN_PERITO))
                                    {
                                        ServiceAvaluos.AvaluosClient clienteAvaluos = new ServiceAvaluos.AvaluosClient();

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
                                        ServiceAvaluos.AvaluosClient clienteAvaluos = new ServiceAvaluos.AvaluosClient();

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

                                    //JACM Se agregan validaciones 

                                    XElement xmlVAL = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;
                                    bool esComercial = (Decimal)xmlVAL.Descendants((XName)"Comercial").Count<XElement>() > 0M;
                                    // Boolean checkb7 = false;



                                     try
                                     {
                                         var character1 = "*";
                                         var character2 = "@";
                                         var character3 = "#";
                                         var character4 = "!";
                                         var character5 = "?";
                                         var txt = XmlSearchById(xmlVAL, "a.3").ToStringXElement();

                                         Boolean m = XmlSearchById(xmlVAL, "a.3").ToStringXElement().Contains(character1) ||
                                             XmlSearchById(xmlVAL, "a.3").ToStringXElement().Contains(character2) ||
                                             XmlSearchById(xmlVAL, "a.3").ToStringXElement().Contains(character3) ||
                                             XmlSearchById(xmlVAL, "a.3").ToStringXElement().Contains(character4) ||
                                             XmlSearchById(xmlVAL, "a.3").ToStringXElement().Contains(character5)
                                             ;

                                         if (m)
                                         {
                                             var rowVALregex = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                             rowVALregex["IDERROR"] = id;
                                             rowVALregex["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                             rowVALregex["DESCRIPCION"] = "a.3 - El elemento 'ClaveValuador' contiene uno o más caracteres no válidos.";
                                             erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALregex);
                                             id++;
                                         }


                                     }
                                     catch (Exception ex)
                                     { }


                                    try
                                    {
                                        var rowVAL19del = erroresValidacion.Select("DESCRIPCION like '%''Alcaldia'' no puede contener texto.%'");
                                        int ind = 1;
                                        foreach (var ren in rowVAL19del)
                                        {
                                            ren["DESCRIPCION"]="b."+ind.ToString()+
                                                ".9 El elemento 'Alcaldia' no puede contener texto. Lista esperada de posibles elementos:'ClaveAlcaldia'";
                                            ind++;
                                        }
                                       
                                    }
                                    catch (Exception ex) { }


                                    try
                                    {
                                        string b191 = XmlSearchById(xmlVAL, "b.1.9.1").ToStringXElement();
                                        if (b191.Equals("018"))
                                        {
                                            try
                                            {
                                                string b192 = XmlSearchById(xmlVAL, "b.1.9.2").ToStringXElement();
                                                if (b192.Trim().Equals(""))
                                                {
                                                    var rowVALb192 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVALb192["IDERROR"] = id;
                                                    rowVALb192["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVALb192["DESCRIPCION"] = "b.1.9.2 - El elemento 'Otros' no es válido. El valor '' no es válido según su tipo de datos 'Cadena' - La longitud real es menor que el valor de longitud mínima.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb192);
                                                    id++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                                var rowVALb192 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALb192["IDERROR"] = id;
                                                rowVALb192["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALb192["DESCRIPCION"] = "b.1.9.2 - El contenido del elemento 'Alcaldía' está incompleto. Lista esperada de elementos posibles: 'Otros'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb192);
                                                id++;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        var rowVALb192 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                        rowVALb192["IDERROR"] = id;
                                        rowVALb192["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                        rowVALb192["DESCRIPCION"] = "b.1.9.1 - El contenido del elemento 'Alcaldia' está incompleto. Lista esperada de elementos posibles: 'ClaveAlcaldia'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb192);
                                        id++;
                                    }


                                    try
                                    {
                                        string b291 = XmlSearchById(xmlVAL, "b.2.9.1").ToStringXElement();
                                        if (b291.Equals("018"))
                                        {
                                            try
                                            {
                                                string b192 = XmlSearchById(xmlVAL, "b.2.9.2").ToStringXElement();
                                                if (b192.Trim().Equals(""))
                                                {
                                                    var rowVALb292 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVALb292["IDERROR"] = id;
                                                    rowVALb292["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVALb292["DESCRIPCION"] = "b.2.9.2 - El elemento 'Otros' no es válido. El valor '' no es válido según su tipo de datos 'Cadena' - La longitud real es menor que el valor de longitud mínima.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb292);
                                                    id++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                                var rowVALb292 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALb292["IDERROR"] = id;
                                                rowVALb292["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALb292["DESCRIPCION"] = "b.2.9.2 - El contenido del elemento 'Alcaldía' está incompleto. Lista esperada de elementos posibles: 'Otros'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb292);
                                                id++;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        var rowVALb292 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                        rowVALb292["IDERROR"] = id;
                                        rowVALb292["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                        rowVALb292["DESCRIPCION"] = "b.2.9.1 - El contenido del elemento 'Alcaldia' está incompleto. Lista esperada de elementos posibles: 'ClaveAlcaldia'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb292);
                                        id++;
                                    }



                                    /*try
                                    {
                                        var rowVALb4del = erroresValidacion.Select("DESCRIPCION like '%''PropositoDelAvaluo''%'");
                                        foreach (var ren in rowVALb4del)
                                        {
                                            ren.Delete();
                                        }

                                    }
                                    catch (Exception ex) { }*/

                                    try 
                                    { 
                                        string b4 = XmlSearchById(xmlVAL, "b.4.1").ToStringXElement();
                                        if (b4.Equals("4"))
                                        {
                                            try
                                            {
                                                string b42 = XmlSearchById(xmlVAL, "b.4.2").ToStringXElement();
                                                if (b42.Trim().Equals(""))
                                                {
                                                    var rowVALb4 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVALb4["IDERROR"] = id;
                                                    rowVALb4["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVALb4["DESCRIPCION"] = "b.4.2 - El elemento 'Otros' no es válido. El valor '' no es válido según su tipo de datos 'Cadena' - La longitud real es menor que el valor de longitud mínima.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb4);
                                                    id++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                                var rowVALb4 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALb4["IDERROR"] = id;
                                                rowVALb4["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALb4["DESCRIPCION"] = "b.4.2 - El contenido del elemento 'PropositoDelAvaluo' está incompleto. Lista esperada de elementos posibles: 'Otros'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb4);
                                                id++;
                                            }
                                        }
                                    }
                                        catch (Exception ex)
                                        {

                                            var rowVALb4 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                            rowVALb4["IDERROR"] = id;
                                            rowVALb4["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                            rowVALb4["DESCRIPCION"] = "b.4.1 - El contenido del elemento 'PropositoDelAvaluo' está incompleto. Lista esperada de elementos posibles: 'ClavePropositoAvaluo'.";
                                            erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb4);
                                            id++;
                                        }


                                    try
                                    {
                                        string b7 = XmlSearchById(xmlVAL, "b.7.1").ToStringXElement();
                                        if (b7.Equals("19"))
                                        {
                                            try
                                            {
                                                string b72 = XmlSearchById(xmlVAL, "b.7.2").ToStringXElement();
                                                if (b72.Trim().Equals(""))
                                                {
                                                    var rowVALb7 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVALb7["IDERROR"] = id;
                                                    rowVALb7["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVALb7["DESCRIPCION"] = "b.7.2 - El elemento 'Otros' no es válido. El valor '' no es válido según su tipo de datos 'Cadena' - La longitud real es menor que el valor de longitud mínima.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb7);
                                                    id++;
                                                }
                                            }
                                            catch (Exception ex)
                                            {

                                                var rowVALb7 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                rowVALb7["IDERROR"] = id;
                                                rowVALb7["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALb7["DESCRIPCION"] = "b.7.2 - El contenido del elemento 'TipoDeInmueble' está incompleto. Lista esperada de elementos posibles: 'Otros'.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb7);
                                                id++;
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {

                                        var rowVALb7 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                        rowVALb7["IDERROR"] = id;
                                        rowVALb7["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                        rowVALb7["DESCRIPCION"] = "b.7.1 - El contenido del elemento 'TipoDeInmueble' está incompleto. Lista esperada de elementos posibles: 'ClaveTipoInmueble'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALb7);
                                        id++;
                                    }


                                    try
                                    {
                                        var rowVALe5del = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%e.5 %'").FirstOrDefault();
                                        rowVALe5del.Delete();
                                    }
                                    catch (Exception exe) { }

                                    try
                                    {
                                        var rowVALe5ndel = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%VidaUtilRemanentePonderadaDelInmueble%'").FirstOrDefault();
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
                                        rowVALe5["DESCRIPCION"] = "e.6 - El elemento 'PorcentSuperfUltimNivelRespectoAnterior' contiene un elemento no válido '" + vidas + "'. Lista esperada de elementos posibles: 'PorcentSuperfUltimNivelRespectoAnterior'.";
                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALe5);
                                        id++;
                                    }


                                    try
                                    {
                                        var rowVALe24del = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%e.2.4 %'").FirstOrDefault();
                                        rowVALe24del.Delete();
                                    }
                                    catch (Exception exe) { }

                                    try
                                    {
                                        var rowVALe24ndel = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%ValorTotalDeLasConstruccionesProIndiviso%'").FirstOrDefault();
                                        rowVALe24ndel.Delete();
                                    }
                                    catch (Exception exe) { }

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


                                    if (esComercial)
                                    {
                                        try
                                        {
                                            var rowVAL222 = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.2.2.2%'").FirstOrDefault();
                                            rowVAL222.Delete();
                                            var rowVAL422 = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.4.2.2%'").FirstOrDefault();
                                            rowVAL422.Delete();
                                        }
                                        catch (Exception ex) { }

                                        string h122 = "";
                                        string h1352 = "";
                                        string h222 = "";
                                        string h422 = "";

                                        try
                                        {



                                            var terrenos = XmlSearchById(xmlVAL, "h.1");
                                            foreach (XElement terreno in terrenos)
                                            {
                                                var tipoterreno = XmlSearchById(terreno, "h.1.1").FirstOrDefault().Name.ToString();
                                                if (tipoterreno == "TerrenosResiduales")
                                                {
                                                    try
                                                    {
                                                        var rowVAL122del = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.1.2.2%'").FirstOrDefault();
                                                        rowVAL122del.Delete();
                                                        var rowVAL1352del = (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)erroresValidacion.Select("DESCRIPCION like '%h.1.3.5.2%'").FirstOrDefault();
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
                                                var rowVAL217del = //(ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow)
                                                    erroresValidacion.Select("DESCRIPCION like '%e.2.1.n.17%'");//.FirstOrDefault();
                                                foreach (var ren in rowVAL217del)
                                                {
                                                    ren.Delete();
                                                }
                                            }
                                            catch (Exception ex) { }


                                            var rowVAL2117 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();

                                            var e21 = XmlSearchById(xmlVAL, "e.2.1");
                                            foreach (XElement element in e21)
                                            {
                                                //IEnumerable<XElement> xmlCuentaCat = null;
                                                string uso = XmlSearchById(element, "e.2.1.n.2").ToStringXElement();
                                                string clase = XmlSearchById(element, "e.2.1.n.6").ToStringXElement();
                                                string dep = XmlSearchById(element, "e.2.1.n.17").ToStringXElement();

                                                if ((uso == "P" || uso == "PE" || uso == "PC" || uso == "J") && clase == "U" && dep != "1")
                                                {
                                                    rowVAL2117["IDERROR"] = id;
                                                    rowVAL2117["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL2117["DESCRIPCION"] = "e.2.1.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL2117);
                                                    id++;
                                                }
                                                else if ((uso == "P" || uso == "PE" || uso == "PC" || uso == "J") && clase == "U" && dep == "1")
                                                { }
                                                else { 

                                                    decimal e_2_1_n_7 = XmlSearchById(element, "e.2.1.n.7").ToDecimalXElement();

                                                    if (e_2_1_n_7 > 50M) //Se topa el valor de e_2_1_n_7 a 50
                                                        e_2_1_n_7 = 50M;

                                                    if (!(dep.ToDecimal().ToRound2() == ((100M - (e_2_1_n_7 * 0.8M)) / 100M).ToRound2()))
                                                    {
                                                        var rowVAL19 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                        rowVAL19["IDERROR"] = id;
                                                        rowVAL19["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                        rowVAL19["DESCRIPCION"] = "e.2.1.n.17 Error de calculo.";
                                                        erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL19);
                                                        id++;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                    catch (Exception ex) { }

                                    try
                                    {
                                        var rowVALe25n3 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();

                                        var e25 = XmlSearchById(xmlVAL, "e.2.5");
                                        foreach (XElement element in e25)
                                        {
                                            string numNiveles = XmlSearchById(element, "e.2.5.n.3").ToStringXElement();
                                            if (numNiveles.Length > 3)
                                            {
                                                rowVALe25n3["IDERROR"] = id;
                                                rowVALe25n3["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                rowVALe25n3["DESCRIPCION"] = "e.2.5.n.3 - El elemento 'NumeroDeNivelesDelTipo' no es válido. El valor '"+numNiveles+
                                                    "' no es válido según su tipo de datos 'SUB-NumeroDeNivelesDelTipo' - Error de restricción TotalDigits.";
                                                erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVALe25n3);
                                                id++;
                                            }
                                        }
                                    }
                                    catch (Exception ex) { }

                                    try
                                    {


                                        if (!esComercial)
                                        {

                                            try
                                            {
                                                var rowVAL517del = erroresValidacion.Select("DESCRIPCION like '%e.2.5.n.17%'");
                                                foreach (var ren in rowVAL517del)
                                                {
                                                    ren.Delete();
                                                }
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
                                                else if ((uso == "P" || uso == "PE" || uso == "PC" || uso == "J") && clase == "U" && dep == "1")
                                                { }
                                                else
                                                {

                                                    decimal e_2_5_n_7 = XmlSearchById(element, "e.2.5.n.7").ToDecimalXElement();

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

                                        var e2 = XmlSearchById(xmlVAL, "e.2.1");
                                        foreach (XElement element in e2)
                                        {
                                            decimal e21n11 = XmlSearchById(element, "e.2.1.n.11").ToDecimalXElement();
                                            decimal e21n15 = XmlSearchById(element, "e.2.1.n.15").ToDecimalXElement();

                                            if (esComercial)
                                            {
                                                decimal e21n12 = XmlSearchById(element, "e.2.1.n.12").ToDecimalXElement();
                                                decimal e21n13 = XmlSearchById(element, "e.2.1.n.13").ToDecimalXElement();
                                                if (!(e21n15.ToRound2() == (e21n11 * e21n12 * e21n13).ToRound2()))
                                                {
                                                    var rowVAL2115 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVAL2115["IDERROR"] = id;
                                                    rowVAL2115["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL2115["DESCRIPCION"] = "e.2.1.n.15 Error de calculo.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL2115);
                                                    id++;
                                                }
                                            }
                                            else
                                            {
                                                decimal e21n16 = XmlSearchById(element, "e.2.1.n.16").ToDecimalXElement();
                                                decimal e21n17 = XmlSearchById(element, "e.2.1.n.17").ToDecimalXElement();
                                                if (!(e21n15.ToRound2() == (e21n11 * e21n16 * e21n17).ToRound2()))
                                                {
                                                    var rowVAL2115 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVAL2115["IDERROR"] = id;
                                                    rowVAL2115["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL2115["DESCRIPCION"] = "e.2.1.n.15 Error de calculo.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL2115);
                                                    id++;
                                                }
                                            }
                                        }


                                    }
                                    catch (Exception ex) { }


                                    try
                                    {

                                        var e2 = XmlSearchById(xmlVAL, "e.2.5");
                                        foreach (XElement element in e2)
                                        {
                                            decimal e25n11 = XmlSearchById(element, "e.2.5.n.11").ToDecimalXElement();
                                            decimal e25n15 = XmlSearchById(element, "e.2.5.n.15").ToDecimalXElement();

                                            if (esComercial)
                                            {
                                                decimal e25n12 = XmlSearchById(element, "e.2.5.n.12").ToDecimalXElement();
                                                decimal e25n13 = XmlSearchById(element, "e.2.5.n.13").ToDecimalXElement();
                                                if (!(e25n15.ToRound2() == (e25n11 * e25n12 * e25n13).ToRound2()))
                                                {
                                                    var rowVAL2515 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVAL2515["IDERROR"] = id;
                                                    rowVAL2515["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL2515["DESCRIPCION"] = "e.2.5.n.15 Error de calculo.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL2515);
                                                    id++;
                                                }
                                            }else
                                            {
                                                decimal e25n16 = XmlSearchById(element, "e.2.5.n.16").ToDecimalXElement();
                                                decimal e25n17 = XmlSearchById(element, "e.2.5.n.17").ToDecimalXElement();
                                                if (!(e25n15.ToRound2() == (e25n11 * e25n16 * e25n17).ToRound2()))
                                                {
                                                    var rowVAL2515 = erroresValidacion.NewERROR_VALIDACION_AVALUORow();
                                                    rowVAL2515["IDERROR"] = id;
                                                    rowVAL2515["TIPOERROR"] = "ESQUEMA / DOCUMENTO NO VALIDO";
                                                    rowVAL2515["DESCRIPCION"] = "e.2.5.n.15 Error de calculo.";
                                                    erroresValidacion.AddERROR_VALIDACION_AVALUORow(rowVAL2515);
                                                    id++;
                                                }
                                            }
                                        }
                                            
                                        
                                    }
                                    catch (Exception ex) { }








                                    if (erroresValidacion.Any())
                                    {
                                        ServiceAvaluos.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable newIntentoDT = new ServiceAvaluos.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable();
                                        ServiceAvaluos.DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PRow intentoFallidoRow = newIntentoDT.NewFEXAVA_INTENTOFALLIDO_PRow();

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

                                        foreach (ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow rowError in erroresValidacion)
                                        {
                                            intentoFallidoRow.ERRORES += Environment.NewLine + string.Empty + rowError.IDERROR.ToString() + "- " + rowError.TIPOERROR.ToString() + Constantes.SIMBOLO_DOSPUNTOS + rowError.DESCRIPCION.ToString();
                                        }
                                        try
                                        {
                                            newIntentoDT.Rows.Add(intentoFallidoRow);

                                            ServiceAvaluos.AvaluosClient clienteAvaluos = new ServiceAvaluos.AvaluosClient();

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

                                                ServiceAvaluos.AvaluosClient clienteAvaluos = new ServiceAvaluos.AvaluosClient();

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
                                                    RealizarRegistroAvaluo(documentoXMLComprimido, documentoXML1);
                                                    MostrarMensajeAvRegistrado();
                                                }
                                            }
                                            else //No hay errores de validación 
                                            {
                                                RealizarRegistroAvaluo(documentoXMLComprimido, documentoXML1);
                                                MostrarMensajeAvRegistrado();
                                            }
                                        }
                                        else //No hay errores de validación 
                                        {
                                            RealizarRegistroAvaluo(documentoXMLComprimido, documentoXML1);
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

                                    ServiceAvaluos.AvaluosClient clienteAvaluos = new ServiceAvaluos.AvaluosClient();

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
                                        erroresValidacion = new ServiceAvaluos.DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable();
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
                                                    RealizarRegistroAvaluoAnterior(documentoXMLComprimido,documentoXML1);
                                                    MostrarMensajeAvRegistrado();
                                                }
                                            }
                                            else //No hay errores de validación 
                                            {
                                                RealizarRegistroAvaluoAnterior(documentoXMLComprimido, documentoXML1);
                                                MostrarMensajeAvRegistrado();
                                            }
                                        }
                                        else //No hay errores de validación 
                                        {
                                            RealizarRegistroAvaluoAnterior(documentoXMLComprimido, documentoXML1);
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
                            MostrarMensajeInformativo(string.Format("{0}: <br> {1} <br><br> Error: <br> {2}", 
                                Constantes.MSJ_XML_ERROR, 
                                "El documento introducido no es un XML o no está bien formado.", error), true);
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion + Environment.NewLine + cex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
                RealizarRegistroAvaluo(documentoXML,null);
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
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion + Environment.NewLine + cex.StackTrace;
            MostrarMensajeInfoExcepcion(msj);
        }
        catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
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
    private void RealizarRegistroAvaluo(byte[] documentoXML, byte[] documentoXML1)
    {
        if (ModalConfirmacion.Visible)
        {
            confirmar_ModalPopupExtender.Hide();
        }

        string numUnico;
        ServiceAvaluos.AvaluosClient clienteAvaluos = new ServiceAvaluos.AvaluosClient();

        try
        {
            numUnico = clienteAvaluos.RegistrarAvaluo(documentoXML, Usuarios.IdPersona());
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }

        try
        {
           enviarXML(Usuarios.IdPersona(), documentoXML1, numUnico);
        }
        catch(Exception ex) { }

        ViewState.Add(Constantes.PAR_VIEWSTATE_NUMUNICOREGISTRADO, numUnico);
        LimpiarViewStateDocumentoXML();
    }

    private void enviarXML(string idUsuario, byte[] xml, string numeroUnico)
    {

       using (var wb = new WebClient())
        {
            var data = new NameValueCollection();
            data["files"] = Convert.ToBase64String(xml);
            data["idUsuario"] = idUsuario;
            data["numeroUnico"] = numeroUnico;

            string xml64 = Convert.ToBase64String(xml);
            string url = "https://ovica-servicios.finanzas.cdmx.gob.mx/avaluosNew_backend/public/WsSolucionIdeas/wsRecibeAvaluo";
            var response = wb.UploadValues(url, "POST", data);
            string responseInString = Encoding.UTF8.GetString(response);   

        }

        

    }


    

    private void RealizarRegistroAvaluoAnterior(byte[] documentoXML, byte[] documentoXML1)
    {
        if (ModalConfirmacion.Visible)
        {
            confirmar_ModalPopupExtender.Hide();
        }

        string numUnico;
        ServiceAvaluosAnterior.AvaluosClient clienteAvaluos = new ServiceAvaluosAnterior.AvaluosClient();

        try
        {
            numUnico = clienteAvaluos.RegistrarAvaluo(documentoXML, Usuarios.IdPersona());
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }

        try
        {
          enviarXML(Usuarios.IdPersona(), documentoXML1, numUnico);
        }
        catch (Exception ex) { }

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
