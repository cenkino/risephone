
using AutoMapper;
using EventBus.Base.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace RisePhoneApp.Shared.Core.Base
{
    public class BaseController<Repository>:ControllerBase where Repository : IBaseRepository
    {
        public Repository _repository;
        public IMapper _mapper;
        

        public BaseController(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }
    }
}
