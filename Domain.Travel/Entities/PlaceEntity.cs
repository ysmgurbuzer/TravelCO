using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Travel.Entities
{
    public class PlaceEntity
    {
        public int Id { get; set; } // Mekanın benzersiz kimliği (varsayılan olarak veritabanında auto-increment olabilir)
        public double Latitude { get; set; } // Mekanın enlemi
        public double Longitude { get; set; } // Mekanın boylamı
        public double Rate { get; set; } // Mekanın değerlendirme puanı
    }

}
