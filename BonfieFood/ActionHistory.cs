using System.Data.SqlClient;

namespace BonfieFood
{
    public static class ActionHistory
    {
        public static void SaveActionHistoryToDB(DataBase db, string name)
        {
            string query = @"INSERT INTO UserActionHistory (id_User, nameAction)
                             VALUES (@UserId, @name)";

            using (SqlCommand cmd = new SqlCommand(query, db.getConnection()))
            {
                cmd.Parameters.AddWithValue("@UserId", CurrentUser.UserId);
                cmd.Parameters.AddWithValue("@name", name);

                db.openConnection();
                cmd.ExecuteNonQuery();
                db.closeConnection();
            }
        }
    }
}
