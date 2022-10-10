using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
namespace ApiClient
{
    class Program
    {
        public static string url = "";
        public static string api = "";
        static void Main(string[] args)
        {
            bool Status = true;

            Console.WriteLine("Web Api Client System");

            Console.Write("Url (http://www.site.com/): ");
            url = Console.ReadLine();
            string lastCharacter = url.Substring(url.Length - 1, 1);

            if (lastCharacter != "/") url += "/";

            Console.Write("Api (api/Name): ");
            api = Console.ReadLine();

            

            while (Status)
            {
                Console.WriteLine("Web api url: "+url + api);
                Console.WriteLine("İşlem Seçiniz ( Get Product List(1) \nGet Single Product(2) \nInsert Product(3) \nUpdate Product(4) \nDelete Product(5) \nClose System(0) )");
                Console.Write("İşlem: ");

                char islem = Convert.ToChar(Console.ReadLine());

                switch (islem)
                {
                    case '0':
                        Status = false;
                        Console.WriteLine("Programı Sonlandırmak için Bir Tuşa Basınız.");
                        Console.ReadKey();
                        continue;
                    case '1':
                        ApiControl.GetList(url, api);
                        break;
                    case '2':
                        Console.Write("Enter Product id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        ApiControl.Get(url, api, id);
                        break;
                    case '3':
                        ApiControl.Post(url, api);
                        break;
                    case '4':

                        Console.Write("Enter Product id: ");
                        int editid = Convert.ToInt32(Console.ReadLine());
                        ApiControl.Put(url, api, editid);
                        break;
                    case '5':
                        Console.Write("Enter Delete Product id: ");
                        int deleteid = Convert.ToInt32(Console.ReadLine());
                        ApiControl.Delete(url, api, deleteid);
                        break;
                    default:
                        Console.WriteLine("Tanımsız İşlem. Lütfen Tekrar Deneyiniz.");
                        break;

                }
                Console.ReadKey();
                Console.Clear();
            }
            
        }
    }
}
