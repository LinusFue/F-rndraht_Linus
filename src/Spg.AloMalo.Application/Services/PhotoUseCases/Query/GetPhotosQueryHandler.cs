using MediatR;
using Spg.AloMalo.Application.Services.PhotoUseCases.Filter;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class GetPhotosQueryHandler : IRequestHandler<GetPhotosQueryModel, List<PhotoDto>>
    {
        private readonly IReadOnlyPhotoRepository _photoRepository;

        public GetPhotosQueryHandler(IReadOnlyPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        public Task<List<PhotoDto>> Handle(GetPhotosQueryModel request, CancellationToken cancellationToken)
        {
            IPhotoFilterBuilder builder =
                _photoRepository
                .FilterBuilder;

            var filters = request.Query.Filter.Split(';');
            foreach (var filter in filters)
            {
                builder = new NameContainsParameter(builder).Compile(filter);
                builder = new NameEqualsParameter(builder).Compile(filter);
                builder = new NameStartsWithParameter(builder).Compile(filter);
                builder = new NameEndsWithParameter(builder).Compile(filter);
                builder = new NameRegexParameter(builder).Compile(filter);
            }
            
            
            
            /*builder = new LastNameContainsParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameBeginsWithParameter(builder)
                .Compile(request.Query.Filter);
            builder = new LastNameEndsWithParameter(builder)
                .Compile(request.Query.Filter);*/

            return Task.FromResult(
                builder
                .Build()
                .Select(p => p.ToDto())
                .ToList()
            );
        }
    }
}
