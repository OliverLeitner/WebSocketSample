using WebSocketSharp;
using WebSocketSharp.Server;

namespace websocket
{
    public class ALPHA : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            DataBaseConnector dbCon = new DataBaseConnector();
            var msg = e.Data == "RANMA"
                      ? dbCon.demoSelect()
                      : "[{\"message\": \"im busy!\"}]";
            Send(msg);
        }
    }
}
