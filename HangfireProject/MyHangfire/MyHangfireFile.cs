using Hangfire;

namespace HangfireProject.MyHangfire
{
    public class MyHangfireFile
    {
        public static void MyTxtJobs(string msg)
        {
            // ------------- Fire and Forget -------------
            // Tetiklendikten sonra bir defa çalışır.
            //Hangfire.BackgroundJob.Enqueue<ITxtSender>(a => a.Sender(msg)); 

            // ------------- Delayed -------------
            // Belirlenen zamandan sonra çalışır.
            //Hangfire.BackgroundJob.Schedule<ITxtSender>(a => a.Sender("Delayed job tipi ile 15 Saniye Sonra Çalıştı"), System.TimeSpan.FromSeconds(15));

            // ------------- Continuations -------------
            // Daha önce tetiklenen jobs id baz alıp devamında kendisine ait jobs çalıştırılır.
            //var jobsId = Hangfire.BackgroundJob.Schedule<ITxtSender>(a => a.Sender("Delayed job tipi ile 15 Saniye Sonra Çalıştı"), System.TimeSpan.FromSeconds(15));
            //Hangfire.BackgroundJob.ContinueJobWith<ITxtSender>(jobsId, a => a.Sender("ContinueJobWith"));

            // ------------- Recurring -------------
            // Belirlenen aralıklarla sürekli çalışır. Örneğin bir firma müşterisine doğum günü mesajı göndermek isterse her seferinde mesaj göndermek yerine recurring kullanılarak her sene doğum gününde otomatik mesaj gönderilir.
            Hangfire.RecurringJob.AddOrUpdate<ITxtSender>("RecurringId", a => a.Sender("RecurringId"), Cron.Minutely);
        }
    }
}
