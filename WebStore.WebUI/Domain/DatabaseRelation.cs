using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebStore.Domain.Entities;
using WebStore.WebUI.Domain.Entities;

namespace WebStore.WebUI.Domain
{
    public class DatabaseRelation
    {
        public static List<Game> getGames()
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

            return gamesList;
        }

        public static Game getGame(int gameId)
        {
            List<Game> gamesList = getGames();
            Game game = null;
            foreach(Game g in gamesList)
            {
                if(g.GameID == gameId)
                    game = g;
            }
            return game;
        }

        public static List<User> getUsers()
        {
            List<User> users = new List<User>();

            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

            sqlDataSource.SelectCommand = "SELECT * FROM dbo.Users";
            DataView view = (DataView)sqlDataSource.Select(new DataSourceSelectArguments());
            DataTable groupsTable = view.ToTable();

            foreach (DataRow dataRow in groupsTable.Rows)
            {
                User user = new User();

                user.UserId = (int)dataRow[0];
                user.Name = dataRow["Name"].ToString();
                user.Surname = dataRow["Surname"].ToString();
                user.Patronymic = dataRow["Patronymic"].ToString();
                user.Phone = dataRow["Phone"].ToString();
                user.Mail = dataRow["Mail"].ToString();
                user.Admin = (int)dataRow["Admin"];
                user.Login = dataRow["Login"].ToString();
                user.Password = dataRow["Password"].ToString();

                users.Add(user);
            }

            return users;
        }

        public static User getUser(int userId)
        {
            List<User> userList = getUsers();
            User user = null;
            foreach (User u in userList)
            {
                if (u.UserId == userId)
                    user = u;
            }
            return user;
        }

        public static List<Order> getOrders()
        {
            List<Order> orders = new List<Order>();

            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

            sqlDataSource.SelectCommand = "SELECT * FROM dbo.Orders";
            DataView view = (DataView)sqlDataSource.Select(new DataSourceSelectArguments());
            DataTable groupsTable = view.ToTable();

            foreach (DataRow dataRow in groupsTable.Rows)
            {
                Order order = new Order();

                order.Id = (int)dataRow[0];
                order.UserId = (int)dataRow[1];
                order.GameId = (int)dataRow[2];

                orders.Add(order);
            }

            return orders;
        }

        public static void EditGame(Game game)
        {
            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

            sqlDataSource.UpdateParameters.Add("ID", game.GameID.ToString());
            sqlDataSource.UpdateParameters.Add("Name", game.Name.ToString());
            sqlDataSource.UpdateParameters.Add("Description", game.Description.ToString());
            sqlDataSource.UpdateParameters.Add("Category", game.Category.ToString());
            sqlDataSource.UpdateParameters.Add("Price", game.Price.ToString());
            sqlDataSource.UpdateParameters.Add("Quantity", game.Quanity.ToString());
            sqlDataSource.UpdateCommand = "UPDATE dbo.Games " +
                "SET Name = @Name," +
                "Description = @Description," +
                "Category = @Category," +
                "Price = @Price," +
                "Quantity = @Quantity " +
                "WHERE GameId = @ID";
            sqlDataSource.Update();
        }

        public static void AddGame(Game game)
        {
            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

            sqlDataSource.InsertParameters.Add("Name", game.Name.ToString());
            sqlDataSource.InsertParameters.Add("Description", game.Description.ToString());
            sqlDataSource.InsertParameters.Add("Category", game.Category.ToString());
            sqlDataSource.InsertParameters.Add("Price", game.Price.ToString());
            sqlDataSource.InsertParameters.Add("Quantity", game.Quanity.ToString());
            sqlDataSource.InsertCommand = "INSERT INTO dbo.Games (Name, Description,Category,Price,Quantity) " +
                "VALUES(@Name, @Description,@Category,@Price,@Quantity);";
            sqlDataSource.Insert();
        }

        public static void DeleteGame(int gameID)
        {
            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();
            sqlDataSource.DeleteParameters.Add("ID", gameID.ToString());
            sqlDataSource.DeleteCommand = "DELETE FROM dbo.Games WHERE GameId = @ID";
            sqlDataSource.Delete();
        }

        public static void RegisterUser(RegisterForm user)
        {
            foreach (User u in getUsers())
                if (u.Login == user.Login)
                    return;

            SqlDataSource sqlDataSource = new SqlDataSource();
            sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

            sqlDataSource.InsertParameters.Add("Name", user.Name.ToString());
            sqlDataSource.InsertParameters.Add("Surname", user.Surname.ToString());
            sqlDataSource.InsertParameters.Add("Patronymic", user.Patronymic.ToString());
            sqlDataSource.InsertParameters.Add("Phone", user.Phone.ToString());
            sqlDataSource.InsertParameters.Add("Mail", user.Mail.ToString());
            sqlDataSource.InsertParameters.Add("Admin", "0");
            sqlDataSource.InsertParameters.Add("Login", user.Login.ToString());
            sqlDataSource.InsertParameters.Add("Password", user.Password.ToString());
            sqlDataSource.InsertCommand = "INSERT INTO dbo.Users (Name, Surname,Patronymic,Phone,Mail,Admin,Login,Password) " +
                "VALUES(@Name, @Surname,@Patronymic,@Phone,@Mail,@Admin,@Login,@Password);";
            sqlDataSource.Insert();
        }

        public static User logIn(LoginForm client)
        {
            foreach (User u in getUsers())
                if (u.Login == client.Login)
                    if (u.Password == client.Password)
                        return u;
            return null;
        }

        public static User getAdmin()
        {
            return getUsers()[0];
        }

        public static void MakeOrder(Cart cart, User user)
        {
            foreach(CartLine cartLine in cart.Lines)
            {
                for(int i = 0; i < cartLine.quantity; i++)
                {
                    if (getGame(cartLine.game.GameID).Quanity <= 0)
                        continue;
                    Game game = cartLine.game;
                    game.Quanity -= 1;
                    EditGame(game);
                    SqlDataSource sqlDataSource = new SqlDataSource();
                    sqlDataSource.ConnectionString = ConfigurationManager.ConnectionStrings["WebStoreDBConnectionString"].ToString();

                    sqlDataSource.InsertParameters.Add("GameID", cartLine.game.GameID.ToString());
                    sqlDataSource.InsertParameters.Add("UserID", user.UserId.ToString());
                    sqlDataSource.InsertCommand = "INSERT INTO dbo.Orders (UserID, GameID) VALUES(@UserID, @GameID);";
                    sqlDataSource.Insert();
                }
            }
        }
    }
}