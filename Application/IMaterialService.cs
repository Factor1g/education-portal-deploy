using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public interface IMaterialService
    {
        void CreateMaterial(Material material);
        void UpdateMaterial(Material material);
        void DeleteMaterial(string title);
        Material GetMaterial(string title);
    }
}
