using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entity
{
    public class ProductFile : IEntity
    {
        public int ProductFileId { get; set; }
        public string? ProductImagePath { get; set; }
        public string FileExtension { get; set; }
        public string? FileName { get; set; }
        public int ProductId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
