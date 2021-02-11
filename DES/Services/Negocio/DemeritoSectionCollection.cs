// Decompiled with JetBrains decompiler
// Type: SIGAPred.FuentesExternas.Avaluos.Services.Negocio.DemeritoSectionCollection
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using System.Configuration;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio
{
    [ConfigurationCollection(typeof(DemeritoSectionElement))]
    public class DemeritoSectionCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement() => (ConfigurationElement)new DemeritoSectionElement();

        protected override object GetElementKey(ConfigurationElement element) => (object)((DemeritoSectionElement)element).FechaInicio;

        public DemeritoSectionElement this[int idx] => (DemeritoSectionElement)this.BaseGet(idx);

        [ConfigurationProperty("source", IsKey = true, IsRequired = true)]
        public string Source
        {
            get => (string)this["source"];
            set => this["source"] = (object)value;
        }
    }
}
