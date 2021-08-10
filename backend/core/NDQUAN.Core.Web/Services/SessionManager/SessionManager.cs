using Microsoft.AspNetCore.Http;
using NDQUAN.Core.Models.AuthModels;
using NDQUAN.Core.Web.Services.SessionManager;
using RedisAPI;
using System;
using System.Collections.Generic;

public static class SessionManager
{
    private static readonly SessionEnum? _sessionKey;
    private static readonly RedisDataAgent _cache = new RedisDataAgent();

    //private 

    public static void Add(AuthenticateResponse authenResponse)
    {
        SessionData session = new SessionData();
        session.session_id = authenResponse.SessionId;
        session.user_info = new UserInfo();
        session.user_info.user_id = authenResponse.UserId;
        _cache.Set<SessionData>(String.Format(CacheKeySession.CacheUserSession, authenResponse.UserId, authenResponse.SessionId), session);
    }

    public static SessionData Get(Guid userId)
    {
        var sessionData = _cache.Get<SessionData>(String.Format(CacheKeySession.CacheUserSession, userId));
        return sessionData;
    }

    public static SessionData Get(Guid userId, string sessionId)
    {
        var sessionData = _cache.Get<SessionData>(String.Format(CacheKeySession.CacheUserSession, userId, sessionId));
        return sessionData;
    }

}

public static class CacheKeySession
{
    public const string CacheUserSession = "key_session_{0}_{1}"; 
}
