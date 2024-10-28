using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.DTOs
{
    public class GitHubRepositoryDto
    {
        public string Name { get; set; }
        public OwnerDto Owner { get; set; }
        public string Description { get; set; }
    }
}
