using System;

namespace POOProjet
{
    class Program
    {
        static void Main(string[] args)
        {
            NetWorkManager Bruxelles = new NetWorkManager();
            Bruxelles.AffichageMini();
            System.Threading.Thread.Sleep(5000);
            Bruxelles.UpdateNetWork();
            Bruxelles.AffichageMini();
        }
    }
}
