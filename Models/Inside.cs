using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_VaderData.Models
{
    internal class Inside
    {
        public Inside(double avgTemp) 
        {   
            AvgTemp = avgTemp;
        }


        public double AvgTemp { get; set; }
    }
}
