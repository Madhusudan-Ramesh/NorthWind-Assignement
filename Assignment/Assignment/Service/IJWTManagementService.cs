using Assignment.Model;

namespace Assignment.Service
{
    public interface IJWTManagementService
    {
        Tokens Authenticate(Users user);

        Users GetUsers(string userName);
    }
}
