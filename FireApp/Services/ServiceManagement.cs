namespace FireApp.Services
{
    public class ServiceManagement : IServiceManagement
    {
        public void GenerateMerchandise()
        {
            Console.WriteLine($"Generate Merchandise: Long running task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void SendEmail()
        {
            Console.WriteLine($"Send Email: Long running task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void SyncData()
        {
            Console.WriteLine($"Sync Data: Long running task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }

        public void UpdateDatabase()
        {
            Console.WriteLine($"Update Database: Long running task {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
        }
    }
}
