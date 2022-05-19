using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Game game, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.game.GameID == game.GameID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    game = game,
                    quantity = quantity
                });
            }
            else
            {
                line.quantity += quantity;
            }
        }

        public void RemoveLine(Game game)
        {
            lineCollection.RemoveAll(l => l.game.GameID == game.GameID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.game.Price * e.quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Game game { get; set; }
        public int quantity { get; set; }
    }
}