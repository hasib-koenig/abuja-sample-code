    using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Server.DTOs.Product;
    using Server.Models;

    namespace Server.Context
    {
        public class MapperProfile:Profile
        {
            public MapperProfile()
            {
            CreateMap<CreateRequestDTO, Product>();
            }

        }
}
