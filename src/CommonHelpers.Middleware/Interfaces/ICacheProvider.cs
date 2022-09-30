namespace CommonHelpers.Middleware.Interfaces;

// TODO use simple json file backing
public interface ICacheProvider
{
    void SaveJson(string key, string json);
    string LoadJson(string key);
}
