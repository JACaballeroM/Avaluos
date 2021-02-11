// Decompiled with JetBrains decompiler
// Type: SIGAPred.FuentesExternas.Avaluos.Services.Negocio.DemeritoSection
// Assembly: SIGAPred.FuentesExternas.Avaluos.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 15C2054E-E542-4F35-A814-71DFD0FC4314
// Assembly location: C:\Users\EdgarAntunezMartinez\Downloads\Avaluos_BK_2020DIC17\Avaluos_BK_2020DIC17\bin\SIGAPred.FuentesExternas.Avaluos.Services.dll

using System.Configuration;

namespace SIGAPred.FuentesExternas.Avaluos.Services.Negocio
{
    public class DemeritoSection : ConfigurationSection
    {
        [ConfigurationProperty("ItemSection")]
        public DemeritoSectionCollection HashKeys => (DemeritoSectionCollection)this["ItemSection"];
    }
}
