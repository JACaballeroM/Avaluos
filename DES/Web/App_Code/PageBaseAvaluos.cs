using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SIGAPred.Common.Web;
using ServiceAvaluos;
using System.ComponentModel;
using ServiceDocumental;
using ServiceCatastral;
using ServiceRCON;
using SIGAPred.Common.DigitoVerificador;
using SIGAPred.Common.WCF;
using System.ServiceModel;


/// <summary>
/// Clase base de avalúos
/// </summary>
public class PageBaseAvaluos : PageBase
{
    /// <summary>
    /// Propiedad que almacena dirección de la ordenación del grid de tareas
    /// </summary>
    public string SortDirectionP
    {
        get { return (ViewState[Constantes.PAR_VIEWSTATE_SORTDIRECTION] == null ? string.Empty : ViewState[Constantes.PAR_VIEWSTATE_SORTDIRECTION].ToString()); }
        set { ViewState[Constantes.PAR_VIEWSTATE_SORTDIRECTION] = value; }
    }

    /// <summary>
    /// Propiedad que almacena el criterio de ordenación del grid de tareas
    /// </summary>
    public string SortExpression
    {
        get { return (ViewState[Constantes.PAR_VIEWSTATE_SORTEXPRESION] == null ? string.Empty : ViewState[Constantes.PAR_VIEWSTATE_SORTEXPRESION].ToString()); }
        set { ViewState[Constantes.PAR_VIEWSTATE_SORTEXPRESION] = value; }
    }

    #region VALIDACIONES

    /// <summary>
    /// Método que verifica que el dígito corresponde con la cuenta catastral
    /// </summary>
    /// <param name="region">región</param>
    /// <param name="manzana">manzana</param>
    /// <param name="lote">lote</param>
    /// <param name="unidadPriv">unidad privativa</param>
    /// <param name="digito">digito verificador</param>
    /// <returns>True si pasa la validación, false en caso contrario</returns>
    public bool verificarDigito(string region, string manzana, string lote, string unidadPriv, string digito)
    {
        return DigitoVerificadorUtils.ComprobarDigitoVerificador(region, manzana, lote, unidadPriv, digito);
    }

    /// <summary>
    /// Método que verifica que la cuenta catastral introducida existe
    /// </summary>
    /// <param name="region">región</param>
    /// <param name="manzana">manzana</param>
    /// <param name="lote">lote</param>
    /// <param name="unidadPriv">unidad privativa</param>
    /// <returns>True si pasa la validación, false en caso contrario</returns>
    public bool verificarCuentaCat(string region, string manzana, string lote, string unidadPriv)
    {
        DseInmueble inm = null;
        ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();

        try
        {
            inm = clienteCAS.GetInmuebleByClave(region, manzana, lote, unidadPriv); ;
        }
        catch (FaultException<ConsultaCatastralInfoException> ex)
        {
            if (ex.Message.ToUpper().Trim().Equals("El predio solicitado no es fiscalmente válido".ToUpper().Trim()))
            {

                throw new FaultException<ExcepcionCuentaNoExiste>(new ExcepcionCuentaNoExiste());
            }
            else
                throw ex;


        }
        finally
        {
            clienteCAS.Disconnect();
        }

        return inm.Inmueble.Any();
    }
    #endregion

    /// <summary>
    /// Comprueba si existe un xml asociado para un idavalúo
    /// </summary>
    /// <param name="idavaluo">identificador de avalúo</param>
    /// <returns>
    /// True si el avalúo tiene un xml asociado
    /// False si el avalúo no tiene un xml asociado
    /// False si el avalúo no existe
    /// </returns>
    public bool ExisteXMLAsociado(decimal idavaluo)
    {
        DocumentosDigitalesClient clienteDOC = new DocumentosDigitalesClient();

        try
        {
            return clienteDOC.CountFicheroDocumento(idavaluo) > 0;
        }
        finally
        {
            clienteDOC.Disconnect();
        }
    }


    /// <summary>
    /// Une los campos región, manzana, lote y unidad privativa y crea la cuenta catatral
    /// </summary>
    /// <param name="Region">región</param>
    /// <param name="Manzana">manzana</param>
    /// <param name="Lote">lote</param>
    /// <param name="UnidadPrivativa">unidad privativa</param>
    /// <returns>Devuelve la cuenta catastral completa con el formato correcto</returns>
    public string ComponerCuenta(string Region, string Manzana, string Lote, string UnidadPrivativa)
    {
        return Region + Constantes.CUENTACAT_SIMBOLO_UNION_CAMPOS + Manzana + Constantes.CUENTACAT_SIMBOLO_UNION_CAMPOS + Lote + Constantes.CUENTACAT_SIMBOLO_UNION_CAMPOS + UnidadPrivativa;
    }


    /// <summary>
    /// Crea el nombre del fichero xml que contiene el avalúo
    /// </summary>
    /// <param name="cuentaCat">Cuenta catastral</param>
    /// <returns>Nombre del fichero xml que contiene el avalúo</returns>
    public string CrearNombreFicheroXML(string tipoAv, string cuentaCat)
    {
        if (tipoAv.Equals(Constantes.PAR_AVALUO_CATASTRAL_SHORT))
        {
            return Constantes.NOMBRE_FICHEROAV_PREFIJO + Constantes.SIMBOLO_GUION + "Cat" + Constantes.SIMBOLO_GUION + cuentaCat + Constantes.XML_FILE_EXTENSION;
        }
        else
        {
            return Constantes.NOMBRE_FICHEROAV_PREFIJO + Constantes.SIMBOLO_GUION + "Com" + Constantes.SIMBOLO_GUION + cuentaCat + Constantes.XML_FILE_EXTENSION;
        }
    }
}
