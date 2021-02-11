using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControlsCommon_Paginacion : System.Web.UI.UserControl
{

   /// <summary>
    ///  delegado para número de página
   /// </summary>
   /// <param name="numPage"></param>
    public delegate void PageNumberClickEventHandler2(int numPage);
    /// <summary>
    /// Manejador de número de página
    /// </summary>
    public event PageNumberClickEventHandler2 PageNumberClicked2;

    #region Propiedades

    /// <summary>
    /// Total páginas
    /// </summary>
    public int TotalCountPaged
    {
        get
        {
            return Convert.ToInt32(ViewState["countPaged"]);
        }
        set
        {
            ViewState["countPaged"] = value;
           
        }
    }

    /// <summary>
    /// Página actual
    /// </summary>
    public int PaginaActual
    {
        get
        {
            return Convert.ToInt32(ViewState["PageIndex"]);
        }
        set
        {
            ViewState["PageIndex"] = value;
            
        }
    }

    /// <summary>
    /// Tamaño de la página
    /// </summary>
    public int PageSize
    {
        get
        {
            try
            {
                ViewState["pageSize"] = ConfigurationManager.AppSettings.Get("PageSize");
            }
            catch (Exception)
            {
                ViewState["pageSize"] = 5;
            }
            return Convert.ToInt32(ViewState["pageSize"]);

        }
    }

    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                this.lblTotalNumberOfPages.Text = "Pagina : 1 de " + this.TotalCountPaged.ToString();
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            string msj = Constantes.MSJ_ERROR_OPERACION + Environment.NewLine + Environment.NewLine + ex.Message;
            MostrarMensajeInfoExcepcion(msj);
        }
    }

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

    private void MostrarMensajeInfoExcepcion(string mensaje)
    {
        errorTareas.TextoBasicoMostrar = Constantes.MSJ_ERROR_APLICACION;
        errorTareas.TextoAvanzadoMostrar = mensaje;
        mpeErrorTareas.Show();
        uppErrorTareas.Update();
    }

    protected void txtIraPag_TextChanged(object sender, EventArgs e)
    {
        int Numero = Convert.ToInt32(txtIraPag.Text);
        if (txtIraPag.Text != string.Empty && int.Parse(txtIraPag.Text) > 0 && int.Parse(txtIraPag.Text) <= TotalCountPaged)
        {
            PageNumberClicked2(int.Parse(txtIraPag.Text));
            this.lblTotalNumberOfPages.Text = "Pagina : " + txtIraPag.Text + " de " + this.TotalCountPaged.ToString();
            txtIraPag.Text = string.Empty;
        }
        else
        {
            this.txtIraPag.Text = string.Empty;
        }
    }
    
    protected void lnkFirtsPage_Click(object sender, EventArgs e)
    {
        PageNumberClicked2(1); 
    }

    protected void lnkPrevPage_Click(object sender, EventArgs e)
    {
        if ((PaginaActual - 1) >= 1)
        {
            PageNumberClicked2(PaginaActual - 1);
        }
    }

    protected void lnkNextPage_Click(object sender, EventArgs e)
    {
        if ((PaginaActual + 1) <= TotalCountPaged)
        {
            PageNumberClicked2(PaginaActual + 1);
        }
    }

    protected void lnkLastPage_Click(object sender, EventArgs e)
    {
        PageNumberClicked2(TotalCountPaged);
    }

}

