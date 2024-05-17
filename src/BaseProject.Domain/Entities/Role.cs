using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Domain.Entities
{
    public class Role
    {
        public byte Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
    }
}