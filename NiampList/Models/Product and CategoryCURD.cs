using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using NiampList.Models;


namespace NiampList.Models
{
    public class Product_and_CategoryCURD
    {
       public string cs = ConfigurationManager.ConnectionStrings["dbcs"].ConnectionString;


        //pagination
        
        public List<Product> pagination(int pageSize, int pageNumber)
        {
                        
            SqlConnection conn = new SqlConnection(cs);
           conn.Open();
            int startRow = (pageNumber - 1) * pageSize + 1;
            int endRow = pageNumber * pageSize;

            string query = $@"
                SELECT *
                FROM (
                    SELECT p.*, c.CategoryName, ROW_NUMBER() OVER (ORDER BY p.ProductId) AS RowNum
                    FROM Product p
                    JOIN Category c ON p.CategoryId = c.CategoryId
                ) AS NumberedRows
                WHERE RowNum BETWEEN {startRow} AND {endRow}";
            SqlCommand cmd= new SqlCommand(query,conn);
            SqlDataReader reader= cmd.ExecuteReader();

            List<Product> products = new List<Product>();
            while(reader.Read())
            {
                Product product = new Product
                {
                    ProductId = Convert.ToInt32(reader["ProductId"]),
                    ProductName = Convert.ToString(reader["ProductName"]),
                    CategoryId = Convert.ToInt32(reader["CategoryId"]),
                    Category = new Category
                    {
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryName = Convert.ToString(reader["CategoryName"])
                    }
                };

                products.Add(product);
            }
            return products;

        }

        // count of product
        public int GetTotalProductCount()
        {
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();
            string query = "SELECT COUNT(*) FROM Product";
            SqlCommand cmd = new SqlCommand(query, conn);
            object result = cmd.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int totalCount))
            {
                return totalCount;
            }
            return 0;
        }











            //  Category CURD
            // List Show
            public List<Category> GetCategories()
        {
            List<Category> categorieslist = new List<Category>(); 
            SqlConnection conn = new SqlConnection(cs);
           // string Query = " select * from Category";
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "select * from Category";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Category cat = new Category();
                cat.CategoryId = Convert.ToInt32(reader.GetValue(0).ToString());
                cat.CategoryName= reader.GetValue(1).ToString()  ;
                categorieslist.Add(cat);

            }
            conn.Close();

            return categorieslist;
        }
        // Add Category
        public bool AddCategory(Category caty)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "Insert Into Category (CategoryName) Values(@CategoryName)";
            cmd.Parameters.AddWithValue("@CategoryName", caty.CategoryName);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i>0)
            {
                return true;
            }
            else
            {
                return false;
            }
               
        }

        // Update Category
        public bool UpdateCategory(Category caty)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "Update Category  set CategoryName=@CategoryName where CategoryId=@CategoryId";
            cmd.Parameters.AddWithValue("@CategoryId", caty.CategoryId);
            cmd.Parameters.AddWithValue("@CategoryName", caty.CategoryName);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        // Delete Category 
        public bool DeleteCategory(int id)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "DELETE FROM Category WHERE CategoryId = @CategoryId";
            cmd.Parameters.AddWithValue("@CategoryId", id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0 )
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }






        // Products

        // List Product
        public List<Product> GetProducts()
        {
            List<Product> productlist = new List<Product>();
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "select * from Product";
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Product pro = new Product();

                pro.ProductId = Convert.ToInt32(reader.GetValue(0).ToString());
                pro.ProductName = reader.GetValue(1).ToString();
                pro.CategoryId = Convert.ToInt32(reader.GetValue(2).ToString());
                productlist.Add(pro);

            }
            conn.Close();
            return productlist;
        }

        // Add Product
        public bool AddProduct(Product pro)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);
            //Insert into dbo.Product (ProductName,CategoryId) Values ('Lenovo',2)
            cmd.CommandText = "Insert Into Product (ProductName,CategoryId) Values(@ProductName,@CategoryId)";
            cmd.Parameters.AddWithValue("@ProductName", pro.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", pro.CategoryId);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0 || pro.CategoryId>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        // Update Product
        public bool UpdateProduct(Product pro)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);

            /*Update Product set ProductName='Mahindra',CategoryId=1 where ProductId=1;*/

            cmd.CommandText = "Update Product set ProductName=@ProductName,CategoryId=@CategoryId where ProductId=@ProductId";
            cmd.Parameters.AddWithValue("@ProductId", pro.ProductId);
            cmd.Parameters.AddWithValue("@ProductName", pro.ProductName);
            cmd.Parameters.AddWithValue("@CategoryId", pro.CategoryId);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0 || pro.CategoryId>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        // Delete Product
        public bool DeleteProduct(int id)
        {
            SqlConnection conn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("", conn);
            cmd.CommandText = "DELETE FROM Product WHERE ProductId = @ProductId";
            cmd.Parameters.AddWithValue("@ProductId", id);
            conn.Open();
            int i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}