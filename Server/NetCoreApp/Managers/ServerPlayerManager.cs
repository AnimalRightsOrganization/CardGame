using System;
using System.Linq;
using System.Collections.Generic;

namespace NetCoreServer
{
    public class ServerPlayerManager
    {
        public ServerPlayerManager()
        {
            players = new Dictionary<Guid, string>();
        }
        protected Dictionary<Guid, string> players;
        public int Count => players.Count;

        public void AddPlayer(Guid _guid, string _username)
        {
            players.Add(_guid, _username);
        }
        public void RemovePlayer(Guid _guid)
        {
            players.Remove(_guid);
        }
        // 登录成功，赋值用户名
        public void UpdatePlayer(Guid _guid, string _username)
        {
            players[_guid] = _username;
        }
        public Guid GetGuidByUsername(string _username)
        {
            var p = players.Where(x => x.Value == _username).ToList().FirstOrDefault();
            return p.Key;
        }
        public string GetUsernameByGuid(Guid _guid)
        {
            string username = string.Empty;
            players.TryGetValue(_guid, out username);
            return username;
        }
    }
}