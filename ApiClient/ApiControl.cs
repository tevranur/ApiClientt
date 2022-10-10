using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
namespace ApiClient
{
    class ApiControl
    {
        public static void GetList(string url, string api)
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = client.GetAsync(api).Result;

                if(message.IsSuccessStatusCode)//dönen kod 200 ise 
                {
                    var products = message.Content.ReadAsAsync<List<Product>>().Result;

                    Console.WriteLine("Product List");
                    Console.WriteLine("-------------------------------------");
                    foreach (var prdct in products)
                    {
                        Console.WriteLine(  "Id         : " + prdct.Id + 
                                            "\nName       : " + prdct.Name+
                                            "\nDescription: "+prdct.Description+
                                            "\nPrice      : "+prdct.Price+
                                            "\nStatus     : "+prdct.Status);
                        Console.WriteLine("------------------------");
                    }
                    Console.WriteLine("-------------------------------------");

                }
                else
                {
                    Console.WriteLine("Api ile bağlantı kurulamadı. Lütfen Tekrar Deneyiniz.");
                }
            }
        }
        public static void Get(string url, string api,int id)
        {
            using(var client=new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage message = client.GetAsync(api + "/" + id).Result;

                if (message.IsSuccessStatusCode)
                {
                    var product = message.Content.ReadAsAsync<Product>().Result;

                    Console.WriteLine("Product Detail");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine(  "Id         : " + product.Id +
                                        "\nName       : " + product.Name +
                                        "\nDescription: " + product.Description +
                                        "\nPrice      : " + product.Price +
                                        "\nStatus     : " + product.Status);
                    Console.WriteLine("-------------------------------------");
                }
                else
                {
                    Console.WriteLine("Ürün bulunamadı ya da api ile iletişim kurulamadı.");
                }
            }
        }
        public static void Delete(string url, string api, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage messageGet = client.GetAsync(api + "/" + id).Result;
                if (messageGet.IsSuccessStatusCode)
                {
                    var product = messageGet.Content.ReadAsAsync<Product>().Result;
                    Console.WriteLine("Delete Product Detail");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Id         : " + product.Id +
                                        "\nName       : " + product.Name +
                                        "\nDescription: " + product.Description +
                                        "\nPrice      : " + product.Price +
                                        "\nStatus     : " + product.Status);
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Ürünü silmek için bir tuşa basınız");
                    Console.ReadKey();
                    HttpResponseMessage messageDelete = client.DeleteAsync(api + "/" + id).Result;
                    if (messageDelete.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Ürün Silme İşlemi Başarılı.");
                        Console.WriteLine(messageDelete.Content.ReadAsAsync<string>().Result);
                    }
                    else
                    {
                        Console.WriteLine("Ürün Silme İşleminde Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.");
                    }
                }
                else
                {
                    Console.WriteLine("Ürün bulunamadı ya da api ile iletişim kurulamadı.");
                }
            }
        }
        public static void Post(string url, string api)
        {
            Product product = new Product();
            Console.WriteLine("Add New Product");
            Console.WriteLine("-------------------------------------");
            Console.Write("Name        : ");
            product.Name = Console.ReadLine();
            Console.Write("Description : ");
            product.Description = Console.ReadLine();
            Console.Write("Price       : ");
            product.Price = Convert.ToDouble(Console.ReadLine());
            Console.Write("Status (true-false: ");
            product.Status = Convert.ToBoolean(Console.ReadLine());

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(api, product).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Add Product Successful");
                }
                else
                {
                    Console.WriteLine("Adding Product Failed or Failed to Connect to Api.");
                }
            }
        }
        public static void Put(string url, string api,int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage message = client.GetAsync(api + "/" + id).Result;

                if (message.IsSuccessStatusCode)
                {
                    var selectproduct = message.Content.ReadAsAsync<Product>().Result;

                    Console.WriteLine("Product Detail");
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Id         : " + selectproduct.Id +
                                        "\nName       : " + selectproduct.Name +
                                        "\nDescription: " + selectproduct.Description +
                                        "\nPrice      : " + selectproduct.Price +
                                        "\nStatus     : " + selectproduct.Status);
                    Console.WriteLine("-------------------------------------");


                    Product product = new Product();
                    product.Id = selectproduct.Id;

                    Console.WriteLine("Edit Product");
                    Console.WriteLine("-------------------------------------");
                    Console.Write("Name        : ");
                    product.Name = Console.ReadLine();
                    Console.Write("Description : ");
                    product.Description = Console.ReadLine();
                    Console.Write("Price       : ");
                    product.Price = Convert.ToDouble(Console.ReadLine());
                    Console.Write("Status (true-false): ");
                    product.Status = Convert.ToBoolean(Console.ReadLine());

                    var response = client.PutAsJsonAsync(api, product).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Edit Product Successful");
                    }
                    else
                    {
                        Console.WriteLine("Editing Product Failed or Failed to Connect to Api.");
                    }
                }
                else
                {
                    Console.WriteLine("Ürün bulunamadı ya da api ile iletişim kurulamadı.");
                }


                
            }
        }
    }
}
