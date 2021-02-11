using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

/// <summary>
/// Clase para gestión de excepciones.
/// </summary>
public class ExceptionPolicyWrapper
{
    /// <summary>
    /// Método para la gestión de excepciones.
    /// </summary>
    /// <param name="ex">Excepción.</param>
    public static void HandleException(Exception ex)
    {
        string exceptionPolicy = System.Configuration.ConfigurationManager.AppSettings["SIGAPred.FuentesExternas.Avaluos.ExceptionPolicy"];
        //ExceptionPolicy.HandleException(ex, exceptionPolicy);
    }
}
