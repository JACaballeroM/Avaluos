using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio
{
    /// <summary>
    /// Clase para la gestión de políticas de excepciones
    /// </summary>
    public static class ExceptionPolicyWrapper
    {
        /// <summary>
        /// Método que getsiona las excepciones
        /// </summary>
        /// <param name="ex"></param>
        public static void HandleException(Exception ex)
        {
            string exceptionPolicy = System.Configuration.ConfigurationManager.AppSettings["SIGAPred.FuentesExternas.Avaluos.ExceptionPolicy"];
            ExceptionPolicy.HandleException(ex, exceptionPolicy);
        }
    }
}
