using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace consoleApp.Models
{
    public class GitHubOptions
    {
        public string ApiBaseUrl { get; set; } = string.Empty;
        public List<string>? Usernames { get; set; }     
        public int MaxConcurrentRequests { get; set; }

    }
}