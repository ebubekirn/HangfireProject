namespace HangfireProject.MyHangfire
{
    public interface ITxtSender
    {
        Task Sender(string msg);
    }
}
