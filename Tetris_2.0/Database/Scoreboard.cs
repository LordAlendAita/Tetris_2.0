using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_2.Database
{

    internal class Scoreboard
    {
        public int Id { get; set; }
        public User user { get; set; } = null!;
        public int userId { get; set; }
        public int score { get; set; }
        public DateTime Timestamp { get; set; }

    }
}
