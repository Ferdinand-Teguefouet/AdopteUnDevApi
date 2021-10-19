using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interface
{
    public interface ILogin
    {
        UserConnected GetExistedUser(UserLogin _uLogin);
        IEnumerable<UserConnected> GetAll();
        UserConnected GetById(int _id);
    }
}
