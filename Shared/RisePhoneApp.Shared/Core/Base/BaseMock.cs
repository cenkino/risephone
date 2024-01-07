using AutoMapper;
using EventBus.Base.Abstraction;
using Moq;


namespace RisePhoneApp.Shared.Core.Base
{
    public class BaseMock<RepositoryT,ControllerT,ProfileT> 
        where RepositoryT : class,IBaseRepository
        where ControllerT :BaseController<RepositoryT>
        where ProfileT : Profile,new()
        
    {
        public Mock<RepositoryT> RepositoryM;
        public ControllerT ControllerM;
        public IMapper MapperM;
      

        public BaseMock()
        {

            RepositoryM = new Mock<RepositoryT>();

            MapperM = new MapperConfiguration(cfg => cfg.AddProfile<ProfileT>()).CreateMapper();

            ControllerM = (ControllerT)Activator.CreateInstance(typeof(ControllerT), RepositoryM.Object , MapperM);
        }
        public BaseMock(IEventBus eventBus)
        {

            RepositoryM = new Mock<RepositoryT>();

            MapperM = new MapperConfiguration(cfg => cfg.AddProfile<ProfileT>()).CreateMapper();

            ControllerM = (ControllerT)Activator.CreateInstance(typeof(ControllerT), RepositoryM.Object, MapperM, eventBus);
        }
    }
}
