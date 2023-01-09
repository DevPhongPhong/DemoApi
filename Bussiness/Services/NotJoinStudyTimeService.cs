using Bussiness.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Services
{
    public class NotJoinStudyTimeService : INotJoinStudyTimeService
    {
        readonly INotJoinStudyTimeRepository _notJoinStudyTimeRepository;

        public NotJoinStudyTimeService(INotJoinStudyTimeRepository notJoinStudyTimeRepository)
        {
            _notJoinStudyTimeRepository = notJoinStudyTimeRepository;
        }

        public int Create(NotJoinStudyTime entity)
        {
            return _notJoinStudyTimeRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _notJoinStudyTimeRepository.Delete(id);  
        }

        public NotJoinStudyTime Get(int id)
        {
            return _notJoinStudyTimeRepository.Get(id);
        }

        public List<NotJoinStudyTime> Get(List<int> ids)
        {
            return _notJoinStudyTimeRepository.Get(ids);
        }

        public int Update(NotJoinStudyTime newEntity)
        {
            return _notJoinStudyTimeRepository.Update(newEntity);
        }

        public List<NotJoinStudyTime> GetAll()
        {
            return _notJoinStudyTimeRepository.GetAll();
        }
    }
}
