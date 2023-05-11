using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Model;
using Services;

namespace WarAPI.Controllers
{
    public class GameController : Controller
    {

        public Service service = new Service();

        [HttpGet]
        [Route("get-by-PlayerId/{id}")]
        public PlayerModel GetPlayerByID(int id)
        {
            return service.APIGetPlayerByID(id);
        }

        [HttpGet]
        [Route("get-by-CardId/{id}")]
        public CardModel GetCardById(int id)
        {
            return service.APIGetWinCardByID(id);
        }

        [HttpPost]
        [Route("insert-player")]
        public int PostPlayer([FromBody] PlayerModel model) 
        { 
            return service.APIInsertPlayer(model);
        }

        [HttpPost]
        [Route("insert-card")]
        public int PostCard([FromBody] CardModel model) 
       { 
        return service.APIInsertCard(model);
        }


    }
}
