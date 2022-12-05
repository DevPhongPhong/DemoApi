using Repository.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DTOs.User
{
    public class DEMOSchedule<TEntity, TId>
    {
        public TEntity User { get; set; }
        public Dictionary<DayOfWeek, List<TaskTime>> Data { get; set; }
        public DEMOSchedule()
        {
            var dict = new Dictionary<DayOfWeek, List<TaskTime>>();
            dict.Add(DayOfWeek.Monday, new List<TaskTime>());
            dict.Add(DayOfWeek.Tuesday, new List<TaskTime>());
            dict.Add(DayOfWeek.Wednesday, new List<TaskTime>());
            dict.Add(DayOfWeek.Thursday, new List<TaskTime>());
            dict.Add(DayOfWeek.Friday, new List<TaskTime>());
            dict.Add(DayOfWeek.Saturday, new List<TaskTime>());
            dict.Add(DayOfWeek.Sunday, new List<TaskTime>());

            Data = dict;
        }
    }
}
