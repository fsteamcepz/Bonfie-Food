using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonfieFood
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public static string ProfilePhotoPath { get; set; }
    }
}