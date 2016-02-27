using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Newtonsoft.Json;

namespace SportSiteServer
{
    public class UtilityFunctions
    {
        public enum DataManageKeyValue
        {
            ROSTER_POSITION = 3,
            PLAYER_POSITION = 5
        }

        public string GetDefaultImage(string team_name,string Image_Width)
        {
            string imageHtml = String.Empty;
            string imagePath = "/DesktopModules/SportSite/Images/Team_Logo.png";
            string divForImage = String.Empty;

            if (Image_Width == "400px")
            {
                divForImage = "TeamPage-Logo-Wrapper";
            }
            else if (Image_Width == "370px")
            {
                divForImage = "TeamPage-Logo-WrapperForTeam";
            }

            else if (Image_Width == "160px")
            {
                divForImage = "TeamPage-Logo-Wrapper-160PX";
            }
            else if (Image_Width == "120px")
            {
                divForImage = "TeamPage-Logo-Wrapper-120PX";
            }
            else if (Image_Width == "80px")
            {
                divForImage = "TeamPage-Logo-Wrapper-80PX";
            }
            else if (Image_Width == "60px")
            {
                divForImage = "TeamPage-Logo-Wrapper-60PX";
            }
            else if (Image_Width == "70px")
            {
                divForImage = "TeamPage-Logo-Wrapper-70PX";
            }
            else if (Image_Width == "45px")
            {
                divForImage = "TeamPage-Logo-Wrapper-45PX";
            }
            else if (Image_Width == "40px")
            {
                divForImage = "TeamPage-Logo-Wrapper-40PX";
            }
            else if (Image_Width == "30px")
            {
                divForImage = "TeamPage-Logo-Wrapper-30PX";
            }
            else if (Image_Width == "20px")
            {
                divForImage = "TeamPage-Logo-Wrapper-20PX";
            }

            imageHtml =
            "<div class="+ divForImage +">" +
                "<img src=" + imagePath + " alt='Team Logo' />" +
                "<div class='TeamName'>" + team_name + "</div></div>";

            return imageHtml.ToString();

            return imageHtml;
        }

        //public string GetGridDefaultImage(string team_name)
        //{
        //    string imageHtml = String.Empty;
        //    string imagePath = "/DesktopModules/SportSite/Images/Team_Logo.png";

        //    imageHtml =
        //    "<div class='TeamPage-Logo-Wrapper-60PX'>" +
        //        "<img src=" + imagePath + " alt='Team Logo' />" +
        //        "<div class='TeamName'>" + team_name + "</div></div>";

        //    return imageHtml.ToString();

        //    return imageHtml;
        //}

		public static List<T> DataReaderMapToList<T>(IDataReader dr)
		{
			List<T> list = new List<T>();
			T obj = default(T);
			while (dr.Read())
			{
				obj = Activator.CreateInstance<T>();
				foreach (PropertyInfo prop in obj.GetType().GetProperties())
				{
					if (!object.Equals(dr[prop.Name], DBNull.Value))
					{
						prop.SetValue(obj, dr[prop.Name], null);
					}
				}
				list.Add(obj);
			}
			return list;
		}

		public static string DataTabletoJson(DataTable dt)
		{
			List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
			Dictionary<string, object> row;
			foreach (DataRow dr in dt.Rows)
			{
				row = new Dictionary<string, object>();
				foreach (DataColumn col in dt.Columns)
				{
					row.Add(col.ColumnName, dr[col]);
				}
				rows.Add(row);
			}

			return JsonConvert.SerializeObject(rows);
		}
    }
}
