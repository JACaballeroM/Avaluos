// Decompiled with JetBrains decompiler
// Type: ValidarCamposCalculados
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using SIGAPred.Common.Extensions;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

public static class ValidarCamposCalculados
{
    public static bool ValidarCampoCalculado_c_6_4(
      Decimal valorCalculado,
      Decimal c_6_2,
      Decimal c_6_3)
    {
        try
        {
            return valorCalculado.ToRound2() == ((1M - c_6_2 / 100M) * c_6_3).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_d_5_n_10(
      Decimal valorCalculado,
      Decimal d_5_n_3_2,
      Decimal d_5_n_4_2,
      Decimal d_5_n_5_2,
      Decimal d_5_n_6_2,
      Decimal d_5_n_7_2
      //  ,Decimal d_5_n_9
      )
    {
        try
        {
            //return valorCalculado.ToRound2() == Math.Max(0.6M, (d_5_n_3 * d_5_n_4 * d_5_n_5 * d_5_n_6 * d_5_n_7 * d_5_n_9).ToRound2());
            return valorCalculado.ToRound2() ==  (d_5_n_3_2 * d_5_n_4_2 * d_5_n_5_2 * d_5_n_6_2 * d_5_n_7_2 ).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_d_5_n_10", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_d_5_n_11(
      Decimal valorCalculado,
      Decimal h_1_4,
      Decimal d_5_n_2,
      Decimal d_5_n_10)
    {
        try
        {
            return valorCalculado.ToRound2() == (h_1_4 * d_5_n_2 * d_5_n_10).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_d_5_n_11(
      Decimal valorCalculado,
      Decimal d_5_n_8,
      Decimal d_5_n_12)
    {
        try
        {
            return valorCalculado.ToRound2() == (d_5_n_8 * d_5_n_12).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_h_1_1_n_18_1(
      Decimal valorCalculado,
      Decimal h_1_1_n_15,
      Decimal h_1_1_n_16,
      Decimal h_1_1_n_17)
    {
        try
        {
            return valorCalculado.ToRound2() == (h_1_1_n_15 * h_1_1_n_16 * (1M / h_1_1_n_17)).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_d_11(Decimal valorCalculado, Decimal sumatorio_d_5_n_2)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_d_5_n_2.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_d_12(Decimal valorCalculado, Decimal sumatorio_d_5_N_11)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_d_5_N_11.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_d_13(Decimal valorCalculado, Decimal d_12, Decimal d_6)
    {
        try
        {
            return valorCalculado.ToRound2() == (d_12 * d_6).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_1_n_9(
      Decimal valorCalculado,
      Decimal e_2_n_8,
      Decimal e_2_n_7)
    {
        try
        {
            return valorCalculado.ToRound2() == Math.Max(0M, e_2_n_8 - e_2_n_7).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    //JACM Se da de baja el campo 2021-02-11
    /*public static bool ValidarClaveConservacion(string ClaveConservacion, string uso)
    {
        try
        {
            return !ValidarCamposCalculados.aplicaDepreciacion(uso) || uso == "H" ? ClaveConservacion == "3" : ValidarCamposCalculados.EsClaveValida(ClaveConservacion);
        }
        catch (Exception ex)
        {
            return false;
        }
    }*/

    public static bool ValidarCampoCalculado_e_2_1_n_13(
      Decimal valorCalculado,
      Decimal e_2_1_n_7,
      Decimal e_2_1_n_8,
      string uso)
    {
        try
        {
            return ValidarCamposCalculados.aplicaDepreciacion(uso) ? 
                valorCalculado.ToRound2() == Math.Max(Convert.ToDecimal(0.6), 
                (0.1M * e_2_1_n_8 + 0.9M * (e_2_1_n_8 - e_2_1_n_7)) / e_2_1_n_8).ToRound2() : 
                valorCalculado == 1M;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_1_n_14(
      Decimal valorCalculado,
      Decimal e_2_1_n_10,
      Decimal e_2_1_n_13)
    {
        try
        {
            return valorCalculado.ToRound2() == Math.Max(0.6M, (e_2_1_n_10 * e_2_1_n_13).ToRound2());
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_1_n_15_Com(
      Decimal valorCalculado,
      Decimal e_2_1_n_12,
      Decimal e_2_1_n_14,
      Decimal e_2_n_11)
    {
        try
        {
            return valorCalculado.ToRound2() == (e_2_1_n_12 * e_2_1_n_14 * e_2_n_11).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_1_n_15(
      Decimal valorCalculado,
      Decimal e_2_1_n_11,
      Decimal e_2_1_n_12,
      Decimal e_2_1_n_13)
    {
        try
        {
            return valorCalculado.ToRound2() == (e_2_1_n_11 * e_2_1_n_12 * e_2_1_n_13).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_e_2_1_n_15", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_1_n_15_Cat(
      Decimal valorCalculado,
      Decimal e_2_1_n_11,
      Decimal e_2_1_n_16,
      Decimal e_2_1_n_17)
    {
        try
        {
            return valorCalculado.ToRound2() == ((e_2_1_n_16 * e_2_1_n_17) * e_2_1_n_11).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_e_2_1_n_15_Cat", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_1_n_17(
      Decimal valorCalculado,
      Decimal e_2_1_n_7,
      Decimal e_2_1_n_8,
      DateTime fechaAvaluo)
    {
        try
        {
            DemeritoSection section = (DemeritoSection)ConfigurationManager.GetSection("DemeritoSection");
            Decimal num = 0M;
            foreach (DemeritoSectionElement demeritoSectionElement in section.HashKeys.Cast<DemeritoSectionElement>().Where<DemeritoSectionElement>((Func<DemeritoSectionElement, bool>)(item => fechaAvaluo >= item.FechaInicio && fechaAvaluo <= item.FechaFin)))
                num = (Decimal)demeritoSectionElement.Valor;
            //return valorCalculado.ToRound2() == ((100M - Math.Min(40M, e_2_1_n_7 * num)) / 100M).ToRound2();
            if (e_2_1_n_7 > 50M) //Se topa el valor de e_2_1_n_7 a 50
                e_2_1_n_7 = 50M;

            return valorCalculado.ToRound2() == ((100M - (e_2_1_n_7 * num)) / 100M).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_e_2_1_n_17", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_2(
      Decimal valorCalculado,
      Decimal sumatorio_e_2_1_n_11)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_e_2_1_n_11.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_3(
      Decimal valorCalculado,
      Decimal sumatorio_e_2_1_n_15)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_e_2_1_n_15.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_4(
      Decimal valorCalculado,
      Decimal e_2_3,
      Decimal d_6)
    {
        return valorCalculado.ToRound2() == (e_2_3 * d_6).ToRound2();
    }

    /*public static bool ValidarCampoCalculado_e_2_5_n_9(
      Decimal valorCalculado,
      Decimal e_2_5_n_8,
      Decimal e_2_5_n_7)
    {
        try
        {
            return valorCalculado.ToRound2() == Math.Max(0M, e_2_5_n_8 - e_2_5_n_7).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_5_n_13(
      Decimal valorCalculado,
      Decimal e_2_5_n_7,
      Decimal e_2_5_n_8,
      string uso)
    {
        try
        {
            return ValidarCamposCalculados.aplicaDepreciacion(uso) ? valorCalculado.ToRound2() == Math.Max(Convert.ToDecimal(0.6), (0.1M * e_2_5_n_8 + 0.9M * (e_2_5_n_8 - e_2_5_n_7)) / e_2_5_n_8).ToRound2() : valorCalculado == 1M;
        }
        catch (Exception ex)
        {
            return false;
        }
    }*/

    public static bool ValidarCampoCalculado_e_2_5_n_14(
      Decimal valorCalculado,
      Decimal e_2_2_n_13,
      Decimal e_2_5_n_10)
    {
        try
        {
            Decimal val1 = 0.6M;
            return valorCalculado.ToRound2() == Math.Max(val1, e_2_2_n_13 * e_2_5_n_10).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_5_n_15(
      Decimal valorCalculado,
      Decimal e_2_5_n_11,
      Decimal e_2_5_n_12,
      Decimal e_2_5_n_13)
    {
        try
        {
            log("ValidarCampoCalculado_e_2_5_n_15 ", "Datos: ", valorCalculado.ToRound2().ToString() + " | "
                + valorCalculado.ToString()
                + " | " + e_2_5_n_11.ToString()
                + " | " + e_2_5_n_12.ToString()
                + " | " + e_2_5_n_13.ToString()
                + " | " + (e_2_5_n_11 * e_2_5_n_12 * e_2_5_n_13).ToRound2()
                );
            return valorCalculado.ToRound2() == (e_2_5_n_11 * e_2_5_n_12 * e_2_5_n_13).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_e_2_5_n_15 ", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_5_n_15_Cat(
      Decimal valorCalculado,
      Decimal e_2_5_n_11,
      Decimal e_2_5_n_16,
      Decimal e_2_5_n_17)
    {
        try
        {
            log("ValidarCampoCalculado_e_2_5_n_15_Cat ","Datos: ",valorCalculado.ToRound2().ToString()+" | "
                + e_2_5_n_11.ToString()
                + " | " + e_2_5_n_16.ToString()
                + " | " + e_2_5_n_17.ToString()
                + " | " + (e_2_5_n_16 * e_2_5_n_17 * e_2_5_n_11).ToRound2()
                );
            return valorCalculado.ToRound2() == (e_2_5_n_16 * e_2_5_n_17 * e_2_5_n_11).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_e_2_5_n_15_Cat ", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_5_n_17(
      Decimal valorCalculado,
      Decimal e_2_5_n_7,
      Decimal e_2_5_n_8,
      DateTime fechaAvaluo)
    {
        try
        {
            DemeritoSection section = (DemeritoSection)ConfigurationManager.GetSection("DemeritoSection");
            Decimal num = 0M;
            foreach (DemeritoSectionElement demeritoSectionElement in section.HashKeys.Cast<DemeritoSectionElement>().Where<DemeritoSectionElement>((Func<DemeritoSectionElement, bool>)(item => fechaAvaluo >= item.FechaInicio && fechaAvaluo <= item.FechaFin)))
                num = (Decimal)demeritoSectionElement.Valor;
            //return valorCalculado.ToRound2() == ((100M - Math.Min(40M, e_2_5_n_7 * num)) / 100M).ToRound2();
            if (e_2_5_n_7 > 50M) //Se topa el valor de e_2_1_n_7 a 50
                e_2_5_n_7 = 50M;
            return valorCalculado.ToRound2() == ((100M - (e_2_5_n_7 * num)) / 100M).ToRound2();
        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_e_2_5_n_17 ", ex.Message,ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_6(Decimal valorCalculado, Decimal sumatorio_e_2_n_11)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_e_2_n_11.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_7(
      Decimal valorCalculado,
      Decimal sumatorio_e_2_5_n_15)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_e_2_5_n_15.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_2_8(
      Decimal valorCalculado,
      Decimal e_2_7,
      Decimal d_6)
    {
        return valorCalculado.ToRound2() == (e_2_7 * d_6).ToRound2();
    }

    internal static List<Decimal> multiplicados { get; set; }

    public static bool ValidarCampoCalculado_e_2_8(
      List<Decimal> ListaPorcentaje,
      List<Decimal> ListaValor,
      Decimal valorCalculado)
    {
        return ValidarCamposCalculados.ValidarCampoCalculado_e_2_8_Interno(ListaPorcentaje, ListaValor, valorCalculado);
    }

    internal static bool ValidarCampoCalculado_e_2_8_Interno(
      List<Decimal> ListaPorcentaje,
      List<Decimal> ListaValor,
      Decimal valorCalculado)
    {
        ValidarCamposCalculados.multiplicados = new List<Decimal>();
        ValidarCamposCalculados.ObtenerMultiplicados(ListaPorcentaje, ListaValor, 0);
        return valorCalculado.ToRound2() == ValidarCamposCalculados.multiplicados.Sum().ToRound2();
    }

    internal static void ObtenerMultiplicados(
      List<Decimal> ListaPorcentaje,
      List<Decimal> ListaValor,
      int i)
    {
        if (i >= ListaPorcentaje.Count)
            return;
        ValidarCamposCalculados.multiplicados.Add(ListaPorcentaje[i] * ListaValor[i]);
        ValidarCamposCalculados.ObtenerMultiplicados(ListaPorcentaje, ListaValor, i + 1);
    }

    public static bool ValidarCampoCalculado_e_3(
      Decimal valorCalculado,
      Decimal sumatorio_privativas,
      Decimal sumatorio_comunes)
    {
        try
        {
            Decimal round2 = (sumatorio_comunes + sumatorio_privativas).ToRound2();
            return valorCalculado.ToRound2() == round2;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_4(
      Decimal valorCalculado,
      Decimal sumatorio_privativas,
      Decimal sumatorio_comunes)
    {
        try
        {
            Decimal round2 = (sumatorio_comunes + sumatorio_privativas).ToRound2();
            return valorCalculado.ToRound2() == round2;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_e_5(
      Decimal valorCalculado,
      Decimal sumatorio_privativas,
      Decimal sumatorio_comunes)
    {
        try
        {
            Decimal round2 = (sumatorio_comunes + sumatorio_privativas).ToRound2();
            return valorCalculado.ToRound2() == round2;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_9_1_n_8(
      Decimal valorCalculado,
      Decimal f_9_1_n_5,
      Decimal f_9_1_n_6)
    {
        try
        {
            Decimal val1 = 0.6M;
            return valorCalculado.ToRound2() == Math.Max(val1, (1M - f_9_1_n_5 / f_9_1_n_6).ToRound2());
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_10_1_n_8(
      Decimal valorCalculado,
      Decimal f_10_1_n_5,
      Decimal f_10_1_n_6)
    {
        try
        {
            Decimal val1 = 0.6M;
            return valorCalculado.ToRound2() == Math.Max(val1, 1M - f_10_1_n_5 / f_10_1_n_6).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_10_2_n_8(
      Decimal valorCalculado,
      Decimal f_10_2_n_5,
      Decimal f_10_2_n_6)
    {
        try
        {
            Decimal val1 = 0.6M;

            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString()
                + " ValidarCampoCalculado_f_10_2_n_8 "
                + f_10_2_n_5.ToString() + "\n\r"
                + f_10_2_n_6.ToRound2().ToString() + "\n\r"
                + valorCalculado.ToRound2().ToString() + "\n\r"
                + Math.Max(val1, 1M - (f_10_2_n_5 / f_10_2_n_6)).ToRound2().ToString() + "\n\r"
                + "\n\r");

            return valorCalculado.ToRound2() == Math.Max(val1, 1M - (f_10_2_n_5 / f_10_2_n_6)).ToRound2();

        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_f_10_2_n_8 ", ex.Message, ex.StackTrace);
            return false;
        }
    }
    //JACM se da de baja la validación
    /*public static bool ValidarCampoCalculado_f_11_1_n_8(
      Decimal valorCalculado,
      Decimal f_11_1_n_5,
      Decimal f_11_1_n_6,
      string clave)
    {
        try
        {
            Decimal val1 = 0.6M;
            if (clave == "OC03" || clave == "OC06")
                return valorCalculado.ToRound2() == 1M;
            Decimal round2 = ((0.1M * f_11_1_n_6 + 0.9M * (f_11_1_n_6 - f_11_1_n_5)) / f_11_1_n_6).ToRound2();
            return valorCalculado.ToRound2() == Math.Max(val1, round2);
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_11_2_n_8(
      Decimal valorCalculado,
      Decimal f_11_2_n_5,
      Decimal f_11_2_n_6,
      string clave)
    {
        try
        {
            Decimal val1 = 0.6M;
            if (clave == "OC03" || clave == "OC06")
                return valorCalculado.ToRound2() == 1M;
            Decimal round2 = ((0.1M * f_11_2_n_6 + 0.9M * (f_11_2_n_6 - f_11_2_n_5)) / f_11_2_n_6).ToRound2();
            return valorCalculado.ToRound2() == Math.Max(val1, round2);
        }
        catch (Exception ex)
        {
            return false;
        }
    }*/

    public static bool ValidarCampoCalculado_f_9_1_n_9(
      Decimal valorCalculado,
      Decimal f_9_1_n_4,
      Decimal f_9_1_n_7,
      Decimal f_9_1_n_8)
    {
        try
        {
            Decimal val1 = 0.6M;
            return valorCalculado.ToRound2() == Math.Max(val1, (f_9_1_n_4 * f_9_1_n_7 * f_9_1_n_8).ToRound2());
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_9_2_n_8(
      Decimal valorCalculado,
      Decimal f_9_2_n_5,
      Decimal f_9_2_n_6)
    {
        try
        {
            Decimal val1 = 0.6M;
            return valorCalculado.ToRound2() == Math.Max(val1, 1M - f_9_2_n_5 / f_9_2_n_6).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_9_2_N_9(
      Decimal valorCalculado,
      Decimal f_9_2_n_4,
      Decimal f_9_2_n_7,
      Decimal f_9_2_n_8)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_9_2_n_4 * f_9_2_n_7 * f_9_2_n_8).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_9_3(
      Decimal valorCalculado,
      Decimal sumatorio_f_9_1_n_9)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_f_9_1_n_9.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_9_4(
      Decimal valorCalculado,
      Decimal sumatorio_f_9_2_n_9)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_f_9_2_n_9.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_10_1_n_9(
      Decimal valorCalculado,
      Decimal f_10_1_n_4,
      Decimal f_10_1_n_7,
      Decimal f_10_1_n_8)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_10_1_n_4 * f_10_1_n_7 * f_10_1_n_8).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_10_2_n_9(
      Decimal valorCalculado,
      Decimal f_10_2_n_4,
      Decimal f_10_2_n_7,
      Decimal f_10_2_n_8)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_10_2_n_4 * f_10_2_n_7 * f_10_2_n_8).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_10_3(
      Decimal valorCalculado,
      Decimal sumatorio_f_10_1_n_9)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_f_10_1_n_9.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_10_4(
      Decimal valorCalculado,
      Decimal sumatorio_f_10_2_n_9)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_f_10_2_n_9.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_11_1_n_9(
      Decimal valorCalculado,
      Decimal f_11_1_n_4,
      Decimal f_11_1_n_7,
      Decimal f_11_1_n_8)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_11_1_n_4 * f_11_1_n_7 * f_11_1_n_8).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_11_2_n_9(
      Decimal valorCalculado,
      Decimal f_11_2_n_4,
      Decimal f_11_2_n_7,
      Decimal f_11_2_n_8)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_11_2_n_4 * f_11_2_n_7 * f_11_2_n_8).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_11_3(
      Decimal valorCalculado,
      Decimal sumatorio_f_11_1_n_9)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_f_11_1_n_9.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_11_4(
      Decimal valorCalculado,
      Decimal sumatorio_f_11_2_n_9)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio_f_11_2_n_9.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_12(
      Decimal valorCalculado,
      Decimal f_9_3,
      Decimal f_10_3,
      Decimal f_11_3)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_9_3 + f_10_3 + f_11_3).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_13(
      Decimal valorCalculado,
      Decimal f_9_4,
      Decimal f_10_4,
      Decimal f_11_4)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_9_4 + f_10_4 + f_11_4).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_14(Decimal valorCalculado, Decimal f_13, Decimal d_6)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_13 * d_6).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_14(
      List<Decimal> ImporteEspecial,
      List<Decimal> PorcentajeEspecial,
      List<Decimal> ImporteAccesorio,
      List<Decimal> PorcentajeAccesorio,
      List<Decimal> ImporteComplementaria,
      List<Decimal> PorcentajeComplementaria,
      Decimal valorCalculado)
    {
        try
        {
            ValidarCamposCalculados.multiplicados = new List<Decimal>();
            Decimal dec = ValidarCamposCalculados.getSuma(ImporteEspecial, PorcentajeEspecial) + ValidarCamposCalculados.getSuma(ImporteAccesorio, PorcentajeAccesorio) + ValidarCamposCalculados.getSuma(ImporteComplementaria, PorcentajeComplementaria);
            return valorCalculado.ToRound2() == dec.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_f_14(
      List<Decimal> ImporteEspecial,
      List<Decimal> ImporteAccesorio,
      List<Decimal> ImporteComplementaria,
      Decimal d6,
      Decimal valorCalculado)
    {
        try
        {
            Decimal dec = (ImporteEspecial.Sum() + ImporteAccesorio.Sum() + ImporteComplementaria.Sum()) * d6;
            return valorCalculado.ToRound2() == dec.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    internal static Decimal getSuma(List<Decimal> lista1, List<Decimal> lista2)
    {
        ValidarCamposCalculados.multiplicados = (List<Decimal>)null;
        ValidarCamposCalculados.multiplicados = new List<Decimal>();
        ValidarCamposCalculados.ObtenerMultiplicados(lista1, lista2, 0);
        return ValidarCamposCalculados.multiplicados.Sum();
    }

    public static bool ValidarCampoCalculado_f_15(Decimal valorCalculado, Decimal f_12, Decimal d_6)
    {
        try
        {
            return valorCalculado.ToRound2() == (f_12 * d_6).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_h_1_1_n_17(
      Decimal fre,
      Decimal h_1_1_n_10_2,
      Decimal h_1_1_n_11_2,
      Decimal h_1_1_n_12_2,
      Decimal h_1_1_n_13_2,
      Decimal h_1_1_n_14_2)
    {
        try
        {
            //Decimal val1 = 0.6M;
            System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString() 
                + " ValidarCampoCalculado_h_1_1_n_17 "
                + fre.ToString() + "\n\r"
                + h_1_1_n_10_2.ToString() + "\n\r"
                + h_1_1_n_11_2.ToString() + "\n\r"
                + h_1_1_n_12_2.ToString() + "\n\r"
                + h_1_1_n_13_2.ToString() + "\n\r"
                + h_1_1_n_14_2.ToString() + "\n\r"
                + fre.ToRound2().ToString() + "\n\r"
                + (1M / (h_1_1_n_10_2 * h_1_1_n_11_2 * h_1_1_n_12_2 * h_1_1_n_13_2 * h_1_1_n_14_2)).ToRound2().ToString() + "\n\r"
                + "\n\r");

            return fre.ToRound2() == //Math.Max(val1, 
                (1M / (h_1_1_n_10_2 * h_1_1_n_11_2 * h_1_1_n_12_2 * h_1_1_n_13_2 * h_1_1_n_14_2)).ToRound2();

        }
        catch (Exception ex)
        {
            log("ValidarCampoCalculado_h_1_1_n_17", ex.Message, ex.StackTrace);
            return false;
        }
    }

    public static bool ValidarCampoCalculado_i_6(
      Decimal valorCalculado,
      Decimal d_13,
      Decimal f_12,
      Decimal f_14,
      Decimal e_2_3,
      Decimal e_2_8)
    {
        try
        {
            return valorCalculado.ToRound2() == (d_13 + e_2_3 + e_2_8 + f_12 + f_14).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_j_4(
      Decimal valorCalculado,
      Decimal e_2_3,
      Decimal e_2_7,
      Decimal d_6,
      bool esCondominial)
    {
        try
        {
            Decimal num = Convert.ToDecimal(0.08);
            return valorCalculado.ToRound2() == ((e_2_3 + e_2_7) * num).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_j_5(
      Decimal valorCalculado,
      Decimal j_4,
      Decimal d_13,
      Decimal e_2_3,
      Decimal e_2_7,
      Decimal d_6,
      bool esCondominial)
    {
        try
        {
            return !esCondominial ? valorCalculado.ToRound2() == (d_13 + e_2_3 + e_2_7 + j_4).ToRound2() : valorCalculado.ToRound2() == (d_13 + e_2_3 + e_2_7 + j_4).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_j_7(Decimal valorCalculado, Decimal j_5, Decimal j_6)
    {
        try
        {
            return valorCalculado.ToRound2() == (j_5 * j_6).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_k_1(
      Decimal valorCalculado,
      Decimal h_4_2_7,
      Decimal e_2_2)
    {
        try
        {
            return valorCalculado.ToRound2() == (h_4_2_7 * e_2_2).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_k_2_12(Decimal valorCalculado, Decimal sumatorio)
    {
        try
        {
            return valorCalculado.ToRound2() == sumatorio.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_k_2_13(
      Decimal valorCalculado,
      Decimal k_2_12,
      Decimal k_1)
    {
        try
        {
            return valorCalculado.ToRound2() == (k_2_12 / k_1 / 100M).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_k_3(Decimal valorCalculado, Decimal k_1, Decimal k_2_12)
    {
        try
        {
            return valorCalculado.ToRound2() == ((k_1 - k_2_12) * 12M).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_k_5(Decimal valorCalculado, Decimal k_3, Decimal k_4)
    {
        try
        {
            Decimal round2 = (k_3 / k_4).ToRound2();
            return valorCalculado.ToRound2() == round2;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_p_4(Decimal valorCalculado, Decimal p_2, Decimal p_3)
    {
        try
        {
            return valorCalculado.ToRound2() == (p_3 / p_2).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_p_5(Decimal valorCalculado, Decimal p_4, Decimal o_1)
    {
        try
        {
            return valorCalculado.ToRound2() == (p_4 * o_1).ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool ValidarCampoCalculado_p_5(
      Decimal valorCalculado,
      Decimal p_4,
      Decimal o_1,
      string fecha)
    {
        try
        {
            Decimal dec = int.Parse(fecha.Split('-')[0]) >= 1993 ? o_1 / p_4 : o_1 / p_4 * 1000M;
            return valorCalculado.ToRound2() == dec.ToRound2();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static bool aplicaDepreciacion(string coduso)
    {
        string[] strArray = new string[4]
        {
      "PE",
      "PC",
      "P",
      "J"//,
      //"H"
        };
        bool flag = true;
        if (((IEnumerable<string>)strArray).Contains<string>(coduso))
            flag = false;
        return flag;
    }

    public static bool EsClaveValida(string clave) => ((IEnumerable<string>)new string[4]
    {
    "1",
    "2",
    "3",
    "4"
    }).Contains<string>(clave);

    private static void log(string origen, string mensaje, string trace)
    {

        System.IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Avaluos.log", "\n\r" + DateTime.Now.ToString() + " " + origen + ": Exception: " + mensaje + "\n\r" + trace + "\n\r");

    }

}
