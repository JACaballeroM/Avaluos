// Decompiled with JetBrains decompiler
// Type: SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Interfaces.IAvaluos
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using SIGAPred.FuentesExternas.Avaluos.Services.AccesoDatos;
using SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Exceptions;
using System;
using System.Data;
using System.ServiceModel;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Interfaces
{
    [ServiceContract]
    public interface IAvaluos
    {
        [OperationContract]
        string Test();

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        string RegistrarAvaluo(byte[] xmlBytes, string idPersona);

        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        void RegistrarAvaluoEspecial(byte[] xmlBytes);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        void RegistrarIntentoFallido(
          DseAvaluoConsulta.FEXAVA_INTENTOFALLIDO_PDataTable par_intentoFallidoDT);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        void CambiarEstadoAvaluo(Decimal idavaluos, Decimal codEstadoAvaluoNuevo);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        void AsignarNotarioAvaluo(Decimal idavaluos, Decimal idNotario);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaObtnNum(
          string cuentaCatastral,
          int idNotario,
          string registro,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        Cedula ObtenerCedula(Decimal idAvaluo);

        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        DseAvaluoMantInf GuardarAvaluoInformeComercial(Decimal idAvaluo);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoMantInf GetAvaluoAntecedentes(Decimal idAvaluo);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoMantInf TestAv();

        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        DataSet GetInvestigacionMercado(
          string region,
          string manzana,
          string tipo,
          Decimal idDelegacion,
          Decimal idColonia,
          string fechaInicio,
          string fechaFinal);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorFechaNotario(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          int idNotario,
          string registro,
          string vigente,
          string numValuo,
          string idAvaluo,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string sortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorFechaNotarioSF(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          int idNotario,
          string registro,
          string vigente,
          string numAvaluo,
          string idAvaluo,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string sortExpression);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorRegistroPeritoNotario(
          string registro,
          int idNotario,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorFechaPeritoVigenciaEstado(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          int idPerito,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorEstadoPerito(
          Decimal idPersona,
          int codEstado,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorNumValuoPerito_EstadoVig(
          string numValuo,
          Decimal idPersona,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        DseAvaluoConsulta ObtenerAvaluosPorFechaEstado(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          string vigente,
          int codEstado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        DseAvaluoConsulta ObtenerAvaluosPorProximidad(
          int idAvaluo,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluo(int idAvaluo);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluoPorIdAvaluoEstadoVigencia(
          string idAvaluo,
          int codestado,
          string estavigente,
          int idpersonaperito,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluoPorVigEstTodosPeritosSoci(
          string idAvaluo,
          int codestado,
          string estavigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluoPorIdAvaluos_Soci_Vigencia_Estado(
          string idAvaluo,
          int codestado,
          string estavigente,
          int idpersonasoci,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorFechaSoci_EstadoVig(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          Decimal idSoci,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralNotario(
          string cuentaCatastral,
          int idNotario,
          string registro,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralNotarioSF(
          string cuentaCatastral,
          int idNotario,
          string registro,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralPerito(
          string cuentaCatastral,
          string idPerito,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastralPeritoVigenciaEstado(
          string cuentaCatastral,
          string idPerito,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaCatastral(
          string cuentaCatastral,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaVigenciaEstado(
          string cuentaCatastral,
          int codestado,
          string vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorProximidadPerito(
          int idAvaluo,
          int idPerito,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorProximidadSociedad(
          int idAvaluo,
          int idSociedad,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorNumAvaluoSoci(
          string numValuo,
          string registroPerito,
          Decimal idSoci,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorNumAvaluoSoci_EstadoVig(
          string numValuo,
          string registroPerito,
          Decimal idSoci,
          string vigente,
          Decimal codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorNumValuoVigEstado(
          string numValuo,
          string registroPerito,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorFechaSoci(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          Decimal idSoci,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaSoci(
          string cuentaCatastral,
          Decimal idSoci,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta ObtenerAvaluosPorCuentaSociEstadoVig(
          string cuentaCatastral,
          Decimal idSoci,
          string vigente,
          int codestado,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseAvaluoConsulta ObtenerAvaluosPorEstadoSoci(
          Decimal idSoci,
          int codEstado,
          bool vigente,
          int pageSize,
          int indice,
          ref int rowsTotal,
          string SortExpression);

        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        DseAvaluosCatConsulta ObtenerCatalogos();

        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        byte[] GenerarAvaluo(
          string region,
          string manzana,
          string lote,
          string unidad,
          int tipoAvaluo,
          Decimal idValuador,
          string fun,
          string digito);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        int ExisteAvaluoAsociado(string región, string manzana, string lote, string unidadprivativa);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsultaFis ObtenerDatosAvaluoCatastral(Decimal idDocumentoDigital);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        bool ValidarTamanioFichero(int bytesXmlAvaluo);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable EsValidoAvaluo(
          byte[] xmlAvaluoByte,
          int idPersona,
          bool esPerito);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        DseAvaluoConsulta.ERROR_VALIDACION_AVALUODataTable ValidarXmlValorUnitarioSuelo(
          string region,
          string manzana,
          string lote,
          string unidad,
          string areaValor,
          Decimal valorUnitario);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DseNotarios BusquedaNotarios(
          Decimal? numero,
          string nombre,
          string apellidoPaterno,
          string apellidoMaterno,
          string rfc,
          string curp,
          string claveife,
          Decimal pageSize,
          Decimal indice,
          ref int rowsTotal,
          string SortExpression);

        [OperationContract]
        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        bool EsXml(byte[] xmlByte);

        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        string ObtenerNombreDelegacion(string codDelegacion);

        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        string ObtenerDigitoVerificadorBD(
          string region,
          string manzana,
          string lote,
          string unidadPrivativa);

        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        [FaultContract(typeof(AvaluosException))]
        DseNotarios.FEXAVA_NOTARIOSDataTable GetNotarios();

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        bool EsFolioValido(string folio, string cuenta, ref string mensaje);

        [FaultContract(typeof(AvaluosException))]
        [FaultContract(typeof(AvaluosInfoException))]
        [OperationContract]
        DataSet ObtenerAvaluosCedula(
          DateTime? fechaInicio,
          DateTime? fechaFin,
          string idAvaluo,
          string numeroAvaluo,
          string registroPerito,
          string cuentaCatastral,
          int pageSize,
          int indice,
          string sortExpression,
          ref int rowsTotal,
          ref string errorMessage);

        [FaultContract(typeof(AvaluosException))]
        [OperationContract]
        [FaultContract(typeof(AvaluosInfoException))]
        bool GuardarCedula(Cedula cedula, Decimal idAvaluo, string estado, Decimal idUsuario);
    }
}
