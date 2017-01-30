using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; //these libraries must be added
using System.Data.SqlClient; //sql server tools
using System.Configuration; //to talk to the config file

/// <summary>
/// Summary description for GrantRequestClass
/// </summary>
public class BookClass
{
    //declare the connection string
    private SqlConnection connect;
    public BookClass()
    {
        //get the connection string
        string connectString = 
            ConfigurationManager.ConnectionStrings["BookReviewConnection"].ToString();
        //initilize the connection object
        connect = new SqlConnection(connectString);
    }

    //this method takes a parameter authorkey
    //and returns all the books for that author
    public DataTable Getbooks(int authorKey)
    {
        //declare a new data table to store the results
        DataTable table = new DataTable();
        //sql statement
        string sql = "Select * From Book inner Join AuthorBook " +
            "on Book.BookKey=AuthorBook.BookKey " +
            "Where AuthorKey = @author";
        //the command object passes the sql through the connection
        SqlCommand cmd = new SqlCommand(sql, connect);
        //the parameter replaces the sql variable with a value
        cmd.Parameters.AddWithValue("@author", authorKey);
        //declare a reader to stream the results
        SqlDataReader reader = null;
        //open the connection
        connect.Open();
        //execute the command and reader
        reader = cmd.ExecuteReader();
        //load the results into the table 
        table.Load(reader);
        //close stuff
        reader.Close();
        connect.Close();
        return table;
    }

    public DataTable GetAuthors()
    {
        DataTable table = new DataTable();
        string sql = "Select AuthorKey, AuthorName from Author";
        SqlCommand cmd = new SqlCommand(sql, connect);
        SqlDataReader reader = null;
        connect.Open();
        reader = cmd.ExecuteReader();
        table.Load(reader);
        reader.Close();
        connect.Close();
        return table;
    }
}