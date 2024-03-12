using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Commands.AIRecommendationCommands
{
    public class ExportToExcelCommand : IRequest<string>
    {
    }
}
