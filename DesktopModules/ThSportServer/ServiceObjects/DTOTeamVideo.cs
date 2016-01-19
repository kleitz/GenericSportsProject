using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThSportServer.ServiceObjects
{
	public class DTOTeamVideo
	{
		public int TeamVideoID { get; set; }
		public int TeamID { get; set; }
		public string Title { get; set; }
		public string VideoPath { get; set; }
		public DateTime VideoDate { get; set; }
		public bool IsDisplayOnHomePage { get; set; }
		public int PortalID { get; set; }
		public string CreatedBy { get; set; }
		public string ModifyBy { get; set; }
	}
}
