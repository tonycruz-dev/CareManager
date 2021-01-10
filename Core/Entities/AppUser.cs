using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
        public string Avatar { get; set; }
        public string NickName { get; set; }
        public Agency Agency { get; set; }
        public Candidate Candidates { get; set; }
    }
}
