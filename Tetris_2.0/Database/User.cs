using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris_2.Database
{

    public class User
    {
        //public int Id { get; set; }
        public string usern { get; set; } = null!;
        //public string Account { get; set; } = null!;
        public string Pw { get; set; } = null!;
        public DateTime Timestamp { get; set; }
        public int HighScore { get; set; }
    }


}
