// Decompiled with JetBrains decompiler
// Type: SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Avaluos
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using MyExtentions;
using Oracle.DataAccess.Client;
using SIGAPred.Common.DataAccess.Extensions;
using SIGAPred.Common.Extensions;
using SIGAPred.Common.Reflection;
using SIGAPred.Common.WCF;
using SIGAPred.Common.Xml;
using SIGAPred.Data;
using SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.Enum;
using SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.Transaccional;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaTableAdapters;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseNotariosTableAdapters;
using SIGAPred.FuentesExternas.Avaluos.Services.Enum;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Esquema;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Interfaces;
using SIGAPred.FuentesExternas.Avaluos.Services.Properties;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceAnalisisValores;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceDocumentosDigitales;
using SIGAPred.FuentesExternas.Avaluos.Services.ServicePeritosSociedades;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceRCON;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class Avaluos : IAvaluos
    {
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_AVALUOTableAdapter avaluoTA;
        private FEXAVA_AVALUO_VTableAdapter avaluo_vTA;
        private FEXAVA_CARPINTERIATableAdapter carpinteriaTA;
        private FEXAVA_CONSTRUCCIONESMERTableAdapter construccionesMercadoTA;
        private FEXAVA_DATOSPERSONASTableAdapter datosPersonalesTA;
        private FEXAVA_DATOSTERRENOSTableAdapter datosTerrenosTA;
        private FEXAVA_ELEMENTOSCONSTTableAdapter elementosConstruccionTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_ELEMENTOSEXTRATableAdapter elementosExtraTA;
        private FEXAVA_ENFOQUECOSTESCATTableAdapter enfoqueCostesCatastralTA;
        private FEXAVA_INSTALACIONHIDSANTableAdapter instalacionHidraulicaSanitariaTA;
        private FEXAVA_INVESTPRODUCTOSCOMPTableAdapter investProductosCompatiblesTA;
        private FEXAVA_OBRANEGRATableAdapter obraNegraTA;
        private FEXAVA_PUERTASYVENTANERIATableAdapter puertasYVentaneriaMetalicaTA;
        private FEXAVA_REVESTIMIENTOACABADOTableAdapter revestimientoAcabadoInteriorTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_SUPERFICIETableAdapter superficieTA;
        private FEXAVA_TERRENOMERCADOTableAdapter terrenoMercadoTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_TIPOCONSTRUCCIONTableAdapter tipoConstruccionTA;
        private FEXAVA_FOTOAVALUOTableAdapter fotoAvaluoTA;
        private FEXAVA_FOTOCOMPARABLETableAdapter fotoComparableTA;
        private FEXAVA_NOTARIOSTableAdapter notarioTA;
        private FEXAVA_INTENTOFALLIDO_PTableAdapter intentoFallidoTA;
        private FEXAVA_SENTENCIATableAdapter sentenciaTA;
        private FEXAVA_CONTRATOPRIVADOTableAdapter contratoPrivadoTA;
        private FEXAVA_ESCRITURATableAdapter escrituraTA;
        private FEXAVA_ALINEAMIENTONUMOFITableAdapter aliniamientoNumOfTA;
        private FEXAVA_FUENTEINFORMACIONLEGTableAdapter fuenteInfLegalTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_AVALUOTableAdapter fis_avaluoTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_ELEMENTOSEXTRATableAdapter fis_elementosExtraTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_SUPERFICIETableAdapter fis_superficieTA;
        private SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_TIPOCONSTRUCCIONTableAdapter fis_tipoConstruccionTA;
        private bool obligatorioPriv = true;
        private bool obligatorioComun = true;
        private bool existeWP;
        private bool existeWC;

        

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_AVALUOTableAdapter Fis_AvaluoTA
        {
            get
            {
                if (this.fis_avaluoTA == null)
                    this.fis_avaluoTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_AVALUOTableAdapter();
                return this.fis_avaluoTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_ELEMENTOSEXTRATableAdapter Fis_ElementosExtraTA
        {
            get
            {
                if (this.fis_elementosExtraTA == null)
                    this.fis_elementosExtraTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_ELEMENTOSEXTRATableAdapter();
                return this.fis_elementosExtraTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_SUPERFICIETableAdapter Fis_SuperficieTA
        {
            get
            {
                if (this.fis_superficieTA == null)
                    this.fis_superficieTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_SUPERFICIETableAdapter();
                return this.fis_superficieTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_TIPOCONSTRUCCIONTableAdapter Fis_TipoConstruccionTA
        {
            get
            {
                if (this.fis_tipoConstruccionTA == null)
                    this.fis_tipoConstruccionTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaFisTableAdapters.FEXAVA_TIPOCONSTRUCCIONTableAdapter();
                return this.fis_tipoConstruccionTA;
            }
        }

        protected FEXAVA_INTENTOFALLIDO_PTableAdapter IntentoFallidoTA
        {
            get
            {
                if (this.intentoFallidoTA == null)
                    this.intentoFallidoTA = new FEXAVA_INTENTOFALLIDO_PTableAdapter();
                return this.intentoFallidoTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_AVALUOTableAdapter AvaluoTA
        {
            get
            {
                if (this.avaluoTA == null)
                    this.avaluoTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_AVALUOTableAdapter();
                return this.avaluoTA;
            }
        }

        protected FEXAVA_AVALUO_VTableAdapter Avaluo_vTA
        {
            get
            {
                if (this.avaluo_vTA == null)
                    this.avaluo_vTA = new FEXAVA_AVALUO_VTableAdapter();
                return this.avaluo_vTA;
            }
        }

        protected FEXAVA_CARPINTERIATableAdapter CarpinteriaTA
        {
            get
            {
                if (this.carpinteriaTA == null)
                    this.carpinteriaTA = new FEXAVA_CARPINTERIATableAdapter();
                return this.carpinteriaTA;
            }
        }

        protected FEXAVA_CONSTRUCCIONESMERTableAdapter ConstruccionesMercadoTA
        {
            get
            {
                if (this.construccionesMercadoTA == null)
                    this.construccionesMercadoTA = new FEXAVA_CONSTRUCCIONESMERTableAdapter();
                return this.construccionesMercadoTA;
            }
        }

        protected FEXAVA_DATOSPERSONASTableAdapter DatosPersonalesTA
        {
            get
            {
                if (this.datosPersonalesTA == null)
                    this.datosPersonalesTA = new FEXAVA_DATOSPERSONASTableAdapter();
                return this.datosPersonalesTA;
            }
        }

        protected FEXAVA_DATOSTERRENOSTableAdapter DatosTerrenosTA
        {
            get
            {
                if (this.datosTerrenosTA == null)
                    this.datosTerrenosTA = new FEXAVA_DATOSTERRENOSTableAdapter();
                return this.datosTerrenosTA;
            }
        }

        protected FEXAVA_ELEMENTOSCONSTTableAdapter ElementosConstruccionTA
        {
            get
            {
                if (this.elementosConstruccionTA == null)
                    this.elementosConstruccionTA = new FEXAVA_ELEMENTOSCONSTTableAdapter();
                return this.elementosConstruccionTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_ELEMENTOSEXTRATableAdapter ElementosExtraTA
        {
            get
            {
                if (this.elementosExtraTA == null)
                    this.elementosExtraTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_ELEMENTOSEXTRATableAdapter();
                return this.elementosExtraTA;
            }
        }

        protected FEXAVA_ENFOQUECOSTESCATTableAdapter EnfoqueCostesCatastralTA
        {
            get
            {
                if (this.enfoqueCostesCatastralTA == null)
                    this.enfoqueCostesCatastralTA = new FEXAVA_ENFOQUECOSTESCATTableAdapter();
                return this.enfoqueCostesCatastralTA;
            }
        }

        protected FEXAVA_INSTALACIONHIDSANTableAdapter InstalacionHidraulicaSanitariaTA
        {
            get
            {
                if (this.instalacionHidraulicaSanitariaTA == null)
                    this.instalacionHidraulicaSanitariaTA = new FEXAVA_INSTALACIONHIDSANTableAdapter();
                return this.instalacionHidraulicaSanitariaTA;
            }
        }

        protected FEXAVA_INVESTPRODUCTOSCOMPTableAdapter InvestProductosCompatiblesTA
        {
            get
            {
                if (this.investProductosCompatiblesTA == null)
                    this.investProductosCompatiblesTA = new FEXAVA_INVESTPRODUCTOSCOMPTableAdapter();
                return this.investProductosCompatiblesTA;
            }
        }

        protected FEXAVA_OBRANEGRATableAdapter ObraNegraTA
        {
            get
            {
                if (this.obraNegraTA == null)
                    this.obraNegraTA = new FEXAVA_OBRANEGRATableAdapter();
                return this.obraNegraTA;
            }
        }

        protected FEXAVA_PUERTASYVENTANERIATableAdapter PuertasYVentaneriaMetalicaTA
        {
            get
            {
                if (this.puertasYVentaneriaMetalicaTA == null)
                    this.puertasYVentaneriaMetalicaTA = new FEXAVA_PUERTASYVENTANERIATableAdapter();
                return this.puertasYVentaneriaMetalicaTA;
            }
        }

        protected FEXAVA_REVESTIMIENTOACABADOTableAdapter RevestimientoAcabadoInteriorTA
        {
            get
            {
                if (this.revestimientoAcabadoInteriorTA == null)
                    this.revestimientoAcabadoInteriorTA = new FEXAVA_REVESTIMIENTOACABADOTableAdapter();
                return this.revestimientoAcabadoInteriorTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_SUPERFICIETableAdapter SuperficieTA
        {
            get
            {
                if (this.superficieTA == null)
                    this.superficieTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_SUPERFICIETableAdapter();
                return this.superficieTA;
            }
        }

        protected FEXAVA_TERRENOMERCADOTableAdapter TerrenoMercadoTA
        {
            get
            {
                if (this.terrenoMercadoTA == null)
                    this.terrenoMercadoTA = new FEXAVA_TERRENOMERCADOTableAdapter();
                return this.terrenoMercadoTA;
            }
        }

        protected SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_TIPOCONSTRUCCIONTableAdapter TipoConstruccionTA
        {
            get
            {
                if (this.tipoConstruccionTA == null)
                    this.tipoConstruccionTA = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoMantTableAdapters.FEXAVA_TIPOCONSTRUCCIONTableAdapter();
                return this.tipoConstruccionTA;
            }
        }

        protected FEXAVA_FOTOAVALUOTableAdapter FotoAvaluoTA
        {
            get
            {
                if (this.fotoAvaluoTA == null)
                    this.fotoAvaluoTA = new FEXAVA_FOTOAVALUOTableAdapter();
                return this.fotoAvaluoTA;
            }
        }

        protected FEXAVA_FOTOCOMPARABLETableAdapter FotoComparableTA
        {
            get
            {
                if (this.fotoComparableTA == null)
                    this.fotoComparableTA = new FEXAVA_FOTOCOMPARABLETableAdapter();
                return this.fotoComparableTA;
            }
        }

        protected FEXAVA_NOTARIOSTableAdapter NotarioTA
        {
            get
            {
                if (this.notarioTA == null)
                    this.notarioTA = new FEXAVA_NOTARIOSTableAdapter();
                return this.notarioTA;
            }
        }

        protected FEXAVA_FUENTEINFORMACIONLEGTableAdapter FuenteInfLegalTA
        {
            get
            {
                if (this.fuenteInfLegalTA == null)
                    this.fuenteInfLegalTA = new FEXAVA_FUENTEINFORMACIONLEGTableAdapter();
                return this.fuenteInfLegalTA;
            }
        }

        protected FEXAVA_SENTENCIATableAdapter SentenciaTA
        {
            get
            {
                if (this.sentenciaTA == null)
                    this.sentenciaTA = new FEXAVA_SENTENCIATableAdapter();
                return this.sentenciaTA;
            }
        }

        protected FEXAVA_CONTRATOPRIVADOTableAdapter ContratoPrivadoTA
        {
            get
            {
                if (this.contratoPrivadoTA == null)
                    this.contratoPrivadoTA = new FEXAVA_CONTRATOPRIVADOTableAdapter();
                return this.contratoPrivadoTA;
            }
        }

        protected FEXAVA_ESCRITURATableAdapter EscrituraTA
        {
            get
            {
                if (this.escrituraTA == null)
                    this.escrituraTA = new FEXAVA_ESCRITURATableAdapter();
                return this.escrituraTA;
            }
        }

        protected FEXAVA_ALINEAMIENTONUMOFITableAdapter AliniamientoNumOfTA
        {
            get
            {
                if (this.aliniamientoNumOfTA == null)
                    this.aliniamientoNumOfTA = new FEXAVA_ALINEAMIENTONUMOFITableAdapter();
                return this.aliniamientoNumOfTA;
            }
        }

        public string Test() => "OK";

        public string RegistrarAvaluo(byte[] xmlBytesComprimido, string idpersona)
        {
            string pasos = "";
            try
            {
                
                XElementUtils.CultureInfoConfigured = new CultureInfo("es-MX");
                StringBuilder stringBuilder = new StringBuilder();
                byte[] buffer = SIGAPred.Common.Compresion.Compresion.Descomprimir(xmlBytesComprimido);
                XmlDocument xmlAvaluo = new XmlDocument();
                pasos = "Paso 1";
                xmlAvaluo.Load((Stream)new MemoryStream(buffer));
                pasos = "Paso 2";
                AvaluosUtils.EliminarEspaciosBlancos(ref xmlAvaluo);
                pasos = "Paso 3";
                return this.ObtenerNumeroUnicoAvaluo(this.GuardarAvaluo(xmlAvaluo, idpersona));
                
            }
            catch (TimeoutException ex)
            {
                log("RegistrarAvaluo TimeoutException "+pasos+" | ", ex.Message,ex.StackTrace);
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("No se ha recibido respuesta de la aplicación en el tiempo establecido"));
            }
            catch (XmlException ex)
            {
                log("RegistrarAvaluo XmlException " + pasos + " | ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("Error al leer archivo XML. Formato de fichero incorrecto"));
            }
            catch (FaultException<AvaluosException> ex)
            {
                log("RegistrarAvaluo FaultException " + pasos + " | ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw;
            }
            catch (Exception ex)
            {
                log("RegistrarAvaluo Exception " + pasos + " | ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex));
            }
        }

        public void RegistrarAvaluoEspecial(byte[] xmlBytesComprimido)
        {
            try
            {
                byte[] buffer = SIGAPred.Common.Compresion.Compresion.Descomprimir(xmlBytesComprimido);
                DateTime dateTime = new DateTime();
                XmlDocument xmlAvaluo = new XmlDocument();
                xmlAvaluo.Load((Stream)new MemoryStream(buffer));
                AvaluosUtils.EliminarEspaciosBlancos(ref xmlAvaluo);
                XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
                string cuentaCat = (string)null;
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "b");
                if (xelements1.IsFull())
                {
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(xelements1.First<XElement>(), "b.3.10.1");
                    if (xelements2.IsFull())
                        cuentaCat = xelements2.ToStringXElement() + "-";
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(xelements1.First<XElement>(), "b.3.10.2");
                    if (xelements3.IsFull())
                        cuentaCat = cuentaCat + xelements3.ToStringXElement() + "-";
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(xelements1.First<XElement>(), "b.3.10.3");
                    if (xelements4.IsFull())
                        cuentaCat = cuentaCat + xelements4.ToStringXElement() + "-";
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements1.First<XElement>(), "b.3.10.4");
                    if (xelements5.IsFull())
                        cuentaCat += xelements5.ToStringXElement();
                }
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "a");
                DateTime date;
                if (xelements6.IsFull())
                {
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(xelements6.First<XElement>(), "a.2");
                    if (xelements2.IsFull())
                    {
                        try
                        {
                            date = xelements2.First<XElement>().Value.To<DateTime>();
                        }
                        catch(Exception ex)
                        {
                            log("RegistrarAvaluoEspecial Exception", ex.Message, ex.StackTrace);
                            date = DateTime.Now.Date;
                        }
                    }
                    else
                        date = DateTime.Now.Date;
                }
                else
                    date = DateTime.Now.Date;
                byte[] bytes = new UTF8Encoding().GetBytes(root.ToString());
                SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.AltasDocumentos altasDocumentos = new SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.AltasDocumentos();
                if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                {
                    altasDocumentos.InsertAvaluoComercial(bytes, AvaluosUtils.CrearNombreDocumentoAv("Comercial", cuentaCat), AvaluosUtils.CrearDescripcionDocumentoAv(cuentaCat), date, new Decimal?());
                }
                else
                {
                    if (root.Descendants((XName)"Catastral").Count<XElement>() <= 0)
                        return;
                    altasDocumentos.InsertAvaluoCatastral(bytes, AvaluosUtils.CrearNombreDocumentoAv("Catastral", cuentaCat), AvaluosUtils.CrearDescripcionDocumentoAv(cuentaCat), date, new Decimal?());
                }
            }
            catch (TimeoutException ex)
            {
                log("RegistrarAvaluoEspecial TimeoutException", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("No se ha recibido respuesta de la aplicación en el tiempo establecido"));
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                log("RegistrarAvaluoEspecial FaultException", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw ex;
            }
            catch (XmlException ex)
            {
                log("RegistrarAvaluoEspecial XmlException", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("Error al leer archivo XML. Formato de fichero incorrecto"));
            }
            catch (Exception ex)
            {
                log("RegistrarAvaluoEspecial Exception", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public void CambiarEstadoAvaluo(Decimal idavaluos, Decimal codEstadoAvaluoNuevo)
        {
            try
            {
                this.AvaluoTA.ModificarEstadoAvaluo(new Decimal?(idavaluos), new Decimal?(codEstadoAvaluoNuevo.ToDecimal()));
            }
            catch (Exception ex)
            {
                log("CambiarEstadoAvaluo Exception", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public void AsignarNotarioAvaluo(Decimal idavaluos, Decimal idNotario)
        {
            try
            {
                this.AvaluoTA.AsignarNotarioAvaluo(new Decimal?(idNotario), new Decimal?(idavaluos));
            }
            catch (Exception ex)
            {
                log("AsignarNotarioAvaluo Exception", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluosCatConsulta ObtenerCatalogos() => ApplicationCache.DseCatalogosConsulta;

        public DseAvaluoConsulta ObtenerAvaluo(int idAvaluo)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                this.Avaluo_vTA.FillByIdAvaluo(dseAvaluoConsulta.FEXAVA_AVALUO_V, new Decimal?((Decimal)idAvaluo), out C_AVALUOS);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("ObtenerAvaluo Exception", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluoPorIdAvaluoEstadoVigencia(
          string idAvaluo,
          int codestado,
          string estavigente,
          int idpersonaperito,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                this.Avaluo_vTA.FillByIdAvaluos_Vigencia_Estado(dseAvaluoConsulta.FEXAVA_AVALUO_V, idAvaluo, new Decimal?((Decimal)codestado), estavigente, new Decimal?((Decimal)idpersonaperito), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("Exception ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluoPorVigEstTodosPeritosSoci(
          string idAvaluo,
          int codestado,
          string estavigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                this.Avaluo_vTA.FillByIdAdeudo_VigEstTodosPeritosSoci(dseAvaluoConsulta.FEXAVA_AVALUO_V, idAvaluo, new Decimal?((Decimal)codestado), estavigente, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("Exception ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluoPorIdAvaluos_Soci_Vigencia_Estado(
          string idAvaluo,
          int codestado,
          string estavigente,
          int idpersonasoci,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                this.Avaluo_vTA.FillByIdAvaluos_Soci_Vigencia_Estado(dseAvaluoConsulta.FEXAVA_AVALUO_V, idAvaluo, new Decimal?((Decimal)codestado), estavigente, new Decimal?((Decimal)idpersonasoci), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("Exception ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorProximidad(
          int idAvaluo,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByProximidad(dseAvaluoConsulta.FEXAVA_AVALUO_V, new Decimal?((Decimal)idAvaluo), new Decimal?((Decimal)codestado), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                dseAvaluoConsulta.SchemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("Exception ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorFechaNotario(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          int idNotario,
          string registro,
          string vigente,
          string numValuo,
          string idAvaluo,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string sortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                log("ObtenerAvaluosPorFechaNotario", "idNotario:",idNotario.ToString());
                /*log("ObtenerAvaluosPorFechaNotario", 
                    "fechaInicio:"+ fechaInicio.ToString() + ","+
                    "fechaFin:" + fechaFin.ToString() + "," +
                    "registro:" + registro.ToString() + "," +
                    "vigente:" + vigente.ToString() + "," +
                    "numValuo:" + numValuo.ToString() + "," +
                    "idAvaluo:" + idAvaluo.ToString() 
                    ,"");*/
                this.Avaluo_vTA.FillByFecha_Notario(dseAvaluoConsulta.FEXAVA_AVALUO_V, fechaInicio, fechaFin, new Decimal?((Decimal)idNotario), registro, vigente, numValuo, idAvaluo, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), sortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                {
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                    log("ObtenerAvaluosPorFechaNotario", "rowsTotal:", rowsTotal.ToString());
                }
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("Exception ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorFechaNotarioSF(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          int idNotario,
          string registro,
          string vigente,
          string numAvaluo,
          string idAvaluo,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string sortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByFecha_NotarioSF(dseAvaluoConsulta.FEXAVA_AVALUO_V, fechaInicio, fechaFin, new Decimal?((Decimal)idNotario), registro, vigente, numAvaluo, idAvaluo, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), sortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                log("Exception ", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorRegistroPeritoNotario(
          string registro,
          int idNotario,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByRegistroPerito_Notario(dseAvaluoConsulta.FEXAVA_AVALUO_V, registro, new Decimal?((Decimal)idNotario), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorFechaPeritoVigenciaEstado(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          int idPerito,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByFecha_Perito_Vigencia_Estado(dseAvaluoConsulta.FEXAVA_AVALUO_V, fechaInicio, fechaFin, new Decimal?((Decimal)idPerito), vigente, new Decimal?((Decimal)new int?(codestado).Value), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorEstadoPerito(
          Decimal idPersona,
          int codEstado,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByEstado_Perito(dseAvaluoConsulta.FEXAVA_AVALUO_V, new Decimal?(idPersona), new Decimal?((Decimal)codEstado), vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorNumValuoVigEstado(
          string numValuo,
          string registroPerito,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByNumAvaluoVigEstado(dseAvaluoConsulta.FEXAVA_AVALUO_V, numValuo, registroPerito, new Decimal?((Decimal)codestado), vigente, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorNumValuoPerito_EstadoVig(
          string numValuo,
          Decimal idPersona,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByNumAvaluoPerito_EstadoVig(dseAvaluoConsulta.FEXAVA_AVALUO_V, numValuo, new Decimal?(idPersona), vigente, new Decimal?((Decimal)codestado), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorNumAvaluoSoci(
          string numValuo,
          string registroPerito,
          Decimal idSoci,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByNumAvaluo_idSoci(dseAvaluoConsulta.FEXAVA_AVALUO_V, numValuo, registroPerito, new Decimal?(idSoci), vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorNumAvaluoSoci_EstadoVig(
          string numValuo,
          string registroPerito,
          Decimal idSoci,
          string vigente,
          Decimal codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByNumAvaluo_idSociEstadoVig(dseAvaluoConsulta.FEXAVA_AVALUO_V, numValuo, registroPerito, new Decimal?(idSoci), new Decimal?(codestado), vigente, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorFechaSoci(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          Decimal idSoci,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByFecha_idSoci(dseAvaluoConsulta.FEXAVA_AVALUO_V, fechaInicio, fechaFin, new Decimal?(idSoci), vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorFechaSoci_EstadoVig(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          Decimal idSoci,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByFecha_idSociEstadoVig(dseAvaluoConsulta.FEXAVA_AVALUO_V, fechaInicio, fechaFin, new Decimal?(idSoci), vigente, new Decimal?((Decimal)codestado), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaSoci(
          string cuentaCatastral,
          Decimal idSoci,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta_idSoci(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?(idSoci), vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaVigenciaEstado(
          string cuentaCatastral,
          int codestado,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuentaVigenciaEstado(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, vigente, new Decimal?((Decimal)codestado), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaSociEstadoVig(
          string cuentaCatastral,
          Decimal idSoci,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta_idsociEstadoVig(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?(idSoci), vigente, new Decimal?((Decimal)new int?(codestado).Value), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorEstadoSoci(
          Decimal idSoci,
          int codEstado,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByEstado_idSoci(dseAvaluoConsulta.FEXAVA_AVALUO_V, new Decimal?(idSoci), new Decimal?((Decimal)codEstado), vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorFechaEstado(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          string vigente,
          int codEstado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByFechaEstado(dseAvaluoConsulta.FEXAVA_AVALUO_V, fechaInicio, fechaFin, vigente, new Decimal?((Decimal)codEstado), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralNotario(
          string cuentaCatastral,
          int idNotario,
          string registro,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta_Notario(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?((Decimal)idNotario), registro, vigente, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralNotarioSF(
          string cuentaCatastral,
          int idNotario,
          string registro,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta_NotarioSF(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?((Decimal)idNotario), registro, vigente, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaObtnNum(
          string cuentaCatastral,
          int idNotario,
          string registro,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuentaObtNum(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?((Decimal)idNotario), registro, vigente, new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastral(
          string cuentaCatastral,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorProximidadPerito(
          int idAvaluo,
          int idPerito,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByProximidad_Perito(dseAvaluoConsulta.FEXAVA_AVALUO_V, new Decimal?((Decimal)idAvaluo), new Decimal?((Decimal)idPerito), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                dseAvaluoConsulta.SchemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorProximidadSociedad(
          int idAvaluo,
          int idSociedad,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByProximidad_Soci(dseAvaluoConsulta.FEXAVA_AVALUO_V, new Decimal?((Decimal)idAvaluo), new Decimal?((Decimal)idSociedad), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                dseAvaluoConsulta.SchemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralPerito(
          string cuentaCatastral,
          string idPerito,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta_IdPer(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?(Convert.ToDecimal(idPerito)), vigente.ToOracleCharSNFromBoolean(), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralPeritoVigenciaEstado(
          string cuentaCatastral,
          string idPerito,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_AVALUOS = (object)null;
                DseAvaluoConsulta dseAvaluoConsulta = new DseAvaluoConsulta();
                rowsTotal = 0;
                this.Avaluo_vTA.FillByCuenta_IdPerEstadoVig(dseAvaluoConsulta.FEXAVA_AVALUO_V, cuentaCatastral, new Decimal?(Convert.ToDecimal(idPerito)), vigente, new Decimal?((Decimal)new int?(codestado).Value), new Decimal?((Decimal)pageSize), new Decimal?((Decimal)indice), SortExpression, out C_AVALUOS);
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    rowsTotal = Convert.ToInt32(dseAvaluoConsulta.FEXAVA_AVALUO_V[0].ROWS_TOTAL);
                return dseAvaluoConsulta;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public string ObtenerRegistroValuador(Decimal idValuador)
        {
            try
            {
                PeritosSociedadesClient proxy = new PeritosSociedadesClient();
                DsePeritosSociedades peritosSociedades = (DsePeritosSociedades)null;
                try
                {
                    peritosSociedades = proxy.GetPerito(idValuador, false);
                }
                finally
                {
                    proxy.Disconnect();
                }
                return peritosSociedades.Perito.Any<DsePeritosSociedades.PeritoRow>() ? peritosSociedades.Perito[0].REGISTRO : string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public string ObtenerRegistroSociedad(Decimal idSociedad)
        {
            try
            {
                PeritosSociedadesClient proxy = new PeritosSociedadesClient();
                DsePeritosSociedades peritosSociedades = (DsePeritosSociedades)null;
                try
                {
                    peritosSociedades = proxy.GetSociedadValuacion(idSociedad, false);
                }
                finally
                {
                    proxy.Disconnect();
                }
                return peritosSociedades.SociedadValuacion.Any<DsePeritosSociedades.SociedadValuacionRow>() ? peritosSociedades.SociedadValuacion[0].REGISTRO : string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public string ObtenerNumeroUnicoAvaluo(Decimal idAvaluo)
        {
            try
            {
                string V_NUMUNICO;
                this.Avaluo_vTA.ObtenerNumeroUnicoByIdAvaluo(new Decimal?(idAvaluo), out V_NUMUNICO);
                return V_NUMUNICO.Trim();
            }
            catch (Exception ex)
            {
                log("ObtenerNumeroUnicoAvaluo | idAvaluo: " +idAvaluo+" | ", ex.Message,ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsultaFis ObtenerDatosAvaluoCatastral(Decimal idAvaluo)
        {
            try
            {
                DseAvaluoConsultaFis avaluoConsultaFis = new DseAvaluoConsultaFis();
                object C_AVALUOS = (object)null;
                this.Fis_AvaluoTA.FillById(avaluoConsultaFis.FEXAVA_AVALUO, new Decimal?(idAvaluo), out C_AVALUOS);
                this.Fis_ElementosExtraTA.FillByAvaluo(avaluoConsultaFis.FEXAVA_ELEMENTOSEXTRA, new Decimal?(idAvaluo), out C_AVALUOS);
                this.Fis_SuperficieTA.FillByAvaluo(avaluoConsultaFis.FEXAVA_SUPERFICIE, new Decimal?(idAvaluo), out C_AVALUOS);
                this.Fis_TipoConstruccionTA.FillByAvaluo(avaluoConsultaFis.FEXAVA_TIPOCONSTRUCCION, new Decimal?(idAvaluo), out C_AVALUOS);
                return avaluoConsultaFis;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseNotarios.FEXAVA_NOTARIOSDataTable GetNotarios()
        {
            try
            {
                object C_NOTARIOS = (object)null;
                return this.NotarioTA.GetData(out C_NOTARIOS);
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public byte[] GenerarAvaluo(
          string region,
          string manzana,
          string lote,
          string unidad,
          int tipoAvaluo,
          Decimal idValuador,
          string fun,
          string digito)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                Avaluo avaluo = new Avaluo();
                List<string> stringList = new List<string>();
                XmlDocument xmlDocument = new XmlDocument();
                UTF8Encoding utF8Encoding = new UTF8Encoding();
                switch (tipoAvaluo)
                {
                    case 1:
                        avaluo.Item = (object)new AvaluoComercial();
                        break;
                    case 2:
                        avaluo.Item = (object)new AvaluoCatastral();
                        break;
                }
                List<string> allClasses = ReflectionUtils.GetAllClasses(avaluo.GetType().Assembly, "SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Esquema");
                this.InvokeAvaluo(avaluo.Item, allClasses);
                switch (tipoAvaluo - 1)
                {
                    case 0:
                        ((AvaluoComercial)avaluo.Item).Terreno.SuperficieDelTerreno = new TerrenoSuperficieDelTerreno[1]
                        {
              new TerrenoSuperficieDelTerreno()
                        };
                        ((IEnumerable<TerrenoSuperficieDelTerreno>)((AvaluoComercial)avaluo.Item).Terreno.SuperficieDelTerreno).First<TerrenoSuperficieDelTerreno>().ClaveDeAreaDeValor = 
                            new SUBClaveDeAreaDeValor();
                        break;
                    case 1:
                        ((AvaluoCatastral)avaluo.Item).Terreno.SuperficieDelTerreno = new TerrenoSuperficieDelTerreno[1]
                        {
              new TerrenoSuperficieDelTerreno()
                        };
                        ((IEnumerable<TerrenoSuperficieDelTerreno>)((AvaluoCatastral)avaluo.Item).Terreno.SuperficieDelTerreno).First<TerrenoSuperficieDelTerreno>().ClaveDeAreaDeValor = 
                            new SUBClaveDeAreaDeValor();
                        break;
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Avaluo));
                XmlTextWriter xmlTextWriter = new XmlTextWriter((Stream)memoryStream, Encoding.UTF8);
                xmlSerializer.Serialize((XmlWriter)xmlTextWriter, (object)avaluo);
                MemoryStream baseStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlDocument.Load((Stream)new MemoryStream(baseStream.ToArray()));
                XElement xelement = XDocument.Parse(xmlDocument.InnerXml).Root;
                IEnumerable<XElement> source1 = XmlUtils.XmlSearchById(xelement, "a.2");
                if (source1.Count<XElement>() > 0)
                {
                    DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    source1.First<XElement>().SetValue((object)dateTime.ToShortDateString());
                }
                if ("FunPerito" == fun)
                {
                    string str = this.ObtenerRegistroValuador(idValuador);
                    IEnumerable<XElement> source2 = XmlUtils.XmlSearchById(xelement, "a.3");
                    if (source2.Count<XElement>() > 0)
                        source2.First<XElement>().SetValue((object)str);
                }
                if ("FunSociedad" == fun)
                {
                    string str = this.ObtenerRegistroSociedad(idValuador);
                    IEnumerable<XElement> source2 = XmlUtils.XmlSearchById(xelement, "a.4");
                    if (source2.Count<XElement>() > 0)
                        source2.First<XElement>().SetValue((object)str);
                }
                IEnumerable<XElement> source3 = XmlUtils.XmlSearchById(xelement, "b.3.4");
                if (source3.Count<XElement>() > 0)
                    source3.First<XElement>().SetValue((object)manzana);
                IEnumerable<XElement> source4 = XmlUtils.XmlSearchById(xelement, "b.3.5");
                if (source4.Count<XElement>() > 0)
                    source4.First<XElement>().SetValue((object)lote);
                IEnumerable<XElement> source5 = XmlUtils.XmlSearchById(xelement, "b.3.10.1");
                if (source5.Count<XElement>() > 0)
                    source5.First<XElement>().SetValue((object)region);
                IEnumerable<XElement> source6 = XmlUtils.XmlSearchById(xelement, "b.3.10.2");
                if (source6.Count<XElement>() > 0)
                    source6.First<XElement>().SetValue((object)manzana);
                IEnumerable<XElement> source7 = XmlUtils.XmlSearchById(xelement, "b.3.10.3");
                if (source7.Count<XElement>() > 0)
                    source7.First<XElement>().SetValue((object)lote);
                IEnumerable<XElement> source8 = XmlUtils.XmlSearchById(xelement, "b.3.10.4");
                if (source8.Count<XElement>() > 0)
                    source8.First<XElement>().SetValue((object)unidad);
                IEnumerable<XElement> source9 = XmlUtils.XmlSearchById(xelement, "b.3.10.5");
                if (source9.Count<XElement>() > 0)
                    source9.First<XElement>().SetValue((object)digito);
                IEnumerable<XElement> source10 = XmlUtils.XmlSearchById(xelement, "b.6");
                if (source10.Count<XElement>() > 0)
                    source10.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source11 = XmlUtils.XmlSearchById(xelement, "c.1");
                if (source11.Count<XElement>() > 0)
                    source11.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source12 = XmlUtils.XmlSearchById(xelement, "c.2");
                if (source12.Count<XElement>() > 0)
                    source12.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source13 = XmlUtils.XmlSearchById(xelement, "c.3");
                if (source13.Count<XElement>() > 0)
                    source13.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source14 = XmlUtils.XmlSearchById(xelement, "c.4");
                if (source14.Count<XElement>() > 0)
                    source14.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source15 = XmlUtils.XmlSearchById(xelement, "c.5");
                if (source15.Count<XElement>() > 0)
                    source15.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source16 = XmlUtils.XmlSearchById(xelement, "c.6.3");
                if (source16.Count<XElement>() > 0)
                    source16.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source17 = XmlUtils.XmlSearchById(xelement, "c.6.2");
                if (source17.Count<XElement>() > 0)
                    source17.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source18 = XmlUtils.XmlSearchById(xelement, "c.6.4");
                if (source18.Count<XElement>() > 0)
                    source18.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source19 = XmlUtils.XmlSearchById(xelement, "c.8.1");
                if (source19.Count<XElement>() > 0)
                    source19.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source20 = XmlUtils.XmlSearchById(xelement, "c.8.2");
                if (source20.Count<XElement>() > 0)
                    source20.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source21 = XmlUtils.XmlSearchById(xelement, "c.8.3");
                if (source21.Count<XElement>() > 0)
                    source21.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source22 = XmlUtils.XmlSearchById(xelement, "c.8.4");
                if (source22.Count<XElement>() > 0)
                    source22.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source23 = XmlUtils.XmlSearchById(xelement, "c.8.5");
                if (source23.Count<XElement>() > 0)
                    source23.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source24 = XmlUtils.XmlSearchById(xelement, "c.8.7");
                if (source24.Count<XElement>() > 0)
                    source24.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source25 = XmlUtils.XmlSearchById(xelement, "c.8.8");
                if (source25.Count<XElement>() > 0)
                    source25.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source26 = XmlUtils.XmlSearchById(xelement, "c.8.9");
                if (source26.Count<XElement>() > 0)
                    source26.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source27 = XmlUtils.XmlSearchById(xelement, "c.8.10");
                if (source27.Count<XElement>() > 0)
                    source27.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source28 = XmlUtils.XmlSearchById(xelement, "c.8.11");
                if (source28.Count<XElement>() > 0)
                    source28.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source29 = XmlUtils.XmlSearchById(xelement, "c.8.12");
                if (source29.Count<XElement>() > 0)
                    source29.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source30 = XmlUtils.XmlSearchById(xelement, "c.8.13");
                if (source30.Count<XElement>() > 0)
                    source30.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source31 = XmlUtils.XmlSearchById(xelement, "c.8.14");
                if (source31.Count<XElement>() > 0)
                    source31.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source32 = XmlUtils.XmlSearchById(xelement, "c.8.15");
                if (source32.Count<XElement>() > 0)
                    source32.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source33 = XmlUtils.XmlSearchById(xelement, "c.8.16");
                if (source33.Count<XElement>() > 0)
                    source33.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source34 = XmlUtils.XmlSearchById(xelement, "c.8.17");
                if (source34.Count<XElement>() > 0)
                    source34.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source35 = XmlUtils.XmlSearchById(xelement, "c.8.18");
                if (source35.Count<XElement>() > 0)
                    source35.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source36 = XmlUtils.XmlSearchById(xelement, "c.8.19");
                if (source36.Count<XElement>() > 0)
                    source36.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source37 = XmlUtils.XmlSearchById(xelement, "c.8.20");
                if (source37.Count<XElement>() > 0)
                    source37.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source38 = XmlUtils.XmlSearchById(xelement, "c.8.21");
                if (source38.Count<XElement>() > 0)
                    source38.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source39 = XmlUtils.XmlSearchById(xelement, "c.8.22");
                if (source39.Count<XElement>() > 0)
                    source39.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source40 = XmlUtils.XmlSearchById(xelement, "c.8.23");
                if (source40.Count<XElement>() > 0)
                    source40.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source41 = XmlUtils.XmlSearchById(xelement, "c.8.24");
                if (source41.Count<XElement>() > 0)
                    source41.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source42 = XmlUtils.XmlSearchById(xelement, "c.8.25");
                if (source42.Count<XElement>() > 0)
                    source42.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source43 = XmlUtils.XmlSearchById(xelement, "c.8.26");
                if (source43.Count<XElement>() > 0)
                    source43.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source44 = XmlUtils.XmlSearchById(xelement, "c.8.27");
                if (source44.Count<XElement>() > 0)
                    source44.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source45 = XmlUtils.XmlSearchById(xelement, "c.8.28");
                if (source45.Count<XElement>() > 0)
                    source45.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source46 = XmlUtils.XmlSearchById(xelement, "c.8.29");
                if (source46.Count<XElement>() > 0)
                    source46.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source47 = XmlUtils.XmlSearchById(xelement, "c.8.30");
                if (source47.Count<XElement>() > 0)
                    source47.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source48 = XmlUtils.XmlSearchById(xelement, "c.8.31");
                if (source48.Count<XElement>() > 0)
                    source48.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source49 = XmlUtils.XmlSearchById(xelement, "c.8.32");
                if (source49.Count<XElement>() > 0)
                    source49.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source50 = XmlUtils.XmlSearchById(xelement, "c.8.33");
                if (source50.Count<XElement>() > 0)
                    source50.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source51 = XmlUtils.XmlSearchById(xelement, "i.6");
                if (source51.Count<XElement>() > 0)
                    source51.ElementAt<XElement>(0).SetValue((object)"");
                IEnumerable<XElement> source52 = XmlUtils.XmlSearchById(xelement, "k.1");
                if (source52.Count<XElement>() > 0)
                    source52.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source53 = XmlUtils.XmlSearchById(xelement, "k.2.1");
                if (source53.Count<XElement>() > 0)
                    source53.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source54 = XmlUtils.XmlSearchById(xelement, "k.2.2");
                if (source54.Count<XElement>() > 0)
                    source54.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source55 = XmlUtils.XmlSearchById(xelement, "k.2.3");
                if (source55.Count<XElement>() > 0)
                    source55.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source56 = XmlUtils.XmlSearchById(xelement, "k.2.4");
                if (source56.Count<XElement>() > 0)
                    source56.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source57 = XmlUtils.XmlSearchById(xelement, "k.2.5");
                if (source57.Count<XElement>() > 0)
                    source57.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source58 = XmlUtils.XmlSearchById(xelement, "k.2.7");
                if (source58.Count<XElement>() > 0)
                    source58.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source59 = XmlUtils.XmlSearchById(xelement, "k.2.6");
                if (source59.Count<XElement>() > 0)
                    source59.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source60 = XmlUtils.XmlSearchById(xelement, "k.2.8");
                if (source60.Count<XElement>() > 0)
                    source60.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source61 = XmlUtils.XmlSearchById(xelement, "k.2.9");
                if (source61.Count<XElement>() > 0)
                    source61.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source62 = XmlUtils.XmlSearchById(xelement, "k.2.10");
                if (source62.Count<XElement>() > 0)
                    source62.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source63 = XmlUtils.XmlSearchById(xelement, "k.2.11");
                if (source63.Count<XElement>() > 0)
                    source63.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source64 = XmlUtils.XmlSearchById(xelement, "k.2.12");
                if (source64.Count<XElement>() > 0)
                    source64.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source65 = XmlUtils.XmlSearchById(xelement, "k.2.13");
                if (source65.Count<XElement>() > 0)
                    source65.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source66 = XmlUtils.XmlSearchById(xelement, "k.3");
                if (source66.Count<XElement>() > 0)
                    source66.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source67 = XmlUtils.XmlSearchById(xelement, "k.4");
                if (source67.Count<XElement>() > 0)
                    source67.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source68 = XmlUtils.XmlSearchById(xelement, "k.5");
                if (source68.Count<XElement>() > 0)
                    source68.ElementAt<XElement>(0).SetValue((object)"");
                IEnumerable<XElement> source69 = XmlUtils.XmlSearchById(xelement, "o.1");
                if (source69.Count<XElement>() > 0)
                    source69.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source70 = XmlUtils.XmlSearchById(xelement, "o.2");
                if (source70.Count<XElement>() > 0)
                    source70.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source71 = XmlUtils.XmlSearchById(xelement, "j.4");
                if (source71.Count<XElement>() > 0)
                    source71.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source72 = XmlUtils.XmlSearchById(xelement, "j.5");
                if (source72.Count<XElement>() > 0)
                    source72.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source73 = XmlUtils.XmlSearchById(xelement, "j.6");
                if (source73.Count<XElement>() > 0)
                    source73.First<XElement>().SetValue((object)"");
                IEnumerable<XElement> source74 = XmlUtils.XmlSearchById(xelement, "j.7");
                if (source74.Count<XElement>() > 0)
                    source74.ElementAt<XElement>(0).SetValue((object)"");
                ConsultaCatastralServiceClient proxy = new ConsultaCatastralServiceClient();
                DseInmueble.InmuebleDataTable source75 = (DseInmueble.InmuebleDataTable)null;
                try
                {
                    source75 = proxy.GetInmuebleConTitularesByClave(region, manzana, lote, unidad, false).Inmueble;
                }
                finally
                {
                    proxy.Disconnect();
                }
                if (source75.Any<DseInmueble.InmuebleRow>())
                    this.FillAvaluo(xelement, source75.First<DseInmueble.InmuebleRow>());
                else
                    xelement = (XElement)null;
                string empty = string.Empty;
                if (xelement != null)
                    empty = xelement.ToString();
                Encoding utF8 = Encoding.UTF8;
                return utF8Encoding.GetBytes(empty);
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        private void FillAvaluo(XElement data, DseInmueble.InmuebleRow inmuebleRow)
        {
            IEnumerable<XElement> source1 = XmlUtils.XmlSearchById(data, "b.3.1");
            if (source1.Count<XElement>() > 0 && !inmuebleRow.IsCalleNull())
                source1.First<XElement>().SetValue((object)inmuebleRow.Calle);
            IEnumerable<XElement> source2 = XmlUtils.XmlSearchById(data, "b.3.2");
            if (source2.Count<XElement>() > 0 && !inmuebleRow.IsNUMEROINTERIORNull())
                source2.First<XElement>().SetValue((object)inmuebleRow.NUMEROINTERIOR);
            IEnumerable<XElement> source3 = XmlUtils.XmlSearchById(data, "b.3.3");
            if (source3.Count<XElement>() > 0)
                source3.First<XElement>().SetValue((object)inmuebleRow.NUMEROEXTERIOR);
            IEnumerable<XElement> source4 = XmlUtils.XmlSearchById(data, "b.3.6");
            if (source4.Count<XElement>() > 0 && !inmuebleRow.IsEDIFICIONull())
                source4.First<XElement>().SetValue((object)inmuebleRow.EDIFICIO);
            IEnumerable<XElement> source5 = XmlUtils.XmlSearchById(data, "b.3.8");
            if (source5.Count<XElement>() > 0 && !inmuebleRow.IsCODIGOPOSTALNull())
                source5.First<XElement>().SetValue((object)inmuebleRow.CODIGOPOSTAL);
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(data, "b.3.9");
            if (xelements1.IsFull())
            {
                string stringXelement = xelements1.ToStringXElement();
                inmuebleRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(data, "b.3.7");
                if (xelements2.IsFull())
                    inmuebleRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements2.ToStringXElement(), stringXelement);
            }
            IEnumerable<XElement> source6 = XmlUtils.XmlSearchById(data, "d.5.n.8");
            if (source6.Count<XElement>() <= 0)
                return;
            foreach (XElement xelement in source6)
                xelement.SetValue((object)inmuebleRow.ZonaValor);
        }

        private void InvokeAvaluo(object o, List<string> clases)
        {
            foreach (PropertyInfo property in Type.GetType(o.GetType().FullName).GetProperties())
            {
                Type type = Type.GetType(property.PropertyType.FullName);
                if (!clases.Contains(type.Name))
                    break;
                object o1 = type.InvokeMember(property.Name, BindingFlags.CreateInstance, (Binder)null, (object)null, (object[])null);
                ReflectionUtils.SetPropertyValue(property.Name, o, o1, false);
                if (Type.GetType(o1.GetType().FullName).GetProperties().Length > 0)
                    this.InvokeAvaluo(o1, clases);
            }
        }

        private Decimal GuardarAvaluo(XmlDocument xmlAvaluo, string idpersona)
        {
            DseAvaluoMant dseAvaluoMant = new DseAvaluoMant();
            List<Decimal> listaFicheros = new List<Decimal>();

            log("GuardarAvaluo","InicioMetodo","");
            using (SIGAPred.Common.DataAccess.OracleDataAccess.TransactionHelper transactionHelper = new SIGAPred.Common.DataAccess.OracleDataAccess.TransactionHelper(Settings.Default.ConnectionString))
            {
                try
                {

                    
                    AltasDocumentosTran altasDocumentosTran = new AltasDocumentosTran();
                    transactionHelper.BeginTransaction();
                    XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
                    Convert.ToDateTime(XmlUtils.XmlSearchById(root, "a.2").ToStringXElement());
                    bool esComercial = root.Descendants((XName)"Comercial").Count<XElement>() > 0;
                    foreach (DataColumn column in (InternalDataCollectionBase)dseAvaluoMant.FEXAVA_AVALUO.Columns)
                        column.AllowDBNull = true;
                    dseAvaluoMant.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(dseAvaluoMant.FEXAVA_AVALUO.NewFEXAVA_AVALUORow());
                    dseAvaluoMant.FEXAVA_AVALUO[0].CODESTADOAVALUO = EstadosAvaluoEnum.Recibido.ToDecimal();
                    dseAvaluoMant.FEXAVA_AVALUO[0].FECHA_PRESENTACION = DateTime.Now;
                    if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                        dseAvaluoMant.FEXAVA_AVALUO[0].CODTIPOTRAMITE = "1";
                    else if (root.Descendants((XName)"Catastral").Count<XElement>() > 0)
                        dseAvaluoMant.FEXAVA_AVALUO[0].CODTIPOTRAMITE = "2";
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "a");
                    if (xelements1.IsFull())
                        this.GuardarAvaluoIdentificacion(xelements1.First<XElement>(), ref dseAvaluoMant, idpersona);
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "b");
                    if (xelements2.IsFull())
                        this.GuardarAvaluoAntecedentes(xelements2.First<XElement>(), ref dseAvaluoMant);
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "c");
                    if (xelements3.IsFull())
                        this.GuardarAvaluoCaracteristicasUrbanas(xelements3.First<XElement>(), ref dseAvaluoMant);
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "d");
                    if (xelements4.IsFull())
                        listaFicheros = this.GuardarAvaluoTerreno(transactionHelper, xelements4.First<XElement>(), ref dseAvaluoMant);
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "e");
                    if (xelements5.IsFull())
                        this.GuardarAvaluoDescripcionImueble(xelements5.First<XElement>(), ref dseAvaluoMant,esComercial);
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f");
                    if (xelements6.IsFull())
                        this.GuardarAvaluoElementosConstruccion(xelements6.First<XElement>(), ref dseAvaluoMant);
                    if (esComercial)//Seccion Comercial
                    {
                        IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h");
                        if (xelements7.IsFull())
                            this.GuardarAvaluoEnfoqueMercado(root, xelements7.First<XElement>(), ref dseAvaluoMant);
                    }
                    if (esComercial)//Seccion Comercial
                    {
                        IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "i");
                        if (xelements7.IsFull())
                            this.GuardarAvaluoEnfoqueCostosComercial(xelements7.First<XElement>(), ref dseAvaluoMant);
                    }
                    if (!esComercial)
                    {
                        IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "j");
                        if (xelements7.IsFull())
                            this.GuardarAvaluoEnfoqueCostosCatastral(xelements7.First<XElement>(), ref dseAvaluoMant);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "k");
                    if (xelements8.IsFull())
                        this.GuardarAvaluoEnfoqueIngresos(root, xelements8.First<XElement>(), ref dseAvaluoMant);
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "o");
                    if (xelements9.IsFull())
                        this.GuardarAvaluoResumenConclusionAvaluo(xelements9.First<XElement>(), ref dseAvaluoMant);
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "p");
                    if (xelements10.IsFull())
                        this.GuardarAvaluoValorReferido(xelements10.First<XElement>(), ref dseAvaluoMant);
                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "q");
                    if (xelements11.IsFull())
                        this.GuardarAvaluoAnexoFotografico(transactionHelper, xelements11.First<XElement>(), ref dseAvaluoMant);
                    string cuentaCat = dseAvaluoMant.FEXAVA_AVALUO.First<DseAvaluoMant.FEXAVA_AVALUORow>().REGION + "-" + dseAvaluoMant.FEXAVA_AVALUO.First<DseAvaluoMant.FEXAVA_AVALUORow>().MANZANA + "-" + dseAvaluoMant.FEXAVA_AVALUO.First<DseAvaluoMant.FEXAVA_AVALUORow>().LOTE + "-" + dseAvaluoMant.FEXAVA_AVALUO.First<DseAvaluoMant.FEXAVA_AVALUORow>().UNIDADPRIVATIVA;
                    byte[] bytes = new UTF8Encoding().GetBytes(root.ToString());
                    Decimal idDocumentoDigital = 0M;
                    if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                        idDocumentoDigital = altasDocumentosTran.Tran_InsertAvaluoComercial(transactionHelper, bytes, AvaluosUtils.CrearNombreDocumentoAv("Comercial", cuentaCat), AvaluosUtils.CrearDescripcionDocumentoAv(cuentaCat), dseAvaluoMant.FEXAVA_AVALUO.First<DseAvaluoMant.FEXAVA_AVALUORow>().FECHAAVALUO, new Decimal?()).Value;
                    else if (root.Descendants((XName)"Catastral").Count<XElement>() > 0)
                        idDocumentoDigital = altasDocumentosTran.Tran_InsertAvaluoCatastral(transactionHelper, bytes, AvaluosUtils.CrearNombreDocumentoAv("Catastral", cuentaCat), AvaluosUtils.CrearDescripcionDocumentoAv(cuentaCat), dseAvaluoMant.FEXAVA_AVALUO.First<DseAvaluoMant.FEXAVA_AVALUORow>().FECHAAVALUO, new Decimal?()).Value;
                    altasDocumentosTran.Tran_UpdateDocumentoDigitalFicheros(listaFicheros, idDocumentoDigital, transactionHelper);
                    dseAvaluoMant.FEXAVA_AVALUO[0].IDAVALUO = idDocumentoDigital;
                    this.AvaluoTA.SetTransaction(transactionHelper);
                    this.CarpinteriaTA.SetTransaction(transactionHelper);
                    this.ConstruccionesMercadoTA.SetTransaction(transactionHelper);
                    this.DatosPersonalesTA.SetTransaction(transactionHelper);
                    this.DatosTerrenosTA.SetTransaction(transactionHelper);
                    this.ElementosConstruccionTA.SetTransaction(transactionHelper);
                    this.EnfoqueCostesCatastralTA.SetTransaction(transactionHelper);
                    this.InstalacionHidraulicaSanitariaTA.SetTransaction(transactionHelper);
                    this.InvestProductosCompatiblesTA.SetTransaction(transactionHelper);
                    this.ObraNegraTA.SetTransaction(transactionHelper);
                    this.PuertasYVentaneriaMetalicaTA.SetTransaction(transactionHelper);
                    this.RevestimientoAcabadoInteriorTA.SetTransaction(transactionHelper);
                    this.SuperficieTA.SetTransaction(transactionHelper);
                    this.TerrenoMercadoTA.SetTransaction(transactionHelper);
                    this.TipoConstruccionTA.SetTransaction(transactionHelper);
                    this.ElementosExtraTA.SetTransaction(transactionHelper);
                    this.FotoAvaluoTA.SetTransaction(transactionHelper);
                    this.FotoComparableTA.SetTransaction(transactionHelper);
                    this.FuenteInfLegalTA.SetTransaction(transactionHelper);
                    this.SentenciaTA.SetTransaction(transactionHelper);
                    this.ContratoPrivadoTA.SetTransaction(transactionHelper);
                    this.AliniamientoNumOfTA.SetTransaction(transactionHelper);
                    this.EscrituraTA.SetTransaction(transactionHelper);
                    this.AvaluoTA.Update(dseAvaluoMant.FEXAVA_AVALUO);
                    this.DatosPersonalesTA.Update(dseAvaluoMant.FEXAVA_DATOSPERSONAS);
                    this.SuperficieTA.Update(dseAvaluoMant.FEXAVA_SUPERFICIE);
                    this.TipoConstruccionTA.Update(dseAvaluoMant.FEXAVA_TIPOCONSTRUCCION);
                    this.EnfoqueCostesCatastralTA.Update(dseAvaluoMant.FEXAVA_ENFOQUECOSTESCAT);
                    this.TerrenoMercadoTA.Update(dseAvaluoMant.FEXAVA_TERRENOMERCADO);
                    this.DatosTerrenosTA.Update(dseAvaluoMant.FEXAVA_DATOSTERRENOS);
                    this.ConstruccionesMercadoTA.Update(dseAvaluoMant.FEXAVA_CONSTRUCCIONESMER);
                    this.InvestProductosCompatiblesTA.Update(dseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMP);
                    this.ElementosConstruccionTA.Update(dseAvaluoMant.FEXAVA_ELEMENTOSCONST);
                    this.CarpinteriaTA.Update(dseAvaluoMant.FEXAVA_CARPINTERIA);
                    this.ObraNegraTA.Update(dseAvaluoMant.FEXAVA_OBRANEGRA);
                    this.RevestimientoAcabadoInteriorTA.Update(dseAvaluoMant.FEXAVA_REVESTIMIENTOACABADO);
                    this.PuertasYVentaneriaMetalicaTA.Update(dseAvaluoMant.FEXAVA_PUERTASYVENTANERIA);
                    this.InstalacionHidraulicaSanitariaTA.Update(dseAvaluoMant.FEXAVA_INSTALACIONHIDSAN);
                    this.ElementosExtraTA.Update(dseAvaluoMant.FEXAVA_ELEMENTOSEXTRA);
                    this.FotoAvaluoTA.Update(dseAvaluoMant.FEXAVA_FOTOAVALUO);
                    this.FotoComparableTA.Update(dseAvaluoMant.FEXAVA_FOTOCOMPARABLE);
                    this.FuenteInfLegalTA.Update(dseAvaluoMant.FEXAVA_FUENTEINFORMACIONLEG);
                    this.SentenciaTA.Update(dseAvaluoMant.FEXAVA_SENTENCIA);
                    this.ContratoPrivadoTA.Update(dseAvaluoMant.FEXAVA_CONTRATOPRIVADO);
                    this.AliniamientoNumOfTA.Update(dseAvaluoMant.FEXAVA_ALINEAMIENTONUMOFI);
                    this.EscrituraTA.Update(dseAvaluoMant.FEXAVA_ESCRITURA);
                    transactionHelper.Commit();
                    return idDocumentoDigital;
                }
                catch (FaultException<AvaluosException> ex)
                {
                    log("GuardaAvaluo FaultException ", ex.Message, ex.StackTrace);
                    transactionHelper.RollBack();
                    throw;
                }
                catch (Exception ex)
                {
                    log("GuardaAvaluo Exception ", ex.Message,ex.StackTrace);
                    transactionHelper.RollBack();
                    throw;
                }
            }
        }

        private void GuardarAvaluoIdentificacion(
          XElement identificacion,
          ref DseAvaluoMant dseAvaluo,
          string idpersona)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(identificacion, "a.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].NUMEROAVALUO = xelements1.ToStringXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(identificacion, "a.2");
            if (xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO = xelements2.First<XElement>().Value.To<DateTime>();
            string registroPerito = string.Empty;
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(identificacion, "a.3");
            if (xelements3.IsFull())
            {
                registroPerito = xelements3.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONAPERITO = Convert.ToDecimal(idpersona);
            }
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(identificacion, "a.4");
            if (xelements4.IsFull())
            {
                string stringXelement = xelements4.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONAPERITO = this.IdPeritoSociedadByRegistro(registroPerito, string.Empty, true);
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONASOCIEDAD = this.IdPeritoSociedadByRegistro(registroPerito, stringXelement, false);
            }
            string tipo = string.Empty;
            if (dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE.Equals("2"))
                tipo = "CAT";
            else if (dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE.Equals("1"))
                tipo = "COM";
            dseAvaluo.FEXAVA_AVALUO[0].NUMEROUNICO = AvaluosUtils.ObtenerNumUnicoAv(tipo);

            
        }

        private void GuardarAvaluoAntecedentes(XElement antecedentes, ref DseAvaluoMant dseAvaluo)
        {
            DseAvaluoMant.FEXAVA_DATOSPERSONASRow row1 = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
            DseAvaluoMant.FEXAVA_DATOSPERSONASRow row2 = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
            row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            row2.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(antecedentes, "b.1.1");
            if (xelements1.IsFull())
                row1.APELLIDOPATERNO = xelements1.ToStringXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(antecedentes, "b.1.2");
            if (xelements2.IsFull())
                row1.APELLIDOMATERNO = xelements2.ToStringXElement();
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(antecedentes, "b.1.3");
            if (xelements3.IsFull())
                row1.NOMBRE = xelements3.ToStringXElement();
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(antecedentes, "b.1.4");
            if (xelements4.IsFull())
                row1.CALLE = xelements4.ToStringXElement();
            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(antecedentes, "b.1.5");
            if (xelements5.IsFull())
                row1.NUMEROINTERIOR = xelements5.ToStringXElement();
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(antecedentes, "b.1.6");
            if (xelements6.IsFull())
                row1.NUMEROEXTERIOR = xelements6.ToStringXElement();
            IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(antecedentes, "b.1.8");
            if (xelements7.IsFull())
                row1.CODIGOPOSTAL = xelements7.ToStringXElement();
            /*IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(antecedentes, "b.1.9");
            if (xelements8.IsFull())
            {
                string stringXelement = xelements8.ToStringXElement();
                Decimal num1 = CatastralUtils.ObtenerIdDelegacionPorNombre(stringXelement);
                if (num1 != -1M)
                    row1.IDDELEGACION = num1;
                row1.NOMBREDELEGACION = xelements8.ToStringXElement();
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                if (xelements9.IsFull())
                {
                    Decimal num2 = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                    if (num2 != -1M)
                        row1.IDCOLONIA = num2;
                    row1.NOMBRECOLONIA = xelements9.ToStringXElement();
                }
            }*/

            try
            {
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(antecedentes, "b.1.9.1");
                if (xelements8.IsFull())
                {
                    string alcaldiaString = xelements8.ToStringXElement();
                    decimal alcaldiaDecimal = xelements8.ToDecimalXElement();
                    if (!alcaldiaString.Equals("018"))//Se agrega al catálogo el elemento 018 Otros (Municipios fuera de CDMX)
                    {
                        row1.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(alcaldiaString);
                        row1.NOMBREDELEGACION = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(alcaldiaDecimal);
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                        if (xelements9.IsFull())
                        {
                            row1.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), alcaldiaString);
                            row1.NOMBRECOLONIA = xelements9.ToStringXElement();
                        }
                    }
                    else//Municipios fuera de CDMX
                    {
                        row1.IDDELEGACION = xelements8.ToDecimalXElement();
                        //Se guarda el valor que contenga Otros
                        IEnumerable<XElement> municipio = XmlUtils.XmlSearchById(antecedentes, "b.1.9.2");
                        if (municipio.IsFull())
                        {
                            row1.NOMBREDELEGACION = municipio.ToStringXElement();
                        }
                        //Se guaraa como Colonia lo que tenga el XML 
                        IEnumerable<XElement> colonia = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                        if (colonia.IsFull())
                        {
                            row1.NOMBRECOLONIA = colonia.ToStringXElement();
                            row1.IDCOLONIA = 0M;
                        }
                    }
                }
            }catch(Exception ex)
            {
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(antecedentes, "b.1.9");
                if (xelements8.IsFull())
                {
                    string stringXelement = xelements8.ToStringXElement();
                    Decimal num1 = CatastralUtils.ObtenerIdDelegacionPorNombre(stringXelement);
                    if (num1 != -1M)
                        row1.IDDELEGACION = num1;
                    row1.NOMBREDELEGACION = xelements8.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                    if (xelements9.IsFull())
                    {
                        Decimal num2 = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                        if (num2 != -1M)
                            row1.IDCOLONIA = num2;
                        row1.NOMBRECOLONIA = xelements9.ToStringXElement();
                    }
                }
            }


            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(antecedentes, "b.1.10");
            if (xelements10.IsFull())
                row1.TIPOPERSONA = xelements10.ToStringXElement();
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(antecedentes, "b.2.1");
            if (xelements11.IsFull())
                row2.APELLIDOPATERNO = xelements11.ToStringXElement();
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(antecedentes, "b.2.2");
            if (xelements12.IsFull())
                row2.APELLIDOMATERNO = xelements12.ToStringXElement();
            IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(antecedentes, "b.2.3");
            if (xelements13.IsFull())
                row2.NOMBRE = xelements13.ToStringXElement();
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(antecedentes, "b.2.4");
            if (xelements14.IsFull())
                row2.CALLE = xelements14.ToStringXElement();
            IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(antecedentes, "b.2.5");
            if (xelements15.IsFull())
                row2.NUMEROINTERIOR = xelements15.ToStringXElement();
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(antecedentes, "b.2.6");
            if (xelements16.IsFull())
                row2.NUMEROEXTERIOR = xelements16.ToStringXElement();
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(antecedentes, "b.2.8");
            if (xelements17.IsFull())
                row2.CODIGOPOSTAL = xelements17.ToStringXElement();


            /*
            IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9");
            if (xelements18.IsFull())
            {
                string stringXelement = xelements18.ToStringXElement();
                Decimal num1 = CatastralUtils.ObtenerIdDelegacionPorNombre(stringXelement);
                if (num1 != -1M)
                    row2.IDDELEGACION = num1;
                row2.NOMBREDELEGACION = xelements18.ToStringXElement();
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                if (xelements9.IsFull())
                {
                    Decimal num2 = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                    if (num2 != -1M)
                        row2.IDCOLONIA = num2;
                    row2.NOMBRECOLONIA = xelements9.ToStringXElement();
                }
            }*/

            try
            {
                IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9.1");
                if (xelements18.IsFull())
                {
                    string alcaldiaString = xelements18.ToStringXElement();
                    decimal alcaldiaDecimal = xelements18.ToDecimalXElement();
                    if (!alcaldiaString.Equals("018"))//Se agrega al catálogo el elemento 018 Otros (Municipios fuera de CDMX)
                    {
                        row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(alcaldiaString);
                        row2.NOMBREDELEGACION = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(alcaldiaDecimal);
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                        if (xelements9.IsFull())
                        {
                            row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), alcaldiaString);
                            row2.NOMBRECOLONIA = xelements9.ToStringXElement();
                        }
                    }
                    else//Municipios fuera de CDMX
                    {
                        row2.IDDELEGACION = xelements18.ToDecimalXElement();
                        //Se guarda el valor que contenga Otros
                        IEnumerable<XElement> municipio = XmlUtils.XmlSearchById(antecedentes, "b.2.9.2");
                        if (municipio.IsFull())
                        {
                            row2.NOMBREDELEGACION = municipio.ToStringXElement();
                        }
                        //Se guaraa como Colonia lo que tenga el XML 
                        IEnumerable<XElement> colonia = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                        if (colonia.IsFull())
                        {
                            row2.NOMBRECOLONIA = colonia.ToStringXElement();
                            row2.IDCOLONIA = 0M;
                        }
                    }
                }
            }catch(Exception ex)
            {

                IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9");
                if (xelements18.IsFull())
                {
                    string stringXelement = xelements18.ToStringXElement();
                    Decimal num1 = CatastralUtils.ObtenerIdDelegacionPorNombre(stringXelement);
                    if (num1 != -1M)
                        row2.IDDELEGACION = num1;
                    row2.NOMBREDELEGACION = xelements18.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                    if (xelements9.IsFull())
                    {
                        Decimal num2 = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                        if (num2 != -1M)
                            row2.IDCOLONIA = num2;
                        row2.NOMBRECOLONIA = xelements9.ToStringXElement();
                    }
                }
            }




            IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(antecedentes, "b.2.10");
            if (xelements19.IsFull())
                row2.TIPOPERSONA = xelements19.ToStringXElement();
            IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.1");
            if (xelements20.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].REGION = xelements20.ToStringXElement();
            IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.2");
            if (xelements21.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].MANZANA = xelements21.ToStringXElement();
            IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.3");
            if (xelements22.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].LOTE = xelements22.ToStringXElement();
            IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.4");
            if (xelements23.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].UNIDADPRIVATIVA = xelements23.ToStringXElement();
            IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.5");
            if (xelements24.IsFull())
            {
                try
                {
                    dseAvaluo.FEXAVA_AVALUO[0].DIGITOVERIFICADOR = xelements24.ToStringXElement();
                }
                catch (Exception ex)
                {
                    ExceptionPolicyWrapper.HandleException(ex);
                    throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
                }
            }
            IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(antecedentes, "b.4");
            if (xelements25.IsFull())
            {
               //try
               // {
                    switch (XmlUtils.XmlSearchById(xelements25, "b.4.1").ToStringXElement().ToInt())
                    {
                        case 1:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P1;
                            break;
                        case 2:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P2;
                            break;
                        case 3:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P3;
                            break;
                        case 4:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = XmlUtils.XmlSearchById(xelements25, "b.4.2").ToStringXElement().ToUpper();
                            break;
                    }
               //}catch(Exception ex) { }
             //   dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = xelements25.ToStringXElement();
            }


            IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(antecedentes, "b.5");
            if (xelements26.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].OBJETO = xelements26.ToStringXElement();
            IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(antecedentes, "b.6");
            if (xelements27.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD = xelements27.ToDecimalXElement();
            row1.CODTIPOFUNCION = "S";
            row2.CODTIPOFUNCION = "P";
            dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(row1);
            dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(row2);

            IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(antecedentes, "b.7");
            if (xelements28.IsFull())
            {
                switch (XmlUtils.XmlSearchById(xelements28, "b.7.1").ToStringXElement().ToInt())
                {
                    case 1:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP1;
                        break;
                    case 2:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP2;
                        break;
                    case 3:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP3;
                        break;
                    case 4:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP4;
                        break;
                    case 5:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP5;
                        break;
                    case 6:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP6;
                        break;
                    case 7:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP7;
                        break;
                    case 8:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP8;
                        break;
                    case 9:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP9;
                        break;
                    case 10:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP10;
                        break;
                    case 11:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP11;
                        break;
                    case 12:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP12;
                        break;
                    case 13:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP13;
                        break;
                    case 14:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP14;
                        break;
                    case 15:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP15;
                        break;
                    case 16:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP16;
                        break;
                    case 17:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP17;
                        break;
                    case 18:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP18;
                        break;
                    case 19:
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = XmlUtils.XmlSearchById(xelements28, "b.7.2").ToStringXElement().ToUpper();
                        break;
                }
                //}catch(Exception ex) { }
                //   dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = xelements25.ToStringXElement();
            }

        }

        private void GuardarAvaluoCaracteristicasUrbanas(
          XElement caracteristicasUrbanas,
          ref DseAvaluoMant dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASIFICACIONZONA = xelements1.ToDecimalXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.2");
            if (xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUINDICESATURACIONZONA = xelements2.ToDecimalXElement();
            DateTime fechaavaluo = dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO;
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.3");
            if (xelements3.IsFull())
            {
                string stringXelement = xelements3.ToStringXElement();
                int num = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaavaluo, stringXelement);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASESCONSTRUCCION = (Decimal)num;
            }
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.4");
            if (xelements4.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDENSIDADPOBLACION = xelements4.ToDecimalXElement();
            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.5");
            if (xelements5.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODNIVELSOCIOECONOMICO = xelements5.ToDecimalXElement();
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.1");
            if (xelements6.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUUSO = xelements6.ToStringXElement();
            IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.2");
            if (xelements7.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUAREALIBREOBLIGATORIO = xelements7.ToDecimalXElement();
            IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.3");
            if (xelements8.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUNUMMAXNIVELESACONSTRUIR = xelements8.ToDecimalXElement();
            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.4");
            if (xelements9.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCOEFICIENTE = xelements9.ToDecimalXElement();
            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.1");
            if (xelements10.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLE = xelements10.ToDecimalXElement();
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.2");
            if (xelements11.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLERESIDUAL = xelements11.ToDecimalXElement();
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.3");
            if (xelements12.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALCALLE = xelements12.ToDecimalXElement();
            IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.4");
            if (xelements13.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALZONA = xelements13.ToDecimalXElement();
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.5");
            if (xelements14.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEINMUEBLE = xelements14.ToDecimalXElement();
            IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.7");
            if (xelements15.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROELECTRICO = xelements15.ToDecimalXElement();
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.8");
            if (xelements16.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLE = xelements16.ToStringXElement();
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.9");
            if (xelements17.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODALUMBRADOPUBLICO = xelements17.ToDecimalXElement();
            IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.10");
            if (xelements18.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODVIALIDADES = xelements18.ToDecimalXElement();
            IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.11");
            if (xelements19.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODBANQUETAS = xelements19.ToDecimalXElement();
            IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.12");
            if (xelements20.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODGUARNICIONES = xelements20.ToDecimalXElement();
            IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.13");
            if (xelements21.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUPORCENTAJEINFRAESTRUCTURA = xelements21.ToDecimalXElement();
            IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.14");
            if (xelements22.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODGASNATURAL = xelements22.ToDecimalXElement();
            IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.15");
            if (xelements23.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROTELEFONICA = xelements23.ToDecimalXElement();
            IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.16");
            if (xelements24.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLETEL = xelements24.ToStringXElement();
            IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.17");
            if (xelements25.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSENALIZACIONVIAS = xelements25.ToStringXElement();
            IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.18");
            if (xelements26.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODNOMENCLATURACALLE = xelements26.ToStringXElement();
            IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.19");
            if (xelements27.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUDISTANCIATRANSPORTEURBANO = xelements27.ToDecimalXElement();
            IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.20");
            if (xelements28.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUFRECUENCIATRANSPORTEURBANO = xelements28.ToDecimalXElement();
            IEnumerable<XElement> xelements29 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.21");
            if (xelements29.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUDISTANCIATRANSPORTESUBURB = xelements29.ToDecimalXElement();
            IEnumerable<XElement> xelements30 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.22");
            if (xelements30.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUFRECUENCIATRANSPORTESUBURB = xelements30.ToDecimalXElement();
            IEnumerable<XElement> xelements31 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.23");
            if (xelements31.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODVIGILANCIAZONA = xelements31.ToDecimalXElement();
            IEnumerable<XElement> xelements32 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.24");
            if (xelements32.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODRECOLECCIONBASURA = xelements32.ToDecimalXElement();
            IEnumerable<XElement> xelements33 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.25");
            if (xelements33.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEIGLESIA = this.BooleanXMLtoOracle(xelements33.First<XElement>().Value);
            IEnumerable<XElement> xelements34 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.26");
            if (xelements34.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEMERCADOS = this.BooleanXMLtoOracle(xelements34.First<XElement>().Value);
            IEnumerable<XElement> xelements35 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.27");
            if (xelements35.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEPLAZASPUBLICOS = this.BooleanXMLtoOracle(xelements35.First<XElement>().Value);
            IEnumerable<XElement> xelements36 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.28");
            if (xelements36.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEPARQUESJARDINES = this.BooleanXMLtoOracle(xelements36.First<XElement>().Value);
            IEnumerable<XElement> xelements37 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.29");
            if (xelements37.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEESCUELAS = this.BooleanXMLtoOracle(xelements37.First<XElement>().Value);
            IEnumerable<XElement> xelements38 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.30");
            if (xelements38.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEHOSPITALES = this.BooleanXMLtoOracle(xelements38.First<XElement>().Value);
            IEnumerable<XElement> xelements39 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.31");
            if (xelements39.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEBANCOS = this.BooleanXMLtoOracle(xelements39.First<XElement>().Value);
            IEnumerable<XElement> xelements40 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.32");
            if (!xelements40.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEESTACIONTRANSPORTE = this.BooleanXMLtoOracle(xelements40.First<XElement>().Value);
        }

        private void GuardarAvaluoDescripcionImueble(
          XElement descripcionInmueble,
          ref DseAvaluoMant dseAvaluo,
          bool esComercial)
        {
            DateTime fechaavaluo = dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(descripcionInmueble, "e.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIUSOACTUAL = xelements1.ToStringXElement();
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.1");
            if (elementos1.IsFull())
            {
                foreach (XElement root in elementos1)
                {
                    DseAvaluoMant.FEXAVA_TIPOCONSTRUCCIONRow row = dseAvaluo.FEXAVA_TIPOCONSTRUCCION.NewFEXAVA_TIPOCONSTRUCCIONRow();
                    row.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "e.2.1.n.1");
                    if (xelements2.IsFull())
                        row.DESCRIPCION = xelements2.ToStringXElement();
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "e.2.1.n.2");
                    if (xelements3.IsFull())
                    {
                        string stringXelement = xelements3.ToStringXElement();
                        int o = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(fechaavaluo, stringXelement);
                        row.IDUSOSEJERCICIO = o.ToDecimal();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "e.2.1.n.3");
                    if (xelements4.IsFull())
                        row.NUMNIVELES = xelements4.ToDecimalXElement();
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "e.2.1.n.4");
                    if (xelements5.IsFull())
                    {
                        string stringXelement = xelements5.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdRangoNivelesByCodeAndAno(fechaavaluo, stringXelement);
                        row.IDRANGONIVELESEJERCICIO = (Decimal)num;
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "e.2.1.n.5");
                    if (xelements6.IsFull())
                        row.PUNTAJECLASIFICACION = xelements6.ToDecimalXElement();
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "e.2.1.n.6");
                    string codClase = "";
                    if (xelements7.IsFull())
                    {
                        codClase = xelements7.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaavaluo, codClase);
                        row.IDCLASESEJERCICIO = (Decimal)num;
                    }

                    //if (!esComercial)
                    //{
                        IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "e.2.1.n.7");
                        if (xelements8.IsFull())
                            row.EDAD = xelements8.ToDecimalXElement();
                    //}
                    //else { row.EDAD = 0M; }

                    if (XmlUtils.XmlSearchById(root, "e.2.1.n.8").IsFull())
                    {
                        int idUsoEjercicio = row.IDUSOSEJERCICIO.ToInt();
                        int idClaseEjercicio = row.IDCLASESEJERCICIO.ToInt();
                        if (codClase != "U")
                        {
                            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                            if (source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>())
                                row.IDUSOCLASEEJERCICIO = source[0].IDUSOCLASEEJERCICIO;
                        }
                    }
                    if (esComercial)
                    {
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "e.2.1.n.9");
                        if (xelements9.IsFull())
                        {
                            if (!xelements9.ToStringXElement().Equals(""))
                                row.VIDAUTILREMANENTE = xelements9.ToDecimalXElement();
                            else
                                row.VIDAUTILREMANENTE = 1M;
                        }
                        else
                            row.VIDAUTILREMANENTE = 1M;
                    }
                    else
                    {
                        row.VIDAUTILREMANENTE = 1M;
                    } 
                    /// //JACM Se da de baja el campo 2021-02-04
                    //IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "e.2.1.n.10");
                    //if (xelements10.IsFull())
                    //    row.CODESTADOCONSERVACION = xelements10.ToDecimalXElement();
                    string conserva = xelements3.ToStringXElement();
                    log("GuardarAvaluoDescripcionImueble CODESTADOCONSERVACION ", "xelements3 : ", conserva);

                    if (conserva != "P"  &&
                        conserva != "PE" &&
                        conserva != "PC" &&
                        conserva != "J"  //&& 
                        //conserva != "H"
                        ) { 
                    row.CODESTADOCONSERVACION = 2M;
                    }
                    else { row.CODESTADOCONSERVACION = 3M; }

                    

                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "e.2.1.n.11");
                    if (xelements11.IsFull())
                        row.SUPERFICIE = xelements11.ToDecimalXElement();


                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "e.2.1.n.12");
                    if (xelements12.IsFull())
                        row.VALORUNITARIOREPNUEVO = xelements12.ToDecimalXElement();
                    row.CODTIPO = "P";
                    dseAvaluo.FEXAVA_TIPOCONSTRUCCION.AddFEXAVA_TIPOCONSTRUCCIONRow(row);
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.5");
            if (elementos2.IsFull())
            {
                foreach (XElement root in elementos2)
                {
                    DseAvaluoMant.FEXAVA_TIPOCONSTRUCCIONRow row = dseAvaluo.FEXAVA_TIPOCONSTRUCCION.NewFEXAVA_TIPOCONSTRUCCIONRow();
                    row.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "e.2.5.n.1");
                    if (xelements2.IsFull())
                        row.DESCRIPCION = xelements2.ToStringXElement();
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "e.2.5.n.2");
                    if (xelements3.IsFull())
                    {
                        string stringXelement = xelements3.ToStringXElement();
                        int o = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(fechaavaluo, stringXelement);
                        row.IDUSOSEJERCICIO = o.ToDecimal();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "e.2.5.n.3");
                    if (xelements4.IsFull())
                        row.NUMNIVELES = xelements4.ToDecimalXElement();
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "e.2.5.n.4");
                    if (xelements5.IsFull())
                    {
                        string stringXelement = xelements5.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdRangoNivelesByCodeAndAno(fechaavaluo, stringXelement);
                        row.IDRANGONIVELESEJERCICIO = (Decimal)num;
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "e.2.5.n.5");
                    if (xelements6.IsFull())
                        row.PUNTAJECLASIFICACION = xelements6.ToDecimalXElement();
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "e.2.5.n.6");
                    string codClase = "";
                    if (xelements7.IsFull())
                    {
                        codClase = xelements7.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaavaluo, codClase);
                        row.IDCLASESEJERCICIO = (Decimal)num;
                    }

                    //JACM Se da de baja el campo 2021-02-15
                    //IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "e.2.5.n.7");
                    //if (xelements8.IsFull())
                    //    row.EDAD = xelements8.ToDecimalXElement();

                    //if (!esComercial)
                    //{
                        IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "e.2.5.n.7");
                        if (xelements8.IsFull())
                            row.EDAD = xelements8.ToDecimalXElement();
                    //}
                    //else { row.EDAD = 0M; }
                    if (esComercial) {
                        int idUsoEjercicio = row.IDUSOSEJERCICIO.ToInt();
                        int idClaseEjercicio = row.IDCLASESEJERCICIO.ToInt();
                        if (codClase != "U")
                        {
                            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                            if (source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>())
                                row.IDUSOCLASEEJERCICIO = source[0].IDUSOCLASEEJERCICIO;
                        }
                    }
                    else
                    {
                        if (XmlUtils.XmlSearchById(root, "e.2.5.n.8").IsFull())
                        {
                            int idUsoEjercicio = row.IDUSOSEJERCICIO.ToInt();
                            int idClaseEjercicio = row.IDCLASESEJERCICIO.ToInt();
                            if (codClase != "U")
                            {
                                DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                                if (source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>())
                                    row.IDUSOCLASEEJERCICIO = source[0].IDUSOCLASEEJERCICIO;
                            }
                        }
                    }
                    if (esComercial)
                    {
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "e.2.5.n.9");
                        if (xelements9.IsFull())
                        {
                            if (!xelements9.ToStringXElement().Equals(""))
                                row.VIDAUTILREMANENTE = xelements9.ToDecimalXElement();
                            else
                                row.VIDAUTILREMANENTE = 1M;
                        }
                        else
                            row.VIDAUTILREMANENTE = 1M;
                    }
                    else
                    {
                        row.VIDAUTILREMANENTE = 1M;

                    }
                    // JACM Se da de baja el campo 2021-02-04
                    //IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "e.2.5.n.10");
                    //if (xelements10.IsFull())
                    //    row.CODESTADOCONSERVACION = xelements10.ToDecimalXElement();

                    string conserva = xelements3.ToStringXElement();
                    log("GuardarAvaluoDescripcionImueble CODESTADOCONSERVACION ", "xelements3 : ", conserva);

                    if (conserva != "P" &&
                        conserva != "PE" &&
                        conserva != "PC" &&
                        conserva != "J"  //&& 
                        //conserva != "H"
                        )
                    {
                        row.CODESTADOCONSERVACION = 2M;
                    }
                    else { row.CODESTADOCONSERVACION = 3M; }


                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "e.2.5.n.11");
                    if (xelements11.IsFull())
                        row.SUPERFICIE = xelements11.ToDecimalXElement();
                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "e.2.5.n.12");
                    if (xelements12.IsFull())
                        row.VALORUNITARIOREPNUEVO = xelements12.ToDecimalXElement();
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "e.2.5.n.18");
                    if (xelements13.IsFull())
                        row.TEINDIVISO = xelements13.ToDecimalXElement();
                    row.CODTIPO = "C";
                    dseAvaluo.FEXAVA_TIPOCONSTRUCCION.AddFEXAVA_TIPOCONSTRUCCIONRow(row);
                }
            }
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.3");
            if (xelements14.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVALORTOTALCONSPRIVATIVAS = xelements14.ToDecimalXElement();
            IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.7");
            if (xelements15.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVALORTOTALCONSTCOMUNES = xelements15.ToDecimalXElement();
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(descripcionInmueble, "e.3");
            if (xelements16.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVIDAUTILPONDERADA = xelements16.ToDecimalXElement();
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(descripcionInmueble, "e.4");
            if (xelements17.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIEDADPONDERADA = xelements17.ToDecimalXElement();
            IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(descripcionInmueble, "e.5");
            if (xelements18.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVIDAUTILREMANENTEPONDERADA = xelements18.ToDecimalXElement();
            IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(descripcionInmueble, "e.6");
            if (!xelements19.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].DIPORCENTAJESUPULTNIVEL = xelements19.ToDecimalXElement();
        }

        private List<Decimal> GuardarAvaluoTerreno(
          SIGAPred.Common.DataAccess.OracleDataAccess.TransactionHelper transactionHelper,
          XElement terreno,
          ref DseAvaluoMant dseAvaluo)
        {
            List<Decimal> numList = new List<Decimal>();
            IEnumerable<XElement> xelements1 = (IEnumerable<XElement>)null;
            DseAvaluoMant.FEXAVA_FUENTEINFORMACIONLEGRow row1 = (DseAvaluoMant.FEXAVA_FUENTEINFORMACIONLEGRow)null;
            DseAvaluoMant.FEXAVA_ESCRITURARow row2 = (DseAvaluoMant.FEXAVA_ESCRITURARow)null;
            DseAvaluoMant.FEXAVA_SENTENCIARow row3 = (DseAvaluoMant.FEXAVA_SENTENCIARow)null;
            DseAvaluoMant.FEXAVA_CONTRATOPRIVADORow row4 = (DseAvaluoMant.FEXAVA_CONTRATOPRIVADORow)null;
            DseAvaluoMant.FEXAVA_ALINEAMIENTONUMOFIRow row5 = (DseAvaluoMant.FEXAVA_ALINEAMIENTONUMOFIRow)null;
            xelements1 = XmlUtils.XmlSearchById(terreno, "d.1");
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(terreno, "d.2");
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(terreno, "d.3");
            AltasDocumentosTran altasDocumentosTran = new AltasDocumentosTran();
            byte[] binariodatos1 = Convert.FromBase64String(xelements2.ToStringXElement());
            Decimal num1 = altasDocumentosTran.Tran_InsertFichero(transactionHelper, "CroquisMicroLocalizacion", "CroquisMicroLocalizacion", binariodatos1).Value;
            numList.Add(num1);
            xelements2.First<XElement>().SetValue((object)num1.ToString());
            byte[] binariodatos2 = Convert.FromBase64String(xelements3.ToStringXElement());
            Decimal num2 = altasDocumentosTran.Tran_InsertFichero(transactionHelper, "CroquisMacroLocalizacion", "CroquisMacroLocalizacion", binariodatos2).Value;
            numList.Add(num2);
            xelements3.First<XElement>().SetValue((object)num2.ToString());
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(terreno, "d.4.1.1");
            if (xelements4.IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("1");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.3");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>();
                row2 = dseAvaluo.FEXAVA_ESCRITURA.NewFEXAVA_ESCRITURARow();
                row2.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.1");
                if (xelements6.IsFull())
                    row2.NUMESCRITURA = xelements6.ToDecimalXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.2");
                if (xelements7.IsFull())
                    row2.NUMVOLUMEN = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.4");
                if (xelements8.IsFull())
                    row2.NUMNOTARIO = xelements8.ToDecimalXElement();
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.5");
                if (xelements9.IsFull())
                    row2.NOMBRENOTARIO = xelements9.ToStringXElement();
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.6");
                if (xelements10.IsFull())
                    row2.DISTRITOJUDICIALNOTARIO = xelements10.ToStringXElement();
            }
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(terreno, "d.4.1.2");
            if (xelements11.IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("2");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements11, "d.4.1.2.2");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>();
                row3 = dseAvaluo.FEXAVA_SENTENCIA.NewFEXAVA_SENTENCIARow();
                row3.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(xelements11, "d.4.1.2.1");
                if (xelements6.IsFull())
                    row3.JUZGADO = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(xelements11, "d.4.1.2.3");
                if (xelements7.IsFull())
                    row3.NUMEXPEDIENTE = xelements7.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(terreno, "d.4.1.3").IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("3");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.1");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>();
                row4 = dseAvaluo.FEXAVA_CONTRATOPRIVADO.NewFEXAVA_CONTRATOPRIVADORow();
                row4.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.2");
                if (xelements6.IsFull())
                    row4.NOMBREADQUIRIENTE = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.3");
                if (xelements7.IsFull())
                    row4.APELLIDOPATERNOADQUIRIENTE = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.4");
                if (xelements8.IsFull())
                    row4.APELLIDOMATERNOADQUIRIENTE = xelements8.ToStringXElement();
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.5");
                if (xelements9.IsFull())
                    row4.NOMBREENAJENANTE = xelements9.ToStringXElement();
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.6");
                if (xelements10.IsFull())
                    row4.APELLIDOPATERNOENAJENANTE = xelements10.ToStringXElement();
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.7");
                if (xelements12.IsFull())
                    row4.APELLIDOMATERNOENAJENANTE = xelements12.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(terreno, "d.4.1.4").IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("4");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(terreno, "d.4.1.4.1");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>();
                row5 = dseAvaluo.FEXAVA_ALINEAMIENTONUMOFI.NewFEXAVA_ALINEAMIENTONUMOFIRow();
                row5.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(terreno, "d.4.1.4.2");
                if (xelements6.IsFull())
                    row5.NUMFOLIO = xelements6.ToStringXElement();
            }
            if (row1 != null)
            {
                dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.AddFEXAVA_FUENTEINFORMACIONLEGRow(row1);
                if (row2 != null)
                    dseAvaluo.FEXAVA_ESCRITURA.AddFEXAVA_ESCRITURARow(row2);
                else if (row3 != null)
                    dseAvaluo.FEXAVA_SENTENCIA.AddFEXAVA_SENTENCIARow(row3);
                else if (row4 != null)
                    dseAvaluo.FEXAVA_CONTRATOPRIVADO.AddFEXAVA_CONTRATOPRIVADORow(row4);
                else if (row5 != null)
                    dseAvaluo.FEXAVA_ALINEAMIENTONUMOFI.AddFEXAVA_ALINEAMIENTONUMOFIRow(row5);
            }
            IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(terreno, "d.5");
            if (elementos.IsFull())
            {
                foreach (XElement root in elementos)
                {
                    DseAvaluoMant.FEXAVA_SUPERFICIERow row6 = dseAvaluo.FEXAVA_SUPERFICIE.NewFEXAVA_SUPERFICIERow();
                    row6.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "d.5.n.1");
                    if (xelements5.IsFull())
                        row6.IDENTIFICADORFRACCION = xelements5.ToDecimalXElement();
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "d.5.n.2");
                    if (xelements6.IsFull())
                        row6.SUPERFICIEFRACCION = xelements6.ToDecimalXElement();

                    //TODO: Asignar valores
                    
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "d.5.n.3");
                    if (xelements7.IsFull()) { 
                        IEnumerable<XElement> xelements7_1 = XmlUtils.XmlSearchById(root, "d.5.n.3.1");
                        //if (xelements7_1.IsFull())
                            //row6.FZO = xelements7_1.ToDecimalXElement();
                        IEnumerable<XElement> xelements7_2 = XmlUtils.XmlSearchById(root, "d.5.n.3.2");
                        if (xelements7_2.IsFull())
                        row6.FZO = xelements7_2.ToDecimalXElement();
                        IEnumerable<XElement> xelements7_3 = XmlUtils.XmlSearchById(root, "d.5.n.3.3");
                        //if (xelements7_3.IsFull())
                        //row6.FZO = xelements7_3.ToDecimalXElement();
                    }

                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "d.5.n.4");
                    //if (xelements8.IsFull())
                    //    row6.FUB = xelements8.ToDecimalXElement();
                    if (xelements8.IsFull())
                    {
                        IEnumerable<XElement> xelements8_1 = XmlUtils.XmlSearchById(root, "d.5.n.4.1");
                        //if (xelements8_1.IsFull())
                        //row6.FUB = xelements8_1.ToDecimalXElement();
                        IEnumerable<XElement> xelements8_2 = XmlUtils.XmlSearchById(root, "d.5.n.4.2");
                        if (xelements8_2.IsFull())
                        row6.FUB = xelements8_2.ToDecimalXElement();
                        IEnumerable<XElement> xelements8_3 = XmlUtils.XmlSearchById(root, "d.5.n.4.3");
                        //if (xelements8_3.IsFull())
                        //row6.FUB = xelements8_3.ToDecimalXElement();
                    }

                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "d.5.n.5");
                    //if (xelements9.IsFull())
                    //    row6.FFR = xelements9.ToDecimalXElement();
                    if (xelements9.IsFull())
                    {
                        IEnumerable<XElement> xelements9_1 = XmlUtils.XmlSearchById(root, "d.5.n.5.1");
                        //if (xelements9_1.IsFull())
                        //row6.FFR = xelements9_1.ToDecimalXElement();
                        IEnumerable<XElement> xelements9_2 = XmlUtils.XmlSearchById(root, "d.5.n.5.2");
                        if (xelements9_2.IsFull())
                        row6.FFR = xelements9_2.ToDecimalXElement();
                        IEnumerable<XElement> xelements9_3 = XmlUtils.XmlSearchById(root, "d.5.n.5.3");
                        //if (xelements9_3.IsFull())
                        //row6.FFR = xelements9_3.ToDecimalXElement();
                    }


                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "d.5.n.6");
                    //if (xelements10.IsFull())
                    //    row6.FFO = xelements10.ToDecimalXElement();
                    if (xelements10.IsFull())
                    {
                        IEnumerable<XElement> xelements10_1 = XmlUtils.XmlSearchById(root, "d.5.n.6.1");
                        //if (xelements10_1.IsFull())
                        //row6.FFO = xelements10_1.ToDecimalXElement();
                        IEnumerable<XElement> xelements10_2 = XmlUtils.XmlSearchById(root, "d.5.n.6.2");
                        if (xelements10_2.IsFull())
                        row6.FFO = xelements10_2.ToDecimalXElement();
                        IEnumerable<XElement> xelements10_3 = XmlUtils.XmlSearchById(root, "d.5.n.6.3");
                        //if (xelements10_3.IsFull())
                        //row6.FFO = xelements10_3.ToDecimalXElement();
                    }


                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "d.5.n.7");
                    //if (xelements12.IsFull())
                    //    row6.FSU = xelements12.ToDecimalXElement();
                    if (xelements12.IsFull())
                    {
                        IEnumerable<XElement> xelements12_1 = XmlUtils.XmlSearchById(root, "d.5.n.7.1");
                        //if (xelements12_1.IsFull())
                        //row6.FSU = xelements12_1.ToDecimalXElement();
                        IEnumerable<XElement> xelements12_2 = XmlUtils.XmlSearchById(root, "d.5.n.7.2");
                        if (xelements12_2.IsFull())
                        row6.FSU = xelements12_2.ToDecimalXElement();
                        IEnumerable<XElement> xelements12_3 = XmlUtils.XmlSearchById(root, "d.5.n.7.3");
                        //if (xelements12_3.IsFull())
                        //row6.FSU = xelements12_3.ToDecimalXElement();
                    }

                    // JACM Se da de baja el campo 2021-02-04
                    /*IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "d.5.n.9.1");
                    if (xelements13.IsFull())
                        row6.FOTVALOR = xelements13.ToDecimalXElement();
                    IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "d.5.n.9.2");
                    if (xelements14.IsFull())
                        row6.FOTDESCRIPCION = xelements14.ToStringXElement();*/
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "d.5.n.12");
                    if (xelements15.IsFull())
                        row6.VALCATASTRALTIERRA = xelements15.ToDecimalXElement();
                    dseAvaluo.FEXAVA_SUPERFICIE.AddFEXAVA_SUPERFICIERow(row6);
                }
            }
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(terreno, "d.6");
            if (xelements16.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TEINDIVISO = xelements16.ToDecimalXElement();
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(terreno, "d.7");
            if (xelements17.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODTOPOGRAFIA = xelements17.ToDecimalXElement();
            IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(terreno, "d.8");
            if (xelements18.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TECARACTERISTICASPARONAMICAS = xelements18.ToStringXElement();
            IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(terreno, "d.9");
            if (xelements19.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TECODDENSIDADHABITACIONAL = xelements19.ToDecimalXElement();
            IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(terreno, "d.10");
            if (xelements20.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TESERVIDUMBRESORESTRICCIONES = xelements20.ToStringXElement();
            return numList;
        }

        private void GuardarAvaluoElementosConstruccion(
          XElement elementosConstruccion,
          ref DseAvaluoMant dseAvaluo)
        {
            DseAvaluoMant.FEXAVA_ELEMENTOSCONSTRow row1 = (DseAvaluoMant.FEXAVA_ELEMENTOSCONSTRow)null;
            DseAvaluoMant.FEXAVA_OBRANEGRARow row2 = (DseAvaluoMant.FEXAVA_OBRANEGRARow)null;
            DseAvaluoMant.FEXAVA_REVESTIMIENTOACABADORow row3 = (DseAvaluoMant.FEXAVA_REVESTIMIENTOACABADORow)null;
            DseAvaluoMant.FEXAVA_CARPINTERIARow row4 = (DseAvaluoMant.FEXAVA_CARPINTERIARow)null;
            DseAvaluoMant.FEXAVA_INSTALACIONHIDSANRow row5 = (DseAvaluoMant.FEXAVA_INSTALACIONHIDSANRow)null;
            DseAvaluoMant.FEXAVA_PUERTASYVENTANERIARow row6 = (DseAvaluoMant.FEXAVA_PUERTASYVENTANERIARow)null;
            DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
            if (row1 == null)
                row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
            row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            dseAvaluo.FEXAVA_ELEMENTOSCONST.AddFEXAVA_ELEMENTOSCONSTRow(row1);
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.1").Descendants<XElement>().IsFull())
            {
                row2 = dseAvaluo.FEXAVA_OBRANEGRA.NewFEXAVA_OBRANEGRARow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.1");
                if (xelements1.IsFull())
                    row2.CIMENTACION = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.2");
                if (xelements2.IsFull())
                    row2.ESTRUCTURA = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.3");
                if (xelements3.IsFull())
                    row2.MUROS = xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.4");
                if (xelements4.IsFull())
                    row2.ENTREPISOS = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.5");
                if (xelements5.IsFull())
                    row2.TECHOS = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.6");
                if (xelements6.IsFull())
                    row2.AZOTEAS = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.7");
                if (xelements7.IsFull())
                    row2.BARDAS = xelements7.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.2").Descendants<XElement>().IsFull())
            {
                row3 = dseAvaluo.FEXAVA_REVESTIMIENTOACABADO.NewFEXAVA_REVESTIMIENTOACABADORow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.1");
                if (xelements1.IsFull())
                    row3.APLANADOS = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.2");
                if (xelements2.IsFull())
                    row3.PLAFONES = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.3");
                if (xelements3.IsFull())
                    row3.LAMBRINES = xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.4");
                if (xelements4.IsFull())
                    row3.PISOS = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.5");
                if (xelements5.IsFull())
                    row3.ZOCLOS = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.6");
                if (xelements6.IsFull())
                    row3.ESCALERAS = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.7");
                if (xelements7.IsFull())
                    row3.PINTURA = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.8");
                if (xelements8.IsFull())
                    row3.RECUBRIMIENTOSESPECIALES = xelements8.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.3").Descendants<XElement>().IsFull())
            {
                row4 = dseAvaluo.FEXAVA_CARPINTERIA.NewFEXAVA_CARPINTERIARow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.1");
                if (xelements1.IsFull())
                    row4.PUERTASINTERIORES = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.2");
                if (xelements2.IsFull())
                    row4.GUARDAROPAS = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.3");
                if (xelements3.IsFull())
                    row4.MUEBLESEMPOTRADOSFIJOS = xelements3.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.4").Descendants<XElement>().IsFull())
            {
                row5 = dseAvaluo.FEXAVA_INSTALACIONHIDSAN.NewFEXAVA_INSTALACIONHIDSANRow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.1");
                if (xelements1.IsFull())
                    row5.MUEBLESBANO = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.2");
                if (xelements2.IsFull())
                    row5.RAMALEOSHIDRAULICOS = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.3");
                if (xelements3.IsFull())
                    row5.RAMALEOSSANITARIOS = xelements3.ToStringXElement();
            }
            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(elementosConstruccion, "f.16");
            if (xelements9.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].IEYALUMBRADO = xelements9.ToStringXElement();
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.5").Descendants<XElement>().IsFull())
            {
                row6 = dseAvaluo.FEXAVA_PUERTASYVENTANERIA.NewFEXAVA_PUERTASYVENTANERIARow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.5.1");
                if (xelements1.IsFull())
                    row6.HERRERIA = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.5.2");
                if (xelements2.IsFull())
                    row6.VENTANERIA = xelements2.ToStringXElement();
            }
            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(elementosConstruccion, "f.6");
            if (xelements10.IsFull())
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.VIDRERIA = xelements10.ToStringXElement();
            }
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(elementosConstruccion, "f.7");
            if (xelements11.IsFull())
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.CERRAJERIA = xelements11.ToStringXElement();
            }
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(elementosConstruccion, "f.8");
            if (xelements12.IsFull())
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.FACHADAS = xelements12.ToStringXElement();
            }
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.9.1");
            if (elementos1.IsFull())
            {
                foreach (XElement root in elementos1)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.9.1.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(xelements1.ToStringXElement()).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M; //codinstespeciales;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.9.1.n.3");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.9.1.n.4");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = xelements3.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.9.1.n.5");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = xelements4.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.9.1.n.7");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = xelements5.ToDecimalXElement();
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "P";
                        row7.IDAVALUO = dseAvaluo.FEXAVA_AVALUO[0].IDAVALUO;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.9.2");
            if (elementos2.IsFull())
            {
                foreach (XElement root in elementos2)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.9.2.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(xelements1.ToStringXElement()).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M; //codinstespeciales;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.9.2.n.3");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.9.2.n.4");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = xelements3.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.9.2.n.5");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = xelements4.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.9.2.n.7");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = xelements5.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.9.2.n.10");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.TEINDIVISO = 0M;//xelements6.ToDecimalXElement();
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "C";
                        row7.IDAVALUO = dseAvaluo.FEXAVA_AVALUO[0].IDAVALUO;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.10.1");
            if (elementos3.IsFull())
            {
                foreach (XElement root in elementos3)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.10.1.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(xelements1.ToStringXElement()).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES= 0M;//= codinstespeciales;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.10.1.n.4");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = xelements2.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.10.1.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.10.1.n.5");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = xelements4.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.10.1.n.7");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = xelements5.ToDecimalXElement();
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "P";
                        row7.IDAVALUO = dseAvaluo.FEXAVA_AVALUO[0].IDAVALUO;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos4 = XmlUtils.XmlSearchById(elementosConstruccion, "f.10.2");
            if (elementos4.IsFull())
            {
                foreach (XElement root in elementos4)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.10.2.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(xelements1.ToStringXElement()).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;// codinstespeciales;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.10.2.n.3");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.10.2.n.4");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = xelements3.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.10.2.n.5");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = xelements4.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.10.2.n.7");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = xelements5.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.10.2.n.10");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.TEINDIVISO = xelements6.ToDecimalXElement();
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "C";
                        row7.IDAVALUO = dseAvaluo.FEXAVA_AVALUO[0].IDAVALUO;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos5 = XmlUtils.XmlSearchById(elementosConstruccion, "f.11.1");
            if (elementos5.IsFull())
            {
                foreach (XElement root in elementos5)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.11.1.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(xelements1.ToStringXElement()).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;/// codinstespeciales;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.11.1.n.3");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.11.1.n.4");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = xelements3.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.11.1.n.5");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = xelements4.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.11.1.n.7");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = xelements5.ToDecimalXElement();
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "P";
                        row7.IDAVALUO = dseAvaluo.FEXAVA_AVALUO[0].IDAVALUO;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos6 = XmlUtils.XmlSearchById(elementosConstruccion, "f.11.2");
            if (elementos6.IsFull())
            {
                foreach (XElement root in elementos6)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.11.2.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(xelements1.ToStringXElement()).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;// codinstespeciales;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.11.2.n.4");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = xelements2.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.11.2.n.5");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = xelements3.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.11.2.n.7");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = xelements4.ToDecimalXElement();
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.11.2.n.3");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements5.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.11.2.n.10");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.TEINDIVISO = xelements6.ToDecimalXElement();
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "C";
                        row7.IDAVALUO = dseAvaluo.FEXAVA_AVALUO[0].IDAVALUO;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            if (row2 != null)
            {
                row2.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_OBRANEGRA.AddFEXAVA_OBRANEGRARow(row2);
            }
            if (row3 != null)
            {
                row3.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_REVESTIMIENTOACABADO.AddFEXAVA_REVESTIMIENTOACABADORow(row3);
            }
            if (row4 != null)
            {
                row4.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_CARPINTERIA.AddFEXAVA_CARPINTERIARow(row4);
            }
            if (row5 != null)
            {
                row5.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_INSTALACIONHIDSAN.AddFEXAVA_INSTALACIONHIDSANRow(row5);
            }
            if (row6 != null)
            {
                row6.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_PUERTASYVENTANERIA.AddFEXAVA_PUERTASYVENTANERIARow(row6);
            }
            if (!dseAvaluo.FEXAVA_ELEMENTOSEXTRA.Any<DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow>())
                return;
            foreach (DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow elementosextraRow in (TypedTableBase<DseAvaluoMant.FEXAVA_ELEMENTOSEXTRARow>)dseAvaluo.FEXAVA_ELEMENTOSEXTRA)
                elementosextraRow.FEXAVA_ELEMENTOSCONSTRow = row1;
        }

        private void GuardarAvaluoEnfoqueMercado(
          XElement data,
          XElement enfoqueMercado,
          ref DseAvaluoMant dseAvaluo)
        {
            DseAvaluoMant.FEXAVA_DATOSTERRENOSRow datosterrenosRow = (DseAvaluoMant.FEXAVA_DATOSTERRENOSRow)null;
            DseAvaluoMant.FEXAVA_CONSTRUCCIONESMERRow construccionesmerRow = (DseAvaluoMant.FEXAVA_CONSTRUCCIONESMERRow)null;
            DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow = (DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow)null;
            bool flag1 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.1").IsFull();
            bool flag2 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3").IsFull();
            if (flag1)
            {
                DseAvaluoMant.FEXAVA_TERRENOMERCADORow row1 = dseAvaluo.FEXAVA_TERRENOMERCADO.NewFEXAVA_TERRENOMERCADORow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.1");
                if (xelements1.IsFull())
                    row1.VALORUNITARIOTIERRAPROMEDIO = xelements1.ToDecimalXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.2");
                if (xelements2.IsFull())
                    row1.VALORUNITARIOTIERRAHOMOLOGADO = xelements2.ToDecimalXElement();
                row1.CODTIPOTERRENO = "D";
                dseAvaluo.FEXAVA_TERRENOMERCADO.AddFEXAVA_TERRENOMERCADORow(row1);
                IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.1");
                if (elementos.IsFull())
                {
                    foreach (XElement root in elementos)
                    {
                        DseAvaluoMant.FEXAVA_DATOSTERRENOSRow row2 = dseAvaluo.FEXAVA_DATOSTERRENOS.NewFEXAVA_DATOSTERRENOSRow();
                        row2.FEXAVA_TERRENOMERCADORow = row1;
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "h.1.1.n.1");
                        if (xelements3.IsFull())
                            row2.CALLE = xelements3.ToStringXElement();
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "h.1.1.n.3");
                        if (xelements4.IsFull())
                        {
                            string stringXelement = xelements4.ToStringXElement();
                            row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "h.1.1.n.2");
                            if (xelements5.IsFull())
                                row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements5.ToStringXElement(), stringXelement);
                        }
                        IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "h.1.1.n.4");
                        if (xelements6.IsFull())
                            row2.CODIGOPOSTAL = xelements6.ToStringXElement();
                        IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.1.1.n.5.1");
                        if (xelements7.IsFull())
                            row2.TELEFONO = xelements7.ToStringXElement();
                        IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.1.1.n.5.2");
                        if (xelements8.IsFull())
                            row2.INFORMANTE = xelements8.ToStringXElement();
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.1.1.n.6");
                        if (xelements9.IsFull())
                            row2.DESCRIPCION = xelements9.ToStringXElement();
                        IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.1.1.n.7");
                        if (xelements10.IsFull())
                            row2.USOSUELO = xelements10.ToStringXElement();
                        IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.1.1.n.8");
                        if (xelements11.IsFull())
                            row2.CUS = xelements11.ToDecimalXElement();
                        IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "h.1.1.n.9");
                        if (xelements12.IsFull())
                            row2.SUPERFICIE = xelements12.ToDecimalXElement();


                        //TODO : Asignar valores

                        IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "h.1.1.n.10");
                        //if (xelements13.IsFull())
                        //    row2.FZO = xelements13.ToDecimalXElement();
                        if (xelements13.IsFull())
                        {
                            //IEnumerable<XElement> xelements13_1 = XmlUtils.XmlSearchById(root, "h.1.1.n.10.1");
                            //if (xelements13_1.IsFull())
                            //row2.FZO = xelements13_1.ToDecimalXElement();
                            IEnumerable<XElement> xelements13_2 = XmlUtils.XmlSearchById(root, "h.1.1.n.10.2");
                            if (xelements13_2.IsFull())
                            row2.FZO = xelements13_2.ToDecimalXElement();
                           
                        }


                        IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "h.1.1.n.11");
                        //if (xelements14.IsFull())
                        //    row2.FUB = xelements14.ToDecimalXElement();
                        if (xelements14.IsFull())
                        {
                            //IEnumerable<XElement> xelements14_1 = XmlUtils.XmlSearchById(root, "h.1.1.n.11.1");
                            //if (xelements14_1.IsFull())
                            //row2.FZO = xelements14_1.ToDecimalXElement();
                            IEnumerable<XElement> xelements14_2 = XmlUtils.XmlSearchById(root, "h.1.1.n.11.2");
                            if (xelements14_2.IsFull())
                            row2.FZO = xelements14_2.ToDecimalXElement();
                            
                        }


                        IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "h.1.1.n.12");
                        //if (xelements15.IsFull())
                        //    row2.FFR = xelements15.ToDecimalXElement();
                        if (xelements15.IsFull())
                        {
                            //IEnumerable<XElement> xelements15_1 = XmlUtils.XmlSearchById(root, "h.1.1.n.12.1");
                            //if (xelements15_1.IsFull())
                            //row2.FZO = xelements15_1.ToDecimalXElement();
                            IEnumerable<XElement> xelements15_2 = XmlUtils.XmlSearchById(root, "h.1.1.n.12.2");
                            if (xelements15_2.IsFull())
                            row2.FZO = xelements15_2.ToDecimalXElement();
                            
                        }
                        
                        
                        IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "h.1.1.n.13");
                        //if (xelements16.IsFull())
                        //    row2.FFO = xelements16.ToDecimalXElement();
                        if (xelements16.IsFull())
                        {
                            //IEnumerable<XElement> xelements16_1 = XmlUtils.XmlSearchById(root, "h.1.1.n.13.1");
                            //if (xelements16_1.IsFull())
                            //row2.FZO = xelements16_1.ToDecimalXElement();
                            IEnumerable<XElement> xelements16_2 = XmlUtils.XmlSearchById(root, "h.1.1.n.13.2");
                            if (xelements16_2.IsFull())
                            row2.FZO = xelements16_2.ToDecimalXElement();
                           
                        }

                        IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root, "h.1.1.n.14");
                        //if (xelements17.IsFull())
                        //    row2.FSU = xelements17.ToDecimalXElement();
                        if (xelements17.IsFull())
                        {
                           // IEnumerable<XElement> xelements17_1 = XmlUtils.XmlSearchById(root, "h.1.1.n.14.1");
                            //if (xelements17_1.IsFull())
                            //row2.FZO = xelements17_1.ToDecimalXElement();
                            IEnumerable<XElement> xelements17_2 = XmlUtils.XmlSearchById(root, "h.1.1.n.14.2");
                            if (xelements17_2.IsFull())
                            row2.FZO = xelements17_2.ToDecimalXElement();
                           
                        }


                        // JACM Se da de baja el campo 2021-02-04
                        /*IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(root, "h.1.1.n.18.1");
                        if (xelements18.IsFull())
                            row2.FOTVALOR = xelements18.ToDecimalXElement();
                        IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(root, "h.1.1.n.18.2");
                        if (xelements19.IsFull())
                            row2.FOTDESCRIPCION = xelements19.ToStringXElement();*/
                        IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(root, "h.1.1.n.15");
                        if (xelements20.IsFull())
                            row2.PRECIOSOLICITADO = xelements20.ToDecimalXElement();
                        dseAvaluo.FEXAVA_DATOSTERRENOS.AddFEXAVA_DATOSTERRENOSRow(row2);
                        datosterrenosRow = (DseAvaluoMant.FEXAVA_DATOSTERRENOSRow)null;
                    }
                }
            }
            if (flag2)
            {
                DseAvaluoMant.FEXAVA_TERRENOMERCADORow row1 = dseAvaluo.FEXAVA_TERRENOMERCADO.NewFEXAVA_TERRENOMERCADORow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.1");
                if (xelements1.IsFull())
                    row1.VALORUNITARIOTIERRAPROMEDIO = xelements1.ToDecimalXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.2");
                if (xelements2.IsFull())
                    row1.VALORUNITARIOTIERRAHOMOLOGADO = xelements2.ToDecimalXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.4");
                if (xelements3.IsFull())
                    row1.VALORUNITARIORESIDUAL = xelements3.ToDecimalXElement();
                row1.CODTIPOTERRENO = "R";
                dseAvaluo.FEXAVA_TERRENOMERCADO.AddFEXAVA_TERRENOMERCADORow(row1);
                IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.4");
                if (elementos.IsFull())
                {
                    foreach (XElement root in elementos)
                    {
                        DseAvaluoMant.FEXAVA_DATOSTERRENOSRow row2 = dseAvaluo.FEXAVA_DATOSTERRENOS.NewFEXAVA_DATOSTERRENOSRow();
                        row2.FEXAVA_TERRENOMERCADORow = row1;
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.1");
                        if (xelements4.IsFull())
                            row2.CALLE = xelements4.ToStringXElement();
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.3");
                        if (xelements5.IsFull())
                        {
                            string stringXelement = xelements5.ToStringXElement();
                            row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.2");
                            if (xelements6.IsFull())
                                row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements6.ToStringXElement(), stringXelement);
                        }
                        IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.4");
                        if (xelements7.IsFull())
                            row2.CODIGOPOSTAL = xelements7.ToStringXElement();
                        IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.5.1");
                        if (xelements8.IsFull())
                            row2.TELEFONO = xelements8.ToStringXElement();
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.5.2");
                        if (xelements9.IsFull())
                            row2.INFORMANTE = xelements9.ToStringXElement();
                        IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.6");
                        if (xelements10.IsFull())
                            row2.DESCRIPCION = xelements10.ToStringXElement();
                        IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.7");
                        if (xelements11.IsFull())
                            row2.SUPERFICIE = xelements11.ToDecimalXElement();
                        IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.8");
                        if (xelements12.IsFull())
                            row2.PRECIOSOLICITADO = xelements12.ToDecimalXElement();
                        dseAvaluo.FEXAVA_DATOSTERRENOS.AddFEXAVA_DATOSTERRENOSRow(row2);
                        datosterrenosRow = (DseAvaluoMant.FEXAVA_DATOSTERRENOSRow)null;
                    }
                }
            }
            IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.4");
            if (xelements21.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORUNITARIOTIERRAAVALUO = xelements21.ToDecimalXElement();
            DseAvaluoMant.FEXAVA_CONSTRUCCIONESMERRow row3 = dseAvaluo.FEXAVA_CONSTRUCCIONESMER.NewFEXAVA_CONSTRUCCIONESMERRow();
            row3.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            row3.IDMODOCONSTRUCCION = "V";
            IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.1");
            if (xelements22.IsFull())
                row3.VALORUNITARIOPROMEDIO = xelements22.ToDecimalXElement();
            IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.2");
            if (xelements23.IsFull())
                row3.VALORUNITARIOHOMOLOGADO = xelements23.ToDecimalXElement();
            IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.7");
            if (xelements24.IsFull())
                row3.VALORUNITARIOAPLICABLE = xelements24.ToDecimalXElement();
            dseAvaluo.FEXAVA_CONSTRUCCIONESMER.AddFEXAVA_CONSTRUCCIONESMERRow(row3);
            construccionesmerRow = (DseAvaluoMant.FEXAVA_CONSTRUCCIONESMERRow)null;
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1");
            if (elementos1.IsFull())
            {
                foreach (XElement root in elementos1)
                {
                    DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow row1 = dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.NewFEXAVA_INVESTPRODUCTOSCOMPRow();
                    row1.FEXAVA_CONSTRUCCIONESMERRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER[0];
                    row1.CODTIPOCOMPARABLE = "V";
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "h.2.1.n.1");
                    if (xelements1.IsFull())
                        row1.CALLE = xelements1.ToStringXElement();
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "h.2.1.n.3");
                    if (xelements2.IsFull())
                    {
                        string stringXelement = xelements2.ToStringXElement();
                        row1.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "h.2.1.n.2");
                        if (xelements3.IsFull())
                            row1.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements3.ToStringXElement(), stringXelement);
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "h.2.1.n.4");
                    if (xelements4.IsFull())
                        row1.CODIGOPOSTAL = xelements4.ToStringXElement();
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "h.2.1.n.5.1");
                    if (xelements5.IsFull())
                        row1.TELEFONO = xelements5.ToStringXElement();
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "h.2.1.n.5.2");
                    if (xelements6.IsFull())
                        row1.INFORMANTE = xelements6.ToStringXElement();
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.2.1.n.6");
                    if (xelements7.IsFull())
                        row1.DESCRIPCION = xelements7.ToStringXElement();
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.2.1.n.7");
                    if (xelements8.IsFull())
                        row1.SUPERFICIEVENDIBLEPORUNIDAD = xelements8.ToDecimalXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.2.1.n.8");
                    if (xelements9.IsFull())
                        row1.PRECIOSOLICITADO = xelements9.ToDecimalXElement();
                    if (XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.1");
                        if (xelements3.IsFull())
                            row1.REGION = xelements3.ToStringXElement();
                        IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.2");
                        if (xelements10.IsFull())
                            row1.MANZANA = xelements10.ToStringXElement();
                        IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.3");
                        if (xelements11.IsFull())
                            row1.LOTE = xelements11.ToStringXElement();
                        IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.4");
                        if (xelements12.IsFull())
                            row1.UNIDADPRIVATIVA = xelements12.ToStringXElement();
                    }
                    dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.AddFEXAVA_INVESTPRODUCTOSCOMPRow(row1);
                    investproductoscompRow = (DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow)null;
                }
            }
            IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(enfoqueMercado, "h.3");
            if (xelements25.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVALORMERCADO = xelements25.ToDecimalXElement();
            DseAvaluoMant.FEXAVA_CONSTRUCCIONESMERRow row4 = dseAvaluo.FEXAVA_CONSTRUCCIONESMER.NewFEXAVA_CONSTRUCCIONESMERRow();
            row4.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.1");
            if (xelements26.IsFull())
                row4.VALORUNITARIOPROMEDIO = xelements26.ToDecimalXElement();
            IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.2");
            if (xelements27.IsFull())
                row4.VALORUNITARIOHOMOLOGADO = xelements27.ToDecimalXElement();
            IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.7");
            if (xelements28.IsFull())
                row4.VALORUNITARIOAPLICABLE = xelements28.ToDecimalXElement();
            row4.IDMODOCONSTRUCCION = "R";
            dseAvaluo.FEXAVA_CONSTRUCCIONESMER.AddFEXAVA_CONSTRUCCIONESMERRow(row4);
            construccionesmerRow = (DseAvaluoMant.FEXAVA_CONSTRUCCIONESMERRow)null;
            if (this.esTerrenoValdio(data))
                return;
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.1");
            if (!elementos2.IsFull())
                return;
            foreach (XElement root in elementos2)
            {
                DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow row1 = dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.NewFEXAVA_INVESTPRODUCTOSCOMPRow();
                row1.FEXAVA_CONSTRUCCIONESMERRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER[1];
                row1.CODTIPOCOMPARABLE = "R";
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "h.4.1.n.1");
                if (xelements1.IsFull())
                    row1.CALLE = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "h.4.1.n.3");
                if (xelements2.IsFull())
                {
                    string stringXelement = xelements2.ToStringXElement();
                    row1.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "h.4.1.n.2");
                    if (xelements3.IsFull())
                        row1.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements3.ToStringXElement(), stringXelement);
                }
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "h.4.1.n.4");
                if (xelements4.IsFull())
                    row1.CODIGOPOSTAL = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "h.4.1.n.5.1");
                if (xelements5.IsFull())
                    row1.TELEFONO = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "h.4.1.n.5.2");
                if (xelements6.IsFull())
                    row1.INFORMANTE = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.4.1.n.7");
                if (xelements7.IsFull())
                    row1.SUPERFICIEVENDIBLEPORUNIDAD = xelements7.ToDecimalXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.4.1.n.8");
                if (xelements8.IsFull())
                    row1.PRECIOSOLICITADO = xelements8.ToDecimalXElement();
                if (XmlUtils.XmlSearchById(root, "h.4.1.n.10").IsFull())
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.1");
                    if (xelements3.IsFull())
                        row1.REGION = xelements3.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.2");
                    if (xelements9.IsFull())
                        row1.MANZANA = xelements9.ToStringXElement();
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.3");
                    if (xelements10.IsFull())
                        row1.LOTE = xelements10.ToStringXElement();
                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.4");
                    if (xelements11.IsFull())
                        row1.UNIDADPRIVATIVA = xelements11.ToStringXElement();
                }
                dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.AddFEXAVA_INVESTPRODUCTOSCOMPRow(row1);
                investproductoscompRow = (DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow)null;
            }
        }

        private void GuardarAvaluoEnfoqueCostosComercial(
          XElement enfoqueCostosComercial,
          ref DseAvaluoMant dseAvaluo)
        {
            IEnumerable<XElement> xelements = XmlUtils.XmlSearchById(enfoqueCostosComercial, "i.6");
            if (!xelements.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].IMPORTETOTALENFCOSTOS = XmlUtils.ToDecimalXElementAv(xelements);
        }

        private void GuardarAvaluoEnfoqueCostosCatastral(
          XElement enfoqueCostosCatastral,
          ref DseAvaluoMant dseAvaluo)
        {
            DseAvaluoMant.FEXAVA_ENFOQUECOSTESCATRow row = dseAvaluo.FEXAVA_ENFOQUECOSTESCAT.NewFEXAVA_ENFOQUECOSTESCATRow();
            row.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.4");
            if (xelements1.IsFull())
                row.IMPINSTALACIONESESPECIALES = xelements1.ToDecimalXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.5");
            if (xelements2.IsFull())
                row.IMPTOTVALORCATASTRAL = xelements2.ToDecimalXElement();
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.6");
            if (xelements3.IsFull())
                row.AVANCEOBRA = xelements3.ToDecimalXElement();
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.7");
            if (xelements4.IsFull())
                row.IMPTOTVALCATASTRALOBRAPROCESO = xelements4.ToDecimalXElement();
            dseAvaluo.FEXAVA_ENFOQUECOSTESCAT.AddFEXAVA_ENFOQUECOSTESCATRow(row);
        }

        private void GuardarAvaluoEnfoqueIngresos(
          XElement data,
          XElement enfoqueIngresos,
          ref DseAvaluoMant dseAvaluo)
        {
            if (this.esTerrenoValdio(data))
                return;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIRENTABRUTAMENSUAL = xelements1.ToDecimalXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.1");
            if (xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIVACIOS = xelements2.ToDecimalXElement();
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.2");
            if (xelements3.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTOPREDIAL = xelements3.ToDecimalXElement();
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.3");
            if (xelements4.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EISERVICIOAGUA = xelements4.ToDecimalXElement();
            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.4");
            if (xelements5.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EICONSERVACIONYMANTENIMIENTO = xelements5.ToDecimalXElement();
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.5");
            if (xelements6.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIENERGIAELECTRICA = xelements6.ToDecimalXElement();
            IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.6");
            if (xelements7.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIADMINISTRACION = xelements7.ToDecimalXElement();
            IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.7");
            if (xelements8.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EISEGUROS = xelements8.ToDecimalXElement();
            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.8");
            if (xelements9.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIDEPRECIACIONFISCAL = xelements9.ToDecimalXElement();
            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.9");
            if (xelements10.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIOTROS = xelements10.ToDecimalXElement();
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.10");
            if (xelements11.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESFISCALES = xelements11.ToDecimalXElement();
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.11");
            if (xelements12.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTORENTA = xelements12.ToDecimalXElement();
            IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.12");
            if (xelements13.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESMENSUALES = xelements13.ToDecimalXElement();
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.13");
            if (xelements14.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIPORCENTAJEDEDUCCIONESMENS = xelements14.ToDecimalXElement();
            IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.3");
            if (xelements15.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIPRODUCTOLIQUIDOANUAL = xelements15.ToDecimalXElement();
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.4");
            if (xelements16.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EITASACAPITALIZACION = xelements16.ToDecimalXElement();
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.5");
            if (!xelements17.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].EIIMPORTE = xelements17.ToDecimalXElement();
        }

        private void GuardarAvaluoResumenConclusionAvaluo(
          XElement conclusionAvaluo,
          ref DseAvaluoMant dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(conclusionAvaluo, "o.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORCOMERCIAL = xelements1.ToDecimalXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(conclusionAvaluo, "o.2");
            if (!xelements2.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].VALORCATASTRAL = xelements2.ToDecimalXElement();
        }

        private void GuardarAvaluoValorReferido(XElement valorReferido, ref DseAvaluoMant dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(valorReferido, "p.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FECHAVALORREFERIDO = xelements1.First<XElement>().Value.To<DateTime>();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(valorReferido, "p.5");
            if (!xelements2.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].VALORREFERIDO = xelements2.ToDecimalXElement();
        }

        private void GuardarAvaluoAnexoFotografico(
          SIGAPred.Common.DataAccess.OracleDataAccess.TransactionHelper transactionHelper,
          XElement anexoFotografico,
          ref DseAvaluoMant dseAvaluos)
        {
            string empty1 = string.Empty;
            int num1 = 1;
            AltasDocumentosTran altasDocumentosTran = new AltasDocumentosTran();
            if (XmlUtils.XmlSearchById(anexoFotografico, "q.1").IsFull())
            {
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.1");
                if (xelements1.IsFull())
                    empty1 += xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.2");
                if (xelements2.IsFull())
                    empty1 += xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.3");
                if (xelements3.IsFull())
                    empty1 += xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.4");
                if (xelements4.IsFull())
                    empty1 += xelements4.ToStringXElement();
                IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(anexoFotografico, "q.1.2");
                if (elementos.IsFull())
                {
                    foreach (XElement root in elementos)
                    {
                        DseAvaluoMant.FEXAVA_FOTOAVALUORow row = dseAvaluos.FEXAVA_FOTOAVALUO.NewFEXAVA_FOTOAVALUORow();
                        row.FEXAVA_AVALUORow = dseAvaluos.FEXAVA_AVALUO[0];
                        TipoFotoInmueble tipoFotoInm = TipoFotoInmueble.I;
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "q.1.2.n.2");
                        if (xelements5.IsFull())
                            tipoFotoInm = this.TipoInmueble(xelements5.First<XElement>());
                        IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "q.1.2.n.1");
                        if (xelements6.IsFull())
                        {
                            string nombreFichero = num1.ToString() + "_" + empty1 + ".jpg";
                            Decimal num2;
                            try
                            {
                                num2 = altasDocumentosTran.Tran_InsertFotoInmueble(transactionHelper, Convert.FromBase64String(xelements6.ToStringXElement()), nombreFichero, "Foto_" + nombreFichero, dseAvaluos.FEXAVA_AVALUO[0].FECHAAVALUO, tipoFotoInm, new Decimal?()).Value;
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicyWrapper.HandleException(ex);
                                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
                            }
                            ++num1;
                            xelements6.First<XElement>().SetValue((object)num2.ToString());
                            row.IDDOCUMENTOFOTO = num2;
                            dseAvaluos.FEXAVA_FOTOAVALUO.AddFEXAVA_FOTOAVALUORow(row);
                        }
                    }
                }
            }
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(anexoFotografico, "q.2");
            if (elementos1.IsFull())
            {
                foreach (XElement root1 in elementos1)
                {
                    List<string> source = new List<string>();
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.1");
                    if (xelements1.IsFull())
                        source.Add(xelements1.ToStringXElement());
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.2");
                    if (xelements2.IsFull())
                        source.Add(xelements2.ToStringXElement());
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.3");
                    if (xelements3.IsFull())
                        source.Add(xelements3.ToStringXElement());
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.4");
                    if (xelements4.IsFull())
                        source.Add(xelements4.ToStringXElement());
                    string empty2 = string.Empty;
                    for (int index = 0; index < source.Count; ++index)
                        empty2 += source[index];
                    int num2 = 1;
                    IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(root1, "q.2.n.2");
                    if (elementos2.IsFull())
                    {
                        foreach (XElement root2 in elementos2)
                        {
                            TipoFotoInmueble tipoFotoInm = TipoFotoInmueble.I;
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "q.2.n.2.n.2");
                            if (xelements5.IsFull())
                                tipoFotoInm = this.TipoInmueble(xelements5.First<XElement>());
                            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root2, "q.2.n.2.n.1");
                            if (xelements6.IsFull())
                            {
                                string nombreFichero = num2.ToString() + "_.jpg";
                                Decimal num3 = altasDocumentosTran.Tran_InsertFotoInmueble(transactionHelper, Convert.FromBase64String(xelements6.ToStringXElement()), nombreFichero, "Foto_" + nombreFichero, dseAvaluos.FEXAVA_AVALUO[0].FECHAAVALUO, tipoFotoInm, new Decimal?()).Value;
                                ++num2;
                                xelements6.First<XElement>().SetValue((object)num3.ToString());
                                if (source.Count<string>() == 4)
                                {
                                    string filterExpression = " REGION  = '" + source[0] + "' AND  MANZANA  = '" + source[1] + "' AND  LOTE  = '" + source[2] + "' AND  UNIDADPRIVATIVA  = '" + source[3] + "'";
                                    if (((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).Any<DataRow>())
                                    {
                                        DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow = (DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow)((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).First<DataRow>();
                                        DseAvaluoMant.FEXAVA_FOTOCOMPARABLERow row = dseAvaluos.FEXAVA_FOTOCOMPARABLE.NewFEXAVA_FOTOCOMPARABLERow();
                                        row.FEXAVA_INVESTPRODUCTOSCOMPRow = investproductoscompRow;
                                        row.IDDOCUMENTOFOTO = num3;
                                        dseAvaluos.FEXAVA_FOTOCOMPARABLE.AddFEXAVA_FOTOCOMPARABLERow(row);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            IEnumerable<XElement> elementos3 = XmlUtils.XmlSearchById(anexoFotografico, "q.3");
            if (!elementos3.IsFull())
                return;
            foreach (XElement root1 in elementos3)
            {
                List<string> source = new List<string>();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.1");
                if (xelements1.IsFull())
                    source.Add(xelements1.ToStringXElement());
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.2");
                if (xelements2.IsFull())
                    source.Add(xelements2.ToStringXElement());
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.3");
                if (xelements3.IsFull())
                    source.Add(xelements3.ToStringXElement());
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.4");
                if (xelements4.IsFull())
                    source.Add(xelements4.ToStringXElement());
                string empty2 = string.Empty;
                for (int index = 0; index < source.Count; ++index)
                    empty2 += source[index];
                int num2 = 1;
                IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(root1, "q.3.n.2");
                if (elementos2.IsFull())
                {
                    foreach (XElement root2 in elementos2)
                    {
                        TipoFotoInmueble tipoFotoInm = TipoFotoInmueble.I;
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "q.3.n.2.n.2");
                        if (xelements5.IsFull())
                            tipoFotoInm = this.TipoInmueble(xelements5.First<XElement>());
                        IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root2, "q.3.n.2.n.1");
                        if (xelements6.IsFull())
                        {
                            string nombreFichero = num2.ToString() + "_" + empty2 + ".jpg";
                            Decimal num3 = altasDocumentosTran.Tran_InsertFotoInmueble(transactionHelper, Convert.FromBase64String(xelements6.ToStringXElement()), nombreFichero, "Foto_" + nombreFichero, dseAvaluos.FEXAVA_AVALUO[0].FECHAAVALUO, tipoFotoInm, new Decimal?()).Value;
                            ++num2;
                            xelements6.First<XElement>().SetValue((object)num3.ToString());
                            if (source.Count<string>() == 4)
                            {
                                string filterExpression = " REGION  = '" + source[0] + "' AND  MANZANA  = '" + source[1] + "' AND  LOTE  = '" + source[2] + "' AND  UNIDADPRIVATIVA  = '" + source[3] + "'";
                                if (((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).Any<DataRow>())
                                {
                                    DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow = (DseAvaluoMant.FEXAVA_INVESTPRODUCTOSCOMPRow)((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).First<DataRow>();
                                    DseAvaluoMant.FEXAVA_FOTOCOMPARABLERow row = dseAvaluos.FEXAVA_FOTOCOMPARABLE.NewFEXAVA_FOTOCOMPARABLERow();
                                    row.FEXAVA_INVESTPRODUCTOSCOMPRow = investproductoscompRow;
                                    row.IDDOCUMENTOFOTO = num3;
                                    dseAvaluos.FEXAVA_FOTOCOMPARABLE.AddFEXAVA_FOTOCOMPARABLERow(row);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void RegistrarIntentoFallido(
          DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable par_intentoFallidoDT)
        {
            try
            {
                this.IntentoFallidoTA.Update(par_intentoFallidoDT);
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable EsValidoAvaluo(
          byte[] xmlAvaluoByteComprimido,
          int idPersona,
          bool esPerito)
        {

            XmlDocument xmlAvaluo = new XmlDocument();
            byte[] buffer = SIGAPred.Common.Compresion.Compresion.Descomprimir(xmlAvaluoByteComprimido);
            DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable errorDT = new DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable();
            XElementUtils.CultureInfoConfigured = new CultureInfo("es-MX");
            StringBuilder avaluoValidateMessage = new StringBuilder();
            
            try
            {
                xmlAvaluo.Load((Stream)new MemoryStream(buffer));
                AvaluosUtils.EliminarEspaciosBlancos(ref xmlAvaluo);
                XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;

                try
                {
                this.ValidarEsquema(xmlAvaluo);
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                log("EsValidoAvaluo ValidarEsquema FaultException ", ex.Message, ex.StackTrace);
                this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, string.Empty);
            }
            //if ((Decimal)avaluoValidateMessage.Length == 0M)
            //{
                string stringXelement = XmlUtils.XmlSearchById(root, "a.1").ToStringXElement();
                try
                {
                    this.ValidarExisteAvaluoRegistrado(stringXelement, (Decimal)idPersona, esPerito);
                }
                catch (FaultException<AvaluosInfoException> ex)
                {
                    log("EsValidoAvaluo ValidarExisteAvaluoRegistrado FaultException ", ex.Message, ex.StackTrace);
                    avaluoValidateMessage.AppendLine("@TIPO_ERROR:Nº AVALÚO");
                    avaluoValidateMessage.AppendLine("a.1 - " + ex.Detail.Descripcion);
                    avaluoValidateMessage.AppendLine();
                }
                IEnumerable<XElement> xelements = XmlUtils.XmlSearchById(root, "a.2");
                if (xelements.IsFull() && !XmlUtils.EsFechaMenorA5Anios(xelements))
                {
                    string str = "a.2 - La fecha de el avaluo no debe ser mayor a 5 años";
                    avaluoValidateMessage.AppendLine(str);
                }

                try
                {
                    this.ValidarVigenciaPeritoSociedad(idPersona, xmlAvaluo, esPerito);
                }
                catch (FaultException<AvaluosInfoException> ex)
                {
                    log("EsValidoAvaluo ValidarVigenciaPeritoSociedad FaultException ", ex.Message, ex.StackTrace);
                    avaluoValidateMessage.AppendLine("@TIPO_ERROR:PERITO Y SOCIEDAD");
                    avaluoValidateMessage.AppendLine(ex.Detail.Descripcion);
                    avaluoValidateMessage.AppendLine();
                }
                try
                {
                    this.ValidarCuentaCatastral(xmlAvaluo);
                }
                catch (FaultException<AvaluosInfoException> ex)
                {
                    log("EsValidoAvaluo ValidarCuentaCatastral FaultException ", ex.Message, ex.StackTrace);
                    this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, "@TIPO_ERROR:CUENTA CATASTRAL");
                }


                if ((Decimal)avaluoValidateMessage.Length == 0M)
                {
                   
                    try
                    {
                        this.ValidarAnexoFotografico(xmlAvaluo);
                    }
                    catch (FaultException<AvaluosInfoException> ex)
                    {
                        log("EsValidoAvaluo ValidarAnexoFotografico FaultException ", ex.Message, ex.StackTrace);
                        this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, "@TIPO_ERROR:IMAGEN");
                    }
                    try
                    {
                        this.ValidarMacroMicroLocalizacion(xmlAvaluo);
                    }
                    catch (FaultException<AvaluosInfoException> ex)
                    {
                        log("EsValidoAvaluo ValidarMacroMicroLocalizacion FaultException ", ex.Message, ex.StackTrace);
                        this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, "@TIPO_ERROR:IMAGEN");
                    }
                    try
                    {
                        this.ValidarEnfoqueCostosComercial(xmlAvaluo);
                    }
                    catch (FaultException<AvaluosInfoException> ex)
                    {
                        log("EsValidoAvaluo ValidarEnfoqueCostosComercial FaultException ", ex.Message, ex.StackTrace);
                        this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, "@TIPO_ERROR:ENFOQUE COSTOS");
                    }
                    try
                    {
                        this.ValidarCaracteristicasUrbanas(xmlAvaluo);
                    }
                    catch (FaultException<AvaluosInfoException> ex)
                    {
                        log("EsValidoAvaluo ValidarCaracteristicasUrbanas FaultException ", ex.Message, ex.StackTrace);
                        this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, "@TIPO_ERROR:CARACTERÍSTICAS URBANAS");
                    }
                    try
                    {
                        this.ValidarValoresCalculados(xmlAvaluo);
                    }
                    catch (FaultException<AvaluosInfoException> ex)
                    {
                        log("EsValidoAvaluo ValidarValoresCalculados FaultException ", ex.Message, ex.StackTrace);
                        this.AnadirErrorValidacionALista(ref avaluoValidateMessage, ex.Detail.Descripcion, "@TIPO_ERROR:CAMPOS CALCULADOS");
                    }
                }
            //}
            
            }catch(Exception ex)
            {
                log("EsValidoAvaluo Exception ", ex.Message, ex.StackTrace);
            }
            if (avaluoValidateMessage.Length > 0)
                this.AddErrorToDT(ref errorDT, avaluoValidateMessage.ToString());
            return errorDT;

        }

        public bool ValidarTamanioFichero(int bytesXmlAvaluo)
        {
            try
            {
                bool flag = true;
                /*int num = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaxLengthUploadFile")) * 1024;
                if (bytesXmlAvaluo > num || (Decimal)bytesXmlAvaluo == 0M)
                    flag = false;*/
                return flag;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        private void ValidarEsquema(XmlDocument xmlAvaluo)
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag3 = true;
            bool flag4 = true;
            StringBuilder stringBuilder = new StringBuilder();
            XmlValidator xmlValidator = new XmlValidator(Resources.EsquemaAvaluo, xmlAvaluo.InnerXml);
            stringBuilder.AppendLine(xmlValidator.Message);
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            try
            {
                this.ValidarTipoPersona(root);
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                log("ValidarEsquema ValidarTipoPersona", ex.Message,ex.StackTrace);
                flag3 = false;
                stringBuilder.AppendLine("@TIPO_ERROR:ESQUEMA - DOCUMENTO NO VÁLIDO");
                stringBuilder.AppendLine(ex.Detail.Descripcion);
            }
            try
            {
                this.ValidarDelegacionesColonias(root);
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                log("ValidarEsquema ValidarDelegacionesColonias", ex.Message, ex.StackTrace);
                flag1 = false;
                stringBuilder.AppendLine("@TIPO_ERROR:ESQUEMA - DOCUMENTO NO VÁLIDO");
                stringBuilder.AppendLine(ex.Detail.Descripcion);
            }
            try
            {
                this.ValidarValorReferido(root);
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                log("ValidarEsquema ValidarValorReferido", ex.Message, ex.StackTrace);
                flag2 = false;
                stringBuilder.AppendLine("@TIPO_ERROR:ESQUEMA - DOCUMENTO NO VÁLIDO");
                stringBuilder.AppendLine(ex.Detail.Descripcion);
            }
            try
            {
                this.ValidarUsoBaldio(root);
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                log("ValidarEsquema ValidarUsoBaldio ", ex.Message, ex.StackTrace);
                flag4 = false;
                stringBuilder.AppendLine("@TIPO_ERROR:ESQUEMA - DOCUMENTO NO VÁLIDO");
                stringBuilder.AppendLine(ex.Detail.Descripcion);
            }
            if (!xmlValidator.IsValid || !flag2 || (!flag1 || !flag3) || !flag4)
            {
                log("ValidarEsquema", "Valores: "+xmlValidator.IsValid.ToString()+
                    "\n\r | ValidarDelegacionesColonias flag1: " + flag1.ToString() +
                    "\n\r | ValidarValorReferido        flag2: " + flag2.ToString() +
                    "\n\r | ValidarTipoPersona          flag3: " + flag3.ToString() +
                    "\n\r | ValidarUsoBaldio            flag4: " + flag4.ToString() 
                    , stringBuilder.ToString());
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
            }
        }

        private void ValidarUsoBaldio(XElement data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<XElement> rootN = XmlUtils.XmlSearchById(data, "e.2");
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(rootN, "e.2.1");
            bool flag1 = true;
            
            bool esComercial = (Decimal)data.Descendants((XName)"Comercial").Count<XElement>() > 0M;
            if (elementos1.IsFull())
            {
                foreach (XElement xelement in elementos1)
                {
                    if (this.usoNoBaldioConSuper(xelement, true))
                    {
                        string stringXelement = XmlUtils.XmlSearchById(xelement, "e.2.1.n.2").ToStringXElement();
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.1").IsFull())
                        {
                            string str = stringXelement == "W" ? "e.2.1.n.1 Campo obligatorio para el uso baldio" : "e.2.1.n.1 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                            flag1 = false;
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.3").IsFull())
                        {
                            string str = stringXelement == "W" ? "e.2.1.n.3 Campo obligatorio para el uso baldio" : "e.2.1.n.3 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                            flag1 = false;
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.4").IsFull())
                        {
                            string str = stringXelement == "W" ? "e.2.1.n.4 Campo obligatorio para el uso baldio" : "e.2.1.n.4 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                            flag1 = false;
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.5").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.1.n.5 Campo obligatorio para el uso baldio" : "e.2.1.n.5 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.6").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.1.n.6 Campo obligatorio para el uso baldio" : "e.2.1.n.6 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }

                        
                        //if (!esComercial) //flag2 Es Comercial, e.2.1.n.7 se elimina de Comercial
                        //{
                            IEnumerable<XElement> xelements = XmlUtils.XmlSearchById(xelement, "e.2.1.n.7");
                            if (!xelements.IsFull())
                            {
                                    flag1 = false;
                                    string str = //stringXelement == "W" ? "e.2.1.n.7 Campo obligatorio para el uso baldio" : 
                                    "e.2.1.n.7 Campo obligatorio";
                                    stringBuilder.AppendLine(str);
                            }
                            else if (xelements.Count<XElement>() != 0 && !XmlUtils.EsDecimalXmlValido(xelements))
                            {
                                flag1 = false;
                                string str = "e.2.1.n.7 El dato no es correcto, se requiere asignar un valor.";
                                stringBuilder.AppendLine(str);
                            }

                        //}
                        

                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.8").IsFull() && !esComercial && stringXelement != "W")
                        {
                            flag1 = false;
                            string str = "e.2.1.n.8 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (esComercial) { 
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.9").IsFull() && stringXelement != "H")
                        {
                            flag1 = false;
                            string str = //stringXelement == "W" ? "e.2.1.n.9 Campo obligatorio para el uso baldio" : 
                                "e.2.1.n.9 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        }
                        /// //JACM Se da de baja el campo 2021-02-04
                        /* if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.10").IsFull() && stringXelement != "H")
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.1.n.10 Campo obligatorio para el uso baldio" : "e.2.1.n.10 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }*/
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.11").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.1.n.11 Campo obligatorio para el uso baldio" : 
                                "e.2.1.n.11 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.12").IsFull() && esComercial)
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.1.n.12 Campo obligatorio para el uso baldio" : "e.2.1.n.12 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.13").IsFull() && esComercial && stringXelement != "H")
                        {
                            flag1 = false;
                            string str = //stringXelement == "W" ? "e.2.1.n.13 Campo obligatorio para el uso baldio" : 
                                "e.2.1.n.13 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        // JACM Se da de baja el campo 2021-02-04
                        /*if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.14").IsFull() && flag2 && stringXelement != "H")
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.1.n.14 Campo obligatorio para el uso baldio" : "e.2.1.n.14 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }*/
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.15").IsFull())
                        {
                            flag1 = false;
                            string str = //stringXelement == "W" ? "e.2.1.n.15 Campo obligatorio para el uso baldio" : 
                                "e.2.1.n.15 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.16").IsFull() && !esComercial && stringXelement != "W")
                        {
                            flag1 = false;
                            string str = "e.2.1.n.16 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!esComercial)
                        {
                            if (!XmlUtils.XmlSearchById(xelement, "e.2.1.n.17").IsFull()) //&& !esComercial)
                            {
                                flag1 = false;
                                
                                    string str = //stringXelement == "W" ? "e.2.1.n.17 Campo obligatorio para el uso baldio" : 
                                    "e.2.1.n.17 Campo obligatorio";
                                    stringBuilder.AppendLine(str);
                            }
                            else
                            {

                                string uso = XmlUtils.XmlSearchById(xelement, "e.2.1.n.2").ToStringXElement();
                                string clase = XmlUtils.XmlSearchById(xelement, "e.2.1.n.6").ToStringXElement();
                                string depreciacion = XmlUtils.XmlSearchById(xelement, "e.2.1.n.17").ToStringXElement();

                                log("ValidaXML e21n17 USO:", uso + " | CLASE:" + clase, " | Dep. : "+depreciacion);

                                if((uso=="P" || uso == "PE" || uso == "PC" || uso == "J" ) && clase=="U" && depreciacion != "1")
                                {
                                    stringBuilder.AppendLine(               "e.2.1.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1" );
                                    log("ValidarValoresCalculados e21n17 ", "e.2.1.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1", "");

                                }
                            }
                        }
                    }
                }
            }
            if (this.obligatorioPriv || !this.existeWP)
            {
                if (!XmlUtils.XmlSearchById(rootN, "e.2.2").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.2.2 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(rootN, "e.2.3").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.2.3 Campo obligatorio");
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(rootN, "e.2.5");
            if (elementos2.IsFull())
            {
                foreach (XElement xelement in elementos2)
                {
                    if (this.usoNoBaldioConSuper(xelement, false))
                    {
                        string stringXelement = XmlUtils.XmlSearchById(xelement, "e.2.5.n.2").ToStringXElement();
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.1").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.1 Campo obligatorio para el uso baldio" : "e.2.5.n.1 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.3").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.3 Campo obligatorio para el uso baldio" : "e.2.5.n.3 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.4").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.4 Campo obligatorio para el uso baldio" : "e.2.5.n.4 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.5").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.5 Campo obligatorio para el uso baldio" : "e.2.5.n.5 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.6").IsFull())
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.6 Campo obligatorio para el uso baldio" : "e.2.5.n.6 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }

                        //JACM Se da de baja el campo 2021-02-15
                        /*if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.7").IsFull()) //&& !flag2)
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.7 Campo obligatorio para el uso baldio" : "e.2.5.n.7 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }*/

                        IEnumerable<XElement> xelements = XmlUtils.XmlSearchById(xelement, "e.2.5.n.7");
                        if (!xelements.IsFull())
                        {
                            flag1 = false;
                            string str = //stringXelement == "W" ? "e.2.5.n.7 Campo obligatorio para el uso baldio" : 
                            "e.2.5.n.7 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        else if (xelements.Count<XElement>() != 0 && !XmlUtils.EsDecimalXmlValido(xelements))
                        {
                            flag1 = false;
                            string str = "e.2.5.n.7 El dato no es correcto, se requiere asignar un valor.";
                            stringBuilder.AppendLine(str);
                        }


                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.8").IsFull() && !esComercial && stringXelement != "W")
                        {
                            flag1 = false;
                            string str = "e.2.5.n.8 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (esComercial)
                        {
                            if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.9").IsFull() && stringXelement != "H")
                            {
                                flag1 = false;
                                string str = //stringXelement == "W" ? "e.2.5.n.9 Campo obligatorio para el uso baldio" : 
                                    "e.2.5.n.9 Campo obligatorio";
                                stringBuilder.AppendLine(str);
                            }
                        }
                        // JACM Se da de baja el campo 2021-02-04
                        /*if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.10").IsFull() && stringXelement != "W")
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.10 Campo obligatorio para el uso baldio" : "e.2.5.n.10 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }*/
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.12").IsFull() && esComercial)
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.12 Campo obligatorio para el uso baldio" : "e.2.5.n.12 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        /*if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.13").IsFull() && flag2 && stringXelement != "H")
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.13 Campo obligatorio para el uso baldio" : "e.2.5.n.13 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }*/
                        // JACM Se da de baja el campo 2021-02-04
                        /*if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.14").IsFull() && flag2 && stringXelement != "H")
                        {
                            flag1 = false;
                            string str = stringXelement == "W" ? "e.2.5.n.14 Campo obligatorio para el uso baldio" : "e.2.5.n.14 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }*/
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.15").IsFull())
                        {
                            flag1 = false;
                            string str = /*(stringXelement != "P"  && 
                                          stringXelement != "PE" && 
                                          stringXelement != "PC" && 
                                          stringXelement != "J"  && 
                                          stringXelement != "H") ? 
                                "e.2.5.n.15 Campo obligatorio para el uso baldio" :*/ "e.2.5.n.15 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.16").IsFull() && !esComercial && stringXelement != "W")
                        {
                            flag1 = false;
                            string str = "e.2.5.n.16 Campo obligatorio";
                            stringBuilder.AppendLine(str);
                        }
                        if (!esComercial)
                        {
                            if (!XmlUtils.XmlSearchById(rootN, "e.2.5.n.17").IsFull()) // && !esComercial)
                            {
                                flag1 = false;
                                string str =  "e.2.5.n.17 Campo obligatorio";
                                stringBuilder.AppendLine(str);
                            }
                        }
                    }
                }
            }
            if (this.obligatorioComun || !this.existeWC)
            {
                if (!XmlUtils.XmlSearchById(rootN, "e.2.6").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.2.6 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(rootN, "e.2.7").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.2.7 Campo obligatorio");
                }
            }
            if (this.obligatorioPriv || !this.existeWP)
            {
                if (!XmlUtils.XmlSearchById(data, "e.3").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.3 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(data, "e.5").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.5 Campo obligatorio");
                }

                //< !--JACM 2021 - 02 - 15 Se da de baja el campo para Comercial
                if (!esComercial) { 
                if (!XmlUtils.XmlSearchById(data, "e.4").IsFull())
                {
                    flag1 = false;
                    stringBuilder.AppendLine("e.4 Campo obligatorio");
                }
                }
                if (!XmlUtils.XmlSearchById(data, "k.1").IsFull() && esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("k.1 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(data, "k.2"), "k.2.13").IsFull() && esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("k.2.13 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(data, "k.3").IsFull() && esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("k.3 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(data, "k.5").IsFull() && esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("k.5 Campo obligatorio");
                }
            }
            if ((this.obligatorioPriv || !this.existeWP) && (this.obligatorioComun && !this.existeWC))
            {
                if (!XmlUtils.XmlSearchById(data, "i.6").IsFull() && esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("i.6 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(data, "j.4").IsFull() && !esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("j.4 Campo obligatorio");
                }
                if (!XmlUtils.XmlSearchById(data, "j.7").IsFull() && !esComercial)
                {
                    flag1 = false;
                    stringBuilder.AppendLine("j.7 Campo obligatorio");
                }
            }
            if (!flag1)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
        }

        private void ValidarTipoPersona(XElement data)
        {
            bool flag1 = true;
            bool flag2 = true;
            bool flag7 = true;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(data, "b.1");
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(data, "b.2");
            StringBuilder stringBuilder = new StringBuilder();
            if (xelements1.IsFull())
            {
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(xelements1, "b.1.10");
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(xelements1, "b.1.1");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements1, "b.1.2");
                if (xelements3.IsFull())
                {
                    if (xelements3.ToStringXElement().Trim().ToUpper().Equals("F".Trim().ToUpper()))
                    {
                        if (xelements4.IsFull())
                        {
                            if (string.IsNullOrEmpty(xelements4.ToStringXElement()))
                            {
                                flag2 = false;
                                stringBuilder.AppendLine("  b.1.10 - Error en el tipo de persona: La persona física debe contener el apellido paterno (b.1.1)");
                            }
                        }
                        else
                        {
                            flag2 = false;
                            stringBuilder.AppendLine("  b.1.10 - Error en el tipo de persona: La persona física debe contener el apellido paterno (b.1.1)");
                        }
                    }
                    else if (xelements3.ToStringXElement().Trim().ToUpper().Equals("M".Trim().ToUpper()))
                    {
                        if (xelements4.IsFull() && !string.IsNullOrEmpty(xelements4.ToStringXElement()))
                        {
                            flag2 = false;
                            stringBuilder.AppendLine("  b.1.10 - Error en el tipo de persona: La persona moral no puede tener apellido paterno (b.1.1)");
                        }
                        if (xelements5.IsFull() && !string.IsNullOrEmpty(xelements5.ToStringXElement()))
                        {
                            flag2 = false;
                            stringBuilder.AppendLine("  b.1.10 - Error en el tipo de persona: La persona moral no puede tener apellido materno (b.1.2)");
                        }
                    }
                }
            }
            if (xelements2.IsFull())
            {
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(xelements2, "b.2.10");
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(xelements2, "b.2.1");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements2, "b.2.2");
                if (xelements3.IsFull())
                {
                    if (xelements3.ToStringXElement().Trim().ToUpper().Equals("F".Trim().ToUpper()))
                    {
                        if (xelements4.IsFull())
                        {
                            if (string.IsNullOrEmpty(xelements4.ToStringXElement()))
                            {
                                flag1 = false;
                                stringBuilder.AppendLine("  b.2.10 - Error en el tipo de persona: La persona física debe contener el apellido paterno (b.2.1)");
                            }
                        }
                        else
                        {
                            flag1 = false;
                            stringBuilder.AppendLine("  b.2.10 - Error en el tipo de persona: La persona física debe contener el apellido paterno (b.2.1)");
                        }
                    }
                    else if (xelements3.ToStringXElement().Trim().ToUpper().Equals("M".Trim().ToUpper()))
                    {
                        if (xelements4.IsFull() && !string.IsNullOrEmpty(xelements4.ToStringXElement()))
                        {
                            flag1 = false;
                            stringBuilder.AppendLine("  b.2.10 - Error en el tipo de persona: La persona moral no puede tener apellido paterno (b.2.1)");
                        }
                        if (xelements5.IsFull() && !string.IsNullOrEmpty(xelements5.ToStringXElement()))
                        {
                            flag1 = false;
                            stringBuilder.AppendLine("  b.2.10 - Error en el tipo de persona: La persona moral no puede tener apellido materno (b.2.2)");
                        }
                    }
                }
            }

            /*IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(data, "b.7");
            if (string.IsNullOrEmpty(xelements7.ToStringXElement()))
            {
                flag7 = false;
                stringBuilder.AppendLine("  b.7 - El contenido del elemento Anteedentes está incompleto. Lista esperada de elementos posibles: 'Tipo de inmueble'.");
            }*/

            if (!flag1 || !flag2 )//|| !flag7)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));

        }

        private bool ValidarResolucionFoto(Image Img)
        {
            bool flag = true;
            if (Img.Width != 640 || Img.Height != 480)
                flag = false;
            return flag;
        }

        private string ValidarFoto(string ImageTextParam, string nombreFoto)
        {
            Image Img = (Image)null;
            StringBuilder stringBuilder = new StringBuilder();
            string ImageText = ImageTextParam.Trim();
            try
            {
                Img = this.DecodificarImagen(ImageText);
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine("La cadena de la imagen " + string.Empty + nombreFoto + string.Empty + " no representa una imagen en base 64");
            }
            if ((Decimal)stringBuilder.Length == 0M)
            {
                if (!this.ValidarResolucionFoto(Img))
                    stringBuilder.AppendLine("La imagen " + nombreFoto + string.Empty + ", tiene resolución incorrecta");
                if (!this.ValidarDPIFoto(Img))
                    stringBuilder.AppendLine("La imagen " + nombreFoto + string.Empty + ", tiene DPI (puntos por pulgada) incorrecto");
                if (!this.ValidarFormatoFoto(Img))
                    stringBuilder.AppendLine("La imagen " + nombreFoto + string.Empty + ", tiene formato incorrecto");
            }
            return stringBuilder.ToString();
        }

        private bool ValidarDimensionesFoto(Image Img)
        {
            bool flag = true;
            double num = Math.Round((double)Img.Height * 2.54 / 72.0, 1);
            if (Math.Round((double)Img.Width * 2.54 / 72.0, 1) != 16.9 || num != 22.6)
                flag = false;
            return flag;
        }

        private bool ValidarDPIFoto(Image Img)
        {
            bool flag = true;
            if ((double)Img.HorizontalResolution != 72.0 || (double)Img.VerticalResolution != 72.0)
                flag = false;
            return flag;
        }

        private bool ValidarFormatoFoto(Image Img)
        {
            bool flag = true;
            if (Img.RawFormat.Guid.Equals((object)ImageFormat.Jpeg))
                flag = false;
            return flag;
        }

        private void ValidarMacroMicroLocalizacion(XmlDocument xmlAvaluo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "d.2");
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "d.3");
            if (xelements1.IsFull())
            {
                string str = this.ValidarFoto(xelements1.ToStringXElement().Trim(), "CroquisMicroLocalizacion");
                if (str.Length > 0)
                    stringBuilder.AppendLine("d.2 - " + str);
            }
            else
                stringBuilder.AppendLine("d.2 - La imagen CroquisMicroLocalizacion está vacía");
            if (xelements2.IsFull())
            {
                string str = this.ValidarFoto(xelements2.ToStringXElement().Trim(), "CroquisMacroLocalizacion");
                if (str.Length > 0)
                    stringBuilder.AppendLine("d.3 - " + str);
            }
            else
                stringBuilder.AppendLine("d.3 - La imagen CroquisMacroLocalizacion está vacía");
            if (stringBuilder.Length > 0)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
        }

        private void ValidarAnexoFotografico(XmlDocument xmlAvaluo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<XElement> rootN = XmlUtils.XmlSearchById(XDocument.Parse(xmlAvaluo.InnerXml).Root, "q");
            string empty1 = string.Empty;
            if (XmlUtils.XmlSearchById(rootN, "q.1").IsFull())
            {
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(rootN, "q.1.1.1");
                if (xelements1.IsFull())
                    empty1 += xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(rootN, "q.1.1.2");
                if (xelements2.IsFull())
                    empty1 += xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(rootN, "q.1.1.3");
                if (xelements3.IsFull())
                    empty1 += xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(rootN, "q.1.1.4");
                if (xelements4.IsFull())
                    empty1 += xelements4.ToStringXElement();
                IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(rootN, "q.1.2");
                if (elementos.IsFull())
                {
                    int num = 1;
                    foreach (XElement root in elementos)
                    {
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "q.1.2.n.1");
                        string str1 = "SUJETO_cc:" + empty1 + "_pos";
                        if (xelements5.IsFull())
                        {
                            string str2 = this.ValidarFoto(xelements5.ToStringXElement().Trim(), str1 + (object)num);
                            if (str2.Length > 0)
                                stringBuilder.AppendLine("q.1.2.n.1 - " + str2);
                        }
                        else
                            stringBuilder.AppendLine("La imagen " + str1 + (object)num + " está vacía (q.1.2.n.1)");
                        ++num;
                    }
                }
            }
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(rootN, "q.2");
            if (elementos1.IsFull())
            {
                foreach (XElement root1 in elementos1)
                {
                    string empty2 = string.Empty;
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.1");
                    if (xelements1.IsFull())
                        empty2 += xelements1.ToStringXElement();
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.2");
                    if (xelements2.IsFull())
                        empty2 += xelements2.ToStringXElement();
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.3");
                    if (xelements3.IsFull())
                        empty2 += xelements3.ToStringXElement();
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.4");
                    if (xelements4.IsFull())
                        empty2 += xelements4.ToStringXElement();
                    IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(root1, "q.2.n.2");
                    if (elementos2.IsFull())
                    {
                        int num = 1;
                        foreach (XElement root2 in elementos2)
                        {
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "q.2.n.2.n.1");
                            string str1 = "RENTAS_cc:" + empty2 + "_pos";
                            if (xelements5.IsFull())
                            {
                                string str2 = this.ValidarFoto(xelements5.ToStringXElement().Trim(), str1 + (object)num);
                                if (str2.Length > 0)
                                    stringBuilder.AppendLine("q.2.n.2.n.1 - " + str2);
                            }
                            else
                                stringBuilder.AppendLine("La imagen " + str1 + (object)num + " está vacía (q.2.n.2.n.1)");
                            ++num;
                        }
                    }
                }
            }
            IEnumerable<XElement> elementos3 = XmlUtils.XmlSearchById(rootN, "q.3");
            if (elementos3.IsFull())
            {
                foreach (XElement root1 in elementos3)
                {
                    string empty2 = string.Empty;
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.1");
                    if (xelements1.IsFull())
                        empty2 += xelements1.ToStringXElement();
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.2");
                    if (xelements2.IsFull())
                        empty2 += xelements2.ToStringXElement();
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.3");
                    if (xelements3.IsFull())
                        empty2 += xelements3.ToStringXElement();
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.4");
                    if (xelements4.IsFull())
                        empty2 += xelements4.ToStringXElement();
                    IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(root1, "q.3.n.2");
                    if (elementos2.IsFull())
                    {
                        int num = 1;
                        foreach (XElement root2 in elementos2)
                        {
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "q.3.n.2.n.1");
                            string str1 = "VENTAS_cc:" + empty2 + "_pos";
                            if (xelements5.IsFull())
                            {
                                string str2 = this.ValidarFoto(xelements5.ToStringXElement().Trim(), str1 + (object)num);
                                if (str2.Length > 0)
                                    stringBuilder.AppendLine("q.3.n.2.n.1 - " + str2);
                            }
                            else
                                stringBuilder.AppendLine("La imagen " + str1 + (object)num + " está vacía (q.3.n.2.n.1)");
                            ++num;
                        }
                    }
                }
            }
            if (stringBuilder.Length > 0)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
        }

        private Image DecodificarImagen(string ImageText)
        {
            byte[] numArray = Convert.FromBase64String(ImageText);
            MemoryStream memoryStream = new MemoryStream(numArray, 0, numArray.Length);
            Encoding.ASCII.GetString(numArray);
            return Image.FromStream((Stream)memoryStream, true);
        }

        private string ValidarPerito(int idPersonaWeb, XmlDocument xmlAvaluo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            IEnumerable<XElement> elemento = XmlUtils.XmlSearchById(root, "a.3");
            IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(root, "a.4");
            PeritosSociedadesClient proxy = new PeritosSociedadesClient();
            DsePeritosSociedades peritosSociedades = (DsePeritosSociedades)null;
            try
            {
                peritosSociedades = proxy.GetPerito((Decimal)idPersonaWeb, false);
            }
            finally
            {
                proxy.Disconnect();
            }
            if (peritosSociedades.Perito.Any<DsePeritosSociedades.PeritoRow>())
            {
                if (!peritosSociedades.Perito[0].VIGENTE)
                    stringBuilder.AppendLine("a.3 - El perito identificado en la aplicación no se encuentra vigente, no puede cargar el fichero avalúo.");
                if (!peritosSociedades.Perito[0].REGISTRO.Equals(elemento.ToStringXElement()))
                    stringBuilder.AppendLine("a.3 - Un perito no puede subir avalúos en nombre de otro perito.");
            }
            else
                stringBuilder.AppendLine("a.3 - El perito valuador del avalúo no existe.");
            if (elementos.IsFull())
                stringBuilder.AppendLine("a.4 - Un perito no puede subir avalúos en nombre de una sociedad.");
            return stringBuilder.ToString();
        }

        private string ValidarSociedad(int idPersonaWeb, XmlDocument xmlAvaluo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            IEnumerable<XElement> elemento1 = XmlUtils.XmlSearchById(root, "a.3");
            IEnumerable<XElement> elemento2 = XmlUtils.XmlSearchById(root, "a.4");
            PeritosSociedadesClient proxy = new PeritosSociedadesClient();
            DsePeritosSociedades peritosSociedades = (DsePeritosSociedades)null;
            try
            {
                peritosSociedades = proxy.GetRelacionPeritoSociedad(elemento1.ToStringXElement(), elemento2.ToStringXElement());
            }
            finally
            {
                proxy.Disconnect();
            }
            elemento1.ToStringXElement();
            elemento2.ToStringXElement();
            if (peritosSociedades.SociedadValuacion.Any<DsePeritosSociedades.SociedadValuacionRow>())
            {
                if (peritosSociedades.SociedadValuacion[0].IDSOCIEDAD == (Decimal)idPersonaWeb)
                {
                    if (peritosSociedades.SociedadValuacion[0].VIGENTE)
                    {
                        if (!peritosSociedades.Perito[0].VIGENTE)
                            stringBuilder.AppendLine("a.4 - El perito valuador del avaluo no se encuentra vigente.");
                    }
                    else
                        stringBuilder.AppendLine("a.4 - La sociedad de valuación no se encuentra vigente");
                }
                else
                    stringBuilder.AppendLine("a.4 - Una sociedad no puede subir avalúos en nombre de otra sociedad");
            }
            else
                stringBuilder.AppendLine("La clave de Sociedad no corresponde con la asociada al perito (a.4)");
            return stringBuilder.ToString();
        }

        private void ValidarVigenciaPeritoSociedad(
          int idPersonaWeb,
          XmlDocument xmlAvaluo,
          bool esPerito)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (esPerito)
            {
                string str = this.ValidarPerito(idPersonaWeb, xmlAvaluo);
                if (str.Length > 0)
                    stringBuilder.AppendLine(str);
            }
            else
            {
                string str = this.ValidarSociedad(idPersonaWeb, xmlAvaluo);
                if (str.Length > 0)
                    stringBuilder.AppendLine(str);
            }
            if (stringBuilder.Length > 0)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
        }

        private void ValidarDelegacionesColonias(XElement xml)
        {
        }

        private string ValidarDelegacionColonia(
          IEnumerable<XElement> DelegacionElem,
          IEnumerable<XElement> ColoniaElem)
        {
            bool flag = false;
            string codDelegacion = string.Empty;
            string codColonia = string.Empty;
            if (ColoniaElem != null && ColoniaElem.IsFull())
                codColonia = ColoniaElem.ToStringXElement().ToUpper().Trim();
            if (DelegacionElem != null && DelegacionElem.IsFull())
                codDelegacion = DelegacionElem.ToStringXElement().Trim();
            EnumerableRowCollection<Decimal> source1 = CatastralUtils.DtDelegacion.Where<DseDelegacionColonia.DelegacionRow>((Func<DseDelegacionColonia.DelegacionRow, bool>)(c => c.CLAVE == codDelegacion)).Select<DseDelegacionColonia.DelegacionRow, Decimal>((Func<DseDelegacionColonia.DelegacionRow, Decimal>)(c => c.IDDELEGACION));
            if (source1.Any<Decimal>())
            {
                Decimal idDelegacion = source1.First<Decimal>().ToDecimal();
                EnumerableRowCollection<Decimal> source2 = CatastralUtils.DtColonia.Where<DseDelegacionColonia.ColoniaRow>((Func<DseDelegacionColonia.ColoniaRow, bool>)(c => c.NOMBRE.ToUpper().Trim() == codColonia && c.IDDELEGACION == idDelegacion)).Select<DseDelegacionColonia.ColoniaRow, Decimal>((Func<DseDelegacionColonia.ColoniaRow, Decimal>)(c => c.IDCOLONIA));
                if (source2.Any<Decimal>())
                {
                    Decimal o = source2.First<Decimal>().ToDecimal();
                    ConsultaCatastralServiceClient proxy = new ConsultaCatastralServiceClient();
                    DseDelegacionColonia delegacionColonia = (DseDelegacionColonia)null;
                    try
                    {
                        delegacionColonia = proxy.GetDelegacionColoniaByidColonia((long)o.ToDecimal());
                    }
                    finally
                    {
                        proxy.Disconnect();
                    }
                    if (delegacionColonia.Delegacion.Any<DseDelegacionColonia.DelegacionRow>() && delegacionColonia.Delegacion[0].CLAVE.ToString().Equals(codDelegacion))
                        flag = true;
                }
            }
            if (flag)
                return string.Empty;
            return "La colonia no pertenece a la delegación especificada: " + codDelegacion + " - " + string.Empty + codColonia;
        }

        private void ValidarValorReferido(XElement data)
        {
            bool flag = false;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(data, "p.5");
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(data, "p.1");
            if (xelements2.IsFull() && xelements1.IsFull())
            {
                if ((Decimal)xelements2.ToStringXElement().Length > 0M && (Decimal)xelements1.ToStringXElement().Length > 0M)
                    flag = true;
                if ((Decimal)xelements2.ToStringXElement().Length == 0M && (Decimal)xelements1.ToStringXElement().Length == 0M)
                    flag = true;
            }
            if (!xelements2.IsFull() && !xelements1.IsFull())
                flag = true;
            if (!flag)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("Valor referido  (p.5 y p.1)"));
        }

        public void ValidarExisteAvaluoRegistrado(
          string numAvaluo,
          Decimal idPersonaSociedad,
          bool esPerito)
        {
            if ((Decimal)this.ExisteAvaluoRegistrado(numAvaluo, idPersonaSociedad, esPerito) != 0M)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("Ya existe un avalúo registrado con nº avalúo " + numAvaluo.ToString()));
        }

        private bool EsValorCatastralValido(
          string clave,
          Decimal anio,
          Decimal valor,
          out string mess)
        {
            bool flag = false;
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            try
            {
                TranHelper tranHelper = new TranHelper();
                using (OracleCommand cmd = new OracleCommand("FEXAVA_FISCALVUS_PKG.fexava_valida_clave_valor_p"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("PANIO", OracleDbType.Decimal)).Value = (object)anio;
                    cmd.Parameters.Add(new OracleParameter("pCLAVE", OracleDbType.Varchar2, 100)).Value = (object)clave;
                    cmd.Parameters.Add(new OracleParameter("pVALOR", OracleDbType.Decimal)).Value = (object)valor;
                    cmd.Parameters.Add(new OracleParameter("pVALIDO", OracleDbType.Decimal)).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(new OracleParameter("pMENSAJE", OracleDbType.Varchar2, 100)).Direction = ParameterDirection.Output;
                    tranHelper.EjecutaNonQuerySP(cmd);
                    empty1 = cmd.Parameters["pMENSAJE"].Value.ToString();
                    flag = cmd.Parameters["pVALIDO"].Value.ToString().Equals("0");
                }
            }
            catch
            {
            }
            finally
            {
                mess = empty1;
            }
            return flag;
        }

        private void ValidarValoresCalculados(XmlDocument xmlAvaluo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string str = " Error al validar el cálculo.";
            IEnumerable<XElement> xelements1 = (IEnumerable<XElement>)null;
            List<IEnumerable<XElement>> xelementsList1 = (List<IEnumerable<XElement>>)null;
            Decimal num1 = 0M;
            XElement root1 = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            bool esComercial = (Decimal)root1.Descendants((XName)"Comercial").Count<XElement>() > 0M;
            bool flag2 = XmlUtils.XmlSearchById(root1, "b.6").ToStringXElement().Equals("2");
            IEnumerable<XElement> rootN1 = XmlUtils.XmlSearchById(root1, "c.6");
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(rootN1, "c.6.4");
            if (xelements2.IsFull() && XmlUtils.EsDecimalXmlValido(xelements2))
            {
                Decimal decimalXelement = xelements2.ToDecimalXElement();
                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                XmlUtils.XmlSearchById(rootN1, "c.6.2");
                IEnumerable<XElement> queryXml = XmlUtils.XmlSearchById(rootN1, "c.6.3");
                listElement.Add(XmlUtils.XmlSearchById(rootN1, "c.6.2"));
                listElement.Add(XmlUtils.XmlSearchById(rootN1, "c.6.3"));
                bool flag3 = XmlUtils.EsDecimalXmlValido(queryXml);
                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                {
                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                    if (!ValidarCamposCalculados.ValidarCampoCalculado_c_6_4(decimalXelement, listDecimal[0], listDecimal[1]))
                        stringBuilder.AppendLine("c.6.4 - Coeficiente de uso del suelo." + str);
                }
                else
                    stringBuilder.AppendLine("c.6.4 - Coeficiente de uso del suelo." + str);
            }
            foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "d.5"))
            {
                
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "d.5.n.8");
                    if (!xelements3.IsFull())
                    {
                        stringBuilder.AppendLine("d.5.n.8 - Es necesario el campo");
                    }
                    else
                    {
                        Decimal anio = Convert.ToDecimal(XmlUtils.XmlSearchById(root1, "a.2").ToStringXElement().Substring(0, 4));
                        string stringXelement = xelements3.ToStringXElement();
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root2, "d.5.n.12");
                        if (xelements4.IsFull())
                        {
                            string mess = string.Empty;
                            Decimal decimalXelement = xelements4.ToDecimalXElement();
                            if (!this.EsValorCatastralValido(stringXelement, anio, decimalXelement, out mess))
                                stringBuilder.AppendLine(mess);
                        }
                    }

                if (esComercial)
                {
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "d.5.n.10");
                if (xelements5.IsFull() && XmlUtils.EsDecimalXmlValido(xelements5))
                {
                    // JACM Se da de baja el campo 2021-02-04
                    /*bool flag3 = false;
                    if (XmlUtils.XmlSearchById(root2, "d.5.n.9").IsFull())
                        flag3 = true;*/
                    bool flag4 = 
                            XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.3.2")) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.4.2")) 
                        && (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.5.2")) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.6.2"))) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.7.2"));
                    /*if (flag3)
                        flag4 = flag4 && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.9.1"));*/
                    if (flag4)
                    {
                        Decimal decimalXelement = xelements5.ToDecimalXElement();
                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.3.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.4.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.5.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.6.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.7.2"));
                        //if (flag3)
                        //    listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.9.1"));
                        if (!XmlUtils.IsListEmpty(listElement))
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                //if (esComercial)
                                //{
                                /*if (!this.esFactorZonaValido(listDecimal[0]))
                                    stringBuilder.AppendLine("d.5.n.3.2 - Valor no permitido");
                                if (!this.esFactorUbicacionValido(listDecimal[1]))
                                    stringBuilder.AppendLine("d.5.n.4.2 - Valor no permitido");
                                if (!this.esFactorFrenteValido(listDecimal[2]))
                                    stringBuilder.AppendLine("d.5.n.5.2 - Valor no permitido");
                                if (!this.esFactorSuperficieValido(listDecimal[4]))
                                    stringBuilder.AppendLine("d.5.n.7.2 - Valor no permitido");*/

                                /*if (listDecimal[0] > 1.99M)
                                    stringBuilder.AppendLine("d.5.n.3.2 - Valor no permitido");
                                if (listDecimal[1] > 1.99M)
                                    stringBuilder.AppendLine("d.5.n.4.2 - Valor no permitido");
                                if (listDecimal[2] > 1.99M)
                                    stringBuilder.AppendLine("d.5.n.5.2 - Valor no permitido");
                                if (listDecimal[4] > 1.99M)
                                    stringBuilder.AppendLine("d.5.n.7.2 - Valor no permitido");*/
                                //}
                                /*if (flag3)
                                {
                                    if (!ValidarCamposCalculados.ValidarCampoCalculado_d_5_n_10(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4], listDecimal[5]))
                                        stringBuilder.AppendLine("d.5.n.10 - Fre." + str);
                                }
                                else
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_d_5_n_10(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4])) //, 1M))
                                    //stringBuilder.AppendLine("d.5.n.10 - Fre." + str);
                                    stringBuilder.AppendLine("d.5.n.10 - Fre. Valor no permitido.");*/
                                string mensaje = "";
                                mensaje = ValidarCamposCalculados.ValidarCampoCalculado_d_5_n_10(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4]);
                                if (!mensaje.Equals("")) //, 1M))
                                                                                                                                                                                              //stringBuilder.AppendLine("d.5.n.10 - Fre." + str);
                                    stringBuilder.AppendLine("d.5.n.10 - Fre. "+mensaje+".");
                            }
                    }
                    else
                        stringBuilder.AppendLine("d.5.n.10 - Fre." + str);
                }
                }
            }
            if (esComercial)
            {
                IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "h.1");
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "d.5"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "d.5.n.11");
                    if (xelements3.IsFull())
                    {
                        if (XmlUtils.EsDecimalXmlValido(xelements3) 
                            && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "h.1.4")) 
                            && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.2")) 
                            && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "d.5.n.10")))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(rootN2, "h.1.4"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.2"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "d.5.n.10"));
                            if (!XmlUtils.IsListEmpty(listElement))
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_d_5_n_11(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("d.5.n.11 - Valor de la fracción n." + str);
                            }
                            else
                                stringBuilder.AppendLine("d.5.n.11 - Valor de la fracción n." + str);
                        }
                        else
                            stringBuilder.AppendLine("d.5.n.11 - Valor de la fracción n." + str);
                    }
                }
            }
            else
            {
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "d.5"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "d.5.n.2");
                    if (xelements3.IsFull())
                    {
                        Decimal decimalXelement1 = xelements3.ToDecimalXElement();
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root2, "d.5.n.12");
                        if (xelements4.IsFull())
                        {
                            Decimal decimalXelement2 = xelements4.ToDecimalXElement();
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "d.5.n.11");
                            if (xelements5.IsFull())
                            {
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_d_5_n_11(xelements5.ToDecimalXElement(), decimalXelement1, decimalXelement2))
                                    stringBuilder.AppendLine("d.5.n.11 - Valor de la fracción n." + str);
                            }
                            else
                                stringBuilder.AppendLine("d.5.n.11 - Valor de la fracción n.Falta valor.");
                        }
                        else
                            stringBuilder.AppendLine(string.Format("{0}-Error al validar el cálculo, falta d.5.n.12", (object)"d.5.n.11 - Valor de la fracción n."));
                    }
                }
            }
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root1, "d.11");
            if (xelements6.IsFull())
            {
                if (XmlUtils.EsDecimalXmlValido(xelements6))
                {
                    Decimal decimalXelement = xelements6.ToDecimalXElement();
                    xelements1 = XmlUtils.XmlSearchById(root1, "d.5");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(root1, "d.5.n.2"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_d_5_n_2 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_d_11(decimalXelement, sumatorio_d_5_n_2))
                                stringBuilder.AppendLine("d.11 - Superficie total del terreno." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("d.11 - Superficie total del terreno. Tipo de dato erróneo" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("d.11 - Superficie total del terreno." + str);
                }
                else
                    stringBuilder.AppendLine("d.11 - Superficie total del terreno." + str);
            }
            IEnumerable<XElement> rootN3 = XmlUtils.XmlSearchById(root1, "d.5");
            IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root1, "d.12");
            if (xelements7.IsFull() && XmlUtils.EsDecimalXmlValido(xelements7))
            {
                Decimal decimalXelement = xelements7.ToDecimalXElement();
                List<IEnumerable<XElement>> xelementsList2 = new List<IEnumerable<XElement>>();
                xelementsList2.Add(XmlUtils.XmlSearchById(rootN3, "d.5.n.11"));
                if (!XmlUtils.IsListEmpty(xelementsList2))
                {
                    try
                    {
                        List<Decimal> listDecimal = new List<Decimal>();
                        for (int index = 0; index < xelementsList2.First<IEnumerable<XElement>>().Count<XElement>(); ++index)
                            listDecimal.Add(xelementsList2[0].ElementAt<XElement>(index).ToDecimalXElement());
                        Decimal sumatorio_d_5_N_11 = this.SumatorioListDecimal(listDecimal);
                        try
                        {
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_d_12(decimalXelement, sumatorio_d_5_N_11))
                                stringBuilder.AppendLine("d.12 - Valor total del terreno." + str);
                        }
                        catch (DivideByZeroException ex)
                        {
                            stringBuilder.AppendLine("d.12 - Valor total del terreno. No se puede dividir por cero" + str);
                        }
                    }
                    catch (FormatException ex)
                    {
                        stringBuilder.AppendLine("d.12 - Valor total del terreno. Tipo de dato erróneo" + str);
                    }
                }
                else
                    stringBuilder.AppendLine("d.12 - Valor total del terreno." + str);
            }
            IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root1, "d.13");
            if (xelements8.IsFull() && XmlUtils.EsDecimalXmlValido(xelements8))
            {
                Decimal decimalXelement = xelements8.ToDecimalXElement();
                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                listElement.Add(XmlUtils.XmlSearchById(root1, "d.12"));
                listElement.Add(XmlUtils.XmlSearchById(root1, "d.6"));
                bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "d.12")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "d.6"));
                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                {
                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                    if (!ValidarCamposCalculados.ValidarCampoCalculado_d_13(decimalXelement, listDecimal[0], listDecimal[1]))
                        stringBuilder.AppendLine("d.13 - Valor total del terreno proporcional." + str);
                }
                else
                    stringBuilder.AppendLine("d.13 - Valor total del terreno proporcional." + str);
            }
            Decimal num2 = 0M;
            Decimal num3 = 0M;
            Decimal num4 = 0M;
            
            
            
            if (!this.esTerrenoValdio(root1))
            {
                /*
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.1"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "e.2.1.n.2").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "e.2.1.n.2").IsFull() && stringXelement != "H")
                    {
                        if (XmlUtils.XmlSearchById(root2, "e.2.1.n.11").IsFull() //&& stringXelement == "W" 
                            && XmlUtils.XmlSearchById(root2, "e.2.1.n.11").ToDecimalXElement() > 0M)
                            num4 += XmlUtils.XmlSearchById(root2, "e.2.1.n.11").ToDecimalXElement();
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.1.n.9");
                        if (xelements3.IsFull() && stringXelement != "W")
                        {
                            if (XmlUtils.EsDecimalXmlValido(xelements3))
                            {
                                Decimal decimalXelement = xelements3.ToDecimalXElement();
                                if (!esComercial)//Se elimina este campo de comercial
                                {
                                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.8")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.7")))
                                    {
                                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                                    listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.8"));
                                    listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.7"));
                                    if (!XmlUtils.IsListEmpty(listElement))
                                    {
                                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                        //if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_9(decimalXelement, listDecimal[0], listDecimal[1]))
                                        //    stringBuilder.AppendLine("e.2.1.n.9 - Vida útil remanente contrucciones privativas." + str);
                                    }
                                    }
                                }
                                else
                                {
                                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.8")) )
                                    {
                                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.8"));
                                        if (!XmlUtils.IsListEmpty(listElement))
                                        {
                                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                            //if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_9(decimalXelement, listDecimal[0], 0))
                                            //    stringBuilder.AppendLine("e.2.1.n.9 - Vida útil remanente contrucciones privativas." + str);
                                        }
                                    }

                                }
                            }
                            //else
                                //stringBuilder.AppendLine("e.2.1.n.9 - Vida útil remanente contrucciones privativas." + str);
                        }
                    }
                }
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.5"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.5.n.9");
                    string stringXelement = XmlUtils.XmlSearchById(root2, "e.2.5.n.2").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "e.2.5.n.2").IsFull() && stringXelement != "H")
                    {
                        if (XmlUtils.XmlSearchById(root2, "e.2.5.n.11").IsFull() && stringXelement == "W" && XmlUtils.XmlSearchById(root2, "e.2.5.n.11").ToDecimalXElement() > 0M)
                            num4 += XmlUtils.XmlSearchById(root2, "e.2.5.n.11").ToDecimalXElement();
                        if (xelements3.IsFull() && stringXelement != "W")
                        {
                            //if (XmlUtils.EsDecimalXmlValido(xelements3))
                            //{
                                //Decimal decimalXelement = xelements3.ToDecimalXElement();
                                //if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.8"))
                    //JACM Se da de baja el campo 2021-02-15
                    //&& XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.7"))
                    //)
                                //{
                                    //List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                                    //listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.8"));
                                    //listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.7"));
                                    //if (!XmlUtils.IsListEmpty(listElement))
                                    //{
                                        //List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                        //if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_5_n_9(decimalXelement, listDecimal[0], 0))
                                        //    stringBuilder.AppendLine("e.2.5.n.9 - Vida útil remanente construcciones comunes." + str);
                                    //}
                                //}
                            //}
                            //else
                            //    stringBuilder.AppendLine("e.2.5.n.9 - Vida útil remanente construcciones comunes." + str);
                        }
                    }
                }
                IEnumerable<XElement> source1 = XmlUtils.XmlSearchById(root1, "e.2.1");

                // JACM Se da de baja el campo 2021-02-04
                for (int index = 0; index < source1.Count<XElement>(); ++index)
                {
                    string ClaveConservacion = XmlUtils.XmlSearchById(source1.ElementAt<XElement>(index), "e.2.1.n.10").IsFull() ? XmlUtils.XmlSearchById(source1.ElementAt<XElement>(index), "e.2.1.n.10").ToStringXElement() : "-1";
                    string uso = "";
                    if (XmlUtils.XmlSearchById(source1.ElementAt<XElement>(index), "e.2.1.n.2").IsFull())
                        uso = XmlUtils.XmlSearchById(source1.ElementAt<XElement>(index), "e.2.1.n.2").ToStringXElement();
                    if (uso != "W")
                    {
                        try
                        {
                            if (!ValidarCamposCalculados.ValidarClaveConservacion(ClaveConservacion, uso))
                                stringBuilder.AppendLine("e.2.1.n.10 - Clave de Conservación.El valor introducido no es valido.");
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("e.2.1.n.10 - Clave de Conservación. Tipo de dato erróneoEl valor introducido no es valido.");
                        }
                    }
                }
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "e.2.1");
                List<IEnumerable<XElement>> xelementsList2 = new List<IEnumerable<XElement>>();
                if(!esComercial) //Se elimina este campo de comercial
                { 
                    xelementsList2.Add(XmlUtils.XmlSearchById(xelements4, "e.2.1.n.7"));
                    xelementsList2.Add(XmlUtils.XmlSearchById(xelements4, "e.2.1.n.8"));
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements4, "e.2.1.n.13");
                    if (xelements5.ExisteXElement())
                {
                    if (xelementsList2[0].ExisteXElement() && xelementsList2[1].ExisteXElement())
                    {
                        for (int index = 0; index < xelements4.Count<XElement>(); ++index)
                        {
                            string uso = "";
                            if (XmlUtils.XmlSearchById(xelements4.ElementAt<XElement>(index), "e.2.1.n.2").IsFull())
                                uso = XmlUtils.XmlSearchById(xelements4.ElementAt<XElement>(index), "e.2.1.n.2").ToStringXElement();
                            if (xelements5.ElementAt<XElement>(index).IsFull() && uso != "H" && xelements5.ElementAt<XElement>(index).IsFull())
                            {
                                if (uso != "W")
                                {
                                    try
                                    {
                                        Decimal decimalXelement = xelements5.ToDecimalXElement(index);
                                        if (decimalXelement < Convert.ToDecimal(0.6))
                                            stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad. El valor no debe ser inferior a 0.6.");
                                        else if (xelementsList2[0].ElementAt<XElement>(index).IsFull() && xelementsList2[1].ElementAt<XElement>(index).IsFull())
                                        {
                                            List<Decimal> numList = new List<Decimal>();
                                            numList.Add(xelementsList2[0].ToDecimalXElement(index));
                                            numList.Add(xelementsList2[1].ToDecimalXElement(index));
                                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_13(decimalXelement, numList[0], numList[1], uso))
                                                stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad." + str);
                                        }
                                        else
                                            stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad." + str);
                                    }
                                    catch (FormatException ex)
                                    {
                                        stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad. Tipo de dato erróneo" + str);
                                    }
                                }
                            }
                        }
                    }
                    else
                        stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad." + str);
                }

                }
                else
                {
                    xelementsList2.Add(XmlUtils.XmlSearchById(xelements4, "e.2.1.n.8"));
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements4, "e.2.1.n.13");
                    if (xelements5.ExisteXElement())
                    {
                        if (xelementsList2[0].ExisteXElement() )
                        {
                            for (int index = 0; index < xelements4.Count<XElement>(); ++index)
                            {
                                string uso = "";
                                if (XmlUtils.XmlSearchById(xelements4.ElementAt<XElement>(index), "e.2.1.n.2").IsFull())
                                    uso = XmlUtils.XmlSearchById(xelements4.ElementAt<XElement>(index), "e.2.1.n.2").ToStringXElement();
                                if (xelements5.ElementAt<XElement>(index).IsFull() && uso != "H" && xelements5.ElementAt<XElement>(index).IsFull())
                                {
                                    if (uso != "W")
                                    {
                                        try
                                        {
                                            Decimal decimalXelement = xelements5.ToDecimalXElement(index);
                                            if (decimalXelement < Convert.ToDecimal(0.6))
                                                stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad. El valor no debe ser inferior a 0.6.");
                                            else if (xelementsList2[0].ElementAt<XElement>(index).IsFull())
                                            {
                                                List<Decimal> numList = new List<Decimal>();
                                                numList.Add(xelementsList2[0].ToDecimalXElement(index));
                                                
                                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_13(decimalXelement, numList[0], 0, uso))
                                                    stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad." + str);
                                            }
                                            else
                                                stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad." + str);
                                        }
                                        catch (FormatException ex)
                                        {
                                            stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad. Tipo de dato erróneo" + str);
                                        }
                                    }
                                }
                            }
                        }
                        else
                            stringBuilder.AppendLine("e.2.1.n.13 - Factor de edad." + str);
                    }

                }
                // JACM Se da de baja el campo 2021-02-04
                
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.1"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.1.n.14");
                    if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3) && (XmlUtils.XmlSearchById(root2, "e.2.1.n.2").ToStringXElement() != "W" && XmlUtils.XmlSearchById(root2, "e.2.1.n.2").ToStringXElement() != "H"))
                    {
                        Decimal decimalXelement = xelements3.ToDecimalXElement();
                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.10"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.13"));
                        if (!XmlUtils.IsListEmpty(listElement))
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                            if (FiscalUtils.EstaEnCatConservacion(listDecimal[0]))
                            {
                                try
                                {
                                    listDecimal[0] = (Decimal)ApplicationCache.DseCatalogosConsulta.FEXAVA_CATESTADOCONSERV.FindByCODESTADOCONSERVACION(listDecimal[0]).VALOR;
                                    if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_14(decimalXelement, listDecimal[0], listDecimal[1]))
                                        stringBuilder.AppendLine("e.2.1.n.14 - Factor resultante." + str);
                                }
                                catch (FormatException ex)
                                {
                                    stringBuilder.AppendLine("e.2.1.n.14 - Factor resultante. Tipo de dato erróneo" + str);
                                }
                            }
                            else
                                stringBuilder.AppendLine("e.2.1.n.14 - Factor resultante.El valor " + (object)listDecimal[0] + " no correponde a ningún un factor de conservacion" + str);
                        }
                        else
                            stringBuilder.AppendLine("e.2.1.n.14 - Factor resultante." + str);
                    }
                }*/
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root1, "e.2.5");
                IEnumerable<XElement> source2 = XmlUtils.XmlSearchById(root1, "a.2");

                /*foreach (XElement root2 in xelements9)
                {
                    if (!esComercial) { 
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.5.n.17");
                    string stringXelement = XmlUtils.XmlSearchById(root2, "e.2.5.n.2").ToStringXElement();
                    if(stringXelement != "P"  && 
                       stringXelement != "PE" && 
                       stringXelement != "PC" && 
                       stringXelement != "J"  //&& 
                       //stringXelement != "H"
                       ) {
                        //if (this.aplicaDepreciacion(stringXelement) && stringXelement != "W" 
                        //&& (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3)))
                        if ((xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3)))
                        {
                                bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.8")) &&
                                    XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.7"));

                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.7"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.8"));
                                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                                {
                                    Decimal decimalXelement = xelements3.ToDecimalXElement();
                                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                    if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_5_n_17(decimalXelement, listDecimal[0], listDecimal[1], source2.First<XElement>().Value.To<DateTime>()))
                                stringBuilder.AppendLine("e.2.5.n.17 - Depreciación por edad." + str);
                        }
                    }
                    }
                    }
                    
                }
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.1"))
                {
                    string uso = XmlUtils.XmlSearchById(root2, "e.2.1.n.2").ToStringXElement();
                    string clase = XmlUtils.XmlSearchById(root2, "e.2.1.n.6").ToStringXElement();

                    log("ValidarValoresCalculados e21n17 ", uso + " | " + clase, "");
                    
                    if (!esComercial)//Se elimina este campo de Comercial
                    {
                        IEnumerable<XElement> depreciacion = XmlUtils.XmlSearchById(root2, "e.2.1.n.17");
                        if (depreciacion.IsFull() && XmlUtils.EsDecimalXmlValido(depreciacion))
                        {
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.7")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.8"));
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.7"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.8"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                Decimal decimalXelement = depreciacion.ToDecimalXElement();
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);

                                log("ValidarValoresCalculados e.2.1.n.17 ", "Valor de Uso: " + uso + " Clase: " + clase,

                                          decimalXelement.ToString() + " || "
                                        + listDecimal[0].ToString() + " || "
                                        + listDecimal[1].ToString());

                                if ((uso == "P" || uso == "PE" || uso == "PC" || uso == "J") && clase == "U")
                                {
                                    log("ValidarValoresCalculados e.2.1.n.17 ", "Valor de Uso: " + uso , " Clase: " + clase);
                                    if (decimalXelement != 1M)
                                    {
                                        stringBuilder.AppendLine("e.2.1.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1" + str);
                                        log("ValidarValoresCalculados e21n17 ", "e.2.1.n.17 Error de restricción Los usos descubiertos, no se pueden depreciar, valor esperado: 1", str);
                                    }
                                }
                                else
                                {
                                    if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_17(decimalXelement, listDecimal[0], listDecimal[1], source2.First<XElement>().Value.To<DateTime>()))
                                    {
                                        stringBuilder.AppendLine("e.2.1.n.17 - Depreciación por edad." + str);
                                        log("ValidarValoresCalculados e21n17 ", "e.2.1.n.17 - Depreciación por edad.", str);
                                    }
                                }

                            }

                        }
                    }
                }*/

                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "e.2"), "e.2.2");
                if (xelements10.IsFull() && XmlUtils.EsDecimalXmlValido(xelements10) && (this.obligatorioPriv || !this.existeWP))
                {
                    num2 = xelements10.ToDecimalXElement();
                    Decimal decimalXelement = xelements10.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "e.2.1");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.1.n.11"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_e_2_1_n_11 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_2(decimalXelement, sumatorio_e_2_1_n_11))
                                stringBuilder.AppendLine("e.2.2 - Superficie total de construcciones PRIVATIVAS." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("e.2.2 - Superficie total de construcciones PRIVATIVAS. Tipo de dato erróneo" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("e.2.2 - Superficie total de construcciones PRIVATIVAS." + str);
                }
                IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "e.2"), "e.2.3");
                if (xelements11.IsFull() && XmlUtils.EsDecimalXmlValido(xelements11) && !this.existeWP)
                {
                    Decimal decimalXelement = xelements11.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "e.2.1");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.1.n.15"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_e_2_1_n_15 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_3(decimalXelement, sumatorio_e_2_1_n_15))
                                stringBuilder.AppendLine("e.2.3 - Valor total de construcciones PRIVATIVAS." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("e.2.3 - Valor total de construcciones PRIVATIVAS. Tipo de dato erróneo" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("e.2.3 - Valor total de construcciones PRIVATIVAS." + str);
                }
                IEnumerable<XElement> source3 = XmlUtils.XmlSearchById(root1, "e.2.5");
                // JACM Se da de baja el campo 2021-02-04
                /*for (int index = 0; index < source3.Count<XElement>(); ++index)
                {
                    string ClaveConservacion = XmlUtils.XmlSearchById(source3.ElementAt<XElement>(index), "e.2.5.n.10").IsFull() ? XmlUtils.XmlSearchById(source3.ElementAt<XElement>(index), "e.2.5.n.10").ToStringXElement() : "-1";
                    string uso = "";
                    if (XmlUtils.XmlSearchById(source3.ElementAt<XElement>(index), "e.2.5.n.2").IsFull())
                        uso = XmlUtils.XmlSearchById(source3.ElementAt<XElement>(index), "e.2.5.n.2").ToStringXElement();
                    if (uso != "W")
                    {
                        try
                        {
                            if (!ValidarCamposCalculados.ValidarClaveConservacion(ClaveConservacion, uso))
                                stringBuilder.AppendLine("e.2.5.n.10 - Clave de Conservación.El valor introducido no es valido.");
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("e.2.5.n.10 - Clave de Conservación. Tipo de dato erróneoEl valor introducido no es valido.");
                        }
                    }
                }
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root1, "e.2.5");
                List<IEnumerable<XElement>> xelementsList3 = new List<IEnumerable<XElement>>();
                //xelementsList3.Add(XmlUtils.XmlSearchById(xelements12, "e.2.5.n.7"));
                xelementsList3.Add(XmlUtils.XmlSearchById(xelements12, "e.2.5.n.8"));
                IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(xelements12, "e.2.5.n.13");
                if (xelements13.ExisteXElement())
                {
                    if (xelementsList3[0].ExisteXElement() && xelementsList3[1].ExisteXElement())
                    {
                        xelements12.Count<XElement>();
                        for (int index = 0; index < xelements12.Count<XElement>(); ++index)
                        {
                            string uso = "";
                            if (XmlUtils.XmlSearchById(xelements12.ElementAt<XElement>(index), "e.2.5.n.2").IsFull())
                                uso = XmlUtils.XmlSearchById(xelements12.ElementAt<XElement>(index), "e.2.5.n.2").ToStringXElement();
                            if (xelements13.ElementAt<XElement>(index).IsFull() && uso != "H" && xelements13.ElementAt<XElement>(index).IsFull())
                            {
                                //En caso de ser usos H, PE, PC, J o P se pone 1
                                if (uso == "H"|| uso == "PE" || uso == "PC" || uso == "J" || uso == "P")
                                { 
                                }
                                    
                                    if (uso != "W")
                                    {
                                        try
                                        {
                                            Decimal decimalXelement = xelements13.ToDecimalXElement(index);
                                            if (decimalXelement < Convert.ToDecimal(0.6))
                                                stringBuilder.AppendLine("e.2.5.n.13 - Factor de edad. El valor no debe ser inferior a 0.6.");
                                            else if (xelementsList3[0].ElementAt<XElement>(index).IsFull() && xelementsList3[1].ElementAt<XElement>(index).IsFull())
                                            {
                                                List<Decimal> numList = new List<Decimal>();
                                                numList.Add(xelementsList3[0].ToDecimalXElement(index));
                                                numList.Add(xelementsList3[1].ToDecimalXElement(index));
                                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_5_n_13(decimalXelement, numList[0], numList[1], uso))
                                                    stringBuilder.AppendLine("e.2.5.n.13 - Factor de edad." + str);
                                            }
                                            else
                                                stringBuilder.AppendLine("e.2.5.n.13 - Factor de edad." + str);
                                        }
                                        catch (FormatException ex)
                                        {
                                            stringBuilder.AppendLine("e.2.5.n.13 - Factor de edad. Tipo de dato erróneo" + str);
                                        }
                                    }
                                }
                        }
                    }
                    //else
                    //    stringBuilder.AppendLine("e.2.5.n.13 - Factor de edad." + str);
                }
                // JACM Se da de baja el campo 2021-02-04
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.5"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.5.n.14");
                    if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3) && (XmlUtils.XmlSearchById(root2, "e.2.5.n.2").ToStringXElement() != "W" && XmlUtils.XmlSearchById(root2, "e.2.5.n.2").ToStringXElement() != "H"))
                    {
                        Decimal decimalXelement = xelements3.ToDecimalXElement();
                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.10"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.13"));
                        bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.10")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.13"));
                        if (!XmlUtils.IsListEmpty(listElement) && flag3)
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                            if (FiscalUtils.EstaEnCatConservacion(listDecimal[0]))
                            {
                                listDecimal[0] = (Decimal)ApplicationCache.DseCatalogosConsulta.FEXAVA_CATESTADOCONSERV.FindByCODESTADOCONSERVACION(listDecimal[0]).VALOR;
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_5_n_14(decimalXelement, listDecimal[0], listDecimal[1]))
                                    stringBuilder.AppendLine("e.2.5.n.14 - Factor resultante." + str);
                            }
                            else
                                stringBuilder.AppendLine("e.2.5.n.14 - Factor resultante.El valor " + (object)listDecimal[0] + " no correponde a ningún un factor de conservacion" + str);
                        }
                        else
                            stringBuilder.AppendLine("e.2.5.n.14 - Factor resultante." + str);
                    }
                }
                if (esComercial)
                {
                    foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.5"))
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.5.n.15");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3) //&& XmlUtils.XmlSearchById(root2, "e.2.5.n.2").ToStringXElement() != "W"
                            )
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.12"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.13"));
                            // JACM Se da de baja el campo 2021-02-04
                            //listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.14"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.11"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.12"))
                                // JACM Se da de baja el campo 2021-02-04
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.13")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.11"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_5_n_15(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("e.2.5.n.15 - Valor de la fracción n." + str);
                            }
                            else
                                stringBuilder.AppendLine("e.2.5.n.15 - Valor de la fracción n." + str);
                        }
                    }
                }
                else
                {
                    foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.5"))
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.5.n.15");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3) //&& XmlUtils.XmlSearchById(root2, "e.2.5.n.2").ToStringXElement() != "W"
                            )
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.11"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.16"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.5.n.17"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.11")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.16")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.5.n.17"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                //log("e.2.5.n.15 - ", "Datos: " ,decimalXelement+"||"+listDecimal[0]);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_5_n_15_Cat(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("e.2.5.n.15 - Valor de la fracción n." + str);
                            }
                            else
                                stringBuilder.AppendLine("e.2.5.n.15 - Valor de la fracción n." + str);
                        }
                    }
                }
                if (esComercial)
                {
                    foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.1"))
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.1.n.15");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3) && 
                            XmlUtils.XmlSearchById(root2, "e.2.1.n.2").ToStringXElement() != "W")
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.11"));
                            // JACM Se da de baja el campo 2021-02-04
                            //listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.14"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.12"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.13"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.11")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.12")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.13"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_15(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("e.2.1.n.15 - Valor de la fracción n." + str);
                            }
                            else
                                stringBuilder.AppendLine("e.2.1.n.15 - Valor de la fracción n." + str);
                        }
                    }
                }
                else
                {
                    foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "e.2.1"))
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.1.n.15");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3) && XmlUtils.XmlSearchById(root2, "e.2.1.n.2").ToStringXElement() != "W")
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.11"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.16"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "e.2.1.n.17"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.11")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.16")) 
                                && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "e.2.1.n.17"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_1_n_15_Cat(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("e.2.1.n.15 - Valor de la fracción n." + str);
                            }
                            else
                                stringBuilder.AppendLine("e.2.1.n.15 - Valor de la fracción n." + str);
                        }
                    }
                }*/
                IEnumerable<XElement> rootN4 = XmlUtils.XmlSearchById(root1, "e.2");
                IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(rootN4, "e.2.6");
                if (xelements14.IsFull() && XmlUtils.EsDecimalXmlValido(xelements14) && (this.obligatorioComun || !this.existeWC))
                {
                    num3 = xelements14.ToDecimalXElement();
                    Decimal decimalXelement = xelements14.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN4, "e.2.5.n.11"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_e_2_n_11 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_6(decimalXelement, sumatorio_e_2_n_11))
                                stringBuilder.AppendLine("e.2.6 - Superficie total de construcciones COMUNES." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("e.2.6 - Superficie total de construcciones COMUNES. Tipo de dato erróneo" + str);
                        }
                    }
                    else if (!decimalXelement.Equals(0M))
                        stringBuilder.AppendLine("e.2.6 - Superficie total de construcciones COMUNES." + str);
                }
                IEnumerable<XElement> rootN5 = XmlUtils.XmlSearchById(root1, "e.2");
                IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(rootN5, "e.2.7");
                if (xelements15.IsFull() && XmlUtils.EsDecimalXmlValido(xelements15) && !this.existeWC)
                {
                    Decimal decimalXelement = xelements15.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN5, "e.2.5.n.15"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_e_2_5_n_15 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_7(decimalXelement, sumatorio_e_2_5_n_15))
                                stringBuilder.AppendLine("e.2.7 - Valor total de construcciones COMUNES." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("e.2.7 - Valor total de construcciones COMUNES. Tipo de dato erróneo" + str);
                        }
                    }
                    else if (!decimalXelement.Equals(0M))
                        stringBuilder.AppendLine("e.2.7 - Valor total de construcciones COMUNES." + str);
                }
                IEnumerable<XElement> rootN6 = XmlUtils.XmlSearchById(root1, "e.2");
                IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(rootN6, "e.2.8");
                if (flag2)
                {
                    if (xelements16.IsFull() && XmlUtils.EsDecimalXmlValido(xelements16))
                    {
                        Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements16);
                        IEnumerable<XElement> source4 = XmlUtils.XmlSearchById(root1, "e.2.5");
                        List<Decimal> ListaPorcentaje = new List<Decimal>();
                        List<Decimal> ListaValor = new List<Decimal>();
                        bool flag3 = false;
                        foreach (XElement root2 in source4)
                        {
                            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.5.n.15");
                            if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                            {
                                ListaValor.Add(XmlUtils.ToDecimalXElementAv(xelements3));
                            }
                            else
                            {
                                flag3 = true;
                                stringBuilder.AppendLine(string.Format("{0} - El campo es necesario para el calculo del campo 2.8", (object)"e.2.5.n.15 - Valor de la fracción n."));
                            }
                            if (!flag3)
                            {
                                IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root2, "e.2.5.n.18");
                                if (xelements17.IsFull())
                                {
                                    if (XmlUtils.EsDecimalXmlValidoDif100(xelements17))
                                        ListaPorcentaje.Add(XmlUtils.ToDecimalXElementAv(xelements17));
                                    else
                                        stringBuilder.AppendLine(string.Format("{0} - El campo debe ser menor al 100%.", (object)"e.2.5.n.18 -Porcentaje de indiviso."));
                                }
                                else
                                {
                                    flag3 = true;
                                    stringBuilder.AppendLine(string.Format("{0} - El campo es obligatorio para el regimen condominal.", (object)"e.2.5.n.18 -Porcentaje de indiviso."));
                                }
                            }
                        }
                        if (source4.Count<XElement>() == 0 && !esComercial)
                            flag3 = true;
                        if (!flag3)
                        {
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_8(ListaPorcentaje, ListaValor, decimalXelementAv))
                                stringBuilder.AppendLine("e.2.8 - Valor total de las construcciones por INDIVISO." + str);
                        }
                        else
                            stringBuilder.AppendLine("e.2.8 - Valor total de las construcciones por INDIVISO." + str);
                    }
                    else
                        stringBuilder.AppendLine(string.Format("{0} Falta campo o el tipo de dato es incorrecto.", (object)"e.2.8 - Valor total de las construcciones por INDIVISO."));
                }
                else if (xelements16.IsFull() && XmlUtils.EsDecimalXmlValido(xelements16) && !this.existeWC)
                {
                    Decimal decimalXelement = xelements16.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN6, "e.2.7"));
                    listElement.Add(XmlUtils.XmlSearchById(root1, "d.6"));
                    bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN6, "e.2.7")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "d.6"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        if (flag3)
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_e_2_8(decimalXelement, listDecimal[0], listDecimal[1]))
                                stringBuilder.AppendLine("e.2.8 - Valor total de las construcciones por INDIVISO." + str);
                        }
                        else
                            stringBuilder.AppendLine("e.2.8 - Valor total de las construcciones por INDIVISO." + str);
                    }
                    else if (!decimalXelement.Equals(0M))
                        stringBuilder.AppendLine("e.2.8 - Valor total de las construcciones por INDIVISO." + str);
                }
                /*foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.9.1"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.9.1.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.9.1.n.5").IsFull() && XmlUtils.XmlSearchById(root2, "f.9.1.n.6").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.9.1.n.8");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.1.n.5"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.1.n.6"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.1.n.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.1.n.6"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_9_1_n_8(decimalXelement, listDecimal[0], listDecimal[1]))
                                    stringBuilder.AppendLine("f.9.1.n.8 -  Factor de edad instalación especial PRIVATIVAS" + str + " En la instalación con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.9.1.n.8 -  Factor de edad instalación especial PRIVATIVAS" + str + " En la instalación con clave " + stringXelement);
                        }
                    }
                }*/
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.9.1"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.9.1.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.9.1.n.4").IsFull() && XmlUtils.XmlSearchById(root2, "f.9.1.n.7").IsFull() && 
                        XmlUtils.XmlSearchById(root2, "f.9.1.n.8").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.9.1.n.9");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.1.n.4"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.1.n.7"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.1.n.8"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.1.n.4")) && 
                                XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.1.n.7")) && 
                                XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.1.n.8"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_9_1_n_9(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("f.9.1.n.9 - Importe instalación especial PRIVATIVAS." + str + " En la instalación con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.9.1.n.9 - Importe instalación especial PRIVATIVAS." + str + " En la instalación con clave " + stringXelement);
                        }
                    }
                }
                /*foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.9.2"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.9.2.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.9.2.n.5").IsFull() && XmlUtils.XmlSearchById(root2, "f.9.2.n.6").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.9.2.n.8");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.2.n.5"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.2.n.6"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.2.n.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.2.n.6"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_9_2_n_8(decimalXelement, listDecimal[0], listDecimal[1]))
                                    stringBuilder.AppendLine("f.9.2.n.8 - Factor de edad instalación especial COMUNES." + str + " En la instalación con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.9.2.n.8 - Factor de edad instalación especial COMUNES." + str + " En la instalación con clave " + stringXelement);
                        }
                    }
                }*/
                IEnumerable<XElement> rootN7 = XmlUtils.XmlSearchById(root1, "f.9");
                IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(rootN7, "f.9.3");
                if (xelements18.IsFull() && XmlUtils.EsDecimalXmlValido(xelements18))
                {
                    Decimal decimalXelement = xelements18.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN7, "f.9.1.n.9"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_f_9_1_n_9 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_9_3(decimalXelement, sumatorio_f_9_1_n_9))
                                stringBuilder.AppendLine("f.9.3 - Importe total instalaciones especiales PRIVATIVAS." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("f.9.3 - Importe total instalaciones especiales PRIVATIVAS. Tipo de dato erróneo" + str);
                        }
                    }
                }

                //JACM Se da de baja el cálculo 2021-04-15
                /*foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.10.1"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.10.1.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.10.1.n.5").IsFull() && XmlUtils.XmlSearchById(root2, "f.10.1.n.6").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.10.1.n.8");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.1.n.5"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.1.n.6"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.1.n.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.1.n.6"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_10_1_n_8(decimalXelement, listDecimal[0], listDecimal[1]))
                                    stringBuilder.AppendLine("f.10.1.n.8 - Factor de edad elemento accesorio PRIVATIVAS." + str + " En la elemento con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.10.1.n.8 - Factor de edad elemento accesorio PRIVATIVAS." + str + " En la elemento con clave " + stringXelement);
                        }
                    }
                }*/
                /*foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.10.2"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.10.2.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.10.2.n.5").IsFull() && XmlUtils.XmlSearchById(root2, "f.10.2.n.6").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.10.2.n.8");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.2.n.5"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.2.n.6"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.2.n.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.2.n.6"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_10_2_n_8(decimalXelement, listDecimal[0], listDecimal[1]))
                                    stringBuilder.AppendLine("f.10.2.n.8 -  Factor de edad elemento accesorio COMUNES." + str + " En el elemento con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.10.1.n.8 - Factor de edad elemento accesorio PRIVATIVAS." + str + " En el elemento con clave " + stringXelement);
                        }
                    }
                }*/
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.10.1"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.10.1.n.9");
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.10.1.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.10.1.n.4").IsFull() && XmlUtils.XmlSearchById(root2, "f.10.1.n.7").IsFull() && (XmlUtils.XmlSearchById(root2, "f.10.1.n.8").IsFull() && xelements3.IsFull()) && XmlUtils.EsDecimalXmlValido(xelements3))
                    {
                        Decimal decimalXelement = xelements3.ToDecimalXElement();
                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.1.n.4"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.1.n.7"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.1.n.8"));
                        bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.1.n.4")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.1.n.7")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.1.n.8"));
                        if (!XmlUtils.IsListEmpty(listElement) && flag3)
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_10_1_n_9(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                stringBuilder.AppendLine("f.10.1.n.9 - Importe elemento accesorio PRIVATIVAS." + str + " En el elemento con clave " + stringXelement);
                        }
                        else
                            stringBuilder.AppendLine("f.10.1.n.9 - Importe elemento accesorio PRIVATIVAS." + str + " En el elemento con clave " + stringXelement);
                    }
                }
                IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "f.10"), "f.10.3");
                if (xelements19.IsFull() && XmlUtils.EsDecimalXmlValido(xelements19))
                {
                    Decimal decimalXelement = xelements19.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "f.10.1");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "f.10.1.n.9"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_f_10_1_n_9 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_10_3(decimalXelement, sumatorio_f_10_1_n_9))
                                stringBuilder.AppendLine("f.10.3 - Importe total elementos accesorios PRIVATIVAS." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("f.10.3 - Importe total elementos accesorios PRIVATIVAS. Tipo de dato erróneo" + str);
                        }
                    }
                }
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.11.1"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.11.1.n.9");
                    if (XmlUtils.XmlSearchById(root2, "f.11.1.n.4").IsFull() && XmlUtils.XmlSearchById(root2, "f.11.1.n.7").IsFull() && XmlUtils.XmlSearchById(root2, "f.11.1.n.8").IsFull())
                    {
                        string stringXelement = XmlUtils.XmlSearchById(root2, "f.11.1.n.1").ToStringXElement();
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.1.n.4"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.1.n.7"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.1.n.8"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.1.n.4")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.1.n.7")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.1.n.8"));
                            if (!XmlUtils.IsListEmpty(listElement) & flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_11_1_n_9(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                    stringBuilder.AppendLine("f.11.1.n.9 - Importe obra complementaria PRIVATIVAS." + str + " En la obra con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.11.1.n.9 - Importe obra complementaria PRIVATIVAS." + str + " En la obra con clave " + stringXelement);
                        }
                    }
                }
                IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(root1, "f.12");
                if (xelements20.IsFull() && XmlUtils.EsDecimalXmlValido(xelements20) && 
                    (XmlUtils.XmlSearchById(root1, "f.9.3").IsFull() && 
                    XmlUtils.XmlSearchById(root1, "f.10.3").IsFull()) && 
                    XmlUtils.XmlSearchById(root1, "f.11.3").IsFull())
                {
                    Decimal decimalXelement = xelements20.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.9.3"));
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.10.3"));
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.11.3"));
                    bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.9.3")) && 
                        XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.10.3")) && 
                        XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.11.3"));
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        if (!ValidarCamposCalculados.ValidarCampoCalculado_f_12(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                            stringBuilder.AppendLine("f.12 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios PRIVATIVAS." + str
                                );
                    }
                    else
                        stringBuilder.AppendLine("f.12 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios PRIVATIVAS." + str
                            );
                }

                if (esComercial)
                {
                    foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.9.2"))
                    {

                        string stringXelement = XmlUtils.XmlSearchById(root2, "f.9.2.n.1").ToStringXElement();
                        if (XmlUtils.XmlSearchById(root2, "f.9.2.n.4").IsFull() 
                            && XmlUtils.XmlSearchById(root2, "f.9.2.n.7").IsFull() 
                            && XmlUtils.XmlSearchById(root2, "f.9.2.n.8").IsFull())
                        {
                            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.9.2.n.9");
                            if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                            {
                                Decimal decimalXelement = xelements3.ToDecimalXElement();
                                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                                listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.2.n.4"));
                                listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.2.n.7"));
                                listElement.Add(XmlUtils.XmlSearchById(root2, "f.9.2.n.8"));
                                bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.2.n.4"))
                                          && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.2.n.7"))
                                          && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.9.2.n.8"));
                                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                                {
                                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                    if (!ValidarCamposCalculados.ValidarCampoCalculado_f_9_2_N_9(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                        stringBuilder.AppendLine("f.9.2.n.9 - Importe instalación especial COMUNES." + str + " En la instalación con clave " + stringXelement);
                                }
                                else
                                    stringBuilder.AppendLine("f.9.2.n.9 - Importe instalación especial COMUNES." + str + " En la instalación con clave " + stringXelement);
                            }
                        }
                    }
                }
                IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "f.9"), "f.9.4");
                if (xelements21.IsFull() && XmlUtils.EsDecimalXmlValido(xelements21))
                {
                    Decimal decimalXelement = xelements21.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "f.9.2");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "f.9.2.n.9"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_f_9_2_n_9 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_9_4(decimalXelement, sumatorio_f_9_2_n_9))
                                stringBuilder.AppendLine("f.9.4 - Importe total instalaciones especiales COMUNES." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("f.9.4 - Importe total instalaciones especiales COMUNES. Tipo de dato erróneo" + str);
                        }
                    }
                }
                if (esComercial)
                {
                    foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.10.2"))
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.10.2.n.9");
                        if (XmlUtils.XmlSearchById(root2, "f.10.2.n.4").IsFull()
                            && XmlUtils.XmlSearchById(root2, "f.10.2.n.7").IsFull()
                            && XmlUtils.XmlSearchById(root2, "f.10.2.n.8").IsFull())
                        {
                            string stringXelement = XmlUtils.XmlSearchById(root2, "f.10.2.n.1").ToStringXElement();
                            if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                            {
                                Decimal decimalXelement = xelements3.ToDecimalXElement();
                                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                                listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.2.n.4"));
                                listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.2.n.7"));
                                listElement.Add(XmlUtils.XmlSearchById(root2, "f.10.2.n.8"));
                                bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.2.n.4")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.2.n.7")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.10.2.n.8"));
                                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                                {
                                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                    if (!ValidarCamposCalculados.ValidarCampoCalculado_f_10_2_n_9(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                        stringBuilder.AppendLine("f.10.2.n.9 - Importe elemento accesorio COMUNES." + str + " En la elemento con clave " + stringXelement);
                                }
                                else
                                    stringBuilder.AppendLine("f.10.2.n.9 - Importe elemento accesorio COMUNES." + str + " En la elemento con clave " + stringXelement);
                            }
                        }
                    }
                }
                IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "f.10"), "f.10.4");
                if (xelements22.IsFull() && XmlUtils.EsDecimalXmlValido(xelements22))
                {
                    Decimal decimalXelement = xelements22.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "f.10.2");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "f.10.2.n.9"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_f_10_2_n_9 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_10_4(decimalXelement, sumatorio_f_10_2_n_9))
                                stringBuilder.AppendLine("f.10.4 - Importe total elementos accesorios COMUNES." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("f.10.4 - Importe total elementos accesorios COMUNES. Tipo de dato erróneo" + str);
                        }
                    }
                }

                // JACM Se da de baja el calculo 
                /*foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.11.1"))
                {
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.11.1.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.11.1.n.5").IsFull() && XmlUtils.XmlSearchById(root2, "f.11.1.n.6").IsFull())
                    {
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.11.1.n.8");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.1.n.5"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.1.n.6"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.1.n.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.1.n.6"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_11_1_n_8(decimalXelement, listDecimal[0], listDecimal[1], stringXelement))
                                    stringBuilder.AppendLine("f.11.1.n.8 - Factor de edad obra complementaria PRIVATIVAS." + str + " En la obra con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.11.1.n.8 - Factor de edad obra complementaria PRIVATIVAS." + str + " En la obra con clave " + stringXelement);
                        }
                    }
                }
                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.11.2"))
                {
                    if (XmlUtils.XmlSearchById(root2, "f.11.2.n.5").IsFull() && XmlUtils.XmlSearchById(root2, "f.11.2.n.6").IsFull())
                    {
                        string stringXelement = XmlUtils.XmlSearchById(root2, "f.11.2.n.1").ToStringXElement();
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.11.2.n.8");
                        if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                        {
                            Decimal decimalXelement = xelements3.ToDecimalXElement();
                            List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.2.n.5"));
                            listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.2.n.6"));
                            bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.2.n.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.2.n.6"));
                            if (!XmlUtils.IsListEmpty(listElement) && flag3)
                            {
                                List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_11_2_n_8(decimalXelement, listDecimal[0], listDecimal[1], stringXelement))
                                    stringBuilder.AppendLine("f.11.2.n.8 - Factor de edad obra complementaria COMUNES." + str + " En la obra con clave " + stringXelement);
                            }
                            else
                                stringBuilder.AppendLine("f.11.2.n.8 - Factor de edad obra complementaria COMUNES." + str + " En la obra con clave " + stringXelement);
                        }
                    }
                }*/


                foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.11.2"))
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "f.11.2.n.9");
                    string stringXelement = XmlUtils.XmlSearchById(root2, "f.11.2.n.1").ToStringXElement();
                    if (XmlUtils.XmlSearchById(root2, "f.11.2.n.4").IsFull() && XmlUtils.XmlSearchById(root2, "f.11.2.n.7").IsFull() && (XmlUtils.XmlSearchById(root2, "f.11.2.n.8").IsFull() && xelements3.IsFull()) && XmlUtils.EsDecimalXmlValido(xelements3))
                    {
                        Decimal decimalXelement = xelements3.ToDecimalXElement();
                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.2.n.4"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.2.n.7"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "f.11.2.n.8"));
                        bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.2.n.4")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.2.n.7")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "f.11.2.n.8"));
                        if (!XmlUtils.IsListEmpty(listElement) && flag3)
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_11_2_n_9(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                                stringBuilder.AppendLine("f.11.2.n.9 - Importe obra complementaria COMUNES." + str + " En la obra con clave " + stringXelement);
                        }
                        else
                            stringBuilder.AppendLine("f.11.2.n.9 - Importe obra complementaria COMUNES." + str + " En la obra con clave " + stringXelement);
                    }
                }
                IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "f.11"), "f.11.3");
                if (xelements23.IsFull() && XmlUtils.EsDecimalXmlValido(xelements23))
                {
                    Decimal decimalXelement = xelements23.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "f.11.1");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "f.11.1.n.9"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_f_11_1_n_9 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_11_3(decimalXelement, sumatorio_f_11_1_n_9))
                                stringBuilder.AppendLine("f.11.3 - Importe total obras complementarias PRIVATIVAS." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("f.11.3 - Importe total obras complementarias PRIVATIVAS. Tipo de dato erróneo" + str);
                        }
                    }
                }
                IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "f.11"), "f.11.4");
                if (xelements24.IsFull() && XmlUtils.EsDecimalXmlValido(xelements24))
                {
                    Decimal decimalXelement = xelements24.ToDecimalXElement();
                    IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "f.11.2");
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "f.11.2.n.9"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio_f_11_2_n_9 = XmlUtils.SumatorioCampo(listElement[0]);
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_11_4(decimalXelement, sumatorio_f_11_2_n_9))
                                stringBuilder.AppendLine("f.11.4 - Importe total obras complementarias COMUNES." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("f.11.4 - Importe total obras complementarias COMUNES. Tipo de dato erróneo" + str);
                        }
                    }
                }
                IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(root1, "f.13");
                if (xelements25.IsFull() && XmlUtils.EsDecimalXmlValido(xelements25) && (XmlUtils.XmlSearchById(root1, "f.9.4").IsFull() && XmlUtils.XmlSearchById(root1, "f.10.4").IsFull()) && XmlUtils.XmlSearchById(root1, "f.11.4").IsFull())
                {
                    Decimal decimalXelement = xelements25.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.9.4"));
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.10.4"));
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.11.4"));
                    bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.9.4")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.10.4")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.11.4"));
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        if (!ValidarCamposCalculados.ValidarCampoCalculado_f_13(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2]))
                            stringBuilder.AppendLine("f.13 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios COMUNES." + str);
                    }
                    else
                        stringBuilder.AppendLine("f.13 - Sumatoria Total Instalaciones Especiales Obras Complementarias Y Elementos Accesorios COMUNES." + str);
                }
                IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(root1, "f.14");
                if (xelements26.IsFull() && XmlUtils.EsDecimalXmlValido(xelements26))
                {
                    Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements26);
                    List<Decimal> ImporteEspecial = new List<Decimal>();
                    List<Decimal> PorcentajeEspecial = new List<Decimal>();
                    List<Decimal> ImporteAccesorio = new List<Decimal>();
                    List<Decimal> PorcentajeAccesorio = new List<Decimal>();
                    List<Decimal> ImporteComplementaria = new List<Decimal>();
                    List<Decimal> PorcentajeComplementaria = new List<Decimal>();
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "f.9.2");
                    bool flag3 = false;
                    foreach (XElement root2 in xelements3)
                    {
                        IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root2, "f.9.2.n.9");
                        if (xelements17.IsFull() && XmlUtils.EsDecimalXmlValido(xelements17))
                        {
                            ImporteEspecial.Add(XmlUtils.ToDecimalXElementAv(xelements17));
                        }
                        else
                        {
                            flag3 = true;
                            stringBuilder.AppendLine(string.Format("{0} - El campo es necesario para el calculo del campo F.14", (object)"f.9.2.n.9 - Importe instalación especial COMUNES."));
                        }
                        if (flag2)
                        {
                            if (!flag3)
                            {
                                IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(root2, "f.9.2.n.10");
                                if (xelements27.IsFull())
                                {
                                    if (XmlUtils.EsDecimalXmlValidoDif100(xelements27))
                                        PorcentajeEspecial.Add(XmlUtils.ToDecimalXElementAv(xelements27));
                                    else
                                        stringBuilder.AppendLine(string.Format("{0} - El campo debe ser menor al 100%.", (object)"f.9.2.n.10 -Porcentaje de indiviso."));
                                }
                                else
                                {
                                    flag3 = true;
                                    stringBuilder.AppendLine(string.Format("{0} - El campo es obligatorio para el regimen condominal.", (object)"f.9.2.n.10 -Porcentaje de indiviso."));
                                }
                            }
                            if (!flag3)
                                flag3 = ImporteEspecial.Count != PorcentajeEspecial.Count;
                        }
                    }
                    if (!flag3)
                    {
                        foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.10.2"))
                        {
                            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root2, "f.10.2.n.9");
                            if (xelements17.IsFull() && XmlUtils.EsDecimalXmlValido(xelements17))
                            {
                                ImporteAccesorio.Add(XmlUtils.ToDecimalXElementAv(xelements17));
                            }
                            else
                            {
                                flag3 = true;
                                stringBuilder.AppendLine(string.Format("{0} - El campo es necesario para el calculo del campo F.14", (object)"f.10.2.n.9 - Importe elemento accesorio COMUNES."));
                            }
                            if (flag2)
                            {
                                if (!flag3)
                                {
                                    IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(root2, "f.10.2.n.10");
                                    if (xelements27.IsFull())
                                    {
                                        if (XmlUtils.EsDecimalXmlValidoDif100(xelements27))
                                            PorcentajeAccesorio.Add(XmlUtils.ToDecimalXElementAv(xelements27));
                                        else
                                            stringBuilder.AppendLine(string.Format("{0} - El campo debe ser menor a 100%", (object)"f.10.2.n.10 -Porcentaje de indiviso."));
                                    }
                                    else
                                        stringBuilder.AppendLine(string.Format("{0} - El campo es obligatorio para el regimen condominal.", (object)"f.10.2.n.10 -Porcentaje de indiviso."));
                                }
                                if (!flag3)
                                    flag3 = ImporteAccesorio.Count != PorcentajeAccesorio.Count;
                            }
                        }
                    }
                    if (!flag3)
                    {
                        foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "f.11.2"))
                        {
                            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root2, "f.11.2.n.9");
                            if (xelements17.IsFull() && XmlUtils.EsDecimalXmlValido(xelements17))
                            {
                                ImporteComplementaria.Add(XmlUtils.ToDecimalXElementAv(xelements17));
                            }
                            else
                            {
                                flag3 = true;
                                stringBuilder.AppendLine(string.Format("{0} - El campo es necesario para el calculo del campo F.14", (object)"f.11.2.n.9 - Importe obra complementaria COMUNES."));
                            }
                            if (flag2)
                            {
                                if (!flag3)
                                {
                                    IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(root2, "f.11.2.n.10");
                                    if (xelements27.IsFull())
                                    {
                                        if (XmlUtils.EsDecimalXmlValidoDif100(xelements27))
                                            PorcentajeComplementaria.Add(XmlUtils.ToDecimalXElementAv(xelements27));
                                        else
                                            stringBuilder.AppendLine("f.11.2.n.10 -Porcentaje de indiviso.El campo debe ser menor al 100%.");
                                    }
                                    else
                                        stringBuilder.AppendLine("f.11.2.n.10 -Porcentaje de indiviso.El campo es obligatorio para el regimen condominal.");
                                }
                                if (!flag3)
                                    flag3 = ImporteComplementaria.Count != PorcentajeComplementaria.Count;
                            }
                        }
                    }
                    if (!flag3)
                    {
                        if (flag2)
                        {
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_f_14(ImporteEspecial, PorcentajeEspecial, ImporteAccesorio, PorcentajeAccesorio, ImporteComplementaria, PorcentajeComplementaria, decimalXelementAv))
                                stringBuilder.AppendLine("f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES." + str);
                        }
                        else
                        {
                            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root1, "d.6");
                            if (xelements17.IsFull() && XmlUtils.EsDecimalXmlValido(xelements17))
                            {
                                Decimal decimalXelement = xelements17.ToDecimalXElement();
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_f_14(ImporteEspecial, ImporteAccesorio, ImporteComplementaria, decimalXelement, decimalXelementAv))
                                    stringBuilder.AppendLine("f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES." + str);
                            }
                            else
                                stringBuilder.AppendLine("f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES." + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES." + str);
                }
                else if (XmlUtils.XmlSearchById(root1, "f.13").IsFull())
                {
                    Decimal decimalXelement = xelements26.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.13"));
                    listElement.Add(XmlUtils.XmlSearchById(root1, "d.6"));
                    bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.13")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "d.6"));
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        if (!ValidarCamposCalculados.ValidarCampoCalculado_f_14(decimalXelement, listDecimal[0], listDecimal[1]))
                            stringBuilder.AppendLine("f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES." + str);
                    }
                    else
                        stringBuilder.AppendLine("f.14 - Importe INDIVISO instalaciones especiales, obras complementarias y elementos accesorios COMUNES." + str);
                }
            }




            foreach (XElement root2 in XmlUtils.XmlSearchById(root1, "h.1.1"))
            {
                if (esComercial) { 
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "h.1.1.n.17");
                if (xelements3.IsFull())
                {
                    //bool flag3 = false;
                    // JACM Se da de baja el campo 2021-02-04
                    //if (XmlUtils.XmlSearchById(root2, "h.1.1.n.18").IsFull())
                    //    flag3 = true;
                    bool flag4 = XmlUtils.EsDecimalXmlValido(xelements3) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "h.1.1.n.10.2")) 
                        && (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "h.1.1.n.11.2")) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "h.1.1.n.12.2"))) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "h.1.1.n.13.2")) 
                        &&  XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "h.1.1.n.14.2"));
                    //if (flag3)
                    //    flag4 = flag4 && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root2, "h.1.1.n.18.1"));
                    if (flag4)
                    {
                        Decimal decimalXelement = xelements3.ToDecimalXElement();
                        List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                        listElement.Add(XmlUtils.XmlSearchById(root2, "h.1.1.n.10.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "h.1.1.n.11.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "h.1.1.n.12.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "h.1.1.n.13.2"));
                        listElement.Add(XmlUtils.XmlSearchById(root2, "h.1.1.n.14.2"));
                        //if (flag3)
                        //    listElement.Add(XmlUtils.XmlSearchById(root2, "h.1.1.n.18.1"));
                        if (!XmlUtils.IsListEmpty(listElement))
                        {
                            List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);

                                /*if (!this.esFactorZonaValido(listDecimal[0]))
                                    stringBuilder.AppendLine("h.1.1.n.10.2 - Valor no permitido");
                                if (!this.esFactorUbicacionValido(listDecimal[1]))
                                    stringBuilder.AppendLine("h.1.1.n.11.2 - Valor no permitido");
                                if (!this.esFactorFrenteValido(listDecimal[2]))
                                    stringBuilder.AppendLine("h.1.1.n.12.2 - Valor no permitido");
                                if (!this.esOfertaValida(listDecimal[3]))
                                    stringBuilder.AppendLine("h.1.1.n.13.2 - Valor no permitido");
                                if (!this.esFactorSuperficieValido(listDecimal[4]))
                                    stringBuilder.AppendLine("h.1.1.n.14.2 - Valor no permitido");*/

                                /*if (listDecimal[0] > 1.99M)
                                    stringBuilder.AppendLine("h.1.1.n.10.2 - Valor no permitido");
                                if (listDecimal[1] > 1.99M)
                                    stringBuilder.AppendLine("h.1.1.n.11.2 - Valor no permitido");
                                if (listDecimal[2] > 1.99M)
                                    stringBuilder.AppendLine("h.1.1.n.12.2 - Valor no permitido");
                                if (listDecimal[3] > 1.99M)
                                    stringBuilder.AppendLine("h.1.1.n.13.2 - Valor no permitido");
                                if (listDecimal[4] > 1.99M)
                                    stringBuilder.AppendLine("h.1.1.n.14.2 - Valor no permitido");*/

                                /*if (flag3)
                                {
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_h_1_1_n_17(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4]))
                                    stringBuilder.AppendLine("h.1.1.n.17 - Fre. Valor no permitido."); // + str);
                                 }
                                 else if (!ValidarCamposCalculados.ValidarCampoCalculado_h_1_1_n_17(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4]))
                                     stringBuilder.AppendLine("h.1.1.n.17 - Fre." + str);*/
                                string mensaje = "";
                                mensaje = ValidarCamposCalculados.ValidarCampoCalculado_h_1_1_n_17(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4]);
                                if (!mensaje.Equals(""))
                                    stringBuilder.AppendLine("h.1.1.n.17 - Fre. "+mensaje+"."); // + str);

                            }
                        else
                            stringBuilder.AppendLine("h.1.1.n.17 - Fre. No hay datos permitidos " + str);
                    }
                    else
                        stringBuilder.AppendLine("h.1.1.n.17 - Fre. No hay datos validos" + str);
                }
                }
            }
            string stringXelement1 = XmlUtils.XmlSearchById(root1, "b.3.10.1").ToStringXElement();
            string stringXelement2 = XmlUtils.XmlSearchById(root1, "b.3.10.2").ToStringXElement();
            string stringXelement3 = XmlUtils.XmlSearchById(root1, "b.3.10.3").ToStringXElement();
            if (!esComercial)
            {
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "a.2");
                DateTime dateTime = new DateTime();
                if (XmlUtils.EsFechaValida(xelements3))
                    dateTime = Convert.ToDateTime(xelements3.ToStringXElement());
                int year = dateTime.Year;
                int periodo = 1;
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "h.1.4");
                if (xelements4.IsFull())
                {
                    Decimal num5 = FiscalUtils.ValidarValorUnitarioSuelo(stringXelement1, stringXelement2, stringXelement3, year, periodo);
                    Decimal decimalXelement = xelements4.ToDecimalXElement();
                    Decimal num6 = Convert.ToDecimal("0.05", (IFormatProvider)CultureInfo.CreateSpecificCulture("es-MX"));
                    if (!decimalXelement.Equals(num5) && !decimalXelement.Equals(num5 + num6) && !decimalXelement.Equals(num5 - num6))
                        stringBuilder.AppendLine("h.1.4 - Valor unitario de tierra aplicable al avalúo. El valor esperado es: " + num5.ToString());
                }
            }
            IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "i"), "i.6");
            if (xelements28.IsFull() && XmlUtils.EsDecimalXmlValido(xelements28) && (this.obligatorioPriv || !this.existeWP) && ((this.obligatorioComun || !this.existeWC) && esComercial))
            {
                bool flag3 = true;
                Decimal decimalXelement = xelements28.ToDecimalXElement();
                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "d.13")))
                    listElement.Add(XmlUtils.XmlSearchById(root1, "d.13"));
                else
                    flag3 = false;
                if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.12")))
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.12"));
                else
                    flag3 = false;
                if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(root1, "f.14")))
                    listElement.Add(XmlUtils.XmlSearchById(root1, "f.14"));
                else
                    flag3 = false;
                IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "e.2");
                if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "e.2.3")))
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.3"));
                else
                    flag3 = false;
                if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "e.2.8")))
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.8"));
                else
                    flag3 = false;
                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                {
                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                    if (!ValidarCamposCalculados.ValidarCampoCalculado_i_6(decimalXelement, listDecimal[0], listDecimal[1], listDecimal[2], listDecimal[3], listDecimal[4]))
                        stringBuilder.AppendLine("i.6 - Importe total del enfoque de costos." + str);
                }
            }
            bool esCondominial = false;
            if (XmlUtils.XmlSearchById(root1, "b.6").ToStringXElement().Equals("2"))
                esCondominial = true;
            Decimal decimalXelement3 = XmlUtils.XmlSearchById(root1, "d.6").ToDecimalXElement();
            bool flag5 = this.TieneElementosDeLaConstruccion(XmlUtils.XmlSearchById(root1, "f"));
            IEnumerable<XElement> xelements29 = XmlUtils.XmlSearchById(XmlUtils.XmlSearchById(root1, "j"), "j.4");
            if (xelements29.IsFull() && XmlUtils.EsDecimalXmlValido(xelements29) && (this.obligatorioComun || !this.existeWC) && ((this.obligatorioPriv || !this.existeWP) && !esComercial))
            {
                bool flag3 = true;
                Decimal decimalXelement1 = xelements29.ToDecimalXElement();
                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "e");
                if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "e.2.3")))
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.3"));
                else
                    flag3 = false;
                if (esCondominial)
                {
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "e.2.8")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.8"));
                    else
                        flag3 = false;
                }
                else if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "e.2.7")))
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "e.2.7"));
                else
                    flag3 = false;
                if (flag5)
                {
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        if (!ValidarCamposCalculados.ValidarCampoCalculado_j_4(decimalXelement1, listDecimal[0], listDecimal[1], decimalXelement3, esCondominial))
                            stringBuilder.AppendLine("j.4 - Importe instalaciones especiales." + str);
                    }
                    else
                        stringBuilder.AppendLine("j.4 - Importe instalaciones especiales." + str);
                }
                else if (!xelements29.ToDecimalXElement().Equals(0M))
                    stringBuilder.AppendLine("j.4 - Importe instalaciones especiales." + str);
            }
            if (!esComercial)
            {
                IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "j");
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(rootN2, "j.5");
                if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                {
                    Decimal decimalXelement1 = xelements3.ToDecimalXElement();
                    xelementsList1 = new List<IEnumerable<XElement>>(4);
                    Decimal j_4 = !XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "j.4")) ? 0M : XmlUtils.XmlSearchById(rootN2, "j.4").ToDecimalXElement();
                    IEnumerable<XElement> rootN4 = XmlUtils.XmlSearchById(root1, "d");
                    Decimal d_13 = !XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN4, "d.13")) ? 0M : XmlUtils.XmlSearchById(rootN4, "d.13").ToDecimalXElement();
                    IEnumerable<XElement> rootN5 = XmlUtils.XmlSearchById(root1, "e");
                    Decimal e_2_3;
                    Decimal e_2_7;
                    if (!flag2)
                    {
                        e_2_3 = !XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "e.2.3")) ? 0M : XmlUtils.XmlSearchById(rootN5, "e.2.3").ToDecimalXElement();
                        e_2_7 = !XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "e.2.7")) ? 0M : XmlUtils.XmlSearchById(rootN5, "e.2.7").ToDecimalXElement();
                    }
                    else
                    {
                        e_2_3 = !XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "e.2.3")) ? 0M : XmlUtils.XmlSearchById(rootN5, "e.2.3").ToDecimalXElement();
                        e_2_7 = !XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "e.2.8")) ? 0M : XmlUtils.XmlSearchById(rootN5, "e.2.8").ToDecimalXElement();
                    }
                    if (!ValidarCamposCalculados.ValidarCampoCalculado_j_5(decimalXelement1, j_4, d_13, e_2_3, e_2_7, decimalXelement3, esCondominial))
                        stringBuilder.AppendLine("j.5 - Importe total valor catastral." + str);
                }
            }
            IEnumerable<XElement> rootN8 = XmlUtils.XmlSearchById(root1, "j");
            IEnumerable<XElement> xelements30 = XmlUtils.XmlSearchById(rootN8, "j.7");
            if (xelements30.IsFull() && XmlUtils.EsDecimalXmlValido(xelements30) && (this.obligatorioPriv || !this.existeWP) && ((this.obligatorioComun || !this.existeWC) && !esComercial))
            {
                Decimal decimalXelement1 = xelements30.ToDecimalXElement();
                List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                listElement.Add(XmlUtils.XmlSearchById(rootN8, "j.5"));
                listElement.Add(XmlUtils.XmlSearchById(rootN8, "j.6"));
                bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN8, "j.5")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN8, "j.6"));
                if (!XmlUtils.IsListEmpty(listElement) && flag3)
                {
                    List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                    if (!ValidarCamposCalculados.ValidarCampoCalculado_j_7(decimalXelement1, listDecimal[0], listDecimal[1]))
                        stringBuilder.AppendLine("j.7 - Importe total valor catastral obra en proceso." + str);
                }
                else
                    stringBuilder.AppendLine("j.7 - Importe total valor catastral obra en proceso." + str);
            }
            if (!this.esTerrenoValdio(root1))
            {
                IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "k.2");
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(rootN2, "k.2.12");
                if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                {
                    Decimal decimalXelement1 = xelements3.ToDecimalXElement();
                    num1 = 0M;
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.1"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.2"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.3"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.4"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.5"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.6"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.7"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.9"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "k.2.11"));
                    if (!XmlUtils.IsListEmpty(listElement))
                    {
                        try
                        {
                            Decimal sumatorio = this.SumatorioListDecimal(XmlUtils.ConvetListElementToListDecimal(listElement));
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_k_2_12(decimalXelement1, sumatorio))
                                stringBuilder.AppendLine("k.2.12 - Deducciones mensuales." + str);
                        }
                        catch (FormatException ex)
                        {
                            stringBuilder.AppendLine("k.2.12 - Deducciones mensuales. Tipo de dato erróneo" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("k.2.12 - Deducciones mensuales." + str);
                }
                IEnumerable<XElement> rootN4 = XmlUtils.XmlSearchById(root1, "k.2");
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(rootN4, "k.2.13");
                if (xelements4.IsFull() && XmlUtils.EsDecimalXmlValido(xelements4) && (this.obligatorioPriv || !this.existeWP) && esComercial)
                {
                    bool flag3 = true;
                    Decimal decimalXelement1 = xelements4.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN4, "k.2.12")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN4, "k.2.12"));
                    else
                        flag3 = false;
                    IEnumerable<XElement> rootN5 = XmlUtils.XmlSearchById(root1, "k");
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "k.1")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN5, "k.1"));
                    else
                        flag3 = false;
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        try
                        {
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_k_2_13(decimalXelement1, listDecimal[0], listDecimal[1]))
                                stringBuilder.AppendLine("k.2.13 - % Deducciones mensuales." + str);
                        }
                        catch (DivideByZeroException ex)
                        {
                            stringBuilder.AppendLine("k.2.13 - % Deducciones mensuales. No se puede dividir por cero" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("k.2.13 - % Deducciones mensuales." + str);
                }
                IEnumerable<XElement> rootN6 = XmlUtils.XmlSearchById(root1, "k");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(rootN6, "k.3");
                if (xelements5.IsFull() && XmlUtils.EsDecimalXmlValido(xelements5) && (this.obligatorioPriv || !this.existeWP) && esComercial)
                {
                    bool flag3 = true;
                    Decimal decimalXelement1 = xelements5.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN6, "k.1")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN6, "k.1"));
                    else
                        flag3 = false;
                    IEnumerable<XElement> rootN5 = XmlUtils.XmlSearchById(root1, "k.2");
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "k.2.12")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN5, "k.2.12"));
                    else
                        flag3 = false;
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        if (!ValidarCamposCalculados.ValidarCampoCalculado_k_3(decimalXelement1, listDecimal[0], listDecimal[1]))
                            stringBuilder.AppendLine("k.3 - % Producto Líquido anual." + str);
                    }
                    else
                        stringBuilder.AppendLine("k.3 - % Producto Líquido anual." + str);
                }
                IEnumerable<XElement> rootN7 = XmlUtils.XmlSearchById(root1, "k");
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(rootN7, "k.5");
                if (xelements9.IsFull() && XmlUtils.EsDecimalXmlValido(xelements9) && (this.obligatorioPriv || !this.existeWP) && esComercial)
                {
                    Decimal decimalXelement1 = xelements9.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN7, "k.3"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN7, "k.4"));
                    bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN7, "k.3")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN7, "k.4"));
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        try
                        {
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_k_5(decimalXelement1, listDecimal[0], listDecimal[1]))
                                stringBuilder.AppendLine("k.5 - Importe enfoque de ingresos." + str);
                        }
                        catch (DivideByZeroException ex)
                        {
                            stringBuilder.AppendLine("k.5 - Importe enfoque de ingresos. No se puede dividir por cero" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("k.5 - Importe enfoque de ingresos." + str);
                }
            }
            xelements1 = XmlUtils.XmlSearchById(root1, "p");
            if (XmlUtils.XmlSearchById(root1, "p").IsFull())
            {
                IEnumerable<XElement> rootN2 = XmlUtils.XmlSearchById(root1, "p");
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(rootN2, "p.4");
                if (xelements3.IsFull() && XmlUtils.EsDecimalXmlValido(xelements3))
                {
                    Decimal decimalXelement1 = xelements3.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "p.2"));
                    listElement.Add(XmlUtils.XmlSearchById(rootN2, "p.3"));
                    bool flag3 = XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "p.2")) && XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN2, "p.3"));
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        try
                        {
                            if (!ValidarCamposCalculados.ValidarCampoCalculado_p_4(decimalXelement1, listDecimal[0], listDecimal[1]))
                                stringBuilder.AppendLine("p.4 - Factor de conversión." + str);
                        }
                        catch (DivideByZeroException ex)
                        {
                            stringBuilder.AppendLine("p.4 - Factor de conversión. No se puede dividir por cero" + str);
                        }
                    }
                    else
                        stringBuilder.AppendLine("p.4 - Factor de conversión." + str);
                }
                string fecha = string.Empty;
                IEnumerable<XElement> rootN4 = XmlUtils.XmlSearchById(root1, "p");
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(rootN4, "p.5");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(rootN4, "p.1");
                if (xelements5.IsFull())
                    fecha = xelements5.ToStringXElement();
                if (xelements4.IsFull() && XmlUtils.EsDecimalXmlValido(xelements4))
                {
                    bool flag3 = true;
                    Decimal decimalXelement1 = xelements4.ToDecimalXElement();
                    List<IEnumerable<XElement>> listElement = new List<IEnumerable<XElement>>();
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN4, "p.4")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN4, "p.4"));
                    else
                        flag3 = false;
                    IEnumerable<XElement> rootN5 = XmlUtils.XmlSearchById(root1, "o");
                    if (XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(rootN5, "o.1")))
                        listElement.Add(XmlUtils.XmlSearchById(rootN5, "o.1"));
                    else
                        flag3 = false;
                    if (!XmlUtils.IsListEmpty(listElement) && flag3)
                    {
                        List<Decimal> listDecimal = XmlUtils.ConvetListElementToListDecimal(listElement);
                        if (!string.IsNullOrEmpty(fecha))
                        {
                            try
                            {
                                if (!ValidarCamposCalculados.ValidarCampoCalculado_p_5(decimalXelement1, listDecimal[0], listDecimal[1], fecha))
                                    stringBuilder.AppendLine("p.5 - Valor referido." + str);
                            }
                            catch (DivideByZeroException ex)
                            {
                                stringBuilder.AppendLine("p.5 - Valor referido. No se puede dividir por cero" + str);
                            }
                        }
                        else
                            stringBuilder.AppendLine("p.5 - Valor referido. No se puede dividir por cero Es necesario ingresar valor al campo p.1.");
                    }
                    else
                        stringBuilder.AppendLine("p.5 - Valor referido." + str);
                }
            }
            Decimal num7 = 0M;
            if (num2 + num3 > 0M && num4 > 0M)
            {
                num7 = num4 * 100M / (num2 + num3);
                if (num7 > 10M)
                    stringBuilder.Append("El uso baldío W, aplica solo para construcciones menores al 10% de la superficie total de terreno  (e.2.2 + e.2.6). El porcentaje actual es: " + (num7 / 100M).ToPercent());
            }
            if (esComercial)
            {
                if (!XmlUtils.XmlSearchById(root1, "q.2").IsFull() && (!this.esTerrenoValdio(root1) || num7 > 10M))
                    stringBuilder.AppendLine("q.2 - Comparable rentas Campo Obligatorio.");
                if (!XmlUtils.XmlSearchById(root1, "q.3").IsFull() && (!this.esTerrenoValdio(root1) || num7 > 10M))
                    stringBuilder.AppendLine("q.3 - Comparable ventas. Campo Obligatorio.");
            }
            if (stringBuilder.Length > 0)
            {

                ExceptionPolicyWrapper.HandleException(new Exception(stringBuilder.ToString()));
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
            }
        }

        private bool esFactorZonaValido(Decimal factor) => new List<Decimal>()
    {
      1M,
      1.2M,
      0.8M
    }.Contains(factor);

        private bool esFactorUbicacionValido(Decimal factor) => new List<Decimal>()
    {
      0.7M,
      1M,
      1.15M,
      1.25M,
      1.35M
    }.Contains(factor);

        private bool esFactorFormaValido(Decimal factor) => new List<Decimal>()
    {
      1M
    }.Contains(factor);

        private bool esOfertaValida(Decimal factor) => new List<Decimal>()
    {
      1M
    }.Contains(factor);

        private bool esFactorFrenteValido(Decimal factor) => new List<Decimal>()
    {
      1M,
      0.8M,
      0.6M
    }.Contains(factor);

        private bool esFactorSuperficieValido(Decimal factor)
        {
            factor *= 100M;
            return !(factor < 62M) && !(factor > 100M) && factor % 2M == 0M;
        }

        private bool usoNoBaldioConSuper(XElement cursor, bool privativa)
        {
            IEnumerable<XElement> xelements1;
            IEnumerable<XElement> xelements2;
            if (privativa)
            {
                xelements1 = XmlUtils.XmlSearchById(cursor, "e.2.1.n.2");
                xelements2 = XmlUtils.XmlSearchById(cursor, "e.2.1.n.11");
            }
            else
            {
                xelements1 = XmlUtils.XmlSearchById(cursor, "e.2.5.n.2");
                xelements2 = XmlUtils.XmlSearchById(cursor, "e.2.5.n.11");
            }
            if (!xelements1.IsFull())
                return false;
            if (!xelements1.ToStringXElement().Equals("W"))
                return true;
            if (privativa)
                this.existeWP = true;
            else
                this.existeWC = true;
            if (xelements2.IsFull())
            {
                if (xelements2.ToDecimalXElement() == 0M)
                {
                    if (privativa)
                        this.obligatorioPriv = false;
                    else
                        this.obligatorioComun = false;
                    return false;
                }
                if (privativa)
                    this.obligatorioPriv = this.obligatorioPriv;
                else
                    this.obligatorioComun = this.obligatorioComun;
                return true;
            }
            if (privativa)
                this.obligatorioPriv = false;
            else
                this.obligatorioComun = false;
            return false;
        }

        private static int ObtenerPeriodo(DateTime fecha) => (int)Math.Ceiling((Decimal)fecha.Month / 2M);

        private bool TieneElementosDeLaConstruccion(
          IEnumerable<XElement> queryElementosDeLaConstruccion)
        {
            IEnumerable<XElement> source1 = XmlUtils.XmlSearchById(queryElementosDeLaConstruccion, "f.9");
            IEnumerable<XElement> source2 = XmlUtils.XmlSearchById(queryElementosDeLaConstruccion, "f.10");
            IEnumerable<XElement> source3 = XmlUtils.XmlSearchById(queryElementosDeLaConstruccion, "f.11");
            return source1.Any<XElement>() || source2.Any<XElement>() || source3.Any<XElement>();
        }

        private void ValidarCuentaCatastral(XmlDocument xmlAvaluo)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            string empty3 = string.Empty;
            string empty4 = string.Empty;
            string empty5 = string.Empty;
            string empty6 = string.Empty;
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            IEnumerable<XElement> rootN = XmlUtils.XmlSearchById(root, "b.3.10");
            string stringXelement1 = XmlUtils.XmlSearchById(rootN, "b.3.10.1").ToStringXElement();
            string stringXelement2 = XmlUtils.XmlSearchById(rootN, "b.3.10.2").ToStringXElement();
            string stringXelement3 = XmlUtils.XmlSearchById(rootN, "b.3.10.3").ToStringXElement();
            string stringXelement4 = XmlUtils.XmlSearchById(rootN, "b.3.10.4").ToStringXElement();
            string stringXelement5 = XmlUtils.XmlSearchById(rootN, "b.3.10.5").ToStringXElement();
            XmlUtils.XmlSearchById(root, "a.3").ToStringXElement();
            if (!this.VerificarCuentaCat(stringXelement1, stringXelement2, stringXelement3, stringXelement4))
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("La cuenta catastral no existe"));
            if (!this.EsDigitoVerificadorValido(stringXelement1, stringXelement2, stringXelement3, stringXelement4, stringXelement5))
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("El dígito verificador no es correcto"));
        }

        private bool esAvaluoBloqueado(
          string registro,
          string region,
          string manzana,
          string lote,
          string unidad)
        {
            bool flag = false;
            try
            {
                int rowsTotal = -1;
                DseAvaluoConsulta dseAvaluoConsulta = this.ObtenerAvaluosPorCuentaVigenciaEstado(string.Format("{0}-{1}-{2}-{3}", (object)region, (object)manzana, (object)lote, (object)unidad), 0, "S", 8, 1, ref rowsTotal, "FECHA_PRESENTACION DESC");
                if (dseAvaluoConsulta.FEXAVA_AVALUO_V.Any<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>())
                    flag = dseAvaluoConsulta.FEXAVA_AVALUO_V.Where<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>((Func<DseAvaluoConsulta.FEXAVA_AVALUO_VRow, bool>)(row => row.REGISTRO_PERITO.Equals(registro))).Count<DseAvaluoConsulta.FEXAVA_AVALUO_VRow>() == 0;
            }
            catch (Exception ex)
            {
                flag = true;
            }
            return flag;
        }

        public DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable ValidarXmlValorUnitarioSuelo(
          string region,
          string manzana,
          string lote,
          string unidad,
          string areaValor,
          Decimal valorUnitario)
        {
            try
            {
                DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable errorDT = new DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable();
                StringBuilder stringBuilder1 = new StringBuilder();
                dsAnalisisValoresInmobiliario.AVA_SERVINMOBILIARIOAVALUODataTable servinmobiliarioavaluoDataTable = new dsAnalisisValoresInmobiliario.AVA_SERVINMOBILIARIOAVALUODataTable();
                AnalisisValoresInmobiliarioClient proxy = new AnalisisValoresInmobiliarioClient();
                int num1 = StringExtension.ToInt(Settings.Default.MinMuestras);
                if (num1 > 0)
                {
                    try
                    {
                        servinmobiliarioavaluoDataTable = proxy.ObtenerValidacionAvaluo(region, manzana, lote, unidad, (string)null);
                    }
                    finally
                    {
                        proxy.Disconnect();
                    }
                    StringBuilder stringBuilder2 = new StringBuilder();
                    if (servinmobiliarioavaluoDataTable[0].NUMMUESTRAS < (Decimal)num1 || servinmobiliarioavaluoDataTable[0].IsMEDIAVUSNull())
                    {
                        StringBuilder stringBuilder3 = stringBuilder2.AppendLine("@TIPO_ERROR:0");
                        stringBuilder3.AppendLine("No ha sido posible hacer la validación de valor unitario de suelo por falta de datos. ¿Desea continuar?");
                        this.AddErrorToDT(ref errorDT, stringBuilder3.ToString());
                    }
                    else
                    {
                        if (servinmobiliarioavaluoDataTable[0].IsDESVIACIONNull())
                            servinmobiliarioavaluoDataTable[0].DESVIACION = 0M;
                        Decimal num2 = servinmobiliarioavaluoDataTable[0].MEDIAVUS - servinmobiliarioavaluoDataTable[0].DESVIACION;
                        Decimal num3 = servinmobiliarioavaluoDataTable[0].MEDIAVUS + servinmobiliarioavaluoDataTable[0].DESVIACION;
                        if (!(valorUnitario <= num3) || !(valorUnitario >= num2))
                        {
                            stringBuilder2.AppendLine("@TIPO_ERROR:1");
                            stringBuilder2.AppendLine("El valor unitario de suelo del avalúo no se encuentra entre en el rango del valor mínimo y máximo de VUS en base a la media para el área de valor. ¿Desea continuar?");
                            this.AddErrorToDT(ref errorDT, stringBuilder2.ToString());
                        }
                    }
                }
                if (stringBuilder1.Length > 0)
                    this.AddErrorToDT(ref errorDT, stringBuilder1.ToString());
                return errorDT;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        private void ValidarEnfoqueCostosComercial(XmlDocument xmlAvaluo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            IEnumerable<XElement> rootN = XmlUtils.XmlSearchById(root, "e.2");
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "a.2");
            DateTime dateTime = new DateTime();
            string str = string.Empty;
            bool esComercial = root.Descendants((XName)"Comercial").Count<XElement>() > 0;
            if (XmlUtils.EsFechaValida(xelements1))
            {
                dateTime = Convert.ToDateTime(xelements1.ToStringXElement());
                str = this.DarFormatoFechaXML(xelements1.ToStringXElement());
            }
            Decimal? idClaseEjercicio = new Decimal?();
            Decimal? idUsoEjercicio = new Decimal?();
            string codClase = "";
            string codUso = "";
            string rangoNiv = "";
            int numNiv = -1;
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(rootN, "e.2.1");
            if (elementos1.IsFull())
            {
                foreach (XElement xelement in elementos1)
                {
                    string stringXelement = XmlUtils.XmlSearchById(xelement, "e.2.1.n.1").ToStringXElement();
                    if (this.usoNoBaldioConSuper(xelement, true))
                    {
                        IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(xelement, "e.2.1.n.2");
                        if (xelements2.IsFull())
                        {
                            codUso = xelements2.ToStringXElement();
                            if (!FiscalUtils.ExisteCatUsoEjercicio(xelements2.ToStringXElement(), dateTime, out idUsoEjercicio))
                                stringBuilder.AppendLine("e.2.1.n.2 - No existe un uso ejercicio para la fecha " + str + " y codUso " + xelements2.ToStringXElement());
                        }
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(xelement, "e.2.1.n.4");
                        if (xelements3.IsFull())
                        {
                            rangoNiv = xelements3.ToStringXElement();
                            if (!FiscalUtils.ExisteCatRangoNivelesEjercicio(xelements3.ToStringXElement(), dateTime))
                                stringBuilder.AppendLine("e.2.1.n.4 - No existe un rango nivel ejercicio para la fecha" + str + " y codRangoNiveles " + xelements3.ToStringXElement());
                        }
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(xelement, "e.2.1.n.3");
                        if (xelements4.IsFull())
                            numNiv = xelements4.ToDecimalXElement().ToInt();
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelement, "e.2.1.n.6");
                        if (xelements5.IsFull())
                        {
                            codClase = xelements5.ToStringXElement();
                            if (!FiscalUtils.ExisteCatClaseEjercicio(xelements5.ToStringXElement(), dateTime, out idClaseEjercicio))
                                stringBuilder.AppendLine("e.2.1.n.6 - No existe una clase Ejercicio para la fecha " + str + " y codClase " + xelements5.ToStringXElement());
                        }
                        if (!this.ExisteClaseUsoEjercicio(idClaseEjercicio, idUsoEjercicio))
                            stringBuilder.AppendLine("No existe relación entre clase(e.2.1.n.6) " + codClase + " y  uso(e.2.1.n.2) " + codUso + " para la fecha " + str);
                        IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(xelement, "e.2.1.n.8");
                        if (xelements6.IsFull() && codUso != "W" && !esComercial)
                        {
                            Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements6);
                            if (!this.ValidarCatUsoClase(codUso, codClase, decimalXelementAv, dateTime))
                                stringBuilder.AppendLine("e.2.1.n.8 - La vida útil especificada no es correctapara la clase y el uso especificados: Clase " + codClase + ", Uso " + codUso);
                        }
                        IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(xelement, "e.2.1.n.16");
                        if (xelements7.IsFull() && codUso != "W")
                        {
                            int periodo = 1;
                            Decimal num1 = FiscalUtils.ValidarValorUnitarioConstruccion(codUso, codClase, rangoNiv, numNiv, dateTime.Year, periodo);
                            Decimal decimalXelement = xelements7.ToDecimalXElement();
                            Decimal num2 = Convert.ToDecimal("0.01", (IFormatProvider)CultureInfo.CreateSpecificCulture("es-MX"));
                            if (!decimalXelement.Equals(num1) && !decimalXelement.Equals(num1 + num2) && !decimalXelement.Equals(num1 - num2))
                                stringBuilder.AppendLine("e.2.1.n.16 - El valor unitario de construcción no es correcto para: Uso: " + codUso + ", Rango niveles: " + rangoNiv + ", Clase:  " + codClase + ", descripción: " + stringXelement + ". El valor ESPERADO es: " + num1.ToString());
                        }
                    }
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(rootN, "e.2.5");
            if (elementos2.IsFull())
            {
                foreach (XElement xelement in elementos2)
                {
                    string stringXelement1 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.1").ToStringXElement();
                    if (this.usoNoBaldioConSuper(xelement, false))
                    {
                        IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.2");
                        if (xelements2.IsFull())
                        {
                            codUso = xelements2.ToStringXElement();
                            if (!FiscalUtils.ExisteCatUsoEjercicio(xelements2.ToStringXElement(), dateTime, out idUsoEjercicio))
                                stringBuilder.AppendLine("e.2.5.n.2 - No existe un uso ejercicio para la fecha " + str + " y codUso " + xelements2.ToStringXElement());
                        }
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(xelement, " e.2.5.n.3");
                        if (xelements3.IsFull())
                            numNiv = xelements3.ToDecimalXElement().ToInt();
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.4");
                        if (xelements4.IsFull())
                        {
                            string stringXelement2 = xelements4.ToStringXElement();
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.4");
                            if (xelements5.IsFull() && !FiscalUtils.ExisteCatRangoNivelesEjercicio(xelements5.ToStringXElement(), dateTime))
                                stringBuilder.AppendLine("e.2.5.n.4 - No existe un rango nivel ejercicio para la fecha" + str + " y codRangoNiveles " + xelements5.ToStringXElement());
                            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.6");
                            if (xelements6.IsFull())
                            {
                                codClase = xelements6.ToStringXElement();
                                if (!FiscalUtils.ExisteCatClaseEjercicio(xelements6.ToStringXElement(), dateTime, out idClaseEjercicio))
                                    stringBuilder.AppendLine("e.2.5.n.6 - No existe una clase Ejercicio para la fecha " + str + " y codClase " + xelements6.ToStringXElement());
                            }
                            if (!this.ExisteClaseUsoEjercicio(idClaseEjercicio, idUsoEjercicio))
                                stringBuilder.AppendLine("No existe relación entre clase(e.2.5.n.6) " + codClase + " y  uso(e.2.5.n.2) " + codUso + " para la fecha " + str);

                            if (!esComercial)
                            {
                                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.8");
                                if (xelements7.IsFull() && codUso != "W")
                                {
                                    Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements7);
                                    if (!this.ValidarCatUsoClase(codUso, codClase, decimalXelementAv, dateTime))
                                        stringBuilder.AppendLine("e.2.5.n.8 - La vida útil especificada no es correcta para la clase y el uso especificados: Clase " + codClase + ", Uso " + codUso);
                                }
                            }

                            IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(xelement, "e.2.5.n.16");
                            if (xelements8.IsFull())
                            {
                                int periodo = 1;
                                Decimal num1 = FiscalUtils.ValidarValorUnitarioConstruccion(codUso, codClase, stringXelement2, numNiv, dateTime.Year, periodo);
                                Decimal decimalXelement = xelements8.ToDecimalXElement();
                                Decimal num2 = Convert.ToDecimal("0.01", (IFormatProvider)CultureInfo.CreateSpecificCulture("es-MX"));
                                if (codUso != "W" && !decimalXelement.Equals(num1) && (!decimalXelement.Equals(num1 - num2) && !decimalXelement.Equals(num1 + num2)))
                                    stringBuilder.AppendLine("e.2.5.n.16 - El valor unitario de construcción no es correcto para: Uso: " + codUso + ", Rango niveles: " + stringXelement2 + ", Clase:  " + codClase + ", descripción: " + stringXelement1 + ". El valor ESPERADO es: " + num1.ToString());
                            }
                        }
                    }
                }
            }
            if (stringBuilder.Length > 0)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
        }

        private void ValidarCaracteristicasUrbanas(XmlDocument xmlAvaluo)
        {
            Decimal? idClaseEjercicio = new Decimal?();
            StringBuilder stringBuilder = new StringBuilder();
            XElement root = XDocument.Parse(xmlAvaluo.InnerXml).Root;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "a.2");
            DateTime fecha = new DateTime();
            string str = string.Empty;
            if (XmlUtils.EsFechaValida(xelements1))
            {
                fecha = Convert.ToDateTime(xelements1.ToStringXElement());
                str = this.DarFormatoFechaXML(xelements1.ToStringXElement());
            }
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "c.3");
            if (xelements2.IsFull() && !FiscalUtils.ExisteCatClaseEjercicio(xelements2.ToStringXElement(), fecha, out idClaseEjercicio))
                stringBuilder.AppendLine(" c.3 - No existe una clase ejercicio para la fecha " + str + " y codClase " + xelements2.ToStringXElement());
            if (stringBuilder.Length > 0)
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(stringBuilder.ToString()));
        }

        public bool ValidarCatUsoClase(
          string codUso,
          string codClase,
          Decimal vidaUtilTotal,
          DateTime fechaAvaluo)
        {
            try
            {
                Decimal? idUsoEjercicio = new Decimal?();
                Decimal? idClaseEjercicio = new Decimal?();
                return FiscalUtils.ExisteCatClaseEjercicio(codClase, fechaAvaluo, out idClaseEjercicio) && FiscalUtils.ExisteCatUsoEjercicio(codUso, fechaAvaluo, out idUsoEjercicio) && FiscalUtils.EstaEnCatEjercicio(fechaAvaluo) && (codClase.Equals("U") || this.ComprobarEdadUtilTipo((Decimal)FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(fechaAvaluo, codUso), (Decimal)FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaAvaluo, codClase), vidaUtilTotal));
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        private bool ComprobarEdadUtilTipo(
          Decimal idUsoEjercicio,
          Decimal idClaseEjercicio,
          Decimal vidaUtilTotal)
        {
            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase((int)idUsoEjercicio, (int)idClaseEjercicio);
            return source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>() && vidaUtilTotal == source[0].EDADUTIL;
        }

        private bool VerificarCuentaCat(string region, string manzana, string lote, string unidad)
        {
            ConsultaCatastralServiceClient proxy = new ConsultaCatastralServiceClient();
            try
            {
                return proxy.GetInmuebleByClave(region, manzana, lote, unidad).Inmueble.Any<DseInmueble.InmuebleRow>();
            }
            catch (FaultException<ConsultaCatastralInfoException> ex)
            {
                if (ex.Message.ToUpper().Trim().Equals("El predio solicitado no es fiscalmente válido".ToUpper().Trim()))
                    throw new FaultException<AvaluosInfoException>(new AvaluosInfoException("La cuenta catastral no existe"));
                throw;
            }
            finally
            {
                proxy.Disconnect();
            }
        }

        private string ConvierteBaseDecimal(string origen)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char o in origen.ToUpper().ToCharArray())
            {
                if (int.TryParse(o.ToString(), out int _))
                    stringBuilder.Append(o.ToString());
                else
                    stringBuilder.Append((o.ToInt() - 55).ToString());
            }
            return stringBuilder.ToString();
        }

        public int ExisteAvaluoAsociado(
          string región,
          string manzana,
          string lote,
          string unidadprivativa)
        {
            return this.Avaluo_vTA.ExisteAvaluoAsociado(región, manzana, lote, unidadprivativa).ToInt();
        }

        public bool ExisteClaseUsoEjercicio(Decimal? idClaseejercicio, Decimal? idUsoejercicio) => true;

        public int ExisteAvaluoRegistrado(string numAvaluo, Decimal idpersona, bool esPerito)
        {
            Decimal EXISTE;
            if (esPerito)
                this.Avaluo_vTA.ExisteAvaluoRegistrado(numAvaluo.Trim(), new Decimal?(idpersona), "0", out EXISTE);
            else
                this.Avaluo_vTA.ExisteAvaluoRegistrado(numAvaluo.Trim(), new Decimal?(idpersona), "1", out EXISTE);
            return EXISTE.ToInt();
        }

        private void AddErrorToDT(
          ref DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable errorDT,
          string errorLog)
        {
            if (errorDT == null)
                errorDT = new DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable();
            string str = string.Empty;
            string[] strArray = errorLog.Split(new string[1]
            {
        "\r\n"
            }, StringSplitOptions.RemoveEmptyEntries);
            for (int index = 0; index < strArray.Length; ++index)
            {
                if (strArray[index].Contains("@TIPO_ERROR:"))
                {
                    str = strArray[index].Replace("@TIPO_ERROR:", string.Empty);
                }
                else
                {
                    if (strArray[index].Length >= 200)
                    {
                        int startIndex = 0;
                        if (strArray[index].Contains(". El valor"))
                            startIndex = strArray[index].IndexOf(". El valor") + 11;
                        if (strArray[index].Contains("- The value"))
                            startIndex = strArray[index].IndexOf("- The value") + 12;
                        if (startIndex + 150 <= strArray[index].Length)
                        {
                            if (!strArray[index].Substring(startIndex, 150).Contains(" "))
                                strArray[index] = this.AcortarString(strArray[index], 150);
                        }
                        else if (200 - startIndex > 0)
                        {
                            if (!strArray[index].Substring(startIndex, 200 - startIndex).Contains(" "))
                                strArray[index] = this.AcortarString(strArray[index], 150);
                        }
                        else if (!strArray[index].Substring(startIndex, 1).Contains(" "))
                            strArray[index] = this.AcortarString(strArray[index], 150);
                    }
                    strArray[index] = this.TraducirError(strArray[index]);
                    DseAvaluoConsulta.ERROR_VALIDACION_AVALUORow row = errorDT.NewERROR_VALIDACION_AVALUORow();
                    row.TIPOERROR = str;
                    row.DESCRIPCION = strArray[index];
                    errorDT.AddERROR_VALIDACION_AVALUORow(row);
                }
            }
        }

        private string AcortarString(string str, int length)
        {
            string str1 = str;
            if (str.Length > length)
                str1 = str.Substring(0, length) + "...'";
            return str1;
        }

        private string TraducirError(string strError)
        {
            string str = strError;
            if (strError.Contains("Enumeration"))
                str = str.Replace("Enumeration", "Enumeración");
            if (strError.Contains("MaxLength"))
                str = str.Replace("MaxLength", "longitud máxima");
            if (strError.Contains("NonNegativeInteger"))
                str = str.Replace("NonNegativeInteger", "número entero no negativo");
            if (strError.Contains("Integer"))
                str = str.Replace("Integer", "número entero no negativo");
            if (strError.Contains("String"))
                str = str.Replace("String", "Cadena");
            if (strError.Contains("MinInclusive"))
                str = str.Replace("MinInclusive", "mínimo incluido");
            if (strError.Contains("MaxInclusive"))
                str = str.Replace("MaxInclusive", "máximo incluido");
            if (strError.Contains("Minlength"))
                str = str.Replace("Minlength", "longitud mínima");
            if (strError.Contains("Maxlength"))
                str = str.Replace("Maxlength", "longitud máxima");
            if (strError.Contains("XsdDateTime"))
                str = str.Replace("XsdDateTime", "fecha");
            if (strError.Contains("DateTime"))
                str = str.Replace("DateTime", "fecha");
            if (strError.Contains("Pattern"))
                str = str.Replace("Pattern", "formato");
            return str;
        }

        private void AnadirErrorValidacionALista(
          ref StringBuilder avaluoValidateMessage,
          string desc,
          string tipo)
        {
            avaluoValidateMessage.AppendLine(tipo);
            avaluoValidateMessage.AppendLine(desc);
            avaluoValidateMessage.AppendLine();
        }

        public bool EsXml(byte[] xmlByte)
        {
            try
            {
                new XmlDocument().Load((Stream)new MemoryStream(xmlByte));
                return true;
            }
            catch (XmlException ex)
            {
                return false;
            }
        }

        private bool EsInteger(string str)
        {
            try
            {
                StringExtension.ToInt(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool EsDecimal(string str)
        {
            try
            {
                StringExtension.ToDecimal(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string DarFormatoFechaXML(string fechaXml)
        {
            string empty = string.Empty;
            DateTime dateTime = Convert.ToDateTime(fechaXml);
            return dateTime.Day.ToString() + "/" + dateTime.Month.ToString() + "/" + dateTime.Year.ToString();
        }

        private string BooleanXMLtoOracle(string s)
        {
            string str = string.Empty;
            if (s == "1" || s.ToUpper() == "TRUE")
                str = "1";
            else if (s == "0" || s.ToUpper() == "FALSE")
                str = "0";
            return str;
        }

        private TipoFotoInmueble TipoInmueble(XElement query)
        {
            switch (query.Value.ToString())
            {
                case "I":
                    return TipoFotoInmueble.I;
                default:
                    return TipoFotoInmueble.F;
            }
        }

        public SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseNotarios BusquedaNotarios(
          Decimal? numero,
          string nombre,
          string apellidoPaterno,
          string apellidoMaterno,
          string rfc,
          string curp,
          string claveife,
          Decimal pageSize,
          Decimal indice,
          ref int rowsTotal,
          string SortExpression)
        {
            try
            {
                object C_NOTARIOS = (object)null;
                SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseNotarios dseNotarios = new SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseNotarios();
                rowsTotal = 0;
                this.NotarioTA.FillByBusqueda(dseNotarios.FEXAVA_NOTARIOS, numero, nombre, apellidoPaterno, apellidoMaterno, rfc, curp, claveife, new Decimal?(pageSize), new Decimal?(indice), SortExpression, out C_NOTARIOS);
                if (dseNotarios.FEXAVA_NOTARIOS.Any<SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseNotarios.FEXAVA_NOTARIOSRow>())
                    rowsTotal = Convert.ToInt32(dseNotarios.FEXAVA_NOTARIOS[0].ROWS_TOTAL);
                return dseNotarios;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public static Decimal AplicarFormulaPrivativas(
          Decimal e_2_1_n_11,
          Decimal e_2_2,
          Decimal e_2_6,
          Decimal d_6,
          Decimal e_2_1_n)
        {
            return e_2_1_n_11 / (e_2_2 + e_2_6 * d_6) * e_2_1_n;
        }

        public static Decimal AplicarFormulaComunes(
          Decimal e_2_2,
          Decimal e_2_6,
          Decimal d_6,
          Decimal e_2_5_n)
        {
            return e_2_6 * d_6 / (e_2_2 + e_2_6 * d_6) * e_2_5_n;
        }

        private Decimal SumatorioListDecimal(List<Decimal> listDecimal)
        {
            Decimal num1 = 0M;
            foreach (Decimal num2 in listDecimal)
                num1 += num2;
            return num1;
        }

        private Decimal IdPeritoSociedadByRegistro(
          string registroPerito,
          string registroSoci,
          bool esPerito)
        {
            if (esPerito)
            {
                PeritosSociedadesClient proxy = new PeritosSociedadesClient();
                DsePeritosSociedades peritosSociedades = (DsePeritosSociedades)null;
                try
                {
                    peritosSociedades = proxy.GetRelacionPeritoSociedad(registroPerito, (string)null);
                }
                finally
                {
                    proxy.Disconnect();
                }
                if (peritosSociedades.Perito.Any<DsePeritosSociedades.PeritoRow>())
                    return peritosSociedades.Perito[0].IDPERITO;
            }
            else
            {
                PeritosSociedadesClient proxy = new PeritosSociedadesClient();
                DsePeritosSociedades peritosSociedades = (DsePeritosSociedades)null;
                try
                {
                    peritosSociedades = proxy.GetRelacionPeritoSociedad(registroPerito, registroSoci);
                }
                finally
                {
                    proxy.Disconnect();
                }
                if (peritosSociedades.SociedadValuacion.Any<DsePeritosSociedades.SociedadValuacionRow>())
                    return peritosSociedades.SociedadValuacion[0].IDSOCIEDAD;
            }
            return -1M;
        }

        private DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable ObtenerClaseUsoByIdUsoIdClase(
          int idUsoEjercicio,
          int idClaseEjercicio)
        {
            object C_CATCLASEUSO = (object)null;
            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable dataTable = new DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable();
            ApplicationCache.CatClaseUsoTA.FillBy(dataTable, new Decimal?((Decimal)idUsoEjercicio), new Decimal?((Decimal)idClaseEjercicio), out C_CATCLASEUSO);
            return dataTable;
        }

        public string ObtenerNombreDelegacion(string codDelegacion) => CatastralUtils.ObtenerNombreDelegacion(codDelegacion);

        public string ObtenerDigitoVerificadorBD(
          string region,
          string manzana,
          string lote,
          string unidadPrivativa)
        {
            return CatastralUtils.ObtenerDigitoVerificadorBD(region, manzana, lote, unidadPrivativa);
        }

        private bool aplicaDepreciacion(string coduso)
        {
            string[] strArray = new string[4];
            bool flag = true;
            if (((IEnumerable<string>)strArray).Contains<string>(coduso))
                flag = false;
            return flag;
        }

        public bool esTerrenoValdio(XElement data)
        {
            bool flag1 = false;
            bool flag2 = false;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(data, "e.2.1");
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(data, "e.2.5");
            if (xelements1.IsFull() && xelements1.Count<XElement>() > 0)
                flag1 = true;
            if (xelements2.IsFull() && xelements2.Count<XElement>() > 0)
                flag2 = true;
            return !flag1 && !flag2;
        }

        public DataSet GetInvestigacionMercado(
          string region,
          string manzana,
          string tipo,
          Decimal idDelegacion,
          Decimal idColonia,
          string fechaInicio,
          string fechaFinal)
        {
            DataSet dataSet = new DataSet();
            DateTime dateTime1 = DateTime.Parse(fechaInicio);
            DateTime dateTime2 = DateTime.Parse(fechaFinal);
            Decimal? nullable1 = idDelegacion.ToString().Equals("-1") ? new Decimal?() : new Decimal?(idDelegacion);
            Decimal? nullable2 = idColonia.ToString().Equals("-1") ? new Decimal?() : new Decimal?(idColonia);
            try
            {
                TranHelper tranHelper = new TranHelper();
                using (OracleCommand cmd = new OracleCommand("FEXAVA.FEXAVA_INVMERCADO_PKG_MX.FEXAVA_OBTENDATOSINVESTMERCADO"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new OracleParameter("P_REGION", OracleDbType.Varchar2, (int)short.MaxValue)).Value = string.IsNullOrEmpty(region) ? (object)(string)null : (object)region;
                    cmd.Parameters.Add(new OracleParameter("P_MANZANA", OracleDbType.Varchar2, (int)short.MaxValue)).Value = string.IsNullOrEmpty(manzana) ? (object)(string)null : (object)manzana;
                    cmd.Parameters.Add(new OracleParameter("P_TIPO", OracleDbType.Varchar2, (int)short.MaxValue)).Value = tipo.Equals("T") ? (object)(string)null : (object)tipo;
                    cmd.Parameters.Add(new OracleParameter("P_DELEGACION", (object)nullable1));
                    cmd.Parameters.Add(new OracleParameter("P_COLONIA", (object)nullable2));
                    cmd.Parameters.Add(new OracleParameter("P_FECHAINICIO", OracleDbType.Date)).Value = (object)dateTime1;
                    cmd.Parameters.Add(new OracleParameter("P_FECHAFIN", OracleDbType.Date)).Value = (object)dateTime2;
                    cmd.Parameters.Add(new OracleParameter("P_CONSULTA", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    return tranHelper.EjecutaConsultaSP(cmd);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public bool EsDigitoVerificadorValido(
          string region,
          string manzana,
          string lote,
          string unidad,
          string digito)
        {
            try
            {
                string empty = string.Empty;
                TranHelper tranHelper = new TranHelper();
                using (OracleCommand cmd = new OracleCommand("FEXAVA.FEXAVA_AVALUOS_PKG.FEXAVA_VALIDA_DIGITOVF_P"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_REGION", OracleDbType.Char, 3).Value = (object)region;
                    cmd.Parameters.Add("P_MANZANA", OracleDbType.Char, 3).Value = (object)manzana;
                    cmd.Parameters.Add("P_LOTE", OracleDbType.Char, 2).Value = (object)lote;
                    cmd.Parameters.Add("P_UNIDADPRIVATIVA", OracleDbType.Char, 3).Value = (object)unidad;
                    cmd.Parameters.Add("P_DIGITOVERIFICADOR", OracleDbType.Char, 1).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_CODRES", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("P_DESRES", OracleDbType.Varchar2, (int)short.MaxValue).Direction = ParameterDirection.Output;
                    tranHelper.EjecutaNonQuerySP(cmd);
                    return cmd.Parameters["P_DIGITOVERIFICADOR"].Value.ToString().Equals(digito);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        public DataSet ObtenerAvaluosCedula(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          string idAvaluo,
          string numeroAvaluo,
          string registroPerito,
          string cuentaCatastral,
          int pageSize,
          int indice,
          string sortExpression,
          ref int rowsTotal,
          ref string errorMessage)
        {
            DataSet dataSet = new DataSet();
            SecurityCore.TransactionHelper transactionHelper = new SecurityCore.TransactionHelper();
            string empty = string.Empty;
            errorMessage = string.Empty;
            try
            {
                using (OracleCommand oracleCommand = new OracleCommand("fexava.fexava_admonelectr_avaluos_pkg.fexava_obtn_cedula_avaluo_p"))
                {
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(new OracleParameter("P_FECHAINICIO", OracleDbType.Date)).Value = (object)(fechaInicio.HasValue ? fechaInicio : new DateTime?());
                    oracleCommand.Parameters.Add(new OracleParameter("P_FECHAFIN", OracleDbType.Date)).Value = (object)(fechaFin.HasValue ? fechaFin : new DateTime?());
                    oracleCommand.Parameters.Add(new OracleParameter("P_NUMEROAVALUO", OracleDbType.Varchar2, 200)).Value = string.IsNullOrEmpty(numeroAvaluo) ? (object)(string)null : (object)numeroAvaluo;
                    oracleCommand.Parameters.Add(new OracleParameter("P_NUMEROAUNICO", OracleDbType.Varchar2, 200)).Value = string.IsNullOrEmpty(idAvaluo) ? (object)(string)null : (object)idAvaluo;
                    oracleCommand.Parameters.Add(new OracleParameter("P_CUENTA", OracleDbType.Varchar2, 200)).Value = string.IsNullOrEmpty(cuentaCatastral) ? (object)(string)null : (object)cuentaCatastral;
                    oracleCommand.Parameters.Add(new OracleParameter("P_REGISTRO", OracleDbType.Varchar2, 200)).Value = string.IsNullOrEmpty(registroPerito) ? (object)(string)null : (object)registroPerito;
                    oracleCommand.Parameters.Add(new OracleParameter("PAR_PAGESIZE", OracleDbType.Int32)).Value = (object)pageSize;
                    oracleCommand.Parameters.Add(new OracleParameter("PAR_PAGE", OracleDbType.Int32)).Value = (object)indice;
                    oracleCommand.Parameters.Add(new OracleParameter("SORTEXPRESSION", OracleDbType.Varchar2, 200)).Value = (object)sortExpression;
                    oracleCommand.Parameters.Add(new OracleParameter("ROWS_TOTAL", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    oracleCommand.Parameters.Add(new OracleParameter("C_AVALUOS", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    dataSet = transactionHelper.EjecutaConsultaSP(oracleCommand);
                    string str = oracleCommand.Parameters["ROWS_TOTAL"].Value.ToString();
                    rowsTotal = string.IsNullOrEmpty(str) ? 0 : Convert.ToInt32(str);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
            finally
            {
            }
            return dataSet;
        }

        public bool EsFolioValido(string folio, string cuenta, ref string mensaje)
        {
            bool flag = false;
            mensaje = string.Empty;
            SecurityCore.TransactionHelper transactionHelper = new SecurityCore.TransactionHelper();
            cuenta = cuenta.Replace("-", "");
            cuenta = cuenta.Substring(0, cuenta.Length - 1);
            try
            {
                using (OracleCommand oracleCommand = new OracleCommand("fexava.fexava_admonelectr_avaluos_pkg.fexava_validafolio_avaluo_p"))
                {
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(new OracleParameter("P_FOLIO", OracleDbType.Varchar2, 1000)).Value = (object)folio;
                    oracleCommand.Parameters.Add(new OracleParameter("P_CUENTA", OracleDbType.Varchar2, 1000)).Value = (object)cuenta;
                    oracleCommand.Parameters.Add(new OracleParameter("P_VALIDO", OracleDbType.Int32)).Direction = ParameterDirection.Output;
                    oracleCommand.Parameters.Add(new OracleParameter("p_error", OracleDbType.Varchar2, 1000)).Direction = ParameterDirection.Output;
                    transactionHelper.EjecutaConsultaSP(oracleCommand);
                    flag = oracleCommand.Parameters["P_VALIDO"].Value.ToString().Equals("1");
                    mensaje = oracleCommand.Parameters["p_error"].Value.ToString();
                    mensaje = mensaje.Equals("null") ? string.Empty : mensaje;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
            finally
            {
            }
            return flag;
        }

        public bool GuardarCedula(Cedula cedula, Decimal idAvaluo, string estado, Decimal idUsuario)
        {
            bool flag = false;
            SecurityCore.TransactionHelper transactionHelper = new SecurityCore.TransactionHelper();
            string empty = string.Empty;
            DateTime dateTime = DateTime.Parse(cedula.fechaIngreso);
            try
            {
                using (OracleCommand oracleCommand = new OracleCommand("fexava.fexava_admonelectr_avaluos_pkg.fexava_insupd_detcedul_avalu_p"))
                {
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(new OracleParameter("P_IDAVALUO", OracleDbType.Int32)).Value = (object)idAvaluo;
                    oracleCommand.Parameters.Add(new OracleParameter("P_ESTADO", OracleDbType.Varchar2)).Value = (object)estado;
                    oracleCommand.Parameters.Add(new OracleParameter("P_folio", OracleDbType.Varchar2)).Value = (object)cedula.folio;
                    oracleCommand.Parameters.Add(new OracleParameter("P_fechaingreso_avaluo_xml", OracleDbType.Date)).Value = (object)dateTime;
                    oracleCommand.Parameters.Add(new OracleParameter("P_superficie_terreno_xml", OracleDbType.Int32)).Value = (object)StringExtension.ToDecimal(cedula.superficieTerreno);
                    oracleCommand.Parameters.Add(new OracleParameter("P_numero_escritura_xml", OracleDbType.Varchar2)).Value = (object)cedula.numeroEscritura;
                    oracleCommand.Parameters.Add(new OracleParameter("P_superficie_contruccion_xml", OracleDbType.Varchar2)).Value = (object)StringExtension.ToDecimal(cedula.superficieConstruccion);
                    oracleCommand.Parameters.Add(new OracleParameter("P_clasificacion_xml", OracleDbType.Varchar2)).Value = (object)cedula.clasificacion;
                    oracleCommand.Parameters.Add(new OracleParameter("P_edad_xml", OracleDbType.Varchar2)).Value = (object)cedula.edad;
                    oracleCommand.Parameters.Add(new OracleParameter("P_reque_juridico", OracleDbType.Int32)).Value = (object)cedula.tieneRequerimiento.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_condominio_completo", OracleDbType.Int32)).Value = (object)cedula.necesarioPresentar.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_sin_soporte_superterreno", OracleDbType.Int32)).Value = (object)cedula.sinSoporteTerreno.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_sin_soporte_superconstru", OracleDbType.Int32)).Value = (object)cedula.sinSoporteConstruccion.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_p_acot_cro_levantamiento", OracleDbType.Int32)).Value = (object)cedula.planoaAcotados.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_clasificacion_uso", OracleDbType.Int32)).Value = (object)cedula.clasificacionIncorrecta.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_no_usos_descubiertos", OracleDbType.Int32)).Value = (object)cedula.noConsideraDescubiertos.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_no_usos_cubiertos", OracleDbType.Int32)).Value = (object)cedula.noConsideraCubiertos.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_demerito_edad", OracleDbType.Int32)).Value = (object)cedula.sinDemerito.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_rango", OracleDbType.Int32)).Value = (object)cedula.rangoIncorrecto.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_rectificacion_cuenta", OracleDbType.Int32)).Value = (object)cedula.necesitaRatificacion.ToDecimal();
                    oracleCommand.Parameters.Add(new OracleParameter("P_idusuario ", OracleDbType.Int32)).Value = (object)idUsuario;
                    oracleCommand.Parameters.Add(new OracleParameter("P_val_catast_sigapred", OracleDbType.Int32)).Value = (object)StringExtension.ToDecimal(cedula.valorCatastralCas);
                    oracleCommand.Parameters.Add(new OracleParameter("p_error", OracleDbType.Varchar2)).Direction = ParameterDirection.Output;
                    transactionHelper.EjecutaConsultaSP(oracleCommand);
                    string upper = oracleCommand.Parameters["p_error"].Value.ToString().ToUpper();
                    flag = string.IsNullOrEmpty(upper) || upper.Equals("NULL");
                    if (!flag)
                        throw new Exception(string.Format("Error al insertar la información. Detalle: {0}", (object)upper));
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
            return flag;
        }

        public Cedula ObtenerCedula(Decimal idAvaluo)
        {
            Cedula cedula;
            try
            {
                cedula = this.SetDatosCedula(this.ObtnerCedulaBase(idAvaluo) ?? throw new Exception("No fue posible obtener los datos de la cédula."));
                if (string.IsNullOrEmpty(cedula.fechaIngreso))
                    cedula = this.ObtenerDatosAvaluo(idAvaluo, cedula);
                cedula.fechaIngreso = DateTime.Parse(cedula.fechaIngreso).ToShortDateString();
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
            return cedula;
        }

        private DataSet ObtnerCedulaBase(Decimal idAvaluo)
        {
            DataSet dataSet = (DataSet)null;
            SecurityCore.TransactionHelper transactionHelper = new SecurityCore.TransactionHelper();
            try
            {
                using (OracleCommand oracleCommand = new OracleCommand("fexava.fexava_admonelectr_avaluos_pkg.fexa_obt_detcedul_avaluo_xml_p"))
                {
                    oracleCommand.CommandType = CommandType.StoredProcedure;
                    oracleCommand.Parameters.Add(new OracleParameter("P_IDAVALUO", OracleDbType.Int32)).Value = (object)idAvaluo;
                    oracleCommand.Parameters.Add(new OracleParameter("C_AVALUOS", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    dataSet = transactionHelper.EjecutaConsultaSP(oracleCommand);
                    if (dataSet.Tables.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows.Count <= 0)
                            dataSet = (DataSet)null;
                    }
                    else
                        dataSet = (DataSet)null;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new Exception(string.Format("Ocurrio un error al consultar los datos de la cédula. Detalles: {0}", (object)ex.Message));
            }
            finally
            {
            }
            return dataSet;
        }

        protected Cedula SetDatosCedula(DataSet datos)
        {
            Cedula cedula = new Cedula();
            try
            {
                DataRow row = datos.Tables[0].Rows[0];
                cedula.folio = row.ToStringValue("FOLIO");
                cedula.fechaIngreso = row.ToStringValue("FECHAINGRESO_AVALUO_XML");
                cedula.cuenta = row.ToStringValue("CUENTA");
                cedula.registroPerito = row.ToStringValue("CLAVE_PERITO/SOCIEDAD_XML");
                cedula.nombrePerito = row.ToStringValue("NOMBRE_PERITO/SOCIEDAD_XML");
                cedula.superficieTerreno = this.ChangeFormat(row.ToStringValue("SUPERFICIE_TERRENO_XML"));
                cedula.superficieTerrenoCas = this.ChangeFormat(row.ToStringValue("SUPERFICIE_TERRENO_SIGAPRED"));
                cedula.numeroEscritura = row.ToStringValue("NUMERO_ESCRITURA_XML");
                cedula.superficieConstruccion = this.ChangeFormat(row.ToStringValue("SUP_CONSTRUCCION_XML"));
                cedula.superficieConstruccionCas = this.ChangeFormat(row.ToStringValue("SUP_CONSTRUCCION_XML"));
                cedula.clasificacion = row.ToStringValue("CLASIFICACION_XML");
                cedula.clasificacionCas = row.ToStringValue("CLASIFICACION_SIGAPRED");
                cedula.edad = row.ToStringValue("EDAD_XML");
                cedula.edadCas = row.ToStringValue("EDAD_SIGAPRED");
                cedula.NombreUsuario = row.ToStringValue("NOMBRE_USUARIO");
                cedula.valorCatastral = this.ChangeFormat(row.ToStringValue("CATAST_COMER_XML"));
                cedula.valorCatastralCas = this.ChangeFormat(row.ToStringValue("CATAST_SIGAPRED "));
                cedula.valorCatastralCas = this.ChangeFormat(string.IsNullOrEmpty(cedula.valorCatastralCas) ? "0" : cedula.valorCatastralCas);
                cedula.tieneRequerimiento = Convert.ToBoolean(row.ToStringValue("REQUE_JURIDICO"));
                cedula.necesarioPresentar = Convert.ToBoolean(row.ToStringValue("CONDOMINIO_COMPLETO"));
                cedula.sinSoporteTerreno = Convert.ToBoolean(row.ToStringValue("SIN_SOPORTE_SUPERTERRENO"));
                cedula.sinSoporteConstruccion = Convert.ToBoolean(row.ToStringValue("SIN_SOPORTE_SUPERCONSTRU"));
                cedula.planoaAcotados = Convert.ToBoolean(row.ToStringValue("P_ACOT_CRO_LEVANTAMIENTO"));
                cedula.clasificacionIncorrecta = Convert.ToBoolean(row.ToStringValue("CLASIFICACION_USO"));
                cedula.noConsideraDescubiertos = Convert.ToBoolean(row.ToStringValue("NO_USOS_DESCUBIERTOS"));
                cedula.noConsideraCubiertos = Convert.ToBoolean(row.ToStringValue("NO_USOS_CUBIERTOS"));
                cedula.sinDemerito = Convert.ToBoolean(row.ToStringValue("DEMERITO_EDAD"));
                cedula.rangoIncorrecto = Convert.ToBoolean(row.ToStringValue("RANGO"));
                cedula.necesitaRatificacion = Convert.ToBoolean(row.ToStringValue("RECTIFICACION_CUENTA"));
                if (cedula.registroPerito.Contains("/"))
                {
                    string[] strArray = cedula.registroPerito.Split('/');
                    if (((IEnumerable<string>)strArray).Count<string>() > 1)
                    {
                        cedula.registroPerito = strArray[0];
                        cedula.registroSociedad = strArray[1];
                    }
                }
                else
                    cedula.registroSociedad = string.Empty;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new Exception(string.Format("Ocurrio un error al consultar los datos de la cédula. Detalles: {0}", (object)ex.Message));
            }
            return cedula;
        }

        private Cedula ObtenerDatosAvaluo(Decimal idAvaluo, Cedula cedula)
        {
            try
            {
                List<string> stringList = this.ObtenerDatosAvaluo(idAvaluo);
                if (stringList.Count <= 0)
                    throw new Exception("No se obtuvieron todos los valores del avalúo");
                cedula.fechaIngreso = stringList.Count >= 4 ? stringList[0] : throw new Exception("No se obtuvieron todos los valores del avalúo");
                cedula.numeroEscritura = stringList[1];
                cedula.clasificacion = stringList[2];
                cedula.edad = stringList[3];
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
            return cedula;
        }

        private string ChangeFormat(string value)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                double result = 0.0;
                if (double.TryParse(value, out result))
                    str = string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0:0,0.0}", (object)result);
            }
            return str;
        }

        private List<string> ObtenerDatosAvaluo(Decimal idAvaluo)
        {
            List<string> stringList = new List<string>();
            XElement root = XDocument.Parse(this.GetXmlAvaluo(idAvaluo).InnerXml).Root;
            bool esComercial = (Decimal)root.Descendants((XName)"Comercial").Count<XElement>() > 0M;
            try
            {
                XmlDocument xmlAvaluo;
                try
                {
                    xmlAvaluo = this.GetXmlAvaluo(idAvaluo);
                }
                catch
                {
                    throw new Exception("No existe xml asociado a al avaluo.");
                }
                XElement root1 = XDocument.Parse(xmlAvaluo.InnerXml).Root;
                XmlUtils.XmlSearchById(root1, "a.2");
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "a.2");
                if (!xelements1.IsFull())
                    throw new Exception("No fue posible obtener la fecha del avaluo");
                stringList.Add(xelements1.First<XElement>().Value.To<DateTime>().ToShortDateString());
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "d.4.1.1");
                if (xelements2.IsFull())
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(xelements2, "d.4.1.1.1");
                    if (!xelements3.IsFull())
                        throw new Exception("No fue posible obtener el numero de escritura.");
                    stringList.Add(xelements3.ToStringXElement());
                }
                else
                {
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "d.4.1.2");
                    if (xelements3.IsFull())
                    {
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(xelements3, "d.4.1.2.3");
                        if (!xelements4.IsFull())
                            throw new Exception("No fue posible obtener el numero de escritura.");
                        stringList.Add(xelements4.ToStringXElement());
                    }
                    else if (XmlUtils.XmlSearchById(root1, "d.4.1.3").IsFull())
                    {
                        stringList.Add("Contrato privado");
                    }
                    else
                    {
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "d.4.1.4");
                        IEnumerable<XElement> xelements5 = xelements4.IsFull() ? XmlUtils.XmlSearchById(xelements4, "d.4.1.4.2") : throw new Exception("No fue posible obtener el numero de escritura.");
                        if (!xelements5.IsFull())
                            throw new Exception("No fue posible obtener el numero de escritura.");
                        stringList.Add(xelements5.ToStringXElement());
                    }
                }
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root1, "e.2.1");
                string str1 = string.Empty;
                string str2 = string.Empty;
                string str3 = string.Empty;
                if (xelements6.IsFull())
                {
                    XElement root2 = xelements6.FirstOrDefault<XElement>();
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.1.n.2");
                    if (xelements3.IsFull())
                        str1 = xelements3.ToStringXElement();
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root2, "e.2.1.n.3");
                    if (xelements4.IsFull())
                        str2 = xelements4.ToStringXElement();
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "e.2.1.n.6");
                    if (xelements5.IsFull())
                        str3 = xelements5.ToStringXElement();
                }
                stringList.Add(string.Format("{0}-{1}-{2}", (object)str1, (object)str2, (object)str3));
                
                if (xelements6.IsFull())
                {
                    List<Decimal> source = new List<Decimal>();
                    foreach (XElement root2 in xelements6)
                    {
                        //Revisar si hay que poner un valor 0
                        //if (!esComercial) { 
                            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root2, "e.2.1.n.7");
                            if (xelements3.IsFull())
                                source.Add(xelements3.ToDecimalXElement());
                       //}
                    }
                    if (source.Count <= 0)
                        throw new Exception("No fue posible obtener la edad del avalúo.");
                    stringList.Add(source.Max().ToString());
                }
                else
                    stringList.Add("0");
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new Exception(string.Format("No fue posible obtener los datos del avaluo. Detalle: {0}", (object)ex.Message));
            }
            return stringList;
        }

        public DseAvaluoMantInf TestAv() => new DseAvaluoMantInf();

        public DseAvaluoMantInf GetAvaluoAntecedentes(Decimal idAvaluo)
        {
            DseAvaluoMantInf dseAvaluo = new DseAvaluoMantInf();
            List<Decimal> numList = new List<Decimal>();
            try
            {
                XElement root = XDocument.Parse(this.GetXmlAvaluo(idAvaluo).InnerXml).Root;
                foreach (DataColumn column in (InternalDataCollectionBase)dseAvaluo.FEXAVA_AVALUO.Columns)
                    column.AllowDBNull = true;
                dseAvaluo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(dseAvaluo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow());
                dseAvaluo.FEXAVA_AVALUO[0].CODESTADOAVALUO = EstadosAvaluoEnum.Recibido.ToDecimal();
                dseAvaluo.FEXAVA_AVALUO[0].FECHA_PRESENTACION = DateTime.Now.Date;
                if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                    dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE = "1";
                else if (root.Descendants((XName)"Catastral").Count<XElement>() > 0)
                    dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE = "2";
                IEnumerable<XElement> xelements = XmlUtils.XmlSearchById(root, "b");
                if (xelements.IsFull())
                    this.GuardarAvaluoAntecedentesReporte(xelements.First<XElement>(), ref dseAvaluo);
                return dseAvaluo;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
        }

        public DseAvaluoMantInf GuardarAvaluoInformeComercial(Decimal idAvaluo)
        {
            DseAvaluoMantInf dseAvaluoMantInf = new DseAvaluoMantInf();
            List<Decimal> numList = new List<Decimal>();
            bool esComercial = false;
            try
            {
                XElement root = XDocument.Parse(this.GetXmlAvaluo(idAvaluo).InnerXml).Root;
                foreach (DataColumn column in (InternalDataCollectionBase)dseAvaluoMantInf.FEXAVA_AVALUO.Columns)
                    column.AllowDBNull = true;
                dseAvaluoMantInf.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(dseAvaluoMantInf.FEXAVA_AVALUO.NewFEXAVA_AVALUORow());
                dseAvaluoMantInf.FEXAVA_AVALUO[0].CODESTADOAVALUO = EstadosAvaluoEnum.Recibido.ToDecimal();
                dseAvaluoMantInf.FEXAVA_AVALUO[0].FECHA_PRESENTACION = DateTime.Now.Date;
                if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                {
                    esComercial = true;
                    dseAvaluoMantInf.FEXAVA_AVALUO[0].CODTIPOTRAMITE = "1";
                }
                else if (root.Descendants((XName)"Catastral").Count<XElement>() > 0)
                {
                    esComercial = false;
                    dseAvaluoMantInf.FEXAVA_AVALUO[0].CODTIPOTRAMITE = "2";
                }
                //IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "g.1");
                //if (xelements1.IsFull()) {
                    //dseAvaluoMantInf.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = xelements1.ToStringXElement();

                IEnumerable<XElement> xelements1_1 = XmlUtils.XmlSearchById(root, "g.1.1");
                if (xelements1_1.IsFull()) { }
                dseAvaluoMantInf.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = xelements1_1.ToStringXElement();

                //TODO Asignar valores
                if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                {
                    IEnumerable<XElement> xelements1_2 = XmlUtils.XmlSearchById(root, "g.1.2");
                if (xelements1_2.IsFull()) { }
                    //dseAvaluoMantInf.FEXAVA_AVALUO[0]. = xelements1_2.ToStringXElement();

                    IEnumerable<XElement> xelements1_3 = XmlUtils.XmlSearchById(root, "g.1.3");
                if (xelements1_3.IsFull()) { }
                    //dseAvaluoMantInf.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = xelements1_3.ToStringXElement();

                    IEnumerable<XElement> xelements1_4 = XmlUtils.XmlSearchById(root, "g.1.4");
                if (xelements1_4.IsFull()) { }
                    //dseAvaluoMantInf.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = xelements1_4.ToStringXElement();

                    IEnumerable<XElement> xelements1_5 = XmlUtils.XmlSearchById(root, "g.1.5");
                if (xelements1_5.IsFull()) { }
                    //dseAvaluoMantInf.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = xelements1_5.ToStringXElement();

                }

                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "n.1");
                if (xelements2.IsFull())
                    dseAvaluoMantInf.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIASCONCLUSION = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "a");
                if (xelements3.IsFull())
                    this.GuardarAvaluoIdentificacion(xelements3.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "b");
                if (xelements4.IsFull())
                    this.GuardarAvaluoAntecedentes(xelements4.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "c");
                if (xelements5.IsFull())
                    this.GuardarAvaluoCaracteristicasUrbanas(xelements5.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "d");
                if (xelements6.IsFull())
                    this.GuardarAvaluoTerreno(xelements6.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "e");
                if (xelements7.IsFull())
                    this.GuardarAvaluoDescripcionImuebleAI(xelements7.First<XElement>(), ref dseAvaluoMantInf, esComercial);
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f");
                if (xelements8.IsFull())
                    this.GuardarAvaluoElementosConstruccion(xelements8.First<XElement>(), ref dseAvaluoMantInf);
                if (esComercial)
                {
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h");
                    if (xelements9.IsFull())
                        this.GuardarAvaluoEnfoqueMercado(xelements9.First<XElement>(), ref dseAvaluoMantInf);
                }
                if (esComercial)
                {
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "i");
                    if (xelements9.IsFull())
                        this.GuardarAvaluoEnfoqueCostosComercial(xelements9.First<XElement>(), ref dseAvaluoMantInf);
                }
                if (!esComercial)
                {
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "j");
                    if (xelements9.IsFull())
                        this.GuardarAvaluoEnfoqueCostosCatastral(xelements9.First<XElement>(), ref dseAvaluoMantInf);
                }
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "k");
                if (xelements10.IsFull())
                    this.GuardarAvaluoEnfoqueIngresos(xelements10.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "o");
                if (xelements11.IsFull())
                    this.GuardarAvaluoResumenConclusionAvaluo(xelements11.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "p");
                if (xelements12.IsFull())
                    this.GuardarAvaluoValorReferido(xelements12.First<XElement>(), ref dseAvaluoMantInf);
                IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "q");
                if (xelements13.IsFull())
                    this.GuardarAvaluoAnexoFotografico(xelements13.First<XElement>(), ref dseAvaluoMantInf);
                this.ObtenerInsertarDescripciones(ref dseAvaluoMantInf);
                return dseAvaluoMantInf;
            }
            catch (FaultException<AvaluosInfoException> ex)
            {
                ExceptionPolicyWrapper.HandleException((Exception)ex);
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(ex.Detail.Descripcion));
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        private void GuardarAvaluoIdentificacion(
          XElement identificacion,
          ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(identificacion, "a.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].NUMEROAVALUO = xelements1.ToStringXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(identificacion, "a.2");
            if (xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO = xelements2.First<XElement>().Value.To<DateTime>().ToShortDateString();
            string registroPerito = string.Empty;
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(identificacion, "a.3");
            if (xelements3.IsFull())
            {
                registroPerito = xelements3.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONAPERITO = this.IdPeritoSociedadByRegistro(registroPerito, string.Empty, true);
                Decimal idpersonaperito = dseAvaluo.FEXAVA_AVALUO[0].IDPERSONAPERITO;
                dseAvaluo.FEXAVA_AVALUO[0].REGTDF_PERITO = xelements3.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].NOMBRE_PERITO = this.ObtenerNombrePersona(idpersonaperito);
            }
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(identificacion, "a.4");
            if (xelements4.IsFull())
            {
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONASOCIEDAD = this.IdPeritoSociedadByRegistro(registroPerito, xelements4.ToStringXElement(), false);
                Decimal idpersonasociedad = dseAvaluo.FEXAVA_AVALUO[0].IDPERSONASOCIEDAD;
                dseAvaluo.FEXAVA_AVALUO[0].REGTDF_SOCIEDAD = xelements4.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].NOMBRE_SOCI = this.ObtenerNombrePersona(idpersonasociedad);
            }
            string tipo = string.Empty;
            if (dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE.Equals("2"))
                tipo = "CAT";
            else if (dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE.Equals("1"))
                tipo = "COM";
            dseAvaluo.FEXAVA_AVALUO[0].NUMEROUNICO = AvaluosUtils.ObtenerNumUnicoAv(tipo);
        }

        private void GuardarAvaluoAntecedentes(XElement antecedentes, ref DseAvaluoMantInf dseAvaluo)
        {
            try
            {
                DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow row1 = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
                DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow row2 = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row2.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(antecedentes, "b.1.1");
                if (xelements1.IsFull())
                    row1.APELLIDOPATERNO = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(antecedentes, "b.1.2");
                if (xelements2.IsFull())
                    row1.APELLIDOMATERNO = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(antecedentes, "b.1.3");
                if (xelements3.IsFull())
                    row1.NOMBRE = xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(antecedentes, "b.1.4");
                if (xelements4.IsFull())
                    row1.CALLE = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(antecedentes, "b.1.5");
                if (xelements5.IsFull())
                    row1.NUMEROINTERIOR = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(antecedentes, "b.1.6");
                if (xelements6.IsFull())
                    row1.NUMEROEXTERIOR = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(antecedentes, "b.1.8");
                if (xelements7.IsFull())
                    row1.CODIGOPOSTAL = xelements7.ToStringXElement();

                try
                {
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(antecedentes, "b.1.9.1");
                    if (xelements8.IsFull())
                    {
                        string stringXelement = xelements8.ToStringXElement();
                        if (!stringXelement.Equals("018"))//Se agrega al catálogo el elemento 018 Otros (Municipios fuera de CDMX)
                        {
                            row1.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                            row1["DescDeleg"] = (object)xelements8.ToStringXElement();
                            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                            if (xelements9.IsFull())
                            {
                                row1.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                                row1["DescColonia"] = (object)xelements9.ToStringXElement();
                            }
                        }
                        else//Municipios fuera de CDMX
                        {
                            row1.IDDELEGACION = xelements8.ToDecimalXElement();
                            //Se guarda el valor que contenga Otros
                            IEnumerable<XElement> municipio = XmlUtils.XmlSearchById(antecedentes, "b.1.9.2");
                            if (municipio.IsFull())
                            {
                                row1["DescDeleg"] = (object)municipio.ToStringXElement();
                            }
                            //Se guaraa como Colonia lo que tenga el XML 
                            IEnumerable<XElement> colonia = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                            if (colonia.IsFull())
                            {
                                row1["DescColonia"] = (object)colonia.ToStringXElement();
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(antecedentes, "b.1.9");
                if (xelements8.IsFull())
                {
                    string stringXelement = xelements8.ToStringXElement();
                    row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                    row2["DescDeleg"] = (object)xelements8.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                    if (xelements9.IsFull())
                    {
                        row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                        row2["DescColonia"] = (object)xelements9.ToStringXElement();
                    }
                }
                }


                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(antecedentes, "b.1.10");
                if (xelements10.IsFull())
                    row1.TipoPersona = xelements10.ToStringXElement();
                row1.ROL = "S";
                IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(antecedentes, "b.2.1");
                if (xelements11.IsFull())
                    row2.APELLIDOPATERNO = xelements11.ToStringXElement();
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(antecedentes, "b.2.2");
                if (xelements12.IsFull())
                    row2.APELLIDOMATERNO = xelements12.ToStringXElement();
                IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(antecedentes, "b.2.3");
                if (xelements13.IsFull())
                    row2.NOMBRE = xelements13.ToStringXElement();
                IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(antecedentes, "b.2.4");
                if (xelements14.IsFull())
                    row2.CALLE = xelements14.ToStringXElement();
                IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(antecedentes, "b.2.5");
                if (xelements15.IsFull())
                    row2.NUMEROINTERIOR = xelements15.ToStringXElement();
                IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(antecedentes, "b.2.6");
                if (xelements16.IsFull())
                    row2.NUMEROEXTERIOR = xelements16.ToStringXElement();
                IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(antecedentes, "b.2.8");
                if (xelements17.IsFull())
                    row2.CODIGOPOSTAL = xelements17.ToStringXElement();


                /*
                IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9");
                if (xelements18.IsFull())
                {
                    string stringXelement = xelements18.ToStringXElement();
                    row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                    row2["DescDeleg"] = (object)xelements18.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                    if (xelements9.IsFull())
                    {
                        row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                        row2["DescColonia"] = (object)xelements9.ToStringXElement();
                    }
                }*/

                try
                {
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9.1");
                    if (xelements18.IsFull())
                    {
                        string stringXelement = xelements18.ToStringXElement();
                        if (!stringXelement.Equals("018"))//Se agrega al catálogo el elemento 018 Otros (Municipios fuera de CDMX)
                        {
                            row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                            row2["DescDeleg"] = (object)xelements18.ToStringXElement();
                            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                            if (xelements9.IsFull())
                            {
                                row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                                row2["DescColonia"] = (object)xelements9.ToStringXElement();
                            }
                        }
                        else//Municipios fuera de CDMX
                        {
                            row2.IDDELEGACION = xelements18.ToDecimalXElement();
                            //Se guarda el valor que contenga Otros
                            IEnumerable<XElement> municipio = XmlUtils.XmlSearchById(antecedentes, "b.2.9.2");
                            if (municipio.IsFull())
                            {
                                row2["DescDeleg"] = (object)municipio.ToStringXElement();
                            }
                            //Se guaraa como Colonia lo que tenga el XML 
                            IEnumerable<XElement> colonia = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                            if (colonia.IsFull())
                            {
                                row2["DescColonia"] = (object)colonia.ToStringXElement();
                            }
                        }
                    }
                }catch(Exception ex)
                {
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9");
                    if (xelements18.IsFull())
                    {
                        string stringXelement = xelements18.ToStringXElement();
                        row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        row2["DescDeleg"] = (object)xelements18.ToStringXElement();
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                        if (xelements9.IsFull())
                        {
                            row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                            row2["DescColonia"] = (object)xelements9.ToStringXElement();
                        }
                    }
                }




                IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(antecedentes, "b.2.10");
                if (xelements19.IsFull())
                    row2.TipoPersona = xelements19.ToStringXElement();
                row2.ROL = "P";
                IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(antecedentes, "b.3.1");
                if (xelements20.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CALLE = xelements20.ToStringXElement();
                IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(antecedentes, "b.3.2");
                if (xelements21.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].INT = xelements21.ToStringXElement();
                IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(antecedentes, "b.3.3");
                if (xelements22.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].EXT = xelements22.ToStringXElement();
                IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(antecedentes, "b.3.5");
                if (xelements23.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].LOTE = xelements23.ToStringXElement();
                IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(antecedentes, "b.3.6");
                if (xelements24.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].EDIFICIO = xelements24.ToStringXElement();
                IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(antecedentes, "b.3.8");
                if (xelements25.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CP = xelements25.ToStringXElement();
                IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(antecedentes, "b.3.9");
                if (xelements26.IsFull())
                {
                    xelements26.ToStringXElement();
                    dseAvaluo.FEXAVA_AVALUO[0].DELEGACION = xelements26.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.3.7");
                    if (xelements9.IsFull())
                        dseAvaluo.FEXAVA_AVALUO[0].COLONIA = xelements9.ToStringXElement();
                }
                IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.1");
                if (xelements27.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].REGION = xelements27.ToStringXElement();
                IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.2");
                if (xelements28.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].MANZANA = xelements28.ToStringXElement();
                IEnumerable<XElement> xelements29 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.3");
                if (xelements29.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].LOTE_CC = xelements29.ToStringXElement();
                IEnumerable<XElement> xelements30 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.4");
                if (xelements30.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].UNIDADPRIVATIVA = xelements30.ToStringXElement();
                IEnumerable<XElement> xelements31 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.5");
                if (xelements31.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].DIGITOVERIFICADOR = xelements31.ToStringXElement();
                IEnumerable<XElement> xelements32 = XmlUtils.XmlSearchById(antecedentes, "b.3.11");
                if (xelements32.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CUENTAAGUA = xelements32.ToStringXElement();
                
                IEnumerable<XElement> xelements33 = XmlUtils.XmlSearchById(antecedentes, "b.4");
                //if (xelements33.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = xelements33.ToStringXElement();
                if (xelements33.IsFull())
                {
                    try
                     {
                    switch (XmlUtils.XmlSearchById(xelements33, "b.4.1").ToStringXElement().ToInt())
                    {
                        case 1:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P1;
                            break;
                        case 2:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P2;
                            break;
                        case 3:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P3;
                            break;
                        case 4:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = XmlUtils.XmlSearchById(xelements33, "b.4.2").ToStringXElement().ToUpper();
                            break;
                    }
                    }catch(Exception ex) { }
                       dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = xelements33.ToStringXElement();
                }

                IEnumerable<XElement> xelements34 = XmlUtils.XmlSearchById(antecedentes, "b.5");
                if (xelements34.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].OBJETO = xelements34.ToStringXElement();
                IEnumerable<XElement> xelements35 = XmlUtils.XmlSearchById(antecedentes, "b.6");
                if (xelements35.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD = XmlUtils.ToDecimalXElementAv(xelements35);
                dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(row1);
                dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(row2);


                IEnumerable<XElement> xelements36 = XmlUtils.XmlSearchById(antecedentes, "b.7");
                if (xelements36.IsFull())
                {
                    try
                    {
                        switch (XmlUtils.XmlSearchById(xelements36, "b.7.1").ToStringXElement().ToInt())
                        {
                            case 1:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP1;
                                break;
                            case 2:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP2;
                                break;
                            case 3:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP3;
                                break;
                            case 4:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP4;
                                break;
                            case 5:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP5;
                                break;
                            case 6:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP6;
                                break;
                            case 7:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP7;
                                break;
                            case 8:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP8;
                                break;
                            case 9:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP9;
                                break;
                            case 10:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP10;
                                break;
                            case 11:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP11;
                                break;
                            case 12:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP12;
                                break;
                            case 13:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP13;
                                break;
                            case 14:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP14;
                                break;
                            case 15:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP15;
                                break;
                            case 16:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP16;
                                break;
                            case 17:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP17;
                                break;
                            case 18:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP18;
                                break;
                            case 19:
                                dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = XmlUtils.XmlSearchById(xelements36, "b.7.2").ToStringXElement().ToUpper();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = xelements36.ToStringXElement();
                    }
                }

            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
        }



        private void GuardarAvaluoAntecedentesReporte(XElement antecedentes, ref DseAvaluoMantInf dseAvaluo)
        {
            try
            {
                dseAvaluo.FEXAVA_DATOSPERSONAS.IDCOLONIAColumn.AllowDBNull=true;
                dseAvaluo.FEXAVA_DATOSPERSONAS.IDDELEGACIONColumn.AllowDBNull = true;
                DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow row1 = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
                DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow row2 = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row2.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(antecedentes, "b.1.1");
                if (xelements1.IsFull())
                    row1.APELLIDOPATERNO = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(antecedentes, "b.1.2");
                if (xelements2.IsFull())
                    row1.APELLIDOMATERNO = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(antecedentes, "b.1.3");
                if (xelements3.IsFull())
                    row1.NOMBRE = xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(antecedentes, "b.1.4");
                if (xelements4.IsFull())
                    row1.CALLE = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(antecedentes, "b.1.5");
                if (xelements5.IsFull())
                    row1.NUMEROINTERIOR = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(antecedentes, "b.1.6");
                if (xelements6.IsFull())
                    row1.NUMEROEXTERIOR = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(antecedentes, "b.1.8");
                if (xelements7.IsFull())
                    row1.CODIGOPOSTAL = xelements7.ToStringXElement();


                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(antecedentes, "b.1.9.1");
                if (xelements8.IsFull())
                {
                    try
                    {
                        log("GuardarAvaluoAntecedentesReporte", "b191", "ClaveAlcaldia: " + xelements8.ToStringXElement() );
                        string stringXelement = xelements8.ToStringXElement();
                        if (!stringXelement.Equals("018"))//Se agrega al catálogo el elemento 018 Otros (Municipios fuera de CDMX)
                        {
                            row1.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                            row1["DescDeleg"] = (object)xelements8.ToStringXElement();
                            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                            if (xelements9.IsFull())
                            {
                                log("GuardarAvaluoAntecedentesReporte","b191", 
                                     "ClaveAlcaldia: "+xelements8.ToStringXElement()
                                    +" | colonia(b17): "+xelements9.ToStringXElement());
                                log("GuardarAvaluoAntecedentesReporte", "IDCOLONIA 1",
                                     "ClaveAlcaldia: " + xelements8.ToStringXElement()
                                    + " | colonia(b17): " + xelements9.ToStringXElement()
                                    + " | stringXelement: " + stringXelement
                                    //+ " | IDCOLONIA : "+ 
                                    //CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement)
                                    );
                                row1.IDCOLONIA = -1M;
                                    //CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                                row1["DescColonia"] = (object)xelements9.ToStringXElement();
                            }
                        }
                        else//Municipios fuera de CDMX
                        {
                            row1.IDDELEGACION = xelements8.ToDecimalXElement();
                            //Se guarda el valor que contenga Otros
                            IEnumerable<XElement> municipio = XmlUtils.XmlSearchById(antecedentes, "b.1.9.2");
                            if (municipio.IsFull())
                            {
                                row1["DescDeleg"] = (object)municipio.ToStringXElement();
                            }
                            //Se guaraa como Colonia lo que tenga el XML 
                            IEnumerable<XElement> colonia = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                            if (colonia.IsFull())
                            {
                                log("GuardarAvaluoAntecedentesReporte", "b191 fuera de CDMX",
                                     "ClaveAlcaldia: " + xelements8.ToStringXElement()
                                    + " | idColonia = 0 | colonia(b17): " + colonia.ToStringXElement());
                                row1.IDCOLONIA = 0M;
                                row1["DescColonia"] = (object)colonia.ToStringXElement();
                            }
                        }
                    }catch(Exception ex)
                    {
                        log("GuardarAvaluoAntecedentesReporte b191 CatchException",ex.Message,ex.StackTrace);
                        string stringXelement = xelements8.ToStringXElement();
                        row1.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        row1["DescDeleg"] = (object)xelements8.ToStringXElement();
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                        if (xelements9.IsFull())
                        {
                            row1.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                            row1["DescColonia"] = (object)xelements9.ToStringXElement();
                            log("GuardarAvaluoAntecedentesReporte", "b191 CatchException",
                                     "Alcaldia: " + xelements8.ToStringXElement()
                                    + "IDalcaldia: " + row1.IDDELEGACION.ToString()
                                    + " | idColonia = "+ row1.IDCOLONIA.ToString() 
                                    + " | colonia(b17): " + xelements9.ToStringXElement());
                        }
                    }
                   
                }
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(antecedentes, "b.1.10");
                if (xelements10.IsFull())
                    row1.TipoPersona = xelements10.ToStringXElement();
                row1.ROL = "S";
                IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(antecedentes, "b.2.1");
                if (xelements11.IsFull())
                    row2.APELLIDOPATERNO = xelements11.ToStringXElement();
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(antecedentes, "b.2.2");
                if (xelements12.IsFull())
                    row2.APELLIDOMATERNO = xelements12.ToStringXElement();
                IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(antecedentes, "b.2.3");
                if (xelements13.IsFull())
                    row2.NOMBRE = xelements13.ToStringXElement();
                IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(antecedentes, "b.2.4");
                if (xelements14.IsFull())
                    row2.CALLE = xelements14.ToStringXElement();
                IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(antecedentes, "b.2.5");
                if (xelements15.IsFull())
                    row2.NUMEROINTERIOR = xelements15.ToStringXElement();
                IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(antecedentes, "b.2.6");
                if (xelements16.IsFull())
                    row2.NUMEROEXTERIOR = xelements16.ToStringXElement();
                IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(antecedentes, "b.2.8");
                if (xelements17.IsFull())
                    row2.CODIGOPOSTAL = xelements17.ToStringXElement();

                IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(antecedentes, "b.2.9.1");
                if (xelements18.IsFull())
                {
                    try
                    {
                        string stringXelement = xelements18.ToStringXElement();
                        if (!stringXelement.Equals("018"))//Se agrega al catálogo el elemento 018 Otros (Municipios fuera de CDMX)
                        {
                            row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                            row2["DescDeleg"] = (object)xelements18.ToStringXElement();
                            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                            if (xelements9.IsFull())
                            {
                                log("GuardarAvaluoAntecedentesReporte", "IDCOLONIA 2",
                                     "ClaveAlcaldia: " + xelements8.ToStringXElement()
                                    + " | colonia(b27): " + xelements9.ToStringXElement()
                                    + " | stringXelement: " + stringXelement
                                    //+ " | IDCOLONIA : " + 
                                    //CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement)
                                    );
                                row2.IDCOLONIA = -1M;
                                    //CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                                row2["DescColonia"] = (object)xelements9.ToStringXElement();
                            }
                        }
                        else//Municipios fuera de CDMX
                        {
                            row2.IDDELEGACION = xelements8.ToDecimalXElement();
                            //Se guarda el valor que contenga Otros
                            IEnumerable<XElement> municipio = XmlUtils.XmlSearchById(antecedentes, "b.2.9.2");
                            if (municipio.IsFull())
                            {
                                row2["DescDeleg"] = (object)municipio.ToStringXElement();
                            }
                            //Se guaraa como Colonia lo que tenga el XML 
                            IEnumerable<XElement> colonia = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                            if (colonia.IsFull())
                            {
                                row2.IDCOLONIA = 0M;
                                row2["DescColonia"] = (object)colonia.ToStringXElement();
                            }
                        }
                    }catch(Exception ex)
                    {
                        string stringXelement = xelements18.ToStringXElement();
                        row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        row2["DescDeleg"] = (object)xelements18.ToStringXElement();
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                        if (xelements9.IsFull())
                        {
                            log("GuardarAvaluoAntecedentesReporte", "IDCOLONIA 2 Exception",
                                     "ClaveAlcaldia: " + xelements8.ToStringXElement()
                                    + " | colonia(b27): " + xelements9.ToStringXElement()
                                    + " | stringXelement: " + stringXelement
                                    + " | IDCOLONIA : " + CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement));
                            row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                            row2["DescColonia"] = (object)xelements9.ToStringXElement();
                        }
                    }
                    finally
                    {
                        row2.IDDELEGACION = 18M;
                        row2["DescDeleg"] = (object)xelements8.ToStringXElement();
                        row2.IDCOLONIA = 0M;
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                        if (xelements9.IsFull())
                        {
                            row2["DescColonia"] = (object)xelements9.ToStringXElement();
                        }
                    }
                }


                IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(antecedentes, "b.2.10");
                if (xelements19.IsFull())
                    row2.TipoPersona = xelements19.ToStringXElement();
                row2.ROL = "P";
                IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(antecedentes, "b.3.1");
                if (xelements20.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CALLE = xelements20.ToStringXElement();
                IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(antecedentes, "b.3.2");
                if (xelements21.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].INT = xelements21.ToStringXElement();
                IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(antecedentes, "b.3.3");
                if (xelements22.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].EXT = xelements22.ToStringXElement();
                IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(antecedentes, "b.3.5");
                if (xelements23.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].LOTE = xelements23.ToStringXElement();
                IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(antecedentes, "b.3.6");
                if (xelements24.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].EDIFICIO = xelements24.ToStringXElement();
                IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(antecedentes, "b.3.8");
                if (xelements25.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CP = xelements25.ToStringXElement();
                IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(antecedentes, "b.3.9");
                if (xelements26.IsFull())
                {
                    xelements26.ToStringXElement();
                    dseAvaluo.FEXAVA_AVALUO[0].DELEGACION = xelements26.ToStringXElement();
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(antecedentes, "b.3.7");
                    if (xelements9.IsFull())
                        dseAvaluo.FEXAVA_AVALUO[0].COLONIA = xelements9.ToStringXElement();
                }
                IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.1");
                if (xelements27.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].REGION = xelements27.ToStringXElement();
                IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.2");
                if (xelements28.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].MANZANA = xelements28.ToStringXElement();
                IEnumerable<XElement> xelements29 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.3");
                if (xelements29.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].LOTE_CC = xelements29.ToStringXElement();
                IEnumerable<XElement> xelements30 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.4");
                if (xelements30.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].UNIDADPRIVATIVA = xelements30.ToStringXElement();
                IEnumerable<XElement> xelements31 = XmlUtils.XmlSearchById(antecedentes, "b.3.10.5");
                if (xelements31.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].DIGITOVERIFICADOR = xelements31.ToStringXElement();
                IEnumerable<XElement> xelements32 = XmlUtils.XmlSearchById(antecedentes, "b.3.11");
                if (xelements32.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CUENTAAGUA = xelements32.ToStringXElement();

                IEnumerable<XElement> xelements33 = XmlUtils.XmlSearchById(antecedentes, "b.4");
                if (xelements33.IsFull())
                {
                    try
                     {
                    switch (XmlUtils.XmlSearchById(xelements33, "b.4.1").ToStringXElement().ToInt())
                    {
                        case 1:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P1;
                            break;
                        case 2:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P2;
                            break;
                        case 3:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = Constantes.P3;
                            break;
                        case 4:
                            dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = XmlUtils.XmlSearchById(xelements33, "b.4.2").ToStringXElement().ToUpper();
                            break;
                    }
                    }catch(Exception ex) { dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = xelements33.ToStringXElement(); }
                       
                }

                IEnumerable<XElement> xelements34 = XmlUtils.XmlSearchById(antecedentes, "b.5");
                if (xelements34.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].OBJETO = xelements34.ToStringXElement();
                IEnumerable<XElement> xelements35 = XmlUtils.XmlSearchById(antecedentes, "b.6");
                if (xelements35.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD = XmlUtils.ToDecimalXElementAv(xelements35);
                dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(row1);
                dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(row2);


                IEnumerable<XElement> xelements36 = XmlUtils.XmlSearchById(antecedentes, "b.7");
                if (xelements36.IsFull())
                {
                    try { 
                    switch (XmlUtils.XmlSearchById(xelements36, "b.7.1").ToStringXElement().ToInt())
                    {
                        case 1:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP1;
                            break;
                        case 2:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP2;
                            break;
                        case 3:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP3;
                            break;
                        case 4:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP4;
                            break;
                        case 5:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP5;
                            break;
                        case 6:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP6;
                            break;
                        case 7:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP7;
                            break;
                        case 8:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP8;
                            break;
                        case 9:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP9;
                            break;
                        case 10:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP10;
                            break;
                        case 11:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP11;
                            break;
                        case 12:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP12;
                            break;
                        case 13:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP13;
                            break;
                        case 14:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP14;
                            break;
                        case 15:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP15;
                            break;
                        case 16:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP16;
                            break;
                        case 17:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP17;
                            break;
                        case 18:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = Constantes.TP18;
                            break;
                        case 19:
                            dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = XmlUtils.XmlSearchById(xelements36, "b.7.2").ToStringXElement().ToUpper();
                            break;
                    }
                    }catch(Exception ex) { dseAvaluo.FEXAVA_AVALUO[0].TIPOCONDOMINIO = xelements36.ToStringXElement(); }
                }

            }
            catch (Exception ex)
            {
                log("GuardarAvaluoAntecedentesReporte", ex.Message, ex.StackTrace);
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
        }



        private void GuardarAvaluoCaracteristicasUrbanas(
          XElement caracteristicasUrbanas,
          ref DseAvaluoMantInf dseAvaluo)
        {
            string empty1 = string.Empty;
            string empty2 = string.Empty;
            DseAvaluosCatConsulta catalogosConsulta = ApplicationCache.DseCatalogosConsulta;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.0");
            if (xelements1.IsFull())
            {
                string stringXelement = xelements1.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].CUCONTAMINACIONAMBIENTALZONA = stringXelement.Trim();
            }
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.1");
            if (xelements2.IsFull())
            {
                Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements2);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASIFICACIONZONA = decimalXelementAv;
            }
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.2");
            if (xelements3.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUINDICESATURACIONZONA = XmlUtils.ToDecimalXElementAv(xelements3) * 100M;
            DateTime dateTime = Convert.ToDateTime(dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO);
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.3");
            if (xelements4.IsFull())
            {
                string stringXelement = xelements4.ToStringXElement();
                int num = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(dateTime, stringXelement);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASESCONSTRUCCION = (Decimal)num;
            }
            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.4");
            if (xelements5.IsFull())
            {
                Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements5);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDENSIDADPOBLACION = decimalXelementAv;
            }
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.5");
            if (xelements6.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODNIVELSOCIOECONOMICO = XmlUtils.ToDecimalXElementAv(xelements6);
            IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.1");
            if (xelements7.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUUSO = xelements7.ToStringXElement();
            IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.2");
            if (xelements8.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUAREALIBREOBLIGATORIO = xelements8.ToStringXElement();
            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.3");
            if (xelements9.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUNUMMAXNIVELESACONSTRUIR = XmlUtils.ToDecimalXElementAv(xelements9);
            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.4");
            if (xelements10.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCOEFICIENTE = XmlUtils.ToDecimalXElementAv(xelements10);
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.7");
            if (xelements11.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VIASDEACCESO = xelements11.ToStringXElement();
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.1");
            if (xelements12.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLE = XmlUtils.ToDecimalXElementAv(xelements12);
            IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.2");
            if (xelements13.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLERESIDUAL = XmlUtils.ToDecimalXElementAv(xelements13);
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.3");
            if (xelements14.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALCALLE = XmlUtils.ToDecimalXElementAv(xelements14);
            IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.4");
            if (xelements15.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALZONA = XmlUtils.ToDecimalXElementAv(xelements15);
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.5");
            if (xelements16.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEINMUEBLE = XmlUtils.ToDecimalXElementAv(xelements16);
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.7");
            if (xelements17.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROELECTRICO = XmlUtils.ToDecimalXElementAv(xelements17);
            IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.8");
            if (xelements18.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLE = XmlUtils.ToDecimalXElementAv(xelements18);
            IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.9");
            if (xelements19.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODALUMBRADOPUBLICO = XmlUtils.ToDecimalXElementAv(xelements19);
            IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.10");
            if (xelements20.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODVIALIDADES = XmlUtils.ToDecimalXElementAv(xelements20);
            IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.11");
            if (xelements21.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODBANQUETAS = XmlUtils.ToDecimalXElementAv(xelements21);
            IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.12");
            if (xelements22.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODGUARNICIONES = XmlUtils.ToDecimalXElementAv(xelements22);
            IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.13");
            if (xelements23.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUPORCENTAJEINFRAESTRUCTURA = XmlUtils.ToDecimalXElementAv(xelements23) * 100M;
            IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.14");
            if (xelements24.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODGASNATURAL = XmlUtils.ToDecimalXElementAv(xelements24);
            IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.15");
            if (xelements25.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROTELEFONICA = XmlUtils.ToDecimalXElementAv(xelements25);
            IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.16");
            if (xelements26.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLETEL = XmlUtils.ToDecimalXElementAv(xelements26);
            IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.17");
            if (xelements27.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSENALIZACIONVIAS = xelements27.ToStringXElement();
            IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.18");
            if (xelements28.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODNOMENCLATURACALLE = xelements28.ToStringXElement();
            IEnumerable<XElement> xelements29 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.19");
            if (xelements29.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUDISTANCIATRANSPORTEURBANO = XmlUtils.ToDecimalXElementAv(xelements29);
            IEnumerable<XElement> xelements30 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.20");
            if (xelements30.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUFRECUENCIATRANSPORTEURBANO = XmlUtils.ToDecimalXElementAv(xelements30);
            IEnumerable<XElement> xelements31 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.21");
            if (xelements31.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUDISTANCIATRANSPORTESUBURB = XmlUtils.ToDecimalXElementAv(xelements31);
            IEnumerable<XElement> xelements32 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.22");
            if (xelements32.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUFRECUENCIATRANSPORTESUBURB = XmlUtils.ToDecimalXElementAv(xelements32);
            IEnumerable<XElement> xelements33 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.23");
            if (xelements33.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODVIGILANCIAZONA = XmlUtils.ToDecimalXElementAv(xelements33);
            IEnumerable<XElement> xelements34 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.24");
            if (xelements34.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODRECOLECCIONBASURA = XmlUtils.ToDecimalXElementAv(xelements34);
            IEnumerable<XElement> xelements35 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.25");
            if (xelements35.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEIGLESIA = this.BooleanXMLtoOracle(xelements35.First<XElement>().Value);
            IEnumerable<XElement> xelements36 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.26");
            if (xelements36.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEMERCADOS = this.BooleanXMLtoOracle(xelements36.First<XElement>().Value);
            IEnumerable<XElement> xelements37 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.27");
            if (xelements37.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEPLAZASPUBLICOS = this.BooleanXMLtoOracle(xelements37.First<XElement>().Value);
            IEnumerable<XElement> xelements38 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.28");
            if (xelements38.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEPARQUESJARDINES = this.BooleanXMLtoOracle(xelements38.First<XElement>().Value);
            IEnumerable<XElement> xelements39 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.29");
            if (xelements39.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEESCUELAS = this.BooleanXMLtoOracle(xelements39.First<XElement>().Value);
            IEnumerable<XElement> xelements40 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.30");
            if (xelements40.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEHOSPITALES = this.BooleanXMLtoOracle(xelements40.First<XElement>().Value);
            IEnumerable<XElement> xelements41 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.31");
            if (xelements41.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEBANCOS = this.BooleanXMLtoOracle(xelements41.First<XElement>().Value);
            IEnumerable<XElement> xelements42 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.32");
            if (xelements42.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEESTACIONTRANSPORTE = this.BooleanXMLtoOracle(xelements42.First<XElement>().Value);
            IEnumerable<XElement> xelements43 = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.33");
            if (!xelements43.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].NIVELEQUIPAMIENTOURBANO = XmlUtils.ToDecimalXElementAv(xelements43);
        }

        private void GuardarAvaluoTerreno(XElement terreno, ref DseAvaluoMantInf dseAvaluo)
        {
            List<Decimal> numList = new List<Decimal>();
            DseAvaluoMantInf.FEXAVA_FUENTEINFORMACIONLEGRow row1 = (DseAvaluoMantInf.FEXAVA_FUENTEINFORMACIONLEGRow)null;
            DseAvaluoMantInf.FEXAVA_ESCRITURARow row2 = (DseAvaluoMantInf.FEXAVA_ESCRITURARow)null;
            DseAvaluoMantInf.FEXAVA_SENTENCIARow row3 = (DseAvaluoMantInf.FEXAVA_SENTENCIARow)null;
            DseAvaluoMantInf.FEXAVA_CONTRATOPRIVADORow row4 = (DseAvaluoMantInf.FEXAVA_CONTRATOPRIVADORow)null;
            DseAvaluoMantInf.FEXAVA_ALINEAMIENTONUMOFIRow row5 = (DseAvaluoMantInf.FEXAVA_ALINEAMIENTONUMOFIRow)null;
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(terreno, "d.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DTRAMOCALLES = xelements1.ToStringXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(terreno, "d.2");
            if (xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DMICRO = xelements2.ToStringXElement();
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(terreno, "d.3");
            if (xelements3.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DMACRO = xelements3.ToStringXElement();
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(terreno, "d.4.1.1");
            if (xelements4.IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("1");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.3");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>().Date;
                row2 = dseAvaluo.FEXAVA_ESCRITURA.NewFEXAVA_ESCRITURARow();
                row2.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.1");
                if (xelements6.IsFull())
                    row2.NUMESCRITURA = XmlUtils.ToDecimalXElementAv(xelements6);
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.2");
                if (xelements7.IsFull())
                    row2.NUMVOLUMEN = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.4");
                if (xelements8.IsFull())
                    row2.NUMNOTARIO = XmlUtils.ToDecimalXElementAv(xelements8);
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.5");
                if (xelements9.IsFull())
                    row2.NOMBRENOTARIO = xelements9.ToStringXElement();
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(xelements4, "d.4.1.1.6");
                if (xelements10.IsFull())
                    row2.DISTRITOJUDICIALNOTARIO = xelements10.ToStringXElement();
            }
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(terreno, "d.4.1.2");
            if (xelements11.IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("2");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(xelements11, "d.4.1.2.2");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>().Date;
                row3 = dseAvaluo.FEXAVA_SENTENCIA.NewFEXAVA_SENTENCIARow();
                row3.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(xelements11, "d.4.1.2.1");
                if (xelements6.IsFull())
                    row3.JUZGADO = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(xelements11, "d.4.1.2.3");
                if (xelements7.IsFull())
                    row3.NUMEXPEDIENTE = xelements7.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(terreno, "d.4.1.3").IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("3");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.1");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>().Date;
                row4 = dseAvaluo.FEXAVA_CONTRATOPRIVADO.NewFEXAVA_CONTRATOPRIVADORow();
                row4.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.2");
                if (xelements6.IsFull())
                    row4.NOMBREADQUIRIENTE = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.3");
                if (xelements7.IsFull())
                    row4.APELLIDOPATERNOADQUIRIENTE = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.4");
                if (xelements8.IsFull())
                    row4.APELLIDOMATERNOADQUIRIENTE = xelements8.ToStringXElement();
                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.5");
                if (xelements9.IsFull())
                    row4.NOMBREENAJENANTE = xelements9.ToStringXElement();
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.6");
                if (xelements10.IsFull())
                    row4.APELLIDOPATERNOENAJENANTE = xelements10.ToStringXElement();
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(terreno, "d.4.1.3.7");
                if (xelements12.IsFull())
                    row4.APELLIDOMATERNOENAJENANTE = xelements12.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(terreno, "d.4.1.4").IsFull())
            {
                row1 = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                row1.CODTIPOFUENTEINFORMACION = Convert.ToDecimal("4");
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(terreno, "d.4.1.4.1");
                if (xelements5.IsFull())
                    row1.FECHA = xelements5.First<XElement>().Value.To<DateTime>().Date;
                row5 = dseAvaluo.FEXAVA_ALINEAMIENTONUMOFI.NewFEXAVA_ALINEAMIENTONUMOFIRow();
                row5.FEXAVA_FUENTEINFORMACIONLEGRow = row1;
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(terreno, "d.4.1.4.2");
                if (xelements6.IsFull())
                    row5.NUMFOLIO = xelements6.ToStringXElement();
            }
            if (row1 != null)
            {
                dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.AddFEXAVA_FUENTEINFORMACIONLEGRow(row1);
                if (row2 != null)
                    dseAvaluo.FEXAVA_ESCRITURA.AddFEXAVA_ESCRITURARow(row2);
                else if (row3 != null)
                    dseAvaluo.FEXAVA_SENTENCIA.AddFEXAVA_SENTENCIARow(row3);
                else if (row4 != null)
                    dseAvaluo.FEXAVA_CONTRATOPRIVADO.AddFEXAVA_CONTRATOPRIVADORow(row4);
                else if (row5 != null)
                    dseAvaluo.FEXAVA_ALINEAMIENTONUMOFI.AddFEXAVA_ALINEAMIENTONUMOFIRow(row5);
            }
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(terreno, "d.4.2");
            if (elementos1.IsFull())
            {
                int num = 0;
                foreach (XElement root in elementos1)
                {
                    DseAvaluoMantInf.FEXAVA_COLINDANCIASRow row6 = dseAvaluo.FEXAVA_COLINDANCIAS.NewFEXAVA_COLINDANCIASRow();
                    row6.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "d.4.2.n.2");
                    if (xelements5.IsFull())
                        row6.ORIENTACION = xelements5.ToStringXElement();
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "d.4.2.n.3");
                    if (xelements6.IsFull())
                        row6.MEDIDA = XmlUtils.ToDecimalXElementAv(xelements6);
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "d.4.2.n.4");
                    if (xelements7.IsFull())
                        row6.DESCRIPCION = xelements7.ToStringXElement();
                    ++num;
                    row6.IDCOLINDANCIA = num;
                    dseAvaluo.FEXAVA_COLINDANCIAS.AddFEXAVA_COLINDANCIASRow(row6);
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(terreno, "d.5");
            if (elementos2.IsFull())
            {
                foreach (XElement root in elementos2)
                {
                    DseAvaluoMantInf.FEXAVA_SUPERFICIERow row6 = dseAvaluo.FEXAVA_SUPERFICIE.NewFEXAVA_SUPERFICIERow();
                    row6.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "d.5.n.1");
                    if (xelements5.IsFull())
                        row6.IDENTIFICADORFRACCION = XmlUtils.ToDecimalXElementAv(xelements5);
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "d.5.n.2");
                    if (xelements6.IsFull())
                        row6.SUPERFICIEFRACCION = XmlUtils.ToDecimalXElementAv(xelements6);
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "d.5.n.3.2");
                    if (xelements7.IsFull())
                        row6.FZO = XmlUtils.ToDecimalXElementAv(xelements7);
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "d.5.n.4.2");
                    if (xelements8.IsFull())
                        row6.FUB = XmlUtils.ToDecimalXElementAv(xelements8);
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "d.5.n.5.2");
                    if (xelements9.IsFull())
                        row6.FFR = XmlUtils.ToDecimalXElementAv(xelements9);
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "d.5.n.6.2");
                    if (xelements10.IsFull())
                        row6.FFO = XmlUtils.ToDecimalXElementAv(xelements10);
                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "d.5.n.7.2");
                    if (xelements12.IsFull())
                        row6.FSU = XmlUtils.ToDecimalXElementAv(xelements12);
                    // JACM Se da de baja el campo 2021-02-04
                    try { 
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "d.5.n.9");
                    if (xelements13.IsFull())
                        throw new FaultException("El elemento contiene elementos secundarios no válidos ('d.5.n.9'). Se esperaba 'd.5.n.10'");
                    }
                    catch (Exception ex) { }
                    //    row6.FOTVALOR = XmlUtils.ToDecimalXElementAv(xelements13);
                    //IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "d.5.n.9.2");
                    //if (xelements14.IsFull())
                    //    row6.FOTDESCRIPCION = xelements14.ToStringXElement();
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "d.5.n.8");
                    if (xelements15.IsFull())
                        row6.CLAVE = xelements15.ToStringXElement();
                    if (root.Descendants((XName)"Comercial").Count<XElement>() > 0)
                    {
                        IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "d.5.n.10");
                        if (xelements16.IsFull())
                            row6.FRE = XmlUtils.ToDecimalXElementAv(xelements16);
                    }
                    else
                    {
                        row6.FRE = 0;
                    }
                    IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root, "d.5.n.11");
                    if (xelements17.IsFull())
                        row6.VALORFRACCION = XmlUtils.ToDecimalXElementAv(xelements17);
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(root, "d.5.n.12");
                    if (xelements18.IsFull())
                        row6.VALORCATASTRALFRACCION = XmlUtils.ToDecimalXElementAv(xelements18);
                    dseAvaluo.FEXAVA_SUPERFICIE.AddFEXAVA_SUPERFICIERow(row6);
                }
            }
            IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(terreno, "d.6");
            if (xelements19.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TEINDIVISO = XmlUtils.ToDecimalXElementAv(xelements19);
            IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(terreno, "d.7");
            if (xelements20.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODTOPOGRAFIA = XmlUtils.ToDecimalXElementAv(xelements20);
            IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(terreno, "d.8");
            if (xelements21.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TECARACTERISTICASPARONAMICAS = xelements21.ToStringXElement();
            IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(terreno, "d.9");
            if (xelements22.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TECODDENSIDADHABITACIONAL = XmlUtils.ToDecimalXElementAv(xelements22);
            IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(terreno, "d.10");
            if (xelements23.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TESERVIDUMBRESORESTRICCIONES = xelements23.ToStringXElement();
            IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(terreno, "d.13");
            if (!xelements24.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].VALORTOTALDELTERRENOPROP = XmlUtils.ToDecimalXElementAv(xelements24);
        }

        private void GuardarAvaluoElementosConstruccion(
          XElement elementosConstruccion,
          ref DseAvaluoMantInf dseAvaluo)
        {
            DseAvaluoMantInf.FEXAVA_ELEMENTOSCONSTRow row1 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSCONSTRow)null;
            DseAvaluoMantInf.FEXAVA_OBRANEGRARow row2 = (DseAvaluoMantInf.FEXAVA_OBRANEGRARow)null;
            DseAvaluoMantInf.FEXAVA_REVESTIMIENTOACABADORow row3 = (DseAvaluoMantInf.FEXAVA_REVESTIMIENTOACABADORow)null;
            DseAvaluoMantInf.FEXAVA_CARPINTERIARow row4 = (DseAvaluoMantInf.FEXAVA_CARPINTERIARow)null;
            DseAvaluoMantInf.FEXAVA_INSTALACIONHIDSANRow row5 = (DseAvaluoMantInf.FEXAVA_INSTALACIONHIDSANRow)null;
            DseAvaluoMantInf.FEXAVA_PUERTASYVENTANERIARow row6 = (DseAvaluoMantInf.FEXAVA_PUERTASYVENTANERIARow)null;
            DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.1").Descendants<XElement>().IsFull())
            {
                row2 = dseAvaluo.FEXAVA_OBRANEGRA.NewFEXAVA_OBRANEGRARow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.1");
                if (xelements1.IsFull())
                    row2.CIMENTACION = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.2");
                if (xelements2.IsFull())
                    row2.ESTRUCTURA = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.3");
                if (xelements3.IsFull())
                    row2.MUROS = xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.4");
                if (xelements4.IsFull())
                    row2.ENTREPISOS = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.5");
                if (xelements5.IsFull())
                    row2.TECHOS = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.6");
                if (xelements6.IsFull())
                    row2.AZOTEAS = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.7");
                if (xelements7.IsFull())
                    row2.BARDAS = xelements7.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.2").Descendants<XElement>().IsFull())
            {
                row3 = dseAvaluo.FEXAVA_REVESTIMIENTOACABADO.NewFEXAVA_REVESTIMIENTOACABADORow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.1");
                if (xelements1.IsFull())
                    row3.APLANADOS = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.2");
                if (xelements2.IsFull())
                    row3.PLAFONES = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.3");
                if (xelements3.IsFull())
                    row3.LAMBRINES = xelements3.ToStringXElement();
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.4");
                if (xelements4.IsFull())
                    row3.PISOS = xelements4.ToStringXElement();
                IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.5");
                if (xelements5.IsFull())
                    row3.ZOCLOS = xelements5.ToStringXElement();
                IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.6");
                if (xelements6.IsFull())
                    row3.ESCALERAS = xelements6.ToStringXElement();
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.7");
                if (xelements7.IsFull())
                    row3.PINTURA = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.8");
                if (xelements8.IsFull())
                    row3.RECUBRIMIENTOSESPECIALES = xelements8.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.3").Descendants<XElement>().IsFull())
            {
                row4 = dseAvaluo.FEXAVA_CARPINTERIA.NewFEXAVA_CARPINTERIARow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.1");
                if (xelements1.IsFull())
                    row4.PUERTASINTERIORES = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.2");
                if (xelements2.IsFull())
                    row4.GUARDAROPAS = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.3");
                if (xelements3.IsFull())
                    row4.MUEBLESEMPOTRADOSFIJOS = xelements3.ToStringXElement();
            }
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.4").Descendants<XElement>().IsFull())
            {
                row5 = dseAvaluo.FEXAVA_INSTALACIONHIDSAN.NewFEXAVA_INSTALACIONHIDSANRow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.1");
                if (xelements1.IsFull())
                    row5.MUEBLESBANO = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.2");
                if (xelements2.IsFull())
                    row5.RAMALEOSHIDRAULICOS = xelements2.ToStringXElement();
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.3");
                if (xelements3.IsFull())
                    row5.RAMALEOSSANITARIOS = xelements3.ToStringXElement();
            }
            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(elementosConstruccion, "f.16");
            if (xelements9.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].IEYALUMBRADO = xelements9.ToStringXElement();
            if (XmlUtils.XmlSearchById(elementosConstruccion, "f.5").Descendants<XElement>().IsFull())
            {
                row6 = dseAvaluo.FEXAVA_PUERTASYVENTANERIA.NewFEXAVA_PUERTASYVENTANERIARow();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.5.1");
                if (xelements1.IsFull())
                    row6.HERRERIA = xelements1.ToStringXElement();
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.5.2");
                if (xelements2.IsFull())
                    row6.VENTANERIA = xelements2.ToStringXElement();
            }
            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(elementosConstruccion, "f.6");
            if (xelements10.IsFull())
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.VIDRERIA = xelements10.ToStringXElement();
            }
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(elementosConstruccion, "f.7");
            if (xelements11.IsFull())
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.CERRAJERIA = xelements11.ToStringXElement();
            }
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(elementosConstruccion, "f.8");
            if (xelements12.IsFull())
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.FACHADAS = xelements12.ToStringXElement();
            }
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.9.1");
            if (elementos1.IsFull())
            {
                foreach (XElement root in elementos1)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.9.1.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string stringXelement = xelements1.ToStringXElement();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(stringXelement).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;//codinstespeciales;
                        row7.ClaveInstEsp = stringXelement;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.9.1.n.2");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.DESCRIPCION = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.9.1.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.9.1.n.4");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = XmlUtils.ToDecimalXElementAv(xelements4);
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.9.1.n.5");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = XmlUtils.ToDecimalXElementAv(xelements5);
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.9.1.n.6");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VIDAUTIL = XmlUtils.ToDecimalXElementAv(xelements6);
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "f.9.1.n.7");
                    if (xelements7.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(xelements7);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f.9.1.n.9");
                    if (xelements8.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.IMPORTE = XmlUtils.ToDecimalXElementAv(xelements8);
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "P";
                        row7.CODTIPOELEMENTO = "I";
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(elementosConstruccion, "f.9.2");
            if (elementos2.IsFull())
            {
                foreach (XElement root in elementos2)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.9.2.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string stringXelement = xelements1.ToStringXElement();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(stringXelement).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M; // codinstespeciales;
                        row7.ClaveInstEsp = stringXelement;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.9.2.n.2");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.DESCRIPCION = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.9.2.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.9.2.n.4");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = XmlUtils.ToDecimalXElementAv(xelements4);
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.9.2.n.5");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = XmlUtils.ToDecimalXElementAv(xelements5);
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.9.2.n.6");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VIDAUTIL = XmlUtils.ToDecimalXElementAv(xelements6);
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "f.9.2.n.7");
                    if (xelements7.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(xelements7);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f.9.2.n.9");
                    if (xelements8.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.IMPORTE = XmlUtils.ToDecimalXElementAv(xelements8);
                    }
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "f.9.2.n.10");
                    if (xelements13.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = "0"; //SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Avaluos.Truncate(xelements13.ToDecimalXElement(), 4);
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "C";
                        row7.CODTIPOELEMENTO = "I";
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos3 = XmlUtils.XmlSearchById(elementosConstruccion, "f.10.1");
            if (elementos3.IsFull())
            {
                foreach (XElement root in elementos3)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.10.1.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string stringXelement = xelements1.ToStringXElement();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(stringXelement).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;// codinstespeciales;
                        row7.ClaveInstEsp = stringXelement;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.10.1.n.2");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.DESCRIPCION = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.10.1.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.10.1.n.4");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = XmlUtils.ToDecimalXElementAv(xelements4);
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.10.1.n.5");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = XmlUtils.ToDecimalXElementAv(xelements5);
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.10.1.n.6");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VIDAUTIL = XmlUtils.ToDecimalXElementAv(xelements6);
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "f.10.1.n.7");
                    if (xelements7.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(xelements7);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f.10.1.n.9");
                    if (xelements8.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.IMPORTE = XmlUtils.ToDecimalXElementAv(xelements8);
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "P";
                        row7.CODTIPOELEMENTO = "E";
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos4 = XmlUtils.XmlSearchById(elementosConstruccion, "f.10.2");
            if (elementos4.IsFull())
            {
                foreach (XElement root in elementos4)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.10.2.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string stringXelement = xelements1.ToStringXElement();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(stringXelement).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;// codinstespeciales;
                        row7.ClaveInstEsp = stringXelement;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.10.2.n.2");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.DESCRIPCION = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.10.2.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.10.2.n.4");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = XmlUtils.ToDecimalXElementAv(xelements4);
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.10.2.n.5");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = XmlUtils.ToDecimalXElementAv(xelements5);
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.10.2.n.6");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VIDAUTIL = XmlUtils.ToDecimalXElementAv(xelements6);
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "f.10.2.n.7");
                    if (xelements7.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(xelements7);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f.10.2.n.9");
                    if (xelements8.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.IMPORTE = XmlUtils.ToDecimalXElementAv(xelements8);
                    }
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "f.10.2.n.10");
                    if (xelements13.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Avaluos.Truncate(xelements13.ToDecimalXElement(), 4);
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "C";
                        row7.CODTIPOELEMENTO = "E";
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos5 = XmlUtils.XmlSearchById(elementosConstruccion, "f.11.1");
            if (elementos5.IsFull())
            {
                foreach (XElement root in elementos5)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.11.1.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string stringXelement = xelements1.ToStringXElement();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(stringXelement).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;// codinstespeciales;
                        row7.ClaveInstEsp = stringXelement;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.11.1.n.2");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.DESCRIPCION = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.11.1.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.11.1.n.4");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = XmlUtils.ToDecimalXElementAv(xelements4);
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.11.1.n.5");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = XmlUtils.ToDecimalXElementAv(xelements5);
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.11.1.n.6");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VIDAUTIL = XmlUtils.ToDecimalXElementAv(xelements6);
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "f.11.1.n.7");
                    if (xelements7.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(xelements7);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f.11.1.n.9");
                    if (xelements8.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.IMPORTE = XmlUtils.ToDecimalXElementAv(xelements8);
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "P";
                        row7.CODTIPOELEMENTO = "O";
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> elementos6 = XmlUtils.XmlSearchById(elementosConstruccion, "f.11.2");
            if (elementos6.IsFull())
            {
                foreach (XElement root in elementos6)
                {
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root, "f.11.2.n.1");
                    if (xelements1.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string stringXelement = xelements1.ToStringXElement();
                        //Decimal codinstespeciales = CatastralUtils.ObtenerInstEspecialByClave(stringXelement).CODINSTESPECIALES;
                        row7.CODINSTALACIONESESPECIALES = 0M;// codinstespeciales;
                        row7.ClaveInstEsp = stringXelement;
                    }
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root, "f.11.2.n.2");
                    if (xelements2.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.DESCRIPCION = xelements2.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root, "f.11.2.n.3");
                    if (xelements3.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = xelements3.ToStringXElement();
                    }
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root, "f.11.2.n.4");
                    if (xelements4.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.CANTIDAD = XmlUtils.ToDecimalXElementAv(xelements4);
                    }
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "f.11.2.n.5");
                    if (xelements5.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.EDAD = XmlUtils.ToDecimalXElementAv(xelements5);
                    }
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "f.11.2.n.6");
                    if (xelements6.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VIDAUTIL = XmlUtils.ToDecimalXElementAv(xelements6);
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "f.11.2.n.7");
                    if (xelements7.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(xelements7);
                    }
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "f.11.2.n.9");
                    if (xelements8.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.IMPORTE = XmlUtils.ToDecimalXElementAv(xelements8);
                    }
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "f.11.2.n.10");
                    if (xelements13.IsFull())
                    {
                        if (row7 == null)
                            row7 = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        row7.UNIDAD = SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Avaluos.Truncate(xelements13.ToDecimalXElement(), 4);
                    }
                    if (row7 != null)
                    {
                        row7.CODTIPO = "C";
                        row7.CODTIPOELEMENTO = "O";
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(row7);
                        row7 = (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow)null;
                    }
                }
            }
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(elementosConstruccion, "f.14");
            if (xelements14.IsFull())
            {
                Decimal decimalXelementAv = XmlUtils.ToDecimalXElementAv(xelements14);
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(elementosConstruccion, "f.15");
                if (xelements1.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].VALORTOTALIEEAOC = decimalXelementAv + XmlUtils.ToDecimalXElementAv(xelements1);
            }
            if (row1 != null || row2 != null || (row3 != null || row4 != null) || (row5 != null || row6 != null || dseAvaluo.FEXAVA_ELEMENTOSEXTRA.Any<DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow>()))
            {
                if (row1 == null)
                    row1 = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                dseAvaluo.FEXAVA_ELEMENTOSCONST.AddFEXAVA_ELEMENTOSCONSTRow(row1);
            }
            if (row2 != null)
            {
                row2.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_OBRANEGRA.AddFEXAVA_OBRANEGRARow(row2);
            }
            if (row3 != null)
            {
                row3.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_REVESTIMIENTOACABADO.AddFEXAVA_REVESTIMIENTOACABADORow(row3);
            }
            if (row4 != null)
            {
                row4.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_CARPINTERIA.AddFEXAVA_CARPINTERIARow(row4);
            }
            if (row5 != null)
            {
                row5.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_INSTALACIONHIDSAN.AddFEXAVA_INSTALACIONHIDSANRow(row5);
            }
            if (row6 != null)
            {
                row6.FEXAVA_ELEMENTOSCONSTRow = row1;
                dseAvaluo.FEXAVA_PUERTASYVENTANERIA.AddFEXAVA_PUERTASYVENTANERIARow(row6);
            }
            if (!dseAvaluo.FEXAVA_ELEMENTOSEXTRA.Any<DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow>())
                return;
            foreach (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow elementosextraRow in (TypedTableBase<DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow>)dseAvaluo.FEXAVA_ELEMENTOSEXTRA)
                elementosextraRow.FEXAVA_ELEMENTOSCONSTRow = row1;
        }

        private void GuardarAvaluoDescripcionImueble(
          XElement descripcionInmueble,
          ref DseAvaluoMantInf dseAvaluo, bool esComercial)
        {
            
            DateTime dateTime = Convert.ToDateTime(dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO);
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(descripcionInmueble, "e.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIUSOACTUAL = xelements1.ToStringXElement();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(descripcionInmueble, "e.3");
            if (xelements2.IsFull() && xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVIDAUTILPONDERADA = XmlUtils.ToDecimalXElementAv(xelements2);
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(descripcionInmueble, "e.4");
            if (xelements3.IsFull() && xelements3.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIEDADPONDERADA = XmlUtils.ToDecimalXElementAv(xelements3);
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(descripcionInmueble, "e.5");
            if (xelements4.IsFull() && xelements4.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVIDAUTILREMANENTEPONDERADA = XmlUtils.ToDecimalXElementAv(xelements4);
            DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow construccionesRow = (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow)null;
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.1");
            if (elementos1.IsFull())
            {
                foreach (XElement root in elementos1)
                {
                    DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow row = dseAvaluo.FEXAVA_CONSTRUCCIONES.NewFEXAVA_CONSTRUCCIONESRow();
                    row.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "e.2.1.n.1");
                    if (xelements5.IsFull())
                        row.DESCRIPCION = xelements5.ToStringXElement();
                    string codUso = string.Empty;
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "e.2.1.n.2");
                    if (xelements6.IsFull())
                    {
                        codUso = xelements6.ToStringXElement();
                        row.CODUSOSCONSTRUCCIONES = codUso;
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "e.2.1.n.3");
                    if (xelements7.IsFull())
                        row.NUMNIVELES = XmlUtils.ToDecimalXElementAv(xelements7);
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "e.2.1.n.4");
                    if (xelements8.IsFull())
                    {
                        string stringXelement = xelements8.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdRangoNivelesByCodeAndAno(dateTime, stringXelement);
                        row.CODRANGONIVELES = (Decimal)num;
                    }
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "e.2.1.n.5");
                    if (xelements9.IsFull())
                        row.PUNTAJECLASIFICACION = XmlUtils.ToDecimalXElementAv(xelements9);
                    string codClase = string.Empty;
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "e.2.1.n.6");
                    if (xelements10.IsFull())
                    {
                        codClase = xelements10.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(dateTime, codClase);
                        row.CODCLASESCONSTRUCCION = (Decimal)num;
                    }
                    //if(!esComercial)
                    //{ 
                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "e.2.1.n.7");
                        if (xelements11.IsFull())
                            row.EDAD = XmlUtils.ToDecimalXElementAv(xelements11);
                    //}
                    //else
                    //{
                    //    row.EDAD = 0M;
                    //}
                    if (XmlUtils.XmlSearchById(root, "e.2.1.n.8").IsFull())
                    {
                        int idUsoEjercicio = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(dateTime.Date, codUso);
                        int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(dateTime.Date, codClase);
                        if (codClase != "U")
                        {
                            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                            if (source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>())
                                row.CODCLASESVIDAS = source[0].IDUSOCLASEEJERCICIO;
                        }
                    }
                    if (esComercial)
                    {
                        IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "e.2.1.n.9");
                        if (xelements12.IsFull())
                        {
                            if (!xelements12.ToStringXElement().Equals(""))
                                row.VIDAUTILREMANENTE = XmlUtils.ToDecimalXElementAv(xelements12);
                            else
                                row.VIDAUTILREMANENTE = 1M;
                        }
                        else
                            row.VIDAUTILREMANENTE = 1M;
                    }
                    else
                    {
                        row.VIDAUTILREMANENTE = 1M;
                    }
                    //IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "e.2.1.n.10");
                    //if (xelements13.IsFull())
                    //    row.CODESTADOCONSERVACION = XmlUtils.ToDecimalXElementAv(xelements13);

                    string conserva = xelements3.ToStringXElement();
                    log("GuardarAvaluoDescripcionImueble CODESTADOCONSERVACION ", "conserva : ", conserva);

                    if (conserva != "P"  &&
                        conserva != "PE" &&
                        conserva != "PC" &&
                        conserva != "J"  //&& 
                        //conserva != "H"
                            )
                    {
                        row.CODESTADOCONSERVACION = 2M;
                    }
                    else { row.CODESTADOCONSERVACION = 3M; }

                    IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "e.2.1.n.11");
                    if (xelements14.IsFull())
                        row.SUPERFICIE = XmlUtils.ToDecimalXElementAv(xelements14);
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "e.2.1.n.12");
                    if (xelements15.IsFull())
                        row.VALORUNITARIOREPOSICIONNUEVO = XmlUtils.ToDecimalXElementAv(xelements15);

                    if (esComercial) { 
                    IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "e.2.1.n.13");
                    if (xelements16.IsFull())
                        row.FED = XmlUtils.ToDecimalXElementAv(xelements16);
                    }
                    else
                    {
                        row.FED = 0M;
                    }
                    // JACM Se da de baja el campo 2021-02-04
                    //IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root, "e.2.1.n.14");
                    //if (xelements17.IsFull())
                    //    row.FRE = XmlUtils.ToDecimalXElementAv(xelements17);
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(root, "e.2.1.n.15");
                    if (xelements18.IsFull())
                        row.VALORFRACCION = XmlUtils.ToDecimalXElementAv(xelements18);
                    IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(root, "e.2.1.n.16");
                    if (xelements19.IsFull())
                        row.VALORUNITARIOCAT = XmlUtils.ToDecimalXElementAv(xelements19);
                    if (!esComercial){ 
                        IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(root, "e.2.1.n.17");
                        if (xelements20.IsFull())
                            row.DEPRECIACIONEDAD = XmlUtils.ToDecimalXElementAv(xelements20);
                    }
                    else { row.DEPRECIACIONEDAD = 1M; }
                    row.CODTIPO = "P";
                    dseAvaluo.FEXAVA_CONSTRUCCIONES.AddFEXAVA_CONSTRUCCIONESRow(row);
                    construccionesRow = (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow)null;
                }
            }
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.5");
            if (elementos2.IsFull())
            {
                foreach (XElement root in elementos2)
                {
                    DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow row = dseAvaluo.FEXAVA_CONSTRUCCIONES.NewFEXAVA_CONSTRUCCIONESRow();
                    row.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                    IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "e.2.5.n.1");
                    if (xelements5.IsFull())
                        row.DESCRIPCION = xelements5.ToStringXElement();
                    string codUso = string.Empty;
                    IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "e.2.5.n.2");
                    if (xelements6.IsFull())
                    {
                        codUso = xelements6.ToStringXElement();
                        row.CODUSOSCONSTRUCCIONES = codUso;
                    }
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "e.2.5.n.3");
                    if (xelements7.IsFull())
                        row.NUMNIVELES = XmlUtils.ToDecimalXElementAv(xelements7);
                    string empty = string.Empty;
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "e.2.5.n.4");
                    if (xelements8.IsFull())
                    {
                        string stringXelement = xelements8.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdRangoNivelesByCodeAndAno(dateTime, stringXelement);
                        row.CODRANGONIVELES = (Decimal)num;
                    }
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "e.2.5.n.5");
                    if (xelements9.IsFull())
                        row.PUNTAJECLASIFICACION = XmlUtils.ToDecimalXElementAv(xelements9);
                    string codClase = string.Empty;
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "e.2.5.n.6");
                    if (xelements10.IsFull())
                    {
                        codClase = xelements10.ToStringXElement();
                        int num = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(dateTime, codClase);
                        row.CODCLASESCONSTRUCCION = (Decimal)num;
                    }

                    //JACM Se da de baja el campo 2021-02-15
                    //IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "e.2.5.n.7");
                    //if (xelements11.IsFull())
                    //    row.EDAD = XmlUtils.ToDecimalXElementAv(xelements11);

                    //if (!esComercial)
                    //{
                        IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "e.2.5.n.7");
                        if (xelements11.IsFull())
                            row.EDAD = xelements11.ToDecimalXElement();
                    //}
                    //else { row.EDAD = 0M; }

                    if (esComercial) {
                        int idUsoEjercicio = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(dateTime.Date, codUso);
                        int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(dateTime.Date, codClase);
                        if (codClase != "U")
                        {
                            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                            if (source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>())
                                row.CODCLASESVIDAS = source[0].IDUSOCLASEEJERCICIO;
                        }
                    }
                    else
                    {
                        if (XmlUtils.XmlSearchById(root, "e.2.5.n.8").IsFull())
                        {
                            int idUsoEjercicio = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(dateTime.Date, codUso);
                            int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(dateTime.Date, codClase);
                            if (codClase != "U")
                            {
                                DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable source = this.ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                                if (source.Any<DseAvaluosCatConsulta.FEXAVA_CATCLASEUSORow>())
                                    row.CODCLASESVIDAS = source[0].IDUSOCLASEEJERCICIO;
                            }
                        }
                    }
                    if (esComercial)
                    {
                        IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "e.2.5.n.9");
                        if (xelements12.IsFull())
                        {
                            if (!xelements12.ToStringXElement().Equals(""))
                                row.VIDAUTILREMANENTE = XmlUtils.ToDecimalXElementAv(xelements12);
                            else
                                row.VIDAUTILREMANENTE = 1M;
                        }
                        else
                            row.VIDAUTILREMANENTE = 1M;
                    }
                    else
                    {
                        row.VIDAUTILREMANENTE = 1M;
                    }
                    // JACM Se da de baja el campo 2021-02-04
                    try {
                        if (esComercial)
                        {
                            IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "e.2.5.n.10");
                            if (xelements13.IsFull())
                                throw new FaultException("El elemento contiene elementos secundarios no válidos ('d.5.n.10'). Se esperaba 'd.5.n.11'");
                        }
                    }
                    catch (Exception ex) { log("GuardarAvaluoDescripcionInmueble e.2.5.n.10 Exception",ex.Message,ex.StackTrace); }
                    //    row.CODESTADOCONSERVACION = XmlUtils.ToDecimalXElementAv(xelements13);

                    string conserva = xelements3.ToStringXElement();
                    log("GuardarAvaluoDescripcionImueble CODESTADOCONSERVACION ", "xelements3 : ", conserva);

                    if (conserva != "P"  &&
                        conserva != "PE" &&
                        conserva != "PC" &&
                        conserva != "J"  //&& 
                        //conserva != "H"
                        )
                    {
                        row.CODESTADOCONSERVACION = 2M;
                    }
                    else { row.CODESTADOCONSERVACION = 3M; }


                    //row.CODESTADOCONSERVACION = 0M;


                    IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "e.2.5.n.11");
                    if (xelements14.IsFull())
                        row.SUPERFICIE = XmlUtils.ToDecimalXElementAv(xelements14);
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "e.2.5.n.12");
                    if (xelements15.IsFull())
                        row.VALORUNITARIOREPOSICIONNUEVO = XmlUtils.ToDecimalXElementAv(xelements15);
                    //IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "e.2.5.n.13");
                    //if (xelements16.IsFull())
                    //    row.FED = XmlUtils.ToDecimalXElementAv(xelements16);
                    // JACM Se da de baja el campo 2021-02-04
                    /*IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root, "e.2.5.n.14");
                    if (xelements17.IsFull())
                        row.FRE = XmlUtils.ToDecimalXElementAv(xelements17);*/
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(root, "e.2.5.n.15");
                    if (xelements18.IsFull())
                        row.VALORFRACCION = XmlUtils.ToDecimalXElementAv(xelements18);
                    IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(root, "e.2.5.n.16");
                    if (xelements19.IsFull())
                        row.VALORUNITARIOCAT = XmlUtils.ToDecimalXElementAv(xelements19);
                    if (!esComercial) { 
                    IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(root, "e.2.5.n.17");
                    if (xelements20.IsFull())
                        row.DEPRECIACIONEDAD = XmlUtils.ToDecimalXElementAv(xelements20);
                    }
                    else { row.DEPRECIACIONEDAD = 1M; }
                    IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(root, "e.2.5.n.18");
                    row.DataColumn1 = !xelements21.IsFull() ? string.Empty : string.Format("{0}", (object)XmlUtils.ToDecimalXElementAv(xelements21));
                    row.CODTIPO = row.CODTIPO = "C";
                    dseAvaluo.FEXAVA_CONSTRUCCIONES.AddFEXAVA_CONSTRUCCIONESRow(row);
                    construccionesRow = (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow)null;
                }
            }
            IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.3");
            if (xelements22.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALTOTCONSTRUCCIONESPRIVATIVAS = XmlUtils.ToDecimalXElementAv(xelements22);
            IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.7");
            if (xelements23.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALTOTCONSTRUCCIONESCOMUNES = XmlUtils.ToDecimalXElementAv(xelements23);
            IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(descripcionInmueble, "d.11");
            if (!xelements24.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].DIPORCENTAJESUPULTNIVEL = XmlUtils.ToDecimalXElementAv(xelements24);
        }

        private static string Truncate(Decimal number, int digits)
        {
            double num = Math.Pow(10.0, (double)digits);
            return (Math.Truncate((double)number * num) / num).ToString();
        }

        private void GuardarAvaluoEnfoqueMercado(
          XElement enfoqueMercado,
          ref DseAvaluoMantInf dseAvaluo)
        {
            DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow terrenomercadoRow = (DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow)null;
            DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow datosterrenosRow = (DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow)null;
            DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow construccionesmerRow = (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow)null;
            DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)null;
            DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow row1 = dseAvaluo.FEXAVA_TERRENOMERCADO.NewFEXAVA_TERRENOMERCADORow();
            row1.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.1");
            if (xelements1.IsFull())
                row1.VALORUNITARIOTIERRAPROMEDIO = XmlUtils.ToDecimalXElementAv(xelements1);
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.2");
            if (xelements2.IsFull())
                row1.VALORUNITARIOTIERRAHOMOLOGADO = XmlUtils.ToDecimalXElementAv(xelements2);
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.3");
            if (xelements3.IsFull())
                row1.VALORUNITARIOSINHOMMIN = XmlUtils.ToDecimalXElementAv(xelements3);
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.4");
            if (xelements4.IsFull())
                row1.VALORUNITARIOSINHOMMAX = XmlUtils.ToDecimalXElementAv(xelements4);
            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.5");
            if (xelements5.IsFull())
                row1.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(xelements5);
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.6");
            if (xelements6.IsFull())
                row1.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(xelements6);
            row1.CODTIPOTERRENO = "D";
            dseAvaluo.FEXAVA_TERRENOMERCADO.AddFEXAVA_TERRENOMERCADORow(row1);
            terrenomercadoRow = (DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow)null;
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.1");
            if (elementos1.IsFull())
            {
                foreach (XElement root in elementos1)
                {
                    DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow row2 = dseAvaluo.FEXAVA_DATOSTERRENOS.NewFEXAVA_DATOSTERRENOSRow();
                    row2.FEXAVA_TERRENOMERCADORow = dseAvaluo.FEXAVA_TERRENOMERCADO[0];
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.1.1.n.1");
                    if (xelements7.IsFull())
                        row2.CALLE = xelements7.ToStringXElement();
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.1.1.n.3");
                    if (xelements8.IsFull())
                    {
                        string stringXelement = xelements8.ToStringXElement();
                        row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.1.1.n.2");
                        if (xelements9.IsFull())
                        {
                            row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                            row2["DescColonia"] = (object)xelements9.ToStringXElement();
                        }
                    }
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.1.1.n.4");
                    if (xelements10.IsFull())
                        row2.CODIGOPOSTAL = xelements10.ToStringXElement();
                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.1.1.n.5.1");
                    if (xelements11.IsFull())
                        row2.TELEFONO = xelements11.ToStringXElement();
                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "h.1.1.n.5.2");
                    if (xelements12.IsFull())
                        row2.INFORMANTE = xelements12.ToStringXElement();
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "h.1.1.n.6");
                    if (xelements13.IsFull())
                        row2.DESCRIPCION = xelements13.ToStringXElement();
                    IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "h.1.1.n.7");
                    if (xelements14.IsFull())
                        row2.USOSUELO = xelements14.ToStringXElement();
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "h.1.1.n.8");
                    if (xelements15.IsFull())
                        row2.CUS = XmlUtils.ToDecimalXElementAv(xelements15);
                    IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "h.1.1.n.9");
                    if (xelements16.IsFull())
                        row2.SUPERFICIE = XmlUtils.ToDecimalXElementAv(xelements16);
                    IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root, "h.1.1.n.10.2");
                    if (xelements17.IsFull())
                        row2.FZO = XmlUtils.ToDecimalXElementAv(xelements17);
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(root, "h.1.1.n.11.2");
                    if (xelements18.IsFull())
                        row2.FUB = XmlUtils.ToDecimalXElementAv(xelements18);
                    IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(root, "h.1.1.n.12.2");
                    if (xelements19.IsFull())
                        row2.FFR = XmlUtils.ToDecimalXElementAv(xelements19);
                    IEnumerable<XElement> xelements20 = XmlUtils.XmlSearchById(root, "h.1.1.n.13.2");
                    if (xelements20.IsFull())
                        row2.FFO = XmlUtils.ToDecimalXElementAv(xelements20);
                    IEnumerable<XElement> xelements21 = XmlUtils.XmlSearchById(root, "h.1.1.n.14.2");
                    if (xelements21.IsFull())
                        row2.FSU = XmlUtils.ToDecimalXElementAv(xelements21);
                    // JACM Se da de baja el campo 2021-02-04
                    /*IEnumerable<XElement> xelements22 = XmlUtils.XmlSearchById(root, "h.1.1.n.18.1");
                    if (xelements22.IsFull())
                        row2.FOTVALOR = XmlUtils.ToDecimalXElementAv(xelements22);
                    IEnumerable<XElement> xelements23 = XmlUtils.XmlSearchById(root, "h.1.1.n.18.2");
                    if (xelements23.IsFull())
                        row2.FOTDESCRIPCION = xelements23.ToStringXElement();*/
                    IEnumerable<XElement> xelements24 = XmlUtils.XmlSearchById(root, "h.1.1.n.15");
                    if (xelements24.IsFull())
                        row2.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(xelements24);
                    if (root.Descendants((XName)"Comercial").Count<XElement>() > 0) { 
                        IEnumerable<XElement> xelements25 = XmlUtils.XmlSearchById(root, "h.1.1.n.17");
                        if (xelements25.IsFull())
                            row2.FRE = XmlUtils.ToDecimalXElementAv(xelements25);
                    }
                    else { row2.FRE = 0M; }
                    IEnumerable<XElement> xelements26 = XmlUtils.XmlSearchById(root, "h.1.1.n.16");
                    if (xelements26.IsFull())
                        row2.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(xelements26);
                    row2.CODTIPOTERRENO = "D";
                    dseAvaluo.FEXAVA_DATOSTERRENOS.AddFEXAVA_DATOSTERRENOSRow(row2);
                    datosterrenosRow = (DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow)null;
                }
            }
            DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow row3 = dseAvaluo.FEXAVA_TERRENOMERCADO.NewFEXAVA_TERRENOMERCADORow();
            row3.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements27 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.1");
            if (xelements27.IsFull())
                row3.VALORUNITARIOTIERRAPROMEDIO = XmlUtils.ToDecimalXElementAv(xelements27);
            IEnumerable<XElement> xelements28 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.2");
            if (xelements28.IsFull())
                row3.VALORUNITARIOTIERRAHOMOLOGADO = XmlUtils.ToDecimalXElementAv(xelements28);
            IEnumerable<XElement> xelements29 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.3");
            if (xelements29.IsFull())
                row3.VALORUNITARIOSINHOMMIN = XmlUtils.ToDecimalXElementAv(xelements29);
            IEnumerable<XElement> xelements30 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.4");
            if (xelements30.IsFull())
                row3.VALORUNITARIOSINHOMMAX = XmlUtils.ToDecimalXElementAv(xelements30);
            IEnumerable<XElement> xelements31 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.5");
            if (xelements31.IsFull())
                row3.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(xelements31);
            IEnumerable<XElement> xelements32 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.6");
            if (xelements32.IsFull())
                row3.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(xelements32);
            IEnumerable<XElement> xelements33 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.7");
            if (xelements33.IsFull())
                row3.VALORUNITARIOAPLICADORESIDUAL = XmlUtils.ToDecimalXElementAv(xelements33);
            IEnumerable<XElement> xelements34 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.1");
            if (xelements34.IsFull())
                row3.TOTALINGRESOS = XmlUtils.ToDecimalXElementAv(xelements34);
            IEnumerable<XElement> xelements35 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.2");
            if (xelements35.IsFull())
                row3.TOTALEGRESOS = XmlUtils.ToDecimalXElementAv(xelements35);
            IEnumerable<XElement> xelements36 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.3");
            if (xelements36.IsFull())
                row3.UTILIDAD = xelements36.ToStringXElement();
            IEnumerable<XElement> xelements37 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.4");
            if (xelements37.IsFull())
                row3.VALORUNITARIORESIDUAL = XmlUtils.ToDecimalXElementAv(xelements37);
            row3.CODTIPOTERRENO = "R";
            IEnumerable<XElement> xelements38 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.1");
            if (xelements38.IsFull())
                row3.TIPOINMPROPUESTO = xelements38.ToStringXElement();
            IEnumerable<XElement> xelements39 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.2");
            if (xelements39.IsFull())
                row3.NUMUNIDADESVENDIBLE = XmlUtils.ToDecimalXElementAv(xelements39);
            IEnumerable<XElement> xelements40 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.3");
            if (xelements40.IsFull())
                row3.SUPVENDIBLEPORUNIDAD = XmlUtils.ToDecimalXElementAv(xelements40);
            dseAvaluo.FEXAVA_TERRENOMERCADO.AddFEXAVA_TERRENOMERCADORow(row3);
            IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.4");
            if (elementos2.IsFull())
            {
                foreach (XElement root in elementos2)
                {
                    DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow row2 = dseAvaluo.FEXAVA_DATOSTERRENOS.NewFEXAVA_DATOSTERRENOSRow();
                    row2.FEXAVA_TERRENOMERCADORow = dseAvaluo.FEXAVA_TERRENOMERCADO[1];
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.1");
                    if (xelements7.IsFull())
                        row2.CALLE = xelements7.ToStringXElement();
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.3");
                    if (xelements8.IsFull())
                    {
                        string stringXelement = xelements8.ToStringXElement();
                        row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.2");
                        if (xelements9.IsFull())
                        {
                            row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                            row2["DescColonia"] = (object)xelements9.ToStringXElement();
                        }
                    }
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.4");
                    if (xelements10.IsFull())
                        row2.CODIGOPOSTAL = xelements10.ToStringXElement();
                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.5.1");
                    if (xelements11.IsFull())
                        row2.TELEFONO = xelements11.ToStringXElement();
                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.5.2");
                    if (xelements12.IsFull())
                        row2.INFORMANTE = xelements12.ToStringXElement();
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.6");
                    if (xelements13.IsFull())
                        row2.DESCRIPCION = xelements13.ToStringXElement();
                    IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.7");
                    if (xelements14.IsFull())
                        row2.SUPERFICIE = XmlUtils.ToDecimalXElementAv(xelements14);
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.8");
                    if (xelements15.IsFull())
                        row2.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(xelements15);
                    IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "h.1.3.4.n.9");
                    if (xelements16.IsFull())
                        row2.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(xelements16);
                    row2.CODTIPOTERRENO = "R";
                    dseAvaluo.FEXAVA_DATOSTERRENOS.AddFEXAVA_DATOSTERRENOSRow(row2);
                    datosterrenosRow = (DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow)null;
                }
            }
            IEnumerable<XElement> xelements41 = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.4");
            if (xelements41.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORUNITARIOTIERRAAVALUO = XmlUtils.ToDecimalXElementAv(xelements41);
            DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow row4 = dseAvaluo.FEXAVA_CONSTRUCCIONESMER.NewFEXAVA_CONSTRUCCIONESMERRow();
            row4.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            row4.IDMODOCONSTRUCCION = "V";
            IEnumerable<XElement> xelements42 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.1");
            if (xelements42.IsFull())
                row4.VALORUNITARIOPROMEDIO = XmlUtils.ToDecimalXElementAv(xelements42);
            IEnumerable<XElement> xelements43 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.2");
            if (xelements43.IsFull())
                row4.VALORUNITARIOHOMOLOGADO = XmlUtils.ToDecimalXElementAv(xelements43);
            IEnumerable<XElement> xelements44 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.7");
            if (xelements44.IsFull())
                row4.VALORUNITARIOAPLICABLE = XmlUtils.ToDecimalXElementAv(xelements44);
            IEnumerable<XElement> xelements45 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.3");
            if (xelements45.IsFull())
                row4.VALORUNISINHOMMIN = XmlUtils.ToDecimalXElementAv(xelements45);
            IEnumerable<XElement> xelements46 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.4");
            if (xelements46.IsFull())
                row4.VALORUNISINHOMMAX = XmlUtils.ToDecimalXElementAv(xelements46);
            IEnumerable<XElement> xelements47 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.5");
            if (xelements47.IsFull())
                row4.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(xelements47);
            IEnumerable<XElement> xelements48 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.6");
            if (xelements48.IsFull())
                row4.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(xelements48);
            dseAvaluo.FEXAVA_CONSTRUCCIONESMER.AddFEXAVA_CONSTRUCCIONESMERRow(row4);
            construccionesmerRow = (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow)null;
            IEnumerable<XElement> elementos3 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1");
            if (elementos3.IsFull())
            {
                foreach (XElement root in elementos3)
                {
                    DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow row2 = dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.NewFEXAVA_INVESTPRODUCTOSCOMPRow();
                    row2.FEXAVA_CONSTRUCCIONESMERRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER[0];
                    row2.CODTIPOCOMPARABLE = "V";
                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.2.1.n.1");
                    if (xelements7.IsFull())
                        row2.CALLE = xelements7.ToStringXElement();
                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.2.1.n.3");
                    if (xelements8.IsFull())
                    {
                        string stringXelement = xelements8.ToStringXElement();
                        row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.2.1.n.2");
                        if (xelements9.IsFull())
                        {
                            row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                            row2["DescColonia"] = (object)xelements9.ToStringXElement();
                        }
                    }
                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.2.1.n.4");
                    if (xelements10.IsFull())
                        row2.CODIGOPOSTAL = xelements10.ToStringXElement();
                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.2.1.n.5.1");
                    if (xelements11.IsFull())
                        row2.TELEFONO = xelements11.ToStringXElement();
                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "h.2.1.n.5.2");
                    if (xelements12.IsFull())
                        row2.INFORMANTE = xelements12.ToStringXElement();
                    IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "h.2.1.n.6");
                    if (xelements13.IsFull())
                        row2.DESCRIPCION = xelements13.ToStringXElement();
                    IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "h.2.1.n.7");
                    if (xelements14.IsFull())
                        row2.SUPERFICIEVENDIBLEPORUNIDAD = XmlUtils.ToDecimalXElementAv(xelements14);
                    IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "h.2.1.n.8");
                    if (xelements15.IsFull())
                        row2.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(xelements15);
                    IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "h.2.1.n.9");
                    if (xelements16.IsFull())
                        row2.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(xelements16);
                    if (XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10").IsFull())
                    {
                        IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.1");
                        if (xelements9.IsFull())
                            row2.REGION = xelements9.ToStringXElement();
                        IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.2");
                        if (xelements17.IsFull())
                            row2.MANZANA = xelements17.ToStringXElement();
                        IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.3");
                        if (xelements18.IsFull())
                            row2.LOTE = xelements18.ToStringXElement();
                        IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.4");
                        if (xelements19.IsFull())
                            row2.UNIDADPRIVATIVA = xelements19.ToStringXElement();
                    }
                    row2.CODTIPOCOMPARABLE = "V";
                    dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.AddFEXAVA_INVESTPRODUCTOSCOMPRow(row2);
                    investproductoscompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)null;
                }
            }
            IEnumerable<XElement> xelements49 = XmlUtils.XmlSearchById(enfoqueMercado, "h.3");
            if (xelements49.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVALORMERCADO = XmlUtils.ToDecimalXElementAv(xelements49);
            DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow row5 = dseAvaluo.FEXAVA_CONSTRUCCIONESMER.NewFEXAVA_CONSTRUCCIONESMERRow();
            row5.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements50 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.1");
            if (xelements50.IsFull())
                row5.VALORUNITARIOPROMEDIO = XmlUtils.ToDecimalXElementAv(xelements50);
            IEnumerable<XElement> xelements51 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.2");
            if (xelements51.IsFull())
                row5.VALORUNITARIOHOMOLOGADO = XmlUtils.ToDecimalXElementAv(xelements51);
            IEnumerable<XElement> xelements52 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.7");
            if (xelements52.IsFull())
                row5.VALORUNITARIOAPLICABLE = XmlUtils.ToDecimalXElementAv(xelements52);
            IEnumerable<XElement> xelements53 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.3");
            if (xelements53.IsFull())
                row5.VALORUNISINHOMMIN = XmlUtils.ToDecimalXElementAv(xelements53);
            IEnumerable<XElement> xelements54 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.4");
            if (xelements54.IsFull())
                row5.VALORUNISINHOMMAX = XmlUtils.ToDecimalXElementAv(xelements54);
            IEnumerable<XElement> xelements55 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.5");
            if (xelements55.IsFull())
                row5.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(xelements55);
            IEnumerable<XElement> xelements56 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.6");
            if (xelements56.IsFull())
                row5.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(xelements56);
            row5.IDMODOCONSTRUCCION = "R";
            dseAvaluo.FEXAVA_CONSTRUCCIONESMER.AddFEXAVA_CONSTRUCCIONESMERRow(row5);
            construccionesmerRow = (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow)null;
            IEnumerable<XElement> elementos4 = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.1");
            if (!elementos4.IsFull())
                return;
            foreach (XElement root in elementos4)
            {
                DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow row2 = dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.NewFEXAVA_INVESTPRODUCTOSCOMPRow();
                row2.FEXAVA_CONSTRUCCIONESMERRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER[1];
                row2.CODTIPOCOMPARABLE = "R";
                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root, "h.4.1.n.1");
                if (xelements7.IsFull())
                    row2.CALLE = xelements7.ToStringXElement();
                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root, "h.4.1.n.3");
                if (xelements8.IsFull())
                {
                    string stringXelement = xelements8.ToStringXElement();
                    row2.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(stringXelement);
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.4.1.n.2");
                    if (xelements9.IsFull())
                    {
                        row2.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(xelements9.ToStringXElement(), stringXelement);
                        row2["DescColonia"] = (object)xelements9.ToStringXElement();
                    }
                }
                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root, "h.4.1.n.4");
                if (xelements10.IsFull())
                    row2.CODIGOPOSTAL = xelements10.ToStringXElement();
                IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root, "h.4.1.n.5.1");
                if (xelements11.IsFull())
                    row2.TELEFONO = xelements11.ToStringXElement();
                IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(root, "h.4.1.n.5.2");
                if (xelements12.IsFull())
                    row2.INFORMANTE = xelements12.ToStringXElement();
                IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(root, "h.4.1.n.6");
                if (xelements13.IsFull())
                    row2.DESCRIPCION = xelements13.ToStringXElement();
                IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(root, "h.4.1.n.7");
                if (xelements14.IsFull())
                    row2.SUPERFICIEVENDIBLEPORUNIDAD = XmlUtils.ToDecimalXElementAv(xelements14);
                IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(root, "h.4.1.n.8");
                if (xelements15.IsFull())
                    row2.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(xelements15);
                IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(root, "h.4.1.n.9");
                if (xelements16.IsFull())
                    row2.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(xelements16);
                if (XmlUtils.XmlSearchById(root, "h.4.1.n.10").IsFull())
                {
                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.1");
                    if (xelements9.IsFull())
                        row2.REGION = xelements9.ToStringXElement();
                    IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.2");
                    if (xelements17.IsFull())
                        row2.MANZANA = xelements17.ToStringXElement();
                    IEnumerable<XElement> xelements18 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.3");
                    if (xelements18.IsFull())
                        row2.LOTE = xelements18.ToStringXElement();
                    IEnumerable<XElement> xelements19 = XmlUtils.XmlSearchById(root, "h.4.1.n.10.4");
                    if (xelements19.IsFull())
                        row2.UNIDADPRIVATIVA = xelements19.ToStringXElement();
                }
                row2.CODTIPOCOMPARABLE = "R";
                dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.AddFEXAVA_INVESTPRODUCTOSCOMPRow(row2);
                investproductoscompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)null;
            }
        }

        private void GuardarAvaluoEnfoqueCostosComercial(
          XElement enfoqueCostosComercial,
          ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> xelements = XmlUtils.XmlSearchById(enfoqueCostosComercial, "i.6");
            if (!xelements.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].IMPORTETOTALENFOQUECOSTOS = XmlUtils.ToDecimalXElementAv(xelements);
        }

        private void GuardarAvaluoEnfoqueCostosCatastral(
          XElement enfoqueCostosCatastral,
          ref DseAvaluoMantInf dseAvaluo)
        {
            DseAvaluoMantInf.FEXAVA_ENFOQUECOSTESCATRow row = dseAvaluo.FEXAVA_ENFOQUECOSTESCAT.NewFEXAVA_ENFOQUECOSTESCATRow();
            row.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.4");
            if (xelements1.IsFull())
                row.IMPINSTALACIONESESPECIALES = XmlUtils.ToDecimalXElementAv(xelements1);
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.5");
            if (xelements2.IsFull())
                row.IMPTOTVALORCATASTRAL = XmlUtils.ToDecimalXElementAv(xelements2);
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.6");
            if (xelements3.IsFull())
                row.AVANCEOBRA = XmlUtils.ToDecimalXElementAv(xelements3) * 100M;
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.7");
            if (xelements4.IsFull())
                row.IMPTOTVALCATASTRALOBRAPROCESO = XmlUtils.ToDecimalXElementAv(xelements4);
            dseAvaluo.FEXAVA_ENFOQUECOSTESCAT.AddFEXAVA_ENFOQUECOSTESCATRow(row);
        }

        private void GuardarAvaluoEnfoqueIngresos(
          XElement enfoqueIngresos,
          ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.1");
            dseAvaluo.FEXAVA_AVALUO[0].EIRENTABRUTAMENSUAL = !xelements1.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements1);
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.1");
            dseAvaluo.FEXAVA_AVALUO[0].EIVACIOS = !xelements2.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements2);
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.2");
            dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTOPREDIAL = !xelements3.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements3);
            IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.3");
            dseAvaluo.FEXAVA_AVALUO[0].EISERVICIOAGUA = !xelements4.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements4);
            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.4");
            dseAvaluo.FEXAVA_AVALUO[0].EICONSERVACIONYMANTENIMIENTO = !xelements5.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements5);
            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.5");
            dseAvaluo.FEXAVA_AVALUO[0].EIENERGIAELECTRICA = !xelements6.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements6);
            IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.6");
            dseAvaluo.FEXAVA_AVALUO[0].EIADMINISTRACION = !xelements7.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements7);
            IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.7");
            dseAvaluo.FEXAVA_AVALUO[0].EISEGUROS = !xelements8.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements8);
            IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.8");
            dseAvaluo.FEXAVA_AVALUO[0].EIDEPRECIACIONFISCAL = !xelements9.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements9);
            IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.9");
            dseAvaluo.FEXAVA_AVALUO[0].EIOTROS = !xelements10.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements10);
            IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.10");
            dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESFISCALES = !xelements11.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements11);
            IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.11");
            dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTORENTA = !xelements12.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements12);
            IEnumerable<XElement> xelements13 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.12");
            dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESMENSUALES = !xelements13.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements13);
            IEnumerable<XElement> xelements14 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.13");
            dseAvaluo.FEXAVA_AVALUO[0].EIPORCENTAJEDEDUCCIONESMENS = !xelements14.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements14) * 100M;
            IEnumerable<XElement> xelements15 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.3");
            dseAvaluo.FEXAVA_AVALUO[0].EIPRODUCTOLIQUIDOANUAL = !xelements15.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements15);
            IEnumerable<XElement> xelements16 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.4");
            dseAvaluo.FEXAVA_AVALUO[0].EITASACAPITALIZACION = !xelements16.IsFull() ? 0M : XmlUtils.ToDecimalXElementAv(xelements16) * 100M;
            IEnumerable<XElement> xelements17 = XmlUtils.XmlSearchById(enfoqueIngresos, "k.5");
            if (xelements17.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPORTE = XmlUtils.ToDecimalXElementAv(xelements17);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPORTE = 0M;
        }

        private void GuardarAvaluoResumenConclusionAvaluo(
          XElement conclusionAvaluo,
          ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(conclusionAvaluo, "o.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORCOMERCIAL = XmlUtils.ToDecimalXElementAv(xelements1);
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(conclusionAvaluo, "o.2");
            if (!xelements2.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].VALORCATASTRAL = XmlUtils.ToDecimalXElementAv(xelements2);
        }

        private void GuardarAvaluoValorReferido(XElement valorReferido, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(valorReferido, "p.1");
            if (xelements1.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FECHAVALORREFERIDO = xelements1.First<XElement>().Value.To<DateTime>().ToShortDateString();
            IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(valorReferido, "p.5");
            if (xelements2.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORREFERIDO = XmlUtils.ToDecimalXElementAv(xelements2);
            IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(valorReferido, "p.4");
            if (!xelements3.IsFull())
                return;
            dseAvaluo.FEXAVA_AVALUO[0].FACTORCONVERSION = XmlUtils.ToDecimalXElementAv(xelements3);
        }

        private void GuardarAvaluoAnexoFotografico(
          XElement anexoFotografico,
          ref DseAvaluoMantInf dseAvaluos)
        {
            string empty1 = string.Empty;
            if (XmlUtils.XmlSearchById(anexoFotografico, "q.1").IsFull())
            {
                IEnumerable<XElement> elementos = XmlUtils.XmlSearchById(anexoFotografico, "q.1.2");
                if (elementos.IsFull())
                {
                    int num = 0;
                    foreach (XElement root in elementos)
                    {
                        DseAvaluoMantInf.FEXAVA_FOTOAVALUORow row = dseAvaluos.FEXAVA_FOTOAVALUO.NewFEXAVA_FOTOAVALUORow();
                        row.FEXAVA_AVALUORow = dseAvaluos.FEXAVA_AVALUO[0];
                        IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.1");
                        if (xelements1.IsFull())
                            row.REGION = xelements1.ToStringXElement();
                        IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.2");
                        if (xelements2.IsFull())
                            row.MANZANA = xelements2.ToStringXElement();
                        IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.3");
                        if (xelements3.IsFull())
                            row.LOTE = xelements3.ToStringXElement();
                        IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.4");
                        if (xelements4.IsFull())
                            row.UNIDADPRIVATIVA = xelements4.ToStringXElement();
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root, "q.1.2.n.1");
                        if (xelements5.IsFull())
                        {
                            row.BINARIOS = this.ObtenerImagenPorId(xelements5.ToDecimalXElement());
                            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root, "q.1.2.n.2");
                            if (xelements6.IsFull())
                                row.INTEXT = xelements6.ToStringXElement();
                            row.ROWNUMBER = (Decimal)num;
                            row.IDDOCUMENTOFOTO = XmlUtils.ToDecimalXElementAv(xelements5);
                            dseAvaluos.FEXAVA_FOTOAVALUO.AddFEXAVA_FOTOAVALUORow(row);
                        }
                        ++num;
                    }
                }
            }
            IEnumerable<XElement> elementos1 = XmlUtils.XmlSearchById(anexoFotografico, "q.2");
            if (elementos1.IsFull())
            {
                int num1 = 0;
                foreach (XElement root1 in elementos1)
                {
                    List<string> source = new List<string>();
                    IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.1");
                    if (xelements1.IsFull())
                        source.Add(xelements1.ToStringXElement());
                    IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.2");
                    if (xelements2.IsFull())
                        source.Add(xelements2.ToStringXElement());
                    IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.3");
                    if (xelements3.IsFull())
                        source.Add(xelements3.ToStringXElement());
                    IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.4");
                    if (xelements4.IsFull())
                        source.Add(xelements4.ToStringXElement());
                    string empty2 = string.Empty;
                    for (int index = 0; index < source.Count; ++index)
                        empty2 += source[index];
                    IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(root1, "q.2.n.2");
                    if (elementos2.IsFull())
                    {
                        foreach (XElement root2 in elementos2)
                        {
                            IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "q.2.n.2.n.2");
                            if (xelements5.IsFull())
                            {
                                int num2 = (int)this.TipoInmueble(xelements5.First<XElement>());
                            }
                            IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root2, "q.2.n.2.n.1");
                            if (xelements6.IsFull() && source.Count<string>() == 4)
                            {
                                string filterExpression = " REGION  = " + source[0] + " AND  MANZANA  = " + source[1] + " AND  LOTE  = " + source[2] + " AND  UNIDADPRIVATIVA  = " + source[3];
                                if (((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).Any<DataRow>())
                                {
                                    DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).First<DataRow>();
                                    DseAvaluoMantInf.FEXAVA_FOTOCOMPARABLERow row = dseAvaluos.FEXAVA_FOTOCOMPARABLE.NewFEXAVA_FOTOCOMPARABLERow();
                                    row.FEXAVA_INVESTPRODUCTOSCOMPRow = investproductoscompRow;
                                    IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.1");
                                    if (xelements7.IsFull())
                                        row.REGION = xelements7.ToStringXElement();
                                    IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.2");
                                    if (xelements8.IsFull())
                                        row.MANZANA = xelements8.ToStringXElement();
                                    IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.3");
                                    if (xelements9.IsFull())
                                        row.LOTE = xelements9.ToStringXElement();
                                    IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root1, "q.2.n.1.n.4");
                                    if (xelements10.IsFull())
                                        row.UNIDAD = xelements10.ToStringXElement();
                                    IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root1, "q.2.n.2.n.1");
                                    if (xelements11.IsFull())
                                        row.BINARIOS = this.ObtenerImagenPorId(xelements11.ToDecimalXElement());
                                    IEnumerable<XElement> xelements12 = XmlUtils.XmlSearchById(anexoFotografico, "q.2.n.2.n.2");
                                    if (xelements12.IsFull())
                                        row.INTEXT = xelements12.ToStringXElement();
                                    row.ROWNUMBER = (Decimal)num1;
                                    row.TIPO = "R";
                                    row.IDDOCUMENTOFOTO = XmlUtils.ToDecimalXElementAv(xelements6);
                                    dseAvaluos.FEXAVA_FOTOCOMPARABLE.AddFEXAVA_FOTOCOMPARABLERow(row);
                                }
                            }
                            ++num1;
                        }
                    }
                }
            }
            IEnumerable<XElement> elementos3 = XmlUtils.XmlSearchById(anexoFotografico, "q.3");
            if (!elementos3.IsFull())
                return;
            int num3 = 0;
            foreach (XElement root1 in elementos3)
            {
                List<string> source = new List<string>();
                IEnumerable<XElement> xelements1 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.1");
                if (xelements1.IsFull())
                    source.Add(xelements1.ToStringXElement());
                IEnumerable<XElement> xelements2 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.2");
                if (xelements2.IsFull())
                    source.Add(xelements2.ToStringXElement());
                IEnumerable<XElement> xelements3 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.3");
                if (xelements3.IsFull())
                    source.Add(xelements3.ToStringXElement());
                IEnumerable<XElement> xelements4 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.4");
                if (xelements4.IsFull())
                    source.Add(xelements4.ToStringXElement());
                string empty2 = string.Empty;
                for (int index = 0; index < source.Count; ++index)
                    empty2 += source[index];
                IEnumerable<XElement> elementos2 = XmlUtils.XmlSearchById(root1, "q.3.n.2");
                if (elementos2.IsFull())
                {
                    foreach (XElement root2 in elementos2)
                    {
                        IEnumerable<XElement> xelements5 = XmlUtils.XmlSearchById(root2, "q.3.n.2.n.2");
                        if (xelements5.IsFull())
                        {
                            int num1 = (int)this.TipoInmueble(xelements5.First<XElement>());
                        }
                        IEnumerable<XElement> xelements6 = XmlUtils.XmlSearchById(root2, "q.3.n.2.n.1");
                        if (xelements6.IsFull() && source.Count<string>() == 4)
                        {
                            string filterExpression = " REGION  = " + source[0] + " AND  MANZANA  = " + source[1] + " AND  LOTE  = " + source[2] + " AND  UNIDADPRIVATIVA  = " + source[3];
                            if (((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).Any<DataRow>())
                            {
                                DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)((IEnumerable<DataRow>)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(filterExpression)).First<DataRow>();
                                DseAvaluoMantInf.FEXAVA_FOTOCOMPARABLERow row = dseAvaluos.FEXAVA_FOTOCOMPARABLE.NewFEXAVA_FOTOCOMPARABLERow();
                                row.FEXAVA_INVESTPRODUCTOSCOMPRow = investproductoscompRow;
                                row.BINARIOS = this.ObtenerImagenPorId(xelements6.ToDecimalXElement());
                                IEnumerable<XElement> xelements7 = XmlUtils.XmlSearchById(root2, "q.3.n.2.n.2");
                                if (xelements7.IsFull())
                                    row.INTEXT = xelements7.ToStringXElement();
                                IEnumerable<XElement> xelements8 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.1");
                                if (xelements8.IsFull())
                                    row.REGION = xelements8.ToStringXElement();
                                IEnumerable<XElement> xelements9 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.2");
                                if (xelements9.IsFull())
                                    row.MANZANA = xelements9.ToStringXElement();
                                IEnumerable<XElement> xelements10 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.3");
                                if (xelements10.IsFull())
                                    row.LOTE = xelements10.ToStringXElement();
                                IEnumerable<XElement> xelements11 = XmlUtils.XmlSearchById(root1, "q.3.n.1.n.4");
                                if (xelements11.IsFull())
                                    row.UNIDAD = xelements11.ToStringXElement();
                                row.ROWNUMBER = (Decimal)num3;
                                row.TIPO = "V";
                                row.IDDOCUMENTOFOTO = xelements6.ToDecimalXElement();
                                dseAvaluos.FEXAVA_FOTOCOMPARABLE.AddFEXAVA_FOTOCOMPARABLERow(row);
                            }
                        }
                        ++num3;
                    }
                }
            }
        }

        private XmlDocument GetXmlAvaluo(Decimal idAvaluo)
        {
            try
            {
                SIGAPred.Documental.Services.Negocio.Gestion.DocumentosDigitales.DocumentosDigitales documentosDigitales = new SIGAPred.Documental.Services.Negocio.Gestion.DocumentosDigitales.DocumentosDigitales();
                DocumentosDigitalesClient documentosDigitalesClient = new DocumentosDigitalesClient();
                dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable documentoDigital = documentosDigitalesClient.GetFicherosByDocumentoDigital(idAvaluo);
                if (!documentoDigital.Any<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>())
                    return (XmlDocument)null;
                dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable ficheroDocumentoById = documentosDigitalesClient.GetFicheroDocumentoById(documentoDigital.Where<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>((Func<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow, bool>)(a => a.NOMBRE.Contains(".xml"))).First<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>().IDFICHERODOCUMENTO);
                if (!ficheroDocumentoById.Any<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>())
                    return (XmlDocument)null;
                byte[] binariodatos = ficheroDocumentoById.First<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>().BINARIODATOS;
                if (binariodatos == null)
                    return (XmlDocument)null;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load((Stream)new MemoryStream(binariodatos));
                return xmlDocument;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
        }

        private void ObtenerInsertarDescripciones(ref DseAvaluoMantInf dseAvaluo)
        {
            DocumentosDigitalesClient proxy = new DocumentosDigitalesClient();
            byte[] inArray1 = (byte[])null;
            byte[] inArray2 = (byte[])null;
            try
            {
                inArray1 = proxy.GetFicheroDocumentoById(Convert.ToDecimal(dseAvaluo.FEXAVA_AVALUO[0].DMICRO)).First<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>().BINARIODATOS;
                inArray2 = proxy.GetFicheroDocumentoById(Convert.ToDecimal(dseAvaluo.FEXAVA_AVALUO[0].DMACRO)).First<dseDocumentosDigitales.DOC_FICHERODOCUMENTORow>().BINARIODATOS;
            }
            finally
            {
                proxy.Disconnect();
            }
            dseAvaluo.FEXAVA_AVALUO[0].BYTES_MICRO = Convert.ToBase64String(inArray1);
            dseAvaluo.FEXAVA_AVALUO[0].BYTES_MACRO = Convert.ToBase64String(inArray2);
            string str1 = FiscalUtils.ObtenerDescripcionClaseConstruccion(dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASESCONSTRUCCION);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_CLASECONST = str1;
            string str2 = FiscalUtils.ObtenerDescripcionClasificacionZona(dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASIFICACIONZONA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_CLASIFICACIONZONA = str2;
            string str3 = FiscalUtils.ObtenerDescripcionDensidadPob(dseAvaluo.FEXAVA_AVALUO[0].CUCODDENSIDADPOBLACION);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DENSIDAD = str3;
            string str4 = FiscalUtils.ObtenerDescripcionNivelSocioEc(dseAvaluo.FEXAVA_AVALUO[0].CUCODNIVELSOCIOECONOMICO);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_NIVELSOCIOEC = str4;
            string str5 = FiscalUtils.ObtenerDescripcionAcometidaInm(dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_ACOMETIDAIM = str5;
            string str6 = FiscalUtils.ObtenerDescripcionAcometidaInm(dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLETEL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_ACOMETIDAIMTEL = str6;
            string str7 = FiscalUtils.ObtenerDescripcionAlumbradoPublico(dseAvaluo.FEXAVA_AVALUO[0].CUCODALUMBRADOPUBLICO);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_ALUMBRADOPUBL = str7;
            string str8 = FiscalUtils.ObtenerDescripcionAguaPotable(dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_AGUAPOTABLE = str8;
            string str9 = FiscalUtils.ObtenerDescripcionSuministroElectrico(dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROELECTRICO);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SUMELECTRICO = str9;
            string str10 = FiscalUtils.ObtenerDescripcionGasNatural(dseAvaluo.FEXAVA_AVALUO[0].CUCODGASNATURAL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_GASNATURAL = str10;
            string str11 = FiscalUtils.ObtenerDescripcionGuarniciones(dseAvaluo.FEXAVA_AVALUO[0].CUCODGUARNICIONES);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_GUARNICIONES = str11;
            string str12 = FiscalUtils.ObtenerDescripcionBanquetas(dseAvaluo.FEXAVA_AVALUO[0].CUCODBANQUETAS);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_BANQUETAS = str12;
            string str13 = FiscalUtils.ObtenerDescripcionSenalizacionVias(dseAvaluo.FEXAVA_AVALUO[0].CUCODSENALIZACIONVIAS);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SENALIZACIONVIAS = str13;
            string str14 = FiscalUtils.ObtenerDescripcionSuministroTel(dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROTELEFONICA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SUMINISTROTEL = str14;
            string str15 = FiscalUtils.ObtenerDescripcionVigilanciaZona(dseAvaluo.FEXAVA_AVALUO[0].CUCODVIGILANCIAZONA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_VIGILANCIA = str15;
            string str16 = FiscalUtils.ObtenerDescripcionVialidades(dseAvaluo.FEXAVA_AVALUO[0].CUCODVIALIDADES);
            dseAvaluo.FEXAVA_AVALUO[0].DESCVIALIDADES = str16;
            string str17 = FiscalUtils.ObtenerDescripcionDrenajePluvial(dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALZONA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DRENAJEPLUVZONA = str17;
            string str18 = FiscalUtils.ObtenerDescripcionDrenajePluvial(dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALCALLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DRENAJEPLUVCALLE = str18;
            string str19 = FiscalUtils.ObtenerDescripcionDrenaje(dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEINMUEBLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SISTEMAMIXTO = str19;
            string str20 = FiscalUtils.ObtenerDescripcionDrenaje(dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLERESIDUAL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_RECOLECAGUAS = str20;
            string str21 = FiscalUtils.ObtenerDescripcionTopografia(dseAvaluo.FEXAVA_AVALUO[0].CUCODTOPOGRAFIA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_TOPOGRAFIA = str21;
            string str22 = FiscalUtils.ObtenerDescripcionDensidadHab(dseAvaluo.FEXAVA_AVALUO[0].TECODDENSIDADHABITACIONAL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DENSIDADHAB = str22;
            string str23 = FiscalUtils.ObtenerDescripcionRegimenPropiedad(dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_REGIMENPROPIEDAD = str23;
            string str24 = FiscalUtils.ObtenerDescripcionRecoleccionBasura(dseAvaluo.FEXAVA_AVALUO[0].CUCODRECOLECCIONBASURA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_RECBASURA = str24;
            dseAvaluo.FEXAVA_AVALUO[0].DESC_NOMECLATURACALLES = FiscalUtils.ObtenerDescripcionNomenclaturaCalles(dseAvaluo.FEXAVA_AVALUO[0].CUCODNOMENCLATURACALLE);
            foreach (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow construccionesRow in (TypedTableBase<DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow>)dseAvaluo.FEXAVA_CONSTRUCCIONES)
            {
                if (!construccionesRow.IsCODCLASESCONSTRUCCIONNull())
                    construccionesRow["DescClaseConst"] = (object)FiscalUtils.ObtenerDescripcionClaseConstruccion(construccionesRow.CODCLASESCONSTRUCCION);
            }
            foreach (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow construccionesRow in (TypedTableBase<DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow>)dseAvaluo.FEXAVA_CONSTRUCCIONES)
            {
                string str25 = construccionesRow.IsCODESTADOCONSERVACIONNull() ? string.Empty : FiscalUtils.ObtenerDescripcionEstadoConservacion(construccionesRow.CODESTADOCONSERVACION);
                construccionesRow["DescEstadoConserv"] = (object)str25;
                string str26 = construccionesRow.IsCODCLASESVIDASNull() ? string.Empty : FiscalUtils.ObtenerDescripcionClasesVidas(construccionesRow.CODCLASESVIDAS);
                construccionesRow["DescClaseVida"] = (object)str26;
                string str27 = construccionesRow.IsCODRANGONIVELESNull() ? string.Empty : FiscalUtils.ObtenerDescripcionRangoNiveles(construccionesRow.CODRANGONIVELES);
                construccionesRow["DescRangoNiv"] = (object)str27;
                string str28 = construccionesRow.IsCODCLASESCONSTRUCCIONNull() ? string.Empty : FiscalUtils.ObtenerDescripcionClaseConstruccion(construccionesRow.CODCLASESCONSTRUCCION);
                construccionesRow["DescClaseConst"] = (object)str28;
            }
            foreach (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow elementosextraRow in (TypedTableBase<DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow>)dseAvaluo.FEXAVA_ELEMENTOSEXTRA)
            {
                if (!elementosextraRow.IsCODINSTALACIONESESPECIALESNull())
                {
                    string str25 = FiscalUtils.ObtenerDescripcionInstalacionesEspeciales(Convert.ToDecimal(elementosextraRow.CODINSTALACIONESESPECIALES));
                    elementosextraRow["DescInstEsp"] = (object)str25;
                }
            }
            foreach (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investproductoscompRow in (TypedTableBase<DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow>)dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP)
            {
                string str25 = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(investproductoscompRow.IDDELEGACION);
                investproductoscompRow["DescDeleg"] = (object)str25;
            }
            foreach (DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow datosterrenosRow in (TypedTableBase<DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow>)dseAvaluo.FEXAVA_DATOSTERRENOS)
            {
                string str25 = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(datosterrenosRow.IDDELEGACION);
                datosterrenosRow["DescDeleg"] = (object)str25;
            }
        }

        private string ObtenerNombrePersona(Decimal idPersona)
        {
            try
            {
                RegistroContribuyentesClient proxy = new RegistroContribuyentesClient();
                DseInfoContribuyente infoContribuyente = (DseInfoContribuyente)null;
                try
                {
                    infoContribuyente = proxy.GetInfoContribuyente(idPersona);
                }
                finally
                {
                    proxy.Disconnect();
                }
                if (!infoContribuyente.Contribuyente.Any<DseInfoContribuyente.ContribuyenteRow>())
                    return string.Empty;
                if (infoContribuyente.Contribuyente[0].CODTIPOPERSONA.Equals("F"))
                {
                    StringBuilder stringBuilder = new StringBuilder("");
                    if (!infoContribuyente.Contribuyente[0].IsAPELLIDOPATERNONull())
                        stringBuilder.Append(infoContribuyente.Contribuyente[0].APELLIDOPATERNO).Append(" ");
                    else
                        stringBuilder.Append(" ");
                    if (!infoContribuyente.Contribuyente[0].IsAPELLIDOMATERNONull())
                        stringBuilder.Append(infoContribuyente.Contribuyente[0].APELLIDOMATERNO).Append(", ");
                    else
                        stringBuilder.Append(" ");
                    if (!infoContribuyente.Contribuyente[0].IsNOMBRENull())
                        stringBuilder.Append(infoContribuyente.Contribuyente[0].NOMBRE);
                    else
                        stringBuilder.Append(" ");
                    return stringBuilder.ToString();
                }
                return infoContribuyente.Contribuyente[0].CODTIPOPERSONA.Equals("M") && !infoContribuyente.Contribuyente[0].IsAPELLIDOPATERNONull() ? infoContribuyente.Contribuyente[0].APELLIDOPATERNO : " ";
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        private string ObtenerImagenPorId(Decimal idImagen)
        {
            DocumentosDigitalesClient proxy = new DocumentosDigitalesClient();
            try
            {
                proxy = new DocumentosDigitalesClient();
                Decimal[] listaIdDocumentoDigital = new Decimal[1]
                {
          idImagen
                };
                return Convert.ToBase64String(proxy.GetImagenes(listaIdDocumentoDigital)[0].BINARIODATOS);
            }
            finally
            {
                proxy.Disconnect();
            }
        }

        private void log(string origen, string mensaje, string trace)
        {

            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString() + " " + origen + ": Exception: " + mensaje + "\n\r" + trace + "\n\r");

        }

        public string RegistrarAvaluo(byte[] xmlBytes) => throw new NotImplementedException();
    }


}
