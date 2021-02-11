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
using SIGAPred.Common.Web;

/// <summary>
/// Clase base para la gestión de User Control.
/// </summary>
public class UserControlBase : System.Web.UI.UserControl
{
    #region RedirecUtil

    /// <summary>
    /// Redirect
    /// </summary>
    private Redirect redirect = new Redirect();
    /// <summary>
    /// Obtiene la utilidad redirect.
    /// </summary>
    /// <value>
    /// La utilidad redirect.
    /// </value>
    protected Redirect RedirectUtil
    {
        get
        {
            if (redirect == null)
            {
                redirect = new Redirect();
                redirect.EndResponse = true;
            }
            return redirect;
        }
    }

    #endregion
}
