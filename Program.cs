using System;

namespace POOProjet
{
    public class Program
    {
        static void Main(string[] args)
        {
            NetWorkManager Bruxelles = new NetWorkManager();
            Bruxelles.AffichageMini();
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Bruxelles.AffichageGraphique();
            //System.Threading.Thread.Sleep(5000);
            //Bruxelles.UpdateNetWork();
            //Bruxelles.AffichageGraphique();
            //Bruxelles.AffichageMini();

        }
    }
}
