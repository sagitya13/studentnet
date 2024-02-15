using crudtask.Models;
using crudtask.Repository.interfaces;
using Microsoft.AspNetCore.Hosting.Server;
using System.Data.SqlClient;

namespace crudtask.Repository
{
    public class customerRepo : ICustomerRepo
    {
        readonly string connectionString = "";
        public customerRepo()
        {
            connectionString = "Data Source=APINP-ELPT4W3IG\\SQLEXPRESS;Initial Catalog=coffee;User ID=tap2023;Password=tap2023;Encrypt=False";

        }
        public Customer GetCustomerById(string id)
        {
            Customer c = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                    string query = $@"SELECT 
    s.CustomerID, 
    s.CompanyName, 
    s.Address, 
    s.ContactName,
    t.OrderID, 
    t.OrderDate,
    t.ShipVia,
    d.CompanyName,
    d.Phone,
    t.ShipAddress,
    t.ShipName,
    do.ProductID,
    r.ProductName,
    do.Quantity,
    do.UnitPrice,
    do.Discount
FROM 
    Customers s
INNER JOIN
    Orders t ON s.CustomerID = t.CustomerID
INNER JOIN
    Shippers d ON d.ShipperID = t.ShipVia
INNER JOIN
    [Order Details] do ON t.OrderID = do.OrderID
INNER JOIN
    Products r ON do.ProductID = r.ProductID
WHERE 
    s.CustomerID = '{id}'";
        
       
                    SqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = query;
                        con.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            c = new Customer();
                            c.CustomerId = dr["CustomerId"].ToString();
                            c.CompanyName = dr["CompanyName"].ToString();
                            c.ContactName = dr["ContactName"].ToString();
                        }


                    }
                    return c;
                }

        public List<string> GetCustomers()
        {
            throw new NotImplementedException();
        }



        public Customer CreateCustomer(Customer customer)
        {
            string query = @"INSERT INTO Customers(CustomerID, CompanyName, ContactName) values(@CustomerID, @CompanyName, @ContactName)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", customer.ContactName);


                    con.Open();
                    cmd.ExecuteNonQuery();



                    return customer;
                }
            }
        }




        public Customer UpdateCustomer(Customer customer, string id)
        {

            string query = @"UPDATE Customers SET CompanyName = @CompanyName, ContactName = @ContactName where CustomerID=@CustomerID";




            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerId);
                    cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                    cmd.Parameters.AddWithValue("@ContactName", customer.ContactName);


                    con.Open();
                    cmd.ExecuteNonQuery();



                    return customer;
                }
            }
        }




        public void DeleteCustomer(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("Customer ID cannot be null or empty.");
            }


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string deleteOrderDetailsQuery = "DELETE FROM [OrderDetails] WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID)";
                SqlCommand deleteOrderDetailsCmd = con.CreateCommand();
                deleteOrderDetailsCmd.CommandText = deleteOrderDetailsQuery;
                deleteOrderDetailsCmd.Parameters.AddWithValue("@CustomerID", id);
                con.Open();
                deleteOrderDetailsCmd.ExecuteNonQuery();

                string deleteOrdersQuery = "DELETE FROM Orders WHERE CustomerID = @CustomerID";
                SqlCommand deleteOrdersCmd = con.CreateCommand();
                deleteOrdersCmd.CommandText = deleteOrdersQuery;
                deleteOrdersCmd.Parameters.AddWithValue("@CustomerID", id);
                deleteOrdersCmd.ExecuteNonQuery();

                string deleteCustomerQuery = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                SqlCommand deleteCustomerCmd = con.CreateCommand();
                deleteCustomerCmd.CommandText = deleteCustomerQuery;
                deleteCustomerCmd.Parameters.AddWithValue("@CustomerID", id);
                deleteCustomerCmd.ExecuteNonQuery();
            }
        }

    }

    
}
