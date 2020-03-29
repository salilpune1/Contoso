using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using Contoso.Entity;
using Contoso.Entity.UnitofWork;

namespace Contoso.Domain.Service
{

    public class ContactService<Tv, Te> : GenericService<Tv, Te>
                                                where Tv : ContactViewModel
                                                where Te : Contact
    {
        //DI must be implemented in specific service as well beside GenericService constructor
        public ContactService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
            if (_mapper == null)
                _mapper = mapper;
        }
       
       
    }

}
