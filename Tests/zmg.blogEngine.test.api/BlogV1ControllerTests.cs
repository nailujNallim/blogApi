using Moq;
using NUnit.Framework;
using zmg.blogEngine.api.Controllers;
using zmg.blogEngine.services;

namespace zmg.blogEngine.test.api
{
    public class BlogV1ControllerTests
    {
        private BlogV1Controller BlogV1Controller;
        Mock<IBlogService> BlogServiceMock;

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}