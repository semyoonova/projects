using BeautySalon;
using BeautySalon.Abstractions;
using BeautySalon.Services;
using AutoMapper;
using Moq;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace BeautyTests
{
    public class FavorServiceTests
    {
        private Mock<IFavorRepository> _mockFavorRepository;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<FavorService>> _mockLogger;

        public FavorServiceTests()
        {
            _mockFavorRepository = new Mock<IFavorRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<FavorService>>();
        }
        [Fact]
        public async Task SendNegativePriceShouldBeNoneNegative()
        {
            int favorId = 1;
            int newPrice = -1000;

            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object, _mockLogger.Object );

            await Assert.ThrowsAsync<Exception>(async () => await favorService.ChangeFavorPrice(favorId, newPrice));
        }

        [Fact]
        public async Task SendNotExistedFavorShouldThrowExecption()
        {
            int favorId = 0;
            int newPrice = 1000;

            _mockFavorRepository.Setup(repo => repo.GetById(favorId))
                .ReturnsAsync((Favor)null);

            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object, _mockLogger.Object);
            await Assert.ThrowsAsync<Exception>(async () => await favorService.ChangeFavorPrice(favorId, newPrice));
        }

        [Fact]
        public async Task SendCorrectNewPriceWithExistedFavorShouldUpdatePrice()
        {

            int favorId = 1;
            int newPrice = 2000;

            Favor favor = new Favor( )
            {
                Id = favorId,
                Price = newPrice - 100
            };

            _mockFavorRepository.Setup(repo => repo.GetById(favorId))
               .ReturnsAsync(favor);
            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object, _mockLogger.Object);
            Favor favorResult = await favorService.ChangeFavorPrice(favorId, newPrice);
            Assert.Equal(newPrice, favorResult.Price);
            Assert.Equal(favorId, favorResult.Id);
        }

        [Fact]
        public async Task SendNotExistedFavorIdShouldThrowExecption()
        {
            int favorId = 0;

            _mockFavorRepository.Setup(repo => repo.GetById(favorId))
                .ReturnsAsync((Favor)null);
            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object, _mockLogger.Object);
            await Assert.ThrowsAsync<Exception>(async () => await favorService.FindFavorById(favorId));
        }

        [Fact]
        public async Task SendNotExistedFavorIdToRemoveShouldThrowExecption()
        {
            int favorId = 0;

            _mockFavorRepository.Setup(repo => repo.GetById(favorId))
                .ReturnsAsync((Favor)null);
            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object, _mockLogger.Object);
            await Assert.ThrowsAsync<Exception>(async () => await favorService.RemoveFavor(favorId));
        }


    }
}
