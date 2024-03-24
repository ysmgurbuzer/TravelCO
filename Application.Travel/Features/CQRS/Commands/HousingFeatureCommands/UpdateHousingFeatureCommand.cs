﻿using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.HousingFeatureCommands
{
    public class UpdateHousingFeatureCommand : IRequest<Response<HousingFeatures>>
    {

        public int Id { get; set; }
        public FeatureCommand Command { get; set; }

    }
    public class FeatureCommand
    {
        public int HousingId { get; set; }
        public HomeServiceTypes ServiceId { get; set; }
        public bool Available { get; set; }
    }
}