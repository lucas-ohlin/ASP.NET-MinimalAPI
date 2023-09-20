using AutoMapper;
using Labb1_MinimalAPI.Models;
using Labb1_MinimalAPI.Models.DTOs;
//using Labb1_MinimalAPI.Models.DTOs;
//using static Azure.Core.HttpHeader;

namespace Labb1_MinimalAPI {
    public class MappingConfig : Profile {

        public MappingConfig() {
            CreateMap<Book, Book>().ReverseMap();
            CreateMap<Book, BookCreateDTO>().ReverseMap();
            CreateMap<Book, BookUpdateDTO>().ReverseMap();
        }

    }
}
