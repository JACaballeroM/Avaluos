using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using SIGAPred.Common.Extensions;
using SIGAPred.Common.WCF;
using SIGAPred.Documental.Services.AccesoDatos.Gestion.DocumentosDigitales;
using SIGAPred.Documental.Services.Negocio.Gestion.DocumentosDigitales;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos;
using SIGAPred.FuentesExternas.Avaluos.Services.Enum;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceDocumentosDigitales;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceRCON;
using System.Text;
using Oracle.DataAccess.Client;
using MyExtentions;
namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio
{
    /// <summary>
    /// Clase con métodos que a partir del xml del avalúo 
    /// Rellenan el dataset que se le pasa al justificante del avalúo
    /// </summary>
    public partial class Avaluos : Interfaces.IAvaluos
    {
        /// <summary>
        /// Método para test
        /// </summary>
        /// <returns>Dataset de prueba vacío</returns>
        public DseAvaluoMantInf testAv()
        {
            return new DseAvaluoMantInf();
        }

        
        /// <summary>
        /// Obtiene la información para el reporte de las cuentas duplicadas
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del reporte</param>
        /// <param name="fechaFin">Fecha final de la consulta</param>
        /// <param name="region">region de la cuenta catastral</param>
        /// <param name="manzana">Manzana de la cuenta catastral</param>
        /// <param name="lote">Lote de la cuenta catastral</param>
        /// <param name="unidad">Unidad privativa de la cuenta catastral</param>
        /// <param name="registro">Registro del perito o la sociedad</param>
        /// <param name="completa">Indica si es consulta completa o parcial</param>
        /// <returns>Un dataset con el resultado de la consulta</returns>
        public DataSet GetCuentasDuplicadas(DateTime fechaInicio, DateTime fechaFin, string region, string manzana, string lote, string unidad, string registro, bool completa)
        {
            DataSet ds = new DataSet();
            try
            {
                SIGAPred.Data.TranHelper THelper = new SIGAPred.Data.TranHelper();
                string comp = completa ? "T" : "P";
                using (OracleCommand command = new OracleCommand("FEXAVA.FEXAVA_AVALUOS_PKG.fexava_cuentasduplicadas_p"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("p_fechainicial", OracleDbType.Date, fechaInicio, ParameterDirection.Input);
                    command.Parameters.Add("p_fechafinal", OracleDbType.Date, fechaFin, ParameterDirection.Input);
                    command.Parameters.Add("p_region", OracleDbType.Varchar2, region, ParameterDirection.Input);
                    command.Parameters.Add("p_manzana", OracleDbType.Varchar2, manzana, ParameterDirection.Input);
                    command.Parameters.Add("p_lote", OracleDbType.Varchar2, lote, ParameterDirection.Input);
                    command.Parameters.Add("p_unidad", OracleDbType.Varchar2, unidad, ParameterDirection.Input);
                    command.Parameters.Add("p_registro", OracleDbType.Varchar2, 2000, registro, ParameterDirection.Input);
                    command.Parameters.Add("p_tipoconsulta", OracleDbType.Varchar2, 20, comp, ParameterDirection.Input);
                    command.Parameters.Add(new OracleParameter("c_avaluos", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    ds = THelper.EjecutaConsultaSP(command);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// Obtiene solo la informacion de los antecedentes en un objeto de tipo DSeAvaluoMantInf.
        /// </summary>
        /// <param name="idAvaluo">Id del avalúo.</param>
        /// <returns>
        /// DseAvaluoMantInf con la información cargada.
        /// </returns>
        public DseAvaluoMantInf GetAvaluoAntecedentesAI(decimal idAvaluo)
        {
            DseAvaluoMantInf dseAvaluo = new DseAvaluoMantInf();
            List<decimal> listaFicheros = new List<decimal>();
            bool esComercial = false;

            try
            {
                //Cargar el xml recibido.
                XmlDocument xmlAvaluo = GetXmlAvaluo(idAvaluo);

                IEnumerable<XElement> query = null;
                XElement data = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;

                foreach (DataColumn column in dseAvaluo.FEXAVA_AVALUO.Columns)
                    column.AllowDBNull = true;

                dseAvaluo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(dseAvaluo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow());
                dseAvaluo.FEXAVA_AVALUO[0].CODESTADOAVALUO = EstadosAvaluoEnum.Recibido.ToDecimal();
                dseAvaluo.FEXAVA_AVALUO[0].FECHA_PRESENTACION = DateTime.Now.Date;

                if (data.Descendants(Constantes.XML_TIPO_COMERCIAL).Count() > 0)
                {
                    esComercial = true;
                    dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE = Constantes.PAR_COD_TIPOTRAMITE_COMERCIAL;
                }
                else if (data.Descendants(Constantes.XML_TIPO_CATASTRAL).Count() > 0)
                {
                    esComercial = false;
                    dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE = Constantes.PAR_COD_TIPOTRAMITE_CATASTRAL;
                }

                query = XmlUtils.XmlSearchById(data, "b");
                if (query.IsFull())
                    GuardarAvaluoAntecedentesAI(query.First(), ref dseAvaluo);

                return dseAvaluo;


            }
            //catch (FaultException<AvaluosInfoException> diex)
            //{
            //    ExceptionPolicyWrapper.HandleException(diex);
            //    throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(diex.Detail.Descripcion));
            //}
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;// new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        /// <summary>
        /// Guarda la información del avalúo en el dataset DSeAvaluoMantInf.
        /// </summary>
        /// <param name="idAvaluo">Id del avalúo.</param>
        /// <returns>
        /// DseAvaluoMantInf con la información cargada.
        /// </returns>
        public DseAvaluoMantInf GuardarAvaluoInformeComercialAI(decimal idAvaluo)
        {
            DseAvaluoMantInf dseAvaluo = new DseAvaluoMantInf();
            List<decimal> listaFicheros = new List<decimal>();
            bool esComercial = false;

            try
            {
                //Cargar el xml recibido.
                XmlDocument xmlAvaluo = GetXmlAvaluo(idAvaluo);

                IEnumerable<XElement> query = null;
                XElement data = (XElement)XDocument.Parse(xmlAvaluo.InnerXml).Root;

                foreach (DataColumn column in dseAvaluo.FEXAVA_AVALUO.Columns)
                    column.AllowDBNull = true;

                dseAvaluo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(dseAvaluo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow());
                dseAvaluo.FEXAVA_AVALUO[0].CODESTADOAVALUO = EstadosAvaluoEnum.Recibido.ToDecimal();
                dseAvaluo.FEXAVA_AVALUO[0].FECHA_PRESENTACION = DateTime.Now.Date;

                if (data.Descendants(Constantes.XML_TIPO_COMERCIAL).Count() > 0)
                {
                    esComercial = true;
                    dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE = Constantes.PAR_COD_TIPOTRAMITE_COMERCIAL;
                }
                else if (data.Descendants(Constantes.XML_TIPO_CATASTRAL).Count() > 0)
                {
                    esComercial = false;
                    dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE = Constantes.PAR_COD_TIPOTRAMITE_CATASTRAL;
                }

                #region g - ConsideracionesPrevias al avalúo
                query = XmlUtils.XmlSearchById(data, "g.1");
                //#if DEBUG
                //                dseAvaluo.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = "dsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu dsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojerdsahklfjasfdasdfasfdasfreqwrjqwiruqwioepuropqwiurqwporuweqoruqwopu pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojer pqweruwqjlasdhfla sñfhsodfopqeruqwperiuqwpiorjlasdfjlañhflñasfjañslfjkojer";
                //#else


                //                if(query.IsFull())
                //                 dseAvaluo.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = query.ToStringXElement();
                //#endif
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIAS = query.ToStringXElement();
                #endregion

                #region n - ConsideracionesPrevias a la conclusion
                query = XmlUtils.XmlSearchById(data, "n.1");
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CONSIDERACIONESPREVIASCONCLUSION = query.ToStringXElement();
                #endregion

                #region  a - Identificacion.
                query = XmlUtils.XmlSearchById(data, "a");
                if (query.IsFull())
                    GuardarAvaluoIdentificacion(query.First(), ref dseAvaluo);
                #endregion

                #region  b - Antecedentes.
                query = XmlUtils.XmlSearchById(data, "b");
                if (query.IsFull())
                    GuardarAvaluoAntecedentes(query.First(), ref dseAvaluo);
                #endregion

                #region  c - Caracteristicas Urbanas.
                query = XmlUtils.XmlSearchById(data, "c");
                if (query.IsFull())
                    GuardarAvaluoCaracteristicasUrbanas(query.First(), ref dseAvaluo);
                #endregion

                #region  d - Terreno.
                query = XmlUtils.XmlSearchById(data, "d");
                if (query.IsFull())
                    GuardarAvaluoTerreno(query.First(), ref dseAvaluo);
                #endregion

                #region e - Descripcion del inmueble.
                query = XmlUtils.XmlSearchById(data, "e");
                if (query.IsFull())
                    GuardarAvaluoDescripcionImueble(query.First(), ref dseAvaluo, esComercial);
                #endregion

                #region f - Elementos de la construccion.
                query = XmlUtils.XmlSearchById(data, "f");
                if (query.IsFull())
                    GuardarAvaluoElementosConstruccion(query.First(), ref dseAvaluo);
                #endregion

                #region  h - Enfoque del mercado.
                if (esComercial)
                {
                    query = XmlUtils.XmlSearchById(data, "h");
                    if (query.IsFull())
                        GuardarAvaluoEnfoqueMercado(query.First(), ref dseAvaluo);
                }
                #endregion

                #region i - Enfoque de costos Avaluo Comercial
                if (esComercial)
                {
                    query = XmlUtils.XmlSearchById(data, "i");
                    if (query.IsFull())
                        GuardarAvaluoEnfoqueCostosComercial(query.First(), ref dseAvaluo);
                }
                #endregion

                #region j - Enfoque de costos Avaluo Catastral
                if (!esComercial)
                {
                    query = XmlUtils.XmlSearchById(data, "j");
                    if (query.IsFull())
                        GuardarAvaluoEnfoqueCostosCatastral(query.First(), ref dseAvaluo);
                }
                #endregion

                #region k - Enfoque de ingresos.
                query = XmlUtils.XmlSearchById(data, "k");
                if (query.IsFull())
                    GuardarAvaluoEnfoqueIngresos(query.First(), ref dseAvaluo);
                #endregion

                #region o - Conclusiones del avaluo.
                query = XmlUtils.XmlSearchById(data, "o");
                if (query.IsFull())
                    GuardarAvaluoResumenConclusionAvaluo(query.First(), ref dseAvaluo);
                #endregion

                #region p - Valor referido.
                query = XmlUtils.XmlSearchById(data, "p");
                if (query.IsFull())
                    GuardarAvaluoValorReferido(query.First(), ref dseAvaluo);
                #endregion

                #region q - Anexo Fotografico.
                query = XmlUtils.XmlSearchById(data, "q");
                if (query.IsFull())
                    GuardarAvaluoAnexoFotografico(query.First(), ref dseAvaluo);
                #endregion

                ObtenerInsertarDescripciones(ref dseAvaluo);

                return dseAvaluo;


            }
            catch (FaultException<AvaluosInfoException> diex)
            {
                ExceptionPolicyWrapper.HandleException(diex);
                throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(diex.Detail.Descripcion));
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        /// <summary>
        /// Inserta los datos referentes a los identificacion en el dseAvaluos desde el elemento xml.
        /// </summary>
        /// <param name="identificacion">Elemento xml con los datos de identificacion.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoIdentificacionAI(XElement identificacion, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;
            query = XmlUtils.XmlSearchById(identificacion, "a.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].NUMEROAVALUO = query.ToStringXElement();


            query = XmlUtils.XmlSearchById(identificacion, "a.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO = (query.First().Value.To<DateTime>()).ToShortDateString();

            string registroPerito = string.Empty;
            query = XmlUtils.XmlSearchById(identificacion, "a.3");
            if (query.IsFull())
            {
                registroPerito = query.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONAPERITO = IdPeritoSociedadByRegistro(registroPerito, string.Empty, true);
                decimal idpersona = dseAvaluo.FEXAVA_AVALUO[0].IDPERSONAPERITO;
                dseAvaluo.FEXAVA_AVALUO[0].REGTDF_PERITO = query.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].NOMBRE_PERITO = ObtenerNombrePersona(idpersona);
            }

            query = XmlUtils.XmlSearchById(identificacion, "a.4");
            if (query.IsFull())
            {
                dseAvaluo.FEXAVA_AVALUO[0].IDPERSONASOCIEDAD = IdPeritoSociedadByRegistro(registroPerito, query.ToStringXElement(), false);
                decimal idpersona = dseAvaluo.FEXAVA_AVALUO[0].IDPERSONASOCIEDAD;
                dseAvaluo.FEXAVA_AVALUO[0].REGTDF_SOCIEDAD = query.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].NOMBRE_SOCI = ObtenerNombrePersona(idpersona);
            }
            string tipo = string.Empty;
            if (dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE.Equals(Constantes.PAR_COD_TIPOTRAMITE_CATASTRAL))
                tipo = Constantes.PAR_AVALUO_CATASTRAL_SHORT_MAYUS;
            else if (dseAvaluo.FEXAVA_AVALUO[0].CODTIPOTRAMITE.Equals(Constantes.PAR_COD_TIPOTRAMITE_COMERCIAL))
                tipo = Constantes.PAR_AVALUO_COMERCIAL_SHORT_MAYUS;

            dseAvaluo.FEXAVA_AVALUO[0].NUMEROUNICO = AvaluosUtils.ObtenerNumUnicoAv(tipo);
        }

        /// <summary>
        /// Inserta los datos referentes a los antecedentes en el dseAvaluos desde el elemento xml.
        /// </summary>
        /// <param name="antecedentes">Elemento xml con los datos de antecedentes.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoAntecedentesAI(XElement antecedentes, ref DseAvaluoMantInf dseAvaluo)
        {
            try
            {
                IEnumerable<XElement> query = null;
                DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow SolicitanteRow = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
                DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow PropietarioRow = dseAvaluo.FEXAVA_DATOSPERSONAS.NewFEXAVA_DATOSPERSONASRow();
                string region = string.Empty;
                string manzana = string.Empty;
                string lote = string.Empty;
                string unidad = string.Empty;
                SolicitanteRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                PropietarioRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                #region b.1 - Solicitante
                query = XmlUtils.XmlSearchById(antecedentes, "b.1.1");
                if (query.IsFull())
                    SolicitanteRow.APELLIDOPATERNO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.2");
                if (query.IsFull())
                    SolicitanteRow.APELLIDOMATERNO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.3");
                if (query.IsFull())
                    SolicitanteRow.NOMBRE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.4");
                if (query.IsFull())
                    SolicitanteRow.CALLE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.5");
                if (query.IsFull())
                    SolicitanteRow.NUMEROINTERIOR = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.6");
                if (query.IsFull())
                    SolicitanteRow.NUMEROEXTERIOR = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.8");
                if (query.IsFull())
                    SolicitanteRow.CODIGOPOSTAL = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.1.9");
                if (query.IsFull())
                {
                    string codDelegacion = query.ToStringXElement();
                    SolicitanteRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(codDelegacion);
                    SolicitanteRow[Constantes.COL_DESC_DELEGACION] = query.ToStringXElement();
                    IEnumerable<XElement> queryn = XmlUtils.XmlSearchById(antecedentes, "b.1.7");
                    if (queryn.IsFull())
                    {
                        SolicitanteRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion);
                        SolicitanteRow[Constantes.COL_DESC_COLONIA] = queryn.ToStringXElement();
                        // SolicitanteRow[Constantes.COL_DESC_COLONIA] = CatastralUtils.ObtenerNombreColoniaPorIdColonia(SolicitanteRow.IDCOLONIA);

                    }
                }
                query = XmlUtils.XmlSearchById(antecedentes, "b.1.10");
                if (query.IsFull())
                {
                    SolicitanteRow.TipoPersona = query.ToStringXElement();
                }


                SolicitanteRow.ROL = Constantes.COD_PERSONA_SOLICITANTE;
                #endregion

                #region b.2 - Propietario
                query = XmlUtils.XmlSearchById(antecedentes, "b.2.1");
                if (query.IsFull())
                    PropietarioRow.APELLIDOPATERNO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.2");
                if (query.IsFull())
                    PropietarioRow.APELLIDOMATERNO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.3");
                if (query.IsFull())
                    PropietarioRow.NOMBRE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.4");
                if (query.IsFull())
                    PropietarioRow.CALLE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.5");
                if (query.IsFull())
                    PropietarioRow.NUMEROINTERIOR = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.6");
                if (query.IsFull())
                    PropietarioRow.NUMEROEXTERIOR = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.8");
                if (query.IsFull())
                    PropietarioRow.CODIGOPOSTAL = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.2.9");
                if (query.IsFull())
                {
                    string codDelegacion = query.ToStringXElement();
                    PropietarioRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(codDelegacion);
                    PropietarioRow[Constantes.COL_DESC_DELEGACION] = query.ToStringXElement();
                    IEnumerable<XElement> queryn = XmlUtils.XmlSearchById(antecedentes, "b.2.7");
                    if (queryn.IsFull())
                    {
                        PropietarioRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion);
                        PropietarioRow[Constantes.COL_DESC_COLONIA] = queryn.ToStringXElement();
                        // PropietarioRow[Constantes.COL_DESC_COLONIA] = CatastralUtils.ObtenerNombreColoniaPorIdColonia(PropietarioRow.IDCOLONIA);
                    }
                }
                query = XmlUtils.XmlSearchById(antecedentes, "b.2.10");
                if (query.IsFull())
                {
                    PropietarioRow.TipoPersona = query.ToStringXElement();
                }

                PropietarioRow.ROL = Constantes.COD_PERSONA_PROPIETARIO;
                #endregion

                #region b.3- Inmueble que se valua
                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.1");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].CALLE = query.ToStringXElement();

                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.2");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].INT = query.ToStringXElement();

                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.3");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].EXT = query.ToStringXElement();

                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.4");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].MANZANA_CC = query.ToStringXElement();


                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.5");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].LOTE = query.ToStringXElement();

                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.6");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].EDIFICIO = query.ToStringXElement();

                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.8");
                //if (query.IsFull())
                //    dseAvaluo.FEXAVA_AVALUO[0].CP = query.ToStringXElement();

                //query = XmlUtils.XmlSearchById(antecedentes, "b.3.9");
                //if (query.IsFull())
                //{
                //    string codDelegacion = query.ToStringXElement();
                //    dseAvaluo.FEXAVA_AVALUO[0].DELEGACION = query.ToStringXElement();
                //    IEnumerable<XElement> queryn = XmlUtils.XmlSearchById(antecedentes, "b.3.7");
                //    if (queryn.IsFull())
                //        dseAvaluo.FEXAVA_AVALUO[0].COLONIA = queryn.ToStringXElement();// ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion).ToString();
                //}

                query = XmlUtils.XmlSearchById(antecedentes, "b.3.10.1");
                if (query.IsFull())
                    region= dseAvaluo.FEXAVA_AVALUO[0].REGION = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.3.10.2");
                if (query.IsFull())
                    manzana = dseAvaluo.FEXAVA_AVALUO[0].MANZANA = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.3.10.3");
                if (query.IsFull())
                    lote = dseAvaluo.FEXAVA_AVALUO[0].LOTE_CC = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.3.10.4");
                if (query.IsFull())
                    unidad= dseAvaluo.FEXAVA_AVALUO[0].UNIDADPRIVATIVA = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.3.10.5");
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].DIGITOVERIFICADOR = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.3.11");
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].CUENTAAGUA = query.ToStringXElement();
                #endregion

                query = XmlUtils.XmlSearchById(antecedentes, "b.4");
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].PROPOSITO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.5");
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].OBJETO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(antecedentes, "b.6");
                if (query.IsFull())
                {
                    dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD = XmlUtils.ToDecimalXElementAv(query);
                    dseAvaluo.FEXAVA_AVALUO[0].REGIMEN = 0;
                    dseAvaluo.FEXAVA_AVALUO[0].TIPOCODOMINIO = 0;
                }
                query = XmlUtils.XmlSearchById(antecedentes, "b.6.1");
                if (query.IsFull())
                {
                    dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD = XmlUtils.ToDecimalXElementAv(query);
                    dseAvaluo.FEXAVA_AVALUO[0].REGIMEN = XmlUtils.ToDecimalXElementAv(query);
                }

                query = XmlUtils.XmlSearchById(antecedentes, "b.6.2");
                if (query.IsFull())
                    if (!query.ToStringXElement().Trim().Equals(""))
                        dseAvaluo.FEXAVA_AVALUO[0].TIPOCODOMINIO = XmlUtils.ToDecimalXElementAv(query);
                DataSet ds = GetDireccionCas(region, manzana, lote,unidad);
                if(ds.Tables.Count >0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dseAvaluo.FEXAVA_AVALUO[0].CALLE = ds.Tables[0].Rows[0].ToStringValue("CALLE");
                        dseAvaluo.FEXAVA_AVALUO[0].INT = ds.Tables[0].Rows[0].ToStringValue("NUMINT");
                        dseAvaluo.FEXAVA_AVALUO[0].EXT = ds.Tables[0].Rows[0].ToStringValue("NUMEXT");
                        dseAvaluo.FEXAVA_AVALUO[0].LOTE = ds.Tables[0].Rows[0].ToStringValue("LOTE");
                        dseAvaluo.FEXAVA_AVALUO[0].EDIFICIO = ds.Tables[0].Rows[0].ToStringValue("EDIFICIO");
                        dseAvaluo.FEXAVA_AVALUO[0].CP = ds.Tables[0].Rows[0].ToStringValue("CODIGOPOSTAL");
                        dseAvaluo.FEXAVA_AVALUO[0].DELEGACION = ds.Tables[0].Rows[0].ToStringValue("DELEGACION");
                        dseAvaluo.FEXAVA_AVALUO[0].COLONIA = ds.Tables[0].Rows[0].ToStringValue("COLONIA");
                    }
                    else
                    {
                        throw new Exception("No se pudieron obtener los datos de la dirección");
                    }
                }
                else
                {
                    throw new Exception("No se pudieron obtener los datos de la dirección");
                }

                dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(SolicitanteRow);
                dseAvaluo.FEXAVA_DATOSPERSONAS.AddFEXAVA_DATOSPERSONASRow(PropietarioRow);

            }
            //catch (FaultException<AvaluosInfoException> diex)
            //{
            //    ExceptionPolicyWrapper.HandleException(diex);
            //    throw new FaultException<AvaluosInfoException>(new AvaluosInfoException(diex.Detail.Descripcion));
            //}
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;// new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        /// <summary>
        /// Metodo que obtiene la dirección de catastro
        /// </summary>
        /// <param name="region">Region de la cuenta catastral</param>
        /// <param name="manzana">Manzana de la cuenta catastral</param>
        /// <param name="lote">Lote de la cuenta catastral</param>
        /// <param name="unidad">Unidad de la cuenta catastral</param>
        /// <returns>Un DataSet con la dirección de catastro</returns>
        private DataSet GetDireccionCas(string region, string manzana, string lote, string unidad)
        {
            DataSet ds = new DataSet();
            try
            {
                string cuenta = string.Format("{0}{1}{2}{3}", region, manzana, lote, unidad);
                SIGAPred.Data.TranHelper THelper = new SIGAPred.Data.TranHelper();
                using (OracleCommand command = new OracleCommand("FEXAVA.fexava_datosestadisticos_pkg.fexava_obtndirecc_cuent_p"))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("par_cuenta", OracleDbType.Varchar2, cuenta, ParameterDirection.Input);
                    command.Parameters.Add(new OracleParameter("c_avaluo", OracleDbType.RefCursor)).Direction = ParameterDirection.Output;
                    ds = THelper.EjecutaConsultaSP(command);
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new Exception("Se presentó un error al intentar obtener los datos de catastro");// new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
            return ds;
        }
            
        /// <summary>
        /// Inserta los datos referentes a las caracteristicas urbanas en el dseAvaluos desde el elemento
        /// xml.
        /// </summary>
        /// <param name="caracteristicasUrbanas">Elemento xml con los datos de caracteristicas urbanas.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoCaracteristicasUrbanasAI(XElement caracteristicasUrbanas, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;
            string descCaracteristica = string.Empty;
            string codCaracteristicaStr = string.Empty;
            decimal codCaracteristica;
            DseAvaluosCatConsulta dsCat = ApplicationCache.DseCatalogosConsulta;

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.0");
            if (query.IsFull())
            {
                codCaracteristicaStr = query.ToStringXElement();
                dseAvaluo.FEXAVA_AVALUO[0].CUCONTAMINACIONAMBIENTALZONA = codCaracteristicaStr.Trim();
            }
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.1");
            if (query.IsFull())
            {
                codCaracteristica = XmlUtils.ToDecimalXElementAv(query);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASIFICACIONZONA = codCaracteristica;
            }

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUINDICESATURACIONZONA = XmlUtils.ToDecimalXElementAv(query) * 100;//Es un porcentaje indicado entre 0 y 1 en el xml pero que se debe mostrar entre 0 y 100 en el report

            DateTime fechaAvaluo = Convert.ToDateTime(dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO);
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.3");
            if (query.IsFull())
            {
                string codClase = query.ToStringXElement();
                int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaAvaluo, codClase);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASESCONSTRUCCION = idClaseEjercicio;
            }
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.4");
            if (query.IsFull())
            {
                codCaracteristica = XmlUtils.ToDecimalXElementAv(query);
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDENSIDADPOBLACION = codCaracteristica;

            }
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.5");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODNIVELSOCIOECONOMICO = XmlUtils.ToDecimalXElementAv(query);

            #region c.6 - Uso del suelo
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUUSO = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUAREALIBREOBLIGATORIO = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUNUMMAXNIVELESACONSTRUIR = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.6.4");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCOEFICIENTE = XmlUtils.ToDecimalXElementAv(query);
            #endregion

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.7");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VIASDEACCESO = query.ToStringXElement();

            #region c.8 - Servicios públicos y equipamiento urbano
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLE = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLERESIDUAL = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALCALLE = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.4");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALZONA = XmlUtils.ToDecimalXElementAv(query);
            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.5");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEINMUEBLE = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.7");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROELECTRICO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.8");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLE = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.9");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODALUMBRADOPUBLICO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.10");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODVIALIDADES = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.11");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODBANQUETAS = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.12");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODGUARNICIONES = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.13");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUPORCENTAJEINFRAESTRUCTURA = XmlUtils.ToDecimalXElementAv(query) * 100;//Es un porcentaje indicado entre 0 y 1 en el xml pero que se debe mostrar entre 0 y 100 en el report

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.14");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODGASNATURAL = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.15");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROTELEFONICA = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.16");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLETEL = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.17");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODSENALIZACIONVIAS = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.18");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODNOMENCLATURACALLE = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.19");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUDISTANCIATRANSPORTEURBANO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.20");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUFRECUENCIATRANSPORTEURBANO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.21");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUDISTANCIATRANSPORTESUBURB = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.22");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUFRECUENCIATRANSPORTESUBURB = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.23");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODVIGILANCIAZONA = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.24");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODRECOLECCIONBASURA = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.25");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEIGLESIA = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.26");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEMERCADOS = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.27");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEPLAZASPUBLICOS = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.28");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEPARQUESJARDINES = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.29");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEESCUELAS = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.30");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEHOSPITALES = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.31");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEBANCOS = BooleanXMLtoOracle(query.First().Value);

            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.32");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUEXISTEESTACIONTRANSPORTE = BooleanXMLtoOracle(query.First().Value);


            query = XmlUtils.XmlSearchById(caracteristicasUrbanas, "c.8.33");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].NIVELEQUIPAMIENTOURBANO = XmlUtils.ToDecimalXElementAv(query);

            #endregion
        }

        /// <summary>
        /// Inserta los datos referentes al terreno en el dseAvaluos desde el elemento xml.
        /// </summary>
        /// <param name="terreno">Elemento xml con los datos del terreno.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoTerrenoAI(XElement terreno, ref DseAvaluoMantInf dseAvaluo, bool esComercial)
        {
            List<decimal> listaIdFicheros = new List<decimal>();
            IEnumerable<XElement> query = null;
            IEnumerable<XElement> queryn = null;

            DseAvaluoMantInf.FEXAVA_FUENTEINFORMACIONLEGRow fuenteInformacionLegRow = null;
            DseAvaluoMantInf.FEXAVA_ESCRITURARow escrituraRow = null;
            DseAvaluoMantInf.FEXAVA_SENTENCIARow sentenciaRow = null;
            DseAvaluoMantInf.FEXAVA_CONTRATOPRIVADORow contratoPrivadoRow = null;
            DseAvaluoMantInf.FEXAVA_ALINEAMIENTONUMOFIRow alineamientoNumOfiRow = null;

            #region Fotos Microlocalización y Macrolocalización
            query = XmlUtils.XmlSearchById(terreno, "d.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DTRAMOCALLES = query.ToStringXElement();

            ////Almacenar fotos Microsoft y macro
            query = XmlUtils.XmlSearchById(terreno, "d.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DMICRO = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(terreno, "d.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DMACRO = query.ToStringXElement();
            #endregion

            #region Fuente Información Legal

            #region d.4.1.1 - Escritura

            query = XmlUtils.XmlSearchById(terreno, "d.4.1.1");
            if (query.IsFull())
            {
                fuenteInformacionLegRow = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                fuenteInformacionLegRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                fuenteInformacionLegRow.CODTIPOFUENTEINFORMACION = Convert.ToDecimal(Constantes.PAR_COD_TIPOFUENTEINFO_ESCRITURA);

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.1.3");
                if (queryn.IsFull())
                    fuenteInformacionLegRow.FECHA = (queryn.First().Value.To<DateTime>()).Date;


                escrituraRow = dseAvaluo.FEXAVA_ESCRITURA.NewFEXAVA_ESCRITURARow();
                escrituraRow.FEXAVA_FUENTEINFORMACIONLEGRow = fuenteInformacionLegRow;

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.1.1");
                if (queryn.IsFull())
                    escrituraRow.NUMESCRITURA = XmlUtils.ToDecimalXElementAv(queryn);

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.1.2");
                if (queryn.IsFull())
                    escrituraRow.NUMVOLUMEN = queryn.ToStringXElement();

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.1.4");
                if (queryn.IsFull())
                    escrituraRow.NUMNOTARIO = XmlUtils.ToDecimalXElementAv(queryn);

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.1.5");
                if (queryn.IsFull())
                    escrituraRow.NOMBRENOTARIO = queryn.ToStringXElement();

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.1.6");
                if (queryn.IsFull())
                    escrituraRow.DISTRITOJUDICIALNOTARIO = queryn.ToStringXElement();
            }
            #endregion

            #region d.4.1.2 - Sentencia

            query = XmlUtils.XmlSearchById(terreno, "d.4.1.2");
            if (query.IsFull())
            {
                fuenteInformacionLegRow = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                fuenteInformacionLegRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                fuenteInformacionLegRow.CODTIPOFUENTEINFORMACION = Convert.ToDecimal(Constantes.PAR_COD_TIPOFUENTEINFO_SENTENCIA);

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.2.2");
                if (queryn.IsFull())
                    fuenteInformacionLegRow.FECHA = (queryn.First().Value.To<DateTime>()).Date;


                sentenciaRow = dseAvaluo.FEXAVA_SENTENCIA.NewFEXAVA_SENTENCIARow();
                sentenciaRow.FEXAVA_FUENTEINFORMACIONLEGRow = fuenteInformacionLegRow;

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.2.1");
                if (queryn.IsFull())
                    sentenciaRow.JUZGADO = queryn.ToStringXElement();

                queryn = XmlUtils.XmlSearchById(query, "d.4.1.2.3");
                if (queryn.IsFull())
                    sentenciaRow.NUMEXPEDIENTE = queryn.ToStringXElement();
            }

            #endregion

            #region d.4.1.3 - Contrato privado

            query = XmlUtils.XmlSearchById(terreno, "d.4.1.3");
            if (query.IsFull())
            {
                fuenteInformacionLegRow = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                fuenteInformacionLegRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                fuenteInformacionLegRow.CODTIPOFUENTEINFORMACION = Convert.ToDecimal(Constantes.PAR_COD_TIPOFUENTEINFO_CONTRATO);

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.1");
                if (query.IsFull())
                    fuenteInformacionLegRow.FECHA = (query.First().Value.To<DateTime>()).Date;


                contratoPrivadoRow = dseAvaluo.FEXAVA_CONTRATOPRIVADO.NewFEXAVA_CONTRATOPRIVADORow();
                contratoPrivadoRow.FEXAVA_FUENTEINFORMACIONLEGRow = fuenteInformacionLegRow;
                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.2");
                if (query.IsFull())
                    contratoPrivadoRow.NOMBREADQUIRIENTE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.3");
                if (query.IsFull())
                    contratoPrivadoRow.APELLIDOPATERNOADQUIRIENTE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.4");
                if (query.IsFull())
                    contratoPrivadoRow.APELLIDOMATERNOADQUIRIENTE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.5");
                if (query.IsFull())
                    contratoPrivadoRow.NOMBREENAJENANTE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.6");
                if (query.IsFull())
                    contratoPrivadoRow.APELLIDOPATERNOENAJENANTE = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.3.7");
                if (query.IsFull())
                    contratoPrivadoRow.APELLIDOMATERNOENAJENANTE = query.ToStringXElement();
            }

            #endregion

            #region d.4.1.4 - Alineamiento y número oficial

            query = XmlUtils.XmlSearchById(terreno, "d.4.1.4");
            if (query.IsFull())
            {
                fuenteInformacionLegRow = dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.NewFEXAVA_FUENTEINFORMACIONLEGRow();
                fuenteInformacionLegRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                fuenteInformacionLegRow.CODTIPOFUENTEINFORMACION = Convert.ToDecimal(Constantes.PAR_COD_TIPOFUENTEINFO_ALINEAMIENTO);

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.4.1");
                if (query.IsFull())
                    fuenteInformacionLegRow.FECHA = (query.First().Value.To<DateTime>()).Date;


                alineamientoNumOfiRow = dseAvaluo.FEXAVA_ALINEAMIENTONUMOFI.NewFEXAVA_ALINEAMIENTONUMOFIRow();
                alineamientoNumOfiRow.FEXAVA_FUENTEINFORMACIONLEGRow = fuenteInformacionLegRow;

                query = XmlUtils.XmlSearchById(terreno, "d.4.1.4.2");
                if (query.IsFull())
                    alineamientoNumOfiRow.NUMFOLIO = query.ToStringXElement();
            }

            #endregion

            if (fuenteInformacionLegRow != null)
            {
                dseAvaluo.FEXAVA_FUENTEINFORMACIONLEG.AddFEXAVA_FUENTEINFORMACIONLEGRow(fuenteInformacionLegRow);
                if (escrituraRow != null)
                    dseAvaluo.FEXAVA_ESCRITURA.AddFEXAVA_ESCRITURARow(escrituraRow);
                else if (sentenciaRow != null)
                    dseAvaluo.FEXAVA_SENTENCIA.AddFEXAVA_SENTENCIARow(sentenciaRow);
                else if (contratoPrivadoRow != null)
                    dseAvaluo.FEXAVA_CONTRATOPRIVADO.AddFEXAVA_CONTRATOPRIVADORow(contratoPrivadoRow);
                else if (alineamientoNumOfiRow != null)
                    dseAvaluo.FEXAVA_ALINEAMIENTONUMOFI.AddFEXAVA_ALINEAMIENTONUMOFIRow(alineamientoNumOfiRow);
            }
            #endregion

            #region d.4.2- Colindancias
            query = XmlUtils.XmlSearchById(terreno, "d.4.2");
            if (query.IsFull())
            {
                int idCol = 0;
                foreach (XElement cursor in query)
                {
                    DseAvaluoMantInf.FEXAVA_COLINDANCIASRow ColindanciaRow = dseAvaluo.FEXAVA_COLINDANCIAS.NewFEXAVA_COLINDANCIASRow();
                    ColindanciaRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                    //queryn = XmlUtils.XmlSearchById(cursor, "d.4.2.1");
                    //if (queryn.IsFull())
                    //    ColindanciaRow.ELEMENTODESCRITO = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "d.4.2.n.2");
                    if (queryn.IsFull())
                        ColindanciaRow.ORIENTACION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "d.4.2.n.3");
                    if (queryn.IsFull())
                        ColindanciaRow.MEDIDA = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.4.2.n.4");
                    if (queryn.IsFull())
                        ColindanciaRow.DESCRIPCION = queryn.ToStringXElement();

                    idCol++;
                    ColindanciaRow.IDCOLINDANCIA = idCol;

                    dseAvaluo.FEXAVA_COLINDANCIAS.AddFEXAVA_COLINDANCIASRow(ColindanciaRow);
                }
            }
            #endregion
            //EMC
            #region d.5 - Superficie del terreno.  Privativas
            query = XmlUtils.XmlSearchById(terreno, "d.5");
            if (query.IsFull() && !XmlUtils.XmlSearchById(terreno, "d.5.1").IsFull())
            {
                foreach (XElement cursor in query)
                {
                    DseAvaluoMantInf.FEXAVA_SUPERFICIERow SuperficieRow = dseAvaluo.FEXAVA_SUPERFICIE.NewFEXAVA_SUPERFICIERow();
                    SuperficieRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.1");
                    if (queryn.IsFull())
                        SuperficieRow.IDENTIFICADORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.2");
                    if (queryn.IsFull())
                        SuperficieRow.SUPERFICIEFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.3");
                    if (queryn.IsFull())
                        SuperficieRow.FZO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.4");
                    if (queryn.IsFull())
                        SuperficieRow.FUB = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.5");
                    if (queryn.IsFull())
                        SuperficieRow.FFR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.6");
                    if (queryn.IsFull())
                        SuperficieRow.FFO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.7");
                    if (queryn.IsFull())
                        SuperficieRow.FSU = XmlUtils.ToDecimalXElementAv(queryn);

                    // JACM Se da de baja el campo 2021-02-04
                    //queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.9.1");
                    //if (queryn.IsFull())
                    //    SuperficieRow.FOTVALOR = XmlUtils.ToDecimalXElementAv(queryn);

                    //queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.9.2");
                    //if (queryn.IsFull())
                    //    SuperficieRow.FOTDESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.8");
                    if (queryn.IsFull())
                        SuperficieRow.CLAVE = queryn.ToStringXElement();

                    if (esComercial)
                    {
                        queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.10");
                        if (queryn.IsFull())
                            SuperficieRow.FRE = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    else { SuperficieRow.FRE = 0; }
                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.11");
                    if (queryn.IsFull())
                        SuperficieRow.VALORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.n.12");
                    if (queryn.IsFull())
                        SuperficieRow.VALORCATASTRALFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    dseAvaluo.FEXAVA_SUPERFICIE.AddFEXAVA_SUPERFICIERow(SuperficieRow);
                }
            }
            #endregion
            //EMC
            #region d.5 - Superficie del terreno.  Privativas
            query = XmlUtils.XmlSearchById(terreno, "d.5.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    DseAvaluoMantInf.FEXAVA_SUPERFICIERow SuperficieRow = dseAvaluo.FEXAVA_SUPERFICIE.NewFEXAVA_SUPERFICIERow();
                    SuperficieRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.1");
                    if (queryn.IsFull())
                        SuperficieRow.IDENTIFICADORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.2");
                    if (queryn.IsFull())
                        SuperficieRow.SUPERFICIEFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.3");
                    if (queryn.IsFull())
                        SuperficieRow.FZO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.4");
                    if (queryn.IsFull())
                        SuperficieRow.FUB = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.5");
                    if (queryn.IsFull())
                        SuperficieRow.FFR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.6");
                    if (queryn.IsFull())
                        SuperficieRow.FFO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.7");
                    if (queryn.IsFull())
                        SuperficieRow.FSU = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.9.1");
                    if (queryn.IsFull())
                        SuperficieRow.FOTVALOR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.9.2");
                    if (queryn.IsFull())
                        SuperficieRow.FOTDESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.8");
                    if (queryn.IsFull())
                        SuperficieRow.CLAVE = queryn.ToStringXElement();


                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.10");
                    if (queryn.IsFull())
                        SuperficieRow.FRE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.11");
                    if (queryn.IsFull())
                        SuperficieRow.VALORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.1.n.12");
                    if (queryn.IsFull())
                        SuperficieRow.VALORCATASTRALFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    SuperficieRow.CODTIPO = Constantes.CODTIPOCONSTRUCCION_PRIVATIVA;
                    dseAvaluo.FEXAVA_SUPERFICIE.AddFEXAVA_SUPERFICIERow(SuperficieRow);
                }
            }
            #endregion
            //EMC
            #region d.5 - Superficie del terreno.  Comunes
            query = XmlUtils.XmlSearchById(terreno, "d.5.2");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    DseAvaluoMantInf.FEXAVA_SUPERFICIERow SuperficieRow = dseAvaluo.FEXAVA_SUPERFICIE.NewFEXAVA_SUPERFICIERow();
                    SuperficieRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.1");
                    if (queryn.IsFull())
                        SuperficieRow.IDENTIFICADORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.2");
                    if (queryn.IsFull())
                        SuperficieRow.SUPERFICIEFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.3");
                    if (queryn.IsFull())
                        SuperficieRow.FZO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.4");
                    if (queryn.IsFull())
                        SuperficieRow.FUB = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.5");
                    if (queryn.IsFull())
                        SuperficieRow.FFR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.6");
                    if (queryn.IsFull())
                        SuperficieRow.FFO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.7");
                    if (queryn.IsFull())
                        SuperficieRow.FSU = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.9.1");
                    if (queryn.IsFull())
                        SuperficieRow.FOTVALOR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.9.2");
                    if (queryn.IsFull())
                        SuperficieRow.FOTDESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.8");
                    if (queryn.IsFull())
                        SuperficieRow.CLAVE = queryn.ToStringXElement();


                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.10");
                    if (queryn.IsFull())
                        SuperficieRow.FRE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.11");
                    if (queryn.IsFull())
                        SuperficieRow.VALORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "d.5.2.n.12");
                    if (queryn.IsFull())
                        SuperficieRow.VALORCATASTRALFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    SuperficieRow.CODTIPO = Constantes.CODTIPOCONSTRUCCION_COMUN;
                    dseAvaluo.FEXAVA_SUPERFICIE.AddFEXAVA_SUPERFICIERow(SuperficieRow);
                }
            }
            #endregion
            query = XmlUtils.XmlSearchById(terreno, "d.6");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TEINDIVISO = XmlUtils.ToDecimalXElementAv(query);//Es un porcentaje indicado entre 0 y 1 en el xml pero que se debe mostrar entre 0 y 100 en el report

            query = XmlUtils.XmlSearchById(terreno, "d.7");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].CUCODTOPOGRAFIA = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(terreno, "d.8");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TECARACTERISTICASPARONAMICAS = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(terreno, "d.9");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TECODDENSIDADHABITACIONAL = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(terreno, "d.10");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].TESERVIDUMBRESORESTRICCIONES = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(terreno, "d.13");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORTOTALDELTERRENOPROP = XmlUtils.ToDecimalXElementAv(query);
        }

        /// <summary>
        /// Inserta los datos referentes a los elementos de construcción en el dseAvaluos desde el
        /// elemento xml.
        /// </summary>
        /// <param name="elementosConstruccion">Elemento xml con los datos de los elementos de
        /// construccion.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoElementosConstruccionAI(XElement elementosConstruccion, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;
            IEnumerable<XElement> queryn = null;

            DseAvaluoMantInf.FEXAVA_ELEMENTOSCONSTRow elementosConstruccionRow = null;
            DseAvaluoMantInf.FEXAVA_OBRANEGRARow obraNegraRow = null;
            DseAvaluoMantInf.FEXAVA_REVESTIMIENTOACABADORow revestimientoAcabadoRow = null;
            DseAvaluoMantInf.FEXAVA_CARPINTERIARow carpinteriaRow = null;
            DseAvaluoMantInf.FEXAVA_INSTALACIONHIDSANRow instalacionHidraulicaSanitariaRow = null;
            DseAvaluoMantInf.FEXAVA_PUERTASYVENTANERIARow puertasYVentaneriaRow = null;
            DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow elementosExtraRow = null;

            #region f.1 - Obra Negra

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1");
            if (query.Descendants().IsFull())
            {
                obraNegraRow = dseAvaluo.FEXAVA_OBRANEGRA.NewFEXAVA_OBRANEGRARow();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.1");
                if (query.IsFull())
                    obraNegraRow.CIMENTACION = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.2");
                if (query.IsFull())
                    obraNegraRow.ESTRUCTURA = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.3");
                if (query.IsFull())
                    obraNegraRow.MUROS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.4");
                if (query.IsFull())
                    obraNegraRow.ENTREPISOS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.5");
                if (query.IsFull())
                    obraNegraRow.TECHOS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.6");
                if (query.IsFull())
                    obraNegraRow.AZOTEAS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.1.7");
                if (query.IsFull())
                    obraNegraRow.BARDAS = query.ToStringXElement();
            }
            #endregion

            #region f.2 - Revestimientos y acabados interiores

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2");
            if (query.Descendants().IsFull())
            {
                revestimientoAcabadoRow = dseAvaluo.FEXAVA_REVESTIMIENTOACABADO.NewFEXAVA_REVESTIMIENTOACABADORow();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.1");
                if (query.IsFull())
                    revestimientoAcabadoRow.APLANADOS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.2");
                if (query.IsFull())
                    revestimientoAcabadoRow.PLAFONES = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.3");
                if (query.IsFull())
                    revestimientoAcabadoRow.LAMBRINES = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.4");
                if (query.IsFull())
                    revestimientoAcabadoRow.PISOS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.5");
                if (query.IsFull())
                    revestimientoAcabadoRow.ZOCLOS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.6");
                if (query.IsFull())
                    revestimientoAcabadoRow.ESCALERAS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.7");
                if (query.IsFull())
                    revestimientoAcabadoRow.PINTURA = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.2.8");
                if (query.IsFull())
                    revestimientoAcabadoRow.RECUBRIMIENTOSESPECIALES = query.ToStringXElement();
            }
            #endregion

            #region f.3 - Carpintería

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.3");
            if (query.Descendants().IsFull())
            {
                carpinteriaRow = dseAvaluo.FEXAVA_CARPINTERIA.NewFEXAVA_CARPINTERIARow();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.1");
                if (query.IsFull())
                    carpinteriaRow.PUERTASINTERIORES = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.2");
                if (query.IsFull())
                    carpinteriaRow.GUARDAROPAS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.3.3");
                if (query.IsFull())
                    carpinteriaRow.MUEBLESEMPOTRADOSFIJOS = query.ToStringXElement();
            }
            #endregion

            #region f.4 - Instalaciones hidráulicas y sanitrias

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.4");
            if (query.Descendants().IsFull())
            {
                instalacionHidraulicaSanitariaRow = dseAvaluo.FEXAVA_INSTALACIONHIDSAN.NewFEXAVA_INSTALACIONHIDSANRow();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.1");
                if (query.IsFull())
                    instalacionHidraulicaSanitariaRow.MUEBLESBANO = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.2");
                if (query.IsFull())
                    instalacionHidraulicaSanitariaRow.RAMALEOSHIDRAULICOS = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.4.3");
                if (query.IsFull())
                    instalacionHidraulicaSanitariaRow.RAMALEOSSANITARIOS = query.ToStringXElement();
            }
            #endregion
            #region f.16 - Instalaciones Especiales y alumbrado

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.16");
            if (query.IsFull())
            {
                dseAvaluo.FEXAVA_AVALUO[0].IEYALUMBRADO = query.ToStringXElement();
            }
            #endregion
            #region f.5 - Puertas y ventanería metálica

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.5");
            if (query.Descendants().IsFull())
            {
                puertasYVentaneriaRow = dseAvaluo.FEXAVA_PUERTASYVENTANERIA.NewFEXAVA_PUERTASYVENTANERIARow();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.5.1");
                if (query.IsFull())
                    puertasYVentaneriaRow.HERRERIA = query.ToStringXElement();

                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.5.2");
                if (query.IsFull())
                    puertasYVentaneriaRow.VENTANERIA = query.ToStringXElement();
            }

            #endregion

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.6");
            if (query.IsFull())
            {
                if (elementosConstruccionRow == null)
                    elementosConstruccionRow = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                elementosConstruccionRow.VIDRERIA = query.ToStringXElement();
            }

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.7");
            if (query.IsFull())
            {
                if (elementosConstruccionRow == null)
                    elementosConstruccionRow = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                elementosConstruccionRow.CERRAJERIA = query.ToStringXElement();
            }

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.8");
            if (query.IsFull())
            {
                if (elementosConstruccionRow == null)
                    elementosConstruccionRow = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                elementosConstruccionRow.FACHADAS = query.ToStringXElement();
            }

            #region f.9.1 - Instalaciones Especiales. Privativas

            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.9.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.1");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string claveInstEsp = queryn.ToStringXElement();
                        decimal codInstEsp = CatastralUtils.ObtenerInstEspecialByClave(claveInstEsp).CODINSTESPECIALES;
                        elementosExtraRow.CODINSTALACIONESESPECIALES = codInstEsp;
                        elementosExtraRow.ClaveInstEsp = claveInstEsp;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.2");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.DESCRIPCION = queryn.ToStringXElement();
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.3");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = queryn.ToStringXElement();
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.4");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.CANTIDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.5");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.6");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VIDAUTIL = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.7");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.1.n.9");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.IMPORTE = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    if (elementosExtraRow != null)
                    {
                        elementosExtraRow.CODTIPO = Constantes.CODTIPO_ELEMEXTRA_PRIVATIVA;
                        elementosExtraRow.CODTIPOELEMENTO = Constantes.CODTIPO_ELEMEXTRA_INSTALACIONESPECIAL;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(elementosExtraRow);
                        elementosExtraRow = null;
                    }
                }
            }
            #endregion

            #region f.9.2 - Instalaciones Especiales. Comunes
            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.9.2");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.1");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string claveInstEsp = queryn.ToStringXElement();
                        decimal codInstEsp = CatastralUtils.ObtenerInstEspecialByClave(claveInstEsp).CODINSTESPECIALES;
                        elementosExtraRow.CODINSTALACIONESESPECIALES = codInstEsp;
                        elementosExtraRow.ClaveInstEsp = claveInstEsp;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.2");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.DESCRIPCION = queryn.ToStringXElement();
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.3");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = queryn.ToStringXElement();
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.4");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.CANTIDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.5");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.6");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VIDAUTIL = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.7");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.9");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.IMPORTE = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    queryn = XmlUtils.XmlSearchById(cursor, "f.9.2.n.10");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = Truncate(queryn.ToDecimalXElement(), 4);
                    }


                    if (elementosExtraRow != null)
                    {
                        elementosExtraRow.CODTIPO = Constantes.CODTIPO_ELEMEXTRA_COMUN;
                        elementosExtraRow.CODTIPOELEMENTO = Constantes.CODTIPO_ELEMEXTRA_INSTALACIONESPECIAL;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(elementosExtraRow);
                        elementosExtraRow = null;
                    }
                }
            }
            #endregion

            #region f.10.1 - Elementos Accesorios. Privativas
            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.10.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.1");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string claveInstEsp = queryn.ToStringXElement();
                        decimal codInstEsp = CatastralUtils.ObtenerInstEspecialByClave(claveInstEsp).CODINSTESPECIALES;
                        elementosExtraRow.CODINSTALACIONESESPECIALES = codInstEsp;
                        elementosExtraRow.ClaveInstEsp = claveInstEsp;


                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.2");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.DESCRIPCION = queryn.ToStringXElement();
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.3");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.4");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.CANTIDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.5");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.6");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VIDAUTIL = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.7");

                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.1.n.9");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.IMPORTE = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    if (elementosExtraRow != null)
                    {
                        elementosExtraRow.CODTIPO = Constantes.CODTIPO_ELEMEXTRA_PRIVATIVA;
                        elementosExtraRow.CODTIPOELEMENTO = Constantes.CODTIPO_ELEMEXTRA_ELEMENTOSACCESORIOS;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(elementosExtraRow);
                        elementosExtraRow = null;
                    }
                }
            }
            #endregion

            #region f.10.2 - Elementos Accesorios. Comunes
            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.10.2");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.1");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string claveInstEsp = queryn.ToStringXElement();
                        decimal codInstEsp = CatastralUtils.ObtenerInstEspecialByClave(claveInstEsp).CODINSTESPECIALES;
                        elementosExtraRow.CODINSTALACIONESESPECIALES = codInstEsp;
                        elementosExtraRow.ClaveInstEsp = claveInstEsp;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.2");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.DESCRIPCION = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.3");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.4");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.CANTIDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.5");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.6");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VIDAUTIL = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.7");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.9");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.IMPORTE = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.10.2.n.10");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD =  Truncate(queryn.ToDecimalXElement(), 4);
                    }

                    if (elementosExtraRow != null)
                    {
                        elementosExtraRow.CODTIPO = Constantes.CODTIPO_ELEMEXTRA_COMUN;
                        elementosExtraRow.CODTIPOELEMENTO = Constantes.CODTIPO_ELEMEXTRA_ELEMENTOSACCESORIOS;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(elementosExtraRow);
                        elementosExtraRow = null;
                    }
                }
            }
            #endregion

            #region f.11.1 - Obras Complementarias. Privativas
            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.11.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.1");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string claveInstEsp = queryn.ToStringXElement();
                        decimal codInstEsp = CatastralUtils.ObtenerInstEspecialByClave(claveInstEsp).CODINSTESPECIALES;
                        elementosExtraRow.CODINSTALACIONESESPECIALES = codInstEsp;
                        elementosExtraRow.ClaveInstEsp = claveInstEsp;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.2");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.DESCRIPCION = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.3");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.4");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.CANTIDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.5");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.6");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VIDAUTIL = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.7");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.1.n.9");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.IMPORTE = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    if (elementosExtraRow != null)
                    {
                        elementosExtraRow.CODTIPO = Constantes.CODTIPO_ELEMEXTRA_PRIVATIVA;
                        elementosExtraRow.CODTIPOELEMENTO = Constantes.CODTIPO_ELEMEXTRA_OBRACOMPLEMENTARIA;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(elementosExtraRow);
                        elementosExtraRow = null;
                    }
                }
            }
            #endregion

            #region f.11.2 - Obras Complementarias. Comunes
            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.11.2");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.1");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        string claveInstEsp = queryn.ToStringXElement();
                        decimal codInstEsp = CatastralUtils.ObtenerInstEspecialByClave(claveInstEsp).CODINSTESPECIALES;
                        elementosExtraRow.CODINSTALACIONESESPECIALES = codInstEsp;
                        elementosExtraRow.ClaveInstEsp = claveInstEsp;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.2");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.DESCRIPCION = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.3");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = queryn.ToStringXElement();
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.4");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.CANTIDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.5");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.6");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VIDAUTIL = XmlUtils.ToDecimalXElementAv(queryn);
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.7");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.VALORUNITARIO = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.9");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.IMPORTE = XmlUtils.ToDecimalXElementAv(queryn);
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "f.11.2.n.10");
                    if (queryn.IsFull())
                    {
                        if (elementosExtraRow == null)
                            elementosExtraRow = dseAvaluo.FEXAVA_ELEMENTOSEXTRA.NewFEXAVA_ELEMENTOSEXTRARow();
                        elementosExtraRow.UNIDAD = Truncate( queryn.ToDecimalXElement(), 4);
                    }

                    if (elementosExtraRow != null)
                    {
                        elementosExtraRow.CODTIPO = Constantes.CODTIPO_ELEMEXTRA_COMUN;
                        elementosExtraRow.CODTIPOELEMENTO = Constantes.CODTIPO_ELEMEXTRA_OBRACOMPLEMENTARIA;
                        dseAvaluo.FEXAVA_ELEMENTOSEXTRA.AddFEXAVA_ELEMENTOSEXTRARow(elementosExtraRow);
                        elementosExtraRow = null;
                    }
                }
            }
            #endregion

            #region total
            query = XmlUtils.XmlSearchById(elementosConstruccion, "f.14");

            if (query.IsFull())
            {
                decimal valor14 = XmlUtils.ToDecimalXElementAv(query);
                query = XmlUtils.XmlSearchById(elementosConstruccion, "f.15");
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].VALORTOTALIEEAOC = valor14 + XmlUtils.ToDecimalXElementAv(query);
            }




            #endregion

            // Las FK de las tablas estan relacionadas con la tabla ElementosConstruccion.                                    
            if (elementosConstruccionRow != null ||
                obraNegraRow != null ||
                revestimientoAcabadoRow != null ||
                carpinteriaRow != null ||
                instalacionHidraulicaSanitariaRow != null ||
                puertasYVentaneriaRow != null ||
                dseAvaluo.FEXAVA_ELEMENTOSEXTRA.Any())
            {
                if (elementosConstruccionRow == null)
                    elementosConstruccionRow = dseAvaluo.FEXAVA_ELEMENTOSCONST.NewFEXAVA_ELEMENTOSCONSTRow();
                elementosConstruccionRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];
                dseAvaluo.FEXAVA_ELEMENTOSCONST.AddFEXAVA_ELEMENTOSCONSTRow(elementosConstruccionRow);
            }
            if (obraNegraRow != null)
            {
                obraNegraRow.FEXAVA_ELEMENTOSCONSTRow = elementosConstruccionRow;
                dseAvaluo.FEXAVA_OBRANEGRA.AddFEXAVA_OBRANEGRARow(obraNegraRow);
            }
            if (revestimientoAcabadoRow != null)
            {
                revestimientoAcabadoRow.FEXAVA_ELEMENTOSCONSTRow = elementosConstruccionRow;
                dseAvaluo.FEXAVA_REVESTIMIENTOACABADO.AddFEXAVA_REVESTIMIENTOACABADORow(revestimientoAcabadoRow);
            }
            if (carpinteriaRow != null)
            {
                carpinteriaRow.FEXAVA_ELEMENTOSCONSTRow = elementosConstruccionRow;
                dseAvaluo.FEXAVA_CARPINTERIA.AddFEXAVA_CARPINTERIARow(carpinteriaRow);
            }
            if (instalacionHidraulicaSanitariaRow != null)
            {
                instalacionHidraulicaSanitariaRow.FEXAVA_ELEMENTOSCONSTRow = elementosConstruccionRow;
                dseAvaluo.FEXAVA_INSTALACIONHIDSAN.AddFEXAVA_INSTALACIONHIDSANRow(instalacionHidraulicaSanitariaRow);
            }
            if (puertasYVentaneriaRow != null)
            {
                puertasYVentaneriaRow.FEXAVA_ELEMENTOSCONSTRow = elementosConstruccionRow;
                dseAvaluo.FEXAVA_PUERTASYVENTANERIA.AddFEXAVA_PUERTASYVENTANERIARow(puertasYVentaneriaRow);
            }
            if (dseAvaluo.FEXAVA_ELEMENTOSEXTRA.Any())
            {
                foreach (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow row in dseAvaluo.FEXAVA_ELEMENTOSEXTRA)
                    row.FEXAVA_ELEMENTOSCONSTRow = elementosConstruccionRow;
            }
        }

        /// <summary>
        /// Inserta los datos referentes a la descricpión del inmueble en el dseAvaluos desde el elemento
        /// xml.
        /// </summary>
        /// <param name="descripcionInmueble">Elemento xml con los datos de la descripción del inmueble.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>   
        private void GuardarAvaluoDescripcionImuebleAI(XElement descripcionInmueble, ref DseAvaluoMantInf dseAvaluo, bool esComercial)
        {
            DateTime fechaAvaluo = Convert.ToDateTime(dseAvaluo.FEXAVA_AVALUO[0].FECHAAVALUO);
            IEnumerable<XElement> query = null;
            IEnumerable<XElement> queryn = null;

            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.1");

            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIUSOACTUAL = query.ToStringXElement();

            #region e.3 - Vida útil promedio
            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.3");

            if (query.IsFull())
            {
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].DIVIDAUTILPONDERADA = XmlUtils.ToDecimalXElementAv(query);

            }
            #endregion

            #region e.4 - Edad promedio
            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.4");

            if (query.IsFull())
            {
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].DIEDADPONDERADA = XmlUtils.ToDecimalXElementAv(query); ;

            }
            #endregion

            #region e.4 - Vida útil remanente promedio
            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.5");

            if (query.IsFull())
            {
                if (query.IsFull())
                    dseAvaluo.FEXAVA_AVALUO[0].DIVIDAUTILREMANENTEPONDERADA = XmlUtils.ToDecimalXElementAv(query);

            }
            #endregion


            #region construcciones

            #region e.2.1 - Construcciones Privativas

            DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow construccionesRow = null;
            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    construccionesRow = dseAvaluo.FEXAVA_CONSTRUCCIONES.NewFEXAVA_CONSTRUCCIONESRow();
                    construccionesRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.1");
                    if (queryn.IsFull())
                    {
                        construccionesRow.DESCRIPCION = queryn.ToStringXElement();
                    }

                    string codUso = string.Empty;
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.2");
                    if (queryn.IsFull())
                    {
                        codUso = queryn.ToStringXElement();
                        construccionesRow.CODUSOSCONSTRUCCIONES = codUso;

                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.3");
                    if (queryn.IsFull())
                        construccionesRow.NUMNIVELES = XmlUtils.ToDecimalXElementAv(queryn);


                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.4");
                    if (queryn.IsFull())
                    {
                        string codRangoNiveles = queryn.ToStringXElement();
                        int idRangoNivelesEjercicio = FiscalUtils.SolicitarObtenerIdRangoNivelesByCodeAndAno(fechaAvaluo, codRangoNiveles);
                        construccionesRow.CODRANGONIVELES = idRangoNivelesEjercicio; ;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.5");
                    if (queryn.IsFull())
                        construccionesRow.PUNTAJECLASIFICACION = XmlUtils.ToDecimalXElementAv(queryn);

                    string codClase = string.Empty;
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.6");
                    if (queryn.IsFull())
                    {
                        codClase = queryn.ToStringXElement();
                        int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaAvaluo, codClase);

                        construccionesRow.CODCLASESCONSTRUCCION = idClaseEjercicio;
                    }


                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.7");
                    if (queryn.IsFull())
                        construccionesRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.8");
                    if (queryn.IsFull())
                    {

                        int idUsoEjercicio = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(fechaAvaluo.Date, codUso);
                        int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaAvaluo.Date, codClase);
                        if (codClase != "U")// •	En el caso de clase única (U), no se debe validar el campo e.2.1.n.8 - Vida útil total del tipo y por tanto no existe la relación clase uso en la tabla fexava_claseuso
                        {
                            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable dt = ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                            if (dt.Any())
                            {
                                construccionesRow.CODCLASESVIDAS = dt[0].IDUSOCLASEEJERCICIO;
                            }
                        }

                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.9");
                    if (queryn.IsFull())
                        construccionesRow.VIDAUTILREMANENTE = XmlUtils.ToDecimalXElementAv(queryn);

                    //queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.10");
                    //if (queryn.IsFull())
                    //    construccionesRow.CODESTADOCONSERVACION = XmlUtils.ToDecimalXElementAv(queryn);

                    //string stringXelement = xelements3.ToStringXElement();
                    log("GuardarAvaluoDescripcionImueble CODESTADOCONSERVACION ", "codUso : ", codUso);

                  
                    if (codUso.ToString() != "P" &&
                        codUso.ToString() != "PE" &&
                        codUso.ToString() != "PC" &&
                        codUso.ToString() != "J")
                    {
                        construccionesRow.CODESTADOCONSERVACION = 2M;
                    }
                    else { construccionesRow.CODESTADOCONSERVACION = 3M; }

                  
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.11");
                    if (queryn.IsFull())
                        construccionesRow.SUPERFICIE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.12");
                    if (queryn.IsFull())
                        construccionesRow.VALORUNITARIOREPOSICIONNUEVO = XmlUtils.ToDecimalXElementAv(queryn);

                    if (esComercial) { 
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.13");
                    if (queryn.IsFull())
                        construccionesRow.FED = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    else { construccionesRow.FED = 0; }
                    //queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.14");
                    //if (queryn.IsFull())
                    //    construccionesRow.FRE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.15");
                    if (queryn.IsFull())
                        construccionesRow.VALORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.16");
                    if (queryn.IsFull())
                        construccionesRow.VALORUNITARIOCAT = XmlUtils.ToDecimalXElementAv(queryn);

                    if (!esComercial) { 
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.1.n.17");
                    if (queryn.IsFull())
                        construccionesRow.DEPRECIACIONEDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    else { construccionesRow.DEPRECIACIONEDAD = 1; }

                    construccionesRow.CODTIPO = Constantes.CODTIPOCONSTRUCCION_PRIVATIVA;

                    dseAvaluo.FEXAVA_CONSTRUCCIONES.AddFEXAVA_CONSTRUCCIONESRow(construccionesRow);
                    construccionesRow = null;
                }
            }
            #endregion


            #region e.2.5 - Construcciones Comunes

            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.5");

            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    construccionesRow = dseAvaluo.FEXAVA_CONSTRUCCIONES.NewFEXAVA_CONSTRUCCIONESRow();
                    construccionesRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.1");
                    if (queryn.IsFull())
                    {
                        construccionesRow.DESCRIPCION = queryn.ToStringXElement();
                    }

                    string codUso = string.Empty;
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.2");
                    if (queryn.IsFull())
                    {
                        codUso = queryn.ToStringXElement();
                        construccionesRow.CODUSOSCONSTRUCCIONES = codUso;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.3");
                    if (queryn.IsFull())
                        construccionesRow.NUMNIVELES = XmlUtils.ToDecimalXElementAv(queryn);

                    string codRangoNiveles = string.Empty;
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.4");
                    if (queryn.IsFull())
                    {
                        codRangoNiveles = queryn.ToStringXElement();
                        int idRangoNivelesEjercicio = FiscalUtils.SolicitarObtenerIdRangoNivelesByCodeAndAno(fechaAvaluo, codRangoNiveles);
                        construccionesRow.CODRANGONIVELES = idRangoNivelesEjercicio; ;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.5");
                    if (queryn.IsFull())
                        construccionesRow.PUNTAJECLASIFICACION = XmlUtils.ToDecimalXElementAv(queryn);


                    string codClase = string.Empty;
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.6");
                    if (queryn.IsFull())
                    {
                        codClase = queryn.ToStringXElement();
                        int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaAvaluo, codClase);
                        construccionesRow.CODCLASESCONSTRUCCION = idClaseEjercicio;
                    }


                    //JACM Se da de baja el campo 2021-02-15
                    //queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.7");
                    //if (queryn.IsFull())
                    //    construccionesRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    if (!esComercial)
                    {
                        queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.7");
                        if (queryn.IsFull())
                            construccionesRow.EDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    else { construccionesRow.EDAD = 1; }


                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.8");
                    if (queryn.IsFull())
                    {
                        int idUsoEjercicio = FiscalUtils.SolicitarObtenerIdUsosByCodeAndAno(fechaAvaluo.Date, codUso);
                        int idClaseEjercicio = FiscalUtils.SolicitarObtenerIdClasesByCodeAndAno(fechaAvaluo.Date, codClase);
                        if (codClase != "U")// •	En el caso de clase única (U), no se debe validar el campo e.2.1.n.8 - Vida útil total del tipo y por tanto no existe la relación clase uso en la tabla fexava_claseuso
                        {
                            DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable dt = ObtenerClaseUsoByIdUsoIdClase(idUsoEjercicio, idClaseEjercicio);
                            if (dt.Any())
                            {
                                construccionesRow.CODCLASESVIDAS = dt[0].IDUSOCLASEEJERCICIO;
                            }
                        }
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.9");
                    if (queryn.IsFull())
                        construccionesRow.VIDAUTILREMANENTE = XmlUtils.ToDecimalXElementAv(queryn);


                    // JACM Se da de baja el campo 2021-02-04
                    //queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.10");
                    //if (queryn.IsFull())
                    //    construccionesRow.CODESTADOCONSERVACION = XmlUtils.ToDecimalXElementAv(queryn);
                    //construccionesRow.CODESTADOCONSERVACION = 0M;

                    log("GuardarAvaluoDescripcionImueble CODESTADOCONSERVACION ", "codUso : ", codUso);


                    if (codUso.ToString() != "P" &&
                        codUso.ToString() != "PE" &&
                        codUso.ToString() != "PC" &&
                        codUso.ToString() != "J")
                    {
                        construccionesRow.CODESTADOCONSERVACION = 2M;
                    }
                    else { construccionesRow.CODESTADOCONSERVACION = 3M; }

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.11");
                    if (queryn.IsFull())
                        construccionesRow.SUPERFICIE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.12");
                    if (queryn.IsFull())
                        construccionesRow.VALORUNITARIOREPOSICIONNUEVO = XmlUtils.ToDecimalXElementAv(queryn);

                    //queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.13");
                    //if (queryn.IsFull())
                        //construccionesRow.FED = XmlUtils.ToDecimalXElementAv(queryn);

                    // JACM Se da de baja el campo 2021-02-04
                    /*queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.14");
                    if (queryn.IsFull())
                        construccionesRow.FRE = XmlUtils.ToDecimalXElementAv(queryn);*/

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.15");
                    if (queryn.IsFull())
                        construccionesRow.VALORFRACCION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.16");
                    if (queryn.IsFull())
                        construccionesRow.VALORUNITARIOCAT = XmlUtils.ToDecimalXElementAv(queryn);

                    if (!esComercial) { 
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.17");
                    if (queryn.IsFull())
                        construccionesRow.DEPRECIACIONEDAD = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    else
                    {
                        construccionesRow.DEPRECIACIONEDAD = 1;
                    }
                    queryn = XmlUtils.XmlSearchById(cursor, "e.2.5.n.18");
                    if (queryn.IsFull())
                        construccionesRow.DataColumn1 = string.Format("{0}", XmlUtils.ToDecimalXElementAv(queryn));
                    else
                        construccionesRow.DataColumn1 = string.Empty;
                    construccionesRow.CODTIPO = construccionesRow.CODTIPO = Constantes.CODTIPOCONSTRUCCION_COMUN;

                    dseAvaluo.FEXAVA_CONSTRUCCIONES.AddFEXAVA_CONSTRUCCIONESRow(construccionesRow);
                    construccionesRow = null;
                }
            }
            #endregion


            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALTOTCONSTRUCCIONESPRIVATIVAS = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(descripcionInmueble, "e.2.7");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALTOTCONSTRUCCIONESCOMUNES = XmlUtils.ToDecimalXElementAv(query);

            #endregion

            query = XmlUtils.XmlSearchById(descripcionInmueble, "d.11");

            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIPORCENTAJESUPULTNIVEL = XmlUtils.ToDecimalXElementAv(query);
        }

        private static string TruncateAI(decimal number, int digits)
        {
            double conversionFactor = (Math.Pow(10.0, digits));
            return (Math.Truncate((double)number * conversionFactor) / conversionFactor).ToString();
        }

        /// <summary>
        /// Inserta los datos referentes al enfoque de mercado en el dseAvaluos desde el elemento xml.
        /// </summary>
        /// <param name="enfoqueMercado">Elemento xml con los datos de enfoque de mercado.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>       
        private void GuardarAvaluoEnfoqueMercadoAI(XElement enfoqueMercado, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;
            IEnumerable<XElement> queryn = null;

            DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow terrenoMercadoRow = null;
            DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow datosTerrenosRow = null;
            DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESMERRow construccionesMercadoRow = null;
            DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investigacionProductosComparablesRow = null;

            #region h.1.2 - Conclusiones homologación terrenos

            terrenoMercadoRow = dseAvaluo.FEXAVA_TERRENOMERCADO.NewFEXAVA_TERRENOMERCADORow();
            terrenoMercadoRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.1");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOTIERRAPROMEDIO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.2");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOTIERRAHOMOLOGADO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.3");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOSINHOMMIN = XmlUtils.ToDecimalXElementAv(query);


            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.4");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOSINHOMMAX = XmlUtils.ToDecimalXElementAv(query);


            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.5");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(query);


            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.2.6");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(query);


            terrenoMercadoRow.CODTIPOTERRENO = "D";
            dseAvaluo.FEXAVA_TERRENOMERCADO.AddFEXAVA_TERRENOMERCADORow(terrenoMercadoRow);

            terrenoMercadoRow = null;

            #endregion

            #region h.1.1 - Terrenos directos
            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    datosTerrenosRow = dseAvaluo.FEXAVA_DATOSTERRENOS.NewFEXAVA_DATOSTERRENOSRow();
                    datosTerrenosRow.FEXAVA_TERRENOMERCADORow = dseAvaluo.FEXAVA_TERRENOMERCADO[0];

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.1");
                    if (queryn.IsFull())
                        datosTerrenosRow.CALLE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.3");
                    if (queryn.IsFull())
                    {
                        string codDelegacion = queryn.ToStringXElement();
                        datosTerrenosRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(codDelegacion);
                        queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.2");
                        if (queryn.IsFull())
                        {
                            datosTerrenosRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion);
                            datosTerrenosRow[Constantes.COL_DESC_COLONIA] = queryn.ToStringXElement();
                            //  datosTerrenosRow[Constantes.COL_DESC_COLONIA] = CatastralUtils.ObtenerNombreColoniaPorIdColonia(datosTerrenosRow.IDCOLONIA);
                        }
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.4");
                    if (queryn.IsFull())
                        datosTerrenosRow.CODIGOPOSTAL = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.5.1");
                    if (queryn.IsFull())
                        datosTerrenosRow.TELEFONO = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.5.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.INFORMANTE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.6");
                    if (queryn.IsFull())
                        datosTerrenosRow.DESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.7");
                    if (queryn.IsFull())
                        datosTerrenosRow.USOSUELO = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.8");
                    if (queryn.IsFull())
                        datosTerrenosRow.CUS = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.9");
                    if (queryn.IsFull())
                        datosTerrenosRow.SUPERFICIE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.10.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.FZO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.11.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.FUB = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.12.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.FFR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.13.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.FFO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.14.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.FSU = XmlUtils.ToDecimalXElementAv(queryn);

                    // JACM Se da de baja el campo 2021-02-04
                    /*queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.18.1");
                    if (queryn.IsFull())
                        datosTerrenosRow.FOTVALOR = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.18.2");
                    if (queryn.IsFull())
                        datosTerrenosRow.FOTDESCRIPCION = queryn.ToStringXElement();*/

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.15");
                    if (queryn.IsFull())
                        datosTerrenosRow.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(queryn);

                    if (cursor.Descendants((XName)"Comercial").Count<XElement>() > 0)
                    {
                        queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.17");
                        if (queryn.IsFull())
                            datosTerrenosRow.FRE = XmlUtils.ToDecimalXElementAv(queryn);
                    }
                    else
                    {
                        datosTerrenosRow.FRE = 0;
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.1.n.16");
                    if (queryn.IsFull())
                        datosTerrenosRow.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(queryn);

                    datosTerrenosRow.CODTIPOTERRENO = "D";

                    dseAvaluo.FEXAVA_DATOSTERRENOS.AddFEXAVA_DATOSTERRENOSRow(datosTerrenosRow);
                    datosTerrenosRow = null;
                }
            }
            #endregion

            #region h.1.3.5 - Conclusiones homologación comp. Residuales

            terrenoMercadoRow = dseAvaluo.FEXAVA_TERRENOMERCADO.NewFEXAVA_TERRENOMERCADORow();
            terrenoMercadoRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.1");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOTIERRAPROMEDIO = XmlUtils.ToDecimalXElementAv(query); ;

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.2");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOTIERRAHOMOLOGADO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.3");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOSINHOMMIN = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.4");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOSINHOMMAX = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.5");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.6");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.5.7");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIOAPLICADORESIDUAL = XmlUtils.ToDecimalXElementAv(query);

            #endregion

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.1");
            if (query.IsFull())
                terrenoMercadoRow.TOTALINGRESOS = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.2");
            if (query.IsFull())
                terrenoMercadoRow.TOTALEGRESOS = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.3");
            if (query.IsFull())
                terrenoMercadoRow.UTILIDAD = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.6.4");
            if (query.IsFull())
                terrenoMercadoRow.VALORUNITARIORESIDUAL = XmlUtils.ToDecimalXElementAv(query);

            terrenoMercadoRow.CODTIPOTERRENO = Constantes.TIPO_TIPOTERRENO_RESIDUAL;

            #region h.1.3 - Terrenos residual



            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.1");
            if (query.IsFull())
                terrenoMercadoRow.TIPOINMPROPUESTO = query.ToStringXElement();

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.2");
            if (query.IsFull())
                terrenoMercadoRow.NUMUNIDADESVENDIBLE = XmlUtils.ToDecimalXElementAv(query); ;

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.3");
            if (query.IsFull())
                terrenoMercadoRow.SUPVENDIBLEPORUNIDAD = XmlUtils.ToDecimalXElementAv(query);


            dseAvaluo.FEXAVA_TERRENOMERCADO.AddFEXAVA_TERRENOMERCADORow(terrenoMercadoRow);
            #endregion

            #region h.1.3.4 - Investigación productos comparables
            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.3.4");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    datosTerrenosRow = dseAvaluo.FEXAVA_DATOSTERRENOS.NewFEXAVA_DATOSTERRENOSRow();
                    datosTerrenosRow.FEXAVA_TERRENOMERCADORow = dseAvaluo.FEXAVA_TERRENOMERCADO[1];

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.1");
                    if (queryn.IsFull())
                        datosTerrenosRow.CALLE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.3");
                    if (queryn.IsFull())
                    {
                        string codDelegacion = queryn.ToStringXElement();
                        datosTerrenosRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(codDelegacion);
                        queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.2");
                        if (queryn.IsFull())
                        {
                            datosTerrenosRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion);
                            datosTerrenosRow[Constantes.COL_DESC_COLONIA] = queryn.ToStringXElement();
                            //   datosTerrenosRow[Constantes.COL_DESC_COLONIA] = CatastralUtils.ObtenerNombreColoniaPorIdColonia(datosTerrenosRow.IDCOLONIA);
                        }
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.4");
                    if (queryn.IsFull())
                        datosTerrenosRow.CODIGOPOSTAL = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.5.1");
                    if (queryn.IsFull())
                        // investigacionProductosComparablesRow.TELEFONO = queryn.ToStringXElement();       
                        datosTerrenosRow.TELEFONO = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.5.2");
                    if (queryn.IsFull())
                        //investigacionProductosComparablesRow.INFORMANTE = queryn.ToStringXElement();
                        datosTerrenosRow.INFORMANTE = queryn.ToStringXElement();
                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.6");
                    if (queryn.IsFull())
                        // investigacionProductosComparablesRow.DESCRIPCION = queryn.ToStringXElement(); 
                        datosTerrenosRow.DESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.7");
                    if (queryn.IsFull())
                        //investigacionProductosComparablesRow.SUPERFICIEVENDIBLEPORUNIDAD = queryn.ToDecimalXElement();
                        datosTerrenosRow.SUPERFICIE = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.8");
                    if (queryn.IsFull())
                        //investigacionProductosComparablesRow.PRECIOSOLICITADO = queryn.ToDecimalXElement(); 
                        datosTerrenosRow.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.1.3.4.n.9");
                    if (queryn.IsFull())
                        datosTerrenosRow.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(queryn);


                    datosTerrenosRow.CODTIPOTERRENO = Constantes.TIPO_TIPOTERRENO_RESIDUAL;
                    dseAvaluo.FEXAVA_DATOSTERRENOS.AddFEXAVA_DATOSTERRENOSRow(datosTerrenosRow);
                    datosTerrenosRow = null;
                }
            }
            #endregion

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.1.4");
            if (query.IsFull())
            {
                //    foreach (DseAvaluoMantInf.FEXAVA_TERRENOMERCADORow row in dseAvaluo.FEXAVA_TERRENOMERCADO)
                dseAvaluo.FEXAVA_AVALUO[0].VALORUNITARIOTIERRAAVALUO = XmlUtils.ToDecimalXElementAv(query);
            }

            #region h.2.2 - Conclusiones homologación construcciones en venta

            construccionesMercadoRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER.NewFEXAVA_CONSTRUCCIONESMERRow();
            construccionesMercadoRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

            construccionesMercadoRow.IDMODOCONSTRUCCION = Constantes.PAR_COD_MODOCONSTRUCCION_VENTA;

            // System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-US", false);


            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.1");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOPROMEDIO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.2");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOHOMOLOGADO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.7");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOAPLICABLE = XmlUtils.ToDecimalXElementAv(query);


            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.3");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNISINHOMMIN = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.4");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNISINHOMMAX = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.5");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.2.6");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(query);

            dseAvaluo.FEXAVA_CONSTRUCCIONESMER.AddFEXAVA_CONSTRUCCIONESMERRow(construccionesMercadoRow);
            construccionesMercadoRow = null;

            #endregion

            #region h.2.1 - Investigación productos comparables
            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1");

            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    investigacionProductosComparablesRow = dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.NewFEXAVA_INVESTPRODUCTOSCOMPRow();
                    investigacionProductosComparablesRow.FEXAVA_CONSTRUCCIONESMERRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER[0];

                    investigacionProductosComparablesRow.CODTIPOCOMPARABLE = Constantes.PAR_COD_TIPOCOMPARABLE_VENTA;

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.1");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.CALLE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.3");
                    if (queryn.IsFull())
                    {
                        string codDelegacion = queryn.ToStringXElement();
                        investigacionProductosComparablesRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(codDelegacion);
                        queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.2");
                        if (queryn.IsFull())
                        {
                            investigacionProductosComparablesRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion);
                            investigacionProductosComparablesRow[Constantes.COL_DESC_COLONIA] = queryn.ToStringXElement();
                            // investigacionProductosComparablesRow[Constantes.COL_DESC_COLONIA] = CatastralUtils.ObtenerNombreColoniaPorIdColonia(investigacionProductosComparablesRow.IDCOLONIA);
                        }
                    }
                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.4");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.CODIGOPOSTAL = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.5.1");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.TELEFONO = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.5.2");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.INFORMANTE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.6");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.DESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.7");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.SUPERFICIEVENDIBLEPORUNIDAD = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.8");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(queryn);

                    //queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.9");
                    //if (queryn.IsFull())
                    //    investigacionProductosComparablesRow.SUPERFICIEVENDIBLEPORUNIDAD = queryn.ToDecimalXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.2.1.n.9");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10");
                    if (queryn.IsFull())
                    {
                        queryn = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.1");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.REGION = queryn.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.2");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.MANZANA = queryn.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.3");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.LOTE = queryn.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(enfoqueMercado, "h.2.1.n.10.4");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.UNIDADPRIVATIVA = queryn.ToStringXElement();
                    }
                    investigacionProductosComparablesRow.CODTIPOCOMPARABLE = Constantes.PAR_COD_MODOCONSTRUCCION_VENTA;

                    dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.AddFEXAVA_INVESTPRODUCTOSCOMPRow(investigacionProductosComparablesRow);
                    investigacionProductosComparablesRow = null;
                }
            }
            #endregion

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].DIVALORMERCADO = XmlUtils.ToDecimalXElementAv(query);

            #region h.4.2 - Conclusiones homologación construcciones en renta

            construccionesMercadoRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER.NewFEXAVA_CONSTRUCCIONESMERRow();
            construccionesMercadoRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];


            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.1");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOPROMEDIO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.2");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOHOMOLOGADO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.7");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOAPLICABLE = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.3");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNISINHOMMIN = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.4");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNISINHOMMAX = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.5");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOHOMMIN = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.2.6");
            if (query.IsFull())
                construccionesMercadoRow.VALORUNITARIOHOMMAX = XmlUtils.ToDecimalXElementAv(query);


            construccionesMercadoRow.IDMODOCONSTRUCCION = Constantes.PAR_COD_MODOCONSTRUCCION_RENTA;

            dseAvaluo.FEXAVA_CONSTRUCCIONESMER.AddFEXAVA_CONSTRUCCIONESMERRow(construccionesMercadoRow);
            construccionesMercadoRow = null;
            #endregion

            #region h.4.1 - Investigación productos comparables
            query = XmlUtils.XmlSearchById(enfoqueMercado, "h.4.1");
            if (query.IsFull())
            {
                foreach (XElement cursor in query)
                {
                    investigacionProductosComparablesRow = dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.NewFEXAVA_INVESTPRODUCTOSCOMPRow();
                    investigacionProductosComparablesRow.FEXAVA_CONSTRUCCIONESMERRow = dseAvaluo.FEXAVA_CONSTRUCCIONESMER[1];

                    investigacionProductosComparablesRow.CODTIPOCOMPARABLE = Constantes.PAR_COD_TIPOCOMPARABLE_RENTA;

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.1");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.CALLE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.3");
                    if (queryn.IsFull())
                    {
                        string codDelegacion = queryn.ToStringXElement();
                        investigacionProductosComparablesRow.IDDELEGACION = CatastralUtils.ObtenerIdDelegacionPorClave(codDelegacion);
                        queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.2");
                        if (queryn.IsFull())
                        {
                            investigacionProductosComparablesRow.IDCOLONIA = CatastralUtils.ObtenerIdColoniaPorNombreyDelegacion(queryn.ToStringXElement(), codDelegacion);
                            investigacionProductosComparablesRow[Constantes.COL_DESC_COLONIA] = queryn.ToStringXElement();
                            // investigacionProductosComparablesRow[Constantes.COL_DESC_COLONIA] = CatastralUtils.ObtenerNombreColoniaPorIdColonia(investigacionProductosComparablesRow.IDCOLONIA);
                        }
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.4");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.CODIGOPOSTAL = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.5.1");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.TELEFONO = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.5.2");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.INFORMANTE = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.6");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.DESCRIPCION = queryn.ToStringXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.7");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.SUPERFICIEVENDIBLEPORUNIDAD = XmlUtils.ToDecimalXElementAv(queryn);


                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.8");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.PRECIOSOLICITADO = XmlUtils.ToDecimalXElementAv(queryn);

                    //queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.9");
                    //if (queryn.IsFull())
                    //    investigacionProductosComparablesRow.SUPERFICIEVENDIBLEPORUNIDAD = queryn.ToDecimalXElement();

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.9");
                    if (queryn.IsFull())
                        investigacionProductosComparablesRow.FACTORNEGOCIACION = XmlUtils.ToDecimalXElementAv(queryn);

                    queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.10");
                    if (queryn.IsFull())
                    {
                        queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.10.1");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.REGION = queryn.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.10.2");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.MANZANA = queryn.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.10.3");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.LOTE = queryn.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(cursor, "h.4.1.n.10.4");
                        if (queryn.IsFull())
                            investigacionProductosComparablesRow.UNIDADPRIVATIVA = queryn.ToStringXElement();
                    }
                    investigacionProductosComparablesRow.CODTIPOCOMPARABLE = Constantes.PAR_COD_MODOCONSTRUCCION_RENTA;
                    dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP.AddFEXAVA_INVESTPRODUCTOSCOMPRow(investigacionProductosComparablesRow);
                    investigacionProductosComparablesRow = null;
                }
            }
            #endregion
        }

        /// <summary>
        /// Inserta los datos referentes al enfoque de costos del avaluo Comercial en el dseAvaluos desde
        /// el elemento xml.
        /// </summary>
        /// <param name="enfoqueCostosComercial">Elemento xml con los datos de enfoque de costos
        /// comerciales.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>       
        private void GuardarAvaluoEnfoqueCostosComercialAI(XElement enfoqueCostosComercial, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;

            query = XmlUtils.XmlSearchById(enfoqueCostosComercial, "i.6");
            if (query.IsFull())
            {
                dseAvaluo.FEXAVA_AVALUO[0].IMPORTETOTALENFOQUECOSTOS = XmlUtils.ToDecimalXElementAv(query);

            }
        }

        /// <summary>
        /// Inserta los datos referentes al enfoque de costos del avaluo Catastral en el dseAvaluos desde
        /// el elemento xml.
        /// </summary>
        /// <param name="enfoqueCostosCatastral">Elemento xml con los datos de enfoque de costos
        /// catastrales.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoEnfoqueCostosCatastralAI(XElement enfoqueCostosCatastral, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;
            DseAvaluoMantInf.FEXAVA_ENFOQUECOSTESCATRow enfoqueCostesCatRow = null;

            enfoqueCostesCatRow = dseAvaluo.FEXAVA_ENFOQUECOSTESCAT.NewFEXAVA_ENFOQUECOSTESCATRow();
            enfoqueCostesCatRow.FEXAVA_AVALUORow = dseAvaluo.FEXAVA_AVALUO[0];

            query = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.4");
            if (query.IsFull())
                enfoqueCostesCatRow.IMPINSTALACIONESESPECIALES = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.5");
            if (query.IsFull())
                enfoqueCostesCatRow.IMPTOTVALORCATASTRAL = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.6");
            if (query.IsFull())
            {
                enfoqueCostesCatRow.AVANCEOBRA = XmlUtils.ToDecimalXElementAv(query) * 100; //Es un porcentaje indicado entre 0 y 1 en el xml pero que se debe mostrar entre 0 y 100 en el report
            }

            query = XmlUtils.XmlSearchById(enfoqueCostosCatastral, "j.7");
            if (query.IsFull())
                enfoqueCostesCatRow.IMPTOTVALCATASTRALOBRAPROCESO = XmlUtils.ToDecimalXElementAv(query);

            dseAvaluo.FEXAVA_ENFOQUECOSTESCAT.AddFEXAVA_ENFOQUECOSTESCATRow(enfoqueCostesCatRow);
        }

        /// <summary>
        /// Inserta los datos referentes al enfoque de ingresos en el dseAvaluos desde el elemento xml.
        /// </summary>
        /// <param name="enfoqueIngresos">Elemento xml con los datos de enfoque de ingresos.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>       
        private void GuardarAvaluoEnfoqueIngresosAI(XElement enfoqueIngresos, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIRENTABRUTAMENSUAL = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIRENTABRUTAMENSUAL = decimal.Zero;

            #region k.2 - Deducciones.
            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIVACIOS = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIVACIOS = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTOPREDIAL = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTOPREDIAL = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EISERVICIOAGUA = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EISERVICIOAGUA = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.4");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EICONSERVACIONYMANTENIMIENTO = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EICONSERVACIONYMANTENIMIENTO = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.5");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIENERGIAELECTRICA = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIENERGIAELECTRICA = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.6");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIADMINISTRACION = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIADMINISTRACION = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.7");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EISEGUROS = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EISEGUROS = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.8");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIDEPRECIACIONFISCAL = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIDEPRECIACIONFISCAL = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.9");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIOTROS = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIOTROS = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.10");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESFISCALES = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESFISCALES = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.11");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTORENTA = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPUESTORENTA = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.12");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESMENSUALES = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIDEDUCCIONESMENSUALES = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.2.13");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIPORCENTAJEDEDUCCIONESMENS = XmlUtils.ToDecimalXElementAv(query) * 100;//Es un porcentaje indicado entre 0 y 1 en el xml pero que se debe mostrar entre 0 y 100 en el report
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIPORCENTAJEDEDUCCIONESMENS = decimal.Zero;

            #endregion

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.3");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIPRODUCTOLIQUIDOANUAL = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIPRODUCTOLIQUIDOANUAL = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.4");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EITASACAPITALIZACION = XmlUtils.ToDecimalXElementAv(query) * 100; //Es un porcentaje indicado entre 0 y 1 en el xml pero que se debe mostrar entre 0 y 100 en el report
            else
                dseAvaluo.FEXAVA_AVALUO[0].EITASACAPITALIZACION = decimal.Zero;

            query = XmlUtils.XmlSearchById(enfoqueIngresos, "k.5");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPORTE = XmlUtils.ToDecimalXElementAv(query);
            else
                dseAvaluo.FEXAVA_AVALUO[0].EIIMPORTE = decimal.Zero;
        }

        /// <summary>
        /// Inserta los datos referentes al resumen conclusion del avaluo en el dseAvaluos desde el
        /// elemento xml.
        /// </summary>
        /// <param name="conclusionAvaluo">Elemento xml con los datos de la conclusion del avaluo.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoResumenConclusionAvaluoAI(XElement conclusionAvaluo, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;

            query = XmlUtils.XmlSearchById(conclusionAvaluo, "o.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORCOMERCIAL = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(conclusionAvaluo, "o.2");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORCATASTRAL = XmlUtils.ToDecimalXElementAv(query);
        }

        /// <summary>
        /// Inserta los datos referentes al valor referido del avaluo en el dseAvaluos desde el elemento
        /// xml.
        /// </summary>
        /// <param name="valorReferido">Elemento xml con los datos del valor referido del avaluo.</param>
        /// <param name="dseAvaluo">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoValorReferidoAI(XElement valorReferido, ref DseAvaluoMantInf dseAvaluo)
        {
            IEnumerable<XElement> query = null;

            query = XmlUtils.XmlSearchById(valorReferido, "p.1");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FECHAVALORREFERIDO = query.First().Value.To<DateTime>().ToShortDateString();

            query = XmlUtils.XmlSearchById(valorReferido, "p.5");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].VALORREFERIDO = XmlUtils.ToDecimalXElementAv(query);

            query = XmlUtils.XmlSearchById(valorReferido, "p.4");
            if (query.IsFull())
                dseAvaluo.FEXAVA_AVALUO[0].FACTORCONVERSION = XmlUtils.ToDecimalXElementAv(query);
        }

        /// <summary>
        /// Inserta los datos referentes al anexo fotografico del avaluo en el dseAvaluos desde el
        /// elemento xml.
        /// </summary>
        /// <param name="anexoFotografico">Elemento xml con los datos del anexo fotografico del avaluo.</param>
        /// <param name="dseAvaluos">[in,out] DataSet de avaluos en el que se insertan los datos del xml.</param>
        private void GuardarAvaluoAnexoFotograficoAI(XElement anexoFotografico, ref DseAvaluoMantInf dseAvaluos)
        {
            IEnumerable<XElement> query = null;
            IEnumerable<XElement> queryn = null;
            IEnumerable<XElement> querynn = null;

            SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.Enum.TipoFotoInmueble tipoFoto = new SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.Enum.TipoFotoInmueble();
            string cuentaCatastral = string.Empty;
            List<string> listCuentaCatastral = null;

            DseAvaluoMantInf.FEXAVA_FOTOAVALUORow fotoAvaluoRow = null;
            DseAvaluoMantInf.FEXAVA_FOTOCOMPARABLERow fotoComparableRow = null;
            DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investProductosCompRow = null;

            #region q.1 - Sujeto

            query = XmlUtils.XmlSearchById(anexoFotografico, "q.1");
            if (query.IsFull())
            {
                query = XmlUtils.XmlSearchById(anexoFotografico, "q.1.2");
                if (query.IsFull())
                {
                    int rownumber = 0;
                    foreach (XElement cursor in query)
                    {
                        //Inicializar una nueva row.
                        fotoAvaluoRow = dseAvaluos.FEXAVA_FOTOAVALUO.NewFEXAVA_FOTOAVALUORow();
                        fotoAvaluoRow.FEXAVA_AVALUORow = dseAvaluos.FEXAVA_AVALUO[0];

                        query = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.1");
                        if (query.IsFull())
                            fotoAvaluoRow.REGION = query.ToStringXElement();

                        query = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.2");
                        if (query.IsFull())
                            fotoAvaluoRow.MANZANA = query.ToStringXElement();

                        query = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.3");
                        if (query.IsFull())
                            fotoAvaluoRow.LOTE = query.ToStringXElement();

                        query = XmlUtils.XmlSearchById(anexoFotografico, "q.1.1.4");
                        if (query.IsFull())
                            fotoAvaluoRow.UNIDADPRIVATIVA = query.ToStringXElement();

                        queryn = XmlUtils.XmlSearchById(cursor, "q.1.2.n.1");
                        if (queryn.IsFull())
                        {
                            fotoAvaluoRow.BINARIOS = ObtenerImagenPorId(queryn.ToDecimalXElement());
                            query = XmlUtils.XmlSearchById(cursor, "q.1.2.n.2");
                            if (query.IsFull())
                                fotoAvaluoRow.INTEXT = query.ToStringXElement();

                            //query = XmlUtils.XmlSearchById(cursor, "q.1.2.n.1");
                            //if (query.IsFull())
                            //{

                            //}
                            //Insertar en Fexava_FotoAvaluo
                            fotoAvaluoRow.ROWNUMBER = rownumber;
                            fotoAvaluoRow.IDDOCUMENTOFOTO = XmlUtils.ToDecimalXElementAv(queryn);
                            dseAvaluos.FEXAVA_FOTOAVALUO.AddFEXAVA_FOTOAVALUORow(fotoAvaluoRow);
                        }

                        rownumber++;
                    }
                }
            }
            #endregion

            #region q.2 - Comparable rentas

            query = XmlUtils.XmlSearchById(anexoFotografico, "q.2");
            if (query.IsFull())
            {
                int rownumber = 0;

                foreach (XElement cursor in query)
                {
                    listCuentaCatastral = new List<string>();

                    queryn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.1");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    queryn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.2");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    queryn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.3");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    queryn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.4");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    cuentaCatastral = string.Empty;
                    for (int i = 0; i < listCuentaCatastral.Count; i++)
                    {
                        cuentaCatastral += listCuentaCatastral[i];
                    }

                    queryn = XmlUtils.XmlSearchById(cursor, "q.2.n.2");

                    if (queryn.IsFull())
                    {

                        foreach (XElement cursorn in queryn)
                        {
                            tipoFoto = new SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.Enum.TipoFotoInmueble();
                            querynn = XmlUtils.XmlSearchById(cursorn, "q.2.n.2.n.2");
                            if (querynn.IsFull())
                                tipoFoto = TipoInmueble(querynn.First());
                            queryn = XmlUtils.XmlSearchById(cursorn, "q.2.n.2.n.1");

                            if (queryn.IsFull())
                            {


                                //if (query.IsFull())
                                //{

                                if (listCuentaCatastral.Count() == 4)
                                {
                                    string sqlQuery = Constantes.REGION + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[0] + Constantes.OPERADOR_AND +
                                                      Constantes.MANZANA + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[1] + Constantes.OPERADOR_AND +
                                                      Constantes.LOTE + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[2] + Constantes.OPERADOR_AND +
                                                      Constantes.UNIDADPRIV + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[3];

                                    if (dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(sqlQuery).Any())
                                    {
                                        investProductosCompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(sqlQuery).First();

                                        //Insertar en FEXAVA_FOTOCOMPARABLE
                                        fotoComparableRow = dseAvaluos.FEXAVA_FOTOCOMPARABLE.NewFEXAVA_FOTOCOMPARABLERow();
                                        fotoComparableRow.FEXAVA_INVESTPRODUCTOSCOMPRow = investProductosCompRow;

                                        querynn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.1");
                                        if (querynn.IsFull())
                                            fotoComparableRow.REGION = querynn.ToStringXElement();

                                        querynn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.2");
                                        if (querynn.IsFull())
                                            fotoComparableRow.MANZANA = querynn.ToStringXElement();

                                        querynn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.3");
                                        if (querynn.IsFull())
                                            fotoComparableRow.LOTE = querynn.ToStringXElement();

                                        querynn = XmlUtils.XmlSearchById(cursor, "q.2.n.1.n.4");
                                        if (querynn.IsFull())
                                            fotoComparableRow.UNIDAD = querynn.ToStringXElement();

                                        query = XmlUtils.XmlSearchById(cursor, "q.2.n.2.n.1");
                                        if (query.IsFull())
                                            fotoComparableRow.BINARIOS = ObtenerImagenPorId(query.ToDecimalXElement());
                                        query = XmlUtils.XmlSearchById(anexoFotografico, "q.2.n.2.n.2");
                                        if (query.IsFull())
                                            fotoComparableRow.INTEXT = query.ToStringXElement();
                                        fotoComparableRow.ROWNUMBER = rownumber;
                                        fotoComparableRow.TIPO = Constantes.PAR_COD_TIPOCOMPARABLE_RENTA;
                                        fotoComparableRow.IDDOCUMENTOFOTO = XmlUtils.ToDecimalXElementAv(queryn);
                                        dseAvaluos.FEXAVA_FOTOCOMPARABLE.AddFEXAVA_FOTOCOMPARABLERow(fotoComparableRow);
                                    }
                                }
                                //}
                            }
                            rownumber++;
                        }

                    }

                }

            }

            #endregion

            #region q.3 - Comparable ventas

            query = XmlUtils.XmlSearchById(anexoFotografico, "q.3");

            if (query.IsFull())
            {
                int rownumber = 0;
                foreach (XElement cursor in query)
                {
                    listCuentaCatastral = new List<string>();

                    queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.1");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.2");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.3");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.4");
                    if (queryn.IsFull())
                        listCuentaCatastral.Add(queryn.ToStringXElement());

                    cuentaCatastral = string.Empty;
                    for (int i = 0; i < listCuentaCatastral.Count; i++)
                    {
                        cuentaCatastral += listCuentaCatastral[i];
                    }
                    queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.2");
                    if (queryn.IsFull())
                    {

                        foreach (XElement cursorn in queryn)
                        {
                            tipoFoto = new SIGAPred.Documental.Services.Negocio.Gestion.AltasDocumentos.Enum.TipoFotoInmueble();

                            querynn = XmlUtils.XmlSearchById(cursorn, "q.3.n.2.n.2");
                            if (querynn.IsFull())
                                tipoFoto = TipoInmueble(querynn.First());

                            querynn = XmlUtils.XmlSearchById(cursorn, "q.3.n.2.n.1");
                            if (querynn.IsFull())
                            {

                                //query = XmlUtils.XmlSearchById(cursor, "q.3.n.2.n.1");
                                //if (query.IsFull())

                                if (listCuentaCatastral.Count() == 4)
                                {
                                    string sqlQuery = Constantes.REGION + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[0] + Constantes.OPERADOR_AND +
                                                      Constantes.MANZANA + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[1] + Constantes.OPERADOR_AND +
                                                      Constantes.LOTE + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[2] + Constantes.OPERADOR_AND +
                                                      Constantes.UNIDADPRIV + Constantes.SIMBOLO_IGUAL_A + listCuentaCatastral[3];

                                    if (dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(sqlQuery).Any())
                                    {
                                        investProductosCompRow = (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow)dseAvaluos.FEXAVA_INVESTPRODUCTOSCOMP.Select(sqlQuery).First();
                                        fotoComparableRow = dseAvaluos.FEXAVA_FOTOCOMPARABLE.NewFEXAVA_FOTOCOMPARABLERow();
                                        fotoComparableRow.FEXAVA_INVESTPRODUCTOSCOMPRow = investProductosCompRow;
                                        fotoComparableRow.BINARIOS = ObtenerImagenPorId(querynn.ToDecimalXElement());
                                        query = XmlUtils.XmlSearchById(cursorn, "q.3.n.2.n.2");
                                        if (query.IsFull())
                                            fotoComparableRow.INTEXT = query.ToStringXElement(); //Insertar en FEXAVA_FOTOCOMPARABLE


                                        queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.1");
                                        if (queryn.IsFull())
                                        {
                                            fotoComparableRow.REGION = queryn.ToStringXElement();
                                        }

                                        queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.2");
                                        if (queryn.IsFull())
                                        {
                                            fotoComparableRow.MANZANA = queryn.ToStringXElement();
                                        }

                                        queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.3");
                                        if (queryn.IsFull())
                                        {
                                            fotoComparableRow.LOTE = queryn.ToStringXElement();
                                        }

                                        queryn = XmlUtils.XmlSearchById(cursor, "q.3.n.1.n.4");
                                        if (queryn.IsFull())
                                        {
                                            fotoComparableRow.UNIDAD = queryn.ToStringXElement();
                                        }

                                        fotoComparableRow.ROWNUMBER = rownumber;
                                        fotoComparableRow.TIPO = Constantes.PAR_COD_TIPOCOMPARABLE_VENTA; ;
                                        fotoComparableRow.IDDOCUMENTOFOTO = querynn.ToDecimalXElement();
                                        dseAvaluos.FEXAVA_FOTOCOMPARABLE.AddFEXAVA_FOTOCOMPARABLERow(fotoComparableRow);
                                    }
                                }
                            }

                            rownumber++;
                        }
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Obtiene de documental, el xml correspondiente a al idavalúo que se le pasa
        /// </summary>
        /// <param name="idAvaluo">id del avaluo en cuestion</param>
        /// <returns>xml del avaluo</returns>
        private XmlDocument GetXmlAvaluoAI(decimal idAvaluo)
        {
            try
            {
                byte[] avaluoByte = null;
                DocumentosDigitales documentosDig = new DocumentosDigitales();
                ServiceDocumentosDigitales.DocumentosDigitalesClient cliente = new DocumentosDigitalesClient();

                //Obtener el xml de documental.
                ServiceDocumentosDigitales.dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable ficheroDocumentoDT = cliente.GetFicherosByDocumentoDigital(idAvaluo);
           //     Documental.Services.AccesoDatos.Gestion.DocumentosDigitales.
           //dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable ficheroDocumentoDT = documentosDig.GetFicherosByDocumentoDigital(idAvaluo);
                if (!ficheroDocumentoDT.Any())
                    return null;

                ficheroDocumentoDT = cliente.GetFicheroDocumentoById((from a in ficheroDocumentoDT where a.NOMBRE.Contains(Constantes.XML_FILE_EXTENSION) select a).First().IDFICHERODOCUMENTO);
                if (!ficheroDocumentoDT.Any())
                    return null;

                avaluoByte = ficheroDocumentoDT.First().BINARIODATOS;
                if (avaluoByte == null)
                    return null;

                XmlDocument xmlAvaluo = new XmlDocument();
                xmlAvaluo.Load(new MemoryStream(avaluoByte));
                return xmlAvaluo;
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw ex;// FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        /// <summary>
        /// Obtiene las descripciones de BD de los campos que son códigos en el xml.
        /// </summary>
        /// <param name="dseAvaluo">datset avlaúo</param>
        private void ObtenerInsertarDescripcionesAI(ref DseAvaluoMantInf dseAvaluo)
        {

            #region Imagenes

            #region Croquis Microlocalización y Macrolocalización

            DocumentosDigitalesClient clienteDOC = new DocumentosDigitalesClient();
            byte[] fotoBinMi = null;
            byte[] fotoBinMa = null;

            try
            {
                fotoBinMi = clienteDOC.GetFicheroDocumentoById(Convert.ToDecimal(dseAvaluo.FEXAVA_AVALUO[0].DMICRO)).First().BINARIODATOS;
                fotoBinMa = clienteDOC.GetFicheroDocumentoById(Convert.ToDecimal(dseAvaluo.FEXAVA_AVALUO[0].DMACRO)).First().BINARIODATOS;
            }
            finally
            {
                clienteDOC.Disconnect();
            }

            //MICRO
            dseAvaluo.FEXAVA_AVALUO[0].BYTES_MICRO = Convert.ToBase64String(fotoBinMi);

            //MACRO
            dseAvaluo.FEXAVA_AVALUO[0].BYTES_MACRO = Convert.ToBase64String(fotoBinMa);

            #endregion

            #region Anexo Fotográfico Sujeto
            // ServiceDocumentosDigitales.dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable tableFoto = null;
            //decimal[] imagenes = new decimal[dseAvaluo.FEXAVA_FOTOAVALUO.Rows.Count];
            //for (int i = 0; i < dseAvaluo.FEXAVA_FOTOAVALUO.Rows.Count; i++)
            //    imagenes[i] = ((DseAvaluoMantInf.FEXAVA_FOTOAVALUORow)(dseAvaluo.FEXAVA_FOTOAVALUO.Rows[i])).IDDOCUMENTOFOTO;

            //clienteDOC = new DocumentosDigitalesClient();

            //try
            //{
            //    tableFoto = clienteDOC.GetImagenes(imagenes);
            //}
            //finally
            //{
            //    clienteDOC.Disconnect();
            //}

            //int ind = 0;
            //foreach (ServiceDocumentosDigitales.dseDocumentosDigitales.DOC_FICHERODOCUMENTORow rowFoto in tableFoto)
            //{
            //    byte[] fotoBin = rowFoto.BINARIODATOS;
            //    ((DseAvaluoMantInf.FEXAVA_FOTOAVALUORow)(dseAvaluo.FEXAVA_FOTOAVALUO.Rows[ind])).BINARIOS = Convert.ToBase64String(fotoBin);
            //    ind++;
            //}
            #endregion

            //#region Anexo Fotográfico Comparables
            //tableFoto = null;
            //imagenes = new decimal[dseAvaluo.FEXAVA_FOTOCOMPARABLE.Rows.Count];
            //for (int i = 0; i < dseAvaluo.FEXAVA_FOTOCOMPARABLE.Rows.Count; i++)
            //    imagenes[i] = ((DseAvaluoMantInf.FEXAVA_FOTOCOMPARABLERow)(dseAvaluo.FEXAVA_FOTOCOMPARABLE.Rows[i])).IDDOCUMENTOFOTO;

            //clienteDOC = new DocumentosDigitalesClient();

            //try
            //{
            //    tableFoto = clienteDOC.GetImagenes(imagenes);
            //}
            //finally
            //{
            //    clienteDOC.Disconnect();
            //}

            //ind = 0;
            //foreach (ServiceDocumentosDigitales.dseDocumentosDigitales.DOC_FICHERODOCUMENTORow rowFoto in tableFoto)
            //{
            //    byte[] fotoBin = rowFoto.BINARIODATOS;
            //    ((DseAvaluoMantInf.FEXAVA_FOTOCOMPARABLERow)(dseAvaluo.FEXAVA_FOTOCOMPARABLE.Rows[ind])).BINARIOS = Convert.ToBase64String(fotoBin);
            //    ind++;

            //}
            //#endregion

            #endregion

            #region DescripcionesCaracteristicasurbanas

            //Obtener Descripciones y Añadir descripciones al report 
            string claseConstDesc = FiscalUtils.ObtenerDescripcionClaseConstruccion(dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASESCONSTRUCCION);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_CLASECONST = claseConstDesc;

            //Clasificacion Zona
            string clasificacionZonaDesc = FiscalUtils.ObtenerDescripcionClasificacionZona(dseAvaluo.FEXAVA_AVALUO[0].CUCODCLASIFICACIONZONA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_CLASIFICACIONZONA = clasificacionZonaDesc;

            //Densidad
            string densidadDesc = FiscalUtils.ObtenerDescripcionDensidadPob(dseAvaluo.FEXAVA_AVALUO[0].CUCODDENSIDADPOBLACION);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DENSIDAD = densidadDesc;

            //Nivel socioeconomico
            string NivelSocioEc = FiscalUtils.ObtenerDescripcionNivelSocioEc(dseAvaluo.FEXAVA_AVALUO[0].CUCODNIVELSOCIOECONOMICO);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_NIVELSOCIOEC = NivelSocioEc;

            //Acometida Inmueble
            string AcometidaInm = FiscalUtils.ObtenerDescripcionAcometidaInm(dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_ACOMETIDAIM = AcometidaInm;

            //Acometida Inmueble
            string AcometidaInmTel = FiscalUtils.ObtenerDescripcionAcometidaInm(dseAvaluo.FEXAVA_AVALUO[0].CUCODACOMETIDAINMUEBLETEL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_ACOMETIDAIMTEL = AcometidaInmTel;

            //Alumbrado publico 
            string AlumbradoPub = FiscalUtils.ObtenerDescripcionAlumbradoPublico(dseAvaluo.FEXAVA_AVALUO[0].CUCODALUMBRADOPUBLICO);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_ALUMBRADOPUBL = AlumbradoPub;

            //AguaPotable
            string AguaPotable = FiscalUtils.ObtenerDescripcionAguaPotable(dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_AGUAPOTABLE = AguaPotable;

            //Suministro electrico
            string SumElectrico = FiscalUtils.ObtenerDescripcionSuministroElectrico(dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROELECTRICO);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SUMELECTRICO = SumElectrico;

            //Gas Natural
            string GasNatural = FiscalUtils.ObtenerDescripcionGasNatural(dseAvaluo.FEXAVA_AVALUO[0].CUCODGASNATURAL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_GASNATURAL = GasNatural;

            //Guarniciones
            string Guarniciones = FiscalUtils.ObtenerDescripcionGuarniciones(dseAvaluo.FEXAVA_AVALUO[0].CUCODGUARNICIONES);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_GUARNICIONES = Guarniciones;

            //Banquetas
            string Banquetas = FiscalUtils.ObtenerDescripcionBanquetas(dseAvaluo.FEXAVA_AVALUO[0].CUCODBANQUETAS);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_BANQUETAS = Banquetas;

            //SenalizacionVias
            string SenalizacionVias = FiscalUtils.ObtenerDescripcionSenalizacionVias(dseAvaluo.FEXAVA_AVALUO[0].CUCODSENALIZACIONVIAS);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SENALIZACIONVIAS = SenalizacionVias;

            //SuministroTel
            string SuministroTel = FiscalUtils.ObtenerDescripcionSuministroTel(dseAvaluo.FEXAVA_AVALUO[0].CUCODSUMINISTROTELEFONICA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SUMINISTROTEL = SuministroTel;

            //VigilanciaZona
            string VigilanciaZona = FiscalUtils.ObtenerDescripcionVigilanciaZona(dseAvaluo.FEXAVA_AVALUO[0].CUCODVIGILANCIAZONA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_VIGILANCIA = VigilanciaZona;

            //Vialidades
            string Vialidades = FiscalUtils.ObtenerDescripcionVialidades(dseAvaluo.FEXAVA_AVALUO[0].CUCODVIALIDADES);
            dseAvaluo.FEXAVA_AVALUO[0].DESCVIALIDADES = Vialidades;

            //DrenajePluvialZona
            string DrenajePluvZona = FiscalUtils.ObtenerDescripcionDrenajePluvial(dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALZONA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DRENAJEPLUVZONA = DrenajePluvZona;

            //DrenajePluvialCalle
            string DrenajePluvCalle = FiscalUtils.ObtenerDescripcionDrenajePluvial(dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEPLUVIALCALLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DRENAJEPLUVCALLE = DrenajePluvCalle;

            //SistemaMixto
            string SistemMixto = FiscalUtils.ObtenerDescripcionDrenaje(dseAvaluo.FEXAVA_AVALUO[0].CUCODDRENAJEINMUEBLE);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_SISTEMAMIXTO = SistemMixto;

            //RecolecAguas
            string RecolecAguas = FiscalUtils.ObtenerDescripcionDrenaje(dseAvaluo.FEXAVA_AVALUO[0].CUCODAGUAPOTABLERESIDUAL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_RECOLECAGUAS = RecolecAguas;

            //Topografia
            string Topografia = FiscalUtils.ObtenerDescripcionTopografia(dseAvaluo.FEXAVA_AVALUO[0].CUCODTOPOGRAFIA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_TOPOGRAFIA = Topografia;

            //DensidadHabitacional
            string DensidadHab = FiscalUtils.ObtenerDescripcionDensidadHab(dseAvaluo.FEXAVA_AVALUO[0].TECODDENSIDADHABITACIONAL);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_DENSIDADHAB = DensidadHab;

            //Regimen Propiedad
            string RegimenProp = FiscalUtils.ObtenerDescripcionRegimenPropiedad(dseAvaluo.FEXAVA_AVALUO[0].CODREGIMENPROPIEDAD);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_REGIMENPROPIEDAD = RegimenProp;

            //Recoleccion Basura
            string RecoleccionBasura = FiscalUtils.ObtenerDescripcionRecoleccionBasura(dseAvaluo.FEXAVA_AVALUO[0].CUCODRECOLECCIONBASURA);
            dseAvaluo.FEXAVA_AVALUO[0].DESC_RECBASURA = RecoleccionBasura;

            //NomenclatruaCalles
            dseAvaluo.FEXAVA_AVALUO[0].DESC_NOMECLATURACALLES = FiscalUtils.ObtenerDescripcionNomenclaturaCalles(dseAvaluo.FEXAVA_AVALUO[0].CUCODNOMENCLATURACALLE);

            #endregion

            #region DescripcionConstrucciones

            foreach (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow constRow in dseAvaluo.FEXAVA_CONSTRUCCIONES)
            {
                if (!constRow.IsCODCLASESCONSTRUCCIONNull())
                {
                    constRow[Constantes.COL_DESC_CLASECONST] = FiscalUtils.ObtenerDescripcionClaseConstruccion(constRow.CODCLASESCONSTRUCCION);
                }
            }
            #endregion

            #region UbicacionInmueble

            ////Delegación
            //string nombreDeleg = CatastralUtils.ObtenerNombreDelegacion(dseAvaluo.FEXAVA_AVALUO[0].DELEGACION);
            //dseAvaluo.FEXAVA_AVALUO[0].DELEGACION = nombreDeleg;

            #endregion

            #region DescripcionesTipoConstruccion

            string descElemento;
            decimal codElemento;

            foreach (DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESRow tipoConstRow in dseAvaluo.FEXAVA_CONSTRUCCIONES)
            {
                if (!tipoConstRow.IsCODESTADOCONSERVACIONNull())
                {
                    codElemento = tipoConstRow.CODESTADOCONSERVACION;
                    descElemento = FiscalUtils.ObtenerDescripcionEstadoConservacion(codElemento);
                }
                else
                {
                    descElemento = string.Empty;
                }

                tipoConstRow[Constantes.COL_DESC_CONSERVACION] = descElemento;
                if (!tipoConstRow.IsCODCLASESVIDASNull())
                {
                    codElemento = tipoConstRow.CODCLASESVIDAS;
                    descElemento = FiscalUtils.ObtenerDescripcionClasesVidas(codElemento);

                }
                else
                {
                    descElemento = string.Empty;
                }
                tipoConstRow[Constantes.COL_DESC_CLASEVIDA] = descElemento;

                if (!tipoConstRow.IsCODRANGONIVELESNull())
                {
                    codElemento = tipoConstRow.CODRANGONIVELES;
                    descElemento = FiscalUtils.ObtenerDescripcionRangoNiveles(codElemento);
                }
                else
                {
                    descElemento = string.Empty;
                }
                tipoConstRow[Constantes.COL_DESC_RANGONIV] = descElemento;

                if (!tipoConstRow.IsCODCLASESCONSTRUCCIONNull())
                {
                    codElemento = tipoConstRow.CODCLASESCONSTRUCCION;
                    descElemento = FiscalUtils.ObtenerDescripcionClaseConstruccion(codElemento);
                }
                else
                {
                    descElemento = string.Empty;
                }
                tipoConstRow[Constantes.COL_DESC_CLASECONST] = descElemento;
            }
            #endregion

            #region DescripcionesElementosAcc
            decimal cod;
            string desc;

            foreach (DseAvaluoMantInf.FEXAVA_ELEMENTOSEXTRARow elemExtraRow in dseAvaluo.FEXAVA_ELEMENTOSEXTRA)
            {
                if (!elemExtraRow.IsCODINSTALACIONESESPECIALESNull())
                {
                    cod = Convert.ToDecimal(elemExtraRow.CODINSTALACIONESESPECIALES);
                    desc = FiscalUtils.ObtenerDescripcionInstalacionesEspeciales(cod);
                    elemExtraRow[Constantes.COL_DESC_INSTESPECIAL] = desc;
                }
            }
            #endregion

            #region DescripcionDatosPersonas

            decimal idDeleg;

            // Datos del propietario  y del solicitante
            //foreach (DseAvaluoMantInf.FEXAVA_DATOSPERSONASRow datoPersonaRow in dseAvaluo.FEXAVA_DATOSPERSONAS)
            //{
            //    //Obtener el nombre de la delegacion
            //    idDeleg = datoPersonaRow.IDDELEGACION;
            //    desc = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(idDeleg);
            //    datoPersonaRow[Constantes.COL_DESC_DELEGACION] = desc;
            //}

            #endregion

            #region DescripcionesInvestproductoscomp

            // Datos del propietario  y del solicitante
            foreach (DseAvaluoMantInf.FEXAVA_INVESTPRODUCTOSCOMPRow investDatosRow in dseAvaluo.FEXAVA_INVESTPRODUCTOSCOMP)
            {
                //Obtener el nombre de la delegacion
                idDeleg = investDatosRow.IDDELEGACION;
                desc = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(idDeleg);
                investDatosRow[Constantes.COL_DESC_DELEGACION] = desc;
            }
            #endregion

            #region DescripcionesDatosTerrenos
            foreach (DseAvaluoMantInf.FEXAVA_DATOSTERRENOSRow datosTerrenoRow in dseAvaluo.FEXAVA_DATOSTERRENOS)
            {
                //Obtener el nombre de la delegacion
                idDeleg = datosTerrenoRow.IDDELEGACION;
                desc = CatastralUtils.ObtenerNombreDelegacionPorIdDeleg(idDeleg);
                datosTerrenoRow[Constantes.COL_DESC_DELEGACION] = desc;
            }
            #endregion


        }

        /// <summary>
        /// Obtiene el nombre de una persona a partir del idpersona
        /// </summary>
        /// <param name="idPersona">idpersona</param>
        /// <returns>Nombre de la persona</returns>
        private string ObtenerNombrePersonaAI(decimal idPersona)
        {
            try
            {
                RegistroContribuyentesClient clienteRCON = new RegistroContribuyentesClient();
                ServiceRCON.DseInfoContribuyente dsePersona = null;

                try
                {
                    dsePersona = clienteRCON.GetInfoContribuyente(idPersona);
                }
                finally
                {
                    clienteRCON.Disconnect();
                }

                if (dsePersona.Contribuyente.Any())
                {
                    if (dsePersona.Contribuyente[0].CODTIPOPERSONA.Equals(Constantes.COD_PERSONA_FISICA))
                    {
                        StringBuilder nombreCompleto = new StringBuilder("");
                        if (!dsePersona.Contribuyente[0].IsAPELLIDOPATERNONull())
                            nombreCompleto.Append(dsePersona.Contribuyente[0].APELLIDOPATERNO).Append(" ");
                        else
                            nombreCompleto.Append(" ");
                        if (!dsePersona.Contribuyente[0].IsAPELLIDOMATERNONull())
                            nombreCompleto.Append(dsePersona.Contribuyente[0].APELLIDOMATERNO).Append(", ");
                        else
                            nombreCompleto.Append(" ");
                        if (!dsePersona.Contribuyente[0].IsNOMBRENull())
                            nombreCompleto.Append(dsePersona.Contribuyente[0].NOMBRE);
                        else
                            nombreCompleto.Append(" ");

                        return nombreCompleto.ToString();//dsePersona.Contribuyente[0].APELLIDOPATERNO + " " + dsePersona.Contribuyente[0].APELLIDOMATERNO + ", " + dsePersona.Contribuyente[0].NOMBRE;
                    }
                    else if (dsePersona.Contribuyente[0].CODTIPOPERSONA.Equals(Constantes.COD_PERSONA_MORAL))
                        if (!dsePersona.Contribuyente[0].IsAPELLIDOPATERNONull())
                            return dsePersona.Contribuyente[0].APELLIDOPATERNO;
                        else
                            return " ";
                    else
                        return " ";

                }
                else
                {
                    return String.Empty;
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
            }
        }

        /// <summary>
        /// A partir del id de una imagen almacenada en documental obtiene su foto
        /// </summary>
        private string ObtenerImagenPorIdAI(decimal idImagen)
        {
            DocumentosDigitalesClient clienteDOC = new DocumentosDigitalesClient();
            try
            {

                clienteDOC = new DocumentosDigitalesClient();
                decimal[] imagen = new decimal[1];
                imagen[0] = idImagen;
                ServiceDocumentosDigitales.dseDocumentosDigitales.DOC_FICHERODOCUMENTODataTable fotos = clienteDOC.GetImagenes(imagen);
                byte[] fotoBin = fotos[0].BINARIODATOS;
                return Convert.ToBase64String(fotoBin);

            }

            finally
            {
                clienteDOC.Disconnect();

            }



        }


        public string RegistrarAvaluoAI(byte[] xmlBytes)
        {
            throw new NotImplementedException();
        }
    }
}

