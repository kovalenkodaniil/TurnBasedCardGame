using _Core.Features.Cards.Scripts;

namespace Core.Data
{
    public class CardDataProvider : IStaticDataProvider
    {
        public CardAssets cardAssets;

        public CardDataProvider(CardAssets cardAssets)
        {
            this.cardAssets = cardAssets;
        }
    }
}