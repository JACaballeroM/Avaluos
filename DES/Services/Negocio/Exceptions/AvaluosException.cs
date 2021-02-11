using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions
{
   
     /// <summary>
    /// Clase que define una excepción en los métodos de comunicación con agilepoint
    /// </summary>
    [DataContract(Namespace = "http://SIGAPred.FuentesExternas/Avaluos/Negocio/Exceptions")]
    public class AvaluosException:ExceptionBase
    {
        #region Constructor

        /// <summary>
        /// Crea una nueva instancia al objeto
        /// </summary>
        /// <param name="ex">Excepción capturada</param>
        public AvaluosException(Exception ex)
            : base(ex)
        {
        }

        /// <summary>
        /// Crea una nueva instancia al objeto
        /// </summary>
        /// <param name="message">Mensaje de la excepcion</param>
        public AvaluosException(string  message)
            : base(message)
        {
        }

        #endregion
    }
}
