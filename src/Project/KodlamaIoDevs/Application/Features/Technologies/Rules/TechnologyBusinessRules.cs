using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task TechnologyShouldExistWhenDeleted(int id)
        {
            Technology? technology = await _technologyRepository.GetAsync(p => p.Id == id);
            if (technology == null) throw new BusinessException("Deleted technology does not exists");
        }  
        
        public async Task TechnologyShouldExistWhenUpdated(int id)
        {
            Technology? technology = await _technologyRepository.GetAsync(p => p.Id == id);
            if (technology == null) throw new BusinessException("Deleted technology does not exists");
        }
    }
}
