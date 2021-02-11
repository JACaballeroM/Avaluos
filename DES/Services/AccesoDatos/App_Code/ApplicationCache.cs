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
using SIGAPred.FuentesExternas.Avaluos.Services.ServiceCatastral;
using System.Collections;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluosCatConsultaTableAdapters;
using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos.DseAvaluoConsultaTableAdapters;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;
using System.ServiceModel;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio;
/// <summary>
/// Clase para gestionar la cache de la aplicación Web.
/// </summary>
public static class ApplicationCache
{
    #region TableAdapters Catalogos
    private static FEXAVA_CATALUMBRADOPUBLICOTableAdapter catAlumbradoPublicoTA = null;
    private static FEXAVA_CATBANQUETASTableAdapter catBanquetasTA = null;
    private static FEXAVA_CATCLASESCONSTTableAdapter catClasesConstruccionTA = null;
    private static FEXAVA_CATCLASIFICACIONZONATableAdapter catClasificacionZonaTA = null;
    private static FEXAVA_CATDENSIDADHABTableAdapter catDensidadHabitacionalTA = null;
    private static FEXAVA_CATDENSIDADPOBTableAdapter catDensidadPoblacionTA = null;
    private static FEXAVA_CATDRENJPLUVTableAdapter catDrenajePluvialTA = null;
    private static FEXAVA_CATESTADOCONSERVTableAdapter catEstadoConservacionTA = null;
    private static FEXAVA_CATESTADOSAVALUOTableAdapter catEstadosAvaluoTA = null;
    private static FEXAVA_CATGASNATURALTableAdapter catGasNaturalTA = null;
    private static FEXAVA_CATUSOSCONSTTableAdapter catUsosConstruccionesTA = null;
    private static FEXAVA_CATVIALIDADESTableAdapter catVialidadesTA = null;
    private static FEXAVA_CATGUARNICIONESTableAdapter catGuarnicionesTA = null;
    private static FEXAVA_CATMODOCONSTRUCCIONTableAdapter catModoConstruccionTA = null;
    private static FEXAVA_CATNIVELSOCIOECONTableAdapter catNivelSocioeconomicoTA = null;
    private static FEXAVA_CATRANGONIVELESTableAdapter catRangoNivelesTA = null;
    private static FEXAVA_CATTIPOELEMENTOTableAdapter catTipoElementoTA = null;
    private static FEXAVA_CATTIPOFUNCIONTableAdapter catTipoFuncionTA = null;
    private static FEXAVA_CATTIPOTableAdapter catTipoTA = null;
    private static FEXAVA_CATTIPOTERRENOTableAdapter catTipoTerrenoTA = null;
    private static FEXAVA_CATTOPOGRAFIATableAdapter catTopografiaTA = null;
    private static FEXAVA_CATAGUAPOTABLETableAdapter catAguaPotableTA = null;
    private static FEXAVA_CATDRENAINMUEBTableAdapter catDrenajeInmuebleTA = null;
    private static FEXAVA_CATSUMINISELECTableAdapter catSuministroElecTA = null;
    private static FEXAVA_CATACOMETIDINMTableAdapter catAcometidaInmTA = null;
    private static FEXAVA_CATNOMENCALLETableAdapter catNomenCalleTA = null;
    private static FEXAVA_CATCLASEUSOTableAdapter catClaseUsoTA = null;
    private static FEXAVA_CATSUMINISTTELTableAdapter catSuministroTelTA = null;
    private static FEXAVA_CATSENALIZVIASTableAdapter catSenalizacionviasTA = null;
    private static FEXAVA_CATVIGILANZONATableAdapter catVigilanciaZonaTA = null;
    private static FEXAVA_CATREGIMENPROPTableAdapter catRegimenPropiedadTA = null;
    private static FEXAVA_CATRECOLECCIONBASURATableAdapter catRecoleccionBasuraTA = null;
    
    /// <summary>
    /// Propiedad que devuelve el catálogo de alumbrado público
    /// </summary>
    public static FEXAVA_CATALUMBRADOPUBLICOTableAdapter CatAlumbradoPublicoTA
    {
        get
        {
            if (catAlumbradoPublicoTA == null)
            {
                catAlumbradoPublicoTA = new FEXAVA_CATALUMBRADOPUBLICOTableAdapter();
            }
            return catAlumbradoPublicoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de banquetas
    /// </summary>
    public static FEXAVA_CATBANQUETASTableAdapter CatBanquetasTA
    {
        get
        {
            if (catBanquetasTA == null)
            {
                catBanquetasTA = new FEXAVA_CATBANQUETASTableAdapter();
            }
            return catBanquetasTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de clases construcción
    /// </summary>
    public static FEXAVA_CATCLASESCONSTTableAdapter CatClasesConstruccionTA
    {
        get
        {
            if (catClasesConstruccionTA == null)
            {
                catClasesConstruccionTA = new FEXAVA_CATCLASESCONSTTableAdapter();
            }
            return catClasesConstruccionTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de clasificación zona
    /// </summary>
    public static FEXAVA_CATCLASIFICACIONZONATableAdapter CatClasificacionZonaTA
    {
        get
        {
            if (catClasificacionZonaTA == null)
            {
                catClasificacionZonaTA = new FEXAVA_CATCLASIFICACIONZONATableAdapter();
            }
            return catClasificacionZonaTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de recolección de basura
    /// </summary>
    public static FEXAVA_CATRECOLECCIONBASURATableAdapter CatRecoleccionBasuraTA
    {
        get
        {
            if (catRecoleccionBasuraTA == null)
            {
                catRecoleccionBasuraTA = new FEXAVA_CATRECOLECCIONBASURATableAdapter();
            }
            return catRecoleccionBasuraTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de la densidad habitacional
    /// </summary>
    public static FEXAVA_CATDENSIDADHABTableAdapter CatDensidadHabitacionalTA
    {
        get
        {
            if (catDensidadHabitacionalTA == null)
            {
                catDensidadHabitacionalTA = new FEXAVA_CATDENSIDADHABTableAdapter();
            }
            return catDensidadHabitacionalTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de densidad población
    /// </summary>
    public static FEXAVA_CATDENSIDADPOBTableAdapter CatDensidadPoblacionTA
    {
        get
        {
            if (catDensidadPoblacionTA == null)
            {
                catDensidadPoblacionTA = new FEXAVA_CATDENSIDADPOBTableAdapter();
            }
            return catDensidadPoblacionTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo del drenaje pluvial
    /// </summary>
    public static FEXAVA_CATDRENJPLUVTableAdapter CatDrenajePluvialTA
    {
        get
        {
            if (catDrenajePluvialTA == null)
            {
                catDrenajePluvialTA = new FEXAVA_CATDRENJPLUVTableAdapter();
            }
            return catDrenajePluvialTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de estado de conservación
    /// </summary>
    public static FEXAVA_CATESTADOCONSERVTableAdapter CatEstadoConservacionTA
    {
        get
        {
            if (catEstadoConservacionTA == null)
            {
                catEstadoConservacionTA = new FEXAVA_CATESTADOCONSERVTableAdapter();
            }
            return catEstadoConservacionTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de estados del avalúo
    /// </summary>
    public static FEXAVA_CATESTADOSAVALUOTableAdapter CatEstadosAvaluoTA
    {
        get
        {
            if (catEstadosAvaluoTA == null)
            {
                catEstadosAvaluoTA = new FEXAVA_CATESTADOSAVALUOTableAdapter();
            }
            return catEstadosAvaluoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de gas natural
    /// </summary>
    public static FEXAVA_CATGASNATURALTableAdapter CatGasNaturalTA
    {
        get
        {
            if (catGasNaturalTA == null)
            {
                catGasNaturalTA = new FEXAVA_CATGASNATURALTableAdapter();
            }
            return catGasNaturalTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de usos de contrucciones
    /// </summary>
    public static FEXAVA_CATUSOSCONSTTableAdapter CatUsosConstruccionesTA
    {
        get
        {
            if (catUsosConstruccionesTA == null)
            {
                catUsosConstruccionesTA = new FEXAVA_CATUSOSCONSTTableAdapter();
            }
            return catUsosConstruccionesTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de vialidades
    /// </summary>
    public static FEXAVA_CATVIALIDADESTableAdapter CatVialidadesTA
    {
        get
        {
            if (catVialidadesTA == null)
            {
                catVialidadesTA = new FEXAVA_CATVIALIDADESTableAdapter();
            }
            return catVialidadesTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de guarniciones
    /// </summary>
    public static FEXAVA_CATGUARNICIONESTableAdapter CatGuarnicionesTA
    {
        get
        {
            if (catGuarnicionesTA == null)
            {
                catGuarnicionesTA = new FEXAVA_CATGUARNICIONESTableAdapter();
            }
            return catGuarnicionesTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo del modo de construcción
    /// </summary>
    public static FEXAVA_CATMODOCONSTRUCCIONTableAdapter CatModoConstruccionTA
    {
        get
        {
            if (catModoConstruccionTA == null)
            {
                catModoConstruccionTA = new FEXAVA_CATMODOCONSTRUCCIONTableAdapter();
            }
            return catModoConstruccionTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo del nivel socio-económico
    /// </summary>
    public static FEXAVA_CATNIVELSOCIOECONTableAdapter CatNivelSocioeconomicoTA
    {
        get
        {
            if (catNivelSocioeconomicoTA == null)
            {
                catNivelSocioeconomicoTA = new FEXAVA_CATNIVELSOCIOECONTableAdapter();
            }
            return catNivelSocioeconomicoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de rango de niveles
    /// </summary>
    public static FEXAVA_CATRANGONIVELESTableAdapter CatRangoNivelesTA
    {
        get
        {
            if (catRangoNivelesTA == null)
            {
                catRangoNivelesTA = new FEXAVA_CATRANGONIVELESTableAdapter();
            }
            return catRangoNivelesTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de tipo de elemento
    /// </summary>
    public static FEXAVA_CATTIPOELEMENTOTableAdapter CatTipoElementoTA
    {
        get
        {
            if (catTipoElementoTA == null)
            {
                catTipoElementoTA = new FEXAVA_CATTIPOELEMENTOTableAdapter();
            }
            return catTipoElementoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de tipo de función
    /// </summary>
    public static FEXAVA_CATTIPOFUNCIONTableAdapter CatTipoFuncionTA
    {
        get
        {
            if (catTipoFuncionTA == null)
            {
                catTipoFuncionTA = new FEXAVA_CATTIPOFUNCIONTableAdapter();
            }
            return catTipoFuncionTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de tipo
    /// </summary>
    public static FEXAVA_CATTIPOTableAdapter CatTipoTA
    {
        get
        {
            if (catTipoTA == null)
            {
                catTipoTA = new FEXAVA_CATTIPOTableAdapter();
            }
            return catTipoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de tipo de terreno
    /// </summary>
    public static FEXAVA_CATTIPOTERRENOTableAdapter CatTipoTerrenoTA
    {
        get
        {
            if (catTipoTerrenoTA == null)
            {
                catTipoTerrenoTA = new FEXAVA_CATTIPOTERRENOTableAdapter();
            }
            return catTipoTerrenoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de topografía
    /// </summary>
    public static FEXAVA_CATTOPOGRAFIATableAdapter CatTopografiaTA
    {
        get
        {
            if (catTopografiaTA == null)
            {
                catTopografiaTA = new FEXAVA_CATTOPOGRAFIATableAdapter();
            }
            return catTopografiaTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de agua potable
    /// </summary>
    public static FEXAVA_CATAGUAPOTABLETableAdapter CatAguaPotableTA
    {
        get
        {
            if (catAguaPotableTA == null)
            {
                catAguaPotableTA = new FEXAVA_CATAGUAPOTABLETableAdapter();
            }
            return catAguaPotableTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de drenaje de inmueble
    /// </summary>
    public static FEXAVA_CATDRENAINMUEBTableAdapter CatDrenajeInmuebleTA
    {
        get
        {
            if (catDrenajeInmuebleTA == null)
            {
                catDrenajeInmuebleTA = new FEXAVA_CATDRENAINMUEBTableAdapter();
            }
            return catDrenajeInmuebleTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de suministro eléctrico
    /// </summary>
    public static FEXAVA_CATSUMINISELECTableAdapter CatSuministroElecTA
    {
        get
        {
            if (catSuministroElecTA == null)
            {
                catSuministroElecTA = new FEXAVA_CATSUMINISELECTableAdapter();
            }
            return catSuministroElecTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de acometida inmueble
    /// </summary>
    public static FEXAVA_CATACOMETIDINMTableAdapter CatAcometidaInmTA
    {
        get
        {
            if (catAcometidaInmTA == null)
            {
                catAcometidaInmTA = new FEXAVA_CATACOMETIDINMTableAdapter();
            }
            return catAcometidaInmTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de nomenclaturas de calle
    /// </summary>
    public static FEXAVA_CATNOMENCALLETableAdapter CatNomenCalleTA
    {
        get
        {
            if (catNomenCalleTA == null)
            {
                catNomenCalleTA = new FEXAVA_CATNOMENCALLETableAdapter();
            }
            return catNomenCalleTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de clases de uso
    /// </summary>
    public static FEXAVA_CATCLASEUSOTableAdapter CatClaseUsoTA
    {
        get
        {
            if (catClaseUsoTA == null)
            {
                catClaseUsoTA = new FEXAVA_CATCLASEUSOTableAdapter();
            }
            return catClaseUsoTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de suministro telefónico
    /// </summary>
    public static FEXAVA_CATSUMINISTTELTableAdapter CatSuministroTelTA
    {
        get
        {
            if (catSuministroTelTA == null)
            {
                catSuministroTelTA = new FEXAVA_CATSUMINISTTELTableAdapter();
            }
            return catSuministroTelTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de señalización vías
    /// </summary>
    public static FEXAVA_CATSENALIZVIASTableAdapter CatSenalizacionviasTA
    {
        get
        {
            if (catSenalizacionviasTA == null)
            {
                catSenalizacionviasTA = new FEXAVA_CATSENALIZVIASTableAdapter();
            }
            return catSenalizacionviasTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de vigilancia zona
    /// </summary>
    public static FEXAVA_CATVIGILANZONATableAdapter CatVigilanciaZonaTA
    {
        get
        {
            if (catVigilanciaZonaTA == null)
            {
                catVigilanciaZonaTA = new FEXAVA_CATVIGILANZONATableAdapter();
            }
            return catVigilanciaZonaTA;
        }
    }

    /// <summary>
    /// Propiedad que devuelve el catálogo de régimen propiedad
    /// </summary>
    public static FEXAVA_CATREGIMENPROPTableAdapter CatRegimenPropiedadTA
    {
        get
        {
            if (catRegimenPropiedadTA == null)
            {
                catRegimenPropiedadTA = new FEXAVA_CATREGIMENPROPTableAdapter();
            }
            return catRegimenPropiedadTA;
        }
    }

    #endregion

    /// <summary>
    /// Obtener los catalogos de avaluo
    /// </summary>
    /// <returns>dataset con los catalogos cargados</returns>
    /// 
    private static DseAvaluosCatConsulta dseCatalogosConsulta = null;
    
    /// <summary>
    /// Propiedad para acceder al dataset de los catálogos de consulta
    /// </summary>
    public static DseAvaluosCatConsulta DseCatalogosConsulta
    {
        get
        {
            if (dseCatalogosConsulta == null)
            {
                dseCatalogosConsulta = ObtenerCatalogos();
            }

            return dseCatalogosConsulta;
        }
 
    }

    private static DseAvaluosCatConsulta ObtenerCatalogos()
    {
        object o = null;
        try
        {

            DseAvaluosCatConsulta dse = new DseAvaluosCatConsulta();
            dse.EnforceConstraints = false;
            dse.SchemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
            CatTopografiaTA.Fill(dse.FEXAVA_CATTOPOGRAFIA, out o);
            CatEstadosAvaluoTA.Fill(dse.FEXAVA_CATESTADOSAVALUO, out o);
            CatEstadoConservacionTA.Fill(dse.FEXAVA_CATESTADOCONSERV, out o);
            CatAguaPotableTA.Fill(dse.FEXAVA_CATAGUAPOTABLE, out o);
            CatSenalizacionviasTA.Fill(dse.FEXAVA_CATSENALIZVIAS, out o);
            CatAlumbradoPublicoTA.Fill(dse.FEXAVA_CATALUMBRADOPUBLICO, out o);
            CatBanquetasTA.Fill(dse.FEXAVA_CATBANQUETAS, out o);
            CatGasNaturalTA.Fill(dse.FEXAVA_CATGASNATURAL, out o);
            CatSuministroTelTA.Fill(dse.FEXAVA_CATSUMINISTTEL, out o);
            CatSuministroElecTA.Fill(dse.FEXAVA_CATSUMINISELEC, out o);
            CatAcometidaInmTA.Fill(dse.FEXAVA_CATACOMETIDINM, out o);
            CatVialidadesTA.Fill(dse.FEXAVA_CATVIALIDADES, out o);
            CatGuarnicionesTA.Fill(dse.FEXAVA_CATGUARNICIONES, out o);
            CatVigilanciaZonaTA.Fill(dse.FEXAVA_CATVIGILANZONA, out o);
            CatDrenajeInmuebleTA.Fill(dse.FEXAVA_CATDRENAINMUEB, out o);
            CatDrenajePluvialTA.Fill(dse.FEXAVA_CATDRENJPLUV, out o);
            CatDensidadPoblacionTA.Fill(dse.FEXAVA_CATDENSIDADPOB, out o);
            CatDensidadHabitacionalTA.Fill(dse.FEXAVA_CATDENSIDADHAB, out o);
            CatNivelSocioeconomicoTA.Fill(dse.FEXAVA_CATNIVELSOCIOECON, out o);
            CatRegimenPropiedadTA.Fill(dse.FEXAVA_CATREGIMENPROP, out o);
            CatClasificacionZonaTA.Fill(dse.FEXAVA_CATCLASIFICACIONZONA, out o);
            CatRecoleccionBasuraTA.Fill(dse.FEXAVA_CATRECOLECCIONBASURA, out o);
            CatClaseUsoTA.Fill(dse.FEXAVA_CATCLASEUSO, out o);
            CatNomenCalleTA.Fill(dse.FEXAVA_CATNOMENCALLE, out o);
            return dse;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    #region CatIndividuales

    /// <summary>
    /// Obtiene la tabla del catálogo de densidad de población
    /// </summary>
    /// <returns>tabla catálogo de densidad de población cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATDENSIDADPOBDataTable ObtenerCatalogoDensidadPob()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATDENSIDADPOB;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de densidad de habitacional
    /// </summary>
    /// <returns>tabla del catálogo de densidad de habitacional cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATDENSIDADHABDataTable ObtenerCatalogoDensidadHab()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATDENSIDADHAB;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de estado de conservación
    /// </summary>
    /// <returns>tabla del catálogo de estado de conservación cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATESTADOCONSERVDataTable ObtenerCatalogoEstadoConserv()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATESTADOCONSERV;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de régimen propiedad
    /// </summary>
    /// <returns>la tabla del catálogo de régimen propiedad cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATREGIMENPROPDataTable ObtenerCatalogoRegimenProp()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATREGIMENPROP;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de clases de uso
    /// </summary>
    /// <returns>tabla del catálogo de clases de uso cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATCLASEUSODataTable ObtenerCatalogoClaseUso()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATCLASEUSO;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de clasificación zonas
    /// </summary>
    /// <returns>tabla del catálogo de clasificación zonas cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATCLASIFICACIONZONADataTable ObtenerCatalogoClasificacionZona()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATCLASIFICACIONZONA;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de recolección de basura
    /// </summary>
    /// <returns>tabla del catálogo de recolección de basura cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATRECOLECCIONBASURADataTable ObtenerCatalogoRecBasura()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATRECOLECCIONBASURA;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de nivel socio-económico
    /// </summary>
    /// <returns>tabla del catálogo de nivel socio-económico cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATNIVELSOCIOECONDataTable ObtenerCatalogoNivelSocioEc()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATNIVELSOCIOECON;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de vigilancia zona
    /// </summary>
    /// <returns>tabla del catálogo de vigilancia zona cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATVIGILANZONADataTable ObtenerCatalogoVigilanciaZona()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATVIGILANZONA;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de drenaje de inmueble
    /// </summary>
    /// <returns>tabla del catálogo de drenaje de inmueble cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATDRENAINMUEBDataTable ObtenerCatalogoDrenajeInm()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATDRENAINMUEB;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de drenaje pluvial
    /// </summary>
    /// <returns>tabla del catálogo de drenaje pluvial cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATDRENJPLUVDataTable ObtenerCatalogoDrenajePluv()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATDRENJPLUV;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de agua potable
    /// </summary>
    /// <returns>tabla del catálogo de agua potable cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATAGUAPOTABLEDataTable ObtenerCatalogoAguaPotable()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATAGUAPOTABLE;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de suministro eléctrico
    /// </summary>
    /// <returns>tabla del catálogo de suministro eléctrico cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATSUMINISELECDataTable ObtenerCatalogoSuministroElec()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATSUMINISELEC;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de banquetas
    /// </summary>
    /// <returns>tabla del catálogo de banquetas cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATBANQUETASDataTable ObtenerCatalogoBanquetas()
    {

        try
        {
            return DseCatalogosConsulta.FEXAVA_CATBANQUETAS;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de suministro telefónico
    /// </summary>
    /// <returns>tabla del catálogo de suministro telefónico cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATSUMINISTTELDataTable ObtenerCatalogoSuministroTel()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATSUMINISTTEL;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de gas natural
    /// </summary>
    /// <returns>tabla del catálogo de gas natural cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATGASNATURALDataTable ObtenerCatalogoGasNatural()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATGASNATURAL;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de guarniciones
    /// </summary>
    /// <returns>tabla del catálogo de guarniciones cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATGUARNICIONESDataTable ObtenerCatalogoGuarniciones()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATGUARNICIONES;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de señalización de vías
    /// </summary>
    /// <returns>tabla del catálogo de señalización de vías cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATSENALIZVIASDataTable ObtenerCatalogoSenalizacionVias()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATSENALIZVIAS;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de vialidades
    /// </summary>
    /// <returns>tabla del catálogo de vialidades cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATVIALIDADESDataTable ObtenerCatalogoVialidades()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATVIALIDADES;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de nomenclatura calle
    /// </summary>
    /// <returns>tabla del catálogo de nomenclatura calle cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATNOMENCALLEDataTable ObtenerCatalagoNomenCalle()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATNOMENCALLE;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }

    }

    /// <summary>
    /// Obtiene la tabla del catálogo de acometida inmueble
    /// </summary>
    /// <returns>tabla del catálogo de acometida inmueble cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATACOMETIDINMDataTable ObtenerCatalogoAcometidaInm()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATACOMETIDINM;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de alumbrado público
    /// </summary>
    /// <returns>tabla del catálogo de alumbrado público cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATALUMBRADOPUBLICODataTable ObtenerCatalogoAlumbradoPublico()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATALUMBRADOPUBLICO;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    /// <summary>
    /// Obtiene la tabla del catálogo de topografía
    /// </summary>
    /// <returns>tabla del catálogo de topografía cargada</returns>
    public static DseAvaluosCatConsulta.FEXAVA_CATTOPOGRAFIADataTable ObtenerCatalogoTopografia()
    {
        try
        {
            return DseCatalogosConsulta.FEXAVA_CATTOPOGRAFIA;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

    #endregion

}

