using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Stylist
  {
    //Variables.
    private int _id;
    private string _name;

    //Constructor.
    public Stylist(string newName, int id = 0)
    {
      _name = newName;
      _id = id;
    }

    //Getters and setters.
    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    //Delete specific stylist from database by ID.
    public void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists where id = @thisId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@thisId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Edits the name of this instance of stylist.
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter stylistName = new MySqlParameter();
      stylistName.ParameterName = "@newName";
      stylistName.Value = newName;
      cmd.Parameters.Add(stylistName);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Overrides Equals for testing.
    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        return this.GetName().Equals(newStylist.GetName());
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    //Save this item to the database and update the ID of local object with ID from database.
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name) VALUES (@newName);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Returns a list of all stylists currently in the database.
    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>() {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allStylists;
    }

    //Finds a specific stylist by Id.
    public static Stylist Find(int id)
    {
      MySqlConnection conn =  DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int stylistId = 0;
      string stylistName = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }

      Stylist newStylist = new Stylist(stylistName, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return newStylist;
    }

    //Deletes all stylists from database.
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Function to get all clients that attend this stylist.

    //Function to delete all clients from this stylist.

  }
}