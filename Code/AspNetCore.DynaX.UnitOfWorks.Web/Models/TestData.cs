using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.DynaX.UnitOfWorks.Web.Models
{
    public class TestData
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string String { get; set; }
        public decimal Number { get; set; }
        public DateTime DateTime { get; set; }
        public bool Bool { get; set; }
    }
}
