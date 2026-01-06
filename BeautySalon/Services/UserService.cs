using BeautySalon.Abstractions;
using BeautySalon.Helpers;
using BeautySalon.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace BeautySalon.Services
{
    public class UserService(IMasterService masterService, IRegisterService registerService, IFavorService favorService,
        IMapper mapper, IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher<User> passwordHasher, ILogger<UserService> logger) : IUserService
    {
        public async Task<List<DateOnly>> GetWorkingDays(int masterId)
        {
            Master master = await masterService.FindMasterById(masterId);
            EntityChecker.Check(master);
            List<DateOnly> workingDays = master.WorkHours.Select(x => x.Date).ToList();
            return workingDays;
        }

        public async Task<List<TimeOnly>> GetFreeSlots(RegisterDto register)
        {
            Master master = await masterService.FindMasterById(register.MasterId);
            EntityChecker.Check(master);

            Favor? favor = master.Favors.FirstOrDefault(x => x.Id == register.FavorId);
            EntityChecker.Check(favor);

            WorkHours? workHours = master.WorkHours.First(x => x.Date == register.Date);
            EntityChecker.Check(workHours);

            List<FreeTime> freeTimes = GetFreeTimeSpans(workHours,
                master.Registers.Where(x => DateOnly.FromDateTime(x.RegisterDate) == register.Date).ToList());
            List<TimeOnly> slots = GetSlots(freeTimes, favor.Duration);

            return await Task.FromResult(slots);
        }

        public async Task<Register> RegistrationToFavor(RegisterDto registerDto)
        {
            Register register = await registerService.CreateRegister(registerDto);
            
            logger.LogInformation
                ("User with login {userPhone} create register for {registerDate} with master {master} for the {favor}", 
                register.User.PhoneNumber, register.RegisterDate, register.Master.Name, register.Favor.FavorName);
            return register;
        }
        private List<FreeTime> GetFreeTimeSpans (WorkHours workHours, List<Register> registers)
        {
            List<FreeTime> freeTimes = new List<FreeTime>();
            if(registers.Count!= 0)
            {
                freeTimes.Add(new FreeTime( )
                {
                    Begin = workHours.Begin,
                    End = TimeOnly.FromDateTime(registers[0].RegisterDate)
                });

                for(int i = 0; i < registers.Count( ) - 1; i++)
                {
                    TimeOnly begin = TimeOnly.FromDateTime(registers[i].RegisterDate).AddMinutes(registers[i].Favor.Duration);
                    TimeOnly end = TimeOnly.FromDateTime(registers[i + 1].RegisterDate);
                    freeTimes.Add(new FreeTime( ) { Begin = begin, End = end });
                }

                freeTimes.Add(new FreeTime( )
                {
                    Begin = TimeOnly.FromDateTime(registers.Last().RegisterDate).AddMinutes(registers.Last().Favor.Duration),
                    End = workHours.End
                });
            }
            else
                freeTimes.Add(new FreeTime( )
                {
                    Begin = workHours.Begin,
                    End = workHours.End
                });
            return freeTimes;
        }

        private List<TimeOnly> GetSlots (List<FreeTime> freeTimes, int duration)
        {
            List<TimeOnly> slots = new List<TimeOnly>();

            foreach(FreeTime freeTime in freeTimes)
            {
                TimeOnly timeStamp = freeTime.Begin;
                TimeOnly endTimeStamp = freeTime.End;
                while(timeStamp < endTimeStamp)
                {
                    TimeOnly timeStampWithDuration = timeStamp.AddMinutes(duration);
                    if(timeStampWithDuration < endTimeStamp)
                    {
                        slots.Add(timeStamp);
                        timeStamp = timeStamp.AddMinutes(10);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return slots;

        }

        public Task<List<MasterDto>> GetAllMasters()
        {

            return masterService.GetAllMasters();
        }

        public Task<List<Favor>> GetAllFavorsByMaster(int masterId)
        {
            return masterService.GetAllFavorsByMaster(masterId);
        }

        public Task<List<Favor>> GetNotAddedFavorsByMaster(int masterId)
        {
            return masterService.GetNotAddedFavorsByMaster(masterId);
        }

        public async Task CreateUser(UserDto userDto)
        {
            userDto.Role = "client";
            User user = mapper.Map<User>(userDto);
            user.Password = passwordHasher.HashPassword(user, user.Password);
            userRepository.Add(user);
            await userRepository.SaveChangesAsync();
            logger.LogWithMaskedPhone(LogLevel.Information, "User with login {phone} created", user.PhoneNumber);
            logger.LogInformation("User with login {userLogin} created", user.PhoneNumber);
        }

        public async Task<AuthResult> LoginUser(string phoneNumber, string password)
        {
            User? user = await userRepository.GetUserByPhone(phoneNumber);

            if (user == null)
            {
                logger.LogInformation("User with phone number {userPhone} want to log in but this phone wasn't registred", phoneNumber);
                return new AuthResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "пользователь не найден",
                    Code = 404
                };
            }
            if (passwordHasher.VerifyHashedPassword(user, user.Password, password) !=
            PasswordVerificationResult.Success)
            {
                logger.LogInformation("User with login {userLogin} input wrong password", user.PhoneNumber);
                return new AuthResult()
                {
                    IsSuccess = false,
                    ErrorMessage = "неверный пароль",
                    Code = 401
                };
            }
            logger.LogInformation("User with login {userLogin} logged in", user.PhoneNumber);
            return new AuthResult( )
            {
                IsSuccess = true,
                Code = 200,
                Token = jwtTokenGenerator.CreateJwtToken(user)!,
                Role = user.Role!
            };
        }

        public async Task<List<Favor>> GetAllFavors()
        {
           return await favorService.GetAllFavors();
        }
    }

}
