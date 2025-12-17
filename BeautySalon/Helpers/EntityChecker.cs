using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AspLessons.Helpers
{
    public static class EntityChecker 
    {
        public static void ThrowIfNull<T>(this T? entity, string message)
        {
            if(entity == null)
            {
                throw new NullException(message);
            }

        }

        public static void Check(Master? master)
        {
            if(master == null)
            {
                throw new Exception("Мастер не найден");
            }
        }

        public static void Check(Favor? favor)
        {
            if(favor == null)
            {
                throw new Exception("У мастера нет такой услуги или такой услуги нет вообще");
            }
        }
        public static void Check(WorkHours? workHours)
        {
            if(workHours == null)
            {
                throw new Exception("У мастера нет такого рабочего времени");
            }
        }
        public static void Check(List<TimeOnly> times, TimeOnly time)
        {
            if(times.Where(x => x == time) == null)
            {
                throw new Exception("У мастера нет такого слота");
            }
        }


    }
}
