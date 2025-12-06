using AspLessons.Abstractions;

namespace AspLessons.Services
{
    public class GetRandomDiscountJob 
    {
        private IRegisterRepository _registerRepository;
        public GetRandomDiscountJob(IRegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }
        public async Task SetRandomDicsount()
        {
            var allRegisters =  await  _registerRepository.GetAll();
            var allRegistersByDay = allRegisters.Where(register => register.RegisterDate.Date == DateTime.Now.Date ).ToList();
            Random rand = new Random();
            var registerIndex = rand.Next(allRegistersByDay.Count-1);
            if(registerIndex == -1)
            {
                return;
            }
            var randomRegister = allRegistersByDay[registerIndex];
            randomRegister.Discount = 10;
            await _registerRepository.SaveChangesAsync();
        }

    }

    
}

