

using Project_HU.Models;

namespace Project_HU.Services;

public interface IUserService {
    List<User> GetUsers();

    User GetUserById(int id);

    ResponseModel SaveEmployee(UserRequest usermodel);

    ResponseModel AssignRole(UserWithRolesDTO userWithRolesDTO);

    List<Project> GetProjectByUserId(int id);

}