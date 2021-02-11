using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Session
/// </summary>
public class SessionVariables
{
    public static DataSet DsReporte
    {
        get
        {
            if (HttpContext.Current.Session["dsreporte"] != null)
            {
                return (DataSet)HttpContext.Current.Session["dsreporte"];
            }
            else
            {
                return null;
            }
        }
        set
        {
            HttpContext.Current.Session["dsreporte"] = value;
        }
    }

    public static void Dispose()
    { 
        HttpContext.Current.Session["dsreporte"] = null;
    }
}