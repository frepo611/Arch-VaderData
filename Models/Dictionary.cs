using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arch_VaderData.Models
{
    internal class Dictionary
    {
        public static Dictionary<DateTime, (Inside?, Outside)> Data { get; } = new Dictionary<DateTime, (Inside?, Outside?)>();
    }
}
