using AutoMapper;
using System.Net;

namespace cepdiWebAPI.Services.Utilerias
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Models.Medicamento, Models.ViewModels.Medicamento>();
            CreateMap<Models.ViewModels.Medicamento, Models.Medicamento>();
        }
    }
}
