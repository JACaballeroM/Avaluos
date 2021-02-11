using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Enum
{


    /// <summary>
    /// Estados Avaluo.
    /// </summary>
    public enum EstadosAvaluoEnum
    {

        /// <summary>
        /// Aceptado
        /// </summary>
        Aceptado = 0,


        /// <summary>
        /// Recibido
        /// </summary>
        Recibido = 1,


        /// <summary>
        /// Cancelado
        /// </summary>
        Cancelado = 2,


        /// <summary>
        /// Pendiente de Validacion
        /// </summary>
        PendienteValidacion = 3,

        /// <summary>
        /// En Validacion
        /// </summary>
        EnValidacion = 4,


        /// <summary>
        /// Rechazado
        /// </summary>
        Rechazado = 5,


        /// <summary>
        /// Enviado a Notario
        /// </summary>
        EnviadoNotario = 6,


        /// <summary>
        /// Enviado a Dictaminador
        /// </summary>
        EnviadoDictaminador = 7
    }

    /// <summary>
    /// Tipos Avaluo
    /// </summary>
    public enum TiposAvaluoEnum
    {

        /// <summary>
        /// Catastral
        /// </summary>
        Catastral = 2,


        /// <summary>
        /// Comercial
        /// </summary>
        Comercial = 1
    }

}
