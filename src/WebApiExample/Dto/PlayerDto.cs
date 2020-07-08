using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiExample.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }

        public PlayerDto(int id, string name, string teamName)
        {
            Id = id;
            Name = name;
            TeamName = teamName;
        }

        public PlayerDto()
        {
            
        }
    }
}
