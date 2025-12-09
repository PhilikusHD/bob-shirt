using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bob.Core.Utils
{
    public static class FileUtils
    {
        public static string GetProductFolder(int typeId) => typeId switch
        {
            1 => "shirt",
            2 => "hoodie",
            3 => "cap",
            _ => "unknown"
        };

        public static string GetMotiveFromName(string name)
        {
            // "Shirt - Gus" -> "gus"
            return name.Split('-')[1].Trim().Replace(" ", "").ToLower();
        }
    }
}
