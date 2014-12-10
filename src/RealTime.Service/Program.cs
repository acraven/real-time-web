namespace RealTime.Service
{
   using Topshelf;

   public class Program
   {
      public static void Main(string[] args)
      {
         HostFactory.Run(h =>
            {
               h.Service<RealTimeService>(s =>
                  {
                     s.ConstructUsing(name => new RealTimeService());
                     s.WhenStarted(c => c.Start());
                     s.WhenStopped(c => c.Stop());
                  });

               h.RunAsLocalSystem();

               h.SetServiceName("RealTime.Service");
               h.SetDisplayName("RealTime Service");
            });                       
      }
   }
}
