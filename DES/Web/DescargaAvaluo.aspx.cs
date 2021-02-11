using System;
using System.Linq;
using System.Text;
using ServiceAvaluos;
using SIGAPred.Common.Seguridad;
using SIGAPred.Common.WCF;
using SIGAPred.Common.Web;
using SIGAPred.FuentesExternas.Avaluos.Services.Enum;
/// <summary>
/// Formulario para la descarga de avalúos.
/// </summary>
public partial class DescargaAvaluo : PageBaseAvaluos
{
    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string cuentaCatastral = WebUtils.QueryString(Constantes.PAR_CUENTACAT);
            string tipoAvaluo = WebUtils.QueryString(Constantes.PAR_TIPO_AVALUO);
            if (!string.IsNullOrEmpty(cuentaCatastral))
                GenerarAvaluo(cuentaCatastral, (TiposAvaluoEnum)Convert.ToInt32(tipoAvaluo));
        }
        catch (UserFailedException ex)
        {
            RedirectUtil.BaseURL = Constantes.URL_DESCARGAR_AVALUO;
            RedirectUtil.AddParameter(Constantes.PAR_ERROR, Constantes.PAR_ERROR_USEREXCEPTION);
            RedirectUtil.AddParameter(Constantes.PAR_ERROR_MSG, ex.Message);
            RedirectUtil.Go();
        }
    }

    /// <summary>
    /// Genera el xml base para crear un avalúo.
    /// </summary>
    /// <param name="cuentaCatastral">Cuenta Catastral</param>
    /// <param name="tipoAvaluo">El tipo avalúo (Catastral/Comercial)</param>
    private void GenerarAvaluo(string cuentaCatastral, TiposAvaluoEnum tipoAvaluo)
    {
        try
        {
            string[] codigosCuenta = CuentaCatastralToCodes(cuentaCatastral);

            ResponseFile responseFile = null;
            byte[] avaluo = null;

            if (Condiciones.Web(Constantes.FUN_PERITO))
            {
                AvaluosClient clienteAvaluos = new AvaluosClient();

                try
                {
                    avaluo = clienteAvaluos.GenerarAvaluo(codigosCuenta[0],
                                                         codigosCuenta[1],
                                                         codigosCuenta[2],
                                                         codigosCuenta[3],
                                                         (int)tipoAvaluo,
                                                         Convert.ToDecimal(Usuarios.IdPersona()), Constantes.FUN_PERITO, codigosCuenta[4]);
                }
                finally
                {
                    
                    clienteAvaluos.Disconnect();
                }
            }
            else if (Condiciones.Web(Constantes.FUN_SOCIEDAD))
            {
                AvaluosClient clienteAvaluos = new AvaluosClient();

                try
                {
                    avaluo = clienteAvaluos.GenerarAvaluo(codigosCuenta[0],
                                                         codigosCuenta[1],
                                                         codigosCuenta[2],
                                                         codigosCuenta[3],
                                                         (int)tipoAvaluo,
                                                         Convert.ToDecimal(Usuarios.IdPersona()), Constantes.FUN_SOCIEDAD, codigosCuenta[4]);
                }
                finally
                {
                    clienteAvaluos.Disconnect();
                }
            }
            if (avaluo.Any())
            {
                byte[] avaluoUTF8 = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, avaluo);
                if (tipoAvaluo == TiposAvaluoEnum.Catastral)
                {
                    responseFile = new ResponseFile(CrearNombreFicheroXML(Constantes.PAR_AVALUO_CATASTRAL_SHORT,ComponerCuenta(codigosCuenta[0], codigosCuenta[1], codigosCuenta[2], codigosCuenta[3])));
                }
                else if (tipoAvaluo == TiposAvaluoEnum.Comercial)
                {
                    responseFile = new ResponseFile(CrearNombreFicheroXML(Constantes.PAR_AVALUO_COMERCIAL_SHORT, ComponerCuenta(codigosCuenta[0], codigosCuenta[1], codigosCuenta[2], codigosCuenta[3])));
                }
                responseFile.SetContentType(ResponseFile.ContentTypes.WithOut);
                responseFile.SendFile(avaluoUTF8);
            }
            else
            {
                RedirectUtil.BaseURL = Constantes.URL_DESCARGAR_AVALUO;
                RedirectUtil.AddParameter(Constantes.PAR_ERROR, Constantes.PAR_ERROR_CC_INMUEBLE);
                RedirectUtil.Go();
            }
        }
        catch (Exception ex)
        {
            RedirectUtil.BaseURL = Constantes.URL_DESCARGAR_AVALUO;
            RedirectUtil.AddParameter(Constantes.PAR_ERROR, Constantes.PAR_ERROR_TOKEN);
            RedirectUtil.AddParameter(Constantes.PAR_ERROR_MSG, ex.Message);
            RedirectUtil.Go();
        }
    }

    /// <summary>
    /// Descompone la cuenta catastral en región, manzana, lote y unidad privativa
    /// </summary>
    /// <param name="cuentaCatastral">Cuenta Catastral</param>
    /// <returns>
    ///Devuelve un array con los codigos de la cuenta castatral en base a la cuenta. 0-> Region 1->
    /// Manzana 2-> Lote 3-> Unidad privativa.
    /// </returns>
    private string[] CuentaCatastralToCodes(string cuentaCatastral)
    {
        string[] resultado = new string[5];

        if (cuentaCatastral.Length == 12)
        {
            resultado[0] = cuentaCatastral.Substring(0, 3); //Region
            resultado[1] = cuentaCatastral.Substring(3, 3); //Manzana
            resultado[2] = cuentaCatastral.Substring(6, 2); //Lote
            resultado[3] = cuentaCatastral.Substring(8, 3); //Unidad  privativa
            resultado[4] = cuentaCatastral.Substring(11, 1); // Digito verificador
        }

        return resultado;
    }
}
