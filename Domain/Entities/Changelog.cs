using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Change
    {
        public string Date { get; set; }
        public List<Detail> Details { get; set; }
    }

    public class Detail
    {
        public string Type { get; set; }
        public string Description { get; set; }
    }

    public class Changelog
    {
        public List<Change> Changes { get; set; }
    }
}
