using System;
using System.Web.Mvc;
using log4net;
using PokerBot.Models;

namespace PokerBot.Controllers
{
    public class PokerController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PokerController));

        private static Game game;

        [HttpPost]
        public void Start(string opponent_name, int starting_chip_count, int hand_limit)
        {
            Log.Info("");
            Log.Info("");
            Log.InfoFormat("Start: opponent_name = {0}, starting_chip_count = {1}, hand_limit = {2}",
                opponent_name, starting_chip_count, hand_limit);

            game = new Game(opponent_name, starting_chip_count, hand_limit);
        }

        [HttpPost]
        public void Update(string command, string data)
        {
            Log.InfoFormat("Update: command = {0}, data = {1}", command, data);

            switch (command)
            {
                case "RECEIVE_BUTTON":
                    game.ReceiveButton();
                    break;
                case "POST_BLIND":
                    game.PostBlind();
                    break;
                case "CARD":
                    game.Card(new Card(data));
                    break;
                case "OPPONENT_MOVE":
                    game.OpponentMove(data);
                    break;
                case "OPPONENT_CARD":
                    game.OpponentCard(data);
                    break;
                case "RECEIVE_CHIPS":
                    game.ReceiveChips(Convert.ToInt32(data));
                    Log.Info("");
                    break;
            }
        }

        [HttpGet]
        public ActionResult Move()
        {
            try
            {
                var result = new ContentResult
                {
                    Content = game.Move()
                };

                Log.InfoFormat("Move: {0}", result.Content);

                return result;
            }
            catch (Exception exception)
            {
                Log.Error(exception);
                throw;
            }
        }
    }
}