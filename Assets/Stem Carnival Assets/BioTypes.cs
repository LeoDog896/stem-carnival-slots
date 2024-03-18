

using System;
using System.Collections.Generic;

public class JoinRequest
{
    public int id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public string userId { get; set; }
    public int gameId { get; set; }
    public bool acknowledged { get; set; }
    public bool terminated { get; set; }
    public JoinRequest linkedJoinRequest { get; set; }
    public JoinRequestUser user { get; set; }
}

public class JoinRequestList
{
    public List<JoinRequest> joinRequests { get; set; }
}

public class JoinRequestUser
{
    public string id { get; set; }
    public string name { get; set; }
    public int score { get; set; }
    public Session[] sessions { get; set; }
}

public class Session
{
    public int id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int gameId { get; set; }
    public string data { get; set; }
    public bool active { get; set; }

}