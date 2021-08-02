using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;



namespace FivemPlayerlistServer
{
    public class FPLServer : BaseScript
    {

        private Dictionary<int, dynamic[]> list = new Dictionary<int, dynamic[]>();
        public FPLServer()
        {
            EventHandlers.Add("fs:getServerInfo", new Action<Player>(ReturnServerInfo));
            Exports.Add("setPlayerRowConfig", new Action<string, string, string, string, string>(SetPlayerConfig2));
            EventHandlers.Add("fs:setPlayerRowConfig", new Action<int, string, int, int, bool>(SetPlayerConfig));
        }

        private async void ReturnServerInfo([FromSource] Player source)
        {
            source.TriggerEvent("fs:setServerInfo", GetConvarInt("sv_maxClients", 30), GetConvar("sv_projectName", "FiveM"), GetConvar("sv_serverType", "Public"));
            var pl = new PlayerList();
            foreach (Player p in pl)
            {
                if (list.ContainsKey(int.Parse(p.Handle)))
                {
                    var listItem = list[int.Parse(p.Handle)];
                    var p1 = listItem[0];
                    var p2 = listItem[1];
                    var p3 = listItem[2];
                    var p4 = listItem[3];
                    var p5 = listItem[4];
                    source.TriggerEvent("fs:setPlayerRowConfig", p1, p2, p3, p4);
                    await Delay(1);
                }
            }
        }

        private void SetPlayerConfig2(string playerServerId, string crewName, string rankNumber, string jobPoints, string showJobPointsIcon)
        {
            SetPlayerConfig(int.Parse(playerServerId), crewName, int.Parse(rankNumber ?? "-1"), int.Parse(jobPoints ?? "-1"), bool.Parse(showJobPointsIcon ?? "false"));
        }
        private void SetPlayerConfig(int playerServerId, string crewName, int rankNumber, int jobPoints, bool showJobPointsIcon)
        {
            if (playerServerId > 0)
            {
                list[playerServerId] = new dynamic[5] { playerServerId, crewName ?? "", rankNumber != null ? rankNumber : -1, jobPoints != null ? jobPoints : -1, showJobPointsIcon != null ? showJobPointsIcon : false };
                TriggerClientEvent("fs:setPlayerConfig", playerServerId, crewName ?? "", rankNumber != null ? rankNumber : -1, jobPoints != null ? jobPoints : -1,
                    showJobPointsIcon != null ? showJobPointsIcon : false);
            }
        }

    }
}
