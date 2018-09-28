using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Speciality
  {
    //Variables.
    private int _id;
    private string _name;

    //Constructor.
    public Speciality(string newName, int id = 0)
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

    //Delete specific speciality from database by ID.
    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialities WHERE id = @thisId; DELETE FROM stylists_specialities WHERE speciality_id = @thisId;";

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

    // //Edits the name of this instance of speciality.
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialities SET name = @newName WHERE id = @searchId;";

      cmd.Parameters.AddWithValue("@searchId", _id);
      cmd.Parameters.AddWithValue("@newName", newName);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Overrides Equals for testing.
    public override bool Equals(System.Object otherSpeciality)
    {
      if (!(otherSpeciality is Speciality))
      {
        return false;
      }
      else
      {
        Speciality newSpeciality = (Speciality) otherSpeciality;
        return this.GetName().Equals(newSpeciality.GetName());
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
      cmd.CommandText = @"INSERT INTO specialities (name) VALUES (@newName);";

      cmd.Parameters.AddWithValue("@newName", this._name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Returns a list of all specialities currently in the database.
    public static List<Speciality> GetAll()
    {
      List<Speciality> allSpecialities = new List<Speciality>() {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities;";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while (rdr.Read())
      {
        int specialityId = rdr.GetInt32(0);
        string specialityName = rdr.GetString(1);
        Speciality newSpeciality = new Speciality(specialityName, specialityId);
        allSpecialities.Add(newSpeciality);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return allSpecialities;
    }

    //Finds a specific speciality by Id.
    public static Speciality Find(int id)
    {
      MySqlConnection conn =  DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialities WHERE id = (@searchId);";

      cmd.Parameters.AddWithValue("@searchId", id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int newId = 0;
      string newName = "";

      while (rdr.Read())
      {
        newId = rdr.GetInt32(0);
        newName = rdr.GetString(1);
      }

      Speciality newSpeciality = new Speciality(newName, newId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return newSpeciality;
    }

    // //Finds a specific stylist by name.
    // public static Stylist FindByName(string name)
    // {
    //   MySqlConnection conn =  DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT * FROM stylists WHERE name = (@searchName);";

    //   MySqlParameter searchName = new MySqlParameter();
    //   searchName.ParameterName = "@searchName";
    //   searchName.Value = name;
    //   cmd.Parameters.Add(searchName);

    //   var rdr = cmd.ExecuteReader() as MySqlDataReader;

    //   int stylistId = 0;
    //   string stylistName = "";

    //   while (rdr.Read())
    //   {
    //     stylistId = rdr.GetInt32(0);
    //     stylistName = rdr.GetString(1);
    //   }

    //   Stylist newStylist = new Stylist(stylistName, stylistId);

    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }

    //   return newStylist;
    // }

    //Deletes all specialities from database.
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE specialities; TRUNCATE TABLE stylists_specialities;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    //Function to get all stylists that have this speciality.
    public List<Stylist> GetStylists()
    {
      List<Stylist> allStylists = new List<Stylist>() {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialities JOIN stylists_specialities ON (specialities.id = stylists_specialities.speciality_id) JOIN stylists ON (stylists_specialities.stylist_id = stylists.id) WHERE specialities.id = @searchId;";

      cmd.Parameters.AddWithValue("@searchId", this._id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
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

    // //Function to delete all clients from this stylist.
    // public void DeleteAllClientsFromStylist()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"DELETE * FROM clients WHERE stylist_id = @stylist_id;";

    //   MySqlParameter stylistId = new MySqlParameter();
    //   stylistId.ParameterName = "@stylist_id";
    //   stylistId.Value = this._id;
    //   cmd.Parameters.Add(stylistId);

    //   cmd.ExecuteNonQuery();

    //   conn.Close();
    //   if (conn != null)
    //   {
    //       conn.Dispose();
    //   }
    // }

    // Function to add a stylist to this speciality.
    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialities (stylist_id, speciality_id) VALUES (@stylistId, @specialityId);";

      cmd.Parameters.AddWithValue(@"stylistId", newStylist.GetId());
      cmd.Parameters.AddWithValue(@"specialityId", this._id);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // Function to get all stylists with this speciality.
    public List<Stylist> GetSpecialities()
    {
      List<Stylist> allStylists = new List<Stylist>() {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylists.* FROM specialities JOIN stylists_specialities ON (specialities.id = stylists_specialities.speciality_id) JOIN stylists ON (stylists_specialities.stylist_id = stylists.id) WHERE specialities.id = @thisId;";

      cmd.Parameters.AddWithValue("@thisId", this._id);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int newId = rdr.GetInt32(0);
        string newName = rdr.GetString(1);
        Stylist newStylist = new Stylist(newName, newId);
        allStylists.Add(newStylist);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return allStylists;
    }
  }
}