using AspLessons;
using AspLessons.Abstractions;
using AspLessons.Services;
using AutoMapper;
using Moq;

namespace BeautyTests
{
    public class FavorServiceTests
    {
        private Mock<IFavorRepository> _mockFavorRepository;
        private Mock<IMapper> _mockMapper;

        public FavorServiceTests()
        {
            _mockFavorRepository = new Mock<IFavorRepository>();
            _mockMapper = new Mock<IMapper>();
        }
        [Fact]
        public async Task SendNegativePriceShouldBeNoneNegative()
        {
            int favorId = 1;
            int newPrice = -1000;

            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object);

            await Assert.ThrowsAsync<Exception>(async () => await favorService.ChangeFavorPrice(favorId, newPrice));
        }

        [Fact]
        public async Task SendNotExistedFavorShouldThrowExecption()
        {
            int favorId = 0;
            int newPrice = 1000;

            _mockFavorRepository.Setup(repo => repo.GetById(favorId))
                .ReturnsAsync((Favor)null);

            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object);
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
            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object);
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
            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object);
            await Assert.ThrowsAsync<Exception>(async () => await favorService.FindFavorById(favorId));
        }

        [Fact]
        public async Task SendNotExistedFavorIdToRemoveShouldThrowExecption()
        {
            int favorId = 0;

            _mockFavorRepository.Setup(repo => repo.GetById(favorId))
                .ReturnsAsync((Favor)null);
            IFavorService favorService = new FavorService(_mockFavorRepository.Object, _mockMapper.Object);
            await Assert.ThrowsAsync<Exception>(async () => await favorService.RemoveFavor(favorId));
        }


    }
}
