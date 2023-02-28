

using Microsoft.EntityFrameworkCore;
using Project_HU.Models;

namespace Project_HU.Services;

public class UserService : IUserService
{

    public TaskContext _context;

    public UserService(TaskContext context)
    {
        _context = context;
    }

    public ResponseModel AssignRole(UserWithRolesDTO userWithRolesDTO)
    {
        ResponseModel model = new ResponseModel();
        var user = _context.Users.Where(a => a.user_id == userWithRolesDTO.user_id).Include(c => c.UserRoles).FirstOrDefault();

        var role = _context.UserRoles.Find(userWithRolesDTO.role_id);

        try {
            user.UserRoles.Add(role);
            _context.SaveChanges();
            model.Messsage = "Role Assigned";
            model.IsSuccess = true;
        } catch (Exception e) {
            model.IsSuccess = false;
            model.Messsage = "Failed to assign Role due to " + e.Message;
        }

        return model;
    }

    public List<Project> GetProjectByUserId(int id)
    {
        List<Project> proj;
        try {
            // proj = _context.Projects.Where(a => a.Creator.All(c => c.user_id == id)).ToList();
            // proj = _context.Projects.Where(a => a.creator_id == id).ToList();
            proj = _context.Projects.Where(a => a.Creator.user_id == id).Include(s => s.Creator).ToList();
        } catch (Exception) {
            throw;
        }
        return proj;
    }

    public User GetUserById(int id)
    {

        User user;
        try {
            user = _context.Users.Where(a => a.user_id == id).Include(s => s.UserRoles).FirstOrDefault();
        } catch (Exception) {
            throw;
        }
        return user;
    }

    public List<User> GetUsers()
    {

        List < User > userList;
        try {
            userList = _context.Users.Include(s=>s.UserRoles).ToList();
        } catch (Exception) {
            throw;
        }
        return userList;
    }

    public ResponseModel SaveEmployee(UserRequest usermodel)
    {

        ResponseModel model = new ResponseModel();

        try {
            User user = new User(){
            username = usermodel.username,
            password = usermodel.password,
            email = usermodel.email
        };

        _context.Add<User>(user);
        model.Messsage = "User inserted Successfully";
        _context.SaveChanges();
        model.IsSuccess = true;
        } catch(Exception e){
            model.IsSuccess = false;
            model.Messsage = "Failed to insert" + e.Message;
        }

        return model;
    }
}