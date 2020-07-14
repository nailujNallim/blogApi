using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using zmg.blogEngine.model;
using zmg.blogEngine.model.Domain;
using zmg.blogEngine.repository;
using Tynamix.ObjectFiller;
using System.Collections.Generic;
using System.Linq;
using zmg.blogEngine.model.Enumerations;

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

            RepositoryBaseMock.Setup(x => x.ToList<User>())
                .Returns(UsersListMock().AsQueryable());

            //Execute            
            var result = await UsersRepository.GetUser(username, UserType.Writer);

            //Assert
            Assert.AreEqual(username, result);
        }

        private List<User> UsersListMock()
        {
            var userlist = new List<User>();

            for (int i = 0; i < 5; i++)
            {
                Filler<User> filler = new Filler<User>();
                filler.Setup();
                userlist.Add(filler.Create());
            }
            return userlist;
        }
    }
}
