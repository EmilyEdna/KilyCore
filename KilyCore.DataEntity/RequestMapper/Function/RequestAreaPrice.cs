using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Function
{
    public class RequestAreaPrice
    {
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public decimal? ProvincePrice { get; set; }
        public decimal? CityPrice { get; set; }
        public decimal? AreaPrice { get; set; }
        public decimal? TownPrice { get; set; }
        public Guid? ProvinceId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? AreaId { get; set; }
        public Guid? TwonId { get; set; }
    }
}
