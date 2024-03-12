using Application.Travel.Features.CQRS.Queries.UserQueries;
using Application.Travel.Features.CQRS.Results.UserResults;
using Application.Travel.Interfaces;
using AutoMapper;
using Domain.Travel.Entities;
using Domain.Travel.Enums;
using Infrastructure.Travel.CustomErrorHandler;
using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel.Features.CQRS.Handlers.UserHandlers
{
    public class GetCheckUserQueryHandler:IRequestHandler<GetCheckUserQuery,Response<GetCheckUserResult>>
    {
        private readonly IRepository<User> _UserRepository;
        private readonly IMapper _mapper;

      

        public GetCheckUserQueryHandler(IRepository<User> userRepository ,IMapper mapper)
        {
            _UserRepository = userRepository;

            _mapper = mapper;
        }

        public async Task<Response<GetCheckUserResult>> Handle(GetCheckUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var values = new GetCheckUserResult();
                var user =  _UserRepository.GetList(x => x.UserName == request.Username);

                if (user != null && BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    values.IsExist = true;
                    var roleType = user.RoleId;
                   
                    values = _mapper.Map(user, values);

                    return Response<GetCheckUserResult>.Success(values);
                }
                else
                {
                    return Response<GetCheckUserResult>.Fail("User not found or invalid password");
                }
            }
            catch (Exception ex)
            {
                return Response<GetCheckUserResult>.Fail($"Error: {ex.Message}");
            }
        }

    }
}
