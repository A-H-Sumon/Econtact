using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Econtact.econtactClasses
{
    class contactClass
    {
        //Getter Setter Propertise
        //Act as data carrier in our application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting Data from database

        public DataTable Select()
        {
            ///Step 1: Database Connection
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2: Writting Sql Querry
                string sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL dataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception )
            {

            }
            finally
            {
                conn.Close();

            }
            return dt;
        }
        //inserting data into database
        public bool Insert (contactClass c)
        {
            //creating a defsult return type and setting its value to false 
            bool isSuccess = false;
            //step 1: connect database
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //step 2: create a SQL querry to insert Data
                string sql = "INSERT INTO tbl_contact (FirstName, LastName, ContactNo, Address, Gender) VALUES(@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNO", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //conection open here
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //if the query runs successfully than the value of rows will be greater than zero else equal to zero
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }

        //method to update data in database from our application
        public bool Update(contactClass c)


        { 
            //create a default return type and set its default value to false
            bool isSuccess = false;
        SqlConnection conn = new SqlConnection(myconnstrng);
        try
        {
         //sql to update data to our database
         string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo=@ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";
        //creating sql command
        SqlCommand cmd = new SqlCommand(sql, conn);
        //creating parameters to add value
        cmd.Parameters.AddWithValue("@FirstName",c.FirstName);
            cmd.Parameters.AddWithValue("@LastName",c.LastName);
            cmd.Parameters.AddWithValue("@ContactNo",c.ContactNo);
            cmd.Parameters.AddWithValue("@Address",c.Address);
            cmd.Parameters.AddWithValue("@Gender",c.Gender);
            cmd.Parameters.AddWithValue("ContactID",c.ContactID);

            //open database connection

            conn.Open();
            int rows = cmd.ExecuteNonQuery();
        //If the query runs successfully then the value of rows will be greater than zero else its value will be zero
        if(rows>0)
            {
             isSuccess= true;
            }
           else
        {
            isSuccess= false;
        }
        }
        catch(Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }
        return isSuccess;
    }
    //method to delete data from database
    public bool Delete(contactClass c)
    {
    //create a default return value and set value to false
    bool isSuccess = false;
    //create sql connection
    SqlConnection conn = new SqlConnection(myconnstrng);
    try
    {
        //sql to delete data from database
        string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";
        //creating sql command to pass contactid to our query
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
        //opening connection
        conn.Open();
        int rows = cmd.ExecuteNonQuery();
        //if the query runs successfully then the the value of rows will be greater than zero
        if(rows>0)
        {
            isSuccess = true;
        }
        else
        {
            isSuccess = false;
        }
    }
    catch(Exception ex)
    {

    }
    finally
    {
        conn.Close();
    }
    return isSuccess;
    } 

    }
}
