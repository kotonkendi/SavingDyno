using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using WordPlay.Enumerables;

namespace WordPlay.Models
{
    public class TopScoreModel
    {
        public GameMode GameMode { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
    }
}
