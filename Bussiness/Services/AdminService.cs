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
    public class AdminService : IAdminService
    {
        readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public int Create(Admin entity)
        {
            return _adminRepository.Create(entity);
        }
        public int Delete(int id)
        {
            return _adminRepository.Delete(id);
        }

        public Admin Get(int id)
        {
            return _adminRepository.Get(id);
        }

        public List<Admin> Get(List<int> ids)
        {
            return _adminRepository.Get(ids);
        }

        public int Update(Admin newEntity)
        {
            return _adminRepository.Update(newEntity);
        }
    }
}
