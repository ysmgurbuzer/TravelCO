using Application.Travel.Interfaces;
using Domain.Travel.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Travel.Context
{
    public class MongoContext
    {

        private readonly IMongoDatabase _database;

        public MongoContext()
        {
            var client = new MongoClient("mongodb+srv://ysmgurbuzer:LoixGtOF4rH7Pdtc@clustertravelco.wtistws.mongodb.net/");
            _database = client.GetDatabase("AIdb");
        }

        public IMongoCollection<AIRecommendation> AIRecommendations => _database.GetCollection<AIRecommendation>("AIRecommendations");
    }

   
    public class AIRecommendationService: AIRecommendationServiceBuilder
    {
        private readonly IMongoCollection<AIRecommendation> _recommendationsCollection;

        public AIRecommendationService(MongoContext dbContext)
        {
            _recommendationsCollection = dbContext.AIRecommendations;
        }

        public void AddRecommendation(AIRecommendation recommendation)
        {
            _recommendationsCollection.InsertOne(recommendation);
        }
    }
}
