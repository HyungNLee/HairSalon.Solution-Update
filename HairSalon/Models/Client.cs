using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
  public class Client
  {
    //Variables
    private int _id;
    private string _name;
    private int _stylistId;

    //Constructor
    public Client(string newName, int newStylistId, int id = 0)
    {
      _name = newName;
      _id = id;
      _stylistId = newStylistId;
    }

    //Getters
    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public int GetStylistId()
    {
      return _stylistId;
    }

    //Edit the instance of client.
    public void Edit(string newName, int newStylistId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE clients SET name = @newName, stylist_id = @newStylistId WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter clientName = new MySqlParameter();
      clientName.ParameterName = "@newName";
      clientName.Value = newName;
      cmd.Parameters.Add(clientName);

      MySqlParameter clientStylistId = new MySqlParameter();
      clientStylistId.ParameterName = "@newStylistId";
      clientStylistId.Value = newStylistId;
      cmd.Parameters.Add(clientStylistId);

      cmd.ExecuteNonQuery();

      _name = newName;
      _stylistId = newStylistId;

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    //Deletes client from database by ID.
    public static void Delete(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients WHERE id = @thisId";

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

    //Gets a list of all clients in database.
    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>() {};

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        allClients.Add(newClient);
      }

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }

      return allClients;
    }

    //Saves this instance of client to the database and updates the local ID with ID from database.
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@newName, @newStylistId);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newName";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter newStylistId = new MySqlParameter();
      newStylistId.ParameterName = "@newStylistId";
      newStylistId.Value = this._stylistId;
      cmd.Parameters.Add(newStylistId);

      cmd.ExecuteNonQuery();

      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    //Deletes all clients from database.
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE clients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
    }

    //Override Equals and Hash for testing
    public override bool Equals(System.Object otherClient)
    {
      if (!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool nameEquality = (this._name == newClient.GetName());
        bool stylistIdEquality = (this._stylistId == newClient.GetStylistId());
        
        return (nameEquality && stylistIdEquality);
      }
    }

    public override int GetHashCode()
    {
      string combinedHash = this.GetName() + this.GetStylistId();
      return combinedHash.GetHashCode();
    }

    //Finds client by client ID.
    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int clientId = 0;
      string clientName = "";
      int clientStylistId = 0;

      while (rdr.Read())
      {
          clientId = rdr.GetInt32(0);
          clientName = rdr.GetString(1);
          clientStylistId = rdr.GetInt32(2);
      }

      Client foundClient = new Client(clientName, clientStylistId, clientId);

       conn.Close();
       if (conn != null)
       {
           conn.Dispose();
       }

      return foundClient;
    }

    //Finds this client's stylist by Id.
    public Stylist FindStylist()
    {
      MySqlConnection conn =  DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _stylistId;
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
  }
}