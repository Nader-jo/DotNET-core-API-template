using System;
using System.Threading;
using System.Threading.Tasks;
using ApiTemplate.Application.Commands;
using ApiTemplate.Domain.Models;
using ApiTemplate.Domain.Repository;
using Moq;
using Xunit;

namespace ApiTemplate.Test
{
    public class AddUserCommandShould
    {
        private readonly AddUserCommandHandler _handler;
        private readonly Mock<IUserRepository> _userRepository;

        public AddUserCommandShould()
        {
            _userRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            _handler = new AddUserCommandHandler(_userRepository.Object);
        }
        
        [Fact]
        public async void AddUserWhenEmailIsValid()
        {
            var command = new AddUserCommand("user0", "example0@example.com", "user");

            _userRepository.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync((User) null)
                .Verifiable();
            
            _userRepository.Setup(repo => repo.Add(It.IsAny<User>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            
            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.IsType<Guid>(result);
            _userRepository.Verify();
        }
        
        [Fact]
        public async void AddUserWhenEmailIsUsed()
        {
            var user = new User();
            var command = new AddUserCommand("user0", "example0@example.com", "user");

            _userRepository.Setup(repo => repo.GetByEmail(It.IsAny<string>()))
                .ReturnsAsync(user)
                .Verifiable();

            var result = await _handler.Handle(command, CancellationToken.None);
            Exception ex = Assert.Throws<Exception>(() => result);

            Assert.Contains("Email address already used with another user.", ex.Message);
            _userRepository.Verify();
        }
    }
}