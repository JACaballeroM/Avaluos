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
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

    /// <summary>
    /// Clase base excepciones.
    /// </summary>
    public class Excepciones
    {
        /// <summary>
        ///Excepción
        /// </summary>
        /// <param name="ex">La excepción.</param>
        public void exception(Exception ex)
        {
            ExceptionPolicy.HandleException(ex, "Security Policy");
        }

    }