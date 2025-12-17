using AspLessons.Abstractions;
using AspLessons.Helpers;
using AspLessons.Models;
using AspLessons.Repositories;
using AutoMapper;

namespace AspLessons.Services
{
    public class MasterService(IMasterRepository masterRepository, IWorkHoursService workHoursService,
        IFavorService favorService, IMapper mapper, ILogger<MasterService> logger) : IMasterService
    {
        public async Task<Master> AddMaster(string name)
        {
            Master master = new Master( ) { Name = name };
            if(await masterRepository.GetMasterByName(name) != null)
            {
                throw new Exception("Мастер с таким именем уже есть");
            }
            int result = await masterRepository.Add(master);
            await masterRepository.SaveChangesAsync( );
            logger.LogInformation("Master {master} was created", master.Name);
            return await Task.FromResult(master);
        }

        public async Task RemoveMaster(Master master)
        {
            await masterRepository.Remove(master);
            await masterRepository.SaveChangesAsync();
            logger.LogInformation("Master {master} was deleted", master.Name);
        }

        public async Task AddFavorToMaster(int favorId, int masterId)
        {
            Master? master = await masterRepository.GetById(masterId);
            master.ThrowIfNull("мастер не найден");
            Favor? favor = await favorService.FindFavorById(favorId);

            if(master!.Favors?.FirstOrDefault(x => x.Id == favorId)!=null)
            {
                throw new Exception("У мастера уже есть эта услуга");
            }

            master.Favors?.Add(favor);
            await masterRepository.SaveChangesAsync( );
            logger.LogInformation("Favor {favor} was added to master {master}", favor.FavorName, master.Name);
        }

        public async Task AddWorkHoursToMaster(WorkHoursDto workHoursDto, int masterId)
        {
            Master? master = await masterRepository.GetById(masterId);
            EntityChecker.Check(master);

            WorkHours workHours = await workHoursService.AddWorkHours(workHoursDto);

            if(master!.WorkHours.FirstOrDefault(x => x.Id == workHours.Id) != null)
            {
                throw new Exception("У мастера уже есть это рабочее время");
            }

            if (master.WorkHours.FindAll(x => x.Date == workHours.Date)
                .Where(x => x.End < workHours.Begin || x.Begin > workHours.End) != null)
            {
                throw new Exception("У мастера есть пересечения в рабочем времени");
            }
            
            master.WorkHours.Add(workHours);
            await masterRepository.SaveChangesAsync( );
            logger.LogInformation
                ("Work hours for {workhoursDate} from {workhoursBegin} to {workhoursEnd} was added to master {master}",
                workHours.Date, workHours.Begin, workHours.End, master.Name);
        }

        public async Task<Master> FindMasterById(int masterId)
        {
            Master? master = await masterRepository.GetById(masterId);
            return master;
        }

        public async Task RemoveFavorFromMaster(int favorId, int masterId)
        {
            Master? master =await masterRepository.GetById(masterId);
            EntityChecker.Check(master);

            Favor? favor = master!.Favors.FirstOrDefault(x => x.Id == favorId);
            EntityChecker.Check(favor);

            master.Favors.Remove(favor!);
            await masterRepository.SaveChangesAsync();
            logger.LogInformation("Favor {favor} was deleted from master {master}", favor!.FavorName, master.Name);
        }

        public async Task RemoveWorkHoursFromMaster(int workHoursId, int masterId)
        {
            Master? master = await masterRepository.GetById(masterId);
            EntityChecker.Check(master);

            WorkHours? workHours = master!.WorkHours.FirstOrDefault(x => x.Id == workHoursId);
            EntityChecker.Check(workHours);

            master.WorkHours.Remove(workHours!);
            await masterRepository.SaveChangesAsync( );
            logger.LogInformation
                ("Work hours for {workhoursDate} from {workhoursBegin} to {workhoursEnd} was deleted from master {master}",
                workHours!.Date, workHours.Begin, workHours.End, master.Name);
        }

        public async Task<List<MasterDto>> GetAllMasters()
        {
            List<Master> masters = await masterRepository.GetAll( );
            List<MasterDto> mastersDto = masters.Select(master => mapper.Map<MasterDto>(master)).ToList();
            return mastersDto;
        }

        public async Task<List<Favor>> GetAllFavorsByMaster(int masterId)
        {
            Master master = await masterRepository.GetById(masterId);
            return master.Favors.ToList();
        }
        public async Task<List<Favor>> GetNotAddedFavorsByMaster(int masterId)
        {
            Master master = await masterRepository.GetById(masterId);
            return (await favorService.GetAllFavors()).Except(master.Favors.ToList( )).ToList();
        }
    }
}
