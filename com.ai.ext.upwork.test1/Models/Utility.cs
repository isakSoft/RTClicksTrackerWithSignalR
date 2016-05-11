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

        public static IEnumerable<ClicksTracker> GetAllClicks(string query)
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
                                CampaignName = reader["CampaignName"].ToString(),
                                Clicks = (int)reader["Clicks"],
                                Conversions = (int)reader["Conversions"],
                                Impressions = (int)reader["Impressions"],
                                AffiliateName = reader["AffiliateName"].ToString()//,
                                //Date = (DateTime)reader["Date"]
                            });
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
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
                        command.Parameters.AddWithValue("@Clicks", item.Clicks);
                        command.Parameters.AddWithValue("@Conversions", item.Conversions);
                        command.Parameters.AddWithValue("@Impressions", item.Impressions);
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
            }
            return false;
        }

        public static bool UpdateClickToDB(ClicksTracker item)
        {
            int rowsUpdated = 0;
            string updateQuery = @"UPDATE DevTest SET CampaignName = @CampaignName,  Conversions, Impressions, AffiliateName)";
            updateQuery += "Clicks = @Clicks,";
            updateQuery += "Conversions = @Conversions,";
            updateQuery += "Impressions = @Impressions,";
            updateQuery += "AffiliateName = @AffiliateName";
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
                        command.Parameters.AddWithValue("@Clicks", item.Clicks);
                        command.Parameters.AddWithValue("@Conversions", item.Conversions);
                        command.Parameters.AddWithValue("@Impressions", item.Impressions);
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
            }
            return false;
        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {         
            if (e.Type == SqlNotificationType.Change)
            {
                ClicksTrackerHub.SendClicks();
            }            
        }
    }
}