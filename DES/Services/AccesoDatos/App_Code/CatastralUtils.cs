using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral;
using System.Collections;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;
using System.ServiceModel;
using SIGAPred.Common;
using SIGAPred.Common.WCF;
using SIGAPred.Common.Extensions;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceRCON;


/// <summary>
/// Clase que agrupa los métodos que obtienen datos del esquema catastral
/// </summary>
public static class CatastralUtils
{
    #region Tablas hash
    /// <summary>
    /// Tabla hash que contiene los nombres y claves de las delegaciones del catálogo de delegaciones
    /// </summary>
    public static Hashtable CatDelegacionClavesNombresHash = new Hashtable(100);//Clave = claveDeleg; Valor: NombreDeleg
    /// <summary>
    /// Tabla hash que contiene los ids y nombres de las delegaciones del catálogo de delegaciones
    /// </summary>
    public static Hashtable CatDelegacionIdsNombresHash = new Hashtable(100);//Clave = IdDeleg; Valor: NombreDeleg
    /// <summary>
    /// Tabla hash que contiene las claves y los ids de las delegaciones del catálogo de delegaciones
    /// </summary>
    public static Hashtable CatDelegacionClavesIdsHash = new Hashtable(100);//Clave = claveDeleg; Valor: IdDeleg
    /// <summary>
    /// Tabla hash que contiene las claves de las colonias del catálogo de colonias
    /// </summary>
    public static Hashtable CatColoniaClavesHash = new Hashtable(100);
    /// <summary>
    /// Tabla hash que contiene los ids de las colonias del catálogo de colonias
    /// </summary>
    public static Hashtable CatColoniaIdsHash = new Hashtable(100);

    #endregion 

    #region Delegaciones
    /// <summary>
    /// Tabla Delegación del dataset Delegación-Colonia
    /// </summary>
    private static DseDelegacionColonia.DelegacionDataTable dtDelegacion;
    /// <summary>
    /// Propiedad que devuelve la tabla Delegación del dataset Delegación-Colonia
    /// </summary>
    public static DseDelegacionColonia.DelegacionDataTable DtDelegacion
    {
        get
        {
            if (dtDelegacion == null)
            {
                ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();

                try
                {
                    dtDelegacion = clienteCAS.GetDelegaciones();
                }
                finally
                {
                    clienteCAS.Disconnect();
                }
                
            }

            return dtDelegacion;
        }
    }

    /// <summary>
    /// Obtiene el iddelgacion de una delegación a partir de una clave delegación existente
    /// </summary>
    /// <param name="codDelegacion">Código de la delegación</param>
    /// <returns>IdDelegacion a partir del código de delegación</returns>
    public static decimal ObtenerIdDelegacionPorClave(string codDelegacion)
    {
        try
        {
          //  return -1;
            decimal idDeleg;
            if (CatDelegacionClavesIdsHash.Contains(codDelegacion))
            {
                idDeleg = CatDelegacionClavesIdsHash[codDelegacion].ToDecimal();
            }
            else
            {
                List<DseDelegacionColonia.DelegacionRow> rowsDelegaciones = DtDelegacion.Where(row => row.CLAVE.Equals(codDelegacion)).ToList();

                if (rowsDelegaciones.Any())
                {
                    DseDelegacionColonia.DelegacionRow delegRow = rowsDelegaciones.First();
                    idDeleg = delegRow.IDDELEGACION;
                    CatDelegacionClavesIdsHash.Add(codDelegacion, idDeleg.ToString());
                }
                else
                {
                    return -1;
                }
            }

            return idDeleg;
        }
        catch (Exception ex)
        {
            SIGAPred.FuentesExternas.Avaluos.Services.Negocio.ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene el nombre de una delegación asociada a un codDelegacion
    /// </summary>
    /// <param name="codDelegacion">código delegación</param>
    /// <returns>nombre de la delegación</returns>
    public static string ObtenerNombreDelegacion(string codDelegacion)
    {
        try
        {
            string nombreDeleg = string.Empty;
            if (CatDelegacionClavesNombresHash.ContainsKey(codDelegacion))
            {
                nombreDeleg = CatDelegacionClavesNombresHash[codDelegacion].ToString();
            }
            else
            {
                List<DseDelegacionColonia.DelegacionRow> rowsDelegaciones = DtDelegacion.Where(row => row.CLAVE.Equals(codDelegacion)).ToList();

                if (rowsDelegaciones.Any())
                {
                    DseDelegacionColonia.DelegacionRow delegRow = rowsDelegaciones.First();
                    string iddelegacion = delegRow.IDDELEGACION.ToString();
                    nombreDeleg = DtDelegacion.FindByIDDELEGACION(Convert.ToDecimal(iddelegacion)).NOMBRE;
                    CatDelegacionClavesNombresHash.Add(codDelegacion, nombreDeleg);
                }
            }

            return nombreDeleg;
        }
        catch (Exception ex)
        {
            SIGAPred.FuentesExternas.Avaluos.Services.Negocio.ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene el nombre de una delegación asociada a un idDelegacion
    /// </summary>
    /// <param name="idDelegacion">id de la delegación</param>
    /// <returns>nombre de la delegación</returns>
    public static string ObtenerNombreDelegacionPorIdDeleg(decimal idDelegacion)
    {
        try
        {
            string nombreDeleg = string.Empty;
            if (CatDelegacionIdsNombresHash.ContainsKey(idDelegacion))
            {
                nombreDeleg = CatDelegacionIdsNombresHash[idDelegacion].ToString();
            }
            else
            {
                DseDelegacionColonia.DelegacionRow delegacionRow = DtDelegacion.FindByIDDELEGACION(idDelegacion);

                if (delegacionRow != null)
                {
                    nombreDeleg = delegacionRow.NOMBRE;
                    CatDelegacionIdsNombresHash.Add(idDelegacion, nombreDeleg);
                }
            }

            return nombreDeleg;
        }
        catch (Exception ex)
        {
            SIGAPred.FuentesExternas.Avaluos.Services.Negocio.ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }
    #endregion 

    #region Colonias
    /// <summary>
    /// Tabla Colonia del dataset Delegación-Colonia
    /// </summary>
    private static DseDelegacionColonia.ColoniaDataTable dtColonia;
    /// <summary>
    /// Propiedad que devuelve la tabla Colonia del dataset Delegación-Colonia
    /// </summary>
    public static DseDelegacionColonia.ColoniaDataTable DtColonia
    {
        get
        {
            if (dtColonia == null)
            {
                ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();

                try
                {
                    dtColonia = clienteCAS.GetDelegacionColonia().Colonia;
                }
                finally
                {
                    clienteCAS.Disconnect();
                }
            }

            return dtColonia;
        }
    }

    /// <summary>
    /// Método que obtiene el dígito verificador a partir de la región, manzana, lote y unidad privativa
    /// </summary>
    /// <param name="region">region de la cuenta catastral</param>
    /// <param name="manzana">manzana de la cuenta catastral</param>
    /// <param name="lote">lote de la cuenta catastral</param>
    /// <param name="unidadPrivativa">unidad privativa de la cuenta catastral</param>
    /// <returns>digito verificador correspondiente a la cuenta catastral introducida</returns>
    public static string ObtenerDigitoVerificadorBD(string region, string manzana, string lote, string unidadPrivativa)
    {
        string digitoVerificador = "";

        ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();
        DseInmueble dsInmueble = clienteCAS.GetInmuebleByClave(region, manzana, lote, unidadPrivativa);

        if (dsInmueble.Inmueble.Any())
           digitoVerificador = dsInmueble.Inmueble[0].DIGITOVERIFICADOR;
 
        return digitoVerificador;
    
    }

    /// <summary>
    /// Obtiene el nombre de una colonia asociada a un idColonia
    /// </summary>
    /// <param name="idColonia">identificador de la colonia</param>
    /// <returns>nombre de la colonia asociado al identificador</returns>
    public static string ObtenerNombreColoniaPorIdColonia(decimal idColonia)
    {
        try
        {
            string nombreColonia = string.Empty;

            if (CatColoniaIdsHash.ContainsKey(idColonia))
            {
                nombreColonia = CatColoniaIdsHash[idColonia].ToString();
            }
            else
            {
                DseDelegacionColonia.ColoniaDataTable dtColonia = null;
                ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();

                try
                {
                    dtColonia = clienteCAS.GetDelegacionColoniaByidColonia((long)idColonia.ToInt()).Colonia;
                }
                finally
                {
                    clienteCAS.Disconnect();
                }

                if (dtColonia.Any())
                {
                    nombreColonia = dtColonia.First().NOMBRE;
                    CatColoniaIdsHash.Add(idColonia, nombreColonia);
                }
            }
            return nombreColonia;
        }
        catch (Exception ex)
        {
            SIGAPred.FuentesExternas.Avaluos.Services.Negocio.ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene el nombre de una colonia asociada a un idColonia
    /// </summary>
    /// <param name="idColonia">identificador de la colonia</param>
    /// <returns>nombre de la colonia asociado al identificador</returns>
    public static string ObtenerNombreColonia(string idColonia)
    {
        try
        {
            idColonia = idColonia.ToUpper();
            string nombreColonia = string.Empty;
            if (CatColoniaClavesHash.ContainsKey(idColonia))
            {
                nombreColonia = CatColoniaClavesHash[idColonia].ToString();
            }
            else
            {

                DseDelegacionColonia dseColonia = null;

                ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();

                try
                {
                    dseColonia = clienteCAS.GetDelegacionColoniaByidColonia((long)idColonia.ToInt());
                }
                finally
                {
                    clienteCAS.Disconnect();
                }

                if (dseColonia.Colonia.Any())
                {
                    nombreColonia = dseColonia.Colonia[0].NOMBRE;
                    CatColoniaClavesHash.Add(idColonia, nombreColonia);
                }
            }
            return nombreColonia;
        }
        catch (Exception ex)
        {
            SIGAPred.FuentesExternas.Avaluos.Services.Negocio.ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene el código de la delegación asociada al nombre de la delegación
    /// </summary>
    /// <param name="nombreDelegacion">nombre de la delegación</param>
    /// <returns>código de la delegación asociada al nombre de la delegación</returns>
    public static string ObtenerCodDelegacionPorNombre(string nombreDelegacion)
    {

        string codDelegacion;
        nombreDelegacion = nombreDelegacion.ToUpper();

        DseDelegacionColonia.DelegacionDataTable dseDelegacion = DtDelegacion;

        List<DseDelegacionColonia.DelegacionRow> rowsDelegaciones = DtDelegacion.Where(row => row.NOMBRE.Equals(nombreDelegacion)).ToList();

        if (rowsDelegaciones.Any())
        {
            codDelegacion = rowsDelegaciones.First().CLAVE;
        }
        else
        {
            return " ";
        }

        return codDelegacion;
    }

    /// <summary>
    /// Obtiene el identificador de la delegación asociado al nombre de la delegación
    /// </summary>
    /// <param name="nombreDelegacion">nombre de la delegación</param>
    /// <returns>identificador de la delegación asociado al nombre de la delegación </returns>
    public static decimal ObtenerIdDelegacionPorNombre(string nombreDelegacion)
    {

        decimal idDelegacion;
        nombreDelegacion = nombreDelegacion.ToUpper();

        DseDelegacionColonia.DelegacionDataTable dseDelegacion = DtDelegacion;

        List<DseDelegacionColonia.DelegacionRow> rowsDelegaciones = DtDelegacion.Where(row => row.NOMBRE.Equals(nombreDelegacion)).ToList();

        if (rowsDelegaciones.Any())
        {
            idDelegacion = rowsDelegaciones.First().IDDELEGACION;
        }
        else
        {
            return -1;
        }

        return idDelegacion;
    }

    /// <summary>
    /// A partir de un nombre de colonia y un código de delegación válidos devuelve el idcolonia
    /// </summary>
    /// <param name="nombreColonia">nombre de la colonia</param>
    /// <param name="codDelegacion">código de la delegación</param>
    /// <returns>identificador de la colonia asociado al nombre y código</returns>
    public static decimal ObtenerIdColoniaPorNombreyDelegacion(string nombreColonia, string codDelegacion)
    {
        decimal idColonia;
        nombreColonia = nombreColonia.ToUpper();

        DseDelegacionColonia.DelegacionDataTable dseDelegacion = DtDelegacion;
        DseDelegacionColonia.ColoniaDataTable dseColonias = DtColonia;
        decimal idDelegacion;

        List<DseDelegacionColonia.DelegacionRow> rowsDelegaciones = DtDelegacion.Where(row => row.CLAVE.Equals(codDelegacion)).ToList();

        if (rowsDelegaciones.Any())
        {
            idDelegacion = rowsDelegaciones.First().IDDELEGACION;
        }
        else
        {
            return -1;
        }

        List<DseDelegacionColonia.ColoniaRow> rowsColonias = DtColonia.Where(row => row.IDDELEGACION == idDelegacion &&
                                                                                    row.NOMBRE.ToUpper().Equals(nombreColonia.ToUpper())).ToList();

        if (rowsColonias.Any())
        {
            idColonia = rowsColonias.First().IDCOLONIA;
        }
        else
        {
            return -1;
        }

        return idColonia;

    }
    #endregion

    #region Instalaciones Especiales

    /// <summary>
    /// Tabla del cátalogo de instalaciones especiales del dataset de catálogos
    /// </summary>
    private static SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral.DseCatalogos.CatInstEspecialesDataTable dseInsteEsp;
    /// <summary>
    /// Propiedad que devuelve la tabla del catálogo de instalaciones especiales del dataset de catálogos
    /// </summary>
    public static SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral.DseCatalogos.CatInstEspecialesDataTable DseInsteEsp
    {
        get
        {
            if (dseInsteEsp == null)
            {
                ConsultaCatastralServiceClient clienteCAS = new ConsultaCatastralServiceClient();

                try
                {
                    dseInsteEsp = clienteCAS.GetInstEspecialesAvaluos().CatInstEspeciales;
                }
                finally
                {
                    clienteCAS.Disconnect();
                }
            }
            return dseInsteEsp;
        }
    }

    /// <summary>
    /// Obtiene del catálogo de instalaciones especiales la fila de la inst. especial
    /// que tiene la clave que se le pasa como parámetro
    /// </summary>
    /// <param name="ClaveInstalEsp">clave de instalación especial</param>
    /// <returns>fila del catálogo de instalaciones especiales del dataset de catálogos que tiene la clave que se le pasa como parametro</returns>
    public static SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral.DseCatalogos.CatInstEspecialesRow ObtenerInstEspecialByClave(string ClaveInstalEsp)
    { 
        foreach (SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral.DseCatalogos.CatInstEspecialesRow IeRow in DseInsteEsp)
        {
            if (IeRow.CLAVE.Equals(ClaveInstalEsp))
            {
                return IeRow;
            }
        }

        //Si no se ha encontrado nada devuelve null
        return null;
    }

    #endregion 
}

