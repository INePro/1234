using System.ComponentModel;

namespace SF2022User_NN_Lib
{
    public class Calculations
    {
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations,
            TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            if (consultationTime<0)
            {
                throw new ArgumentException("You enter exception argument");
            }
            foreach(var d in durations)
            {
                if (d<=0)
                {
                    throw new ArgumentException("You enter data equals or less 0");
                }
            }
            TimeSpan zero = new TimeSpan(0,0,0);
            if(beginWorkingTime<zero)
            {
                throw new ArgumentException("You enter data less than 00:00");
            }
            if(beginWorkingTime>=endWorkingTime)
            {
                throw new ArgumentException("beginWorkingTime equals or more than endWorkingTime");
            }
            TimeSpan biggest = new TimeSpan(24,0,0);
            if (endWorkingTime > biggest)
            {
                throw new ArgumentException("You enter data less than 24:00");
            }
            foreach (var item in startTimes)
            {
                if (item>biggest||item<zero)
                {
                    throw new ArgumentException("Data in startTimes have exception");
                }
            }




            int duration = 0;
            foreach (int i in durations)
                duration += i;

            TimeSpan consultationTimeSpan = new TimeSpan(0, consultationTime, 0);
            int pusto = 0;
            for (int i = 1; i < startTimes.Length; i++)
            {
                TimeSpan durationTimeSpan = new TimeSpan(0, durations[i], 0);
                if (startTimes[i] - (startTimes[i - 1] + durationTimeSpan) < consultationTimeSpan)
                    pusto++;
            }

            int nomer = 0;
            int dlina = (endWorkingTime.Hours * 60 + endWorkingTime.Minutes - beginWorkingTime.Hours * 60 +
                beginWorkingTime.Minutes - duration) / consultationTime - pusto;
            //string[] str = new string[dlina];
            List<string> list = new List<string>();

            TimeSpan now = beginWorkingTime;
            TimeSpan now_1 = new TimeSpan(now.Hours, now.Minutes + consultationTime, now.Seconds);

            for (int i = 0; (now+ consultationTimeSpan) <= endWorkingTime; i++)
            {
                if (nomer <= startTimes.Length - 1 && now > (startTimes[nomer] - consultationTimeSpan))
                {
                    now_1 = new TimeSpan(startTimes[nomer].Hours, startTimes[nomer].Minutes + durations[nomer], startTimes[nomer].Seconds);
                    nomer += 1;
                }
                else
                {
                    list.Add(string.Format("{0:00}:{1:00}", now.Hours, now.Minutes) + " - " + string.Format("{0:00}:{1:00}", now_1.Hours, now_1.Minutes));
                }
                now = now_1;
                now_1 = new TimeSpan(now.Hours, now.Minutes + consultationTime, now.Seconds);
            }


            return list.ToArray();
        }
    }
}