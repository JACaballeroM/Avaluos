using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de TiposInvestigacion
/// </summary>
public class TiposInvestigacion
{
	
        public string clave{get;set;}
        public string descripcion{get;set;}
        
        
}

public class CatalogoTipos
{
    public CatalogoTipos()
	{
            Tipos = new List<TiposInvestigacion>();
            TiposInvestigacion tipo =new TiposInvestigacion();
            TiposInvestigacion tipo1 = new TiposInvestigacion();
            TiposInvestigacion tipo2 = new TiposInvestigacion();
            TiposInvestigacion tipo3 = new TiposInvestigacion();

            tipo.clave = "T";
            tipo.descripcion = "Todos";
            Tipos.Add(tipo);
            tipo1.clave = "R";
            tipo1.descripcion ="Renta";
            Tipos.Add(tipo1);
            tipo2.clave = "V";
            tipo2.descripcion = "Venta";
            Tipos.Add(tipo2);
            tipo3.clave = "T";
            tipo3.descripcion = "Terrenos";
            Tipos.Add(tipo3);
   }
   public List<TiposInvestigacion> Tipos { get; set; }
}