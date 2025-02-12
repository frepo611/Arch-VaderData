using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_VaderData.Models
{
    internal class Outside
    {
        Outside(int avgTemp, int avgHum) 
        {
            AvgTemp = avgTemp;
            AvgHum = avgHum;
                
        }

        public int AvgTemp { get; }
        public int AvgHum { get;}

    }
}
