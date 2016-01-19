using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThSportServer.ServiceObjects
{
	public class DTOAssign
	{
		public int competition_Id;
		public int matchId;
		public int player_id;
		public int PortalId;
	}

	public class DTOAssignUnassign
	{
		public string matchId;
		public string competitionId;
		public string assigned;
		public string unassigned;
	}
}
