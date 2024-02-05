using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceShop.Application.Utilis
{
    public static class FileValidator
    {
        private static string[] videoExtensions = {".mp4"}; 


        public static bool IsVideo(string path)
        {
            string extension = Path.GetExtension(path);
            return videoExtensions.Contains(extension.ToLower());
        }
    }
}
