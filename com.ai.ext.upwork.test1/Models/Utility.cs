using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using com.ai.ext.upwork.test1.Hubs;
using System.Collections.Concurrent;

namespace com.ai.ext.upwork.test1.Models
{
    public class Utility
    {
        public static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; }
        }

        public static List<ClicksTracker> GetAllClicks(string query)
        {
            var clicks = new List<ClicksTracker>();

            try
            {
                using (var connection = new SqlConnection(ConnString))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Notification = null;

                        var dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            clicks.Add(item: new ClicksTracker
                            {
                                Id = (int)reader["ID"],
                                CampaignName = reader["CampaignName"].ToString(),
                                Clicks = reader["Clicks"].ToString() != null ? (int)reader["Clicks"] : 0,
                                Conversions = reader["Conversions"].ToString() != null ? (int)reader["Conversions"] : 0,
                                Impressions = reader["Impressions"].ToString() != null ? (int)reader["Impressions"] : 0,
                                AffiliateName = reader["AffiliateName"].ToString()//,
                                //Date = (DateTime)reader["Date"]
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //Console.WriteLine(ex);
                ClicksTrackerHub.SendClicks();
            }

            return clicks;
        }

        public static bool AddClickToDB(ClicksTracker item)
        {
            int rowsInserted = 0;
            string insertQuery = @"INSERT INTO DevTest (CampaignName, Clicks, Conversions, Impressions, AffiliateName)";
            insertQuery += "VALUES (@CampaignName, @Clicks, @Conversions, @Impressions, @AffiliateName)";

            try
            {
                using (var connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CampaignName", item.CampaignName.ToString());
                        command.Parameters.AddWithValue("@Clicks", item.Clicks != null ? item.Clicks : 0);
                        command.Parameters.AddWithValue("@Conversions", item.Conversions != null ? item.Conversions : 0);// item.Conversions);
                        command.Parameters.AddWithValue("@Impressions", item.Impressions != null ? item.Impressions : 0);// item.Impressions);
                        command.Parameters.AddWithValue("@AffiliateName", item.AffiliateName.ToString());

                        command.Notification = null;

                        var dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        rowsInserted = command.ExecuteNonQuery();

                        if(rowsInserted == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ///TODO: Add logs for exceptions message        
                ///
                ClicksTrackerHub.SendClicks();
            }
            return false;
        }

        public static bool UpdateClickToDB(ClicksTracker item)
        {
            int rowsUpdated = 0;
            string updateQuery = @"UPDATE DevTest SET CampaignName = @CampaignName,";
            updateQuery += "Clicks = @Clicks,";
            updateQuery += "Conversions = @Conversions,";
            updateQuery += "Impressions = @Impressions,";
            updateQuery += "AffiliateName = @AffiliateName ";
            updateQuery += "WHERE Id = @Id";           

            try
            {
                using (var connection = new SqlConnection(ConnString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", item.Id);
                        command.Parameters.AddWithValue("@CampaignName", item.CampaignName.ToString());
                        command.Parameters.AddWithValue("@Clicks", item.Clicks != null ? item.Clicks : 0);
                        command.Parameters.AddWithValue("@Conversions", item.Conversions != null ? item.Conversions : 0);// item.Conversions);
                        command.Parameters.AddWithValue("@Impressions", item.Impressions != null ? item.Impressions : 0);// item.Impressions);
                        command.Parameters.AddWithValue("@AffiliateName", item.AffiliateName.ToString());

                        command.Notification = null;

                        var dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        rowsUpdated = command.ExecuteNonQuery();

                        if (rowsUpdated == 1)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ///TODO: Add logs for exceptions message
                ///
                ClicksTrackerHub.SendClicks();
            }
            return false;
        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //if (e.Info == SqlNotificationInfo.Invalid)
            //{
            //    Console.WriteLine("The above notification query is not valid.");
            //}

            SqlDependency dependency = sender as SqlDependency;

            dependency.OnChange -= dependency_OnChange;

            if (e.Type == SqlNotificationType.Change)
            {
                //Console.WriteLine("Notification Info: " + e.Info);
                //Console.WriteLine("Notification source: " + e.Source);
                //Console.WriteLine("Notification type: " + e.Type);
                ClicksTrackerHub.SendClicks();
            }            
        }
    }
}