using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Serialization;
using SIGAPred.Common.Extensions;
using MyExtentions;
/// <summary>
/// Clase que agrupa utilidades para trabajar con avalúos.
/// </summary>
public  static class AvaluosUtils
{
    /// <summary>
    /// A partir del tipo avalúo obtiene el prefijo nº único de avalúo que se guarda en BD, la última
    /// parte será un autonumérico que se genera en BD.
    /// </summary>
    /// <param name="tipo">Tipo avalúo (comercial/catastral)</param>
    /// <returns>
    /// Prefijo nº único de avalúo.
    /// </returns>
    public static string ObtenerNumUnicoAv(string tipo)
    {
        string anio = DateTime.Now.Year.ToString();
        return Constantes.NUM_UNICOAV_PREFIJO + tipo + Constantes.SIMBOLO_GUION + anio + Constantes.SIMBOLO_GUION;
    }

    /// <summary>
    /// Método que recorre todos los nodos del xml y elimina los espacios en blanco entre los tags
    /// del xml y el contenido.
    /// </summary>
    /// <param name="xmlAvaluo">[in,out] xml del avalúo sin espacios en blanco entre los tags del xml
    /// y el contenido.</param>
    public static void EliminarEspaciosBlancos(ref XmlDocument xmlAvaluo)
    {
        XmlNodeList NodeList;
        bool fin = false;
        NodeList = xmlAvaluo.DocumentElement.FirstChild.ChildNodes;
        XmlNode CurrentNode;
        bool fin2;

        foreach (XmlNode subnode in NodeList)
        {
            CurrentNode = subnode;
            fin = false;
            while (!fin)
            {
                if (CurrentNode != null) //Si el nodo tiene valor eliminar los espacios en blanco
                {
                    if (CurrentNode.Value != null)
                        CurrentNode.Value = ((CurrentNode.Value).TrimStart()).TrimEnd();
                }
                if (CurrentNode.HasChildNodes) // si tiene nodos hijos pasar al primer hijo
                {
                    CurrentNode = CurrentNode.FirstChild;
                }
                else if (CurrentNode.NextSibling != null)// si no tiene nodos hijos ver si tiene nodos hermanos y si tiene pasar al siguiente hermano.
                {
                    CurrentNode = CurrentNode.NextSibling;
                }
                else if (CurrentNode.ParentNode != subnode && CurrentNode.ParentNode != null) //Si tiene padre y el padre no es el nodo inicial
                {
                    fin2 = false;
                    while (!fin2)
                    {
                        if (CurrentNode.ParentNode != null && CurrentNode.ParentNode != subnode) // Ir subiendo niveles
                            CurrentNode = CurrentNode.ParentNode;
                        else
                        {
                            fin2 = true;
                            fin = true;

                        }
                        if (CurrentNode.NextSibling != null) // Cuando uno de los nodos tenga hermanos pasar al siguiente hermano
                        {
                            CurrentNode = CurrentNode.NextSibling;
                            fin2 = true;

                        }
                    }
                }
                else
                    fin = true;
            }
        }
    }

    /// <summary>
    /// Crea el nombre del fichero con el que se guardará el avalúo en la BD.
    /// </summary>
    /// <param name="tipoAv">Tipo avalúo (comercial/catastral)</param>
    /// <param name="cuentaCat">cuenta catastral.</param>
    /// <returns>
    /// nombre del fichero con el que se guardará el avalúo en BD.
    /// </returns>
    public static string CrearNombreDocumentoAv(string tipoAv ,string cuentaCat)
    {
        if (tipoAv.Equals(Constantes.XML_TIPO_CATASTRAL))
            return Constantes.NOMBRE_FICHEROAV_PREFIJO + "Cat-" + cuentaCat + Constantes.XML_FILE_EXTENSION;
        else
            return Constantes.NOMBRE_FICHEROAV_PREFIJO + "Com-" + cuentaCat + Constantes.XML_FILE_EXTENSION;
    }

    /// <summary>
    /// Obtiene la descripción que se guarda en BD asociada al documento avalúo.
    /// </summary>
    /// <param name="cuentaCat">Cuenta catastral.</param>
    /// <returns>
    /// Descripción asociada al documento avalúo en BD.
    /// </returns>
    public static string CrearDescripcionDocumentoAv(string cuentaCat)
    {
        return Constantes.DESC_FICHEROAV_PREFIJO + cuentaCat;
    }

    public static StringBuilder ValidarCamposObligatorios(this XElement xe, List<string> camposObligatorios)
    {
        StringBuilder sb = new StringBuilder();
        List<string> CamposFaltantes = camposObligatorios.Where(e => !xe.ExisteValor(e)).ToList();
        if (CamposFaltantes.Count > 0)
            sb = CamposFaltantes.Aggregate(new StringBuilder(), (a, b) => a.AppendLineEx(b));
        return sb;
    }

    public static bool ExisteValor(this XElement xe, string id)
    {
        return XmlUtils.XmlSearchById(xe, id).IsFull();
    }

    internal static StringBuilder AppendLineEx(this StringBuilder sb, string s)
    {
        return sb.AppendLine(string.Format("{0}-El valor es obligatorio."));
    }

    public static StringBuilder AppendLineErrorCalculo(this StringBuilder sb, string s)
    {
        return sb.AppendLine(string.Format("{0}-{2}", s, Constantes.PAR_ERROR_VALIDARCALCULO));
    }

    public static string ToStringValue(this XElement xe, string s)
    {
        return XmlUtils.XmlSearchById(xe, s).ToStringXElement();
    }

    public static decimal ToDecimalValue(this XElement xe, string s)
    {
        return XmlUtils.XmlSearchById(xe, s).ToDecimalXElement();
    }

    public static bool EsDecimalValido(this XElement xe, string s)
    {
        return XmlUtils.EsDecimalXmlValido(XmlUtils.XmlSearchById(xe, s));
    }


}

