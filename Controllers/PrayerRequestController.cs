﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.IO;

namespace Trafficking_Intervention_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrayerRequestController : ControllerBase
    {
        // GET api/PrayerRequest
        [HttpGet]
        public List<PrayerRequestEntity> GetPrayerRequests() {

            // PrayerRequest will be populated with the result of the query.
            List<PrayerRequestEntity> prayerRequest = new List<PrayerRequestEntity>();

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                // create a string to hold the SQL command.
                string sql = $"select * from prayer_requests;";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    
                    // Reader allows you to read each value that comes back from the query and do something to it.
                    using(SqliteDataReader reader = command.ExecuteReader()) {
                        
                        // Loop through query exit when no more objects are left.
                        while (reader.Read()) {

                            // map the data to the prayerRequests model.
                            PrayerRequestEntity newPrayerRequest = new PrayerRequestEntity() {
                                AppUserID = reader.GetInt32(0),
                                FirstName = reader.GetString(1),
                                LastName = reader.GetString(2),
                                PrayerRequest = reader.GetString(3),
                                Date = reader.GetString(4),
                                Sites = reader.GetString(5)
                            };

                            // Add one to the list.
                            prayerRequest.Add(newPrayerRequest);
                        }
                    }
                }
                // close the connection
                conn.Close();
            }
            return prayerRequest;
        }

        // // GET api/PrayerRequest/user
        // [HttpGet("{id}")]
        // public ActionResult<string> Get(int id)
        // {
        //     return "value";
        // }

        // POST api/PrayerRequest
        [HttpPost]
        public void PostPrayerRequest([FromBody] PrayerRequestEntity postPrayerRequest)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"insert into prayer_requests (FirstName, LastName, PrayerRequest, Date, Sites) values (\"{postPrayerRequest.FirstName}\", \"{postPrayerRequest.LastName}\", \"{postPrayerRequest.PrayerRequest}\", \"{postPrayerRequest.Date}\", \"{postPrayerRequest.Sites}\");";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;           
        }

        // PUT api/PrayerRequest/"named-put"
        [HttpPut]
        public void Put([FromBody] PrayerRequestEntity putPrayerRequest)
        {

            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"update prayer_requests set FirstName = \"{putPrayerRequest.FirstName}\", LastName = \"{putPrayerRequest.LastName}\", PrayerRequest = \"{putPrayerRequest.PrayerRequest}\", Date = \"{putPrayerRequest.Date}\", Sites = \"{putPrayerRequest.Sites}\"  where LastName = \"{putPrayerRequest.LastName}\";";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;
        }

        // DELETE api/PrayerRequest/"named-delete"
        [HttpDelete]
        public void Delete([FromBody] PrayerRequestEntity dropPrayerRequest)
        {
            // GetFullPath will complete the path for the file named passed in as a string.
            string dataSource = "Data Source=" + Path.GetFullPath("traff-int-app.db");

            // Initialize the connection to the .db file.
            using(SqliteConnection conn = new SqliteConnection(dataSource)) {
                conn.Open();
                
                string sql = $"delete from prayer_requests where FirstName = \"{dropPrayerRequest.FirstName}\" and LastName = \"{dropPrayerRequest.LastName}\";";

                // create a new SQL command by combining the location and command string.
                using(SqliteCommand command = new SqliteCommand(sql, conn)) {
                    command.ExecuteNonQuery();
                }
                // close the connection
                conn.Close();
            }
            return;
        }
    }
}
