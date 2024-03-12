﻿using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.SurveyCommands
{
    public class CreateSurveyCommand : IRequest<Response<Survey>>
    {
        public int UserId { get; set; }
        public List<string> PreferredCategories { get; set; }

        public bool LikesArtPlaces { get; set; }
        public bool LikesSports { get; set; }
        public bool LikesCultureAndHistory { get; set; }
        public bool LikesNaturePlaces { get; set; }
        public bool LikesFarEastCuisine { get; set; }
        public bool LikesFastFood { get; set; }
        public bool LikesTraditionalCuisine { get; set; }
        public bool LikesNightlife { get; set; }
        public bool LikesShopping { get; set; }
    }
}
