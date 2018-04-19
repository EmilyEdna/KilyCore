using KilyCore.DataEntity.AttributeMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace KilyCore.DataEntity.RequestMapper.Function
{
    public class RequestAreaPrice
    {
        public Guid Id { get; set; }
        [Mapper(Alter = Updates.Require)]
        public string ProjectName { get; set; }
        [Mapper(Alter = Updates.Require)]
        public decimal? ProvincePrice { get; set; }
        [Mapper(Alter = Updates.Require)]
        public decimal? CityPrice { get; set; }
        [Mapper(Alter = Updates.Require)]
        public decimal? AreaPrice { get; set; }
        [Mapper(Alter = Updates.Require)]
        public decimal? TownPrice { get; set; }
        [Mapper(Alter = Updates.Require)]
        public Guid? ProvinceId { get; set; }
        [Mapper(Alter = Updates.Require)]
        public Guid? CityId { get; set; }
        [Mapper(Alter = Updates.Require)]
        public Guid? AreaId { get; set; }
        [Mapper(Alter = Updates.Require)]
        public Guid? TownId { get; set; }
    }
}
