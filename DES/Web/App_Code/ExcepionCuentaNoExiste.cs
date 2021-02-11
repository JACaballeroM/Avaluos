using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using ServiceAvaluos;
/// <summary>
/// Clase Excepión Cuenta catastral No Existe.
/// </summary>
public class ExcepcionCuentaNoExiste : ExceptionBase
{
         /// <summary>
         /// Excepción para la gestión de errores producidos por la inexistencia de la cuenta catastral.
         /// </summary>
         public ExcepcionCuentaNoExiste() 
         {
             base.Descripcion = "La cuenta catastral no existe";
         } 
}
