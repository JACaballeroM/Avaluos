using System;
using System.Reflection;
using System.ServiceModel;
using Microsoft.Reporting.WebForms;
using ServiceAvaluos;
using SIGAPred.Common.WCF;
using ServiceRCON;
using System.Data;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Clase para mostrar el justificante del avalúo.
/// </summary>
public partial class InformeAvaluo : PageBaseAvaluos
{
    /// <summary>
    /// Contiene el dataset que contiene los datos del informe.
    /// </summary>
    protected DseAvaluoMantInf informeDataSource = null;
    protected bool comer = false;
    #region PAGE

    /// <summary>
    /// Carga de la página.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count > 0)
        {
            for (int i = 0; i < Session.Count; i++)
            {
                if (Session[i].GetType().ToString() == "Microsoft.Reporting.WebForms.ReportHierarchy")
                {
                    Session.RemoveAt(i);
                }
            }
        }

         if (!IsPostBack)
           {
            try
            {

  
                int idAvaluo = idAvaluo = Convert.ToInt32(SIGAPred.Common.Web.WebUtils.QueryString(Constantes.PAR_IDAVALUO));
                AvaluosClient clienteAvaluos = new AvaluosClient();

                try
                {
                    informeDataSource = clienteAvaluos.GuardarAvaluoInformeComercial(idAvaluo);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                finally
                {
                    clienteAvaluos.Disconnect();
                }

                bool avaluoComercial = informeDataSource.FEXAVA_AVALUO[0].NUMEROUNICO.ToUpper().Contains(Constantes.PAR_AVALUO_COMERCIAL_SHORT);
                comer = avaluoComercial;
                if (avaluoComercial)
                {
                    rpvAvaluo.LocalReport.ReportPath = Constantes.URL_INFORMEAV_COM;
                }
                else
                {
                    rpvAvaluo.LocalReport.ReportPath = Constantes.URL_INFORMEAV_CAT;
                }

                // Deshabilitar la opciíon excel de exportación 
                foreach (RenderingExtension extension in rpvAvaluo.LocalReport.ListRenderingExtensions())
                {
                    if (extension.Name == Constantes.EXCEL_FILE_EXTENSION)
                    {
                        FieldInfo fi = extension.GetType().GetField("m_isVisible", BindingFlags.Instance | BindingFlags.NonPublic);
                        fi.SetValue(extension, false);
                    }
                }

                DseAvaluoConsulta dseAvCons = new DseAvaluoConsulta();
                clienteAvaluos = new AvaluosClient();

                try
                {
                    dseAvCons = clienteAvaluos.ObtenerAvaluo((int)idAvaluo);
                }
                finally
                {
                    clienteAvaluos.Close();
                    clienteAvaluos.Disconnect();
                }
                //setData();
                #region IdentificacionAvaluo
                decimal total = 0;

                decimal valorTotal = 0;
                valorTotal = informeDataSource.FEXAVA_CONSTRUCCIONES.GetValorTotal(comer);
                decimal valor = valorTotal;
                decimal importeTotal = valorTotal;
                ReportParameter ValorTotalCon = new ReportParameter("ParameterValorTotal", string.Format("{0:C}", valorTotal));
                total = valorTotal;

                valorTotal = informeDataSource.FEXAVA_AVALUO.GetValorTotalExtra();
                ReportParameter ValorTotalExtras = new ReportParameter("ParameterTotalExtras", string.Format("{0:C}", valorTotal));

                total += valorTotal;
                total += informeDataSource.FEXAVA_AVALUO.GetValorTerreno() + informeDataSource.FEXAVA_AVALUO.GetValorPrivativas();
                ReportParameter ValorTotalExtrasNum = new ReportParameter("ParameterTotalExtrasNum", string.Format("{0:C}", informeDataSource.FEXAVA_AVALUO[0].IsIMPORTETOTALENFOQUECOSTOSNull() ? total : informeDataSource.FEXAVA_AVALUO[0].IMPORTETOTALENFOQUECOSTOS));

                importeTotal += informeDataSource.FEXAVA_AVALUO.GetValorTerreno() + informeDataSource.FEXAVA_AVALUO.GetValorPrivativas() + informeDataSource.FEXAVA_AVALUO.GetImpInstEsp(valor);
                ReportParameter ValorTotalNum = new ReportParameter("ParameterValorTotalNum", string.Format("{0:C}", importeTotal));

                ReportParameter imp = new ReportParameter("ParameterImp", string.Format("{0:C}", informeDataSource.FEXAVA_AVALUO.GetImpInstEsp(valor)));

                string numAv = string.Empty;
                numAv = dseAvCons.FEXAVA_AVALUO_V[0].NUMEROAVALUO;
                ReportParameter numAvaluo = new ReportParameter("parameter_NumAvaluo", numAv);

                string numUnico = string.Empty;
                numUnico = dseAvCons.FEXAVA_AVALUO_V[0].NUMEROUNICO;
                ReportParameter numUnicoAvaluo = new ReportParameter("parameter_NumUnico", numUnico);

                string registroTFD = string.Empty;
                if (!informeDataSource.FEXAVA_AVALUO[0].IsIDPERSONASOCIEDADNull())
                {
                    registroTFD = informeDataSource.FEXAVA_AVALUO[0].REGTDF_SOCIEDAD;
                }
                else
                {
                    registroTFD = informeDataSource.FEXAVA_AVALUO[0].REGTDF_PERITO;
                }
                ReportParameter regTFD = new ReportParameter("parameter_registroTFD", registroTFD);

                string fechaAvaluo = (dseAvCons.FEXAVA_AVALUO_V[0].FECHA_DOCDIGITAL).ToShortDateString();
                ReportParameter fecha = new ReportParameter("parameter_fechaAvaluo", fechaAvaluo);

                rpvAvaluo.LocalReport.EnableExternalImages = true;
                rpvAvaluo.LocalReport.DataSources.Clear();
                rpvAvaluo.LocalReport.SetParameters(new ReportParameter[9] { 
                    ValorTotalCon, 
                    ValorTotalExtras,
                    ValorTotalExtrasNum,
                    ValorTotalNum,
                    imp,
                    numAvaluo,
                    numUnicoAvaluo,
                    regTFD,
                    fecha });


               
                if(comer)
                {
                    string utilidad = string.Empty;
                    List<decimal> lista = informeDataSource.FEXAVA_TERRENOMERCADO.getDatosTerrenoMercado(out utilidad);
                    ReportParameter totalIngesos = new ReportParameter("PAR_TOTALINGRESOS", lista[0].ToString());
                    ReportParameter totalEgresos = new ReportParameter("PAR_TOTALEGRESOS", lista[1].ToString());
                    ReportParameter par_utilidad = new ReportParameter("PAR_UTILIDAD", utilidad);
                    ReportParameter valorUnitarioResidual = new ReportParameter("PAR_VALORUNITARIORESIDUAL", lista[2].ToString());
                    rpvAvaluo.LocalReport.SetParameters(new ReportParameter[4] { totalIngesos, totalEgresos, par_utilidad, valorUnitarioResidual });
                }
               
                #endregion
                if(!comer)
                    rpvAvaluo.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaPrincipal()));
                else
                    rpvAvaluo.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", informeDataSource.FEXAVA_AVALUO.GetAvaPrincipal()));
                
     
                rpvAvaluo.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(ISubreportProcessingEventHandler);

                rpvAvaluo.DataBind();

                rpvAvaluo.LocalReport.Refresh();
          
            }
            catch (FaultException<ServiceAvaluos.AvaluosException> cex)
            {
                string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + cex.Detail.Descripcion;
                MostrarMensajeInfoExcepcion(msj);
            }
            catch (FaultException<ServiceAvaluos.AvaluosInfoException> ciex)
            {
                string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ciex.Detail.Descripcion;
                MostrarMensajeInfoExcepcion(msj);
            }
            catch (Exception ex)
            {
                ExceptionPolicyWrapper.HandleException(ex);
                string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
                MostrarMensajeInfoExcepcion(msj);
            }
         }
    }

    protected List<decimal> getDatosTerrenoMercado(out string utilidad)
    {
        utilidad = "0";
        List<decimal> lista = new List<decimal>();
        if (informeDataSource.FEXAVA_TERRENOMERCADO.Count > 0)
        {
            lista.Add(informeDataSource.FEXAVA_TERRENOMERCADO[0].IsTOTALINGRESOSNull() ? 0M : informeDataSource.FEXAVA_TERRENOMERCADO[0].TOTALINGRESOS);
            lista.Add(informeDataSource.FEXAVA_TERRENOMERCADO[0].IsTOTALEGRESOSNull() ? 0M : informeDataSource.FEXAVA_TERRENOMERCADO[0].TOTALEGRESOS);
            lista.Add(informeDataSource.FEXAVA_TERRENOMERCADO[0].IsVALORUNITARIORESIDUALNull() ? 0M : informeDataSource.FEXAVA_TERRENOMERCADO[0].VALORUNITARIORESIDUAL);
            utilidad = informeDataSource.FEXAVA_TERRENOMERCADO[0].IsUTILIDADNull() ? "0" : informeDataSource.FEXAVA_TERRENOMERCADO[0].UTILIDAD;

        }
        else
        {
            lista.Add(0M);
            lista.Add(0M);
            lista.Add(0M);
        }
        return lista;
    }


    protected decimal GetValorTotalExtra(DseAvaluoMantInf dseAvaluo)
    {
        decimal ret = 0;

        try
        {
            if (dseAvaluo.FEXAVA_AVALUO.Count > 0)
            {
                if (!dseAvaluo.FEXAVA_AVALUO[0].IsVALORTOTALIEEAOCNull())
                    ret = dseAvaluo.FEXAVA_AVALUO[0].VALORTOTALIEEAOC;
                else
                    ret = 0;
            }
        }
        catch
        {
            
        }
        return ret;
    }

    protected decimal GetValorTotal(DseAvaluoMantInf dseAvaluo)
    {
        decimal ret = 0;
        foreach (var constuccion in dseAvaluo.FEXAVA_CONSTRUCCIONES)
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

    protected decimal GetValorTerreno(DseAvaluoMantInf dseAvaluo)
    {
        return (decimal)dseAvaluo.FEXAVA_AVALUO.Compute("SUM(VALORTOTALDELTERRENOPROP)", "");
    }

    protected decimal GetValorPrivativas(DseAvaluoMantInf dseAvaluo)
    {
        return (decimal)dseAvaluo.FEXAVA_AVALUO.Compute("SUM(VALTOTCONSTRUCCIONESPRIVATIVAS)", "");
    }

    protected decimal GetImpInstEsp(DseAvaluoMantInf dseAvaluo, decimal valor)
    {
        return (GetValorPrivativas(dseAvaluo) + valor)*0.08M ;
    }


    protected decimal GetValorTotalPrivativas(DseAvaluoMantInf dseAvaluo)
    {
        decimal ret = 0;
        foreach (var constuccion in dseAvaluo.FEXAVA_CONSTRUCCIONES)
        {
            if (constuccion.CODTIPO.Equals("P"))
            {

                ret += constuccion.VALORFRACCION;
            }
        }
        return ret;
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
            //Se establece el botón de cancelar para los modalpopupextenders
            mpeErrorTareas.CancelControlID = errorTareas.ClientIdCancelacion;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }
    #endregion 

    #region MOSTRAR MENSAJES

    /// <summary>
    /// Mostrar mensaje de error asociado a una  excepcion.
    /// </summary>
    /// <param name="mensaje">El mensaje de error que se quiere mostrar.</param>
    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        uppErrorTareas.Update();
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }

    #endregion 

    #region EVENTOS

    /// <summary>
    /// Manejador, se lanza cuando se procesan los subinformes.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Información del evento.</param>
    void ISubreportProcessingEventHandler(object sender, SubreportProcessingEventArgs e)
    {
        try
        {
            e.Set(informeDataSource, comer);
            //e.SetDatasources(informeDataSource, comer);
        }
        finally
        {
            informeDataSource.Dispose();
        }
  }

    #endregion 

   
}
