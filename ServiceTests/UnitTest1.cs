using Services.Model;

namespace ServiceTests
{
    public class UnitTest1
    {
        Services.Service service = new Services.Service();
        PlayerModel model = new PlayerModel();
        CardModel card = new CardModel();
       

        [Fact]
        public void InsertPlayerTest()
        {
            model.name = "test";
            model.turns = 2;
            model.id = 1;
            Assert.True(service.APIInsertPlayer(model) == 0);
        }

        [Fact]
        public void GetPlayerTest()
        {
            model.name = "test";
            model.turns = 2;
            model.id = 1;
            Assert.True(service.APIGetPlayerByID(1) != null);
        }

        [Fact]
        public void InsertCardTest()
        {
            card.Suit = "Jack";
            card.cardVal = 2;
            card.id = 1;
            card.turnNum = 2;
            Assert.True(service.APIInsertCard(card) == 0);
        }
        [Fact]
        public void GetCardTest()
        {
            card.Suit = "Jack";
            card.cardVal = 2;
            card.id = 1;
            card.turnNum = 2;
            Assert.True(service.APIGetWinCardByID(1) != null);
        }

    }
}