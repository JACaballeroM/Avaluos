using System.Data;
using System.Web;
using ServiceAvaluos;
using ServiceCatastral;
using ServiceRCON;
using SIGAPred.Common.WCF;

/// <summary>
/// Clase para gestionar la cache de la aplicación Web.
/// </summary>
public static class ApplicationCache
{
    /// <summary>
    /// Nombre del dataset que contiene todos los catálogo.
    /// </summary>
    private const string CACHE_CATALOGOS = "DseCatalogos";

    /// <summary>
    /// Propiedad para controlar el acceso a DataSet con los catálogos de la aplicación.
    /// </summary>
    /// <value>
    /// DseCatalogos
    /// </value>
    public static DseAvaluosCatConsulta DseCatalogos
    {
        get
        {
            if (HttpContext.Current.Cache[CACHE_CATALOGOS] == null)
            {
                AvaluosClient clienteAvaluos = new AvaluosClient();
                DseAvaluosCatConsulta dsAvaluosCatConsulta = null;

                try
                {
                    dsAvaluosCatConsulta = clienteAvaluos.ObtenerCatalogos();
                }
                finally
                {
                    clienteAvaluos.Disconnect();
                }

                dsAvaluosCatConsulta.SchemaSerializationMode = SchemaSerializationMode.ExcludeSchema;
                HttpContext.Current.Cache.Insert(CACHE_CATALOGOS, dsAvaluosCatConsulta);
            }

            return (DseAvaluosCatConsulta)HttpContext.Current.Cache[CACHE_CATALOGOS];
        }
        set
        {
            HttpContext.Current.Cache[CACHE_CATALOGOS] = value;
        }
    }
}
