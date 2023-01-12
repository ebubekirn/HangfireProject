# HangfireProject
ASP.Net Core 6 Hangfire Practice

HANGFIRE Implement
Hangfire da birden fazla server vardır. Bu sayede bir makinanın üzerinde birden fazla server çalıştırabilirken clustered yapıyı da destekliyor. Böylelikle her makinada birer tane hangfire server ayağa kaldırarak yükümüzü dengeleyebiliriz.
Job storage kısımlarında da database ya da redis kullanırız. Ya da in memory cache ler vardır. 
Job Types: 
Fire and Forget : Tetiklendikten sonra bir defa çalışır.
Delayed : Belirlenen zamandan sonra çalışır.
Recurring : Belirli aralıklarla çalışır.
Continuations : Daha önce tetiklenen jobs id baz alıp devamında kendisine ait jobs çalıştırır.
Batch (Pro)
Batch Continuations (Pro)

		HANGFIRE Implement STEPS
  
Hangfire.AspNetCore ve Hangfire.SqlServer paketleri yüklendi.
Program.cs de eklenecek kodlar öncelikle build’den önce:
builder.Services.AddHangfire(a => 
a.UseSqlServerStorage(builder.Configuration.GetConnectionString("baglanti")));
builder.Services.AddHangfireServer();
Build’den sonra:
app.UseHangfireDashboard("/hangfire");
AppSettings bağlantısı şu şekilde:
"ConnectionStrings": {
    "baglanti": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=TestDB;User ID=sa;Password=123456;"
  }
Not: Ayrıca aynı bağlantı kodları “appsettings.Development” içine de yapıştırıldı. 
!! Sql.Server’da TestDB database’ini manuel olarak eklememiz gerekiyor (Database first)
Fire and Forget için aşağıdaki kod satırı yazıldı:
Hangfire.BackgroundJob.Enqueue<ITxtSender>(a => a.Sender(msg));
ITxtSender ve TxtSender dosyaları oluşturuldu. Burada jobs’ların geri dönüş mesajlarının döndüren komutlar yazıldı. Bu sayede yapılan işlemler adım adım kayıt altına alınmış olur.
Program.cs de ITextSender için instance yaratma işlemi yapıldı. Controller da metot çağrıldı.
Delayed için :
Hangfire.BackgroundJob.Schedule<ITxtSender>(a => a.Sender(msg), System.TimeSpan.FromSeconds(15));
Recurring için aşağıdaki kod satırı yazıldı:
Hangfire.RecurringJob.AddOrUpdate<ITxtSender>("RecurringId", a => a.Sender("RecurringId"), Cron.Minutely);
Countinations için aşağıdaki kod satırı yazıldı:
var jobsId = Hangfire.BackgroundJob.Schedule<ITxtSender>(a => a.Sender("delayed schedule çalıştı"), System.TimeSpan.FromSeconds(25));
 Hangfire.BackgroundJob.ContinueJobWith<ITxtSender>(jobsId, a => a.Sender("CountinueJobWith job type çalıştırıldı"));

Not: Cronjob ile ilgili çalışırken cron saat çevirilerini bu siteden kontrol edebiliriz: crontab.cronhub.io

Birden fazla uygulama aynı veritabanına bağlanıp birden fazla cron server oluşturup bunun üzerinde çalışabilirler. 
