﻿using System;
using System.Web.Mvc;
using log4net;
using PokerBot.Models;

namespace PokerBot.Controllers
{
    public class PokerController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PokerController));

        private static Game game;

        [HttpPost]
        public void Start(string opponent_name, int starting_chip_count, int hand_limit)
        {
            log.Info("");
            log.Info("");
            log.InfoFormat("Start: opponent_name = {0}, starting_chip_count = {1}, hand_limit = {2}",
                opponent_name, starting_chip_count, hand_limit);

            game = new Game(opponent_name, starting_chip_count, hand_limit);
        }

        [HttpPost]
        public void Update(string command, string data)
        {
            log.InfoFormat("Update: command = {0}, data = {1}", command, data);

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
                    break;
                case "GAME_OVER":
                    log.InfoFormat("Game result: {0} against '{1}' chips = {2}", game.Result(), game.Opponent, game.Chips);
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

                log.InfoFormat("Move: {0}", result.Content);

                return result;
            }
            catch (Exception exception)
            {
                log.Error(exception);
                throw;
            }
        }
    }
}