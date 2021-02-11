// Decompiled with JetBrains decompiler
// Type: SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Interfaces.Cedula
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using System.Runtime.Serialization;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio.Interfaces
{
    [DataContract]
    public class Cedula
    {
        [DataMember]
        public string folio { get; set; }

        [DataMember]
        public string fechaIngreso { get; set; }

        [DataMember]
        public string cuenta { get; set; }

        [DataMember]
        public string registroSociedad { get; set; }

        [DataMember]
        public string registroPerito { get; set; }

        [DataMember]
        public string nombrePerito { get; set; }

        [DataMember]
        public string superficieTerreno { get; set; }

        [DataMember]
        public string superficieTerrenoCas { get; set; }

        [DataMember]
        public string numeroEscritura { get; set; }

        [DataMember]
        public string superficieConstruccion { get; set; }

        [DataMember]
        public string superficieConstruccionCas { get; set; }

        [DataMember]
        public string clasificacion { get; set; }

        [DataMember]
        public string clasificacionCas { get; set; }

        [DataMember]
        public string edad { get; set; }

        [DataMember]
        public string edadCas { get; set; }

        [DataMember]
        public string NombreUsuario { get; set; }

        [DataMember]
        public string valorCatastral { get; set; }

        [DataMember]
        public string valorCatastralCas { get; set; }

        [DataMember]
        public bool tieneRequerimiento { get; set; }

        [DataMember]
        public bool necesarioPresentar { get; set; }

        [DataMember]
        public bool sinSoporteTerreno { get; set; }

        [DataMember]
        public bool sinSoporteConstruccion { get; set; }

        [DataMember]
        public bool planoaAcotados { get; set; }

        [DataMember]
        public bool clasificacionIncorrecta { get; set; }

        [DataMember]
        public bool noConsideraDescubiertos { get; set; }

        [DataMember]
        public bool noConsideraCubiertos { get; set; }

        [DataMember]
        public bool sinDemerito { get; set; }

        [DataMember]
        public bool rangoIncorrecto { get; set; }

        [DataMember]
        public bool necesitaRatificacion { get; set; }
    }
}
