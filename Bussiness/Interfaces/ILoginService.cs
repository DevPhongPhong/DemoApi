using Bussiness.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface ILoginService<TId>
    {
        bool CheckLogin(Login login,out TId id);
        int ChangePassword(TId id, string oldPass, string newPass);
        int ForgotPassword(string emailOrPhoneNumber);
        string CreateToken(string username, int id);
    }
}
