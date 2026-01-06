using BeautySalon;
using BeautySalon.Abstractions;
using BeautySalon.Models;
using BeautySalon.Repositories;
using BeautySalon.Services;
using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyTests
{
    public class WorkhoursServiceTests
    {
        private Mock<IWorkHoursRepository> _mockWorkHoursRepository;
        private Mock<IMapper> _mapperMock;

        public WorkhoursServiceTests()
        {
            _mockWorkHoursRepository = new Mock<IWorkHoursRepository>( );
            _mapperMock = new Mock<IMapper>();

        }
        //public Task<WorkHours> AddWorkHours(DateOnly date, TimeOnly begin, TimeOnly end)
        //{
        //    if(begin > end)
        //        throw new Exception("Некорректные данные");

        //    WorkHours workHours = new WorkHours( )
        //    {
        //        Date = date,
        //        Begin = begin,
        //        End = end
        //    };
        //    _workHoursRepository.Add(workHours);
        //    return Task.FromResult(workHours);
        //}

        [Fact]
        public async Task SendBeginLessThenEndShouldThrowExeption()
        {
            WorkHoursDto workhoursDto = new WorkHoursDto( )
            {
                Date = new DateOnly( ),
                Begin = new TimeOnly(12, 00),
                End = new TimeOnly(11, 00)
            };
            _mapperMock.Setup(m => m.Map<WorkHours>(It.IsAny<WorkHoursDto>( )));
            IWorkHoursService workhoursService = new WorkHoursService(_mockWorkHoursRepository.Object, _mapperMock.Object);
            await Assert.ThrowsAsync<Exception>(async () => await workhoursService.AddWorkHours(workhoursDto));
        }


       

    }
}
