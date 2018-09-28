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
    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId; DELETE FROM stylists_specialities WHERE stylist_id = @thisId;";

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

    //Finds a specific stylist by name.
    public static Stylist FindByName(string name)
    {
      MySqlConnection conn =  DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE name = (@searchName);";

      MySqlParameter searchName = new MySqlParameter();
      searchName.ParameterName = "@searchName";
      searchName.Value = name;
      cmd.Parameters.Add(searchName);

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
      cmd.CommandText = @"TRUNCATE TABLE stylists; TRUNCATE TABLE stylists_specialities;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Function to get all clients that attend this stylist.
    public List<Client> GetClients()
    {
      List<Client> allStylistClients = new List<Client>() {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        allStylistClients.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return allStylistClients;
    }

    //Function to delete all clients from this stylist.
    public void DeleteAllClientsFromStylist()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE * FROM clients WHERE stylist_id = @stylist_id;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    //Function to get number of clients.
    public int NumberOfClients()
    {
      List<Client> allStylistClients = new List<Client>() {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylist_id;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylist_id";
      stylistId.Value = this._id;
      cmd.Parameters.Add(stylistId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        allStylistClients.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return allStylistClients.Count;
    }

    // Function to add a speciality to this stylist.
    public void AddSpeciality(Speciality newSpeciality)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialities (stylist_id, speciality_id) VALUES (@stylistId, @specialityId);";

      cmd.Parameters.AddWithValue(@"stylistId", this._id);
      cmd.Parameters.AddWithValue(@"specialityId", newSpeciality.GetId());

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // Function to get all specialities of this stylist.
    public List<Speciality> GetSpecialities()
    {
      List<Speciality> allSpecialities = new List<Speciality>() {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialities.* FROM stylists JOIN stylists_specialities ON (stylists.id = stylists_specialities.stylist_id) JOIN specialities ON (stylists_specialities.speciality_id = specialities.id) WHERE stylists.id = @thisId;";

      cmd.Parameters.AddWithValue("@thisId", this._id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int newId = rdr.GetInt32(0);
        string newName = rdr.GetString(1);
        Speciality newSpeciality = new Speciality(newName, newId);
        allSpecialities.Add(newSpeciality);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return allSpecialities;
    }
  }
}