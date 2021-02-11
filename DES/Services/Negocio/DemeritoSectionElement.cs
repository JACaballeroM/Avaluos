// Decompiled with JetBrains decompiler
// Type: SIGAPred.FuentesExternas.Avaluos.Services.Negocio.DemeritoSectionElement
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using System;
using System.Configuration;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio
{
    public class DemeritoSectionElement : ConfigurationElement
    {
        [ConfigurationProperty("fechaInicio")]
        public DateTime FechaInicio
        {
            get => (DateTime)this["fechaInicio"];
            set => this["fechaInicio"] = (object)value;
        }

        [ConfigurationProperty("fechaFin")]
        public DateTime FechaFin
        {
            get => (DateTime)this["fechaFin"];
            set => this["fechaFin"] = (object)value;
        }

        [ConfigurationProperty("valor")]
        public double Valor
        {
            get => (double)this["valor"];
            set => this["valor"] = (object)value;
        }
    }
}
