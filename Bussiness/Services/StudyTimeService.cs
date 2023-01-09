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
    public class StudyTimeService:IStudyTimeService
    {

        readonly IStudyTimeRepository _studyTimeRepository;
        readonly INotJoinStudyTimeRepository _notJoinStudyTimeRepository;
        public StudyTimeService(IStudyTimeRepository studyTimeRepository,
            INotJoinStudyTimeRepository notJoinStudyTimeRepository)
        {
            _studyTimeRepository = studyTimeRepository;
            _notJoinStudyTimeRepository = notJoinStudyTimeRepository;
        }

        public int ChangeStatus(int studyTimeID)
        {
           return _studyTimeRepository.ChangeStatus(studyTimeID);
        }

        public int Create(StudyTime entity)
        {
            return _studyTimeRepository.Create(entity);
        }

        public int Delete(int id)
        {
            return _studyTimeRepository.Delete(id);
        }

        public StudyTime Get(int id)
        {
            return _studyTimeRepository.Get(id);
        }

        public List<StudyTime> Get(List<int> ids)
        {
            return _studyTimeRepository.Get(ids);
        }

        public int Update(StudyTime newEntity)
        {
            return _studyTimeRepository.Update(newEntity);
        }

        public List<StudyTime> GetAll()
        {
            return _studyTimeRepository.GetAll();
        }

        public List<NotJoinStudyTime> GetNotJoin(int studyTimeId)
        {
            return _notJoinStudyTimeRepository.GetByStudyTime(studyTimeId);
        }
    }
}
