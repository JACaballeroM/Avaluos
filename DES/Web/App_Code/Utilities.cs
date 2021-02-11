using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;

/// <summary>
/// Descripción breve de Utilities
/// </summary>
public static class Utilities
{

    public static List<decimal> getDatosTerrenoMercado(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_TERRENOMERCADODataTable table, out string utilidad)
    {
        utilidad = "0";
        List<decimal> lista = new List<decimal>();
        if (table.Count > 0)
        {
            lista.Add(table[0].IsTOTALINGRESOSNull() ? 0M : table[0].TOTALINGRESOS);
            lista.Add(table[0].IsTOTALEGRESOSNull() ? 0M : table[0].TOTALEGRESOS);
            lista.Add(table[0].IsVALORUNITARIORESIDUALNull() ? 0M : table[0].VALORUNITARIORESIDUAL);
            utilidad = table[0].IsUTILIDADNull() ? "0" : table[0].UTILIDAD;

        }
        else
        {
            lista.Add(0M);
            lista.Add(0M);
            lista.Add(0M);
        }
        return lista;
    }

    public static decimal GetValorTotalExtra(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        decimal ret = 0;
        try
        {
            if (table.Count > 0)
            {
                if (!table[0].IsVALORTOTALIEEAOCNull())
                    ret = table[0].VALORTOTALIEEAOC;
                else
                    ret = 0;
            }
        }
        catch
        {

        }
        return ret;
    }

    public static decimal GetValorTotal(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESDataTable table, bool comer)
    {
        decimal ret = 0;
        foreach (var constuccion in table)
        {
            if (constuccion.CODTIPO.Equals("C"))
            {
                decimal indiviso = 0;
                decimal.TryParse(constuccion.DataColumn1, out indiviso);
                ret += (constuccion.VALORFRACCION * indiviso);
                constuccion.DataColumn1 = string.Format("{0}%", constuccion.DataColumn1);
            }
            if (comer)
            {
                if (constuccion.CODTIPO.Equals("P"))
                {
                    ret += (constuccion.VALORFRACCION);
                }
            }
        }
        return ret;
    }

    public static decimal GetValorTerreno(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        return (decimal)table.Compute("SUM(VALORTOTALDELTERRENOPROP)", "");
    }

    public static decimal GetValorPrivativas(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        return (decimal)table.Compute("SUM(VALTOTCONSTRUCCIONESPRIVATIVAS)", "");
    }

    public static decimal GetImpInstEsp(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table, decimal valor)
    {
        return (table.GetValorPrivativas() + valor) * 0.08M;
    }

    public static decimal GetValorTotalPrivativas(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_CONSTRUCCIONESDataTable table)
    {
        decimal ret = 0;
        foreach (var constuccion in table)
        {
            if (constuccion.CODTIPO.Equals("P"))
            {

                ret += constuccion.VALORFRACCION;
            }
        }
        return ret; 
    }

    public static DataTable GetAvaComercial_I(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaComercial_I nuevo = new dsAvaComercial_I();
        if (table.Count > 0)
        {
            dsAvaComercial_I.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.CALLE = table[0].CALLE;
            row.COLONIA = table[0].COLONIA;
            row.CP = table[0].CP;
            row.CUAREALIBREOBLIGATORIO = table[0].CUAREALIBREOBLIGATORIO;
            row.CUCONTAMINACIONAMBIENTALZONA = table[0].IsCUCONTAMINACIONAMBIENTALZONANull() ? string.Empty : table[0].CUCONTAMINACIONAMBIENTALZONA;
            row.CUDISTANCIATRANSPORTESUBURB = table[0].CUDISTANCIATRANSPORTESUBURB;
            row.CUDISTANCIATRANSPORTEURBANO = table[0].CUDISTANCIATRANSPORTEURBANO;
            row.CUENTAAGUA = table[0].IsCUENTAAGUANull() ? string.Empty : table[0].CUENTAAGUA;
            row.CUEXISTEBANCOS = table[0].CUEXISTEBANCOS;
            row.CUEXISTEESCUELAS = table[0].CUEXISTEESCUELAS;
            row.CUEXISTEESTACIONTRANSPORTE = table[0].CUEXISTEESTACIONTRANSPORTE;
            row.CUEXISTEHOSPITALES = table[0].CUEXISTEHOSPITALES;
            row.CUEXISTEIGLESIA = table[0].CUEXISTEIGLESIA;
            row.CUEXISTEMERCADOS = table[0].CUEXISTEMERCADOS;
            row.CUEXISTEPARQUESJARDINES = table[0].CUEXISTEPARQUESJARDINES;
            row.CUEXISTEPLAZASPUBLICOS = table[0].CUEXISTEPLAZASPUBLICOS;
            row.CUFRECUENCIATRANSPORTESUBURB = table[0].CUFRECUENCIATRANSPORTESUBURB;
            row.CUFRECUENCIATRANSPORTEURBANO = table[0].CUFRECUENCIATRANSPORTEURBANO;
            row.CUINDICESATURACIONZONA = table[0].CUINDICESATURACIONZONA;
            row.CUPORCENTAJEINFRAESTRUCTURA = table[0].CUPORCENTAJEINFRAESTRUCTURA;
            row.CUUSO = table[0].CUUSO;
            row.DELEGACION = table[0].DELEGACION;
            row.DESC_ACOMETIDAIM = table[0].DESC_ACOMETIDAIM;
            row.DESC_ACOMETIDAIMTEL = table[0].DESC_ACOMETIDAIMTEL;
            row.DESC_AGUAPOTABLE = table[0].DESC_AGUAPOTABLE;
            row.DESC_ALUMBRADOPUBL = table[0].DESC_ALUMBRADOPUBL;
            row.DESC_BANQUETAS = table[0].DESC_BANQUETAS;
            row.DESC_CLASECONST = table[0].DESC_CLASECONST;
            row.DESC_CLASIFICACIONZONA = table[0].DESC_CLASIFICACIONZONA;
            row.DESC_DENSIDAD = table[0].DESC_DENSIDAD;
            row.DESC_DRENAJEPLUVCALLE = table[0].DESC_DRENAJEPLUVCALLE;
            row.DESC_DRENAJEPLUVZONA = table[0].DESC_DRENAJEPLUVZONA;
            row.DESC_GASNATURAL = table[0].DESC_GASNATURAL;
            row.DESC_GUARNICIONES = table[0].DESC_GUARNICIONES;
            row.DESC_NIVELSOCIOEC = table[0].DESC_NIVELSOCIOEC;
            row.DESC_NOMECLATURACALLES = table[0].DESC_NOMECLATURACALLES;
            row.DESC_RECBASURA = table[0].DESC_RECBASURA;
            row.DESC_RECOLECAGUAS = table[0].DESC_RECOLECAGUAS;
            row.DESC_REGIMENPROPIEDAD = table[0].DESC_REGIMENPROPIEDAD;
            row.DESC_SENALIZACIONVIAS = table[0].DESC_SENALIZACIONVIAS;
            row.DESC_SISTEMAMIXTO = table[0].DESC_SISTEMAMIXTO;
            row.DESC_SUMELECTRICO = table[0].DESC_SUMELECTRICO;
            row.DESC_SUMINISTROTEL = table[0].DESC_SUMINISTROTEL;
            row.DESC_VIGILANCIA = table[0].DESC_VIGILANCIA;
            row.DESCVIALIDADES = table[0].DESCVIALIDADES;
            row.DIGITOVERIFICADOR = table[0].DIGITOVERIFICADOR;
            row.EDIFICIO = table[0].IsEDIFICIONull() ? string.Empty : table[0].EDIFICIO;
            row.EXT = table[0].EXT;
            row.FECHAAVALUO = table[0].FECHAAVALUO;
            row.INT = table[0].INT;
            // row.TIPOCODOMINIO = table[0].TIPOCODOMINIO;
            try
            {
                row.LOTE = table[0].LOTE;
            }
            catch
            {
                row.LOTE = string.Empty;
            }
            row.LOTE_CC = table[0].IsLOTE_CCNull() ? string.Empty : table[0].LOTE_CC;
            row.MANZANA = table[0].MANZANA;
            row.NIVELEQUIPAMIENTOURBANO = table[0].NIVELEQUIPAMIENTOURBANO;
            row.OBJETO = table[0].OBJETO;
            row.PROPOSITO = table[0].PROPOSITO;
            row.REGION = table[0].REGION;
            row.REGTDF_PERITO = table[0].REGTDF_PERITO;
            row.REGTDF_SOCIEDAD = table[0].IsREGTDF_SOCIEDADNull() ? string.Empty : table[0].REGTDF_SOCIEDAD;
            row.UNIDADPRIVATIVA = table[0].UNIDADPRIVATIVA;
            row.VIASDEACCESO = table[0].VIASDEACCESO;



            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable)nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaCatastral_3(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCatastral_3 nuevo = new dsAvaCatastral_3();
        if (table.Count > 0)
        {
            dsAvaCatastral_3.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.DTRAMOCALLES = table[0].DTRAMOCALLES;
            row.BYTES_MICRO = table[0].BYTES_MICRO;
            row.BYTES_MACRO = table[0].BYTES_MACRO;
            row.DESC_TOPOGRAFIA = table[0].DESC_TOPOGRAFIA;
            row.TECARACTERISTICASPARONAMICAS = table[0].TECARACTERISTICASPARONAMICAS;
            row.DESC_DENSIDADHAB = table[0].DESC_DENSIDADHAB;
            row.TESERVIDUMBRESORESTRICCIONES = table[0].TESERVIDUMBRESORESTRICCIONES;
            row.DIUSOACTUAL = table[0].DIUSOACTUAL;
            row.TEINDIVISO = table[0].TEINDIVISO;
            row.DIVIDAUTILPONDERADA = table[0].DIVIDAUTILPONDERADA;
            row.DIEDADPONDERADA = table[0].DIEDADPONDERADA;
            row.DIVIDAUTILPONDERADA = table[0].DIVIDAUTILPONDERADA;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable)nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaCat_6(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCat_6 nuevo = new dsAvaCat_6();
        if (table.Count > 0)
        {
            dsAvaCat_6.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.CONSIDERACIONESPREVIAS = table[0].IsCONSIDERACIONESPREVIASNull() ? string.Empty : table[0].CONSIDERACIONESPREVIAS;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable)nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaCom_7(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCom_7 nuevo = new dsAvaCom_7();
        if (table.Count > 0)
        {
            dsAvaCom_7.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.DIVALORMERCADO = table[0].DIVALORMERCADO;

            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable) nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaCat_7(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCat_7 nuevo = new dsAvaCat_7();
        if (table.Count > 0)
        {
            dsAvaCat_7.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.TEINDIVISO = table[0].TEINDIVISO;
            row.VALORTOTALDELTERRENOPROP = table[0].VALORTOTALDELTERRENOPROP;
            row.VALTOTCONSTRUCCIONESPRIVATIVAS = table[0].VALTOTCONSTRUCCIONESPRIVATIVAS;
            row.VALTOTCONSTRUCCIONESCOMUNES = table[0].VALTOTCONSTRUCCIONESCOMUNES;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable)nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaComercial_VIII(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaComercial_VIII nuevo = new dsAvaComercial_VIII();
        if (table.Count > 0)
        {
            dsAvaComercial_VIII.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.TEINDIVISO = table[0].TEINDIVISO;
            row.VALORTOTALDELTERRENOPROP = table[0].VALORTOTALDELTERRENOPROP;
            row.CODREGIMENPROPIEDAD = table[0].CODREGIMENPROPIEDAD.ToString();
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable) nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaComercial_IX(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaComercial_IX nuevo = new dsAvaComercial_IX();
        if (table.Count > 0)
        {
            dsAvaComercial_IX.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.EIADMINISTRACION = table[0].EIADMINISTRACION;
            row.EICONSERVACIONYMANTENIMIENTO = table[0].EICONSERVACIONYMANTENIMIENTO;
            row.EIDEDUCCIONESFISCALES = table[0].EIDEDUCCIONESFISCALES;
            row.EIDEDUCCIONESMENSUALES = table[0].EIDEDUCCIONESMENSUALES;
            row.EIDEPRECIACIONFISCAL = table[0].EIDEPRECIACIONFISCAL;
            row.EIENERGIAELECTRICA = table[0].EIENERGIAELECTRICA;
            row.EIIMPORTE = table[0].EIIMPORTE;
            row.EIIMPUESTOPREDIAL = table[0].EIIMPUESTOPREDIAL;
            row.EIIMPUESTORENTA = table[0].EIIMPUESTORENTA;
            row.EIOTROS = table[0].EIOTROS;
            row.EIPRODUCTOLIQUIDOANUAL = table[0].EIPRODUCTOLIQUIDOANUAL;
            row.EISEGUROS = table[0].EISEGUROS;
            row.EISERVICIOAGUA = table[0].EISERVICIOAGUA;
            row.EITASACAPITALIZACION = table[0].EITASACAPITALIZACION;
            row.EIVACIOS = table[0].EIVACIOS;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable) nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaComercial_XI_XII(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaComercial_XI_XII nuevo = new dsAvaComercial_XI_XII();
        if (table.Count > 0)
        {
            dsAvaComercial_XI_XII.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.NOMBRE_SOCI = table[0].IsNOMBRE_SOCINull() ? string.Empty : table[0].NOMBRE_SOCI;
            row.CONSIDERACIONESPREVIASCONCLUSION = table[0].IsCONSIDERACIONESPREVIASCONCLUSIONNull() ? string.Empty : table[0].CONSIDERACIONESPREVIASCONCLUSION;
            row.VALORCOMERCIAL = table[0].IsVALORCOMERCIALNull() ? 0 : table[0].VALORCOMERCIAL;
            row.VALORREFERIDO = table[0].IsVALORREFERIDONull() ? 0M : table[0].VALORREFERIDO;
            row.FECHAVALORREFERIDO = table[0].IsFECHAVALORREFERIDONull() ? string.Empty : table[0].FECHAVALORREFERIDO;
            row.FACTORCONVERSION = table[0].IsFACTORCONVERSIONNull() ? 0M : table[0].FACTORCONVERSION;
            row.REGTDF_PERITO = table[0].REGTDF_PERITO;
            row.NOMBRE_PERITO = table[0].NOMBRE_PERITO;
            row.REGTDF_SOCIEDAD = table[0].IsREGTDF_SOCIEDADNull() ? string.Empty : table[0].REGTDF_SOCIEDAD;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable) nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaAvaCat_9(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCat_9 nuevo = new dsAvaCat_9();
        if (table.Count > 0)
        {
            dsAvaCat_9.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.CONSIDERACIONESPREVIASCONCLUSION = table[0].IsCONSIDERACIONESPREVIASCONCLUSIONNull()? string.Empty: table[0].CONSIDERACIONESPREVIASCONCLUSION;
            row.VALORCATASTRAL = table[0].IsVALORCATASTRALNull()? 0: table[0].VALORCATASTRAL;
            row.REGTDF_PERITO = table[0].IsREGTDF_PERITONull()? string.Empty: table[0].REGTDF_PERITO;
            row.NOMBRE_PERITO =  table[0].IsNOMBRE_PERITONull()?string.Empty:table[0].NOMBRE_PERITO;
            row.REGTDF_SOCIEDAD = table[0].IsREGTDF_SOCIEDADNull() ? string.Empty : table[0].REGTDF_SOCIEDAD;
            row.NOMBRE_SOCI = table[0].IsNOMBRE_SOCINull() ? string.Empty : table[0].NOMBRE_SOCI;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable) nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaCom_XA(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCom_XA nuevo = new dsAvaCom_XA();
        if (table.Count > 0)
        {
            dsAvaCom_XA.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.IMPORTETOTALENFOQUECOSTOS = table[0].IMPORTETOTALENFOQUECOSTOS;
            row.EIIMPORTE = table[0].EIIMPORTE;
            row.DIVALORMERCADO = table[0].DIVALORMERCADO;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable) nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaCat_5A(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCat_5A nuevo = new dsAvaCat_5A();
        if (table.Count > 0)
        {
            dsAvaCat_5A.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.IEYALUMBRADO = table[0].IsIEYALUMBRADONull() ? string.Empty : table[0].IEYALUMBRADO;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable)nuevo.FEXAVA_AVALUO;
    }

    public static DataTable GetAvaPrincipal(this ServiceAvaluos.DseAvaluoMantInf.FEXAVA_AVALUODataTable table)
    {
        dsAvaCat nuevo = new dsAvaCat();
        if (table.Count > 0)
        {
            dsAvaCat.FEXAVA_AVALUORow row = nuevo.FEXAVA_AVALUO.NewFEXAVA_AVALUORow();
            row.NUMEROAVALUO = table[0].NUMEROAVALUO;
            row.IDPERSONASOCIEDAD = table[0].IsIDPERSONASOCIEDADNull() ? 0M : table[0].IDPERSONASOCIEDAD;
            nuevo.FEXAVA_AVALUO.AddFEXAVA_AVALUORow(row);
        }
        return (DataTable)nuevo.FEXAVA_AVALUO;
    }

    public static void Set
        (this SubreportProcessingEventArgs e, ServiceAvaluos.DseAvaluoMantInf informeDataSource, bool comer)
    {
        switch (e.ReportPath)
        {
            case "I-II":
                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaComercial_I()));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_DATOSPERSONASSOL", informeDataSource.FEXAVA_DATOSPERSONAS.Select("ROL='S'", "", DataViewRowState.CurrentRows)));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_DATOSPERSONAS", informeDataSource.FEXAVA_DATOSPERSONAS.Select("ROL='P'", "", DataViewRowState.CurrentRows)));
                break;
            case "III-IV":
                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaCatastral_3()));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_COLINDANCIAS", (DataTable)informeDataSource.FEXAVA_COLINDANCIAS));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_SUPERFICIE", (DataTable)informeDataSource.FEXAVA_SUPERFICIE));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ESCRITURA", (DataTable)informeDataSource.FEXAVA_ESCRITURA));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_SENTENCIA", (DataTable)informeDataSource.FEXAVA_SENTENCIA));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_CONTRATOPRIVADO", (DataTable)informeDataSource.FEXAVA_CONTRATOPRIVADO));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ALINEAMIENTONUMOFI", (DataTable)informeDataSource.FEXAVA_ALINEAMIENTONUMOFI));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_CONSTRUCCIONES", (DataTable)informeDataSource.FEXAVA_CONSTRUCCIONES));
                break;
            case "VI":
                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaCat_6()));
                break;
            case "VII":
                if (comer)
                {
                    e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaCom_7()));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_DATOSTERRENOS", (DataTable)informeDataSource.FEXAVA_DATOSTERRENOS));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_TERRENOMERCADO", (DataTable)informeDataSource.FEXAVA_TERRENOMERCADO));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_INVESTPRODUCTOSCOMP", (DataTable)informeDataSource.FEXAVA_INVESTPRODUCTOSCOMP));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_CONSTRUCCIONESMER", (DataTable)informeDataSource.FEXAVA_CONSTRUCCIONESMER));
                }
                else
                {
                    e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaCat_7()));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_SUPERFICIE", (DataTable)informeDataSource.FEXAVA_SUPERFICIE));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ELEMENTOSEXTRA", (DataTable)informeDataSource.FEXAVA_ELEMENTOSEXTRA));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ENFOQUECOSTESCAT", (DataTable)informeDataSource.FEXAVA_ENFOQUECOSTESCAT));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_CONSTRUCCIONES", (DataTable)informeDataSource.FEXAVA_CONSTRUCCIONES));
                }
                break;
            case "VIII":

                if (comer)
                {
                    e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaComercial_VIII()));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_SUPERFICIE", (DataTable)informeDataSource.FEXAVA_SUPERFICIE));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ELEMENTOSEXTRA", (DataTable)informeDataSource.FEXAVA_ELEMENTOSEXTRA));
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_CONSTRUCCIONES", (DataTable)informeDataSource.FEXAVA_CONSTRUCCIONES));
                }
                break;
            case "IX":
                if (comer)
                    e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaComercial_IX()));
                else
                    e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_AVALUO", (DataTable)informeDataSource.FEXAVA_AVALUO));

                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_INVESTPRODUCTOSCOMP", (DataTable)informeDataSource.FEXAVA_INVESTPRODUCTOSCOMP));
                break;
            case "XI-XII":
                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaComercial_XI_XII()));
                break;
            case "IX-X":
                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaAvaCat_9()));
                break;
            case "XA":
                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaCom_XA()));
                break;
            case "VA":
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_OBRANEGRA", (DataTable)informeDataSource.FEXAVA_OBRANEGRA));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_REVESTIMIENTOACABADO", (DataTable)informeDataSource.FEXAVA_REVESTIMIENTOACABADO));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_CARPINTERIA", (DataTable)informeDataSource.FEXAVA_CARPINTERIA));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_INSTALACIONHIDSAN", (DataTable)informeDataSource.FEXAVA_INSTALACIONHIDSAN));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_PUERTASYVENTANERIA", (DataTable)informeDataSource.FEXAVA_PUERTASYVENTANERIA));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ELEMENTOSCONST", (DataTable)informeDataSource.FEXAVA_ELEMENTOSCONST));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_ELEMENTOSEXTRA", (DataTable)informeDataSource.FEXAVA_ELEMENTOSEXTRA));

                e.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaCat_5A()));

                break;
            case "ANEXO":
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_FOTOAVALUO", (DataTable)informeDataSource.FEXAVA_FOTOAVALUO));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_FOTOCOMPARABLE", (DataTable)informeDataSource.FEXAVA_FOTOCOMPARABLE));
                break;

            case "ANEXO2":
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_FOTOAVALUO", (DataTable)informeDataSource.FEXAVA_FOTOAVALUO));
                e.DataSources.Add(new ReportDataSource("DseAvaluoMantInf_FEXAVA_FOTOCOMPARABLE", (DataTable)informeDataSource.FEXAVA_FOTOCOMPARABLE));
                break;

        }
    }

}
