using AspLessons.Abstractions;
using AspLessons.Helpers;
using AspLessons.Models;
using AspLessons.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;

namespace AspLessons.Services
{
    public class RegisterService(IMasterService masterService, IRegisterRepository registerRepository,
        IMapper mapper) : IRegisterService
    {
        public async Task<Register> CreateRegister(RegisterDto registerDto)
        {
            Master master = await masterService.FindMasterById(registerDto.MasterId);
            EntityChecker.Check(master);

            Favor? favor = master.Favors.FirstOrDefault(x => x.Id == registerDto.FavorId);
            EntityChecker.Check(favor);

            WorkHours? workHours = master.WorkHours.FirstOrDefault(x => x.Date == registerDto.Date);
            EntityChecker.Check(workHours);

            Register register = mapper.Map<Register>(registerDto);
            register.Master = master;
            register.Favor = favor;
            register.Id = await registerRepository.Add(register);

            master.Registers.Add(register);
            await registerRepository.SaveChangesAsync();
            return await Task.FromResult(register);
        }
    }
}
