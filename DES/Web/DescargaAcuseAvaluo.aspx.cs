using System;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using Microsoft.Reporting.WebForms;
using ServiceAvaluos;
using ServiceDocumental;
using SIGAPred.Common.WCF;
//using SIGAPred.Documental.Services.AccesoDatos.Gestion.DocumentosDigitales;

/// <summary>
/// Clase para la descarga de avalúos.
/// </summary>
public partial class DescargaAcuseAvaluo : PageBaseAvaluos
{
    #region PAGE
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
                System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" 
                    + DateTime.Now.ToString() + " " + "DescargarAcuseAvaluo Page_Load : Constantes.PAR_IDAVALUO: " + Constantes.PAR_IDAVALUO + "\n\r");

                XmlDocument xmlAvaluo = GetXmlAvaluo(Convert.ToInt32(SIGAPred.Common.Web.WebUtils.QueryString(Constantes.PAR_IDAVALUO)));
               //your code here  
                    DataSetAcuseAva DSAcuseAva = new DataSetAcuseAva();
                    string numUnico = (SIGAPred.Common.Web.WebUtils.QueryString(Constantes.PAR_NUMUNIAVALUO)).ToString();

                System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r"
                    + DateTime.Now.ToString() + " " + "DescargarAcuseAvaluo Page_Load : numUnico : " + numUnico + "\n\r");

                //Cargar el DataSet con los valores del xml
                #region [ Identificacion ]
                DataSetAcuseAva.IdentificacionRow drId = DSAcuseAva.Identificacion.NewIdentificacionRow();
                    drId.NumUnico = numUnico;
                    DSAcuseAva.Identificacion.AddIdentificacionRow(drId);
                    #endregion

                    #region [ Cuenta Catastral ]
                    DataSetAcuseAva.CuentaCatastralRow drCat = DSAcuseAva.CuentaCatastral.NewCuentaCatastralRow();
                    drCat.Region = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CUENTACAT + "Region")).InnerText;
                    drCat.Manzana = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CUENTACAT + "Manzana")).InnerText;
                    drCat.Lote = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CUENTACAT + "Lote")).InnerText;
                    drCat.Localidad = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CUENTACAT + "Localidad")).InnerText;
                    drCat.DigitoVerificador = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CUENTACAT + "DigitoVerificador")).InnerText;
                    DSAcuseAva.CuentaCatastral.AddCuentaCatastralRow(drCat);
                    #endregion

                    #region [ Propietario ]
                    DataSetAcuseAva.PropietarioRow drProp = DSAcuseAva.Propietario.NewPropietarioRow();
                    drProp.Nombre = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "Nombre")).InnerText;
                    if ((xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "A.Paterno")) != null)
                        drProp.APaterno = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "A.Paterno")).InnerText;
                    else
                        drProp.APaterno = "";
                    if ((xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "A.Materno")) != null)
                        drProp.AMaterno = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "A.Materno")).InnerText;
                    else
                        drProp.AMaterno = "";
                    drProp.Calle = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "Calle")).InnerText;
                    drProp.NExterior = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "NumeroExterior")).InnerText;
                    drProp.NInterior = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "NumeroInterior")).InnerText;

                    #region colonia delegacion Prop.
                    drProp.Colonia = xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "Colonia").InnerText;
                    drProp.Delegacion = ObtenerNombreDelegacionPorClave(xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "Alcaldia").InnerText);
                    #endregion

                    #region Código postal Prop.
                    string CP = string.Empty;
                    if (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "CodigoPostal") != null)
                        CP = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_PROP + "CodigoPostal")).InnerText;
                    drProp.CodigoPostal = CP;
                    #endregion

                    DSAcuseAva.Propietario.AddPropietarioRow(drProp);
                    #endregion

                    #region [ Inmueble ]
                    DataSetAcuseAva.InmuebleQueSeValuaRow drInm = DSAcuseAva.InmuebleQueSeValua.NewInmuebleQueSeValuaRow();
                    drInm.Calle = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Calle")).InnerText;
                    if ((xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Manzana")) != null)
                        drInm.Manzana = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Manzana")).InnerText;
                    else
                        drInm.Manzana = "";
                    if ((xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Lote")) != null)
                        drInm.Lote = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Lote")).InnerText;
                    else
                        drInm.Lote = "";
                    drInm.NExterior = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "NumeroExterior")).InnerText;
                    drInm.NInterior = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "NumeroInterior")).InnerText;

                    #region cuentaAgua
                    string cuentaAgua = string.Empty;
                    if (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "CuentaDeAgua") != null)
                        cuentaAgua = ((xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "CuentaDeAgua")).InnerText).ToString();

                    drInm.Ca_GeoCodigo = cuentaAgua;
                    drInm.Ca_Deriv = string.Empty;
                    drInm.Ca_Toma = string.Empty;
                    drInm.Ca_DV = string.Empty;
                    #endregion

                    drInm.Colonia = xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Colonia").InnerText;
                    drInm.Delegacion = ObtenerNombreDelegacionPorClave(xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "Alcaldia").InnerText);

                    drInm.CodigoPostal = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_INM + "CodigoPostal")).InnerText;
                    DSAcuseAva.InmuebleQueSeValua.AddInmuebleQueSeValuaRow(drInm);
                    #endregion

                    #region [ Datos de la escritura ]
                    DataSetAcuseAva.DatosDeLaEscrituraRow drEsc = DSAcuseAva.DatosDeLaEscritura.NewDatosDeLaEscrituraRow();
                    if (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "NombreDelNotario") != null)
                    {
                        drEsc.NumEscritura = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "NumeroDeEscritura")).InnerText;
                        drEsc.NumVolumen = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "NumeroDeVolumen")).InnerText;
                        drEsc.FechaEscritura = Convert.ToDateTime((xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "FechaEscritura")).InnerText);
                        drEsc.NumNotaria = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "NumeroNotaria")).InnerText;
                        drEsc.NombreNotario = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "NombreDelNotario")).InnerText;
                        drEsc.DistritoJudicialNotario = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ESC + "DistritoJudicialNotario")).InnerText;
                        DSAcuseAva.DatosDeLaEscritura.AddDatosDeLaEscrituraRow(drEsc);
                    }

                    #endregion

                    #region [ Sentencia ]
                    DataSetAcuseAva.Sentencia_Row drSen = DSAcuseAva.Sentencia_.NewSentencia_Row();
                    if (xmlAvaluo.SelectSingleNode(Constantes.RUTA_SEN + "Fecha") != null)
                    {
                        drSen.Fecha = Convert.ToDateTime((xmlAvaluo.SelectSingleNode(Constantes.RUTA_SEN + "Fecha")).InnerText);
                        drSen.NumExpediente = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_SEN + "NumeroExpediente")).InnerText;
                        drSen.Juzgado = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_SEN + "Juzgado")).InnerText;
                        DSAcuseAva.Sentencia_.AddSentencia_Row(drSen);
                    }

                    #endregion

                    #region [ Contrato privado ]
                    DataSetAcuseAva.ContratoPrivadoRow drCon = DSAcuseAva.ContratoPrivado.NewContratoPrivadoRow();
                    if (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "Fecha") != null)
                    {
                        drCon.Fecha = Convert.ToDateTime((xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "Fecha")).InnerText);
                        drCon.NombreAdquiriente = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "NombreAdquirente")).InnerText;
                        drCon.Apellido1Adquiriente = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "Apellido1Adquirente")).InnerText;
                        drCon.Apellido2Adquiriente = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "Apellido2Adquirente")).InnerText;
                        drCon.NombreEnajenante = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "NombreEnajenante")).InnerText;
                        drCon.Apellido1Enajenante = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "Apellido1Enajenante")).InnerText;
                        drCon.Apellido2Enajenante = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_CON + "Apellido2Enajenante")).InnerText;
                        DSAcuseAva.ContratoPrivado.AddContratoPrivadoRow(drCon);
                    }

                    #endregion

                    #region [ Alineamiento y número oficial ]
                    DataSetAcuseAva.AliniamientoYNumeroOficialRow drAli = DSAcuseAva.AliniamientoYNumeroOficial.NewAliniamientoYNumeroOficialRow();
                    if (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ALI + "Fecha") != null)
                    {
                        drAli.Fecha = Convert.ToDateTime((xmlAvaluo.SelectSingleNode(Constantes.RUTA_ALI + "Fecha")).InnerText);
                        drAli.NumFolio = (xmlAvaluo.SelectSingleNode(Constantes.RUTA_ALI + "NumeroFolio")).InnerText;
                        DSAcuseAva.AliniamientoYNumeroOficial.AddAliniamientoYNumeroOficialRow(drAli);
                    }

                    #endregion

                    #region [ Data Source ]
                    ReportDataSource reportDataSourceProp = new ReportDataSource("DataSetAcuseAva_Propietario", (DataTable)DSAcuseAva.Propietario);
                    //reportDataSourceProp.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceProp);
                    ReportDataSource reportDataSourceCC = new ReportDataSource("DataSetAcuseAva_CuentaCatastral", (DataTable)DSAcuseAva.CuentaCatastral);
                    //reportDataSourceCC.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceCC);
                    ReportDataSource reportDataSourceInm = new ReportDataSource("DataSetAcuseAva_InmuebleQueSeValua", (DataTable)DSAcuseAva.InmuebleQueSeValua);
                    //reportDataSourceInm.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceInm);
                    ReportDataSource reportDataSourceEsc = new ReportDataSource("DataSetAcuseAva_DatosDeLaEscritura", (DataTable)DSAcuseAva.DatosDeLaEscritura);
                    //reportDataSourceEsc.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceEsc);
                    ReportDataSource reportDataSourceSen = new ReportDataSource("DataSetAcuseAva_Sentencia_", (DataTable)DSAcuseAva.Sentencia_);
                    //reportDataSourceSen.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceSen);
                    ReportDataSource reportDataSourceCon = new ReportDataSource("DataSetAcuseAva_ContratoPrivado", (DataTable)DSAcuseAva.ContratoPrivado);
                    //reportDataSourceCon.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceCon);
                    ReportDataSource reportDataSourceAli = new ReportDataSource("DataSetAcuseAva_AliniamientoYNumeroOficial", (DataTable)DSAcuseAva.AliniamientoYNumeroOficial);
                    //reportDataSourceAli.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceAli);
                    ReportDataSource reportDataSourceId = new ReportDataSource("DataSetAcuseAva_Identificacion", (DataTable)DSAcuseAva.Identificacion);
                    //  reportDataSourceId.DataSourceId = "37f45ccc-ea28-4a04-8d6a-443bc71e6682";
                    rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceId);
                    //ReportDataSource reportDataSourceProp = new ReportDataSource("DataSetAcuseAva_Propietario", DSAcuseAva.Propietario);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceProp);
                    //ReportDataSource reportDataSourceCC = new ReportDataSource("DataSetAcuseAva_CuentaCatastral", DSAcuseAva.CuentaCatastral);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceCC);
                    //ReportDataSource reportDataSourceInm = new ReportDataSource("DataSetAcuseAva_InmuebleQueSeValua", DSAcuseAva.InmuebleQueSeValua);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceInm);
                    //ReportDataSource reportDataSourceEsc = new ReportDataSource("DataSetAcuseAva_DatosDeLaEscritura", DSAcuseAva.DatosDeLaEscritura);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceEsc);
                    //ReportDataSource reportDataSourceSen = new ReportDataSource("DataSetAcuseAva_Sentencia_", DSAcuseAva.Sentencia_);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceSen);
                    //ReportDataSource reportDataSourceCon = new ReportDataSource("DataSetAcuseAva_ContratoPrivado", DSAcuseAva.ContratoPrivado);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceCon);
                    //ReportDataSource reportDataSourceAli = new ReportDataSource("DataSetAcuseAva_AliniamientoYNumeroOficial", DSAcuseAva.AliniamientoYNumeroOficial);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceAli);
                    //ReportDataSource reportDataSourceId = new ReportDataSource("DataSetAcuseAva_Identificacion", DSAcuseAva.Identificacion);
                    //rpvAvaluo.LocalReport.DataSources.Add(reportDataSourceId);

                #endregion

                rpvAvaluo.DataBind();
            }else
            {
                System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r"
                   + DateTime.Now.ToString() + " " + "DescargarAcuseAvaluo Page_Load :  POSTBACK \n\r");
            }
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
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Prerender(object sender, EventArgs e)
    {
        try
        {
            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r"
                    + DateTime.Now.ToString() + " " + "DescargarAcuseAvaluo Page_Prerender :  \n\r");
            //Se establece el botón de cancelar para los modalpopupextenders
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
    /// Mostrar mensaje información excepcion.
    /// </summary>
    /// <param name="mensaje">El mensaje que se quiere mostrar.</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString() + " " 
            + "MostrarMensajeInfoExcepcion : Exception: " + mensaje + "\n\r");
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }
    #endregion 

    /// <summary>
    /// Obtener de documental, el xml correspondiente a un avaluo concreto.
    /// </summary>
    /// <param name="idAvaluo">id del avaluo en cuestion.</param>
    /// <returns>
    /// xml del avaluo.
    /// </returns>
    private XmlDocument GetXmlAvaluo(decimal idAvaluo)
    {
        XmlDocument xmlAvaluo = new XmlDocument();
        try
        {
            byte[] avaluoByte = null;
            dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable ficheroXml = new dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable();
            dseDocumentosDigitales.DOC_DOCUMENTODIGITALDataTable docaux = new dseDocumentosDigitales.DOC_DOCUMENTODIGITALDataTable();
            dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable ficheroDocumentoDT = null;

            DocumentosDigitalesClient clienteDOC = new DocumentosDigitalesClient();

            try
            {
                ficheroDocumentoDT = clienteDOC.GetFicherosByDocumentoDigital(idAvaluo);
            }
            finally
            {
                clienteDOC.Disconnect();
            }

            if (ficheroDocumentoDT == null || !ficheroDocumentoDT.Any())
            {
                return null;
            }

            clienteDOC = new DocumentosDigitalesClient();

            try
            {
                foreach (dseDocumentosDigitales.DOC_FICHERODOCUMENTORow ficheroActual in ficheroDocumentoDT.Where(row => row.NOMBRE.Contains(Constantes.XML_FILE_EXTENSION)))
                {
                    ficheroXml = clienteDOC.GetFicheroDocumentoById((decimal)ficheroActual.IDFICHERODOCUMENTO);
                }
            }
            finally
            {
                clienteDOC.Disconnect();
            }

            if (!ficheroDocumentoDT.Any())
            {
                return null;
            }

            if (ficheroXml[0].IsBINARIODATOSNull())
            {
                return null;
            }

            avaluoByte = ficheroXml[0].BINARIODATOS;
            xmlAvaluo.Load(new MemoryStream(avaluoByte));
           
            XmlElement nuevoElemento = xmlAvaluo.CreateElement("IdAvaluo");
            nuevoElemento.SetAttribute(Constantes.XML_IDENTIFICADOR_ELEMENTOS, "a.5");
            nuevoElemento.InnerText = idAvaluo.ToString();
          
            //El xml puede ser Catastral o comercial
            if (xmlAvaluo.SelectSingleNode(Constantes.NODO_RAIZ).FirstChild.Name.Equals(Constantes.PAR_XML_AV_CATASTRAL) || 
                xmlAvaluo.SelectSingleNode(Constantes.NODO_RAIZ).FirstChild.Name.Equals(Constantes.PAR_XML_AV__COMERCIAL))
            {
                xmlAvaluo.SelectSingleNode(Constantes.RUTA_IDENTIF).AppendChild(nuevoElemento);
            }
            else
            {
                xmlAvaluo.SelectSingleNode(Constantes.RUTA_IDENTIF).AppendChild(nuevoElemento);
            }
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
        return xmlAvaluo;
    }

    /// <summary>
    /// A partir de la clave de la delegación devuelve el nombre, si no la encuentra devuelve un
    /// string vacio.
    /// </summary>
    /// <param name="claveDelegacion">La Clave de Delegacion</param>
    /// <returns>
    /// Obtiene el nombre de la delegación asociado a la clave
    /// </returns>
    private string ObtenerNombreDelegacionPorClave(string claveDelegacion)
    {
        AvaluosClient clienteAvaluos = new AvaluosClient();

        try
        {
            return clienteAvaluos.ObtenerNombreDelegacion(claveDelegacion);
        }
        finally
        {
            clienteAvaluos.Disconnect();
        }
    }
}
