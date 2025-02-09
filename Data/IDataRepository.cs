using Model;

namespace Data
{
    public interface IDataRepository
    {
        void AddUser(User user);
        User GetUser(string username);
        void UpdateUser(User user);
        void DeleteUser(string username);
        List<User> LoadUsers();
        void SaveUsers(List<User> users);

        void AddMaterial(Material material);
        Material GetMaterial(string title);
        void UpdateMaterial(Material material);
        void DeleteMaterial(string title);
        List<Material> LoadMaterials();
        void SaveMaterials(List<Material> materials);

        void AddCourse(Course course);
        Course GetCourse(string name);
        void UpdateCourse(Course course);
        void DeleteCourse(string name);
        List<Course> LoadCourses();
        void SaveCourses(List<Course> courses);

        void AddSkill(Skill skill);
        Skill GetSkill(string name);
        void UpdateSkill(Skill skill);
        void DeleteSkill(string name);

        List<User> GetAllUsers();
        List<Course> GetAllCourses();
        List<Material> GetAllMaterials();
        List<Skill> GetAllSkills();
    }
}
