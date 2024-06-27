using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;

namespace Application.Features.ProgrammingLanguages.Rules
{
    public class ProgrammingLanguageBusinessRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Programming language name exists.");
        }

        public async Task ProgrammingLanguageShouldExistsWhenRequested(int id)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(b => b.Id == id);
            if (programmingLanguage == null) throw new BusinessException("Requested programming language does not exists.");
        }

        public async Task ProgrammingLanguageShouldExistsWhenUpdated(int id)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(b => b.Id == id);
            if (programmingLanguage == null) throw new BusinessException("Updated programming language does not exists.");
        }


        public async Task ProgrammingLanguageShouldExistsWhenDeleted(int id)
        {
            ProgrammingLanguage? programmingLanguage = await _programmingLanguageRepository.GetAsync(b => b.Id == id);
            if (programmingLanguage == null) throw new BusinessException("Deleted programming language does not exists.");
        }
    }
}
