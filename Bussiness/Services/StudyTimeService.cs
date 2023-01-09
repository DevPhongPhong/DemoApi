using Bussiness.Interfaces;
using Repository.Entities;
using Repository.Interfaces;
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
        public StudyTimeService(IStudyTimeRepository studyTimeRepository)
        {
            _studyTimeRepository = studyTimeRepository;
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
    }
}
