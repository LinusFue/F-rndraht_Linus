﻿using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Filters;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Filter
{
    public class NameEqualsParameter : IQueryParameter
    {
        private readonly IPhotoFilterBuilder _photoFilterBuilder;

        public NameEqualsParameter(IPhotoFilterBuilder photoFilterBuilder)
        {
            _photoFilterBuilder = photoFilterBuilder;
        }

        public IPhotoFilterBuilder Compile(string queryParameter)
        {
            string[] parts = queryParameter.Split(' ');
            if (parts.Length == 3 && parts[0]?.Trim().ToLower() == "name" && parts[1]?.Trim().ToLower() == "equals")
            {
                return _photoFilterBuilder.ApplyFilter(new EqualsFilter<Photo, string>(p => p.Name, parts[2]));
            }

            return _photoFilterBuilder;
        }
    }

}