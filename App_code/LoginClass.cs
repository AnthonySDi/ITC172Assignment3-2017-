using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; //general data tools
using System.Data.SqlClient; //sql server data tools
using System.Configuration; //tools for talking to config file

/// <summary>
/// Summary description for LoginClass
/// </summary>
public class LoginClass
{
    SqlConnection connect;
    string user;
    string pass;
    public LoginClass(string userName, string password)
    {
        user = userName;
        pass = password;
        string connectString = 
            ConfigurationManager.ConnectionStrings["BookReviewConnection"].ToString();
        connect = new SqlConnection(connectString);
    }

    public int ValidateLogin()
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = connect;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "usp_ReviewerLogin";
        cmd.Parameters.AddWithValue("@ReviewerUserName", user);
        cmd.Parameters.AddWithValue("@ReviewerPassword", pass);
        connect.Open();
        int result = cmd.ExecuteNonQuery();
        connect.Close();
        int key = 0;
        if(result != -1)
        {
            key = GetUserKey();
        }
        return key;
    }

    private int GetUserKey()
    {
        string sql = "Select ReviewerKey from reviewer where ReviewerUserName =@user";
        SqlCommand cmd = new SqlCommand(sql, connect);
        cmd.Parameters.AddWithValue("@user", user);
        SqlDataReader reader = null;
        connect.Open();
        reader = cmd.ExecuteReader();
        int key = 0;
        while (reader.Read())
        {
             key= (int)reader["ReviewerKey"];
        }
        connect.Close();


        return key;
    }
}