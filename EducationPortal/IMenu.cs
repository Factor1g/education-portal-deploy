using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    interface IMenu
    {
        Task Start();
        void Logout();
        Task CreateCourse();
        Task UpdateCourse();
        void DeleteCourse();
        Task ViewCourses();
        void CreateMaterial();
        Task UpdateMaterial();
        void DeleteMaterial();
        Task ViewMaterial();
        Task EnrollInCourse();
    }
}

