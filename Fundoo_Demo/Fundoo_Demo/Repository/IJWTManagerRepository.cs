using CommanLayer.Models;
using CommonLayer.Models;

namespace Fundoo_Demo.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users users);

    }
}