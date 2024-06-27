using Core.Persistence.Repositories;

namespace Domain.Entities
{
    public class ProgrammingLanguage : Entity
    {
        public string Name { get; set; }
        public ProgrammingLanguage()
        {

        }

        public ProgrammingLanguage(int id, string name) : this()
        {
            Name = name;
            Id = id;
        }
    }
}
