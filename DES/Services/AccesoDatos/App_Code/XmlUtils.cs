using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.ServiceModel;
using SIGAPred.Common.Extensions;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;

/// <summary>
/// Métodos para facilitar el trabajo con el documento xml
/// </summary>
public static class XmlUtils
{
    /// <summary>
    /// Realiza una busqueda de elementos a partir de un elemento raiz.
    /// </summary>
    /// <param name="root">Elemento en el que se realizara la busqueda</param>
    /// <param name="id">Valor del atributo ID por el que se raliza la busqueda</param>
    /// <returns>Los elementos encontrados con ese id</returns>
    public static IEnumerable<XElement> XmlSearchById(XElement root, string id)
    {
        IEnumerable<XElement> queryConsulta = from campo in root.Descendants()
                                              where (string)campo.Attribute(Constantes.XML_IDENTIFICADOR_ELEMENTOS) == id
                                              select campo;

        return queryConsulta;
    }

    /// <summary>
    /// Realiza una busqueda de elementos a partir de varios elementos.
    /// </summary>
    /// <param name="rootN">Elementos en los que se realizara la busqueda</param>
    /// <param name="id">Valor del atributo ID por el que se raliza la busqueda</param>
    /// <returns>Los elementos encontrados con ese id</returns>
    public static IEnumerable<XElement> XmlSearchById(IEnumerable<XElement> rootN, string id)
    {
        IEnumerable<XElement> queryConsulta = from campo in rootN.Descendants()
                                              where (string)campo.Attribute(Constantes.XML_IDENTIFICADOR_ELEMENTOS) == id
                                              select campo;

        return queryConsulta;
    }

    /// <summary>
    /// Se comprueba que ninguno de los elementos este vacio.
    /// </summary>
    /// <param name="listElement">Lista de elementos a comprobar</param>
    /// <returns>True si existe algun elemento vacio</returns>
    public static bool IsListEmpty(List<IEnumerable<XElement>> listElement)
    {
        foreach (IEnumerable<XElement> cursorI in listElement)
        {
            if (cursorI.Count() > decimal.Zero)
            {
                foreach (XElement cursorX in cursorI)
                {
                    if (!cursorX.IsFull())
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Realiza el sumatorio de un campo contenido en el elemento entrante. Devuleve.
    /// </summary>
    /// <param name="elementos">Xelement que contiene los datos en cuestion.</param>
    /// <returns>
    /// Valor del sumatorio.
    /// </returns>
    public static decimal SumatorioCampo(IEnumerable<XElement> elementos)
    {
        decimal sumatorio = decimal.Zero;

        foreach (XElement cursor in elementos)
        {
            sumatorio += cursor.ToDecimalXElement();
        }

        return sumatorio;
    }

    /// <summary>
    /// Saca el valor numerico de una lista de elementos, en una lista de decimales.
    /// </summary>
    /// <param name="listElement">Lista de elementos de la que se sacan los valores.</param>
    /// <returns>
    /// Lista con los valores sacados.
    /// </returns>
    public static List<decimal> ConvetListElementToListDecimal(List<IEnumerable<XElement>> listElement)
    {
        List<decimal> listDecimal = new List<decimal>();
        foreach (IEnumerable<XElement> cursor in listElement)
        {
            if (cursor.IsFull())
            {
                listDecimal.Add(cursor.ToDecimalXElement());
            }
        }

        return listDecimal;
    }

    /// <summary>
    /// Comprueba que el valor del elemento que se le pasa es un decimal y no vacío
    /// </summary>
    /// <param name="queryXml">query XML</param>
    /// <returns>true/false para indicar si es o no decimal y no vacío</returns>
    public static bool EsDecimalXmlValido(IEnumerable<XElement> queryXml)
    {
        try
        {
            decimal i = queryXml.ToDecimalXElement();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Comprueba que el valor del elemento que se le pasa es un decimal y no vacío
    /// </summary>
    /// <param name="queryXml">query XML</param>
    /// <returns>true/false para indicar si es o no decimal y no vacío</returns>
    public static bool EsDecimalXmlValidoDif100(IEnumerable<XElement> queryXml)
    {
        bool ret = true;
        try
        {
            if (EsDecimalXmlValido(queryXml))
            {
                if (queryXml.ToDecimalXElement() >= 1)
                    ret = false;
            }
            else
                ret = false;

           
        }
        catch (Exception)
        {
            ret= false;
        }
        return ret;
    }

    /// <summary>
    /// Comprueba si la fecha es válida
    /// </summary>
    /// <param name="fechaXMl">fecha</param>
    /// <returns>true/false indicando si la fecha es o no válida</returns>
    public static bool EsFechaValida(IEnumerable<XElement> fechaXMl)
    {
        try
        {
            DateTime fechaAvaluo = Convert.ToDateTime(fechaXMl.ToStringXElement());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Comprueba si la fecha es válida
    /// </summary>
    /// <param name="fechaXMl">fecha</param>
    /// <returns>true/false indicando si la fecha es o no válida</returns>
    public static bool EsFechaMenorA5Anios(IEnumerable<XElement> fechaXMl)
    {
        //GULE - Se valida que la fecha no sea mayor a 5 años.
        try
        {
            DateTime fechaActual = DateTime.Now;
            DateTime fechaAvaluo = Convert.ToDateTime(fechaXMl.ToStringXElement());
            return (fechaActual.Year - fechaAvaluo.Year)>4? false : true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Convierte un elemento en decimal
    /// </summary>
    /// <param name="query">elemento</param>
    /// <returns>decimal</returns>
    public static decimal ToDecimalXElementAv(IEnumerable<XElement> query)
    {
        try
        {
            decimal result;
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo(Constantes.CULTURE_INFO, false);
            result = Convert.ToDecimal(query.ToStringXElement(), culture);
            return result;
        }
        catch (Exception ex)
        {
            ExceptionPolicyWrapper.HandleException(ex);
            throw new FaultException<AvaluosException>(new AvaluosException(ex.Message));
        }
    }

}

