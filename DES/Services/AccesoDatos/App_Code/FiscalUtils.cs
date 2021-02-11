using System;
using System.Collections;
using System.Linq;
using System.ServiceModel;
using SIGAPred.Common.Extensions;
using SIGAPred.Common.WCF;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral;
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceFiscal;

/// <summary>
/// Clase que agrupa los métodos que obtienen datos del esquema fiscal
/// </summary>
public static class FiscalUtils
{
    #region TABLAS HASH
    //--------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Tablas hash Con claves y descripciones
    /// </summary>
    private static Hashtable CatUsosHash = new Hashtable();
    private static Hashtable CatRangoNivHash = new Hashtable();
    private static Hashtable CatClasesHash = new Hashtable();
    private static Hashtable CatInstEspecialesHash = new Hashtable();
    private static Hashtable CatClasesVidasHash = new Hashtable();
    private static Hashtable CatEstadoConsevHash = new Hashtable();


    /// <summary>
    /// Tablas hash con claves y booleanos (El booleano indica si la clave está en el catálogo)
    /// </summary>
    private static Hashtable EstaEnCatUsosHash = new Hashtable();
    private static Hashtable EstaEnCatClaseConsHash = new Hashtable();
    private static Hashtable EstaEnCatConservacionHash = new Hashtable();
    private static Hashtable EstaEnCatClaseRangoNivHash = new Hashtable();
    private static Hashtable EstaEnCatEjercicioHash = new Hashtable();

    //--------------------------------------------------------------------------------------------------------
    #endregion

    #region OBTENER DESCRIPCIONES CAT

    /// <summary>
    /// A partir del cod de catdensidad obtiene su descripción
    /// </summary>
    /// <param name="codDensidad">código densidad</param>
    /// <returns>descripción del catálogo de densidad de población</returns>
    public static string ObtenerDescripcionDensidadPob(decimal codDensidad)
    {
        try
        {
            string descCaracteristica = string.Empty;
            DseAvaluosCatConsulta.FEXAVA_CATDENSIDADPOBDataTable dsCat = ApplicationCache.ObtenerCatalogoDensidadPob();
            if ((dsCat.FindByCODDENSIDADPOBLACION(codDensidad)).IsDESCRIPCIONNull())
                descCaracteristica = string.Empty;
            else
                descCaracteristica = dsCat.FindByCODDENSIDADPOBLACION(codDensidad).DESCRIPCION;
            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del cod de catnivelsocioeconomico obtiene su descripción
    /// </summary>
    /// <param name="NivelSocioEc">nivel socio-económico</param>
    /// <returns>descripción del catálogo de nivel socio-económico</returns>
    public static string ObtenerDescripcionNivelSocioEc(decimal NivelSocioEc)
    {
        try
        {
            string descCaracteristica = string.Empty;
            DseAvaluosCatConsulta.FEXAVA_CATNIVELSOCIOECONDataTable dsCat = ApplicationCache.ObtenerCatalogoNivelSocioEc();
            if ((dsCat.FindByCODNIVELSOCIOECONOMICO(NivelSocioEc)).IsDESCRIPCIONNull())
                descCaracteristica = string.Empty;
            else
                descCaracteristica = dsCat.FindByCODNIVELSOCIOECONOMICO(NivelSocioEc).DESCRIPCION;
            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del cod de catVigilanciaZona obtiene su descripción
    /// </summary>
    /// <param name="codVigilancia">código vigilancia</param>
    /// <returns>descripción del catálogo de vigilancia zona</returns>
    public static string ObtenerDescripcionVigilanciaZona(decimal codVigilancia)
    {
        try
        {
            string descCaracteristica = string.Empty;
            DseAvaluosCatConsulta.FEXAVA_CATVIGILANZONADataTable dsCat = ApplicationCache.ObtenerCatalogoVigilanciaZona();
            if ((dsCat.FindByCODVIGILANCIAZONA(codVigilancia)).IsDESCRIPCIONNull())
                descCaracteristica = string.Empty;
            else
                descCaracteristica = dsCat.FindByCODVIGILANCIAZONA(codVigilancia).DESCRIPCION;
            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }
    /// <summary>
    /// A partir del código de nomenclatura de la calle obtiene su descripción
    /// </summary>
    /// <param name="codNomenclaturaCalle">código de nomenclatura de la calle</param>
    /// <returns>descripción del catálogo de nomenclatura calle</returns>
    public static string ObtenerDescripcionNomenclaturaCalles(string codNomenclaturaCalle)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATNOMENCALLERow catNomenCalle = ApplicationCache.ObtenerCatalagoNomenCalle().FindByCODNOMENCLATURACALLE(codNomenclaturaCalle.ToDecimal());

            if (catNomenCalle != null && !catNomenCalle.IsDESCRIPCIONNull())
            {
                descCaracteristica = catNomenCalle.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Para un código instalación especial devuelve su descripción
    /// </summary>
    /// <param name="CodInstEsp">código instalación especial</param>
    /// <returns>descripción del catálogo de instalaciones especiales</returns>
    public static string ObtenerDescripcionInstalacionesEspeciales(decimal CodInstEsp)
    {
        try
        {
            string desc = string.Empty;
            //Comprobar si la clave está en la tabla hash
            if (CatInstEspecialesHash.ContainsKey(CodInstEsp))
            {
                desc = CatInstEspecialesHash[CodInstEsp].ToString();
            }
            else
            {
                DseCatalogos.CatInstEspecialesDataTable DseInsteEsp = CatastralUtils.DseInsteEsp;
                DseCatalogos.CatInstEspecialesRow DseInsteEspRow = DseInsteEsp.FindByCODINSTESPECIALES(CodInstEsp);
                desc = DseInsteEspRow.DESCRIPCION;
                //Añadir el registro a la tablaHash
                CatInstEspecialesHash.Add(CodInstEsp, desc);
            }

            return desc;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código de drenaje inmueble obtiene su descripción
    /// </summary>
    /// <param name="codDrenaje">código de drenaje inmueble</param>
    /// <returns>descripción del catálogo de drenaje inmueble</returns>
    public static string ObtenerDescripcionDrenaje(decimal codDrenaje)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATDRENAINMUEBRow catDrenaInmueble = ApplicationCache.ObtenerCatalogoDrenajeInm().FindByCODDRENAJEINMUEBLE(codDrenaje);

            if (catDrenaInmueble != null && !catDrenaInmueble.IsDESCRIPCIONNull())
            {
                descCaracteristica = catDrenaInmueble.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código de drenaje pluvial obtiene su descripción
    /// </summary>
    /// <param name="codDrenaje">código de drenaje pluvial</param>
    /// <returns>descripción del catálogo de drenaje pluvial</returns>
    public static string ObtenerDescripcionDrenajePluvial(decimal codDrenaje)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATDRENJPLUVRow catDrenaPluvial = ApplicationCache.ObtenerCatalogoDrenajePluv().FindByCODDRENAJEPLUVIAL(codDrenaje);

            if (catDrenaPluvial != null && !catDrenaPluvial.IsDESCRIPCIONNull())
            {
                descCaracteristica = catDrenaPluvial.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de agua potable obtiene su descripción
    /// </summary>
    /// <param name="codAguaPot">código de agua potable</param>
    /// <returns>descripción del catálogo de agua potable</returns>
    public static string ObtenerDescripcionAguaPotable(decimal codAguaPot)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATAGUAPOTABLERow catAguaPotable = ApplicationCache.ObtenerCatalogoAguaPotable().FindByCODAGUAPOTABLE(codAguaPot);

            if (catAguaPotable != null && !catAguaPotable.IsDESCRIPCIONNull())
            {
                descCaracteristica = catAguaPotable.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de suministro eléctrico obtiene su descripción
    /// </summary>
    /// <param name="codSuministroElec">código de suministro eléctrico</param>
    /// <returns>descripción del catálogo de suministro eléctrico</returns>
    public static string ObtenerDescripcionSuministroElectrico(decimal codSuministroElec)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATSUMINISELECRow catSuministroElectrico = ApplicationCache.ObtenerCatalogoSuministroElec().FindByCODSUMINISTROELECTRICO(codSuministroElec);

            if (catSuministroElectrico != null && !catSuministroElectrico.IsDESCRIPCIONNull())
            {
                descCaracteristica = catSuministroElectrico.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de banquetas obtiene su descripción
    /// </summary>
    /// <param name="codBanquetas">código de banquetas</param>
    /// <returns>descripción del catálogo de banquetas</returns>
    public static string ObtenerDescripcionBanquetas(decimal codBanquetas)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATBANQUETASRow catBanquetas = ApplicationCache.ObtenerCatalogoBanquetas().FindByCODBANQUETAS(codBanquetas);

            if (catBanquetas != null && !catBanquetas.IsDESCRIPCIONNull())
            {
                descCaracteristica = catBanquetas.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de suministro eléctrico obtiene su descripción
    /// </summary>
    /// <param name="codSumTelefono">código suministro eléctrico</param>
    /// <returns>descripción del catálogo de suministro eléctrico</returns>
    public static string ObtenerDescripcionSuministroTel(decimal codSumTelefono)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATSUMINISTTELRow catSuministroTelefonico = ApplicationCache.ObtenerCatalogoSuministroTel().FindByCODSUMINISTROTELEFONICA(codSumTelefono);

            if (catSuministroTelefonico != null && !catSuministroTelefonico.IsDESCRIPCIONNull())
            {
                descCaracteristica = catSuministroTelefonico.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de gas natural obtiene su descripción
    /// </summary>
    /// <param name="codGasNatural">código de gas natural</param>
    /// <returns>descripción del catálogo de gas natural</returns>
    public static string ObtenerDescripcionGasNatural(decimal codGasNatural)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATGASNATURALRow catGasNatural = ApplicationCache.ObtenerCatalogoGasNatural().FindByCODGASNATURAL(codGasNatural);

            if (catGasNatural != null && !catGasNatural.IsDESCRIPCIONNull())
            {
                descCaracteristica = catGasNatural.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de guarniciones obtiene su descripción
    /// </summary>
    /// <param name="codGuarniciones">código de guarniciones</param>
    /// <returns>descripción del catálogo de guarniciones</returns>
    public static string ObtenerDescripcionGuarniciones(decimal codGuarniciones)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATGUARNICIONESRow catGuarniciones = ApplicationCache.ObtenerCatalogoGuarniciones().FindByCODGUARNICIONES(codGuarniciones);

            if (catGuarniciones != null && !catGuarniciones.IsDESCRIPCIONNull())
            {
                descCaracteristica = catGuarniciones.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de señalizacion vías obtiene su descripción
    /// </summary>
    /// <param name="codSenalizacionVias">código señalizacion vías</param>
    /// <returns>descripción del catálogo de señalizacion vías</returns>
    public static string ObtenerDescripcionSenalizacionVias(string codSenalizacionVias)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATSENALIZVIASRow catSenalizacionVias = ApplicationCache.ObtenerCatalogoSenalizacionVias().FindByCODSENALIZACIONVIAS(codSenalizacionVias);

            if (catSenalizacionVias != null && !catSenalizacionVias.IsDESCRIPCIONNull())
            {
                descCaracteristica = catSenalizacionVias.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de Vialidades obtiene su descripción
    /// </summary>
    /// <param name="codVialidades">código de Vialidades</param>
    /// <returns>descripción del catálogo de Vialidades</returns>
    public static string ObtenerDescripcionVialidades(decimal codVialidades)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATVIALIDADESRow catVialidades = ApplicationCache.ObtenerCatalogoVialidades().FindByCODVIALIDADES(codVialidades);

            if (catVialidades != null && !catVialidades.IsDESCRIPCIONNull())
            {
                descCaracteristica = catVialidades.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de acometida inmueble obtiene su descripción
    /// </summary>
    /// <param name="codAcometida">código de acometida</param>
    /// <returns>descripción del catálogo de acometida</returns>
    public static string ObtenerDescripcionAcometidaInm(decimal codAcometida)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATACOMETIDINMRow catAcometidaInmueble = ApplicationCache.ObtenerCatalogoAcometidaInm().FindByCODACOMETIDAINMUEBLE(codAcometida);

            if (catAcometidaInmueble != null && !catAcometidaInmueble.IsDESCRIPCIONNull())
            {
                descCaracteristica = catAcometidaInmueble.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de alumbrado público obtiene su descripción
    /// </summary>
    /// <param name="codAlumbrado">código de alumbrado público</param>
    /// <returns>descripción del catálogo de alumbrado público</returns>
    public static string ObtenerDescripcionAlumbradoPublico(decimal codAlumbrado)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATALUMBRADOPUBLICORow catAlumbradoPublico = ApplicationCache.ObtenerCatalogoAlumbradoPublico().FindByCODALUMBRADOPUBLICO(codAlumbrado);

            if (catAlumbradoPublico != null && !catAlumbradoPublico.IsDESCRIPCIONNull())
            {
                descCaracteristica = catAlumbradoPublico.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de topografía obtiene su descripción
    /// </summary>
    /// <param name="codTopografia">código de topografía</param>
    /// <returns>descripción del catálogo de topografía</returns>
    public static string ObtenerDescripcionTopografia(decimal codTopografia)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATTOPOGRAFIARow catTopografia = ApplicationCache.ObtenerCatalogoTopografia().FindByCODTOPOGRAFIA(codTopografia);

            if (catTopografia != null && !catTopografia.IsDESCRIPCIONNull())
            {
                descCaracteristica = catTopografia.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de densidad habitacional obtiene su descripción
    /// </summary>
    /// <param name="codDensidad">código de densidad habitacional</param>
    /// <returns>descripción del catálogo de densidad habitacional</returns>
    public static string ObtenerDescripcionDensidadHab(decimal codDensidad)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATDENSIDADHABRow catDensidadHabitacional = ApplicationCache.ObtenerCatalogoDensidadHab().FindByCODDENSIDADHABITACIONAL(codDensidad);

            if (catDensidadHabitacional != null && !catDensidadHabitacional.IsDESCRIPCIONNull())
            {
                descCaracteristica = catDensidadHabitacional.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de clase vida obtiene su descripción
    /// </summary>
    /// <param name="codClaseVida">código de clase vida</param>
    /// <returns>descripción del catálogo de clase vida</returns>
    public static string ObtenerDescripcionClasesVidas(decimal codClaseVida)
    {
        try
        {
            string descCaracteristica = string.Empty;
            //Comprobar si está en la tablaHash asociada
            if (CatClasesVidasHash.ContainsKey(codClaseVida))
            {
                descCaracteristica = CatClasesVidasHash[codClaseVida].ToString();
            }
            else
            {
                DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable dsCat = ApplicationCache.ObtenerCatalogoClaseUso();
                descCaracteristica = dsCat.FindByIDUSOCLASEEJERCICIO(codClaseVida).EDADUTIL.ToString();

                //Añadir el registro a la tablaHash
                CatClasesVidasHash.Add(codClaseVida, descCaracteristica);
            }
            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de estado conservación obtiene su descripción
    /// </summary>
    /// <param name="codEstadoConserv">código de estado conservación</param>
    /// <returns>descripción del catálogo de estado conservación</returns>
    public static string ObtenerDescripcionEstadoConservacion(decimal codEstadoConserv)
    {
        try
        {
            string descCaracteristica = string.Empty;

            if (CatEstadoConsevHash.ContainsKey(codEstadoConserv))
            {
                descCaracteristica = CatEstadoConsevHash[codEstadoConserv].ToString();
            }
            else
            {
                DseAvaluosCatConsulta.FEXAVA_CATESTADOCONSERVRow catEstadoConservacion = ApplicationCache.ObtenerCatalogoEstadoConserv().FindByCODESTADOCONSERVACION(codEstadoConserv);

                if (catEstadoConservacion != null && !catEstadoConservacion.IsDESCRIPCIONNull())
                {
                    descCaracteristica = catEstadoConservacion.DESCRIPCION;
                }

                //Añadir el registro a la tablaHash
                CatEstadoConsevHash.Add(codEstadoConserv, descCaracteristica);
            }
            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de régimen de propiedad obtiene su descripción
    /// </summary>
    /// <param name="codRegimenPropiedad">código de régimen de propiedad</param>
    /// <returns>descripción del catálogo de régimen de propiedad</returns>
    public static string ObtenerDescripcionRegimenPropiedad(decimal codRegimenPropiedad)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATREGIMENPROPRow catRegimenPropiedad = ApplicationCache.ObtenerCatalogoRegimenProp().FindByCODREGIMENPROPIEDAD(codRegimenPropiedad);

            if (catRegimenPropiedad != null && !catRegimenPropiedad.IsDESCRIPCIONNull())
            {
                descCaracteristica = catRegimenPropiedad.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de clasificación zona obtiene su descripción
    /// </summary>
    /// <param name="codClase">código de clase zona</param>
    /// <returns>descripción del catálogo de clasificación zona</returns>
    public static string ObtenerDescripcionClasificacionZona(decimal codClase)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATCLASIFICACIONZONARow catClaseZona = ApplicationCache.ObtenerCatalogoClasificacionZona().FindByCODCLASIFICACIONZONA(codClase);

            if (catClaseZona != null && !catClaseZona.IsDESCRIPCIONNull())
            {
                descCaracteristica = catClaseZona.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de recolección de basura obtiene su descripción
    /// </summary>
    /// <param name="codRecoleccion">código recolección de basura</param>
    /// <returns>descripción del catálogo de recolección de basura</returns>
    public static string ObtenerDescripcionRecoleccionBasura(decimal codRecoleccion)
    {
        try
        {
            string descCaracteristica = string.Empty;

            DseAvaluosCatConsulta.FEXAVA_CATRECOLECCIONBASURARow catRecoleccionBasura = ApplicationCache.ObtenerCatalogoRecBasura().FindByCODRECOLECCIONBASURA(codRecoleccion);

            if (catRecoleccionBasura != null && !catRecoleccionBasura.IsDESCRIPCIONNull())
            {
                descCaracteristica = catRecoleccionBasura.DESCRIPCION;
            }

            return descCaracteristica;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de rango de niveles obtiene su descripción
    /// </summary>
    /// <param name="codRangoNiv">código de rango de niveles</param>
    /// <returns>descripción del catálogo de rango de niveles</returns>
    public static string ObtenerDescripcionRangoNiveles(decimal codRangoNiv)
    {
        try
        {
            string desc = string.Empty;

            //Comprobar si está en la tablaHash asociada
            if (CatRangoNivHash.ContainsKey(codRangoNiv))
            {
                desc = CatRangoNivHash[codRangoNiv].ToString();
            }
            else
            {
                AdministracionClient clienteFIS = new AdministracionClient();
                DseAdministracion.FIS_RANGONIVELESEJERCICIODataTable dtRangoNiveles = null;

                try
                {
                    dtRangoNiveles = clienteFIS.SolicitarObtenerRangoNivelesEjercicioByIdRangoNivelesEjercicio(codRangoNiv);
                }
                finally
                {
                    clienteFIS.Disconnect();
                }

                if (dtRangoNiveles.Any() && !dtRangoNiveles[0].IsCODIGODESCRIPCIONNull())
                {
                    desc = dtRangoNiveles[0].CODRANGONIVELES;
                }

                CatRangoNivHash.Add(codRangoNiv, desc);
            }
            return desc;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir del código del catálogo de clases ejercicio obtiene su descripción
    /// </summary>
    /// <param name="codClase">código de clase</param>
    /// <returns>descripción del catálogo de clases ejercicio</returns>
    public static string ObtenerDescripcionClaseConstruccion(decimal codClase)
    {
        try
        {
            string desc = string.Empty;

            if (CatClasesHash.ContainsKey(codClase))
            {
                desc = CatClasesHash[codClase].ToString();
            }
            else
            {
                AdministracionClient clienteFIS = new AdministracionClient();

                try
                {
                    desc = clienteFIS.SolicitarObtenerClasesEjercicioByIdClasesEjercicio(codClase)[0].CLASE;
                }
                finally
                {
                    clienteFIS.Disconnect();
                }

                CatClasesHash.Add(codClase, desc);

            } return desc;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// A partir código del catálogo de usos ejercicio obtiene su descripción
    /// </summary>
    /// <param name="codUso">código de usos</param>
    /// <returns>descripción del catálogo de usos</returns>
    public static string ObtenerDescripcionUsosSuelo(decimal codUso)
    {
        try
        {
            string desc = string.Empty;

            //Comprobar si está en la tablaHash asociada
            if (CatUsosHash.ContainsKey(codUso))
            {
                desc = CatUsosHash[codUso].ToString();
            }
            else
            {
                AdministracionClient clienteFIS = new AdministracionClient();
                DseAdministracion.FIS_USOSEJERCICIODataTable dtUsosEjercicio = null;

                try
                {
                    dtUsosEjercicio = clienteFIS.SolicitarObtenerUsosEjercicioByIdUsosEjercicio(codUso);
                }
                finally
                {
                    clienteFIS.Disconnect();
                }

                if (dtUsosEjercicio.Any() && !dtUsosEjercicio[0].IsCODIGODESCRIPCIONNull())
                {
                    desc = dtUsosEjercicio[0].DESCRIPCION;
                }

                //Añadir el registro a la tablaHash
                CatUsosHash.Add(codUso, desc);
            }

            return desc;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    #endregion

    #region MÉTODOS COMPROBAR DATO PERTENECE A CATÁLOGO

    private static DseAdministracion.FIS_CATCLASESDataTable dt_ClaseConst = null;
    private static DseAdministracion.FIS_CATCLASESDataTable Dt_ClaseConst
    {
        get
        {
            if (dt_ClaseConst == null)
            {
                AdministracionClient clienteFIS = new AdministracionClient();

                try
                {
                    dt_ClaseConst = clienteFIS.SolicitarObtenerCatClases();
                }
                finally
                {
                    clienteFIS.Disconnect();
                }
            }
            return dt_ClaseConst;
        }
    }

    /// <summary>
    /// Comprueba si el elemento que se le pasa, está en el catálogo clases construcción
    /// </summary>
    /// <param name="elem">elemento</param>
    /// <returns>true/false en función de la existencia o no del elemento</returns>
    public static bool EstaEnCatClaseConstruccion(string elem)
    {
        bool resul = false;
        if (EstaEnCatClaseConsHash.Contains(elem))
        {
            resul = ObtenerBool(EstaEnCatClaseConsHash[elem].ToString());
        }
        else
        {
            DseAdministracion.FIS_CATCLASESDataTable catClases = Dt_ClaseConst;
            string codActual;
            for (int i = 0; i < catClases.Count; i++)
            {
                codActual = catClases[i].CODCLASE;

                if (codActual.Equals(elem))
                {//No está en la enumeración
                    resul = true;
                }
            }
            EstaEnCatClaseConsHash.Add(elem, resul.ToString());
        }
        return resul;
    }

    private static DseAdministracion.FIS_CATUSOSDataTable dt_ClaseUso = null;
    private static DseAdministracion.FIS_CATUSOSDataTable Dt_ClaseUso
    {
        get
        {
            if (dt_ClaseUso == null)
            {
                AdministracionClient clienteFIS = new AdministracionClient();

                try
                {
                    dt_ClaseUso = clienteFIS.SolicitarObtenerCatUsos();
                }
                finally
                {
                    clienteFIS.Disconnect();
                }
            }
            return dt_ClaseUso;
        }
    }

    /// <summary>
    /// Comprueba si el elemento que se le pasa, está en el catálogo clases uso
    /// </summary>
    /// <param name="elem">elemento</param>
    /// <returns>true/false en función de la existencia o no del elemento</returns>
    public static bool EstaEnCatClaseUso(string elem)
    {
        bool resul = false;

        if (EstaEnCatUsosHash.Contains(elem))
        {
            resul = ObtenerBool(EstaEnCatUsosHash[elem].ToString());
        }
        else
        {
            DseAdministracion.FIS_CATUSOSDataTable catUso = Dt_ClaseUso;
            string codActual;
            for (int i = 0; i < catUso.Count; i++)
            {
                codActual = catUso[i].CODUSO;

                if (codActual.Trim().Equals(elem.Trim()))
                {//No está en la enumeración
                    resul = true;
                }
            }
            EstaEnCatUsosHash.Add(elem, resul.ToString());
        }

        return resul;
    }

    private static DseAdministracion.FIS_CATRANGONIVELESDataTable dt_CatRangoNiveles = null;
    private static DseAdministracion.FIS_CATRANGONIVELESDataTable Dt_CatRangoNiveles
    {
        get
        {
            if (dt_CatRangoNiveles == null)
            {
                AdministracionClient clienteFIS = new AdministracionClient();

                try
                {
                    dt_CatRangoNiveles = clienteFIS.SolicitarObtenerCatRangoNiveles();
                }
                finally
                {
                    clienteFIS.Disconnect();
                }
            }
            return dt_CatRangoNiveles;
        }
    }

    /// <summary>
    /// Comprueba si el elemento que se le pasa, está en el catálogo clases rango niveles
    /// </summary>
    /// <param name="elem">elemento</param>
    /// <returns>true/false en función de la existencia o no del elemento</returns>
    public static bool EstaEnCatClaseRangoNiveles(string elem)
    {
        bool resul = false;
        if (EstaEnCatClaseRangoNivHash.Contains(elem))
        {
            resul = ObtenerBool(EstaEnCatClaseRangoNivHash[elem].ToString());
        }
        else
        {
            DseAdministracion.FIS_CATRANGONIVELESDataTable catRango = Dt_CatRangoNiveles;
            string codActual;
            for (int i = 0; i < catRango.Count; i++)
            {
                codActual = catRango[i].CODRANGONIVELES;

                if (codActual.Equals(elem))
                {//No está en la enumeración
                    resul = true;
                }
            }

            EstaEnCatClaseRangoNivHash.Add(elem, resul.ToString());
        }
        return resul;
    }


    /// <summary>
    /// Devuelve true si la enumeración cat-Conservación contiene el elemento que se le pasa como parámetro
    /// </summary>
    /// <param name="elem">elemento</param>
    /// <returns>true/false en función de la existencia o no del elemento</returns>
    public static bool EstaEnCatConservacion(decimal elem)
    {
        bool resul = false;
        if (EstaEnCatConservacionHash.Contains(elem))
        {
            resul = ObtenerBool(EstaEnCatConservacionHash[elem].ToString());
        }
        else
        {
            DseAvaluosCatConsulta.FEXAVA_CATESTADOCONSERVDataTable catEstados = ApplicationCache.DseCatalogosConsulta.FEXAVA_CATESTADOCONSERV;
            decimal codActual;
            for (int i = 0; i < catEstados.Count; i++)
            {
                codActual = (decimal)catEstados[i].CODESTADOCONSERVACION;

                if (codActual.Equals(elem))
                {//No está en la enumeración
                    resul = true;
                }
            }
            EstaEnCatConservacionHash.Add(elem, resul.ToString());
        }

        return resul;
    }

    private static DseAdministracion.FIS_EJERCICIODataTable dt_CatEjercicio = null;
    private static DseAdministracion.FIS_EJERCICIODataTable Dt_CatEjercicio
    {
        get
        {
            if (dt_CatEjercicio == null)
            {
                AdministracionClient clienteFIS = new AdministracionClient();

                try
                {
                    dt_CatEjercicio = clienteFIS.SolicitarObtenerEjercicios();
                }
                finally
                {
                    clienteFIS.Disconnect();
                }
            }
            return dt_CatEjercicio;
        }
    }

    /// <summary>
    /// Comprueba si la fecha que se le pasa, está en el catálogo ejercicio
    /// </summary>
    /// <param name="date">fecha</param>
    /// <returns>true/false en función de la existencia o no de la fecha en el catálogo</returns>
    public static bool EstaEnCatEjercicio(DateTime date)
    {
        bool resul = false;
        string dateStr = date.Date.ToString();

        if (EstaEnCatEjercicioHash.Contains(dateStr))
        {
            resul = ObtenerBool(EstaEnCatEjercicioHash[dateStr].ToString());
        }
        else
        {

            DseAdministracion.FIS_EJERCICIODataTable catEjercicios = Dt_CatEjercicio;

            for (int i = 0; i < catEjercicios.Count; i++)
            {
                DateTime fechaIni = catEjercicios[i].FECHAINICIO;
                DateTime fechaFin = catEjercicios[i].FECHAFIN;
                if (date >= fechaIni && date <= fechaFin)
                {//No está en la enumeración
                    resul = true;
                }
            }

            EstaEnCatEjercicioHash.Add(dateStr, resul.ToString());
        }
        return resul;
    }

    /// <summary>
    /// Devuelve true si existe un registro en el catálogo de uso ejercicio, asociado al código, fecha e idUsoEjercicio
    /// </summary>
    /// <param name="codUso">código de uso</param>
    /// <param name="fecha">fecha</param>
    /// <param name="idUsoEjercicio">identificador uso ejercicio</param>
    /// <returns>true/false en función de la existencia o no del código, fecha e idUsoEjercicio, en el catálogo</returns>
    public static bool ExisteCatUsoEjercicio(string codUso, DateTime fecha, out decimal? idUsoEjercicio)
    {
        try
        {
            if (EstaEnCatClaseUso(codUso) && EstaEnCatEjercicio(fecha))
            {
                idUsoEjercicio = SolicitarObtenerIdUsosByCodeAndAno(fecha.Date, codUso).ToDecimal();
                return true;
            }
            else
            {
                idUsoEjercicio = null;
                return false;
            }
        }
        catch (Exception ex)
        {
            //Capturar excepciónd data not found de bd
            if (ex.Message.Contains("ORA-01403"))
            {
                idUsoEjercicio = -1;
                return false;
            }
            else
                throw ex;
        }
    }

    /// <summary>
    /// Devuelve true si existe un registro en el catálogo de clase ejercicio, asociado al código, fecha e idClaseEjercicio
    /// </summary>
    /// <param name="codClase">código clase</param>
    /// <param name="fecha">fecha</param>
    /// <param name="idClaseEjercicio">identificador clase ejercicio</param>
    /// <returns>true/false en función de la existencia o no del código, fecha e idClaseEjercicio, en el catálogo</returns>
    public static bool ExisteCatClaseEjercicio(string codClase, DateTime fecha, out decimal? idClaseEjercicio)
    {
        try
        {
            if (EstaEnCatClaseConstruccion(codClase) && EstaEnCatEjercicio(fecha))
            {
                idClaseEjercicio = SolicitarObtenerIdClasesByCodeAndAno(fecha.Date, codClase).ToDecimal();
                return true;
            }
            else
            {
                idClaseEjercicio = null;
                return false;
            }
        }
        catch (Exception ex)
        {
            //Capturar excepciónd data not found de bd
            if (ex.Message.Contains("ORA-01403"))
            {
                idClaseEjercicio = -1;
                return false;
            }
            else
                throw ex;
        }
    }

    /// <summary>
    /// Devuelve true si existe un registro en el catálogo rango niveles ejercicio, asociado al código rango niveles y la fecha
    /// </summary>
    /// <param name="codRangoNiveles">código rango niveles</param>
    /// <param name="fecha">fecha</param>
    /// <returns>true/false en función de la existencia o no del código rango niveles y la fecha, en el catálogo</returns>
    public static bool ExisteCatRangoNivelesEjercicio(string codRangoNiveles, DateTime fecha)
        {
            try
            {
                if (EstaEnCatClaseRangoNiveles(codRangoNiveles) && EstaEnCatEjercicio(fecha))
                {
                    int idRangoNiveles = SolicitarObtenerIdRangoNivelesByCodeAndAno(fecha.Date, codRangoNiveles);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //Capturar excepciónd data not found de bd
                if (ex.Message.Contains("ORA-01403"))
                {
                    return false;
                }
                else
                    throw ex;
            }
        }

    /// <summary>
    /// Convierte el str ("True","False")  a boolean
    /// </summary>
    /// <param name="str">texto</param>
    /// <returns>true/false en función del texto</returns>
    public static bool ObtenerBool(string str)
    {
        if (str.Equals(true.ToString()))
            return true;
        else
            return false;
    }


    #endregion

    #region OTROS
    /// <summary>
    /// Solicita/obtiene el identificador usos asociado a la fecha y al código uso
    /// </summary>
    /// <param name="fecha">fecha</param>
    /// <param name="codUso">código uso</param>
    /// <returns>identificador de usos</returns>
    public static int SolicitarObtenerIdUsosByCodeAndAno(DateTime fecha, String codUso)
    {
        AdministracionClient clienteFIS = new AdministracionClient();

        try
        {

            return clienteFIS.SolicitarObtenerIdUsosByCodeAndAno(fecha, codUso);
        }
        finally
        {
            clienteFIS.Disconnect();
        }
    }

    /// <summary>
    /// Solicita/obtiene el identificador rango niveles asociado a la fecha y al código rango niveles
    /// </summary>
    /// <param name="fecha">fecha</param>
    /// <param name="codRangoNiveles">código rango niveles</param>
    /// <returns>identificador rango niveles</returns>
    public static int SolicitarObtenerIdRangoNivelesByCodeAndAno(DateTime fecha, String codRangoNiveles)
    {
        AdministracionClient clienteFIS = new AdministracionClient();

        try
        {
            return clienteFIS.SolicitarObtenerIdRangoNivelesByCodeAndAno(fecha, codRangoNiveles);
        }
        finally
        {
            clienteFIS.Disconnect();
        }
    }

    /// <summary>
    /// Solicita/obtiene el identificador clases asociado a la fecha y al código clase
    /// </summary>
    /// <param name="fecha">fecha</param>
    /// <param name="codClase">código clase</param>
    /// <returns>identificador clases</returns>
    public static int SolicitarObtenerIdClasesByCodeAndAno(DateTime fecha, String codClase)
    {
        AdministracionClient clienteFIS = new AdministracionClient();

        try
        {
            return clienteFIS.SolicitarObtenerIdClasesByCodeAndAno(fecha, codClase);
        }
        finally
        {
            clienteFIS.Disconnect();
        }
    }


        public static decimal ValidarValorUnitarioSuelo(string region,string  manzana,string  lote,int anio,int periodo)
        {
            AdministracionClient clienteFIS = new AdministracionClient();

            try
            {
                return clienteFIS.ObtenerValorUnitarioSuelo(region, manzana, lote, anio, periodo);
            }
            finally
            {
                clienteFIS.Disconnect();
            }
        }




        public static decimal ValidarValorUnitarioConstruccion(string codUso,string codClase,string rangoNiv,int numNiv,int anio,int periodo)
        {
            AdministracionClient clienteFIS = new AdministracionClient();

            try
            {
                 DseAdministracion.FIS_VUCDataTable dtVUC = clienteFIS.ObtenerValorUnitario(codUso, codClase, rangoNiv, numNiv, anio, periodo);
                 if (dtVUC.Any())
                     return dtVUC[0].VALORUNITARIO;
                 else
                     return 0;
            }
            finally
            {
                clienteFIS.Disconnect();
            }
        }
        #endregion 
    }
