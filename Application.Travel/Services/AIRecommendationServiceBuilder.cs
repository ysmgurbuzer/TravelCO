using Domain.Travel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Interfaces
{
    public interface AIRecommendationServiceBuilder
    {
        void AddRecommendation(AIRecommendation recommendation);
    }
}
