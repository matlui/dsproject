using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dsproject.Shared
{
    public class InputModel
    {
        public string Sex { get; set; }
        public int Age { get; set; }
        public int Sibsp { get; set; }
        public int Parch { get; set; }
        public float Fare { get; set; }
        public string PassengerClass { get; set; }
        public string Embark { get; set; }
    }
}
