using AutoMapper;

using Moq;

namespace SharedTest
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
    }
}
