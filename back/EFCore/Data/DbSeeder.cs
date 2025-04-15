using EFCore;
using Models.Identity;

public static class DbSeeder
{
    public static void SeedRoles(AppDbContext context)
    {
        if (!context.Choices.Any() && !context.SubChoices.Any())
        {
            // Choix : Artiste, Professionnel
            var choice1Id = Guid.NewGuid();
            var choice2Id = Guid.NewGuid();

            var choices = new List<Choice>
            {
                new Choice { ChoiceId = choice1Id, Name = "Artiste" },
                new Choice { ChoiceId = choice2Id, Name = "Professionnel" }
            };

            context.Choices.AddRange(choices);

            // Sous - Choix :
            // (Musicien, Chanteur, Beatmaker) pour les artistes
            // (Ingénieur du son, Designer de cover, Réalisateur de clip, Distributeur, Promoteur) pour les professionnels
            var subChoice1Id = Guid.NewGuid();
            var subChoice2Id = Guid.NewGuid();
            var subChoice3Id = Guid.NewGuid();
            var subChoice4Id = Guid.NewGuid();
            var subChoice5Id = Guid.NewGuid();
            var subChoice6Id = Guid.NewGuid();
            var subChoice7Id = Guid.NewGuid();
            var subChoice8Id = Guid.NewGuid();

            var subChoices = new List<SubChoice>
            {
                new SubChoice { SubChoiceId = subChoice1Id, Name = "Musicien", ChoiceId = choice1Id },
                new SubChoice { SubChoiceId = subChoice2Id, Name = "Chanteur", ChoiceId = choice1Id },
                new SubChoice { SubChoiceId = subChoice3Id, Name = "Beatmaker", ChoiceId = choice1Id },
                new SubChoice { SubChoiceId = subChoice4Id, Name = "Ingénieur du son", ChoiceId = choice2Id },
                new SubChoice { SubChoiceId = subChoice5Id, Name = "Designer de cover", ChoiceId = choice2Id },
                new SubChoice { SubChoiceId = subChoice6Id, Name = "Réalisateur de clip", ChoiceId = choice2Id },
                new SubChoice { SubChoiceId = subChoice7Id, Name = "Distributeur", ChoiceId = choice2Id },
                new SubChoice { SubChoiceId = subChoice8Id, Name = "Promoteur", ChoiceId = choice2Id }
            };

            context.SubChoices.AddRange(subChoices);

            context.SaveChanges();
        }
    }
}