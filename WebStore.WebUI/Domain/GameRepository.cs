using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.Domain.Entities;

namespace WebStore.WebUI.Domain
{
    public class GameRepository
    {
        public List<Game> games { get; set;}
        public void RepositoryInit()
        {

            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

            sqlDataSource.SelectCommand = "SELECT * FROM dbo.Games";
            DataView view = (DataView)sqlDataSource.Select(new DataSourceSelectArguments());
            DataTable groupsTable = view.ToTable();

            List<Game> gamesList = new List<Game>();

            foreach (DataRow dataRow in groupsTable.Rows)
            {
                Game game = new Game();
                game.GameID = (int)dataRow[0];
                game.Name = dataRow[1].ToString();
                game.Description = dataRow[2].ToString();
                game.Category = dataRow[3].ToString();
                game.Price = (decimal)dataRow[4];
                game.Quanity = (int)dataRow[5];
                gamesList.Add(game);
            }

            games = gamesList;
        }
    }
}