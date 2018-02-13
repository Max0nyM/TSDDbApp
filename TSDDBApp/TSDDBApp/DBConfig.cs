using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace TSDDBApp
{
    public class Db
    {
        MySqlConnection _conn;
        string _host;
        string _name;
        string _user;
        string _pass;
        string _sql;
        bool _admin;
        
        public Db()
        {
            _host = "localhost";
            _name = "forensics";
            _user = "admin";
            _pass = "test1234";
            string connStr = $"server={_host};user id={_user}; password={_pass}; database={_name}; pooling=false";
            _conn = new MySqlConnection(connStr);

            try
            {
                _conn.Open();
            }
            catch (MySqlException err)
            {
                
            }
            
        }

        public string Get()
        {
            return _conn.ConnectionString;
        }

        public ConnectionState Connected()
        {
            return _conn.State;
        }


        public bool GetUser(string username, string email)
        {
            _sql = "Select * from students where username = @username OR email = @email";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {
                Query.Parameters.AddWithValue("@username", username);
                Query.Parameters.AddWithValue("@email", email);

                using (DbDataReader reader = Query.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Query.Dispose();
            }
        }

        public bool CheckUser(string email,string id)
        {
            _sql = "Select * from students where email = @email AND id <> @ID";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {


                Query.Parameters.AddWithValue("@email", email);
                Query.Parameters.AddWithValue("@ID", id);


                using (DbDataReader reader = Query.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                Query.Dispose();
            }
        }

        public User[] GetUsers()
        {
         
            _sql = "Select * from students";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {
                Queue<User> users = new Queue<User>();

                using (DbDataReader reader = Query.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.Name = reader.GetString(reader.GetOrdinal("name"));
                            user.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                            user.Email = reader.GetString(reader.GetOrdinal("email"));
                            user.Age = reader.GetString(reader.GetOrdinal("age"));
                            user.Admin = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("usergroup")));
                            users.Enqueue(user);

                        }
                    }
                    return users.ToArray();
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                Query.Dispose();
            }

        }


        public User[] SearchUsers(string param,User searchUser)
        {

            _sql = "Select * from students where 1 "+param;
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {
                Queue<User> users = new Queue<User>();
                if (searchUser.Username != "")
                    Query.Parameters.AddWithValue("@username", searchUser.Username);
                if (searchUser.Name!="")
                Query.Parameters.AddWithValue("@name", searchUser.Name);
                if (searchUser.LastName != "")
                    Query.Parameters.AddWithValue("@last_name", searchUser.LastName);
                if (searchUser.Email != "")
                    Query.Parameters.AddWithValue("@email", searchUser.Email);
                if (searchUser.Age != "")
                    Query.Parameters.AddWithValue("@age", searchUser.Age);
            
                Console.WriteLine(_sql);
                using (DbDataReader reader = Query.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.Name = reader.GetString(reader.GetOrdinal("name"));
                            user.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                            user.Email = reader.GetString(reader.GetOrdinal("email"));
                            user.Age = reader.GetString(reader.GetOrdinal("age"));
                            user.Admin = Convert.ToBoolean(reader.GetInt32(reader.GetOrdinal("usergroup")));
                            users.Enqueue(user);

                        }
                    }
                    return users.ToArray();
                }

            }
            catch
            {
                return null;
            }
            finally
            {
                Query.Dispose();
            }

        }



        public User InitUser(User user)
        {
            _sql = "Select * from students where username = @username";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {
                Query.Parameters.AddWithValue("@username", user.Username);
                using (DbDataReader reader = Query.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.Exists = true;
                        if (Utils.VerifyMd5Hash(user.Password, reader.GetString(reader.GetOrdinal("password"))))
                        {
                            user.LoggedIn = true;
                            user.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            user.Name = reader.GetString(reader.GetOrdinal("name"));
                            user.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                            user.Email = reader.GetString(reader.GetOrdinal("email"));
                            user.Age = reader.GetString(reader.GetOrdinal("age"));

                            if (reader.GetInt64(reader.GetOrdinal("usergroup")) == 1)
                            {
                                user.Admin = true;
                            }
                        }


                    }

                }
               
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                Query.Dispose();
            }
        }

     

        public bool Insert(User user)
        {
            _sql = "Insert into students (username,password,name,last_name,email,age) values(@username,@password,@name,@last_name,@email,@age)";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {
                
                Query.Parameters.AddWithValue("@username", user.Username);
                Query.Parameters.AddWithValue("@password", Utils.GetMd5Hash(user.Password));
                Query.Parameters.AddWithValue("@name", user.Name);
                Query.Parameters.AddWithValue("@last_name", user.LastName);
                Query.Parameters.AddWithValue("@email", user.Email);
                Query.Parameters.AddWithValue("@age", user.Age);
                int rowCount = Query.ExecuteNonQuery();
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                _sql = "";
                Query.Dispose();
            }
           
           
        }

        public bool Update(User user)
        {
            _sql =
                "Update students set name = @name,last_name = @last_name,email = @email,age = @age where id = @ID";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {
              
                Query.Parameters.AddWithValue("@name", user.Name);
                Query.Parameters.AddWithValue("@last_name", user.LastName);
                Query.Parameters.AddWithValue("@email", user.Email);
                Query.Parameters.AddWithValue("@age", user.Age);
                Query.Parameters.AddWithValue("@ID", user.Id);


                int rowCount = Query.ExecuteNonQuery();
              
                return true;
            }
            catch (Exception err)
            {
                return false;
            }
            finally
            {
                Query.Dispose();
            }

        }

        public bool Delete(User user)
        {
            _sql = "Delete from students where id = @ID";
            MySqlCommand Query = new MySqlCommand(_sql, _conn);
            try
            {

                Query.Parameters.Add("@ID", user.Id);
                int rowCount = Query.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Query.Dispose();
            }
           
        }





    }
}
