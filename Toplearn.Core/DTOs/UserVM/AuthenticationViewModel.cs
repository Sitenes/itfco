using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toplearn.DataLayer.Entities.User;

namespace Toplearn.Core.DTOs.UserVM
{
    public class AuthenticationViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsRememberMe { get; set; }
        public List<UserRole> UserRole { get; set; }
        public bool IsActivated { get; set; }

    }
}
