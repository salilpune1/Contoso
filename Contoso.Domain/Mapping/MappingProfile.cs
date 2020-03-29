using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

using Contoso.Entity;

namespace Contoso.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<ContactViewModel, Contact>();
            CreateMap<Contact, ContactViewModel>();          
        }

    }





}
