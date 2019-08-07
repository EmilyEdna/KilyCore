using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.ResponseMapper.Enterprise
{
    public class ResponseEnterpriseEnv
    {
        public string Flag { get; set; }
        public string CheckUrl { get; set; }
        public string AirEnv { get; set; }
        public string AirHdy { get; set; }
        public string SoilEnv { get; set; }
        public string SoilHdy { get; set; }
        public string Light { get; set; }
        public string CO2 { get; set; }
        public DateTime? Now { get; set; }
    }
}
