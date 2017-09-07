using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using Kaizen.Configuration;
using System.Threading.Tasks;
using Kaizen.BusinessLogic.Configuration;
using System.Linq;

namespace UIServer
{
    public class MainHub : Hub
    {
        public override Task OnConnected()
        {
            //string userEmail = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;
            var KaizenUser = Kaizen.BusinessLogic.Master.OnlineKaizenUser.FirstOrDefault(x => x.ConnectionIds.Contains(connectionId));
            if (KaizenUser == null)
            {
                return base.OnConnected();
            }
            if (KaizenUser.ConnectionIds.Count == 1)//only notify other user if he has new connection
                Clients.OthersInGroup(KaizenUser.CompanyID).OnUserOnline(KaizenUser);
            if (KaizenUser.ConnectionIds.Contains(Context.ConnectionId) == false)
                KaizenUser.ConnectionIds.Add(Context.ConnectionId);
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
        public override Task OnDisconnected(bool stopCalled)
        {
            var httpContext = Context.Request.GetHttpContext();
            var logedUsers = httpContext.Application["LogedUsers"] as List<KaizenSession>;
            if (logedUsers == null)
                return base.OnDisconnected(stopCalled);


            KaizenSession KaizenUser = logedUsers.FirstOrDefault(ee => ee.ConnectionIds.Contains(Context.ConnectionId));
            var userEmail = Context.User.Identity.Name;
            var connectionId = Context.ConnectionId;
            if (KaizenUser != null)
            {
                //remove from local cache server
                KaizenUser.ConnectionIds.RemoveAll(x => x == Context.ConnectionId);

                //remove from goupr this connection
                Groups.Remove(Context.ConnectionId, KaizenUser.CompanyID);

                //only if his all connection is out and user compeltely logged out
                if (!KaizenUser.ConnectionIds.Any())
                {
                    //notify group that this user loggedout
                    Clients.OthersInGroup(KaizenUser.CompanyID).OnUserOffline(KaizenUser);
                    UserServices.RemoveSessionByKaizenPublicKey(KaizenUser.KaizenPublicKey.ToString());

                    //clean his own variable
                    Clients.Client(Context.ConnectionId).LockUserOut();
                }
                //update our server storage 
                Context.Request.GetHttpContext().Application["LogedUsers"] = logedUsers;
            }
            return base.OnDisconnected(stopCalled);
        }
        public async Task NewLoginUser(string KaizenPublicKey)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            await Groups.Add(Context.ConnectionId, KaizenUser.CompanyID);
            KaizenUser.Status = 1;
            //add this connection to user incase if not already exist
            if (KaizenUser.ConnectionIds.Contains(Context.ConnectionId) == false)
                KaizenUser.ConnectionIds.Add(Context.ConnectionId);

            if (KaizenUser.ConnectionIds.Count == 1)//only notify other user if he has new connection
                Clients.OthersInGroup(KaizenUser.CompanyID).OnUserOnline(KaizenUser);

            //update our server storage 
            UserServices.UpdateKaizenSession(KaizenUser);
        }


        public void Send(string name, string message)
        {
            //Clients.Client(connectionID).addContosoChatMessageToPage(name, message);
            Clients.All.broadcastMessage(name, message);
        }
        public void SendChatMessage(string KaizenPublicKey, string UserName, string KaizenMessageLine)
        {
            KaizenSession KaizenUser = UserServices.GetKaizenSession(KaizenPublicKey);
            UserServices srv = new UserServices(KaizenUser);
            srv.AddChattingMessage(KaizenUser.UserName, UserName, KaizenMessageLine);
            // Clients.Group(KaizenUser.CompanyID, connectionId1).ReceivedChatMessage(KaizenUser.UserName, UserName, KaizenMessageLine);
            // Clients.All.ReceivedChatMessage(KaizenUser.UserName, UserName, KaizenMessageLine);
        }
   
        public async Task SendMessage(string KaizenPublicKey, string message, string ToUser)
        {
            KaizenSession KaizenUserFrom = UserServices.GetKaizenSession(KaizenPublicKey);
            KaizenSession KaizenUserTo = UserServices.GetUserSession(ToUser, KaizenUserFrom.CompanyID);

            //TODO: fix KaizenUserTo.ConnectionIds.FirstOrDefault()
            await Clients.Client(KaizenUserTo.ConnectionIds.FirstOrDefault()).RecieveMessage(message, KaizenUserFrom.UserName);
        }
        public string rrrrrrrrrrrrr(string message)
        {
            return message;
        }
        public void JoinGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }
    }
}