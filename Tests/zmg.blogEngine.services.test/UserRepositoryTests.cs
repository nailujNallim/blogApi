using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.repository;
using Tynamix.ObjectFiller;
using System.Collections.Generic;
using System.Linq;

namespace zmg.blogEngine.services.test
{
    [TestFixture]
    public class UserRepositoryTests
    {
        UserRepository UsersRepository;
        Mock<IRepository> RepositoryBaseMock;

        [SetUp]
        public void Setup()
        {
            RepositoryBaseMock = new Mock<IRepository>();
            UsersRepository = new UserRepository(RepositoryBaseMock.Object);
        }

        [Test]
        public void UserRepository_Exists()
        {
            //Assert
            Assert.IsNotNull(UsersRepository);
            Assert.AreEqual(typeof(UserRepository), UsersRepository.GetType());
        }

        [Test]
        public async Task GetWriterByUsername_Ok()
        {
            //Arrange            
            var username = "writerUsername";

            RepositoryBaseMock.Setup(x => x.ToList<Writer>())
                .Returns(UsersListMock().AsQueryable());

            //Execute            
            Writer result = await UsersRepository.GetWriterByUsername(username);

            //Assert
            Assert.AreEqual(username, result);
        }

        private List<Writer> UsersListMock()
        {
            var userlist = new List<Writer>();

            for (int i = 0; i < 5; i++)
            {
                Filler<Writer> filler = new Filler<Writer>();
                filler.Setup();
                userlist.Add(filler.Create());
            }
            return userlist;
        }
    }
}
