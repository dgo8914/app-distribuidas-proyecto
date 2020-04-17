using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace Proye.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = ("Data Source=imsnor.database.windows.net;Initial Catalog=db;User ID=dbUserN;Password=Npas$w0rd;");

        public IActionResult Index()
        {
            var data = new List<Clases>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT [ID], [Clave], [Profesor], [Cupo] FROM [Clases]", con);

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    data.Add(new Clases
                    {
                        Id = (Guid)dr["ID"],
                        Clave = (string)dr["Clave"],
                        Profesor = (string)dr["Profesor"],
                        Cupo = (int)dr["Cupo"]
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }

            return View(data);
        }
        public IActionResult Details(Guid id)
        {
            var data = new Clases();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT [ID], [Clave], [Profesor], [Cupo] FROM [Clases] WHERE [ID] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["ID"];
                    data.Clave = (string)dr["Clave"];
                    data.Profesor= (string)dr["Profesor"];
                    data.Cupo= (int)dr["Cupo"];
                }
                return PartialView(data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Clases data)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [Clases] ([ID],[Clave],[Profesor],[Cupo]) VALUES (NEWID(), @n, @e,@a);", con);

            cmd.Parameters.Add("@n", SqlDbType.NVarChar, 100).Value = data.Clave;
            cmd.Parameters.Add("@e", SqlDbType.NVarChar, 100).Value = data.Profesor;
            cmd.Parameters.Add("@a", SqlDbType.Int, 100).Value = data.Cupo;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Edit(Guid id)
        {
            var data = new Clases();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT [ID], [Clave], [Profesor], [Cupo] FROM [Clases] WHERE [ID] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    data.Id = (Guid)dr["ID"];
                    data.Clave= (string)dr["Clave"];
                    data.Profesor= (string)dr["Profesor"];
                    data.Cupo = (int)dr["Cupo"];
                }
                return View(data);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Clases data)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(@"UPDATE [Clases] SET [Clave] = @n, [Profesor] = @e	, [Cupo] = @a WHERE [ID] = @i;", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = data.Id;
            cmd.Parameters.Add("@n", SqlDbType.NVarChar, 100).Value = data.Clave;
            cmd.Parameters.Add("@e", SqlDbType.NVarChar, 100).Value = data.Profesor;
            cmd.Parameters.Add("@a", SqlDbType.Int, 100).Value = data.Cupo;


            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public IActionResult Delete(Guid id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM [Clases] WHERE [ID] = @i", con);

            cmd.Parameters.Add("@i", SqlDbType.UniqueIdentifier).Value = id;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}